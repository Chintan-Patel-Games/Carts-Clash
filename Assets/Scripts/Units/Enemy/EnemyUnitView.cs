using CartClash.Grid;
using CartClash.Units.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Enemy
{
    public class EnemyUnitView : MonoBehaviour, IUnitView
    {
        private List<GridNode> path;
        private int pathIndex;
        private float moveSpeed;
        private bool isMoving;
        private Quaternion modelRotationOffset = Quaternion.Euler(0f, 90f, 0f);

        [SerializeField] private float rotationSpeed = 720;

        public void MoveAlongPath(List<GridNode> newPath, float speed)
        {
            path = newPath;
            moveSpeed = speed;
            pathIndex = 0;
            isMoving = true;

            transform.position = GridToWorld(path[0]);

            if (path.Count > 1)
                RotateTowards(GridToWorld(path[1]));
        }

        public void Update()
        {
            if (!isMoving || path == null) return;

            Vector3 targetPos = GridToWorld(path[pathIndex]);

            RotateTowards(targetPos);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                pathIndex++;
                if (pathIndex >= path.Count) isMoving = false;
                FaceForward();
            }
        }

        private void RotateTowards(Vector3 targetPos)
        {
            Vector3 direction = targetPos - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction) * modelRotationOffset;

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        private void FaceForward()
        {
            Quaternion forwardDirection = Quaternion.LookRotation(transform.forward);
            transform.rotation = forwardDirection;
        }

        public bool IsMovingComplete() => !isMoving;

        public void SetPosition(GridNode node) =>
            transform.position = new Vector3(node.x, 0, node.y);

        private Vector3 GridToWorld(GridNode node) => new Vector3(node.x, 0, node.y);
    }
}