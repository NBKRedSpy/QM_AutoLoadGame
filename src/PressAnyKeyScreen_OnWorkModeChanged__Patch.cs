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
    [HarmonyPatch(typeof(PressAnyKeyScreen), nameof(PressAnyKeyScreen.OnWorkModeChanged))]
    internal static class PressAnyKeyScreen_OnWorkModeChanged__Patch
    {
        public static void Postfix(PressAnyKeyScreen __instance)
        {
            if(__instance._modeChanged)
            {
                //Prevent auto load if the user clicked the screen.
                //If the mod is autoloading, this should already be false.
                PressAnyKeyScreen_Update__Patch.WaitingForCountdown = false;
            }

        }
    }
}
