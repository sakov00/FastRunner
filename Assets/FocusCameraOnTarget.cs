#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class FocusCameraOnTarget : MonoBehaviour
{
    public GameObject go;
    public Vector3 camOffset;

#if UNITY_EDITOR
    private void OnEnable()
    {
        // Подписка на событие обновления редактора
        EditorApplication.update += Update;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        // Отписка от события обновления редактора
        EditorApplication.update -= Update;
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void Update()
    {
        // Этот метод будет вызываться в редакторе, но не для событий клавиатуры
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        // Проверка нажатия кнопки "0"
        Event e = Event.current;
        if (e.type == EventType.MouseDown && e.button == 2)
        {
            MoveCameraToMouse();
        }
        if(e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha9)
        {
            MoveMouseToCamera();
        }
    }

    private void MoveCameraToMouse()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        if (sceneView == null)
        {
            Debug.LogWarning("Нет активного окна Scene View.");
            return;
        }

        Camera sceneCamera = sceneView.camera;
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Перемещение камеры к объекту
            Vector3 targetPosition = hit.point;
            go.transform.position = new Vector3(sceneView.camera.transform.position.x, sceneView.camera.transform.position.y, sceneView.camera.transform.position.z);
            go.transform.rotation = sceneView.camera.transform.rotation;
            // Перемещение камеры редактора
            //sceneView.pivot = new Vector3(targetPosition.x, sceneView.pivot.y, targetPosition.z + camOffset.z);
            sceneView.Repaint();
        }
    }

    private void MoveMouseToCamera()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        if (sceneView == null)
        {
            Debug.LogWarning("Нет активного окна Scene View.");
            return;
        }

        //Camera gameCamera = cam;
        if (go == null)
        {
            Debug.LogWarning("Нет основной камеры в сцене.");
            return;
        }

        // Синхронизация позиции и вращения камеры Scene View с игровой камерой
        sceneView.pivot = go.transform.position;
        sceneView.rotation = go.transform.rotation;
        sceneView.size = 10f; // Установка размера камеры Scene View (по желанию)
        sceneView.Repaint();
    }
#endif
}
