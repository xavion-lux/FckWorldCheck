using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using VRC.Core;

namespace FckWorldCheck
{
    internal class FckEmm
    {
        internal static Assembly emmVRCAssembly;
        internal static HarmonyLib.Harmony h;
        internal const int nPatches = 2;

        internal static void FckEmmInit()
        {
            try
            {
                emmVRCAssembly = ((MethodInfo)typeof(emmVRCLoader.ModController).GetField("onApplicationStartMethod", AccessTools.all).GetValue(emmVRCLoader.Bootstrapper.mod)).DeclaringType.Assembly;
                if (emmVRCAssembly == null)
                {
                    FckLogger.Error("emmVRC Instance Not Found!");
                    throw new Exception("emmVRC Instance Not Found!");
                }
                    
                //FckLogger.Msg("emmVRC Instance Found");

                h = new HarmonyLib.Harmony(new System.Random((int)DateTime.Now.ToBinary()).Next(1000, 9999999).ToString());
            }
            catch(Exception e)
            {
                FckLogger.Error(e.ToString());
                return;
            }

            //FckLogger.Msg("Patching emmVRC...");

            MelonLoader.MelonCoroutines.Start(FckCheck());
            MelonLoader.MelonCoroutines.Start(FckNoUnlimitedFavs());

            var n = h.GetPatchedMethods();
            var count = 0;
            foreach (var k in n)
            {
                if (k == null) continue;
                count++;
            }

            if (count != nPatches)
            {
                FckLogger.Msg($"Patches Applied: {count}/{nPatches}");
                FckLogger.Error("Failed To Patch emmVRC!");
                return;
            }
            FckLogger.Green("emmVRC Patched");
        }

        internal static IEnumerator FckCheck()
        {
            try
            {
                h.Patch(emmVRCAssembly.GetType("emmVRC.Managers.RiskyFunctionsManager").GetProperty("AreRiskyFunctionsAllowed", BindingFlags.Public | BindingFlags.Static).GetSetMethod(true), postfix: new HarmonyMethod(typeof(FckEmm).GetMethod(nameof(AreRiskyFunctionsAllowedPatch), BindingFlags.NonPublic | BindingFlags.Static)));
            }
            catch(Exception e)
            {
                FckLogger.Error("Failed To Patch emmVRC World Checks!");
                FckLogger.Error(e.ToString());
            }
            yield break;
        }

        internal static IEnumerator FckNoUnlimitedFavs()
        {
            try
            {
                var DoesUserHaveVRCPlusMethod = emmVRCAssembly.GetType("emmVRC.Utils.PlayerUtils").GetMethod("DoesUserHaveVRCPlus", BindingFlags.Public | BindingFlags.Static);

                h.Patch(DoesUserHaveVRCPlusMethod, postfix: new HarmonyMethod(typeof(FckEmm).GetMethod(nameof(DoesUserHaveVRCPlusPatch), BindingFlags.NonPublic | BindingFlags.Static)));
            }
            catch(Exception e)
            {
                FckLogger.Error("Failed To Patch emmVRC Favorites!");
                FckLogger.Error(e.ToString());
            }
            yield break;
        }

        internal static void AreRiskyFunctionsAllowedPatch() => emmVRCAssembly.GetType("emmVRC.Managers.RiskyFunctionsManager").GetField("_areRiskyFunctionsAllowed", BindingFlags.NonPublic | BindingFlags.Static).SetValue(emmVRCAssembly, true);

        internal static void DoesUserHaveVRCPlusPatch(ref bool __result)
        {
            __result = true;
        }
    }
}
