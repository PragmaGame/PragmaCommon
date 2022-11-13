using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace PragmaCommon.Editor.AutoSave
{
    [InitializeOnLoad]
    public static partial class AutoSave
    {
        private const string KEY_PREFIX = "@EDITOR_CUSTOM_AUTO_SAVE/";
        
        private const string KEY_ENABLED = "ENABLED";
        private const string KEY_ON_PLAY = "ON_PLAY";
        private const string KEY_SAVE_INTERVAL = "INTERVAL";

        private const string KEY_LOG = "LOG";
        private const string KEY_POPUP = "POPUP";
        
        private const int DEFAULT_SAVE_INTERVAL = 5;
        private const int MIN_SAVE_INTERVAL = 1;
        private const int MAX_SAVE_INTERVAL = 30;

        public static DateTime SaveTime { get; private set; }

        public static bool Enable
        {
            get => EditorPrefs.GetBool(KEY_PREFIX + KEY_ENABLED, false);
            private set => EditorPrefs.SetBool(KEY_PREFIX + KEY_ENABLED, value);
        }

        public static bool SaveBeforePlay
        {
            get => EditorPrefs.GetBool(KEY_PREFIX + KEY_ON_PLAY, true);
            private set => EditorPrefs.SetBool(KEY_PREFIX + KEY_ON_PLAY, value);
        }

        public static int SaveInterval
        {
            get => EditorPrefs.GetInt(KEY_PREFIX + KEY_SAVE_INTERVAL, DEFAULT_SAVE_INTERVAL);
            private set => EditorPrefs.SetInt(KEY_PREFIX + KEY_SAVE_INTERVAL, value);
        }

        public static bool Log
        {
            get => EditorPrefs.GetBool(KEY_PREFIX + KEY_LOG, true);
            private set => EditorPrefs.SetBool(KEY_PREFIX + KEY_LOG, value); 
        }
        
        public static bool Popup
        {
            get => EditorPrefs.GetBool(KEY_PREFIX + KEY_POPUP, true);
            private set => EditorPrefs.SetBool(KEY_PREFIX + KEY_POPUP, value); 
        }

        static AutoSave()
        {
            InitializeEditorPrefs();

            SwitchEnable(Enable, true);
        }

        private static void InitializeEditorPrefs()
        {
            if (!EditorPrefs.HasKey(KEY_PREFIX + KEY_SAVE_INTERVAL))
            {
                SaveInterval = DEFAULT_SAVE_INTERVAL;
            }

            SaveTime = DateTime.Now;
        }

        private static void ActivateAutoSave()
        {
            EditorApplication.update += EditorApplicationUpdateHandler;
            EditorApplication.playModeStateChanged += PlayModeStateChangedHandler;
        }

        private static void DeactivateAutoSave()
        {
            EditorApplication.update -= EditorApplicationUpdateHandler;
            EditorApplication.playModeStateChanged -= PlayModeStateChangedHandler;
        }

        private static void PlayModeStateChangedHandler(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                {
                    if (SaveBeforePlay)
                    {
                        SaveAll();
                    }

                    break;
                }
                case PlayModeStateChange.EnteredEditMode:
                {
                    SaveTime = DateTime.Now;
                    break;
                }
            }
        }

        private static void EditorApplicationUpdateHandler()
        {
            if ((DateTime.Now - SaveTime).TotalMinutes >= SaveInterval)
            {
                SaveTime = DateTime.Now;
                
                if (EditorApplication.isPlaying || EditorApplication.isCompiling || EditorApplication.isUpdating ||
                    BuildPipeline.isBuildingPlayer)
                {
                    return;
                }

                SaveAll();
            }
        }
        
        public static void SwitchEnable(bool value, bool isSwitchAnyway = false)
        {
            if (!isSwitchAnyway && value == Enable)
            {
                return;
            }
            
            if (value)
            {
                ActivateAutoSave();
            }
            else
            {
                DeactivateAutoSave();
            }

            Enable = value;
        }

        public static void SaveAll()
        {
            if (Popup)
            {
                AutoSavePopup.ShowWindow();
            }
            
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
            
            if (Popup)
            {
                AutoSavePopup.CloseWindow();
            }

            if (Log)
            {
                Debug.Log("[AutoSave] Saved Scenes and Assets.\n" + DateTime.Now);
            }
        }
    }
}