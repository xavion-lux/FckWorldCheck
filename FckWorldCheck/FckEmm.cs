using HarmonyLib;
using System;
using System.Linq;
using System.Collections;
using System.Reflection;

namespace FckWorldCheck
{
    internal class FckEmm
    {
        internal static Type riskyFunctionsManager = null;
        internal const int nPatches = 2;

        internal static IEnumerator FckCheck()
        {
            HarmonyLib.Harmony h = new HarmonyLib.Harmony(new System.Random((int)DateTime.Now.ToBinary()).Next(1000, 9999999).ToString());
            try
            {
                var emmVRC = ((MethodInfo)typeof(emmVRCLoader.ModController).GetField("onApplicationStartMethod", AccessTools.all).GetValue(emmVRCLoader.Bootstrapper.mod)).DeclaringType.Assembly;
                if (emmVRC == null) throw new Exception("emmVRC Instance Not Found!");
                FckLogger.Msg("emmVRC Instance Found");

                riskyFunctionsManager = emmVRC.GetType("emmVRC.Managers.RiskyFunctionsManager");
                if (riskyFunctionsManager == null) throw new Exception("riskyFunctionsManager Not Found!");

                var RiskyFuncsAreAllowedGet = emmVRC.GetType("emmVRC.Managers.RiskyFunctionsManager").GetProperty("RiskyFuncsAreAllowed", BindingFlags.NonPublic | BindingFlags.Static).GetGetMethod(true);

                var CheckThisWrldMethod = riskyFunctionsManager.GetMethod("CheckThisWrld", BindingFlags.NonPublic | BindingFlags.Static);

                FckLogger.Msg("Patching emmVRC...");

                h.Patch(RiskyFuncsAreAllowedGet,
                postfix: new HarmonyMethod(typeof(FckEmm).GetMethod(nameof(RiskyFuncsAreAllowedPatch), BindingFlags.NonPublic | BindingFlags.Static)));

                h.Patch(CheckThisWrldMethod, postfix: new HarmonyMethod(typeof(FckEmm).GetMethod("CheckThisWrldPatch", BindingFlags.Static | BindingFlags.NonPublic)));

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
                    yield break;
                }
                FckLogger.Green("emmVRC Patched");
            }
            catch(Exception e)
            {
                FckLogger.Error(e.ToString());
            }
            yield break;
        }

        internal static void RiskyFuncsAreAllowedPatch(ref bool __result)
        {
            __result = true;
        }

        internal static void CheckThisWrldPatch()
        {
            riskyFunctionsManager.GetField("riskyFuncsAllowed", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, true);
            riskyFunctionsManager.GetField("RiskyFuncsAreChecked", BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, false);
        }
    }
}
