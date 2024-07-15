using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_AutoLoadGame
{
    [HarmonyPatch(typeof(ManageSavesScreen), nameof(ManageSavesScreen.SlotOnStartGame))]
    internal class ManageSavesScreen_SlotOnStartGame_Patch
    {
        public static void Postfix(ManageSavesScreen __instance, int gameSlot)
        {

            Plugin.Config.LastLoadedSlot = gameSlot;
            Plugin.Config.SaveConfig();
        }
    }
}
