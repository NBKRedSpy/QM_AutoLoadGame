using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_AutoLoadGame
{
    /// <summary>
    /// Stores the last loaded slot for future use.
    /// </summary>
    [HarmonyPatch(typeof(ManageSavesScreen), nameof(ManageSavesScreen.SlotOnStartGame))]
    internal static class ManageSavesScreen_SlotOnStartGame__Patch
    {
        public static void Prefix(int gameSlot)
        {
            Plugin.Config.LastLoadedSlot = gameSlot;
            Plugin.Config.SaveConfig();
        }
    }
}
