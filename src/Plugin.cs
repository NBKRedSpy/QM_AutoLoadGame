using HarmonyLib;
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
        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        public static string ConfigPath => Path.Combine(Application.persistentDataPath, ModAssemblyName, ModAssemblyName + ".json");
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, ModAssemblyName);

        public static ModConfig Config { get; set; }

        public static bool IsAutoLoading { get; set; } = false;

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {

            try
            {

                Directory.CreateDirectory(ModPersistenceFolder);

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
    }
}
