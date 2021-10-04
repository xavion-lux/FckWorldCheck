using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using VRChatUtilityKit;

namespace FckWorldCheck
{
    internal class FckVRCUK
    {
        internal const int nPatches = 1;
        internal static void FckCheck()
        {
            HarmonyLib.Harmony h = new HarmonyLib.Harmony(new System.Random((int)DateTime.Now.ToBinary()).Next(1000, 9999999).ToString());

            var p = typeof(VRChatUtilityKit.Utilities.VRCUtils).GetProperty("AreRiskyFunctionsAllowed", BindingFlags.Public | BindingFlags.Static).GetGetMethod(false);

            FckLogger.Msg("Patching VRChatUtilityKit...");

            h.Patch(p, postfix: new HarmonyMethod(typeof(FckVRCUK).GetMethod("AreRiskyFunctionsAllowedPatch", BindingFlags.NonPublic | BindingFlags.Static)));

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
