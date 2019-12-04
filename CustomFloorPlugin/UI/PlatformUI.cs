﻿using HMUI;
using UnityEngine;
using CustomUI.MenuButton;
using CustomUI.Settings;
using CustomUI.BeatSaber;
using CustomFloorPlugin.Util;
using CustomUI.Utilities;
using UnityEngine.SceneManagement;

namespace CustomFloorPlugin
{
    class PlatformUI : MonoBehaviour
    {   
        public static PlatformUI _instance;
                
        public CustomMenu _platformMenu;

        internal static void OnLoad()
        {
            if (_instance != null)
            {
                return;
            }
            new GameObject("PlatformUI").AddComponent<PlatformUI>();
        }

        private void Awake()
        {
            _instance = this;
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.CreateScene("PlatformUIDump"));
            GameScenesManagerSO.MarkSceneAsPersistent("PlatformUIDump");


            BSEvents.menuSceneLoadedFresh += HandleMenuSceneLoadedFresh;
            HandleMenuSceneLoadedFresh();
        }

        private void HandleMenuSceneLoadedFresh()
        {
            if (_platformMenu == null)
            {
                _platformMenu = BeatSaberUI.CreateCustomMenu<CustomMenu>("Platform Select");
                PlatformListViewController platformListViewController = BeatSaberUI.CreateViewController<PlatformListViewController>();
                platformListViewController.backButtonPressed += delegate () { _platformMenu.Dismiss(); };
                _platformMenu.SetMainViewController(platformListViewController, true);
                platformListViewController.DidSelectRowEvent += delegate (TableView view, int row) { PlatformManager.Instance.ChangeToPlatform(row); };
            }

            MenuButtonUI.AddButton( "Platforms", delegate () { _platformMenu.Present(); } );
            
            CreateSettingsUI();
        }

        private static void CreateSettingsUI()
        {
            var subMenu = SettingsUI.CreateSubMenu("Platforms");
            
            var feetMenu = subMenu.AddBool("Always Show Feet");
            feetMenu.GetValue += delegate
            {
                return EnvironmentHider.showFeetOverride;
            };
            feetMenu.SetValue += delegate (bool value)
            {
                EnvironmentHider.showFeetOverride = value;
                Plugin.config.SetBool("Settings", "AlwaysShowFeet", EnvironmentHider.showFeetOverride);
            };
            
            var environment = subMenu.AddList("Environment Override", EnvironmentSceneOverrider.OverrideModes());
            environment.GetValue += delegate
            {
                return (float)EnvironmentSceneOverrider.overrideMode;
            };
            environment.SetValue += delegate (float value)
            {
                EnvironmentSceneOverrider.overrideMode = (EnvironmentSceneOverrider.EnvOverrideMode)value;
                EnvironmentSceneOverrider.OverrideEnvironmentScene();
                Plugin.config.SetInt("Settings", "EnvironmentOverrideMode", (int)EnvironmentSceneOverrider.overrideMode);
            };
            environment.FormatValue += delegate (float value) { return EnvironmentSceneOverrider.Name((EnvironmentSceneOverrider.EnvOverrideMode)value); };
            
            var arrangement = subMenu.AddList("Environment Arrangement", EnvironmentArranger.RepositionModes());
            arrangement.GetValue += delegate
            {
                return (float)EnvironmentArranger.arrangement;
            };
            arrangement.SetValue += delegate (float value)
            {
                EnvironmentArranger.arrangement = (EnvironmentArranger.Arrangement)value;
                Plugin.config.SetInt("Settings", "EnvironmentArrangement", (int)EnvironmentArranger.arrangement);
            };
            arrangement.FormatValue += delegate (float value) { return EnvironmentArranger.Name((EnvironmentArranger.Arrangement)value); };
            
        }
    }
}