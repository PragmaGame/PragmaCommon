using UnityEditor;
using UnityEngine;

namespace PragmaCommon.Editor.AutoSave
{
    public class AutoSavePopup : EditorWindow
    {
        private const string TITLE = "Auto Save Popup";
        
        private static readonly Vector2 _size = new(200, 80);
        
        private static AutoSavePopup _window;
        
        public static void ShowWindow()
        {
            _window = (AutoSavePopup) GetWindow(typeof(AutoSavePopup), true, TITLE);

            _window.minSize = _size;
            _window.maxSize = _size;

            var main = EditorGUIUtility.GetMainWindowPosition();

            var pos = _window.position;
            
            var centerWidth = (main.width - pos.width) * 0.5f;
            var centerHeight = (main.height - pos.height) * 0.5f;
            
            pos.x = main.x + centerWidth;
            pos.y = main.y + centerHeight;
            
            _window.position = pos;
            
            _window.Show();
        }
        
        public static void CloseWindow()
        {
            _window.Close();
        }

        private void OnGUI()
        {
            GUILayout.Label("Wait Save Scenes And Assets", EditorStyles.boldLabel, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        }
    }
}