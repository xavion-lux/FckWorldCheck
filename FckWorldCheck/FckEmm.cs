using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;

namespace FckWorldCheck
{
    internal class FckEmm
    {
        internal const int nPatches = 1;

        internal static void FckCheck()
        {
            HarmonyLib.Harmony h = new HarmonyLib.Harmony(new System.Random((int)DateTime.Now.ToBinary()).Next(1000, 9999999).ToString());
            try
            {
                var emmVRC = ((MethodInfo)typeof(emmVRCLoader.ModController).GetField("onApplicationStartMethod", AccessTools.all).GetValue(emmVRCLoader.Bootstrapper.mod)).DeclaringType.Assembly;
                if (emmVRC == null) throw new Exception("emmVRC Instance Not Found!");
                FckLogger.Msg("emmVRC Instance Found");

                var RiskyFuncsAreAllowedGet = emmVRC.GetType("emmVRC.Managers.RiskyFunctionsManager").GetProperty("RiskyFuncsAreAllowed", BindingFlags.NonPublic | BindingFlags.Static).GetGetMethod(true);

                FckLogger.Msg("Patching emmVRC...");

                h.Patch(RiskyFuncsAreAllowedGet,
                postfix: new HarmonyMethod(typeof(FckEmm).GetMethod(nameof(RiskyFuncsAreAllowedPatch), BindingFlags.NonPublic | BindingFlags.Static)));

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
                    FckLogger.Error("Failed To Patch emmVRC!");
                    return;
                }
                FckLogger.Green("emmVRC Patched");
            }
            catch(Exception e)
            {
                FckLogger.Error(e.ToString());
            }
            return;
        }

        internal static void RiskyFuncsAreAllowedPatch(ref bool __result)
        {
            __result = true;
        }
    }
}
