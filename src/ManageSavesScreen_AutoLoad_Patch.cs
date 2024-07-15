using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_AutoLoadGame
{

    [HarmonyPatch(typeof(ManageSavesScreen), nameof(ManageSavesScreen.Update))]
    internal class ManageSavesScreen_AutoLoad_Patch
    {
        public static void Postfix(ManageSavesScreen __instance)
        {
            if (Plugin.IsAutoLoading)
            {
                Plugin.IsAutoLoading = false;
                SpaceHudScreen_ProcessUpdate_Patch.PauseOnLoad = true;

                __instance.SlotOnStartGame(Plugin.Config.LastLoadedSlot, false);
            }
            else
            {
                MainMenuScreen_Update_Patch.AbortAutoLoad = true;
            }
        }
    }
}
