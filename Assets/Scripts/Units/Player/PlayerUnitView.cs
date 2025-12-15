using CartClash.Grid;
using CartClash.Units.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace CartClash.Units.Player
{
    public class PlayerUnitView : MonoBehaviour, IUnitView
    {
        private List<GridNode> path;
        private int pathIndex;
        private float moveSpeed;
        private bool isMoving;

        public void MoveAlongPath(List<GridNode> newPath, float speed)
        {
            path = newPath;
            moveSpeed = speed;
            pathIndex = 0;
            isMoving = true;

            transform.position = GridToWorld(path[0]);
        }

        public void Update()
        {
            if (!isMoving || path == null) return;

            Vector3 targetPos = GridToWorld(path[pathIndex]);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            {
                pathIndex++;
                if (pathIndex >= path.Count) isMoving = false;
            }
        }

        public bool IsMovingComplete() => !isMoving;

        public void SetPosition(GridNode node) =>
            transform.position = new Vector3(node.x, 0, node.y);

        private Vector3 GridToWorld(GridNode node) => new Vector3(node.x, 0, node.y);
    }
}