using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class PrefabPlacer : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<GameObject> newPrefabs;
    [HideInInspector]
    public int selectedPrefabIndex = -1;
    [HideInInspector]
    public int selectedNewPrefabIndex = -1;
    [HideInInspector]
    public Vector3 offsetInstancePosition;
    private bool placingPrefab = false;
    [HideInInspector]
    public bool RotateRandomX;
    [HideInInspector]
    public float minRandomAngleX;
    [HideInInspector]
    public float maxRandomAngleX;
    [HideInInspector]
    public bool RotateRandomY;
    [HideInInspector]
    public float minRandomAngleY;
    [HideInInspector]
    public float maxRandomAngleY;
    [HideInInspector]
    public bool RotateRandomZ;
    [HideInInspector]
    public float minRandomAngleZ;
    [HideInInspector]
    public float maxRandomAngleZ;
    private float randomAngleX;
    private float randomAngleY;
    private float randomAngleZ;
    [HideInInspector]
    public Quaternion prefabStartRotation;

    [System.Serializable]
    public struct PrefabReplacementInfo
    {
        public GameObject originalPrefab;
        public GameObject newPrefab;
        public Transform newTransform;
    }

    [HideInInspector]
    public List<PrefabReplacementInfo> replacements = new List<PrefabReplacementInfo>();

    public List<GameObject> parentObjects;
    [HideInInspector]
    public bool applyScaleOfNewPrefab = true;

    public bool loadPrefabsFromFolder = false; // Flag to load prefabs from folder
    public bool clearCurrenPrefabsListBeforAddFromFolder;
    public string folderPath; // Folder path for prefabs

#if UNITY_EDITOR
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void Update()
    {
        if (placingPrefab && Event.current != null && Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            PlacePrefab();
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.KeyDown && currentEvent.keyCode == KeyCode.P)
        {
            placingPrefab = !placingPrefab;
            if (placingPrefab)
            {
                Debug.Log("Prefab placement mode enabled. Left click to place.");
            }
            else
            {
                Debug.Log("Prefab placement mode disabled.");
            }
        }

        if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
        {
            if (placingPrefab)
            {
                PlacePrefab();
                currentEvent.Use(); // Mark the event as used to prevent Unity from processing it further
            }
        }

        if (currentEvent.type == EventType.KeyDown && currentEvent.keyCode == KeyCode.R)
        {
            ReplaceSelectedPrefab();
        }

        if (currentEvent.type == EventType.KeyDown && currentEvent.keyCode == KeyCode.T)
        {
            RevertPrefabReplacements();
            UpdateCurrentPrefabs();
        }
    }

    private void PlacePrefab()
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (selectedPrefabIndex >= 0 && selectedPrefabIndex < prefabs.Count && prefabs[selectedPrefabIndex] != null)
            {
                GameObject prefabToPlace = prefabs[selectedPrefabIndex];
                GameObject newObject = PrefabUtility.InstantiatePrefab(prefabToPlace) as GameObject;
                newObject.transform.position = hit.point + offsetInstancePosition;
                if (RotateRandomX)
                {
                    randomAngleX = Random.Range(minRandomAngleX, maxRandomAngleX);
                }
                else
                {
                    randomAngleX = 0;
                }
                if (RotateRandomY)
                {
                    randomAngleY = Random.Range(minRandomAngleY, maxRandomAngleY);
                }
                else
                {
                    randomAngleY = 0;
                }
                if (RotateRandomZ)
                {
                    randomAngleZ = Random.Range(minRandomAngleZ, maxRandomAngleZ);
                }
                else
                {
                    randomAngleZ = 0;
                }
                Quaternion randomRot = Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ);
                newObject.transform.rotation = prefabStartRotation * randomRot;
                Undo.RegisterCreatedObjectUndo(newObject, "Place Prefab");
                Debug.Log("Prefab placed at: " + hit.point);

                // Добавляем новый префаб к родительскому объекту на основе совпадения по словам
                if (parentObjects != null && parentObjects.Count > 0)
                {
                    foreach (GameObject parent in parentObjects)
                    {
                        if (parent != null && NameContainsWords(newObject.name, parent.name))
                        {
                            newObject.transform.SetParent(parent.transform);
                            Debug.Log($"Prefab '{newObject.name}' added to parent object '{parent.name}'.");
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("No prefab selected or prefab not assigned.");
            }
        }

        Event.current.Use();
    }

    public void ReplaceSelectedPrefab()
    {
        if (selectedPrefabIndex >= 0 && selectedPrefabIndex < prefabs.Count && prefabs[selectedPrefabIndex] != null &&
            selectedNewPrefabIndex >= 0 && selectedNewPrefabIndex < newPrefabs.Count && newPrefabs[selectedNewPrefabIndex] != null)
        {
            GameObject oldPrefab = prefabs[selectedPrefabIndex];
            GameObject newPrefab = newPrefabs[selectedNewPrefabIndex];

            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

            bool prefabFound = false;

            foreach (GameObject obj in allObjects)
            {
                PrefabInstanceStatus status = PrefabUtility.GetPrefabInstanceStatus(obj);

                if (status == PrefabInstanceStatus.Connected && PrefabUtility.GetCorrespondingObjectFromSource(obj) == oldPrefab)
                {
                    prefabFound = true;
                    break;
                }
            }

            if (!prefabFound)
            {
                Debug.LogWarning("No instances of the selected prefab found on the scene.");
                return;
            }

            replacements.Clear();

            foreach (GameObject obj in allObjects)
            {
                PrefabInstanceStatus status = PrefabUtility.GetPrefabInstanceStatus(obj);

                if (status == PrefabInstanceStatus.Connected && PrefabUtility.GetCorrespondingObjectFromSource(obj) == oldPrefab)
                {
                    GameObject newObject = PrefabUtility.InstantiatePrefab(newPrefab) as GameObject;
                    newObject.transform.position = obj.transform.position + offsetInstancePosition;
                    newObject.transform.rotation = obj.transform.rotation;
                    newObject.transform.localScale = applyScaleOfNewPrefab ? newPrefab.transform.localScale : obj.transform.localScale; // Применение масштаба нового префаба

                    PrefabReplacementInfo replacementInfo = new PrefabReplacementInfo
                    {
                        originalPrefab = oldPrefab,
                        newPrefab = newPrefab,
                        newTransform = newObject.transform // Сохраняем трансформ нового префаба
                    };

                    // Добавляем новый префаб к родительскому объекту на основе совпадения по словам
                    if (parentObjects != null && parentObjects.Count > 0)
                    {
                        foreach (GameObject parent in parentObjects)
                        {
                            if (parent != null && NameContainsWords(newObject.name, parent.name))
                            {
                                newObject.transform.SetParent(parent.transform);
                                Debug.Log($"Prefab '{newObject.name}' added to parent object '{parent.name}'.");
                                break;
                            }
                        }
                    }

                    replacements.Add(replacementInfo);

                    Undo.RegisterCreatedObjectUndo(newObject, "Replace Prefab");

                    DestroyImmediate(obj);
                }
            }

            prefabs[selectedPrefabIndex] = newPrefab;

            Debug.Log("Objects with old prefab '" + oldPrefab.name + "' replaced with new prefab '" + newPrefab.name + "' throughout the scene.");
        }
        else
        {
            Debug.LogWarning("Please select both an old prefab and a new prefab to perform the replacement.");
        }
    }

    public void RevertPrefabReplacements()
    {
        int step = 0;
        foreach (PrefabReplacementInfo replacement in replacements)
        {
            GameObject restoredObject = PrefabUtility.InstantiatePrefab(replacement.originalPrefab) as GameObject;
            restoredObject.transform.position = replacement.newTransform.position - offsetInstancePosition;
            restoredObject.transform.rotation = replacement.newTransform.rotation;
            restoredObject.transform.localScale = replacement.newTransform.localScale;

            Undo.RegisterCreatedObjectUndo(restoredObject, "Revert Replace Prefab");

            // Удаляем новый объект
            if (replacement.newTransform != null)
            {
                DestroyImmediate(replacement.newTransform.gameObject);
            }
            if (step == 0)
            {
                prefabs.Add(replacement.originalPrefab);
                prefabs.Remove(replacement.newPrefab);
                step++;
            }
            // Добавляем восстановленный префаб к родительскому объекту на основе совпадения по словам
            if (parentObjects != null && parentObjects.Count > 0)
            {
                foreach (GameObject parent in parentObjects)
                {
                    if (parent != null && NameContainsWords(restoredObject.name, parent.name))
                    {
                        restoredObject.transform.SetParent(parent.transform);
                        Debug.Log($"Prefab '{restoredObject.name}' added to parent object '{parent.name}'.");
                        break;
                    }
                }
            }
        }

        replacements.Clear();

        Debug.Log("Reverted prefab replacements to their original state.");
    }

    private void UpdateCurrentPrefabs()
    {
        List<GameObject> currentPrefabs = new List<GameObject>();

        foreach (PrefabReplacementInfo replacement in replacements)
        {
            currentPrefabs.Add(replacement.originalPrefab);
        }

        prefabs = currentPrefabs;

        Debug.Log("Updated current prefabs list after revert.");
    }

    public void LoadPrefabsFromFolder()
    {
        if (!string.IsNullOrEmpty(folderPath) && loadPrefabsFromFolder)
        {
            string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { folderPath });
            if (clearCurrenPrefabsListBeforAddFromFolder)
            {
                prefabs.Clear();
            }
            foreach (string guid in prefabGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null && !prefabs.Contains(prefab))
                {
                    prefabs.Add(prefab);
                }
            }
        }
    }

    private bool NameContainsWords(string name, string words)
    {
        string[] nameParts = name.ToLower().Split('_');
        string[] wordsParts = words.ToLower().Split('_');

        return nameParts.Any(np => wordsParts.Any(wp => wp.Contains(np) || np.Contains(wp)));
    }
#endif
}
