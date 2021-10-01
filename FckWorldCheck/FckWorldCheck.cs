using MelonLoader;
using System.Linq;

namespace FckWorldCheck
{
    class FckWorldCheck : MelonMod
    {
        public override void OnApplicationStart()
        {
            FckLogger.Init();
            FckLogger.Magenta("Loading..");
            MelonCoroutines.Start(FckShield.Bypass());
        }

        public override void OnApplicationLateStart()
        {
            FckLogger.Magenta("Checking For Mods..");
            if (CheckForMelon("emmVRCLoader"))
            {
                FckLogger.Blue("emmVRCLoader Found");
                MelonCoroutines.Start(FckEmm.FckCheck());
            }
            FckLogger.Magenta("Initialized! Enjoy Your Freedom");
        }

        private bool CheckForMelon(string name){

            return MelonHandler.Mods.Any((MelonMod m) => m.Info.Name == name);
        }
    }
}