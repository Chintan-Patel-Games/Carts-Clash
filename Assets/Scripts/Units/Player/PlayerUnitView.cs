using CartClash.PathFinding;
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

        public bool IsMovingComplete() => !isMoving;

        public void SetPosition(GridNode node) =>
            transform.position = new Vector3(node.x, 0, node.y);

        public void MoveAlongPath(List<GridNode> newPath, float speed)
        {
            path = newPath;
            moveSpeed = speed;
            pathIndex = 0;
            isMoving = true;
        }

        public void Update()
        {
            if (!isMoving || path == null) return;

            Vector3 target = new Vector3(path[pathIndex].x, 0, path[pathIndex].y);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.05f)
            {
                pathIndex++;
                if (pathIndex >= path.Count) isMoving = false;
            }
        }
    }
}