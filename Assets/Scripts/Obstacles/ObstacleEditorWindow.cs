using UnityEditor;
using UnityEngine;

namespace CartClash.Obstacles
{
    public class ObstacleEditorWindow : EditorWindow
    {
        private ObstacleSO obstacleSO;

        [MenuItem("CartClash/Obstacle Editor")]
        public static void ShowWindow()
        {
            GetWindow<ObstacleEditorWindow>("Obstacle Editor");
        }

        private void OnGUI()
        {
            obstacleSO = (ObstacleSO)EditorGUILayout.ObjectField("Obstacle SO", obstacleSO, typeof(ObstacleSO), false);

            if (obstacleSO == null) return;

            if (obstacleSO.blockedTiles == null || obstacleSO.blockedTiles.Length != obstacleSO.width * obstacleSO.height)
            {
                if (GUILayout.Button("Initialize Obstacle"))
                    obstacleSO.Initialize();
                return;
            }

            EditorGUILayout.Space();

            for (int y = obstacleSO.height - 1; y >= 0; y--)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < obstacleSO.width; x++)
                {
                    bool current = obstacleSO.IsBlocked(x, y);
                    bool updated = GUILayout.Toggle(current, "");

                    if (updated != current)
                    {
                        Undo.RecordObject(obstacleSO, "Toggle Blocked Tile");
                        obstacleSO.SetBlocked(x, y, updated);
                        EditorUtility.SetDirty(obstacleSO);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}