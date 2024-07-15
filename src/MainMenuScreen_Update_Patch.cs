using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace QM_AutoLoadGame
{


    [HarmonyPatch(typeof(MainMenuScreen), nameof(MainMenuScreen.Update))]
    internal static class MainMenuScreen_Update_Patch
    {

        /// <summary>
        /// The time the auto load will happen.
        /// </summary>
        public static DateTime AutoLoadTime { get; set; } = DateTime.MaxValue;
        
        /// <summary>
        /// The auto load was aborted by the user.
        /// </summary>
        public static bool AbortAutoLoad { get; set; } = false;


        /// <summary>
        /// The auto load settings have been inited.
        /// </summary>
        public static bool AutoLoadInited { get; set; }

        /// <summary>
        /// True if the main window was shown.  
        /// Hack to check if screen changed (like going to options).
        /// </summary>
        public static bool MainMenuWasShown { get; set; } = false;

        public static void Postfix(MainMenuScreen __instance)
        {

            //Also prevents returning to the main screen causing another auto load loop.
            if (AbortAutoLoad == true) return;

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                AbortAutoLoad = true;
                return;
            }



            
            //Prevent the autoloader from start the countdown if the main menu hasn't been visible yet.
            if (!__instance.gameObject.activeSelf) return;

            if (AutoLoadInited == false)
            {
                if(!ValidateSlotFromConfig())
                {
                    AbortAutoLoad = true;
                    return;
                }

                AutoLoadTime = DateTime.Now + TimeSpan.FromSeconds(Plugin.Config.CountDownSeconds);
                AutoLoadInited = true;

            }
            else
            {
                if(DateTime.Now > AutoLoadTime)
                {
                    Plugin.IsAutoLoading = true;


                    AutoLoadTime = DateTime.MaxValue;
                    
                    //Prevent auto load from running if returning to the main window.
                    AbortAutoLoad = true;

                    __instance.StartGameBtnOnClick(null);
                    
                }
            }
        }

        private static bool ValidateSlotFromConfig()
        {
            if (Plugin.Config.LastLoadedSlot == -1) return false;

            if (!SingletonMonoBehaviour<MainMenuUI>.Instance.ManageSavesScreen._saveManager.IsSaveExist(Plugin.Config.LastLoadedSlot))
            {
                Plugin.Config.LastLoadedSlot = -1;
                Plugin.Config.SaveConfig();
                return false;
            }

            return true;
        }

    }
}