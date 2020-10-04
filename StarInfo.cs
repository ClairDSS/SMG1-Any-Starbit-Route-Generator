using System;
using System.Collections.Generic;
using System.Text;

namespace Starbit_Route_Generator
{
    //this class is here to define each galaxy as an object. I didn't want to clog up main with 120 lines of this.
    class StarInfo
    {
        public static Galaxy gateway = new Galaxy(16, 16,false,false);
        public static Galaxy goodegg1 = new Galaxy(18, 50, false, false);
        public static Galaxy goodegg2 = new Galaxy(6, 6, false, false);
        public static Galaxy goodegg3 = new Galaxy(18, 69, false, false);
        public static Galaxy honeyhive1 = new Galaxy(18, 36, false, false);
        public static Galaxy honeyhive2 = new Galaxy(18, 49, false, false);
        public static Galaxy honeyhive3 = new Galaxy(18, 59, false, false);
        public static Galaxy loopdeeloop = new Galaxy(14, 14, true, true);
        public static Galaxy flipswitch = new Galaxy(0, 0, false, true);
        public static Galaxy bowserjr1 = new Galaxy(18, 32, false, true);
        public static Galaxy spacejunk1 = new Galaxy(13, 13, true, false);
        public static Galaxy spacejunk2 = new Galaxy(18, 35, false, false);
        public static Galaxy rollinggreen = new Galaxy(18, 19, true, true);
        public static Galaxy battlerock1 = new Galaxy(18, 88, false, false);
        public static Galaxy battlerock2 = new Galaxy(18, 38, false, false);
        public static Galaxy spacejunkc = new Galaxy(17, 17, true, false);
        public static Galaxy battlerock3 = new Galaxy(18, 51, true, false);
        public static Galaxy battlerockc = new Galaxy(18, 30, false, false);
        public static Galaxy bowser1 = new Galaxy(18, 25, true, true);
        public static Galaxy beachbowl1 = new Galaxy(18, 20, false, false);
        public static Galaxy beachbowl2 = new Galaxy(18, 26, false, false);
        public static Galaxy beachbowls = new Galaxy(18, 52, false, false);
        public static Galaxy ghostly1 = new Galaxy(17, 17, false, false);
        public static Galaxy sweetsweet = new Galaxy(18, 69, false, true);
        public static Galaxy honeyhivec = new Galaxy(0, 0, false, false);
        public static Galaxy goodeggl = new Galaxy(2, 2, false, false);
        public static Galaxy beachbowl3 = new Galaxy(18, 37, false, false);
        public static Galaxy beachbowlc = new Galaxy(5, 5, false, false);
        public static Galaxy ghostlys = new Galaxy(4, 4, false, false);
        public static Galaxy bubblebreeze = new Galaxy(18, 54, false, true);
        public static Galaxy bowserjr2 = new Galaxy(18, 34, false, true);
        public static Galaxy hurryscurry = new Galaxy(17, 17, false, true);
        public static Galaxy battlerockl = new Galaxy(18, 78, false, false);
        public static Galaxy freezeflame1 = new Galaxy(18, 32, false, false);
        public static Galaxy freezeflame2 = new Galaxy(17, 17, false, false);
        public static Galaxy gustygarden1 = new Galaxy(18, 58, false, false);
        public static Galaxy freezeflame3 = new Galaxy(18, 58, false, false);
        public static Galaxy gustygarden2 = new Galaxy(18, 20, false, false);
        public static Galaxy freezeflamec = new Galaxy(18, 31, false, false);
        public static Galaxy gustygarden3 = new Galaxy(18, 78, false, false);
        public static Galaxy gustygardenc = new Galaxy(18, 20, false, false);
        public static Galaxy honeyclimb = new Galaxy(18, 180, false, true);
        public static Galaxy gustygardens = new Galaxy(18, 57, true, false);
        public static Galaxy bowser2 = new Galaxy(18, 20, true, true);
        public static Galaxy goldleaf1 = new Galaxy(18, 20, false, false);
        public static Galaxy goldleaf2 = new Galaxy(18, 35, false, false);
        public static Galaxy goldleaf3 = new Galaxy(18, 55, false, false);
        public static Galaxy toytime1 = new Galaxy(18, 94, false, false);
        public static Galaxy goldleafc = new Galaxy(7, 7, false, false);
        public static Galaxy seaslide1 = new Galaxy(18, 43, false, false);
        public static Galaxy toytime2 = new Galaxy(18, 75, false, false);
        public static Galaxy seaslide2 = new Galaxy(18, 19, true, false);
        public static Galaxy seaslidec = new Galaxy(16, 16, false, false);
        public static Galaxy toytimes = new Galaxy(13, 13, false, false);
        public static Galaxy toytimec = new Galaxy(4, 4, false, false);
        public static Galaxy goldleafs = new Galaxy(9, 9, false, false);
        public static Galaxy buoybase1 = new Galaxy(18, 41, false, false);
        public static Galaxy buoybases = new Galaxy(18, 30, false, false);
        public static Galaxy dripdrop = new Galaxy(1, 1, false, true);
        public static Galaxy honeyhivel = new Galaxy(18, 20, false, false);
        public static Galaxy slingpod = new Galaxy(18, 100, false, true);
        public static Galaxy freezeflames = new Galaxy(18, 46, false, false);
        public static Galaxy spacejunk3 = new Galaxy(18, 50, false, false);
        public static Galaxy ghostly2 = new Galaxy(15, 15, false, false);
        public static Galaxy dustydune1 = new Galaxy();
        public static Galaxy bowser3 = new Galaxy(69, 69, false, false);

        //this is a list of each star, populated during runtime
        public static Galaxy[] levelList = new Galaxy[61];
    }
}
