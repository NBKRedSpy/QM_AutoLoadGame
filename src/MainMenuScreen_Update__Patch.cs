//using HarmonyLib;
//using MGSC;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace QM_AutoLoadGame
//{
//    [HarmonyPatch(typeof(MainMenuScreen), nameof(MainMenuScreen.Update))]
//    internal static class MainMenuScreen_Update__Patch
//    {
//        public static bool Prefix(MainMenuScreen __instance)
//        {
//            if(!PressAnyKeyScreen_Update__Patch.AutoLoad)
//            { 
//                return true; 
//            }

//            __instance.StartGameBtnOnClick(null, 1);
//            return false;
//        }        
//    }
//}
