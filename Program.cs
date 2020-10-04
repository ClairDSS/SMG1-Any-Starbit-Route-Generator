using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;

namespace Starbit_Route_Generator
{
    class Program
    {
        //public variable that keeps track of current star count. Used for determining if there's textboxes.
        public static int currentStarCount = 0;

        //this variable is the amount extra frames the user wants to have from no-notification levels (ie, if 0, min starbits are 18)
        public static int minBitLeeway = 0;

        //this variable will tell how much leeway the player wants with starbits. For instance, do they collect 1030 or 1050 total?
        public static int totalBitLeeway = 0;

        //this variable is a special counter that keeps track if buoy base is completed,
        //as it is the only non-single star galaxy in the run that will be completed.
        public static int buoyBaseCounter = 0;

        //this variable keeps track of the total starbits the route currently has
        public static int currentTotalStarbits = 0;

        //this variable is the path of the splits
        public static string path;

        static void Main(string[] args)
        {
            //Declaring local Variables

            //Boolean just to make sure the user inputs the correct stuff
            bool canContinue = true;

            //Strings
            string[] route = new String[61];

            //gets information from user.

            Console.WriteLine("Hello! This program will create a starbit route for any SMG1 any% route you give it!\n" +
            "Please read the readme to know how to format your text file properly.\n");
            Console.Write("Please copy the path of your text file containing your route: ");
            path = InputColorChange();
            do
            {
                try
                {
                    Console.WriteLine("\nHow much leeway would you like to have in starbits throughout the run?\n" +
                        "If you choose 0, you may have to collect every single starbit in the route: ");
                    totalBitLeeway = int.Parse(InputColorChange());

                    canContinue = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please enter a whole number.\n");
                    canContinue = false;
                }
            } while (!canContinue);

            try
            {
                System.IO.StreamReader splitText = new StreamReader(path);
                for (int i = 0; i < 61; i++)
                {
                    route[i] = splitText.ReadLine().Trim();
                }
                splitText.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }

            //This method will fill the level list in the Star Info section with the order of the stars.
            PopulateLevelList(route);

            //Calls the method that will calculate the starbits for each star and prints out the final splits format
            CalculateLevelBits(route);

            Console.WriteLine("Your starbit route has been generated :)");
            Console.ReadKey();
        }

        public static void CalculateLevelBits(string[] route)
        {
            //declaring local variables

            //this is an array that tells how many starbits each star will collect.
            int[] starbitsCollected = new int[61];

            for (int i = 0; i < 61; i++)
            {
                //marks the level as complete and increments the star counter
                StarInfo.levelList[i].isStarComplete = true;
                currentStarCount++;

                //keeps track of the level's star number
                StarInfo.levelList[i].starNumber = currentStarCount;

                //this special counter checks if buoy base is completed.

                if (StarInfo.levelList[i] == StarInfo.buoybase1 || StarInfo.levelList[i] == StarInfo.buoybases)
                {
                    buoyBaseCounter += 1;
                    if (buoyBaseCounter == 2)
                    {
                        StarInfo.buoybase1.isGalaxyComplete = true;
                        StarInfo.buoybases.isGalaxyComplete = true;
                    }
                }

                if (!StarInfo.levelList[i].HasNotifs() && !SpecialNotifs(StarInfo.levelList[i], StarInfo.levelList[i - 1]))
                {
                    if (StarInfo.levelList[i].minBits < 18 - minBitLeeway)
                    {
                        StarInfo.levelList[i].collectedBits = StarInfo.levelList[i].minBits;
                    }
                    else
                    {
                        StarInfo.levelList[i].collectedBits = 18 - minBitLeeway;
                    }
                }


                else
                {
                    StarInfo.levelList[i].collectedBits = 0;
                }

                if (StarInfo.levelList[i].collectedBits <= 0)
                {
                    StarInfo.levelList[i].collectedBits = 0;
                }

                //this allows the program to check if a certain dome has been unlocked yet.
                if (StarInfo.levelList[i] == StarInfo.bowserjr1)
                {
                    Galaxy.isFoutainUnlocked = true;
                }

                if (StarInfo.levelList[i] == StarInfo.bowser1)
                {
                    Galaxy.isKitchenUnlocked = true;
                }

                if (StarInfo.levelList[i] == StarInfo.bowserjr2)
                {
                    Galaxy.isBedroomUnlocked = true;
                }

                if (StarInfo.levelList[i] == StarInfo.bowser2)
                {
                    Galaxy.isEngineRoomUnlocked = true;
                }

                if (StarInfo.levelList[i] == StarInfo.honeyclimb)
                {
                    StarInfo.honeyclimb.collectedBits = StarInfo.honeyclimb.maxBits;
                }

                if (StarInfo.levelList[i] == StarInfo.bowser3)
                {
                    StarInfo.bowser3.collectedBits = 69;
                }
                //this section will check if the player has enough starbits total. This tells the program whether to require minimum or maximum bits from a level.
                while (StarInfo.levelList[i] == StarInfo.slingpod && currentTotalStarbits < 400 + totalBitLeeway)
                {
                    CurrentMaxStarbitLevel(i).collectedBits = CurrentMaxStarbitLevel(i).maxBits;
                    currentTotalStarbits += CurrentMaxStarbitLevel(i).collectedBits;
                    CurrentMaxStarbitLevel(i).isStarComplete = false;
                }

                while (StarInfo.levelList[i] == StarInfo.sweetsweet && currentTotalStarbits < 400 + totalBitLeeway)
                {

                    CurrentMaxStarbitLevel(i).collectedBits = CurrentMaxStarbitLevel(i).maxBits;
                    currentTotalStarbits += CurrentMaxStarbitLevel(i).collectedBits;
                    CurrentMaxStarbitLevel(i).isStarComplete = false;

                }

                while (StarInfo.levelList[i] == StarInfo.dripdrop && currentTotalStarbits < 600 + totalBitLeeway)
                {

                    CurrentMaxStarbitLevel(i).collectedBits = CurrentMaxStarbitLevel(i).maxBits;
                    currentTotalStarbits += CurrentMaxStarbitLevel(i).collectedBits;
                    CurrentMaxStarbitLevel(i).isStarComplete = false;

                }

                //decrements starbit counter if luma is fed.
                if (StarInfo.levelList[i] == StarInfo.sweetsweet)
                {
                    currentTotalStarbits -= 400;
                }

                if (StarInfo.levelList[i] == StarInfo.dripdrop)
                {
                    currentTotalStarbits -= 600;
                }

                //stores how many bits are collected. Adds to the total current starbits.
                currentTotalStarbits += StarInfo.levelList[i].collectedBits;
            }

            for (int i = 0; i < 61; i++)
            {
                starbitsCollected[i] = StarInfo.levelList[i].collectedBits;
            }


            //Writes final route to text file.
            System.IO.StreamWriter splitText = new StreamWriter(path);
            for (int i = 0; i < 61; i++)
            {
                splitText.WriteLine("{0} ({1})", route[i], starbitsCollected[i]);
            }
            splitText.Close();

        }

        //this method checks for special galaxy unlock requirements. For instance, buoy base requires 30 stars and beach bowl 1.
        public static bool SpecialNotifs(Galaxy currentLevel, Galaxy previousLevel)
        {
            if (currentLevel == StarInfo.spacejunk3 ||
                currentLevel == StarInfo.beachbowl1 ||
                currentLevel == StarInfo.dustydune1 ||
                (currentLevel == StarInfo.ghostly1 && StarInfo.seaslide2.isStarComplete) ||
                (currentLevel == StarInfo.seaslide2 && StarInfo.ghostly1.isStarComplete) ||
                previousLevel == StarInfo.bowser1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Calculates which level should collect more than 18 starbits based on it's total amount of starbits. Prioritizes levels without notifs
        public static Galaxy CurrentMaxStarbitLevel(int k)
        {
            Galaxy currentMax = new Galaxy();
            int notifCounter = 0;

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (StarInfo.levelList[i].HasNotifs())
                        notifCounter++;
                }

                if (notifCounter != 0)
                {
                    if (StarInfo.levelList[i].maxBits > currentMax.maxBits && StarInfo.levelList[i].isStarComplete && !StarInfo.levelList[i].HasNotifs())
                    {
                        currentMax = StarInfo.levelList[i];
                    }
                }

                else
                {
                    if (StarInfo.levelList[i].maxBits > currentMax.maxBits && StarInfo.levelList[i].isStarComplete)
                    {
                        currentMax = StarInfo.levelList[i];
                    }
                }
            }

            return currentMax;
        }

        //this method populates the level list with the order of the levels input by the user.
        //This method is probably very inefficient, as I copy and paste code a whole lot, but it's the best solution I could find right now...
        //I AM a noob to coding, so I hope that at some point I can get around the copy-paste issue.
        public static void PopulateLevelList(string[] route)
        {
            //creates a new array that is for the sanitized version of each star written in
            string[] sanitizedRoute = new string[61];

            for (int i = 0; i < 61; i++)
            {
                sanitizedRoute[i] = route[i].ToLower().Trim();
            }

            //this loops through eeevery star in order to populate the list. Bad code, I know. Please don't bully me.
            for (int i = 0; i < 61; i++)
            {
                if (sanitizedRoute[i] == "gateway" || sanitizedRoute[i] == "gateway 1" || sanitizedRoute[i] == "gateway1")
                    StarInfo.levelList[i] = StarInfo.gateway;
                if (sanitizedRoute[i] == "goodegg1" || sanitizedRoute[i] == "goodegg 1" || sanitizedRoute[i] == "good egg 1")
                    StarInfo.levelList[i] = StarInfo.goodegg1;
                if (sanitizedRoute[i] == "goodegg2" || sanitizedRoute[i] == "goodegg 2" || sanitizedRoute[i] == "good egg 2")
                    StarInfo.levelList[i] = StarInfo.goodegg2;
                if (sanitizedRoute[i] == "honeyhive1" || sanitizedRoute[i] == "honeyhive 1" || sanitizedRoute[i] == "honey hive 1")
                    StarInfo.levelList[i] = StarInfo.honeyhive1;
                if (sanitizedRoute[i] == "honeyhive2" || sanitizedRoute[i] == "honeyhive 2" || sanitizedRoute[i] == "honey hive 2")
                    StarInfo.levelList[i] = StarInfo.honeyhive2;
                if (sanitizedRoute[i] == "honeyhive3" || sanitizedRoute[i] == "honeyhive 3" || sanitizedRoute[i] == "honey hive 3")
                    StarInfo.levelList[i] = StarInfo.honeyhive1;
                if (sanitizedRoute[i] == "loopdeeloop" || sanitizedRoute[i] == "loopdeloop")
                    StarInfo.levelList[i] = StarInfo.loopdeeloop;
                if (sanitizedRoute[i] == "bowser jr. 1" || sanitizedRoute[i] == "bowser jr 1" || sanitizedRoute[i] == "megaleg")
                    StarInfo.levelList[i] = StarInfo.bowserjr1;
                if (sanitizedRoute[i] == "space junk 1" || sanitizedRoute[i] == "spacejunk 1" || sanitizedRoute[i] == "spacejunk1")
                    StarInfo.levelList[i] = StarInfo.spacejunk1;
                if (sanitizedRoute[i] == "space junk 2" || sanitizedRoute[i] == "spacejunk 2" || sanitizedRoute[i] == "spacejunk2")
                    StarInfo.levelList[i] = StarInfo.spacejunk2;
                if (sanitizedRoute[i] == "rollinggreen" || sanitizedRoute[i] == "rolling green" || sanitizedRoute[i] == "rollinggreen1")
                    StarInfo.levelList[i] = StarInfo.rollinggreen;
                if (sanitizedRoute[i] == "battlerock 1" || sanitizedRoute[i] == "battlerock1" || sanitizedRoute[i] == "battle rock 1")
                    StarInfo.levelList[i] = StarInfo.battlerock1;
                if (sanitizedRoute[i] == "battlerock 2" || sanitizedRoute[i] == "battlerock2" || sanitizedRoute[i] == "battle rock 2")
                    StarInfo.levelList[i] = StarInfo.battlerock2;
                if (sanitizedRoute[i] == "space junk c" || sanitizedRoute[i] == "spacejunkc" || sanitizedRoute[i] == "spacejunk c")
                    StarInfo.levelList[i] = StarInfo.spacejunkc;
                if (sanitizedRoute[i] == "battlerock 3" || sanitizedRoute[i] == "battlerock3" || sanitizedRoute[i] == "battle rock 3")
                    StarInfo.levelList[i] = StarInfo.battlerock3;
                if (sanitizedRoute[i] == "battlerock c" || sanitizedRoute[i] == "battlerockc" || sanitizedRoute[i] == "battle rock c")
                    StarInfo.levelList[i] = StarInfo.battlerockc;
                if (sanitizedRoute[i] == "bowser 1" || sanitizedRoute[i] == "bowser1" || sanitizedRoute[i] == "star reactor")
                    StarInfo.levelList[i] = StarInfo.bowser1;
                if (sanitizedRoute[i] == "beachbowl 1" || sanitizedRoute[i] == "beachbowl1" || sanitizedRoute[i] == "beach bowl 1")
                    StarInfo.levelList[i] = StarInfo.beachbowl1;
                if (sanitizedRoute[i] == "beachbowl 2" || sanitizedRoute[i] == "beachbowl2" || sanitizedRoute[i] == "beach bowl 2")
                    StarInfo.levelList[i] = StarInfo.beachbowl2;
                if (sanitizedRoute[i] == "beachbowl s" || sanitizedRoute[i] == "beachbowls" || sanitizedRoute[i] == "beach bowl s")
                    StarInfo.levelList[i] = StarInfo.beachbowls;
                if (sanitizedRoute[i] == "ghostly1" || sanitizedRoute[i] == "ghostly 1")
                    StarInfo.levelList[i] = StarInfo.ghostly1;
                if (sanitizedRoute[i] == "sweet sweet" || sanitizedRoute[i] == "sweetsweet" || sanitizedRoute[i] == "sweetsweet 1")
                    StarInfo.levelList[i] = StarInfo.sweetsweet;
                if (sanitizedRoute[i] == "honeyhive c" || sanitizedRoute[i] == "honey hive c" || sanitizedRoute[i] == "honeyhivec")
                    StarInfo.levelList[i] = StarInfo.honeyhivec;
                if (sanitizedRoute[i] == "good egg l" || sanitizedRoute[i] == "goodegg l" || sanitizedRoute[i] == "goodeggl")
                    StarInfo.levelList[i] = StarInfo.goodeggl;
                if (sanitizedRoute[i] == "beachbowl 3" || sanitizedRoute[i] == "beachbowl3" || sanitizedRoute[i] == "beach bowl 3")
                    StarInfo.levelList[i] = StarInfo.beachbowl3;
                if (sanitizedRoute[i] == "beachbowl c" || sanitizedRoute[i] == "beachbowlc" || sanitizedRoute[i] == "beach bowl c")
                    StarInfo.levelList[i] = StarInfo.beachbowlc;
                if (sanitizedRoute[i] == "ghostlys" || sanitizedRoute[i] == "ghostly s")
                    StarInfo.levelList[i] = StarInfo.ghostlys;
                if (sanitizedRoute[i] == "bubblebreeze" || sanitizedRoute[i] == "bubble breeze")
                    StarInfo.levelList[i] = StarInfo.bubblebreeze;
                if (sanitizedRoute[i] == "bowserjr.2" || sanitizedRoute[i] == "bowser jr. 2" || sanitizedRoute[i] == "airship armada")
                    StarInfo.levelList[i] = StarInfo.bowserjr2;
                if (sanitizedRoute[i] == "hurry scurry" || sanitizedRoute[i] == "hurryscurry")
                    StarInfo.levelList[i] = StarInfo.hurryscurry;
                if (sanitizedRoute[i] == "battlerock l" || sanitizedRoute[i] == "battlerockl" || sanitizedRoute[i] == "battle rock l")
                    StarInfo.levelList[i] = StarInfo.battlerockl;
                if (sanitizedRoute[i] == "freezeflame 1" || sanitizedRoute[i] == "freeze flame 1" || sanitizedRoute[i] == "freezeflame1")
                    StarInfo.levelList[i] = StarInfo.freezeflame1;
                if (sanitizedRoute[i] == "freezeflame 2" || sanitizedRoute[i] == "freeze flame 2" || sanitizedRoute[i] == "freezeflame2")
                    StarInfo.levelList[i] = StarInfo.freezeflame2;
                if (sanitizedRoute[i] == "gusty garden 1" || sanitizedRoute[i] == "gustygarden 1" || sanitizedRoute[i] == "gustygarden1")
                    StarInfo.levelList[i] = StarInfo.gustygarden1;
                if (sanitizedRoute[i] == "freezeflame 3" || sanitizedRoute[i] == "freeze flame 3" || sanitizedRoute[i] == "freezeflame3")
                    StarInfo.levelList[i] = StarInfo.freezeflame3;
                if (sanitizedRoute[i] == "gusty garden 2" || sanitizedRoute[i] == "gustygarden2" || sanitizedRoute[i] == "gustygarden 2")
                    StarInfo.levelList[i] = StarInfo.gustygarden2;
                if (sanitizedRoute[i] == "freezeflame c" || sanitizedRoute[i] == "freezeflamec" || sanitizedRoute[i] == "freeze flame c")
                    StarInfo.levelList[i] = StarInfo.freezeflamec;
                if (sanitizedRoute[i] == "gusty garden 3" || sanitizedRoute[i] == "gustygarden 3" || sanitizedRoute[i] == "gustygarden3")
                    StarInfo.levelList[i] = StarInfo.gustygarden3;
                if (sanitizedRoute[i] == "gustygardenc" || sanitizedRoute[i] == "gusty garden c" || sanitizedRoute[i] == "gustygarden c")
                    StarInfo.levelList[i] = StarInfo.gustygardenc;
                if (sanitizedRoute[i] == "honeyclimb" || sanitizedRoute[i] == "honey climb")
                    StarInfo.levelList[i] = StarInfo.honeyclimb;
                if (sanitizedRoute[i] == "gusty garden s" || sanitizedRoute[i] == "gustygarden s" || sanitizedRoute[i] == "gustygardens")
                    StarInfo.levelList[i] = StarInfo.gustygardens;
                if (sanitizedRoute[i] == "bowser 2" || sanitizedRoute[i] == "bowser2" || sanitizedRoute[i] == "dark matter plant")
                    StarInfo.levelList[i] = StarInfo.bowser2;
                if (sanitizedRoute[i] == "goldleaf1" || sanitizedRoute[i] == "goldleaf 1" || sanitizedRoute[i] == "gold leaf 1")
                    StarInfo.levelList[i] = StarInfo.goldleaf1;
                if (sanitizedRoute[i] == "goldleaf2" || sanitizedRoute[i] == "goldleaf 2" || sanitizedRoute[i] == "gold leaf 2")
                    StarInfo.levelList[i] = StarInfo.goldleaf2;
                if (sanitizedRoute[i] == "goldleaf3" || sanitizedRoute[i] == "goldleaf 3" || sanitizedRoute[i] == "gold leaf 3")
                    StarInfo.levelList[i] = StarInfo.goldleaf3;
                if (sanitizedRoute[i] == "toytime 1" || sanitizedRoute[i] == "toytime1" || sanitizedRoute[i] == "toy time 1")
                    StarInfo.levelList[i] = StarInfo.toytime1;
                if (sanitizedRoute[i] == "gold leaf c" || sanitizedRoute[i] == "goldleaf c" || sanitizedRoute[i] == "goldleafc")
                    StarInfo.levelList[i] = StarInfo.goldleafc;
                if (sanitizedRoute[i] == "seaslide 1" || sanitizedRoute[i] == "sea slide 1" || sanitizedRoute[i] == "seaslide1")
                    StarInfo.levelList[i] = StarInfo.seaslide1;
                if (sanitizedRoute[i] == "toytime 2" || sanitizedRoute[i] == "toy time 2" || sanitizedRoute[i] == "toytime2")
                    StarInfo.levelList[i] = StarInfo.toytime2;
                if (sanitizedRoute[i] == "seaslide 2" || sanitizedRoute[i] == "sea slide 2" || sanitizedRoute[i] == "seaslide2")
                    StarInfo.levelList[i] = StarInfo.seaslide2;
                if (sanitizedRoute[i] == "seaslide c" || sanitizedRoute[i] == "sea slide c" || sanitizedRoute[i] == "seaslidec")
                    StarInfo.levelList[i] = StarInfo.seaslidec;
                if (sanitizedRoute[i] == "toytime s" || sanitizedRoute[i] == "toytimes" || sanitizedRoute[i] == "toy time s")
                    StarInfo.levelList[i] = StarInfo.toytimes;
                if (sanitizedRoute[i] == "toytime c" || sanitizedRoute[i] == "toytimec" || sanitizedRoute[i] == "toy time c")
                    StarInfo.levelList[i] = StarInfo.toytimec;
                if (sanitizedRoute[i] == "goldleaf s" || sanitizedRoute[i] == "goldleafs" || sanitizedRoute[i] == "gold leaf s")
                    StarInfo.levelList[i] = StarInfo.goldleafs;
                if (sanitizedRoute[i] == "buoy base 1" || sanitizedRoute[i] == "buoybase 1" || sanitizedRoute[i] == "buoybase1")
                    StarInfo.levelList[i] = StarInfo.buoybase1;
                if (sanitizedRoute[i] == "buoy base s" || sanitizedRoute[i] == "buoybase s" || sanitizedRoute[i] == "buoybases")
                    StarInfo.levelList[i] = StarInfo.buoybases;
                if (sanitizedRoute[i] == "drip drop" || sanitizedRoute[i] == "dripdrop")
                    StarInfo.levelList[i] = StarInfo.dripdrop;
                if (sanitizedRoute[i] == "honeyhive l" || sanitizedRoute[i] == "honey hive l" || sanitizedRoute[i] == "honeyhivel")
                    StarInfo.levelList[i] = StarInfo.honeyhivel;
                if (sanitizedRoute[i] == "slingpod" || sanitizedRoute[i] == "sling pod")
                    StarInfo.levelList[i] = StarInfo.slingpod;
                if (sanitizedRoute[i] == "freezeflame s" || sanitizedRoute[i] == "freeze flame s")
                    StarInfo.levelList[i] = StarInfo.freezeflames;
                if (sanitizedRoute[i] == "flipswitch" || sanitizedRoute[i] == "flip switch")
                    StarInfo.levelList[i] = StarInfo.flipswitch;
                if (sanitizedRoute[i] == "space junk 3" || sanitizedRoute[i] == "spacejunk 3")
                    StarInfo.levelList[i] = StarInfo.spacejunk3;
                if (sanitizedRoute[i] == "ghostly 2" || sanitizedRoute[i] == "ghostly2")
                    StarInfo.levelList[i] = StarInfo.ghostly2;
                if (sanitizedRoute[i] == "bowser 3" || sanitizedRoute[i] == "bowser3")
                    StarInfo.levelList[i] = StarInfo.bowser3;

            }

            //this checks if one of the levels put in do not corrospond to a level written here.
            for (int i = 0; i < 61; i++)
            {
                if (StarInfo.levelList[i] == null)
                {
                    Console.WriteLine("\n{0} was formatted incorrectly. Please try again.", route[i]);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
        }
        //this method will simply change the color of the user's input.
        public static string InputColorChange()
        {
            string input;
            Console.ForegroundColor = ConsoleColor.Red;
            input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            return input;
        }
    }
}
