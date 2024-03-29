﻿using MelonLoader;
using System;
using System.Linq;

namespace FckWorldCheck
{
    internal class FckWorldCheck : MelonMod
    {
        public override void OnApplicationStart()
        {
            FckLogger.Init();
            FckLogger.Magenta("Loading...");
            MelonCoroutines.Start(FckShield.Bypass());
        }

        public override void OnApplicationLateStart()
        {
            FckLogger.Magenta("Scanning For Mods...");

            if (CheckForMelon("emmVRCLoader"))
            {
                FckLogger.Blue("emmVRCLoader Found");
                FckEmm.FckEmmInit();
                FckEmm.FckCheck();
                FckEmm.FckNoUnlimitedFavs();    
            }

            if (CheckForMelon("VRChatUtilityKit"))
            {
                FckLogger.Blue("VRChatUtilityKit Found");
                FckVRCUK.FckCheck();
            }

            if (CheckForMelon("SeatMod"))
            {
                FckLogger.Blue("SeatMod Found");
                FckSeatMod.FckCheck();
            }

            FckLogger.Magenta("Initialized! Enjoy Your Freedom!");
        }

        private bool CheckForMelon(string name){

            return MelonHandler.Mods.Any((MelonMod m) => m.Info.Name == name);
        }
    }
}