using HarmonyLib;
using MGSC;

namespace QM_AutoLoadGame
{
    [HarmonyPatch(typeof(SpaceHudScreen), nameof(SpaceHudScreen.ProcessUpdate))]
    internal static class SpaceHudScreen_ProcessUpdate_Patch
    {

        public static bool PauseOnLoad { get; set; }
        public static void Postfix(SpaceHudScreen __instance)
        {
            if (PauseOnLoad)
            {
                PauseOnLoad = false;

                SharedUi._instance._escScreen.Show();
            }

        }
    }
}