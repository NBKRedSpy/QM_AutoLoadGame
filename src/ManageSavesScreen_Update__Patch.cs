using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_AutoLoadGame
{
    [HarmonyPatch(typeof(ManageSavesScreen), nameof(ManageSavesScreen.Update))]
    internal static class ManageSavesScreen_Update__Patch
    {
        public static bool Prefix(ManageSavesScreen __instance)
        {
            if (!PressAnyKeyScreen_Patch.AutoLoad)
            {
                return true;
            }

            PressAnyKeyScreen_Patch.AutoLoad = false;
            __instance.SlotOnStartGame(0, false);
            return false;
        }
    }
}
