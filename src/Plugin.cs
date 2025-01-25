﻿using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_AutoLoadGame
{
    public static class Plugin
    {
        public static string ModAssemblyName { get; private set; }

        /// <summary>
        /// The full path to the config file.  Stored in the mod's persistence folder.
        /// </summary>
        public static string ConfigPath { get; private set; }

        /// <summary>
        /// This mod's persistence folder.
        /// </summary>
        public static string ModsPersistenceFolder { get; private set;  }

        /// <summary>
        /// The Quasimorph_Mods folder that is parallel to the game's folder.
        /// This is a workaround for Quasimorph syncing and overwriting all files in the 
        /// Game's App Data folder.
        /// </summary>
        private static string AllModsConfigFolder { get; set; }

        public static ModConfig Config { get; set; }

        public static bool IsAutoLoading { get; set; } = false;

        public static State State { get; set; }

        static Plugin()
        {
            ModAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            
            AllModsConfigFolder = Path.Combine(Application.persistentDataPath, "../Quasimorph_ModConfigs/");
            
            ModsPersistenceFolder = Path.Combine(AllModsConfigFolder,ModAssemblyName);
            
            ConfigPath = Path.Combine(ModsPersistenceFolder, ModAssemblyName + ".json");
        }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            State = context.State;

            try
            {
                Directory.CreateDirectory(AllModsConfigFolder);

                UpgradeModDirectory();

                Directory.CreateDirectory(ModsPersistenceFolder);

                ModConfig.ConfigPath = ConfigPath;
                Config = ModConfig.LoadConfig();

                if (Config.Disable) return;

                new Harmony("nbk_redspy.QM_AutoLoadGame").PatchAll();
            }
            catch (Exception ex)
            {

                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// Moves the config files from the legacy directory to the new directory.
        /// </summary>
        private static void UpgradeModDirectory()
        {
            try
            {
                string oldDirectory = Path.Combine(Application.persistentDataPath, 
                    ModAssemblyName);

                if (!Directory.Exists(oldDirectory)) return;

                Debug.LogWarning($"Moving config folder from '{oldDirectory}' to '{ModsPersistenceFolder}");
                Directory.Move(oldDirectory, ModsPersistenceFolder);
            }
            catch (Exception ex)
            {
                Debug.Log($"Unable to move the config files.  Exception: {ex.ToString()}");
            }
        }
    }
}
