using UnityEngine;

namespace ShellTexturedGrass
{
    public class GrassShellGenerator : MonoBehaviour
    {
        private static readonly int ShellIndex = Shader.PropertyToID("_ShellIndex");
        private static readonly int ShellCount = Shader.PropertyToID("_ShellCount");
        [SerializeField] private ShellType shellType;
        [SerializeField] private Material material;
        [SerializeField] private Mesh mesh;
        [SerializeField] private int count;
        [SerializeField] private float diff = 0.0005f;
        [SerializeField] private Vector3 rotation;

        private MaterialPropertyBlock _block;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                foreach (Transform t in transform) Destroy(t.gameObject);
                for (var i = 0; i < count; i++)
                {
                    var t = GenerateShellItem(i).transform;
                    t.SetParent(transform, true);

                    GetTransformMatrix(i, out var pos, out var rot, out var scale);
                    t.SetLocalPositionAndRotation(pos, rot);
                    t.localScale = scale;
                }

                material.SetInt(ShellCount, count);
            }
        }

        private void GetTransformMatrix(int index, out Vector3 position, out Quaternion rot, out Vector3 scale)
        {
            if (shellType == ShellType.VerticalStack)
            {
                position = new Vector3(0, diff * index, 0);
                rot = Quaternion.Euler(rotation);
                scale = Vector3.one;
            }
            else
            {
                position = Vector3.zero;
                rot = Quaternion.Euler(rotation);
                scale = Vector3.one * (1 + diff * index);
            }
        }

        private GameObject GenerateShellItem(int index)
        {
            var go = new GameObject();
            var filter = go.AddComponent<MeshFilter>();
            var rend = go.AddComponent<MeshRenderer>();

            filter.sharedMesh = mesh;
            rend.sharedMaterial = material;

            _block ??= new MaterialPropertyBlock();
            _block.SetInt(ShellIndex, index);
            rend.SetPropertyBlock(_block);
            if (index == 0) go.SetActive(false);
            return go;
        }
    }
}