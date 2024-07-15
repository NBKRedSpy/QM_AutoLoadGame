using HarmonyLib;
using MGSC;

namespace QM_AutoLoadGame
{
    [HarmonyPatch(typeof(MainMenuScreen), nameof(MainMenuScreen.Hide))]
    internal static class MainMenuScreen_Hide_Patch
    { 
        public static void Postfix()
        {
            //Used to prevent the auto load from occurring if another screen was navigated to.
            MainMenuScreen_Update_Patch.AbortAutoLoad = true;
        }
    }
}