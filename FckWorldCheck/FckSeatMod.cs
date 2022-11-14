using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FckWorldCheck
{
    internal class FckSeatMod
    {
        internal static HarmonyLib.Harmony h;
        internal static int nPatches = 2;
        internal static Assembly assembly;

        internal static void FckCheck()
        {
            try
            {
                h = new HarmonyLib.Harmony(new System.Random((int)DateTime.Now.ToBinary()).Next(1000, 9999999).ToString());

                assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "SeatMod");

                var m1 = typeof(SeatMod.Utils).GetMethod("CanSit", BindingFlags.Public | BindingFlags.Static);

                h.Patch(m1, postfix: new HarmonyMethod(typeof(FckSeatMod).GetMethod(nameof(CanSitPatch), BindingFlags.NonPublic | BindingFlags.Static)));

                var m2 = assembly.GetType("SeatMod.RiskFunct").GetMethod("CheckWorld", BindingFlags.NonPublic | BindingFlags.Static);

                h.Patch(m2, postfix: new HarmonyMethod(typeof(FckSeatMod).GetMethod(nameof(CheckWorldPatch), BindingFlags.NonPublic | BindingFlags.Static)));
            }
            catch (Exception e)
            {
                FckLogger.Error("Failed To Patch SeatMod!");
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
                FckLogger.Msg($"Patches Applied: {count}/{nPatches}");
                FckLogger.Error("Failed To Patch SeatMod!");
                return;
            }
            FckLogger.Green("SeatMod Patched");
        }

        internal static void CanSitPatch(ref int __result)
        {
            __result = 1;
        }

        internal static void CheckWorldPatch()
        {
            SeatMod.Main.WorldType = 0;
            System.Collections.Generic.Dictionary<string, int> dic = (System.Collections.Generic.Dictionary<string, int>)assembly.GetType("SeatMod.RiskFunct").GetField("checkedWorlds").GetValue(null);
            dic[RoomManager.field_Internal_Static_ApiWorld_0.id] = 0;
        }
    }
}
