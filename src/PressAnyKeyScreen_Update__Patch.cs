using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using sd = System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_AutoLoadGame
{
    [HarmonyPatch(typeof(PressAnyKeyScreen), nameof(PressAnyKeyScreen.Update))]
    internal static class PressAnyKeyScreen_Update__Patch
    {

        /// <summary>
        /// Used to disable the auto load once the game is loaded.
        /// Consumed by the other screens to indicate if they need to continue the load chain.
        /// </summary>
        public static bool AutoLoad { get; set; } = false;
        private static sd.Stopwatch LoadStopWatch { get; set; }

        /// <summary>
        /// True if the count down is still in progress and not aborted.
        /// </summary>
        public static bool WaitingForCountdown { get; set; } = true;

        public static bool Prefix(PressAnyKeyScreen __instance) 
        {

            if (Plugin.Config.Disable || !WaitingForCountdown)
            {
                return true;
            }
            //Loaded slot not set yet, or the user has held down a shift key.
            else if (Plugin.Config.LastLoadedSlot == -1 || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                AutoLoad = false;
                WaitingForCountdown = false;
                LoadStopWatch?.Stop();
                return true;
            }
            else
            {
                //Verify the slot is valid
                if(!Plugin.State.Get<SaveManager>().IsSaveExist(Plugin.Config.LastLoadedSlot))
                {
                    Debug.LogWarning($"Last loaded slot {Plugin.Config.LastLoadedSlot} does not exist");
                    WaitingForCountdown = false;
                    return true;
                }
            }

            if(LoadStopWatch is null)
            {
                LoadStopWatch = sd.Stopwatch.StartNew();
            }

            if(Plugin.Config.CountDownSeconds == 0 || 
                (LoadStopWatch.ElapsedMilliseconds / 1000) > Plugin.Config.CountDownSeconds)
            {
                LoadStopWatch.Stop();
                WaitingForCountdown = false;
                AutoLoad = true;
                __instance.gameObject.SetActive(value: false);
                SingletonMonoBehaviour<MainMenuUI>.Instance.MainMenuScreen.Show();
                return false;
            }

            int remainingSeconds = Plugin.Config.CountDownSeconds - LoadStopWatch.Elapsed.Seconds;

            __instance._anyPressText.SetText($"Auto Loading in {remainingSeconds} seconds. Press Any Key to Abort\nTip: Hold shift during game load to also abort auto loading.");

            return true;
        }
    }
}
