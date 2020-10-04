using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Starbit_Route_Generator
{
    class Galaxy
    {
        //declare variables

        //Array of star counts that result in a "new galaxy" or "new chapter" textbox
        public static int[] starCountNotifTerrace = { 1, 3, 5, 7, 8, -1, -1, -1 , -1 };
        public static int[] starCountNotifFountain = { 9, 11, 12, 15, -1, -1, -1, -1, -1 };
        public static int[] starCountNotifKitchen = { 16, 18, 19, 20, 23, 28, 40, 49, 58 };
        public static int[] starCountNotifBedroom = { 24, 26, 29, 30, 32, 33, -1, -1, -1 };
        public static int[] starCountNotifEngineRoom = { 34, 36, 40, 42, 45, -1, -1, -1, -1 };
        public static int[] starCountNotifGarden = { 46, 48, 52, -1, -1, -1, -1, -1, -1 };

        //booleans that check conditions to see if a texbox will appear. Specifically, star specific checks.
        public bool unlockGalaxy;
        public bool getCoin;
        public bool isStarComplete;
        public bool isGalaxyComplete;
        public static bool isFoutainUnlocked;
        public static bool isKitchenUnlocked;
        public static bool isBedroomUnlocked;
        public static bool isEngineRoomUnlocked;
        public static bool isGardenUnlocked;

        //Min and max starbits. Min starbits is determined by how many starbits the level has, and if there's a noticifcation
        //(ie, no notification, starbits = 18
        public int maxBits;
        public int minBits;
        public int collectedBits;

        //variable that keeps track which star number the given level is
        public int starNumber;

        public Galaxy()
        {

        }
        public Galaxy(int min, int max, bool coins, bool galaxyComplete)
        {
            //sets variabls for particular star
            minBits = min;
            maxBits = max;
            getCoin = coins;
            isGalaxyComplete = galaxyComplete;
        }


        public bool HasNotifs()
        {
            //Checks if galaxy is complete (only applicable for single star galaxies in any%, or buoy base)
            if (isGalaxyComplete)
            {
                return true;
            }

            //Checks if level gets coins.
            if (getCoin)
            {
                return true;
            }
            //checks if a galaxy or story chapter is unlocked on this star.
            for (int i = 0; i < 9; i++)
            {
                if (this.starNumber == starCountNotifTerrace[i])
                {
                    return true;
                }

                if (this.starNumber == starCountNotifFountain[i] && isFoutainUnlocked)
                {
                    return true;
                }

                if (this.starNumber == starCountNotifKitchen[i] && isKitchenUnlocked)
                {
                    return true;
                }

                if (this.starNumber == starCountNotifBedroom[i] && isBedroomUnlocked)
                {
                    return true;
                }

                if (this.starNumber == starCountNotifEngineRoom[i] && isEngineRoomUnlocked)
                {
                    return true;
                }

                if (this.starNumber == starCountNotifGarden[i] && isGardenUnlocked)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
