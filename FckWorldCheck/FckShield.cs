using HarmonyLib;
using System;
using System.Collections;

namespace FckWorldCheck
{
    internal class FckShield
    {
        internal static HarmonyLib.Harmony h;
        internal const int nPatches = 1;

        internal static IEnumerator Bypass()
        {
            FckLogger.Cyan("Bypassing Harmony Protections..");
            h = new HarmonyLib.Harmony(new System.Random((int)DateTime.Now.ToBinary()).Next(1000, 9999999).ToString());
            try
            {
                BypassAssembly();
            }
            catch(Exception e)
            {
                FckLogger.Error("Failed To Patch Harmony Protections!");
                FckLogger.Error(e.ToString());
            }

            var n = h.GetPatchedMethods();
            var count = 0;
            foreach (var k in n)
            {
                if (k == null) continue;
                count++;
            }
            if (count != nPatches)
            {
                FckLogger.Error($"Patches Applied: {count}/{nPatches}");
                FckLogger.Error("Failed To Bypass Harmony Protections!");
                yield break;
            }
            FckLogger.Cyan($"Patches Applied: {count}/{nPatches}");
            FckLogger.Cyan("Harmony Protections Bypassed");
            yield break;
        }

        [HarmonyPriority(Priority.First)]
        internal static void BypassAssembly()
        {
            var t = typeof(System.Reflection.Assembly);
            if (t == null) throw new Exception("System.Reflection.Assembly Type Not Found!");

            var m = typeof(System.Reflection.Assembly).GetMethod("GetCustomAttributes", new Type[] { typeof(Type), typeof(bool) });
            if (m == null) throw new Exception("System.Reflection.Assembly.GetCustomAttributes Method Not Found!");

            h.Patch(m, postfix: new HarmonyMethod(typeof(FckShield), nameof(GetCustomAttributesPatch)));
        }

        internal static void GetCustomAttributesPatch(ref Type __0, ref object[] __result)
        {
            if (__0.ToString().ToLower().Contains("patchshield")) __result = new object[0];
        }
    }
}