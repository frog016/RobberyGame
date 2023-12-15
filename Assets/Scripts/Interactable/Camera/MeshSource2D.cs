using System.Linq;
using UnityEngine;

namespace Interactable.Camera
{
    public class MeshSource2D : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private PolygonCollider2D _polygonCollider;

        private Mesh _mesh;

        public void SetMesh(Mesh mesh)
        {
            _mesh = mesh;
            _meshFilter.mesh = mesh;

            SetPolygonColliderPointsFromMesh();
        }

        public void UpdateMesh()
        {
            SetPolygonColliderPointsFromMesh();
        }

        public void SetMeshVisible(bool visible)
        {
            var mesh = visible ? _mesh : null;
            _meshFilter.mesh = mesh;
        }

        private void SetPolygonColliderPointsFromMesh()
        {
            _polygonCollider.points = _mesh.vertices
                .Select(vertex => (Vector2)vertex)
                .ToArray();
        }
    }
}