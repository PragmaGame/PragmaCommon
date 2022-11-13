using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PragmaCommon.Editor.AutoSave
{
    public static partial class AutoSave
    {
        private const string PROVIDER_LABEL = "AutoSave Scenes & Assets";
        private const string PROVIDER_PATH = "Preferences/AutoSave";

        private static readonly GUIContent enableText = EditorGUIUtility.TrTextContent("Enable AutoSave");
        private static readonly GUIContent beforePlayText = EditorGUIUtility.TrTextContent("Save Before Play");
        private static readonly GUIContent frequencyText = EditorGUIUtility.TrTextContent("AutoSave every", "Time span between AutoSaves.");
        private static readonly GUIContent minutesText = EditorGUIUtility.TrTextContent("minutes");
        private static readonly GUIContent logText = EditorGUIUtility.TrTextContent("Show Log");
        private static readonly GUIContent popupText = EditorGUIUtility.TrTextContent("Show Popup Screen");

        private static readonly HashSet<string> keywords = new(new[] { "AutoSave" });
	    
	    [SettingsProvider]
	    public static SettingsProvider ShowAutoSavePreferences()
	    {
		    var provider = new SettingsProvider(PROVIDER_PATH, SettingsScope.User)
		    {
			    label = PROVIDER_LABEL,
			    keywords = keywords,
			    guiHandler = (_) =>
			    {
				    DrawGUI();
			    }				
		    };			

		    return provider;
	    }
	    
        private static void DrawGUI()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(450));

            EditorGUIUtility.labelWidth = 300;
            
            SwitchEnable(EditorGUILayout.Toggle(enableText, Enable));

            using (new EditorGUI.DisabledScope(!Enable))
            {
	            SaveBeforePlay = EditorGUILayout.Toggle(beforePlayText, SaveBeforePlay);

	            GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(700));
                SaveInterval = EditorGUILayout.IntSlider(
                    frequencyText,
                    SaveInterval,
                    MIN_SAVE_INTERVAL,
                    MAX_SAVE_INTERVAL);
                GUILayout.Label(minutesText, GUILayout.MaxWidth(60));
                
                EditorGUILayout.EndHorizontal();
                
                GUILayout.Space(10);

                Log = EditorGUILayout.Toggle(logText, Log);
                Popup = EditorGUILayout.Toggle(popupText, Popup);
                
                GUILayout.Space(10);
            }

            GUILayout.Space(10);
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    }
}