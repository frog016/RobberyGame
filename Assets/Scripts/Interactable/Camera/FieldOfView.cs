using Entity.Movement;
using Structure.Netcode;
using UnityEngine;

namespace Interactable.Camera
{
    [RequireComponent(typeof(ITransformable))]
    public class FieldOfView : ServerBehaviour
    {
        [SerializeField, Min(0)] private float _viewRadius;
        [SerializeField, Range(0, 360)] private float _viewAngleDegrees;
        [SerializeField, Min(0)] private int _stepCount;
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private MeshSource2D _meshSource;

        private ITransformable _transformable;
        private Mesh _fieldOfViewMesh;

        protected override void OnServerNetworkSpawn()
        {
            _transformable = GetComponent<ITransformable>();

            _fieldOfViewMesh = new Mesh();
            _meshSource.SetMesh(_fieldOfViewMesh);
        }

        protected override void OnServerFixedUpdate()
        {
            var points = CalculateFieldOfViewPoints();
            CalculateFieldOfViewMesh(points);

            _meshSource.UpdateMesh();
        }

        public void SetFieldOfViewVisible(bool visible)
        {
            _meshSource.SetMeshVisible(visible);
        }

        private Vector2[] CalculateFieldOfViewPoints()
        {
            var stepAngle = _viewAngleDegrees / _stepCount;
            var viewPoints = new Vector2[_stepCount + 1];

            var angle = -_viewAngleDegrees / 2;
            for (var i = 0; i < viewPoints.Length; i++)
            {
                var lineCastPoint = LineCast(angle);
                viewPoints[i] = lineCastPoint;

                angle += stepAngle;
            }

            return viewPoints;
        }

        private void CalculateFieldOfViewMesh(Vector2[] points)
        {
            var vertexCount = points.Length + 1;
            var vertices = new Vector3[vertexCount];
            var triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
            for (var i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(points[i]);

                if (i >= vertexCount - 2)
                    continue;

                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 2;
                triangles[i * 3 + 2] = i + 1;
            }

            _fieldOfViewMesh.Clear();

            _fieldOfViewMesh.vertices = vertices;
            _fieldOfViewMesh.triangles = triangles;
            _fieldOfViewMesh.RecalculateNormals();
        }


        private Vector2 LineCast(float angleInDegrees)
        {
            var direction = GetDirectionByAngle(angleInDegrees);
            var hit = Physics2D.Raycast(_transformable.Position, direction, _viewRadius, _obstacleLayer);

            return hit.collider != null
                ? hit.point
                : _transformable.Position + direction * _viewRadius;
        }

        private Vector2 GetDirectionByAngle(float angleInDegrees)
        {
            return Quaternion.Euler(0, 0, angleInDegrees) * _transformable.Rotation;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _viewRadius);

            var leftLine = Quaternion.Euler(0, 0, -_viewAngleDegrees / 2f) * transform.right * _viewRadius;
            var rightLine = Quaternion.Euler(0, 0, _viewAngleDegrees / 2f) * transform.right * _viewRadius;

            Gizmos.DrawLine(transform.position, transform.position + leftLine);
            Gizmos.DrawLine(transform.position, transform.position + rightLine);
        }
    }
}