using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace Starbit_Route_Generator
{
    class Galaxy
    {
        //declare variables

        //Array of star counts that result in a "new galaxy" textbox
        public static int[] starCountNotifTerrace = { 1, 3, 5, 7, 8, 60, -1 };
        public static int[] starCountNotifFountain = { 9, 11, 12, 15, -1, -1, -1 };
        public static int[] starCountNotifKitchen = { 16, 18, 19, 20, 23, 30, -1 };
        public static int[] starCountNotifBedroom = { 24, 26, 29, 33, 42, -1, -1 };
        public static int[] starCountNotifEngineRoom = { 34, 36, 40, 45, -1, -1, -1 };
        public static int[] starCountNotifGarden = { 46, 48, 52, -1, -1, -1, -1 };

        //Array of star courns that result in a "new chapter" textbox
        public static int[] storyBookNotif = { 24, 28, 32, 40, 49, 58, -1 };

        //booleans that check conditions to see if a texbox will appear. Specifically, star specific checks.
        public bool unlockGalaxy;
        public bool getCoin;
        public bool isStarComplete = false;
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

        //boolean that checks to see if a star has already collected all of its possible starbits.
        public bool hasStarbitsCollected = false;

        //this boolean is especially for hungry luma galaxies. This keeps track of if they've all been fed or not.
        //Only set to false if it's in level list.
        public bool hasBeenFed = true;

        //this is the galaxy's reason for having a notification
        public string[] reason = new string [5];

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
            //checks if a galaxy or story chapter is unlocked on this star.
            for (int i = 0; i < 7; i++)
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
                
                if (this.starNumber == storyBookNotif[i] && isKitchenUnlocked)
                {
                    return true;
                }
            }

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
            return false;
        }

        //Similar to the "HasNotifs" method, but returns what notification it has instead of if it has a notification
        public void WhatNotifs()
        {

            //checks if a galaxy or story chapter is unlocked on this star.
            for (int i = 0; i < 7; i++)
            {
                if (this.starNumber == starCountNotifTerrace[i])
                {
                    this.reason[2] = "G,";
                }

                if (this.starNumber == starCountNotifFountain[i] && isFoutainUnlocked)
                {
                    this.reason[2] = "G,";
                }

                if (this.starNumber == starCountNotifKitchen[i] && isKitchenUnlocked)
                {
                    this.reason[2] = "G,";
                }

                if (this.starNumber == starCountNotifBedroom[i] && isBedroomUnlocked)
                {
                    this.reason[2] = "G,";
                }

                if (this.starNumber == starCountNotifEngineRoom[i] && isEngineRoomUnlocked)
                {
                    this.reason[2] = "G,";
                }

                if (this.starNumber == starCountNotifGarden[i] && isGardenUnlocked)
                {
                    this.reason[2] = "G,";
                }

                if (this.starNumber == storyBookNotif[i] && isKitchenUnlocked)
                {
                    this.reason[3] = "R,";
                }
            }

            //Checks if galaxy is complete (only applicable for single star galaxies in any%, or buoy base)
            if (isGalaxyComplete)
            {
                this.reason[0] = "C,";
            }

            //Checks if level gets coins.
            if (getCoin)
            {
                this.reason[1] = "Co,";
            }
        }

    }
}
