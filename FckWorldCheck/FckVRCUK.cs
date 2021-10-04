using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using VRChatUtilityKit;

namespace FckWorldCheck
{
    internal class FckVRCUK
    {
        internal static HarmonyLib.Harmony h;
        internal const int nPatches = 1;
        internal static void FckCheck()
        {
            try
            {
                h = new HarmonyLib.Harmony(new System.Random((int)DateTime.Now.ToBinary()).Next(1000, 9999999).ToString());

                var p = typeof(VRChatUtilityKit.Utilities.VRCUtils).GetProperty("AreRiskyFunctionsAllowed", BindingFlags.Public | BindingFlags.Static).GetGetMethod(false);

                FckLogger.Msg("Patching VRChatUtilityKit...");

                h.Patch(p, postfix: new HarmonyMethod(typeof(FckVRCUK).GetMethod(nameof(AreRiskyFunctionsAllowedPatch), BindingFlags.NonPublic | BindingFlags.Static)));
            }
            catch(Exception e)
            {
                FckLogger.Error("Failed To Patch VRChatUtilityKit World Checks!");
                FckLogger.Error(e.ToString());
            }
            
            var n = h.GetPatchedMethods();
            var count = 0;
            foreach (var k in n)
            {
                if (k == null) continue;
                count++;
            }

            FckLogger.Msg($"Patches Applied: {count}/{nPatches}");

            if (count != nPatches)
            {
                FckLogger.Error("Failed To Patch VRChatUtilityKit!");
                return;
            }
            FckLogger.Green("VRChatUtilityKit Patched");
        }

        internal static void AreRiskyFunctionsAllowedPatch(ref bool __result)
        {
            __result = true;
        }
    }
}
