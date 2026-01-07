namespace MedievilArchipelago
{
    public static class Addresses
    {
        public const uint FakeAddress = 0x12345612; // This is a placeholder for an address that doesn't exist in the game. Used for testing.


        ////////////////////////// DAN ADDRESSES /////////////////////////////


        public const uint DansCurrentEnergy = 0x000F152C;
        public const uint DansCurrentStoredEnergy = 0x000F1530;
        public const uint DansCurrentGold = 0x000f15b4;
        public const uint DansCurrentLifeBottles = 0x000f1534; // uses full health bars to count (300 = 1)

        public const uint DansCurrentWeapon = 0x000F1538;
        public const uint DansCurrentEquipmentSlot = 0x000F153C;
        public const uint DansEquippedPrimaryWeapon = 0x000F1540;
        public const uint DansEquippedSecondaryWeapon = 0x000F1544;
        public const uint DansEquippedShield = 0x000F1548;

        // GPS Coords
        public const uint DanRespawnPositionX = 0x000f3b84;
        public const uint DanRespawnPositionY = 0x000f3b88;
        public const uint DanRespawnPositionZ = 0x000f3b8c;

        public const uint DanForwardSpeed = 0x000CBA5C;
        public const uint DanJumpHeight = 0x000CBA6E;
        public const uint DanClimbValue = 0x000cba72;
        public const uint DanPushValue = 0x000cba5e;
        public const uint DanPushRelatedValue = 0x000cba70;
        public const uint DanSidewaysValue = 0x000CBA60;


        // Skills
        public const uint DansHandSkill = 0x000F1694;
        public const uint DaringDashSkill = 0x000F1694;

        // Exit Level Menu
        public const uint ExitLevel = 0x000d359d;

        // Cheat menu
        public const uint CheatMenu = 0x000f0608;

        // Important check toggles
        public const uint WinstonTalkToggle = 0x000ef104;
        public const uint BookInteractToggle = 0x000ef268;
        public const uint CutscenePlayingValue = 0x000ebc5a;

        ////////////////////////// LEVEL ADDRESSES /////////////////////////////

        // Current Level
        public const uint CurrentLevel = 0x000EFF0C; 

        /*  Current Level Bytes: 

            02=Cathedral Spires
            03=The Demon
            04=Iron Slugger
            05=Dankenstein
            06=The Freakshow
            07=Greenwich Observatory
            08=Kew Gardens
            09=Whitechapel
            0a=The Museum(+ first loading screen when loading a save)
            0b=Tyrannosaurus Wrecks
            0d=Lobby
            0e=The Sewers
            0f=The Time Machine
            10=The Count
            11=Wulfrum Hall
            12=Kensington
            13=Main Menu
            1a=Kensington, The Tomb
            1b=Greenwich Naval Academy 
            1c=Time Machine, Sewers
            1d=Time Machine, The Ripper
            1e=Cathedral Spires, the Descent

         */

        // Completed levels
        public const uint TM_LevelStatus = 0x000E1734;
        public const uint TW_LevelStatus = 0x000E1744;
        public const uint KT_LevelStatus = 0x000E1754;
        public const uint TT_LevelStatus = 0x000E1764;
        public const uint TF_LevelStatus = 0x000E1774;
        public const uint GO_LevelStatus = 0x000E1784;
        public const uint GONA_LevelStatus = 0x000E1794;
        public const uint KG_LevelStatus = 0x000E17A4;
        public const uint DK_LevelStatus = 0x000E17B4;
        public const uint IS_LevelStatus = 0x000E17C4;
        public const uint WH_LevelStatus = 0x000E17D4;
        public const uint TC_LevelStatus = 0x000E17E4;
        public const uint WC_LevelStatus = 0x000E17F4;
        public const uint TS_LevelStatus = 0x000E1804;
        public const uint TTM_LevelStatus = 0x000E1814;
        public const uint TTMTS_LevelStatus = 0x000E1824;
        public const uint TR_LevelStatus = 0x000E1834;
        public const uint CS_LevelStatus = 0x000E1844;
        public const uint CSTD_LevelStatus = 0x000E1854;
        public const uint TD_LevelStatus = 0x000E1864; // maps to ending cutscene

        // Open World Setup
        public const uint LabState = 0x000eff84;

        ////////////////////////// LEVEL PICKUPS /////////////////////////////

        // Hub - Professors Lab
        public const uint PL_Book_LifestylesOfThePharaohs = 0x0012B419;
        public const uint PL_Book_ProfessorsDiary = 0x0012A495;
        public const uint PL_ChaliceReward_CaneStick = 0x001006E0;
        public const uint PL_ChaliceReward_Hammer = 0x001007F0;
        public const uint PL_ChaliceReward_Crossbow = 0x00100900;
        public const uint PL_ChaliceReward_Axe = 0x00100658;
        public const uint PL_ChaliceReward_Bombs = 0x00100ADC;
        public const uint PL_ChaliceReward_BroadSword = 0x00100614;
        public const uint PL_ChaliceReward_Lightning = 0x001007AC;
        public const uint PL_ChaliceReward_Blunderbuss = 0x0010069C;
        public const uint PL_ChaliceReward_MagicSword = 0x00100724;
        public const uint PL_ChaliceReward_GatlingGun = 0x00100768;

        // Hub - Professors Lab - After Ripper Has Been Defeated
        public const uint PL_Book_Shift_LifestylesOfThePharaohs = 0x0012B419;
        public const uint PL_Book_Shift_ProfessorsDiary = 0x0012A495;
        public const uint PL_ChaliceReward_Shift_CaneStick = 0x001006B8;
        public const uint PL_ChaliceReward_Shift_Hammer = 0x001007C8;
        public const uint PL_ChaliceReward_Shift_Crossbow = 0x001008D8;
        public const uint PL_ChaliceReward_Shift_Axe = 0x00100630;
        public const uint PL_ChaliceReward_Shift_Bombs = 0x00100AB4;
        public const uint PL_ChaliceReward_Shift_BroadSword = 0x001005EC;
        public const uint PL_ChaliceReward_Shift_Lightning = 0x00100784;
        public const uint PL_ChaliceReward_Shift_Blunderbuss = 0x00100674;
        public const uint PL_ChaliceReward_Shift_MagicSword = 0x001006FC;
        public const uint PL_ChaliceReward_Shift_GatlingGun = 0x00100740;


        // The Museum
        public const uint TM_Book_SirDan = 0x00128bed;
        public const uint TM_Winston_DansRoom = 0x000FFA70;
        public const uint TM_Pickup_ShortSword = 0x0014471c;
        public const uint TM_Pickup_MuseumKey = 0x00144a4c;
        public const uint TM_Pickup_EnergyVialPistolRoom = 0x001450f0;
        public const uint TM_Pickup_GoldCoinsBehindPurpleStructure = 0x001455b8;
        public const uint TM_Pickup_Pistol = 0x001446d8;
        public const uint TM_Winston_PistolRoom = 0x000FFA70;
        public const uint TM_Pickup_EnergyVialMausoleumRoom2F = 0x00145134;
        public const uint TM_Pickup_GoldCoinsMausoleumRoom2F1 = 0x00144ed0;
        public const uint TM_Pickup_GoldCoinsMausoleumRoom2F2 = 0x00144e48;
        public const uint TM_Pickup_GoldCoinsMausoleumRoom2F3 = 0x00144e8c;
        public const uint TM_Winston_ChestOnMausoleumRoom2F = 0x000FFA70;
        public const uint TM_Winston_GoldCoinsOnMausoleumRoom2F = 0x000FFA70;
        public const uint TM_Pickup_CopperShield2ndFloorChest = 0x00145288;
        public const uint TM_Pickup_GoldCoinsBuddahStatueStaircase = 0x001451bc;
        public const uint TM_Winston_ClimbingWall = 0x000FFA70;
        public const uint TM_Winston_StaircaseAfterBuddah = 0x000FFA70;
        public const uint TM_Pickup_GoldCoinsDisplayRoomBalconyRight = 0x0014592c;
        public const uint TM_Pickup_GoldCoinsDisplayRoomBalconyLeft = 0x00146388;
        public const uint TM_Winston_Chalice = 0x000FFA70;
        public const uint TM_Pickup_Cannonball = 0x001453dc;
        public const uint TM_Pickup_Torch = 0x00144650;
        public const uint TM_Pickup_GoldCoinsZarokRoomRaftersBack = 0x00145d6c;
        public const uint TM_Pickup_GoldCoinsZarokRoomRaftersLeft = 0x00145db0;
        public const uint TM_Pickup_GoldCoinsZarokRoomRaftersRight = 0x00145d28;
        public const uint TM_Pickup_DinosaurKey = 0x00145c18;
        public const uint TM_Book_TheKraken = 0x00128570;
        public const uint TM_Book_Zarok = 0x001281b4;
        public const uint TM_Pickup_CopperShieldZarokRoom = 0x00145ca0;
        public const uint TM_Pickup_GoldCoinsTombRoomLeft = 0x0014609c;
        public const uint TM_Pickup_GoldCoinsTombRoomRight = 0x001460e0;
        public const uint TM_Pickup_GoldCoinsFirstHandRoomChest1 = 0x00145970;
        public const uint TM_Pickup_GoldCoinsFirstHandRoomChest2 = 0x001459B4;
        public const uint TM_Pickup_GoldCoinsFirstHandRoomChest3 = 0x00144F14;
        public const uint TM_Pickup_EnergyVialSecondHandRoom = 0x00144FE0;
        public const uint TM_Pickup_GoldCoinsSecondHandRoomChestRightOfVial = 0x001459F8;
        public const uint TM_Pickup_GoldCoinsSecondHandRoomChestLeftOfVial = 0x00145A3C;
        public const uint TM_Pickup_GoldCoinsSecondHandRoomChestBetweenBoxes = 0x00144F58;
        public const uint TM_Pickup_GoldCoinsSecondHandRoomChestHiddenOnPipes = 0x00145AC4;
        public const uint TM_Pickup_GoldCoinsSecondHandRoomChestOnBoxes = 0x00145A80;
        public const uint TM_Pickup_Chalice = 0x0014460c;

        // The Museum - After Ripper Has Been Defeated
        public const uint TM_Winston_Shift_DansRoom = 0x001008F8;
        public const uint TM_Winston_Shift_PistolRoom = 0x001008F8;
        public const uint TM_Winston_Shift_ChestOnMausoleumRoom2F = 0x001008F8;
        public const uint TM_Winston_Shift_GoldCoinsOnMausoleumRoom2F = 0x001008F8;
        public const uint TM_Winston_Shift_ClimbingWall = 0x001008F8;
        public const uint TM_Winston_Shift_StaircaseAfterBuddah = 0x001008F8;
        public const uint TM_Winston_Shift_Chalice = 0x001008F8;

        public const uint TM_Book_Shift_TheKraken = 0x00128570;
        public const uint TM_Book_Shift_Zarok = 0x001281b4;

        // Tyrannosaurus Wrecks
        public const uint TW_Winston_Entrance = 0x000FF628;
        public const uint TW_Pickup_GoldCoinsBagBehindLionStatue = 0x001664A0;
        public const uint TW_Pickup_CopperChestInStairway = 0x0016656c;
        public const uint TW_Pickup_GoldCoinsNearSpivOnStairway = 0x001664e4;
        public const uint TW_Book_DinosaurDisplay = 0x00128414;
        public const uint TW_Pickup_LifeBottle = 0x00166748;
         
        // Tyrannosaurus Wrecks - After Ripper Has Been Defeated
        public const uint TW_Winston_Shift_Entrance = 0x000FF914;

        // Kensington
        public const uint KT_Pickup_CopperShieldOnRailway = 0x00166abc;
        public const uint KT_Winston_PushingAndPulling = 0x001006A0;
        public const uint KT_Pickup_GoldCoinsBagNearWater = 0x001658f0;
        public const uint KT_Pickup_TheDepotKey = 0x00164a10;
        public const uint KT_Pickup_TownHouseKey = 0x001649cc;
        public const uint KT_Winston_WhereTheSpellWasCast = 0x001006A0;
        public const uint KT_Pickup_Pocketwatch = 0x00163f70;
        public const uint KT_Winston_MuseumRoof = 0x001006A0;
        public const uint KT_Pickup_Chalice = 0x001639dc;

        // Kensington - After Ripper Has Been Defeated
        public const uint KT_Winston_Shift_PushingAndPulling = 0x000FF410;
        public const uint KT_Winston_Shift_WhereTheSpellWasCast = 0x000FF410;
        public const uint KT_Winston_Shift_MuseumRoof = 0x000FF410;

        // The Tomb
        public const uint TT_Pickup_GoldCoinsTombEntranceTopR = 0x0014f090;
        public const uint TT_Pickup_GoldCoinsTombEntranceTopL = 0x0014f04c;
        public const uint TT_Pickup_ScrollOfSekhmet = 0x0014c544;
        public const uint TT_Pickup_TabletOfHorus = 0x0014c4bc;
        public const uint TT_Pickup_StaffOfAnubis = 0x0014c500;
        public const uint TT_Pickup_GoldCoinsHandAreaChestGroundFloor = 0x0014f404;
        public const uint TT_Pickup_GoldCoinsHandAreaChestUpperFloor = 0x0014f448;
        public const uint TT_Winston_Entrance = 0x001005C8;

        // The Tomb - After Ripper Has Been Defeated
        public const uint TT_Winston_Shift_Entrance = 0x00100D40;

        // The Freakshow
        public const uint TF_Pickup_GoldCoinsBagAtStartLeft = 0x00153604;
        public const uint TF_Winston_Entrance = 0x00100928;
        public const uint TF_Pickup_GoldCoinsLadiesBagTrap1 = 0x00152218;
        public const uint TF_Pickup_GoldCoinsLadiesBagTrap2 = 0x00151E1C;
        public const uint TF_Pickup_GoldCoinsLadiesBagTrap3 = 0x00151DD8;
        public const uint TF_Pickup_GoldCoinsLadiesBagTrap4 = 0x00151EE8;
        public const uint TF_Pickup_GoldCoinsBagInTunnel = 0x00151F2C;
        public const uint TF_Pickup_GoldCoinsBagOutsideTunnel = 0x0015225C;
        public const uint TF_Pickup_GoldCoinsChestAtWaterJump = 0x001522A0;
        public const uint TF_Pickup_LifeBottle = 0x00151C40;
        public const uint TF_Pickup_HammerGame = FakeAddress;
        public const uint TF_Pickup_WhackAImpDrumsticks = FakeAddress;
        public const uint TF_Winston_Trampoline = 0x00100928;
        public const uint TF_Pickup_GoldCoinsChestBelowGiantClown = 0x00154280; 
        public const uint TF_Pickup_GoldCoinsChestHiddenAtTrampolines = 0x0015137C;
        public const uint TF_Winston_ElephantArmy = 0x00100928;
        public const uint TF_Pickup_CopperShieldInChaliceRoom = 0x001527AC;
        public const uint TF_Pickup_Chalice = 0x00152C74;
        public const uint TF_Pickup_CopperShieldInElephantBossArena = 0x00151AEC;
        public const uint TF_Pickup_ElephantKey1 = 0x00151558;
        public const uint TF_Pickup_ElephantKey2 = 0x0015159C;
        public const uint TF_Pickup_GoldCoinsHandAreaChest = 0x001539BC;
        public const uint TF_Pickup_GoldCoinsHandAreaHiddenChestL = 0x00153934;
        public const uint TF_Pickup_GoldCoinsHandAreaHiddenChestR = 0x00153978;

        // The Freakshow - After Ripper Has Been Defeated
        public const uint TF_Winston_Shift_Entrance = 0x001013E8;
        public const uint TF_Winston_Shift_Trampoline = 0x001013E8;
        public const uint TF_Winston_Shift_ElephantArmy = 0x001013E88;

        // Greenwich Observatory
        public const uint GO_Winston_LostYourHead = 0x000FF6B0;
        public const uint GO_Winston_CloseToLadder = 0x000FF6B0;
        public const uint GO_Pickup_GoldCoinsFountainBag1 = 0x00140DD0;
        public const uint GO_Pickup_GoldCoinsFountainBag2 = 0x001411CC;
        public const uint GO_Pickup_CopperShieldNearBombChest = 0x00141AD4;
        public const uint GO_Winston_LeverPuzzle = 0x000FF6B0;
        public const uint GO_Pickup_GoldCoinsBagBelowSpiv = 0x00140154;
        public const uint GO_Pickup_GoldCoinsBagBelowChalice1 = 0x001423DC;
        public const uint GO_Pickup_GoldCoinsBagBelowChalice2 = 0x00142398;
        public const uint GO_Pickup_LifeBottle = 0x00141298;
        public const uint GO_Pickup_Chalice = 0x00141254;
        public const uint GO_Winston_OnceThroughThisDoor = 0x000FF6B0;
        public const uint GO_Pickup_GoldCoinsChestNearExit = 0x001426C8;
        public const uint GO_Pickup_GoldCoinsHandAreaChest1 = 0x00142684;
        public const uint GO_Pickup_GoldCoinsHandAreaChest2 = 0x00142640;
        public const uint GO_Pickup_GoldCoinsHandAreaChest3 = 0x001425FC;

        // Greenwich Observatory - After Ripper Has Been Defeated
        public const uint GO_Winston_Shift_LostYourHead = 0x00100560;
        public const uint GO_Winston_Shift_CloseToLadder = 0x00100560;
        public const uint GO_Winston_Shift_LeverPuzzle = 0x00100560;
        public const uint GO_Winston_Shift_OnceThroughThisDoor = 0x00100560;

        // Greenwhich Observatory, Naval Academy
        public const uint GONA_Pickup_GoldCoinsBagNearTrees = 0x0014D794;
        public const uint GONA_Pickup_EnergyVialNearTrees = 0x0014D750;
        public const uint GONA_Pickup_Bellows = 0x0014CC6C;
        public const uint GONA_Winston_Balloon = 0x000ff728;

        // Greenwhich Observatory, Naval Academy - After Ripper Has Been Defeated
        public const uint GONA_Winston_Shift_Balloon = 0x001005C0;

        // Kew Gardens
        public const uint KG_Winston_LevelStart = 0x000FFA14;
        public const uint KG_Pickup_PottingShedKey = 0x0014145c;
        public const uint KG_Pickup_EnergyVialBehindFence1 = 0x00144a8c;
        public const uint KG_Pickup_EnergyVialBehindFence2 = 0x00144a48;
        public const uint KG_Pickup_GoldCoinsBehindFenceChest1 = 0x00144ad0;
        public const uint KG_Pickup_GoldCoinsBehindFenceChest2 = 0x00144d78;
        public const uint KG_Pickup_LifeBottle = 0x00140f50;
        public const uint KG_Pickup_LifeBottle_2 = 0x00140cec;
        public const uint KG_Pickup_GoldCoinsBagNearShed = 0x00144030;
        public const uint KG_Pickup_WaterTankValve = 0x001410e8;
        public const uint KG_Winston_Infection = 0x000FFA14;
        public const uint KG_Pickup_GoldCoinsChestAtTopOfTree = 0x00143e98;
        public const uint KG_Pickup_GoldCoinsBridgeRoomVineChest1 = 0x00143f20;
        public const uint KG_Pickup_GoldCoinsBridgeRoomVineChest2 = 0x00143edc;
        public const uint KG_Pickup_GoldCoinsBagUpTree = 0x00143f64;
        public const uint KG_Pickup_GoldCoinsBagOnRoof1 = 0x001440b8;
        public const uint KG_Pickup_GoldCoinsBagOnRoof2 = 0x00144074;
        public const uint KG_Pickup_PondRoomValve = 0x001402d4;
        public const uint KG_Pickup_HotHouseValve = 0x00140318;
        public const uint KG_Pickup_GoldCoinsBagInThirdHumanRoom = 0x00143fa8;
        public const uint KG_Pickup_GoldCoinsHandMazeChest = 0x00143fec;
        public const uint KG_Pickup_GoldCoinsHandMazeChestReward1 = 0x00144d34;
        public const uint KG_Pickup_GoldCoinsHandMazeChestReward2 = 0x00144cf0;
        public const uint KG_Pickup_GoldCoinsHandMazeChestReward3 = 0x00144cac;
        public const uint KG_Pickup_SilverShieldInGauntletRoom = 0x00144184;
        public const uint KG_Pickup_Chalice = 0x0013fe50;

        // Kew Gardens - After Ripper Has Been Defeated
        public const uint KG_Winston_Shift_LevelStart = 0x001005F8;
        public const uint KG_Winston_Shift_Infection = 0x001005F8;

        // Dankenstein
        // 0x10183c new value for number of corpse items
        public const uint DK_Winston_SavePoint = 0x000FECD0;
        public const uint DK_Pickup_Bum = 0x00101870;
        public const uint DK_Pickup_LeftArm = 0x00101870;
        public const uint DK_Pickup_LeftLeg = 0x00101870;
        public const uint DK_Pickup_RightArm = 0x00101870;
        public const uint DK_Pickup_RightLeg = 0x00101870;
        public const uint DK_Pickup_Torso = 0x00101870;
        public const uint DK_Book_Reanimation = 0x00128732;
        public const uint DK_Pickup_SilverShield = 0x0016AFE4;
        public const uint DK_Pickup_LifeBottle = 0x0016AC70;
        public const uint DK_Pickup_Chalice = 0x0016A984;
        public const uint DK_Pickup_GoldCoinsChestAboveProjector = 0x0016A434;
        public const uint DK_Pickup_GoldCoinsHandAreaChest1 = 0x0016AF18;
        public const uint DK_Pickup_GoldCoinsHandAreaChest2 = 0x0016AED4;

        // Dankenstein - After Ripper Has Been Defeated
        public const uint DK_Winston_Shift_SavePoint = 0x000FF4D0;

        // Iron Slugger
        public const uint IS_Book_DankensteinManual = 0x001286a0;

        // Iron Slugger - After Ripper Has Been Defeated - NOTHING AVAILABLE
        //public const uint IS_Book_Shift_DankensteinManual = 0x001286a0;

        // Wulfrum Hall
        public const uint WH_Winston_LevelStart = 0x000FFA84;
        public const uint WH_Pickup_GoldCoinsBagLeftOfEntrance = 0x001534b4;
        public const uint WH_Pickup_FrontDoorKey = 0x001532d8;
        public const uint WH_Pickup_EnergyVialNearKitchenStairs = 0x001544a4;
        public const uint WH_Pickup_EnergyVialLeftRoomOfEntrance = 0x00154570;
        public const uint WH_Pickup_EnergyVialInFrontOfHallsStaircase = 0x00155230;
        public const uint WH_Winston_Vampires = 0x000FFA84;
        public const uint WH_Pickup_GoldCoinsChestCloseToVampireRoom1 = 0x001542c8;
        public const uint WH_Pickup_SilverShieldCloseToVampireRoom2 = 0x00154D68;
        public const uint WH_Pickup_GoldCoinsChestInVampireRoom3 = 0x00153b9c;
        public const uint WH_Pickup_LifeBottle = 0x00153be0;
        public const uint WH_Pickup_GoldCoinsBagInVampireRoom5 = 0x00153fdc;
        public const uint WH_Pickup_Chalice = 0x0015452c;

        // Wulfrum Hall - After Ripper Has Been Defeated
        public const uint WH_Winston_Shift_LevelStart = 0x001008D8;
        public const uint WH_Winston_Shift_Vampires = 0x001008D8;

        // The Count
        public const uint TC_Winston_LevelStart = 0x000FF958;
        public const uint TC_Pickup_GoldChest = 0x00160758;


        // The Count - After Ripper Has Been Defeated
        public const uint TC_Winston_Shift_LevelStart = 0x001004C4;

        // Whitechapel
        public const uint WC_Winston_KiyaLastSeen = 0x000FF640;
        public const uint WC_Pickup_LibraryKeyInHouseBasement = 0x001469E8;
        public const uint WC_Pickup_GoldCoinsBag1InHouseBasement = 0x00148CB4;
        public const uint WC_Pickup_GoldCoinsBag2InHouseBasement = 0x00148C70;
        public const uint WC_Pickup_GriffinShield = 0x001461AC;
        public const uint WC_Book_IsibodBrunel = 0x0012872D;
        public const uint WC_Pickup_SilverShieldInLibrary = 0x00148DC4;
        public const uint WC_Pickup_MembershipCard = 0x0014609C;
        public const uint WC_Pickup_GoldCoinsBag1TailorShopBasement = 0x00148D80;
        public const uint WC_Pickup_GoldCoinsBag2TailorShopBasement = 0x00148D3C;
        public const uint WC_Pickup_GoldCoinsBag3TailorShopBasement = 0x00148CF8;
        public const uint WC_Pickup_ElegantSuite = 0x000F155A;
        public const uint WC_Pickup_Chalice = 0x001469A4;
        public const uint WC_Pickup_FlamingCrossbow = 0x00147598;
        public const uint WC_Pickup_UnicornShield = 0x00146344;
        public const uint WC_Pickup_Beard = 0x00147DD4;
        public const uint WC_Pickup_GoldCoinsChestInsideClub = 0x00148ED4;
        public const uint WC_Pickup_LifeBottle = 0x000FF640;

        // Whitechapel - After Ripper Has Been Defeated
        public const uint WC_Winston_Shift_KiyaLastSeen = 0x0010093C;

        // The Sewers
        public const uint TS_Winston_SavePoint = 0x000FF758;
        public const uint TS_GirlsFreed_1 = 0x000ff904;
        public const uint TS_GirlsFreed_2 = 0x000ff904;
        public const uint TS_GirlsFreed_3 = 0x000ff904;
        public const uint TS_GirlsFreed_4 = 0x000ff904;
        public const uint TS_GirlsFreed_5 = 0x000ff904;
        public const uint TS_Pickup_GoldShieldInHubArea = 0x001717F4;
        public const uint TS_Pickup_GoldCoinsBagAtPipesAreaStart = 0x0016fc98;
        public const uint TS_Pickup_GoldCoinsBag1InPipesPuzzleRoom = 0x0017110c;
        public const uint TS_Pickup_GoldCoinsBag2InPipesPuzzleRoom = 0x001710c8;
        public const uint TS_Winston_SomethingReallyInteresting = 0x000FF758;
        public const uint TS_Pickup_Poster = 0x00170b34;
        public const uint TS_Pickup_EnergyVial1EndPipesArea = 0x00171a14;
        public const uint TS_Pickup_EnergyVial2EndPipesArea = 0x00171a58;
        public const uint TS_Pickup_EnergyVial3EndPipesArea = 0x00171a9c;
        public const uint TS_Pickup_LifeBottle = 0x00170b78;
        public const uint TS_Pickup_Chalice = 0x00170270;
        public const uint TS_Pickup_GoldCoinsRewardChest1 = 0x00171728;
        public const uint TS_Pickup_GoldCoinsRewardChest2 = 0x001716e4;

        // The Sewers - After Ripper Has Been Defeated
        public const uint TS_Winston_Shift_SavePoint = 0x000FFAC8;
        // needs checked
        //public const uint TS_GirlsFreed_Shift_1 = 0x000ff904;
        //public const uint TS_GirlsFreed_Shift_2 = 0x000ff904;
        //public const uint TS_GirlsFreed_Shift_3 = 0x000ff904;
        //public const uint TS_GirlsFreed_Shift_4 = 0x000ff904;
        //public const uint TS_GirlsFreed_Shift_5 = 0x000ff904;
        public const uint TS_Winston_Shift_SomethingReallyInteresting = 0x000FFAC8;

        // The Time Machine
        public const uint TTM_Book_SpaceBeacon = 0x001285da;
        public const uint TTM_Pickup_TimeMachinePiecePlanetarium = 0x00149ac8;
        public const uint TTM_Book_GrammarHorn = 0x001286fc;
        public const uint TTM_Pickup_TimeMachinePieceGrammarHorn = 0x00149b0c;
        public const uint TTM_Book_MoonExhibit = 0x00128218;
        public const uint TTM_Pickup_TimeMachinePieceMoonExhibit = 0x00149a84;
        public const uint TTM_Book_TheTimeMachine = 0x001283e4;
        public const uint TTM_Winston_Entrance = 0x000FF6EC;
        public const uint TTM_Pickup_GoldChestBehindRightLionStatue = 0x0014afc4;

        // The Time Machine - After Ripper Has Been Defeated
        public const uint TTM_Winston_Shift_Entrance = 0x001002C0;

        // The Time Machine, The Sewers
        public const uint TTMTS_Winston_Entrance = 0x000FF734;
        public const uint TTMTS_Winston_KingsHat = 0x000FF734;
        public const uint TTMTS_Pickup_TimeStoneHutTrap = 0x00148ad4;
        public const uint TTMTS_Winston_StealingTimeStone = 0x000FF734;
        public const uint TTMTS_Pickup_KingMullocksKey = 0x00148c28;
        public const uint TTMTS_Pickup_GoodLightning = 0x000efe56;

        // The Time Machine, The Sewers - After Ripper Has Been Defeated

        public const uint TTMTS_Winston_Shift_Entrance = 0x00100300;
        public const uint TTMTS_Winston_Shift_KingsHat = 0x00100300;
        public const uint TTMTS_Winston_Shift_StealingTimeStone = 0x00100300;

        // The Ripper
        public const uint TR_Winston_Entrance = 0x000FF6EC;

        // The Ripper - After Ripper Has Been Defeated
        public const uint TR_Winston_Shift_Entrance = 0x000FF6EC;

        // Cathedral Spires
        // REGULAR NEEDS MAPPED
        public const uint CS_Pickup_LostSoul = 0x00137a50;
        public const uint CS_Winston_Entrance = 0x000eec60;
        public const uint CS_Pickup_GoldShieldInChestAtStart = 0x0013dd1c;
        public const uint CS_Pickup_GoldCoinsBottomRightNearSpiv = 0x0013ecc8;
        public const uint CS_Pickup_EnergyVialNextToSpiv = 0x0013afb0;
        public const uint CS_Pickup_GoldCoinsChestAfterFirstLostSoul = 0x0013f080;
        public const uint CS_Pickup_GoldCoinsChestAfterFirstFlameGargoyles = 0x0013ed50;
        public const uint CS_Pickup_GoldCoinsChestNearSilverShield = 0x0013ed0c;
        public const uint CS_Pickup_SilverShieldChest = 0x0013f0c4;
        public const uint CS_Pickup_LifeBottle = 0x0013c754;
        public const uint CS_Pickup_GoldCoinsBagAtTopOfSpire = 0x0013ed94;

        // Cathedral Spires - After Ripper Has Been Defeated
        public const uint CS_Pickup_Shift_LostSoul = 0x00137a50;
        public const uint CS_Winston_Shift_Entrance = 0x00100994;
        public const uint CS_Pickup_Shift_GoldShieldInChestAtStart = 0x0013dcd4;
        public const uint CS_Pickup_Shift_GoldCoinsBottomRightNearSpiv = 0x0013ecc8;
        public const uint CS_Pickup_Shift_EnergyVialNextToSpiv = 0x0013afb0;
        public const uint CS_Pickup_Shift_GoldCoinsChestAfterFirstLostSoul = 0x0013f080;
        public const uint CS_Pickup_Shift_GoldCoinsChestAfterFirstFlameGargoyles = 0x0013ed50;
        public const uint CS_Pickup_Shift_GoldCoinsChestNearSilverShield = 0x0013ed0c;
        public const uint CS_Pickup_Shift_SilverShieldChest = 0x0013f104;
        public const uint CS_Pickup_Shift_LifeBottle = 0x0013c754;
        public const uint CS_Pickup_Shift_GoldCoinsBagAtTopOfSpire = 0x0013ed94;

        // Cathedral Spires, The Descent
        // REGULAR NEEDS MAPPED
        public const uint CSTD_Pickup_LostSoulLeftChandelier = 0x0013f4b4;
        public const uint CSTD_Pickup_LostSoulRightChandelier = 0x0013f4b4;
        public const uint CSTD_Pickup_LostSoulDemonStatueMausoleum = 0x0013f4b4;
        public const uint CSTD_Pickup_LostSoulEntranceRoom = 0x0013f4b4;
        public const uint CSTD_Pickup_LostSoulTopOfPulleyFront = 0x0013f4b4;
        public const uint CSTD_Pickup_LostSoulTopOfPulleyBack = 0x0013f4b4;
        public const uint CSTD_Pickup_LostSoulPulleyRoomRight = 0x0013f4b4;
        public const uint CSTD_Winston_Entrance = 0x00100328;
        public const uint CSTD_Pickup_LifeBottle = 0x00147a3c;
        public const uint CSTD_Pickup_GoldCoinsGoldenCogRoomEntrance = 0x0014919c;
        public const uint CSTD_Pickup_GoldCoinsGoldenCogRoomBottom = 0x001491e0;
        public const uint CSTD_Pickup_GoldenCogInRoom = 0x00146474;
        public const uint CSTD_Pickup_GoldenCogInHandArea = 0x001458c4;
        public const uint CSTD_Pickup_SpellPageDemonDeath = 0x00146188;

        // Cathedral Spires, The Descent - After Ripper Has Been Defeated
        public const uint CSTD_Pickup_Shift_LostSoulLeftChandelier = 0x0013f4b4;
        public const uint CSTD_Pickup_Shift_LostSoulRightChandelier = 0x0013f4b4;
        public const uint CSTD_Pickup_Shift_LostSoulDemonStatueMausoleum = 0x0013f4b4;
        public const uint CSTD_Pickup_Shift_LostSoulEntranceRoom = 0x0013f4b4;
        public const uint CSTD_Pickup_Shift_LostSoulTopOfPulleyFront = 0x0013f4b4;
        public const uint CSTD_Pickup_Shift_LostSoulTopOfPulleyBack = 0x0013f4b4;
        public const uint CSTD_Pickup_Shift_LostSoulPulleyRoomRight = 0x0013f4b4;
        public const uint CSTD_Winston_Shift_Entrance = 0x00100B14;
        public const uint CSTD_Pickup_Shift_LifeBottle = 0x00147a3c;
        public const uint CSTD_Pickup_Shift_GoldCoinsGoldenCogRoomEntrance = 0x0014919c;
        public const uint CSTD_Pickup_Shift_GoldCoinsGoldenCogRoomBottom = 0x001491e0;
        public const uint CSTD_Pickup_Shift_GoldenCogInRoom = 0x00146474;
        public const uint CSTD_Pickup_Shift_GoldenCogInHandArea = 0x001458c4;
        public const uint CSTD_Pickup_Shift_SpellPageDemonDeath = 0x00146188;

        // The Demon
        // REGULAR NEEDS MAPPED
        public const uint TD_Winston_Entrance = 0x000ff7bc;

        // The Demon - After Ripper Has Been Defeated
        public const uint TD_Winston_Shift_Entrance = 0x000FFAD4;

        ////////////////////////// INVENTORY /////////////////////////////

        // Key Items

        public const uint ChickenDrumsticks = 0x000F1598;
        public const uint Torch = 0x000F159C;
        public const uint Poster = 0x000F15B0;
        public const uint DansHead = 0x000F15BC;
        public const uint ChaliceOfSouls = 0x000F15D0;
        public const uint LeftLeg = 0x000F15D8;
        public const uint RightLeg = 0x000F15DC;
        public const uint LeftArm = 0x000F15E0;
        public const uint RightArm = 0x000F15E4;
        public const uint Bum = 0x000F15E8;
        public const uint Torso = 0x000F15EC;
        public const uint Bellows = 0x000F15F0;
        public const uint LostSoul = 0x000F15F4;
        public const uint GoldenCog = 0x000F15F8;
        public const uint SpellPage = 0x000F15FC;
        public const uint GriffinShield = 0x000F1600;
        public const uint UnicornShield = 0x000F1604;
        public const uint Beard = 0x000F160C;
        public const uint LibraryKey = 0x000F1610;
        public const uint ClubMembershipCard = 0x000F1614;
        public const uint ElephantKey1 = 0x000F1618;
        public const uint ElephantKey2 = 0x000F161C;
        public const uint TimeMachinePieceContactRoom = 0x000F1620;
        public const uint TimeMachinePieceEarthRoom = 0x000F1624;
        public const uint TimeMachinePieceSpaceRoom = 0x000F1628;
        public const uint KingMullocksKey = 0x000F162C;
        public const uint StaffOfAnubis = 0x000F1630;
        public const uint ScrollOfSekhmet = 0x000F1634;
        public const uint TabletOfHorus = 0x000F1638;
        public const uint PocketWatch = 0x000F163C;
        public const uint TownHouseKey = 0x000F1640;
        public const uint TimeStone = 0x000F1644;
        public const uint Antidote = 0x000F1648;
        public const uint PondRoomValve = 0x000F164C;
        public const uint HothouseValve = 0x000F1650;
        public const uint WaterTankValve = 0x000F1658;
        public const uint CannonBall = 0x000F165C;
        public const uint FrontDoorKey = 0x000F1660;
        public const uint PottingShedKey = 0x000F1664;
        public const uint TheDepotKey = 0x000F1668;
        public const uint MuseumKey = 0x000F166C;
        public const uint DinosaurKey = 0x000F1670;

        // Melee weapons
        public const uint SmallSword = 0x000F155C;
        public const uint BroadSword = 0x000F1560;
        public const uint MagicSword = 0x000F1564;
        public const uint CaneStick = 0x000F1568;
        public const uint Hammer = 0x000F1570;
        public const uint Axe = 0x000F157C;


        // Ranged Weapons
        public const uint Pistol = 0x000F156C;
        public const uint Crossbow = 0x000F1574;
        public const uint FlamingCrossbow = 0x000F1578;
        public const uint GatlingGun = 0x000F1580;
        public const uint GoodLightning = 0x000F1584;
        public const uint Lightning = 0x000F1588;
        public const uint Blunderbuss = 0x000F1590;
        public const uint Bombs = 0x000F1594;

        // Shields
        public const uint CopperShield = 0x000F15A0;
        public const uint SilverShield = 0x000F15A4;
        public const uint GoldShield = 0x000F15A8;
        public const uint GoldenArmour = 0x000F15AC;

        // Other
        public const uint DansArm = 0x000F158C;



        //        // checks if you can control Dan
        //        public const uint InGameCheck = 0x000f88a0;

        //        public const uint IsLoaded = 0x001fff5c;


        //        // Game Complete
        //        public const uint WinConditionCredits = 0x00010038;
    }
}
