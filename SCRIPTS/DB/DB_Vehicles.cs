////////////////////////////////////////////////////////////////////////////////
//
//	DB_Vehicles.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://pinetools.com/add-text-each-line
//https://wiki.gtanet.work/index.php?title=Weapons_Models
//https://wiki.gtanet.work/index.php/Peds
//https://cdn.rage.mp/public/odb/index.html#25

namespace MG_Liquidator
{
    public static class DB_Vehicles
    {
        //https://gta.fandom.com/wiki/Vehicles_in_GTA_V

        //https://gta.fandom.com/wiki/Dilettante#Variants
        //https://gta.fandom.com/wiki/Insurgent		
        //https://gta.fandom.com/wiki/Weaponized_Vehicles


        public static string[] BodyGuard_Normal { get; } =
        {
             //--COMPACT Begin
            "asbo",
            "blista",
            "brioso",
            "club",
            "dilettante",
            "issi2",
            "kanjo",
            "panto",
            "prairie",
            "rhapsody",
            //--COMPACT End


             //COUPE
            "cogcabrio",
            "exemplar",
            "f620",
            "felon",
            "felon2",
            "jackal",
            "oracle",
            "oracle2",
            "sentinel",
            "sentinel2",
            "windsor",
            "windsor2",
            "zion",
            "zion2",
            //COUPE

            //MUSCLE (NORMAL)
            "buccaneer2",
            "impaler",
            "impaler3",
            "imperator",
            "imperator2",
            "lurcher",
            "moonbeam",
            "moonbeam2",
            "picador",
            "ruiner",
            "sabregt",
            "sabregt2",
            "tulip",
            "vamos",
            "vigero",
            "virgo",
            "yosemite",
            //MUSCLE (NORMAL)

            //OFF_ROAD (SPORT)
            "brawler",
            //OFF_ROAD (SPORT)

             //SEDAN (NORMAL)
            "asea",
            "asterope",
            "cog55",
            "cognoscenti",
            "fugitive",
            "glendale",
            "glendale2",
            "ingot",
            "intruder",
            "premier",
            "primo",
            "primo2",
            "regina",
            "schafter2",
            "stanier",
            "stratum",
            "superd",
            "surge",
            "tailgater",
            "warrener",
            "washington",
            //SEDAN (NORMAL)

            //NORMAL SPORT
            "blista2",
            "futo",
            "gb200",
            "issi7",
            "jester3",
            "jugular",
            "komoda",
            "kuruma",
            "paragon",
            "penumbra",
            "penumbra",
            "raiden",
            "rapidgt",
            "rapidgt2",
            "revolter",
            "schafter3",
            "schafter4",
            "schlagen",
            "schwarzer",
            "sentinel3",
            "sultan2",
            //NORMAL SPORT

            //SPORT
            "alpha",
            "banshee",
            "bestiagts",
            "buffalo",
            "buffalo2",
            "carbonizzare",
            "comet2",
            "comet3",
            "comet4",
            "comet5",
            "coquette",
            "drafter",
            "elegy",
            "elegy2",
            "feltzer2",
            "flashgt",
            "furoregt",
            "fusilade",
            "imorgon",
            "italigto",
            "khamelion",
            "lynx",
            "massacro",
            "massacro2",
            "omnis",
            "pariah",
            "seven70",
            "specter",
            "specter2",
            "sugoi",
            "sultan",
            "surano",
            "verlierer2",
            "vstr",
            //SPORT

            //SPORT_CLASSIC NORMAL
            "ardent",
            "casco",
            "casco",
            "cheetah2",
            "coquette2",
            //"deluxo",
            "feltzer3",
            "gt500",
            "jb700",
            "michelli",
            "monroe",
            "nebula",
            "rapidgt3",
            "retinue",
            "retinue2",
            "savestra",
            "toreador",
            "viseris",
            "z190",
            "zion3",
            //SPORT_CLASSIC NORMAL

             //SUV
            "baller",
            "baller2",
            "baller3",
            "baller4",
            "baller5",
            "baller6",
            "bjxl",
            "cavalcade",
            "cavalcade2",
            "contender",
            "dubsta",
            "dubsta2",
            "fq2",
            "granger",
            "gresley",
            "habanero",
            "huntley",
            "landstalker",
            "landstalker2",
            "mesa",
            "novak",
            "patriot",
            "radi",
            "rebla",
            "rocoto",
            "seminole",
            "seminole2",
            "serrano",
            "squaddie",
            "toros",
            "xls"
            //SUV
        };

        public static string[] BodyGuard_Assasin { get; } =
        {
             //COUPE
            "cogcabrio",
            "exemplar",
            "f620",
            "felon",
            "felon2",
            "jackal",
            "oracle",
            "oracle2",
            "sentinel",
            "sentinel2",
            "windsor",
            "windsor2",
            "zion",
            "zion2",
            //COUPE

            //MOTORCYCLE (HARLEY)
            "avarus",
            "bagger",
            "daemon",
            "cliffhanger",       
            //MOTORCYCLE (HARLEY)

            //MOTORCYCLE (COOL)
            "akuma",
            "bati",
            "carbonrs",
            "hakuchou",      
            //MOTORCYCLE (COOL)

            //MUSCLE (SPORT)
            "deviant",
            "dominator",
            "dominator3",
            "ellie",
            "faction",
            "faction2",
            "gauntlet",
            "gauntlet4",
            "gauntlet5",
            "ruiner2",
            //MUSCLE (SPORT)

            //OFF_ROAD (SPORT)
            "brawler",
            //OFF_ROAD (SPORT)

            //SUPER SPORT
            "coquette4",
            "jester",
            "locust",
            "neo",
            "ninef",
            "ninef2",
            "ruston",
            //SUPER SPORT

            //SPORT_CLASSIC NORMAL
            "ardent",
            "casco",
            "casco",
            "cheetah2",
            "coquette2",
            //"deluxo",
            "feltzer3",
            "gt500",
            "jb700",
            "michelli",
            "monroe",
            "nebula",
            "rapidgt3",
            "retinue",
            "retinue2",
            "savestra",
            "toreador",
            "viseris",
            "z190",
            "zion3",
            //SPORT_CLASSIC NORMAL

            //SPORT_CLASSIC SUPER
            "infernus2",
            "jb7002",
            "mamba",
            "stinger",
            "stingergt",
            //"stromberg",
            "swinger",
            "torero",
            "turismo2",
            "ztype"
            //SPORT_CLASSIC SUPER
         
        };

        public static string[] BodyGuard_Police { get; } =
        {   //EMERGENCY
            "dilettante2",
             "fbi",
            "fbi2",
            "police",
            "police2",
            "police3",
            "police4",
            "policeb",
            "policet"
            //EMERGENCY



         
        };

        public static string[] BodyGuard_Cartel { get; } =
        {
             "stockade",
           
            //COUPE
            "cogcabrio",
            "exemplar",
            "f620",
            "felon",
            "felon2",
            "jackal",
            "oracle",
            "oracle2",
            "sentinel",
            "sentinel2",
            "windsor",
            "windsor2",
            "zion",
            "zion2",
            //COUPE

            //MOTORCYCLE (MOTOCROSS)
            "enduro",
            "esskey",
            "manchez",
            "manchez2",       
            //MOTORCYCLE (MOTOCROSS)

            //MOTORCYCLE (COOL)
            "akuma",
            "bati",
            "carbonrs",
            "hakuchou",      
            //MOTORCYCLE (COOL)

            //MUSCLE (SPORT)
            "deviant",
            "dominator",
            "dominator3",
            "ellie",
            "faction",
            "faction2",
            "gauntlet",
            "gauntlet4",
            "gauntlet5",
            "ruiner2",
            //MUSCLE (SPORT)

             //MUSCLE (CARTEL)
            "dominator4",
            "dukes2",
            "impaler2",
            //MUSCLE (CARTEL)

            //OFF_ROAD ATW
            "bifta",
            "blazer",
            "blazer2",
            "blazer3",
            "blazer4",
            "blazer5",
            "verus",
            "vagrant",
            //OFF_ROAD ATW

             //OFF_ROAD (SPORT)
            "brawler",
            //OFF_ROAD (SPORT)

            //SEDAN (CARTEL)
            "cog552",
            "cognoscenti2",
            "schafter5",
            "schafter6",
            //SEDAN (CARTEL)

            //SPORT CARTEL
            "kuruma2",
            "paragon2",
            "zr380",
            "zr3802",
            "tropos",
            //SPORT CARTEL

              //SPORT
            "alpha",
            "banshee",
            "bestiagts",
            "buffalo",
            "buffalo2",
            "carbonizzare",
            "comet2",
            "comet3",
            "comet4",
            "comet5",
            "coquette",
            "drafter",
            "elegy",
            "elegy2",
            "feltzer2",
            "flashgt",
            "furoregt",
            "fusilade",
            "imorgon",
            "italigto",
            "khamelion",
            "lynx",
            "massacro",
            "massacro2",
            "omnis",
            "pariah",
            "seven70",
            "specter",
            "specter2",
            "sugoi",
            "sultan",
            "surano",
            "verlierer2",
            "vstr",
            //SPORT

            //VAN CARTEL
             //"burrito5"
            //VAN CARTEL
        };

        public static string[] BodyGuard_Hacker { get; } =
        {
              //--COMPACT Begin
            "asbo",
            "blista",
            "brioso",
            "club",
            "dilettante",
            "issi2",
            "kanjo",
            "panto",
            "prairie",
            "rhapsody",
            //--COMPACT End

            //VERY SMALL CARS (STRANGE)
            "brioso2",
            "issi3",
            "issi4",
            "issi5",
            "issi6",
            "weevil",
            //VERY SMALL CARS (STRANGE)

            //COUPE
            "cogcabrio",
            "exemplar",
            "f620",
            "felon",
            "felon2",
            "jackal",
            "oracle",
            "oracle2",
            "sentinel",
            "sentinel2",
            "windsor",
            "windsor2",
            "zion",
            "zion2",
            //COUPE

            "guardian",

            //MOTORCYCLE (MOTOCROSS)
            "enduro",
            "esskey",
            "manchez",
            "manchez2",       
            //MOTORCYCLE (MOTOCROSS)

            //MUSCLE (SPORT)
            "deviant",
            "dominator",
            "dominator3",
            "ellie",
            "faction",
            "faction2",
            "gauntlet",
            "gauntlet4",
            "gauntlet5",
            "ruiner2",
            //MUSCLE (SPORT)

            //MUSCLE (NORMAL)
            "buccaneer2",
            "impaler",
            "impaler3",
            "imperator",
            "imperator2",
            "lurcher",
            "moonbeam",
            "moonbeam2",
            "picador",
            "ruiner",
            "sabregt",
            "sabregt2",
            "tulip",
            "vamos",
            "vigero",
            "virgo",
            "yosemite",
            //MUSCLE (NORMAL)

             //OFF_ROAD (SPORT)
            "brawler",
            //OFF_ROAD (SPORT)

            //SEDAN (NORMAL)
            "asea",
            "asterope",
            "cog55",
            "cognoscenti",
            "fugitive",
            "glendale",
            "glendale2",
            "ingot",
            "intruder",
            "premier",
            "primo",
            "primo2",
            "regina",
            "schafter2",
            "stanier",
            "stratum",
            "superd",
            "surge",
            "tailgater",
            "warrener",
            "washington",
            //SEDAN (NORMAL)


             //SPORT
            "alpha",
            "banshee",
            "bestiagts",
            "buffalo",
            "buffalo2",
            "carbonizzare",
            "comet2",
            "comet3",
            "comet4",
            "comet5",
            "coquette",
            "drafter",
            "elegy",
            "elegy2",
            "feltzer2",
            "flashgt",
            "furoregt",
            "fusilade",
            "imorgon",
            "italigto",
            "khamelion",
            "lynx",
            "massacro",
            "massacro2",
            "omnis",
            "pariah",
            "seven70",
            "specter",
            "specter2",
            "sugoi",
            "sultan",
            "surano",
            "verlierer2",
            "vstr",
            //SPORT

            //SPORT_CLASSIC NORMAL
            "ardent",
            "casco",
            "casco",
            "cheetah2",
            "coquette2",
            //"deluxo",
            "feltzer3",
            "gt500",
            "jb700",
            "michelli",
            "monroe",
            "nebula",
            "rapidgt3",
            "retinue",
            "retinue2",
            "savestra",
            "toreador",
            "viseris",
            "z190",
            "zion3",
            //SPORT_CLASSIC NORMAL


             //SPORT_CLASSIC SUPER
            "infernus2",
            "jb7002",
            "mamba",
            "stinger",
            "stingergt",
            //"stromberg",
            "swinger",
            "torero",
            "turismo2",
            "ztype",
            //SPORT_CLASSIC SUPER

             //SUPER  MODERN
            "adder",
            "autarch",
            "banshee2",
            "bullet",
            "cheetah",
            "cyclone",
            "deveste",
            "emerus",
            "entity2",
            "entity2",
            "entityxf",
            "fmj",
            "furia",
            "gp1",
            "infernus",
            "italigtb",
            "italigtb2",
            "krieger",
            "le7b",
            "nero",
            "nero2",
            "osiris",
            "penetrator",
            "pfister811",
            "prototipo",
            "reaper",
            "s80",
            "sc1",
            //"scramjet",
            "sheava",
            "sultanrs",
            "t20",
            "taipan",
            "tempesta",
            "tezeract",
            "thrax",
            "tigon",
            "turismor",
            "tyrant",
            "tyrus",
            "vacca",
            "vagner",
            //"vigilante",
            "visione",
            "voltic",
            "voltic2",
            "xa21",
            "zentorno",
            "zorrusso"
            //SUPER  MODERN
        };

        public static string[] BodyGuard_Military { get; } =
        {
            //MILITARY
             "barracks",
             "barracks",
             "barracks",
             "barracks",
             "barracks",
            "barracks2",
            "barracks3",
            "crusader",
             "barracks",
            "barracks2",
            "barracks3",
            "crusader",
             "barracks",
            "barracks2",
            "barracks3",
            "crusader"
   
            //MILITARY

            //MOTORCYCLE (MOTOCROSS)
            //"enduro",
            //"esskey",
            //"manchez",
            //"manchez2",       
            //MOTORCYCLE (MOTOCROSS)

            //OFF_ROAD ATW
            //"bifta",
            //"blazer",
            //"blazer2",
            //"blazer3",
            //"blazer4",
            //"blazer5",
            //"verus",
            //"vagrant"
            //OFF_ROAD ATW

      
        };

        public static string[] BodyGuard_GangMember { get; } =
        {
           
             //COUPE
            "cogcabrio",
            "exemplar",
            "f620",
            "felon",
            "felon2",
            "jackal",
            "oracle",
            "oracle2",
            "sentinel",
            "sentinel2",
            "windsor",
            "windsor2",
            "zion",
            "zion2",
            //COUPE

            //CYCLE
            "bmx",
            "cruiser",
            "fixter",
            "scorcher",
            "tribike",
            "tribike2",
            "tribike3",
            //CYCLE

            
            //MOTORCYCLE (POOR)
            "faggio",
            "faggio2",
            "faggio3",      
            //MOTORCYCLE (POOR)

            //MOTORCYCLE (MOTOCROSS)
            "enduro",
            "esskey",
            "manchez",
            "manchez2",       
            //MOTORCYCLE (MOTOCROSS)

            //MUSCLE (SPORT)
            "deviant",
            "dominator",
            "dominator3",
            "ellie",
            "faction",
            "faction2",
            "gauntlet",
            "gauntlet4",
            "gauntlet5",
            "ruiner2",
            //MUSCLE (SPORT)

            //MUSCLE (GANG)
            "blade",
            "buccaneer",
            "chino",
            "chino2",
            "dukes",
            "faction3",
            "gauntlet3",
            "hermes",
            "hotknife",
            "manana2",
            "nightshade",
            "peyote2",
            "phoenix",
            "ratloader",
            "stalion",
            "tampa",
            "virgo2",
            "virgo3",
            "voodoo",
            "voodoo2",
            //MUSCLE (GANG)

             //OFF_ROAD ATW
            "bifta",
            "blazer",
            "blazer2",
            "blazer3",
            "blazer4",
            "blazer5",
            "verus",
            "vagrant",
            //OFF_ROAD ATW

             //OFF_ROAD
            "brutus",
            "brutus2",
            "caracara2",
            "dune",
            "everon",
            "freecrawler",
            "hellion",
            "kalahari",
            "kamacho",
            "marshall",
            "mesa3",
            "rancherxl",
            "rebel",
            "rebel2",
            "riata",
            "sandking",
            "sandking2",
            "winky",
            "yosemite3",
            //OFF_ROAD

            //OFF_ROAD (OLD)
            "bfinjection",
            "bodhi2",
            "dloader",
            //OFF_ROAD (OLD)

             //SEDAN (GANG)
            "emperor",
            "emperor2",
            //SEDAN (GANG)
         

            //NORMAL SPORT
            "blista2",
            "futo",
            "gb200",
            "issi7",
            "jester3",
            "jugular",
            "komoda",
            "kuruma",
            "paragon",
            "penumbra",
            "penumbra",
            "raiden",
            "rapidgt",
            "rapidgt2",
            "revolter",
            "schafter3",
            "schafter4",
            "schlagen",
            "schwarzer",
            "sentinel3",
            "sultan2",
            //NORMAL SPORT

            //SPORT_CLASSIC GANG
            "manana",
            "peyote",
            "peyote3",
            "pigalle",
            "tornado",
            "tornado2",
            "tornado3",
            "tornado4",
            "tornado5",
            "tornado6",
            //SPORT_CLASSIC GANG

             //SUV
            "baller",
            "baller2",
            "baller3",
            "baller4",
            "baller5",
            "baller6",
            "bjxl",
            "cavalcade",
            "cavalcade2",
            "contender",
            "dubsta",
            "dubsta2",
            "fq2",
            "granger",
            "gresley",
            "habanero",
            "huntley",
            "landstalker",
            "landstalker2",
            "mesa",
            "novak",
            "patriot",
            "radi",
            "rebla",
            "rocoto",
            "seminole",
            "seminole2",
            "serrano",
            "squaddie",
            "toros",
            "xls",
            "xls2",
            //SUV

             //VAN CLASSIC
            "surfer",
            "surfer2",
            //VAN CLASSIC

            //VAN
            "bison",
            "bison2",
            "bison3",
            "bobcatxl",
            "boxville",
            "boxville2",
            "boxville3",
            "boxville4",
            "burrito",
            "burrito2",
            "burrito3",
            "burrito4",

            "gburrito",
            "gburrito2",

            "minivan",
            "minivan2",
            "paradise",
            "pony",
            "pony2",
            "rumpo",
            "rumpo2",
            "rumpo3",
            "speedo",
            "speedo2",
            "speedo4",

            "youga",
            "youga2",
            "youga3"
            //VAN
        };

        public static string[] BodyGuard_Bomber { get; } =
        {
            //--TRUCKS
            "benson",
            "biff",
            "cerberus",
            "cerberus2",
            "cerberus3",//s
            "hauler",
            "hauler2",
            "mule",
            "mule2",
            "mule3",
            "mule4",
            "packer",
            "phantom",
            "phantom2",
            "phantom3",
            "pounder",
            "stockade",
            //--TRUCKS


            "ambulance",
            "firetruk",
            "lguard",

            //INDUSTRIAL
            "bulldozer",
            "dump",
            "flatbed",
            "handler",
            "mixer",
            "mixer2",
            "rubble",
            "tiptruck",
            "tiptruck2",  
            //INDUSTRIAL

            //MOTORCYCLE (MOTOCROSS)
            "enduro",
            "esskey",
            "manchez",
            "manchez2",       
            //MOTORCYCLE (MOTOCROSS)

            //MOTORCYCLE (COOL)
            "akuma",
            "bati",
            "carbonrs",
            "hakuchou",      
            //MOTORCYCLE (COOL)

             //SEDAN (BOMBER)
            "romero",
            "stretch",
            //SEDAN (BOMBER)

             //BUS
            "airbus",
            "bus",
            "coach",
            //BUS

            //TRUCK
            "rallytruck",
            "rentalbus",
            "tourbus",
            "trash",
            "trash2",
            "wastelander",
            //TRUCK

            //VAN BOMBER
            "taco",
             "camper",
             "journey",
             //VAN Bomber

             //VAN
            "bison",
            "bison2",
            "bison3",
            "bobcatxl",
            "boxville",
            "boxville2",
            "boxville3",
            "boxville4",
            "burrito",
            "burrito2",
            "burrito3",
            "burrito4",

            "gburrito",
            "gburrito2",

            "minivan",
            "minivan2",
            "paradise",
            "pony",
            "pony2",
            "rumpo",
            "rumpo2",
            "rumpo3",
            "speedo",
            "speedo2",
            "speedo4",

            "youga",
            "youga2",
            "youga3"
            //VAN
     
        };









































        //public static string[] BodyGuardMobile { get; } =
        // {
        //    "AKUMA",
        //    "NEMESIS",
        //    "FUGITIVE",
        //    "PRIMO",
        //    "SOVEREIGN"

        //    //"INTRUDER",
        //    //"INGOT",
        //    //"MINIVAN",
        //    //"BOXVILLE",
        //    //"BURRITO",
        //    //"ORACLE",
        //    //"JACKAL",
        //    //"SURGE",
        //    //"FUGITIVE",
        //    //"DILETTANTE",
        //    //"TORNADO",
        //    //"MANANA",
        //    //"STANIER",
        //    //"TAILGATER",
        //    //"EMPEROR",
        //    //"WASHINGTON"
        //};

        public static string[] VehicleAllModels { get; } =
        {
            "ADDER",
            "AIRBUS",
            "AIRTUG",
            "AKUMA",
            "AMBULANCE",
            "ANNIHILATOR",
            "ARMYTANKER",
            "ARMYTRAILER",
            "ARMYTRAILER2",
            "ASEA",
            "ASEA2",
            "ASTEROPE",
            "BAGGER",
            "BALETRAILER",
            "BALLER",
            "BALLER2",
            "BANSHEE",
            "BARRACKS",
            "BARRACKS2",
            "BATI",
            "BATI2",
            "BENSON",
            "BFINJECTION",
            "BIFF",
            "BISON",
            "BISON2",
            "BISON3",
            "BJXL",
            "BLAZER",
            "BLAZER2",
            "BLAZER3",
            "BLIMP",
            "BLISTA",
            "BMX",
            "BOATTRAILER",
            "BOBCATXL",
            "BODHI2",
            "BOXVILLE",
            "BOXVILLE2",
            "BOXVILLE3",
            "BUCCANEER",
            "BUFFALO",
            "BUFFALO2",
            "BULLDOZER",
            "BULLET",
            "BURRITO",
            "BURRITO2",
            "BURRITO3",
            "BURRITO4",
            "BURRITO5",
            "BUS",
            "BUZZARD",
            "BUZZARD2",
            "CABLECAR",
            "CADDY",
            "CADDY2",
            "CAMPER",
            "CARBONIZZARE",
            "CARBONRS",
            "CARGOBOB",
            "CARGOBOB2",
            "CARGOBOB3",
            "CARGOPLANE",
            "CAVALCADE",
            "CAVALCADE2",
            "CHEETAH",
            "COACH",
            "COGCABRIO",
            "COMET2",
            "COQUETTE",
            "CRUISER",
            "CRUSADER",
            "CUBAN800",
            "CUTTER",
            "DAEMON",
            "DILETTANTE",
            "DILETTANTE2",
            "DINGHY",
            "DINGHY2",
            "DLOADER",
            "DOCKTRAILER",
            "DOCKTUG",
            "DOMINATOR",
            "DOUBLE",
            "DUBSTA",
            "DUBSTA2",
            "DUMP",
            "DUNE",
            "DUNE2",
            "DUSTER",
            "ELEGY2",
            "EMPEROR",
            "EMPEROR2",
            "EMPEROR3",
            "ENTITYXF",
            "EXEMPLAR",
            "F620",
            "FAGGIO2",
            "FBI",
            "FBI2",
            "FELON",
            "FELON2",
            "FELTZER2",
            "FIRETRUK",
            "FIXTER",
            "FLATBED",
            "FORKLIFT",
            "FQ2",
            "FREIGHT",
            "FREIGHTCAR",
            "FREIGHTCONT1",
            "FREIGHTCONT2",
            "FREIGHTGRAIN",
            "FREIGHTTRAILER",
            "FROGGER",
            "FROGGER2",
            "FUGITIVE",
            "FUSILADE",
            "FUTO",
            "GAUNTLET",
            "GBURRITO",
            "GRAINTRAILER",
            "GRANGER",
            "GRESLEY",
            "HABANERO",
            "HANDLER",
            "HAULER",
            "HEXER",
            "HOTKNIFE",
            "INFERNUS",
            "INGOT",
            "INTRUDER",
            "ISSI2",
            "JACKAL",
            "JB700",
            "JET",
            "JETMAX",
            "JOURNEY",
            "KHAMELION",
            "LANDSTALKER",
            "LAZER",
            "LGUARD",
            "LUXOR",
            "MAMMATUS",
            "MANANA",
            "MARQUIS",
            "MAVERICK",
            "MESA",
            "MESA2",
            "MESA3",
            "METROTRAIN",
            "MINIVAN",
            "MIXER",
            "MIXER2",
            "MONROE",
            "MOWER",
            "MULE",
            "MULE2",
            "NEMESIS",
            "NINEF",
            "NINEF2",
            "ORACLE",
            "ORACLE2",
            "PACKER",
            "PATRIOT",
            "PBUS",
            "PCJ",
            "PENUMBRA",
            "PEYOTE",
            "PHANTOM",
            "PHOENIX",
            "PICADOR",
            "POLICE",
            "POLICE2",
            "POLICE3",
            "POLICE4",
            "POLICEB",
            "POLICEOLD1",
            "POLICEOLD2",
            "POLICET",
            "POLMAV",
            "PONY",
            "PONY2",
            "POUNDER",
            "PRAIRIE",
            "PRANGER",
            "PREDATOR",
            "PREMIER",
            "PRIMO",
            "PROPTRAILER",
            "RADI",
            "RAKETRAILER",
            "RANCHERXL",
            "RANCHERXL2",
            "RAPIDGT",
            "RAPIDGT2",
            "RATLOADER",
            "REBEL",
            "REBEL2",
            "REGINA",
            "RENTALBUS",
            "RHINO",
            "RIOT",
            "RIPLEY",
            "ROCOTO",
            "ROMERO",
            "RUBBLE",
            "RUFFIAN",
            "RUINER",
            "RUMPO",
            "RUMPO2",
            "SABREGT",
            "SADLER",
            "SADLER2",
            "SANCHEZ",
            "SANCHEZ2",
            "SANDKING",
            "SANDKING2",
            "SCHAFTER2",
            "SCHWARZER",
            "SCORCHER",
            "SCRAP",
            "SEASHARK",
            "SEASHARK2",
            "SEMINOLE",
            "SENTINEL",
            "SENTINEL2",
            "SERRANO",
            "SHAMAL",
            "SHERIFF",
            "SHERIFF2",
            "SKYLIFT",
            "SPEEDO",
            "SPEEDO2",
            "SQUALO",
            "STANIER",
            "STINGER",
            "STINGERGT",
            "STOCKADE",
            "STOCKADE3",
            "STRATUM",
            "STRETCH",
            "STUNT",
            "SUBMERSIBLE",
            "SULTAN",
            "SUNTRAP",
            "SUPERD",
            "SURANO",
            "SURFER",
            "SURFER2",
            "SURGE",
            "TACO",
            "TAILGATER",
            "TANKER",
            "TANKERCAR",
            "TAXI",
            "TIPTRUCK",
            "TIPTRUCK2",
            "TITAN",
            "TORNADO",
            "TORNADO2",
            "TORNADO3",
            "TORNADO4",
            "TOURBUS",
            "TOWTRUCK",
            "TOWTRUCK2",
            "TR2",
            "TR3",
            "TR4",
            "TRACTOR",
            "TRACTOR2",
            "TRACTOR3",
            "TRAILERLOGS",
            "TRAILERS",
            "TRAILERS2",
            "TRAILERS3",
            "TRAILERSMALL",
            "TRASH",
            "TRFLAT",
            "TRIBIKE",
            "TRIBIKE2",
            "TRIBIKE3",
            "TROPIC",
            "TVTRAILER",
            "UTILLITRUCK",
            "UTILLITRUCK2",
            "UTILLITRUCK3",
            "VACCA",
            "VADER",
            "VELUM",
            "VIGERO",
            "VOLTIC",
            "VOODOO2",
            "WASHINGTON",
            "YOUGA",
            "ZION",
            "ZION2",
            "ZTYPE",
            "BIFTA",
            "KALAHARI",
            "PARADISE",
            "SPEEDER",
            "BTYPE",
            "JESTER",
            "TURISMOR",
            "ALPHA",
            "VESTRA",
            "MASSACRO",
            "ZENTORNO",
            "HUNTLEY",
            "THRUST",
            "RHAPSODY",
            "WARRENER",
            "BLADE",
            "GLENDALE",
            "PANTO",
            "DUBSTA3",
            "PIGALLE",
            "MONSTER",
            "SOVEREIGN",
            "BESRA",
            "MILJET",
            "COQUETTE2",
            "SWIFT",
            "INNOVATION",
            "HAKUCHOU",
            "FUROREGT",
            "JESTER2",
            "MASSACRO2",
            "RATLOADER2",
            "SLAMVAN",
            "MULE3",
            "VELUM2",
            "TANKER2",
            "CASCO",
            "BOXVILLE4",
            "HYDRA",
            "INSURGENT",
            "INSURGENT2",
            "GBURRITO2",
            "TECHNICAL",
            "DINGHY3",
            "SAVAGE",
            "ENDURO",
            "GUARDIAN",
            "LECTRO",
            "KURUMA",
            "KURUMA2",
            "TRASH2",
            "BARRACKS3",
            "VALKYRIE",
            "SLAMVAN2",
            "SWIFT2",
            "LUXOR2",
            "FELTZER3",
            "OSIRIS",
            "VIRGO",
            "WINDSOR",
            "COQUETTE3",
            "VINDICATOR",
            "T20",
            "BRAWLER",
            "TORO",
            "CHINO",
            "SUBMERSIBLE2",
            "DUKES",
            "DUKES2",
            "BUFFALO3",
            "DOMINATOR2",
            "DODO",
            "MARSHALL",
            "BLIMP2",
            "GAUNTLET2",
            "STALION",
            "STALION2",
            "BLISTA2",
            "BLISTA3"
        };
    }
}
