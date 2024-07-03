using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

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

        if (GUILayout.Button("Add from folder"))
        {
            script.LoadPrefabsFromFolder();
        }

        // Отображаем текущие префабы (Current Prefabs)
        EditorGUILayout.LabelField("Your current prefabs", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Prefab"))
        {
            PrefabPickerWindow.ShowWindow(script.prefabs, false, script);
        }

        int columnsCurrent = Mathf.FloorToInt((EditorGUIUtility.currentViewWidth - padding) / (thumbnailSize + padding));
        if (columnsCurrent < 1) columnsCurrent = 1;

        int rowsCurrent = Mathf.CeilToInt(script.prefabs.Count / (float)columnsCurrent);

        bool anySelectedCurrent = false; // Флаг для проверки выбора какого-либо префаба

        for (int i = 0; i < rowsCurrent; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < columnsCurrent; j++)
            {
                int index = i * columnsCurrent + j;
                if (index >= script.prefabs.Count)
                    break;

                EditorGUILayout.BeginVertical(GUILayout.Width(thumbnailSize));

                GameObject prefab = script.prefabs[index];
                Texture2D preview = AssetPreview.GetAssetPreview(prefab);

                Rect rect = GUILayoutUtility.GetRect(thumbnailSize, thumbnailSize, GUILayout.ExpandWidth(false));

                // Рисуем рамку вокруг картинки префаба в зависимости от выбранности
                if (script.selectedPrefabIndex == index)
                {
                    Handles.DrawSolidRectangleWithOutline(rect, Color.clear, outlineColorSelected);
                    anySelectedCurrent = true; // Если выбран хотя бы один префаб
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

                // Обработка выбора текущего префаба по щелчку мыши
                if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
                {
                    script.selectedPrefabIndex = index;
                    GUI.changed = true;
                }

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        // Проверяем, выбран ли хотя бы один текущий префаб
        if (!anySelectedCurrent)
        {
            EditorGUILayout.LabelField("Nothing selected", new GUIStyle { normal = new GUIStyleState { textColor = noSelectionColor } });
        }
        else if (script.selectedPrefabIndex >= 0 && script.selectedPrefabIndex < script.prefabs.Count)
        {
            GameObject selectedPrefab = script.prefabs[script.selectedPrefabIndex];
            EditorGUILayout.LabelField(selectedPrefab.name, new GUIStyle { normal = new GUIStyleState { textColor = Color.green } });
        }

        EditorGUILayout.Space();

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
        EditorGUILayout.LabelField("Your new prefabs", EditorStyles.boldLabel);

        if(GUILayout.Button("Add Prefab"))
        {
            PrefabPickerWindow.ShowWindow(script.newPrefabs, true, script);
        }

        int columnsNew = Mathf.FloorToInt((EditorGUIUtility.currentViewWidth - padding) / (thumbnailSize + padding));
        if (columnsNew < 1) columnsNew = 1;

        int rowsNew = Mathf.CeilToInt(script.newPrefabs.Count / (float)columnsNew);

        bool anySelectedNew = false; // Флаг для проверки выбора какого-либо нового префаба

        for (int i = 0; i < rowsNew; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < columnsNew; j++)
            {
                int index = i * columnsNew + j;
                if (index >= script.newPrefabs.Count)
                    break;

                EditorGUILayout.BeginVertical(GUILayout.Width(thumbnailSize));

                GameObject newPrefab = script.newPrefabs[index];
                Texture2D previewNew = AssetPreview.GetAssetPreview(newPrefab);

                Rect rectNew = GUILayoutUtility.GetRect(thumbnailSize, thumbnailSize, GUILayout.ExpandWidth(false));

                // Рисуем рамку вокруг картинки нового префаба в зависимости от выбранности
                if (script.selectedNewPrefabIndex == index)
                {
                    Handles.DrawSolidRectangleWithOutline(rectNew, Color.clear, outlineColorSelected);
                    anySelectedNew = true; // Если выбран хотя бы один новый префаб
                }
                else
                {
                    Handles.DrawSolidRectangleWithOutline(rectNew, Color.clear, outlineColorNotSelected);
                }

                if (previewNew != null)
                {
                    GUI.DrawTexture(rectNew, previewNew, ScaleMode.ScaleToFit);
                }
                else
                {
                    EditorGUI.DrawRect(rectNew, new Color(0f, 0f, 0f, 0.1f));
                    GUI.Label(rectNew, "No Preview", new GUIStyle { alignment = TextAnchor.MiddleCenter });
                }

                // Обработка выбора нового префаба по щелчку мыши
                if (Event.current.type == EventType.MouseDown && rectNew.Contains(Event.current.mousePosition))
                {
                    script.selectedNewPrefabIndex = index;
                    GUI.changed = true;
                }

                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        script.applyScaleOfNewPrefab = EditorGUILayout.Toggle("Apply Scale of New Prefab", script.applyScaleOfNewPrefab);

        // Проверяем, выбран ли хотя бы один новый префаб
        if (!anySelectedNew)
        {
            EditorGUILayout.LabelField("Nothing selected", new GUIStyle { normal = new GUIStyleState { textColor = noSelectionColor } });
        }
        else if (script.selectedNewPrefabIndex >= 0 && script.selectedNewPrefabIndex < script.newPrefabs.Count)
        {
            GameObject selectedNewPrefab = script.newPrefabs[script.selectedNewPrefabIndex];
            EditorGUILayout.LabelField(selectedNewPrefab.name, new GUIStyle { normal = new GUIStyleState { textColor = Color.green } });
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Remove", GUILayout.Width(100f), GUILayout.Height(20f)))
        {
            RemovePrefab();
        }

        // Окно замены префаба
        EditorGUILayout.LabelField("Replace Prefabs", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        // Отображаем текущий префаб (слева)
        EditorGUILayout.BeginVertical();

        if (script.selectedPrefabIndex >= 0 && script.selectedPrefabIndex < script.prefabs.Count)
        {
            GameObject selectedPrefab = script.prefabs[script.selectedPrefabIndex];
            Texture2D previewOld = AssetPreview.GetAssetPreview(selectedPrefab);
            Rect rectOld = GUILayoutUtility.GetRect(thumbnailSize, thumbnailSize, GUILayout.ExpandWidth(false));
            if (previewOld != null)
            {
                GUI.DrawTexture(rectOld, previewOld, ScaleMode.ScaleToFit);
            }
            else
            {
                EditorGUI.DrawRect(rectOld, new Color(0f, 0f, 0f, 0.1f));
                GUI.Label(rectOld, "No Preview", new GUIStyle { alignment = TextAnchor.MiddleCenter });
            }
        }
        else
        {
            EditorGUILayout.LabelField("No Current Prefab Selected", EditorStyles.centeredGreyMiniLabel);
        }

        EditorGUILayout.EndVertical();

        // Рисуем стрелку "→"
        GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        centeredStyle.fixedWidth = 30f;
        GUILayout.Label("→", centeredStyle, GUILayout.Width(30f));

        // Отображаем новый префаб (справа)
        EditorGUILayout.BeginVertical();

        if (script.selectedNewPrefabIndex >= 0 && script.selectedNewPrefabIndex < script.newPrefabs.Count)
        {
            GameObject selectedNewPrefab = script.newPrefabs[script.selectedNewPrefabIndex];
            Texture2D previewNew = AssetPreview.GetAssetPreview(selectedNewPrefab);
            Rect rectNew = GUILayoutUtility.GetRect(thumbnailSize, thumbnailSize, GUILayout.ExpandWidth(false));
            if (previewNew != null)
            {
                GUI.DrawTexture(rectNew, previewNew, ScaleMode.ScaleToFit);
            }
            else
            {
                EditorGUI.DrawRect(rectNew, new Color(0f, 0f, 0f, 0.1f));
                GUI.Label(rectNew, "No Preview", new GUIStyle { alignment = TextAnchor.MiddleCenter });
            }
        }
        else
        {
            EditorGUILayout.LabelField("No New Prefab Selected", EditorStyles.centeredGreyMiniLabel);
        }

        EditorGUILayout.EndVertical();

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

        if (GUI.changed)
        {
            EditorUtility.SetDirty(script);
        }
    }

    // Метод для удаления выбранного префаба из списка текущих префабов
    private void RemoveSelectedPrefab()
    {
        if (script.selectedPrefabIndex >= 0 && script.selectedPrefabIndex < script.prefabs.Count)
        {
            script.prefabs.RemoveAt(script.selectedPrefabIndex);
            script.selectedPrefabIndex = -1; // Сбрасываем индекс выбранного префаба
            GUI.changed = true;
        }
    }

    private void RemovePrefab()
    {
        if (script.selectedNewPrefabIndex >= 0 && script.selectedNewPrefabIndex < script.newPrefabs.Count)
        {
            script.newPrefabs.RemoveAt(script.selectedNewPrefabIndex);
            script.selectedNewPrefabIndex = -1; // Сбрасываем индекс выбранного префаба
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

