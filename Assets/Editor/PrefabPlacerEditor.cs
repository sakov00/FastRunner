using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(PrefabPlacer))]
public class PrefabPlacerEditor : Editor
{
    private const float thumbnailSize = 64f;
    private const float padding = 10f;
    private static readonly Color outlineColorSelected = Color.green;
    private static readonly Color outlineColorNotSelected = Color.blue;
    private static readonly Color noSelectionColor = Color.red;
    private PrefabPlacer script;

    public override void OnInspectorGUI()
{
    // Отрисовываем стандартный инспектор
    DrawDefaultInspector();

    // Получаем ссылку на целевой скрипт
    script = (PrefabPlacer)target;

    EditorGUILayout.Space();

    if (GUILayout.Button("Add Prefabs from folder"))
    {
        script.LoadPrefabsFromFolder();
    }

    if (GUILayout.Button("Add Materials from folder"))
    {
        script.LoadMaterialsFromFolder();
    }

    // Отображаем текущие префабы (Current Prefabs)
    DisplayPrefabs(script.prefabs, ref script.selectedPrefabIndex, "Your current prefabs");

    // Кнопка для удаления выбранного префаба из списка текущих префабов
    if (GUILayout.Button("Remove", GUILayout.Width(100f), GUILayout.Height(20f)))
    {
        RemoveSelectedPrefab();
    }

    EditorGUILayout.Space();

    EditorGUILayout.LabelField("Start Rotation", EditorStyles.boldLabel);
    Vector3 eulerRotation = script.prefabStartRotation.eulerAngles;
    eulerRotation = EditorGUILayout.Vector3Field("Rotation", eulerRotation);
    script.prefabStartRotation = Quaternion.Euler(eulerRotation);

    EditorGUILayout.LabelField("Random Rotation Settings", EditorStyles.boldLabel);

    script.RotateRandomX = EditorGUILayout.Toggle("Rotate Random X", script.RotateRandomX);
    if(script.RotateRandomX)
    {
        EditorGUI.BeginDisabledGroup(!script.RotateRandomX);
        script.minRandomAngleX = EditorGUILayout.FloatField("Min Random Angle X", script.minRandomAngleX);
        script.maxRandomAngleX = EditorGUILayout.FloatField("Max Random Angle X", script.maxRandomAngleX);
        EditorGUI.EndDisabledGroup();
    }

    script.RotateRandomY = EditorGUILayout.Toggle("Rotate Random Y", script.RotateRandomY);
    if(script.RotateRandomY)
    {
        EditorGUI.BeginDisabledGroup(!script.RotateRandomY);
        script.minRandomAngleY = EditorGUILayout.FloatField("Min Random Angle Y", script.minRandomAngleY);
        script.maxRandomAngleY = EditorGUILayout.FloatField("Max Random Angle Y", script.maxRandomAngleY);
        EditorGUI.EndDisabledGroup();
    }

    script.RotateRandomZ = EditorGUILayout.Toggle("Rotate Random Z", script.RotateRandomZ);
    if(script.RotateRandomZ)
    {
        EditorGUI.BeginDisabledGroup(!script.RotateRandomZ);
        script.minRandomAngleZ = EditorGUILayout.FloatField("Min Random Angle Z", script.minRandomAngleZ);
        script.maxRandomAngleZ = EditorGUILayout.FloatField("Max Random Angle Z", script.maxRandomAngleZ);
        EditorGUI.EndDisabledGroup();
    }

    EditorGUILayout.LabelField("Start position offset", EditorStyles.boldLabel);
    script.offsetInstancePosition = EditorGUILayout.Vector3Field("Offset", script.offsetInstancePosition);

    // Отображаем новые префабы (New Prefabs)
    DisplayPrefabs(script.newPrefabs, ref script.selectedNewPrefabIndex, "Your new prefabs");

    script.applyScaleOfNewPrefab = EditorGUILayout.Toggle("Apply Scale of New Prefab", script.applyScaleOfNewPrefab);

    // Кнопка для удаления нового префаба из списка новых префабов
    if (GUILayout.Button("Remove New Prefab", GUILayout.Width(150f), GUILayout.Height(20f)))
    {
        RemoveSelectedNewPrefab();
    }

    EditorGUILayout.Space();

    // Окно замены префаба
    EditorGUILayout.LabelField("Replace Prefabs", EditorStyles.boldLabel);

    EditorGUILayout.BeginHorizontal();

    // Отображаем текущий префаб (слева)
    DisplaySelectedPrefab(script.prefabs, script.selectedPrefabIndex, "No Current Prefab Selected");

    // Рисуем стрелку "→"
    GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
    centeredStyle.alignment = TextAnchor.MiddleCenter;
    centeredStyle.fixedWidth = 30f;
    GUILayout.Label("→", centeredStyle, GUILayout.Width(30f));

    // Отображаем новый префаб (справа)
    DisplaySelectedPrefab(script.newPrefabs, script.selectedNewPrefabIndex, "No New Prefab Selected");

    EditorGUILayout.EndHorizontal();

    EditorGUILayout.Space();

    // Кнопка для замены выбранного текущего префаба на выбранный новый префаб по всей сцене
    if (GUILayout.Button("Replace Selected Prefab"))
    {
        script.ReplaceSelectedPrefab();
    }

    if (GUILayout.Button("Revert Prefab Replacements"))
    {
        script.RevertPrefabReplacements();
    }

    // Отображение материалов
    DisplayMaterials();

    // Кнопка для удаления выбранного материала
    if (GUILayout.Button("Remove Material", GUILayout.Width(150f), GUILayout.Height(20f)))
    {
        RemoveSelectedMaterial();
    }

    // Кнопка для открытия окна выбора материалов
    if (GUILayout.Button("Add Material", GUILayout.Width(150f), GUILayout.Height(20f)))
    {
        MaterialsPickerWindow.ShowWindow(script.materials, false, script);  // Открываем окно для добавления материалов в общий список
    }

    if (GUI.changed)
    {
        EditorUtility.SetDirty(script);
    }
}

    private void DisplayPrefabs(List<GameObject> prefabs, ref int selectedIndex, string label)
    {
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

        if (GUILayout.Button("Add Prefab"))
        {
            PrefabPickerWindow.ShowWindow(prefabs, label == "Your new prefabs", script);
        }

        int columns = Mathf.FloorToInt((EditorGUIUtility.currentViewWidth - padding) / (thumbnailSize + padding));
        if (columns < 1) columns = 1;

        int rows = Mathf.CeilToInt(prefabs.Count / (float)columns);

        bool anySelected = false; // Флаг для проверки выбора какого-либо префаба

        for (int i = 0; i < rows; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < columns; j++)
            {
                int index = i * columns + j;
                if (index >= prefabs.Count)
                    break;

                EditorGUILayout.BeginVertical(GUILayout.Width(thumbnailSize));

                GameObject prefab = prefabs[index];
                Texture2D preview = AssetPreview.GetAssetPreview(prefab);

                Rect rect = GUILayoutUtility.GetRect(thumbnailSize, thumbnailSize, GUILayout.ExpandWidth(false));

                // Рисуем рамку вокруг картинки префаба в зависимости от выбранности
                if (selectedIndex == index)
                {
                    Handles.DrawSolidRectangleWithOutline(rect, Color.clear, outlineColorSelected);
                    anySelected = true; // Если выбран хотя бы один префаб
                }
                else
                {
                    Handles.DrawSolidRectangleWithOutline(rect, Color.clear, outlineColorNotSelected);
                }

                if (preview != null)
                {
                    GUI.DrawTexture(rect, preview, ScaleMode.ScaleToFit);
                }
                else
                {
                    EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.1f));
                    GUI.Label(rect, "No Preview", new GUIStyle { alignment = TextAnchor.MiddleCenter });
                }

                // Обработка выбора префаба по щелчку мыши
                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    selectedIndex = index;
                    GUI.changed = true;
                }

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        // Проверяем, выбран ли хотя бы один префаб
        if (!anySelected)
        {
            EditorGUILayout.LabelField("Nothing selected", new GUIStyle { normal = new GUIStyleState { textColor = noSelectionColor } });
        }
        else if (selectedIndex >= 0 && selectedIndex < prefabs.Count)
        {
            GameObject selectedPrefab = prefabs[selectedIndex];
            EditorGUILayout.LabelField(selectedPrefab.name, new GUIStyle { normal = new GUIStyleState { textColor = Color.green } });
        }

        EditorGUILayout.Space();
    }

    private void DisplaySelectedPrefab(List<GameObject> prefabs, int selectedIndex, string noSelectionMessage)
    {
        EditorGUILayout.BeginVertical();

        if (selectedIndex >= 0 && selectedIndex < prefabs.Count)
        {
            GameObject selectedPrefab = prefabs[selectedIndex];
            Texture2D preview = AssetPreview.GetAssetPreview(selectedPrefab);
            Rect rect = GUILayoutUtility.GetRect(thumbnailSize, thumbnailSize, GUILayout.ExpandWidth(false));
            if (preview != null)
            {
                GUI.DrawTexture(rect, preview, ScaleMode.ScaleToFit);
            }
            else
            {
                EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.1f));
                GUI.Label(rect, "No Preview", new GUIStyle { alignment = TextAnchor.MiddleCenter });
            }
        }
        else
        {
            EditorGUILayout.LabelField(noSelectionMessage, EditorStyles.centeredGreyMiniLabel);
        }

        EditorGUILayout.EndVertical();
    }


    private void RemoveSelectedPrefab()
    {
        if (script.selectedPrefabIndex >= 0 && script.selectedPrefabIndex < script.prefabs.Count)
        {
            script.prefabs.RemoveAt(script.selectedPrefabIndex);
            script.selectedPrefabIndex = -1; // Сбрасываем индекс выбранного префаба
            GUI.changed = true;
        }
    }

    private void DisplayMaterials()
    {
        EditorGUILayout.LabelField("Materials", EditorStyles.boldLabel);

        int columns = Mathf.FloorToInt((EditorGUIUtility.currentViewWidth - padding) / (thumbnailSize + padding));
        if (columns < 1) columns = 1;

        int rows = Mathf.CeilToInt(script.materials.Count / (float)columns);

        bool anySelected = false; // Флаг для проверки выбора какого-либо материала

        for (int i = 0; i < rows; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < columns; j++)
            {
                int index = i * columns + j;
                if (index >= script.materials.Count)
                    break;

                EditorGUILayout.BeginVertical(GUILayout.Width(thumbnailSize));

                Material material = script.materials[index];
                Texture2D preview = AssetPreview.GetAssetPreview(material);

                Rect rect = GUILayoutUtility.GetRect(thumbnailSize, thumbnailSize, GUILayout.ExpandWidth(false));

                // Рисуем рамку вокруг картинки материала в зависимости от выбранности
                if (script.selectedMaterialIndex == index)
                {
                    Handles.DrawSolidRectangleWithOutline(rect, Color.clear, outlineColorSelected);
                    anySelected = true; // Если выбран хотя бы один материал
                }
                else
                {
                    Handles.DrawSolidRectangleWithOutline(rect, Color.clear, outlineColorNotSelected);
                }

                if (preview != null)
                {
                    GUI.DrawTexture(rect, preview, ScaleMode.ScaleToFit);
                }
                else
                {
                    EditorGUI.DrawRect(rect, new Color(0f, 0f, 0f, 0.1f));
                    GUI.Label(rect, "No Preview", new GUIStyle { alignment = TextAnchor.MiddleCenter });
                }

                // Обработка выбора материала по щелчку мыши
                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    script.selectedMaterialIndex = index;
                    GUI.changed = true;
                }

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        // Проверяем, выбран ли хотя бы один материал
        if (!anySelected)
        {
            EditorGUILayout.LabelField("Nothing selected", new GUIStyle { normal = new GUIStyleState { textColor = noSelectionColor } });
        }
        else if (script.selectedMaterialIndex >= 0 && script.selectedMaterialIndex < script.materials.Count)
        {
            Material selectedMaterial = script.materials[script.selectedMaterialIndex];
            EditorGUILayout.LabelField(selectedMaterial.name, new GUIStyle { normal = new GUIStyleState { textColor = Color.green } });
        }

        EditorGUILayout.Space();
    }
    private void RemoveSelectedNewPrefab()
    {
        if (script.selectedNewPrefabIndex >= 0 && script.selectedNewPrefabIndex < script.newPrefabs.Count)
        {
            script.newPrefabs.RemoveAt(script.selectedNewPrefabIndex);
            script.selectedNewPrefabIndex = -1; // Сбрасываем индекс выбранного префаба
            GUI.changed = true;
        }
    }

    private void RemoveSelectedMaterial()
    {
        if (script.selectedMaterialIndex >= 0 && script.selectedMaterialIndex < script.materials.Count)
        {
            script.materials.RemoveAt(script.selectedMaterialIndex);
            script.selectedMaterialIndex = -1; // Сбрасываем индекс выбранного материала
            GUI.changed = true;
        }
    }
}


public class PrefabPickerWindow : EditorWindow
{
    private static List<GameObject> prefabList;
    private static bool isNewPrefab;
    private static PrefabPlacer script;
    private Vector2 scrollPosition;

    public static void ShowWindow(List<GameObject> prefabs, bool isNew, PrefabPlacer targetScript)
    {
        prefabList = prefabs;
        isNewPrefab = isNew;
        script = targetScript;

        PrefabPickerWindow window = GetWindow<PrefabPickerWindow>("Prefab Picker");
        window.minSize = new Vector2(300, 400);
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        var guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab != null)
            {
                Texture2D preview = AssetPreview.GetAssetPreview(prefab);

                EditorGUILayout.BeginHorizontal();

                if (preview != null)
                {
                    GUILayout.Label(preview, GUILayout.Width(64), GUILayout.Height(64));
                }
                else
                {
                    EditorGUI.DrawRect(GUILayoutUtility.GetRect(64, 64), new Color(0f, 0f, 0f, 0.1f));
                    GUILayout.Label("No Preview", new GUIStyle { alignment = TextAnchor.MiddleCenter }, GUILayout.Width(64), GUILayout.Height(64));
                }

                if (GUILayout.Button(prefab.name, GUILayout.Height(64)))
                {
                    AddPrefabToList(prefab);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void AddPrefabToList(GameObject prefab)
    {
        if (prefab == null) return;

        if (isNewPrefab)
        {
            // Проверяем, есть ли уже этот префаб в списке новых префабов
            if (script.newPrefabs.Contains(prefab))
            {
                EditorUtility.DisplayDialog("Префаб уже добавлен", $"{prefab.name} уже находится в списке новых префабов.", "ОК");
                return;
            }

            script.newPrefabs.Add(prefab);
            script.selectedNewPrefabIndex = script.newPrefabs.Count - 1;
        }
        else
        {
            // Проверяем, есть ли уже этот префаб в списке текущих префабов
            if (script.prefabs.Contains(prefab))
            {
                EditorUtility.DisplayDialog("Префаб уже добавлен", $"{prefab.name} уже находится в списке текущих префабов.", "ОК");
                return;
            }

            script.prefabs.Add(prefab);
            script.selectedPrefabIndex = script.prefabs.Count - 1;
        }

        Close();
    }
}


public class MaterialsPickerWindow : EditorWindow
{
    private static List<Material> materialList;
    private static PrefabPlacer script;
    private static bool isNewMaterial;
    private Vector2 scrollPosition;

    public static void ShowWindow(List<Material> materials, bool isNew, PrefabPlacer targetScript)
    {
        materialList = materials;
        isNewMaterial = isNew;
        script = targetScript;

        MaterialsPickerWindow window = GetWindow<MaterialsPickerWindow>("Materials Picker");
        window.minSize = new Vector2(300, 400);
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Select Material", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Сортируем материалы по имени в алфавитном порядке
        var guids = AssetDatabase.FindAssets("t:Material");
        var materials = guids
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Select(path => AssetDatabase.LoadAssetAtPath<Material>(path))
            .Where(material => material != null)
            .OrderBy(material => material.name)  // Сортируем материалы по имени
            .ToList();

        foreach (var material in materials)
        {
            EditorGUILayout.BeginHorizontal();

            // Отображаем только название материала
            EditorGUILayout.LabelField(material.name, GUILayout.Width(200));

            if (GUILayout.Button("Add", GUILayout.Width(60)))
            {
                AddMaterialToList(material);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }

    private void AddMaterialToList(Material material)
    {
        if (material == null) return;

        if (isNewMaterial)
        {
            // Проверяем, есть ли уже этот материал в списке новых материалов
            if (script.materials.Contains(material))
            {
                EditorUtility.DisplayDialog("Material Already Added", $"{material.name} is already in the new materials list.", "OK");
                return;
            }

            script.materials.Add(material);
            script.selectedMaterialIndex = script.materials.Count - 1;
        }
        else
        {
            // Проверяем, есть ли уже этот материал в списке материалов
            if (script.materials.Contains(material))
            {
                EditorUtility.DisplayDialog("Material Already Added", $"{material.name} is already in the materials list.", "OK");
                return;
            }

            script.materials.Add(material);
            script.selectedMaterialIndex = script.materials.Count - 1;
        }

        Close();
    }
}


