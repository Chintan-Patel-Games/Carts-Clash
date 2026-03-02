using UnityEditor;
using UnityEngine;
using CartClash.Obstacles.SO;

namespace CartClash.Obstacles
{
    /// <summary>
    /// Provides a custom Unity Editor window for viewing and editing the blocked tiles of an ObstacleSO asset.
    /// </summary>
    public class ObstacleEditorWindow : EditorWindow
    {
        private ObstacleSO obstacleSO;

        [MenuItem("CartClash/Obstacle Editor")]
        public static void ShowWindow() => GetWindow<ObstacleEditorWindow>("Obstacle Editor");

        /// <summary>
        /// Draws and manages the custom inspector GUI for editing the properties of the associated ObstacleSO asset in
        /// the Unity Editor.
        /// </summary>
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