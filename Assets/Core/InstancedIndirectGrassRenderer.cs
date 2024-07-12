using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Profiling;

[ExecuteInEditMode]
public class InstancedIndirectGrassRenderer : MonoBehaviour
{
    [Header("Settings")]
    public float drawDistance = 500; // это настройка сильно влияет на производительность!
    public Material instanceMaterial;

    [Header("Internal")]
    public ComputeShader cullingComputeShader;

    [NonSerialized]
    public List<Vector3> allGrassPos = new List<Vector3>(); // пользователь должен обновлять этот список с помощью C#
    //=====================================================
    [HideInInspector]
    public static InstancedIndirectGrassRenderer instance; // глобальная ссылка на этот скрипт

    private int cellCountX = -1;
    private int cellCountZ = -1;
    private int cellCountY = -1;
    private int dispatchCount = -1;

    // чем меньше число, тем больше времени потребуется на ЦП, но ГП будет быстрее
    public float cellSizeX = 100; // единица Unity (м)
    public float cellSizeZ = 150; // единица Unity (м)
    //private float cellSizeY = 10;

    private int instanceCountCache = -1;
    public Mesh cachedGrassMesh;

    private ComputeBuffer allInstancesPosWSBuffer;
    private ComputeBuffer visibleInstancesOnlyPosWSIDBuffer;
    private ComputeBuffer argsBuffer;

    private List<Vector3>[] cellPosWSsList; // для бинирования: бинирование поместит каждый posWS в правильную ячейку
    private float minX, minY, minZ, maxX, maxY, maxZ;
    private List<int> visibleCellIDList = new List<int>();
    private Plane[] cameraFrustumPlanes = new Plane[6];
    public Camera cam;
    public GameObject camGo;
    bool shouldBatchDispatch = true;
    //=====================================================
    void OnEnable() 
    {
        instance = this;   
    }

    private void Start() 
    {
        camGo.SetActive(false);
        cam = Camera.main;    
    }

    void LateUpdate()
    {
        if(cam == null)
        {
            camGo.SetActive(true);
            cam = camGo.GetComponent<Camera>();
        }
        if(cam != null)
        {
            UpdateAllInstanceTransformBufferIfNeeded();
        //=====================================================================================================
        // грубое быстрое обрезание камеры в ЦП
        //=====================================================================================================
        visibleCellIDList.Clear(); // заполните этот список идентификаторов ячеек с помощью ЦП
        //cam = Camera.main;

        // выполнить отсечение по камере, используя границы ячеек
        
        float cameraOriginalFarPlane = cam.farClipPlane;
        cam.farClipPlane = drawDistance; // позволяет управлять drawDistance
        GeometryUtility.CalculateFrustumPlanes(cam, cameraFrustumPlanes); // Порядок: [0] = Влево, [1] = Вправо, [2] = Вниз, [3] = Вверх, [4] = Ближняя, [5] = Дальняя
        cam.farClipPlane = cameraOriginalFarPlane; // вернуть исходный дальний план камеры

        // медленный цикл
        // TODO: заменить этот цикл на quadtree?
        // TODO: преобразовать этот цикл в job+burst? (UnityException: TestPlanesAABB can only be called from the main thread.)
        Profiler.BeginSample("CPU cell frustum culling (heavy)");
        
        for (int i = 0; i < cellPosWSsList.Length; i++)
        {
            Vector3 centerPosWS = new Vector3(i % cellCountX + 0.5f, 0, i / cellCountX + 0.5f);
            centerPosWS.x = Mathf.Lerp(minX, maxX, centerPosWS.x / cellCountX);
            //centerPosWS.y = (minY + maxY) / 2;
            centerPosWS.z = Mathf.Lerp(minZ, maxZ, centerPosWS.z / cellCountZ);

            Vector3 sizeWS = new Vector3(
                Mathf.Abs(maxX - minX) / cellCountX, 
                0, 
                Mathf.Abs(maxZ - minZ) / cellCountZ
            );

            Bounds cellBound = new Bounds(centerPosWS, sizeWS);

            // Проверка пересечения с фрустумом камеры
            bool isVisible = GeometryUtility.TestPlanesAABB(cameraFrustumPlanes, cellBound);

            // Дополнительная проверка для нижней плоскости
            if (!isVisible)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (cameraFrustumPlanes[j].GetDistanceToPoint(cellBound.min) > 0 ||
                        cameraFrustumPlanes[j].GetDistanceToPoint(cellBound.max) > 0)
                    {
                        isVisible = true;
                        break;
                    }
                }
            }

            if (isVisible)
            {
                visibleCellIDList.Add(i);
            }
    }
        Profiler.EndSample();

        //=====================================================================================================
        // затем перебирайте только видимые ячейки, каждая видимая ячейка отправляет задание GPU culling один раз
        // в конце шейдер вычислит все видимые экземпляры в visibleInstancesOnlyPosWSIDBuffer
        //=====================================================================================================
        Matrix4x4 v = cam.worldToCameraMatrix;
        Matrix4x4 p = cam.projectionMatrix;
        Matrix4x4 vp = p * v ;

        visibleInstancesOnlyPosWSIDBuffer.SetCounterValue(0);

        // установить один раз
        cullingComputeShader.SetMatrix("_VPMatrix", vp);
        cullingComputeShader.SetFloat("_MaxDrawDistance", drawDistance);

        // диспетчеризация для каждой видимой ячейки
        dispatchCount = 0;
        for (int i = 0; i < visibleCellIDList.Count; i++)
        {
            int targetCellFlattenID = visibleCellIDList[i];
            int memoryOffset = 0;
            for (int j = 0; j < targetCellFlattenID; j++)
            {
                memoryOffset += cellPosWSsList[j].Count;
            }
            cullingComputeShader.SetInt("_StartOffset", memoryOffset); // чтение данных culling началось с смещенной позиции, начнется с общего смещения ячейки в памяти
            int jobLength = cellPosWSsList[targetCellFlattenID].Count;

            // батч n диспетчеризирует в 1 диспетч, если память непрерывна в allInstancesPosWSBuffer
            if(shouldBatchDispatch)
            {
                while ((i < visibleCellIDList.Count - 1) && 
                        (visibleCellIDList[i + 1] == visibleCellIDList[i] + 1))
                {
                    jobLength += cellPosWSsList[visibleCellIDList[i + 1]].Count;
                    i++;
                }
            }
            if(jobLength > 0)
            {
                // диспетчеризация для отсеченной ячейки
                cullingComputeShader.Dispatch(0, Mathf.CeilToInt(jobLength / 64f), 1, 1); // количество делений disaptch.X должно соответствовать numthreads.x в compute shader (например, 64)
                dispatchCount++;
            }
        }

        //====================================================================================
        // окончательный DrawMeshInstancedIndirect draw call
        //====================================================================================
        // ГП окончательно обрезает по индексу, копирует количество видимых в argsBuffer, чтобы настроить количество рисуемых элементов DrawMeshInstancedIndirect
        ComputeBuffer.CopyCount(visibleInstancesOnlyPosWSIDBuffer, argsBuffer, 4);

        // Рендеринг одного большого вызова отрисовки с DrawMeshInstancedIndirect
        Bounds renderBound = new Bounds();
        renderBound.SetMinMax(new Vector3(minX, 0, minZ), new Vector3(maxX, 0, maxZ)); // если область отсечения камеры не пересекается с этой границей, DrawMeshInstancedIndirect даже не будет отображать
        Graphics.DrawMeshInstancedIndirect(GetGrassMeshCache(), 0, instanceMaterial, renderBound, argsBuffer);

        
        }

        
    }


    void OnDisable()
    {
        // освободить все буферы вычислений
        if (allInstancesPosWSBuffer != null)
            allInstancesPosWSBuffer.Release();
        allInstancesPosWSBuffer = null;

        if (visibleInstancesOnlyPosWSIDBuffer != null)
            visibleInstancesOnlyPosWSIDBuffer.Release();
        visibleInstancesOnlyPosWSIDBuffer = null;

        if (argsBuffer != null)
            argsBuffer.Release();
        argsBuffer = null;

        instance = null;
    }

    Mesh GetGrassMeshCache()
    {
        if (!cachedGrassMesh)
        {
            // если не существует, создаем треугольную сетку травы с 3 вершинами
            cachedGrassMesh = new Mesh();

            // одна травинка (вершины)
            Vector3[] verts = new Vector3[3];
            verts[0] = new Vector3(-0.25f, 0);
            verts[1] = new Vector3(+0.25f, 0);
            verts[2] = new Vector3(-0.0f, 1);
            // одна травинка (индексы треугольников)
            int[] trinagles = new int[3] { 2, 1, 0, }; // чтобы соответствовать Cull Back в grass shader

            cachedGrassMesh.SetVertices(verts);
            cachedGrassMesh.SetTriangles(trinagles, 0);
        }

        return cachedGrassMesh;
    }

    void UpdateAllInstanceTransformBufferIfNeeded()
    {
        // всегда обновлять
        instanceMaterial.SetVector("_PivotPosWS", transform.position);
        instanceMaterial.SetVector("_BoundSize", new Vector2(transform.localScale.x, transform.localScale.z));

        // предварительный выход, если не нужно обновлять буфер
        if (instanceCountCache == allGrassPos.Count &&
            argsBuffer != null &&
            allInstancesPosWSBuffer != null &&
            visibleInstancesOnlyPosWSIDBuffer != null)
            {
                return;
            }

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////


        ///////////////////////////
        // буфер allInstancesPosWSBuffer
        ///////////////////////////
        if (allInstancesPosWSBuffer != null)
            allInstancesPosWSBuffer.Release();
        allInstancesPosWSBuffer = new ComputeBuffer(allGrassPos.Count, sizeof(float)*3); // float3 posWS только, на каждую травинку

        if (visibleInstancesOnlyPosWSIDBuffer != null)
            visibleInstancesOnlyPosWSIDBuffer.Release();
        visibleInstancesOnlyPosWSIDBuffer = new ComputeBuffer(allGrassPos.Count, sizeof(uint), ComputeBufferType.Append); // только uint, на каждую видимую травинку

        // найти минимальные и максимальные значения позиций для всех экземпляров
        minX = float.MaxValue;
        //minY = float.MaxValue;
        minZ = float.MaxValue;
        maxX = float.MinValue;
        //maxY = float.MinValue;
        maxZ = float.MinValue;
        for (int i = 0; i < allGrassPos.Count; i++)
        {
            Vector3 target = allGrassPos[i];
            minX = Mathf.Min(target.x, minX);
            //minY = Mathf.Min(target.y, minY); // добавьте Y
            minZ = Mathf.Min(target.z, minZ);
            maxX = Mathf.Max(target.x, maxX);
            //maxY = Mathf.Max(target.y, maxY); // добавьте Y
            maxZ = Mathf.Max(target.z, maxZ);
        }

        // решите cellCountX,Z здесь, используя min и max
        // каждая ячейка имеет размер cellSizeX x cellSizeZ
        cellCountX = Mathf.CeilToInt((maxX - minX) / cellSizeX); 
        cellCountZ = Mathf.CeilToInt((maxZ - minZ) / cellSizeZ);
        //cellCountY = Mathf.CeilToInt((maxY - minY) / cellSizeY);

        // инициализация позиций в каждой ячейке
        cellPosWSsList = new List<Vector3>[cellCountX * cellCountZ]; // 2D массив
        for (int i = 0; i < cellPosWSsList.Length; i++)
        {
            cellPosWSsList[i] = new List<Vector3>();
        }

        // бинирование, поместите каждую позицию в правильную ячейку
        for (int i = 0; i < allGrassPos.Count; i++)
        {
             Vector3 pos = allGrassPos[i];

            // найти ID ячейки
            int xID = Mathf.Min(cellCountX-1, Mathf.FloorToInt(Mathf.InverseLerp(minX, maxX, pos.x) * cellCountX)); // используйте min, чтобы заставить оставаться в пределах 0 ~ [cellCountX-1]  
            //int yID = Mathf.Min(cellCountY-1, Mathf.FloorToInt(Mathf.InverseLerp(minY, maxY, pos.y) * cellCountY)); // используйте min, чтобы заставить оставаться в пределах 0 ~ [cellCountY-1]  
            int zID = Mathf.Min(cellCountZ-1, Mathf.FloorToInt(Mathf.InverseLerp(minZ, maxZ, pos.z) * cellCountZ)); // используйте min, чтобы заставить оставаться в пределах 0 ~ [cellCountZ-1]

            cellPosWSsList[xID + zID * cellCountX].Add(pos);
        }

        // объединить в один плоский массив для буфера вычислений
        int offset = 0;
        Vector3[] allGrassPosWSSortedByCell = new Vector3[allGrassPos.Count];
        for (int i = 0; i < cellPosWSsList.Length; i++)
        {
            for (int j = 0; j < cellPosWSsList[i].Count; j++)
            {
                allGrassPosWSSortedByCell[offset] = cellPosWSsList[i][j];
                offset++;
            }
        }

        allInstancesPosWSBuffer.SetData(allGrassPosWSSortedByCell);
        instanceMaterial.SetBuffer("_AllInstancesTransformBuffer", allInstancesPosWSBuffer);
        instanceMaterial.SetBuffer("_VisibleInstanceOnlyTransformIDBuffer", visibleInstancesOnlyPosWSIDBuffer);

        ///////////////////////////
        // буфер аргументов для непосредственных рисунков
        ///////////////////////////
        if (argsBuffer != null)
            argsBuffer.Release();
        uint[] args = new uint[5] { 0, 0, 0, 0, 0 };
        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);

        args[0] = (uint)GetGrassMeshCache().GetIndexCount(0);
        args[1] = (uint)allGrassPos.Count;
        args[2] = (uint)GetGrassMeshCache().GetIndexStart(0);
        args[3] = (uint)GetGrassMeshCache().GetBaseVertex(0);
        args[4] = 0;

        argsBuffer.SetData(args);

        ///////////////////////////
        // Обновление кэша
        ///////////////////////////
        // обновить кэш, чтобы предотвратить будущее бесполезное обновление буфера, которое тратит производительность
        instanceCountCache = allGrassPos.Count;


        // установить буфер
        cullingComputeShader.SetBuffer(0, "_AllInstancesPosWSBuffer", allInstancesPosWSBuffer);
        cullingComputeShader.SetBuffer(0, "_VisibleInstancesOnlyPosWSIDBuffer", visibleInstancesOnlyPosWSIDBuffer);
    }
}