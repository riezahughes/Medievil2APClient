using Archipelago.Core;
using Archipelago.Core.Models;
using Archipelago.Core.Util;
using Archipelago.Core.Util.GPS;
using Archipelago.MultiClient.Net.Models;
using MedievilArchipelago.Models;
using Newtonsoft.Json;
using Serilog;
using SharpDX.DXGI;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Location = Archipelago.Core.Models.Location;

namespace MedievilArchipelago.Helpers
{
    public class LocationHandlers
    {

        public static List<ILocation> BuildLocationList(Dictionary<string, object> options)
        {
            int base_id = 99250000;
            int region_offset = 1000;

            //int gargoyleSanity = int.Parse(options?.GetValueOrDefault("gargoylesanity", "0").ToString());
            //int bookSanity = int.Parse(options?.GetValueOrDefault("booksanity", "0").ToString());

            List<string> table_order = [
                "Hub",
                "The Museum",
                "Tyrannosaurus Wrecks",
                "Kensington",
                "The Tomb",
                "The Freakshow",
                "Greenwich Observatory",
                "Greenwich, Naval Academy",
                "Kew Gardens",
                "Dankenstein",
                "Iron Slugger",
                "Wulfrum Hall",
                "The Count",
                "Whitechapel",
                "The Sewers",
                "The Time Machine",
                "The Time Machine, The Sewers",
                "The Ripper",
                "Cathedral Spires",
                "Cathedral Spires, The Descent",
                "The Demon",
            ];

            List<ILocation> locations = new List<ILocation>();

            Dictionary<string, List<GenericItemsData>> allLevelLocations = new Dictionary<string, List<GenericItemsData>>();

            // Level Locations
            allLevelLocations.Add("Hub", GetHubData());
            allLevelLocations.Add("The Museum", GetTheMuseumData());
            allLevelLocations.Add("Tyrannosaurus Wrecks", GetTyrannosaurusWrexData());
            allLevelLocations.Add("Kensington", GetKensingtonData());
            allLevelLocations.Add("The Tomb", GetTheTombData());
            allLevelLocations.Add("The Freakshow", GetTheFreakshowData());
            allLevelLocations.Add("Greenwich Observatory", GetGreenwichObservatoryData());
            allLevelLocations.Add("Greenwich, Naval Academy", GetNavalAcademyData());
            allLevelLocations.Add("Kew Gardens", GetKewGardensData());
            allLevelLocations.Add("Dankenstein", GetDankensteinData());
            allLevelLocations.Add("Iron Slugger", GetIronSluggerData());
            allLevelLocations.Add("Wulfrum Hall", GetWulfrumHallData());
            allLevelLocations.Add("The Count", GetTheCountData());
            allLevelLocations.Add("Whitechapel", GetWhitechapelData());
            allLevelLocations.Add("The Sewers", GetTheSewersData());
            allLevelLocations.Add("The Time Machine", GetTheTimeMachineData());
            allLevelLocations.Add("The Time Machine, The Sewers", GetTheTimeMachineSewersData());
            allLevelLocations.Add("The Ripper", GetTheRipperData());
            allLevelLocations.Add("Cathedral Spires", GetCathedralSpiresData());
            allLevelLocations.Add("Cathedral Spires, The Descent", GetCathedralSpiresTheDescentData());
            allLevelLocations.Add("The Demon", GetTheDemonData());

            var regional_index = 0;

            var debug_levelCount = 0;
            foreach (var region_name in table_order.ToList())
            {

                long currentRegionBaseId = base_id + regional_index * region_offset;

                if (allLevelLocations.ContainsKey(region_name))
                {
                    // Retrieve the list of locations for the current region
                    List<GenericItemsData> regionLocations = allLevelLocations[region_name];

                    var location_index = 0;

                    foreach (var loc in regionLocations)

                    {

                        int locationId = (int)currentRegionBaseId + location_index;

                        if (loc.Name.Contains("Winston:"))
                        {
                            {
                                List<ILocation> conditionalChoice = new List<ILocation>();

                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Level Check",
                                    Address = Addresses.CurrentLevel,
                                    CheckType = LocationCheckType.Byte,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = loc.LevelId
                                });


                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Winston Check",
                                    Address = Addresses.WinstonTalkToggle,
                                    CheckType = LocationCheckType.UShort,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = "1"
                                });

                                conditionalChoice.Add(new Location()
                                {
                                    Id = -1,
                                    Name = "Talk Check",
                                    Address = loc.Address,
                                    CheckType = loc.CheckType,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = loc.Check
                                });

                                CompositeLocation location = new CompositeLocation()
                                {
                                    Name = loc.Name,
                                    Id = locationId,
                                    CheckType = LocationCheckType.AND,
                                    Conditions = conditionalChoice
                                };

                                locations.Add(location);
                                location_index++;
                                continue;
                            };
                        }

                        if (loc.Name.Contains("Book:"))
                        {
                            {
                                List<ILocation> conditionalChoice = new List<ILocation>();

                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Level Check",
                                    Address = Addresses.CurrentLevel,
                                    CheckType = LocationCheckType.Byte,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = loc.LevelId
                                });


                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Winston Check",
                                    Address = Addresses.WinstonTalkToggle,
                                    CheckType = LocationCheckType.UShort,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = "0"
                                });

                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Book Check",
                                    Address = Addresses.BookInteractToggle,
                                    CheckType = LocationCheckType.UShort,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = "1"
                                });

                                conditionalChoice.Add(new Location()
                                {
                                    Id = -1,
                                    Name = "Read Check",
                                    Address = loc.Address,
                                    CheckType = loc.CheckType,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = loc.Check
                                });

                                CompositeLocation location = new CompositeLocation()
                                {
                                    Name = loc.Name,
                                    Id = locationId,
                                    CheckType = LocationCheckType.AND,
                                    Conditions = conditionalChoice
                                };

                                locations.Add(location);
                                location_index++;
                                continue;
                            };
                        }

                        if (loc.Name.Contains("Cleared: The Demon"))
                        {
                            {
                                List<ILocation> conditionalChoice = new List<ILocation>();

                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Level Check",
                                    Address = Addresses.CurrentLevel,
                                    CheckType = LocationCheckType.Byte,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = "3"
                                });

                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Cinematic Check",
                                    Address = Addresses.CutscenePlayingValue,
                                    CheckType = LocationCheckType.Byte,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = "22"
                                });

                                conditionalChoice.Add(new Location()
                                {
                                    Id = -1,
                                    Name = "Read Check",
                                    Address = loc.Address,
                                    CheckType = loc.CheckType,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = loc.Check
                                });

                                CompositeLocation location = new CompositeLocation()
                                {
                                    Name = loc.Name,
                                    Id = locationId,
                                    CheckType = LocationCheckType.AND,
                                    Conditions = conditionalChoice
                                };

                                locations.Add(location);
                                location_index++;
                                continue;
                            };
                        }


                        if (loc.Name.Contains("Cleared:"))
                        {
                            {
                                List<ILocation> conditionalChoice = new List<ILocation>();

                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Hub Check",
                                    Address = Addresses.CurrentLevel,
                                    CheckType = LocationCheckType.Byte,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = "13"
                                });


                                conditionalChoice.Add(new Location()
                                {
                                    Id = -1,
                                    Name = "Pickup Check",
                                    Address = loc.Address,
                                    CheckType = loc.CheckType,
                                    CompareType = LocationCheckCompareType.Range,
                                    RangeStartValue = "9",
                                    RangeEndValue = "45"
                                });

                                CompositeLocation location = new CompositeLocation()
                                {
                                    Name = loc.Name,
                                    Id = locationId,
                                    CheckType = LocationCheckType.AND,
                                    Conditions = conditionalChoice
                                };

                                locations.Add(location);
                                location_index++;
                                continue;
                            };
                        }

                        if (loc.Name.Contains("Key Item:") || loc.Name.Contains("Event:") || loc.Name.Contains("Chalice Reward") || loc.Name.Contains("Chalice:") || loc.Name.Contains("Equipment:") || loc.Name.Contains("Gold Coins:") || loc.Name.Contains("Skill:") || loc.Name.Contains("Life Bottle:") || loc.Name.Contains("Energy Vial:"))
                        {
                            {
                                List<ILocation> conditionalChoice = new List<ILocation>();

                                conditionalChoice.Add(new Location()
                                {

                                    Id = -1,
                                    Name = "Level Check",
                                    Address = Addresses.CurrentLevel,
                                    CheckType = LocationCheckType.Byte,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = loc.LevelId
                                });

                                conditionalChoice.Add(new Location()
                                {
                                    Id = -1,
                                    Name = "Pickup Check",
                                    Address = loc.Address,
                                    CheckType = loc.CheckType,
                                    CompareType = LocationCheckCompareType.Match,
                                    CheckValue = loc.Check
                                });

                                CompositeLocation location = new CompositeLocation()
                                {
                                    Name = loc.Name,
                                    Id = locationId,
                                    CheckType = LocationCheckType.AND,
                                    Conditions = conditionalChoice
                                };

                                locations.Add(location);
                                location_index++;
                                continue;
                            };
                        }
                        else
                        {
#if DEBUG
                            Console.WriteLine($"Could not add {loc.Name}, id: {loc.Id}");
#endif
                            location_index++;
                        }
                    }
                }
                regional_index++;
            }

            return locations;
        }

        private static List<GenericItemsData> GetHubData()
        {
            List<GenericItemsData> hubLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Book: Lifestyles of the Pharaohs", Addresses.PL_Book_LifestylesOfThePharaohs, "13", "0", LocationCheckType.Byte),
                new GenericItemsData("Book: Professor's Diary", Addresses.PL_Book_ProfessorsDiary, "13", "0", LocationCheckType.Byte),
                new GenericItemsData("Chalice Reward: Cane Stick", Addresses.PL_ChaliceReward_CaneStick, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Hammer", Addresses.PL_ChaliceReward_Hammer, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Crossbow", Addresses.PL_ChaliceReward_Crossbow, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Axe", Addresses.PL_ChaliceReward_Axe, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Bombs", Addresses.PL_ChaliceReward_Bombs, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Broad Sword", Addresses.PL_ChaliceReward_BroadSword, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Lightning", Addresses.PL_ChaliceReward_Lightning, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Blunderbuss", Addresses.PL_ChaliceReward_Blunderbuss, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Magic Sword", Addresses.PL_ChaliceReward_MagicSword, "13", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice Reward: Gatling Gun", Addresses.PL_ChaliceReward_GatlingGun, "13", "704", LocationCheckType.UShort),
            };

            return hubLocations;
        }

        private static List<GenericItemsData> GetTheMuseumData()
        {
            List<GenericItemsData> museumLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Museum Key - TM", Addresses.TM_Pickup_MuseumKey, "10", "704", LocationCheckType.UInt),
                new GenericItemsData("Key Item: Cannonball - TM", Addresses.TM_Pickup_Cannonball, "10", "33049", LocationCheckType.UInt),
                new GenericItemsData("Key Item: Torch - TM", Addresses.TM_Pickup_Torch, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Dinosaur Key - TM", Addresses.TM_Pickup_DinosaurKey, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Short Sword - TM", Addresses.TM_Pickup_ShortSword, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Pistol in Case - TM", Addresses.TM_Pickup_Pistol, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Copper Shield 2nd Floor Chest - TM", Addresses.TM_Pickup_CopperShield2ndFloorChest, "10", "33049",  LocationCheckType.UInt),
                new GenericItemsData("Equipment: Copper Shield Zarok Room - TM", Addresses.TM_Pickup_CopperShieldZarokRoom, "10", "33049",  LocationCheckType.UInt),
                new GenericItemsData("Energy Vial: Pistol Room - TM", Addresses.TM_Pickup_EnergyVialPistolRoom, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Mausoleum Room 2nd Floor - TM", Addresses.TM_Pickup_EnergyVialMausoleumRoom2F, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Second Hand Room - TM", Addresses.TM_Pickup_EnergyVialSecondHandRoom, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hidden Behind Purple Structure - TM", Addresses.TM_Pickup_GoldCoinsBehindPurpleStructure, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Mausoleum Room 2nd Floor 1 - TM", Addresses.TM_Pickup_GoldCoinsMausoleumRoom2F1, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Mausoleum Room 2nd Floor 2 - TM", Addresses.TM_Pickup_GoldCoinsMausoleumRoom2F2, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Mausoleum Room 2nd Floor 3 - TM", Addresses.TM_Pickup_GoldCoinsMausoleumRoom2F3, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Buddah Statue Staircase - TM", Addresses.TM_Pickup_GoldCoinsBuddahStatueStaircase, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Display Room Balcony Right - TM", Addresses.TM_Pickup_GoldCoinsDisplayRoomBalconyRight, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Display Room Balcony Left - TM", Addresses.TM_Pickup_GoldCoinsDisplayRoomBalconyLeft, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Zarok Room Rafters Back - TM", Addresses.TM_Pickup_GoldCoinsZarokRoomRaftersBack, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Zarok Room Rafters Left - TM", Addresses.TM_Pickup_GoldCoinsZarokRoomRaftersLeft, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Zarok Room Rafters Right - TM", Addresses.TM_Pickup_GoldCoinsZarokRoomRaftersRight, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Tomb Room Left - TM", Addresses.TM_Pickup_GoldCoinsTombRoomLeft, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Tomb Room Right - TM", Addresses.TM_Pickup_GoldCoinsTombRoomRight, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: First Hand Room - Chest - TM1", Addresses.TM_Pickup_GoldCoinsFirstHandRoomChest1, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: First Hand Room - Chest 2 - TM", Addresses.TM_Pickup_GoldCoinsFirstHandRoomChest2, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: First Hand Room - Chest 3 - TM", Addresses.TM_Pickup_GoldCoinsFirstHandRoomChest3, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Second Hand Room - Chest Right of Vial - TM", Addresses.TM_Pickup_GoldCoinsSecondHandRoomChestRightOfVial, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Second Hand Room - Chest Left of Vial - TM", Addresses.TM_Pickup_GoldCoinsSecondHandRoomChestLeftOfVial, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Second Hand Room - Chest Between Boxes - TM", Addresses.TM_Pickup_GoldCoinsSecondHandRoomChestBetweenBoxes, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Second Hand Room - Chest Hidden on Pipes - TM", Addresses.TM_Pickup_GoldCoinsSecondHandRoomChestHiddenOnPipes, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Second Hand Room - Chest on Boxes - TM", Addresses.TM_Pickup_GoldCoinsSecondHandRoomChestOnBoxes, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Book: Sir Dan - TM", Addresses.TM_Book_SirDan, "10", "0", LocationCheckType.Byte),
                new GenericItemsData("Book: The Kraken - TM", Addresses.TM_Book_TheKraken, "10", "0", LocationCheckType.Byte),
                new GenericItemsData("Book: Zarok - TM", Addresses.TM_Book_Zarok, "10", "0", LocationCheckType.Byte),
                new GenericItemsData("Winston: Dans Room - TM", Addresses.TM_Winston_DansRoom, "10", "353", LocationCheckType.UShort),
                new GenericItemsData("Winston: Pistol Room - TM", Addresses.TM_Winston_PistolRoom, "10", "356", LocationCheckType.UShort),
                new GenericItemsData("Winston: Chest on Mausoleum Room 2nd Floor - TM", Addresses.TM_Winston_ChestOnMausoleumRoom2F, "10", "326", LocationCheckType.UShort),
                new GenericItemsData("Winston: Gold Coins on Mausoleum Room 2nd Floor - TM", Addresses.TM_Winston_GoldCoinsOnMausoleumRoom2F, "10", "299", LocationCheckType.UShort),
                new GenericItemsData("Winston: Climbing Wall - TM", Addresses.TM_Winston_GoldCoinsOnMausoleumRoom2F, "10", "365", LocationCheckType.UShort),
                new GenericItemsData("Winston: Staircase After Buddah - TM", Addresses.TM_Winston_StaircaseAfterBuddah, "10", "277", LocationCheckType.UShort),
                new GenericItemsData("Winston: Chalice - TM", Addresses.TM_Winston_Chalice, "10", "289", LocationCheckType.UShort),
                new GenericItemsData("Chalice: The Museum", Addresses.TM_Pickup_Chalice, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Museum", Addresses.TM_LevelStatus, "10", "9", LocationCheckType.Byte),
            };

            return museumLocations;
        }

        private static List<GenericItemsData> GetTyrannosaurusWrexData()
        {
            List<GenericItemsData> wrexLocations = new List<GenericItemsData>()
            {

                new GenericItemsData("Life Bottle: Tyrannosaurus Wrecks", Addresses.TW_Pickup_LifeBottle, "11", "2752", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Copper Chest in Stairway - TW", Addresses.TW_Pickup_CopperChestInStairway, "11", "33049", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Behind Lion Statue - TW", Addresses.TW_Pickup_GoldCoinsBagBehindLionStatue, "11", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Near Spiv on Stairway - TW", Addresses.TW_Pickup_GoldCoinsNearSpivOnStairway, "11", "704", LocationCheckType.UShort),
                new GenericItemsData("Book: Dinosaur Display - TW", Addresses.TW_Book_DinosaurDisplay, "11", "0", LocationCheckType.Byte),
                new GenericItemsData("Winston: Entrance - TW", Addresses.TW_Winston_Entrance, "11", "188", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Tyrannosaurus Wrecks", Addresses.TW_LevelStatus, "11", "9", LocationCheckType.Byte),
            };
            return wrexLocations;
        }


        private static List<GenericItemsData> GetKensingtonData()
        {
            List<GenericItemsData> kensingtonLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: The Depot Key", Addresses.KT_Pickup_TheDepotKey, "18", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Town House Key", Addresses.KT_Pickup_TownHouseKey, "18", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Pocketwatch", Addresses.KT_Pickup_Pocketwatch, "18", "33049", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Copper Shield on Railway - KT", Addresses.KT_Pickup_CopperShieldOnRailway, "18", "33049", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Near Water in Quayside Mill - KT", Addresses.KT_Pickup_GoldCoinsBagNearWater, "18", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Pushing and Pulling - KT", Addresses.KT_Winston_PushingAndPulling, "18", "298", LocationCheckType.UShort),
                new GenericItemsData("Winston: Where the Spell was Cast - KT", Addresses.KT_Winston_WhereTheSpellWasCast, "18", "213", LocationCheckType.UShort),
                new GenericItemsData("Winston: Museum Roof - KT", Addresses.KT_Winston_MuseumRoof, "18", "380", LocationCheckType.UShort),
                new GenericItemsData("Chalice: Kensington - KT", Addresses.KT_Pickup_Chalice, "18", "704", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Kensington", Addresses.KT_LevelStatus, "18", "9", LocationCheckType.Byte),
            };
            return kensingtonLocations;
        }

        private static List<GenericItemsData> GetTheTombData()
        {
            List<GenericItemsData> tombLocations = new List<GenericItemsData>()
            {

                new GenericItemsData("Key Item: Scroll of Sekhmet", Addresses.TT_Pickup_ScrollOfSekhmet, "26", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Tablet of Horus", Addresses.TT_Pickup_TabletOfHorus, "26", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Staff of Anubis", Addresses.TT_Pickup_StaffOfAnubis, "26", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Tomb Entrance Top Right - TT", Addresses.TT_Pickup_GoldCoinsTombEntranceTopR, "26", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Tomb Entrance Top Left - TT", Addresses.TT_Pickup_GoldCoinsTombEntranceTopL, "26", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Chest Ground Floor - TT", Addresses.TT_Pickup_GoldCoinsHandAreaChestGroundFloor, "26", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Chest Upper Floor - TT", Addresses.TT_Pickup_GoldCoinsHandAreaChestUpperFloor, "26", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Entrance - TT", Addresses.TT_Winston_Entrance, "26", "404", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Tomb", Addresses.TT_LevelStatus, "26", "9", LocationCheckType.Byte),
            };
            return tombLocations;
        }

        private static List<GenericItemsData> GetTheFreakshowData()
        {
            List<GenericItemsData> freakshowLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Elephant key 1 - TF", Addresses.TF_Pickup_ElephantKey1, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Elephant key 2 - TF", Addresses.TF_Pickup_ElephantKey2, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Life Bottle: The Freakshow", Addresses.TF_Pickup_LifeBottle, "6", "2752", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Copper Shield in Chalice Room - TF", Addresses.TF_Pickup_CopperShieldInChaliceRoom, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Copper Shield in Elephant Boss Arena - TF", Addresses.TF_Pickup_CopperShieldInElephantBossArena, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag at Start Left - TF", Addresses.TF_Pickup_GoldCoinsBagAtStartLeft, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Ladies Bag Trap 1 - TF", Addresses.TF_Pickup_GoldCoinsLadiesBagTrap1, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Ladies Bag Trap 2 - TF", Addresses.TF_Pickup_GoldCoinsLadiesBagTrap2, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Ladies Bag Trap 3 - TF", Addresses.TF_Pickup_GoldCoinsLadiesBagTrap3, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Ladies Bag Trap 4 - TF", Addresses.TF_Pickup_GoldCoinsLadiesBagTrap4, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag in Tunnel - TF", Addresses.TF_Pickup_GoldCoinsBagInTunnel, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Outside Tunnel - TF", Addresses.TF_Pickup_GoldCoinsBagOutsideTunnel, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest at Water Jump - TF", Addresses.TF_Pickup_GoldCoinsChestAtWaterJump, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest Below Giant Clown - TF", Addresses.TF_Pickup_GoldCoinsChestBelowGiantClown, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest Hidden at Trampolines - TF", Addresses.TF_Pickup_GoldCoinsChestHiddenAtTrampolines, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Chest - TF", Addresses.TF_Pickup_GoldCoinsHandAreaChest, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Hidden Chest Left - TF", Addresses.TF_Pickup_GoldCoinsHandAreaHiddenChestL, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Hidden Chest Right - TF", Addresses.TF_Pickup_GoldCoinsHandAreaHiddenChestR, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Entrance - TF", Addresses.TF_Winston_Entrance, "6", "356", LocationCheckType.UShort),
                new GenericItemsData("Winston: Trampoline - TF", Addresses.TF_Winston_Trampoline, "6", "329", LocationCheckType.UShort),
                new GenericItemsData("Winston: Elephant Army - TF", Addresses.TF_Winston_ElephantArmy, "6", "399", LocationCheckType.UShort),
                new GenericItemsData("Chalice: The Freakshow", Addresses.TF_Pickup_Chalice, "6", "704", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Freakshow", Addresses.TF_LevelStatus, "6", "9", LocationCheckType.Byte),
            };
            return freakshowLocations;
        }

        private static List<GenericItemsData> GetGreenwichObservatoryData()
        {
            List<GenericItemsData> observatoryLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Life Bottle: Greenwich Observatory", Addresses.GO_Pickup_LifeBottle, "7", "2752", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Copper Shield near Bomb Chest", Addresses.GO_Pickup_CopperShieldNearBombChest, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Fountain Bag 1 - GO", Addresses.GO_Pickup_GoldCoinsFountainBag1, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Fountain Bag 2 - GO", Addresses.GO_Pickup_GoldCoinsFountainBag2, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Below Spiv - GO", Addresses.GO_Pickup_GoldCoinsBagBelowSpiv, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Below Chalice 1 - GO", Addresses.GO_Pickup_GoldCoinsBagBelowChalice1, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Below Chalice 2 - GO", Addresses.GO_Pickup_GoldCoinsBagBelowChalice2, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest Near Exit - GO", Addresses.GO_Pickup_GoldCoinsChestNearExit, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Chest 1 - GO", Addresses.GO_Pickup_GoldCoinsHandAreaChest1, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Chest 2 - GO", Addresses.GO_Pickup_GoldCoinsHandAreaChest2, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Chest 3 - GO", Addresses.GO_Pickup_GoldCoinsHandAreaChest3, "7", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Lost your head? - GO", Addresses.GO_Winston_LostYourHead, "7", "387", LocationCheckType.UShort),
                new GenericItemsData("Winston: Close To Ladder - GO", Addresses.GO_Winston_CloseToLadder, "7", "124", LocationCheckType.UShort),
                new GenericItemsData("Winston: Lever Puzzle - GO", Addresses.GO_Winston_LeverPuzzle, "7", "367", LocationCheckType.UShort),
                new GenericItemsData("Winston: Once through This Door - GO", Addresses.GO_Winston_OnceThroughThisDoor, "7", "380", LocationCheckType.UShort),
                new GenericItemsData("Chalice: Greenwhich Observatory", Addresses.GO_Pickup_Chalice, "7", "2752", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Greenwhich Observatory", Addresses.GO_LevelStatus, "7", "9", LocationCheckType.Byte),
            };
            return observatoryLocations;
        }

        private static List<GenericItemsData> GetNavalAcademyData()
        {
            List<GenericItemsData> navalLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Bellows - GONA", Addresses.GONA_Pickup_Bellows, "27", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Near Trees - GONA", Addresses.GONA_Pickup_EnergyVialNearTrees, "27", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Near Trees - GONA", Addresses.GONA_Pickup_GoldCoinsBagNearTrees, "27", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Entrance - GONA", Addresses.GONA_Winston_Balloon, "27", "404", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Naval Academy", Addresses.GO_LevelStatus, "27", "9", LocationCheckType.Byte),
            };
            return navalLocations;
        }

        private static List<GenericItemsData> GetKewGardensData()
        {
            List<GenericItemsData> gardenLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Potting Shed Key", Addresses.KG_Pickup_PottingShedKey, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Water Tank Valve - KG", Addresses.KG_Pickup_WaterTankValve, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Pond Room Valve - KG", Addresses.KG_Pickup_PondRoomValve, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Hot House Valve - KG", Addresses.KG_Pickup_HotHouseValve, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Life Bottle: Kew Gardens", Addresses.KG_Pickup_LifeBottle, "8", "2752", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Silver Shield in Gauntlet Room - KG", Addresses.KG_Pickup_SilverShieldInGauntletRoom, "8", "33049", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Behind Fence Vial 1 - KG", Addresses.KG_Pickup_EnergyVialBehindFence1, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Behind Fence Vial 2 - KG", Addresses.KG_Pickup_EnergyVialBehindFence2, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Behind Fence Chest 1 - KG", Addresses.KG_Pickup_GoldCoinsBehindFenceChest1, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Behind Fence Chest 2 - KG", Addresses.KG_Pickup_GoldCoinsBehindFenceChest2, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Near Shed - KG", Addresses.KG_Pickup_GoldCoinsBagNearShed, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest at Top of Tree in First Human Room - KG", Addresses.KG_Pickup_GoldCoinsChestAtTopOfTree, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bridge Room Vine Chest 1 - KG", Addresses.KG_Pickup_GoldCoinsBridgeRoomVineChest1, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bridge Room Vine Chest 2 - KG", Addresses.KG_Pickup_GoldCoinsBridgeRoomVineChest2, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Up Tree in Second Human Room - KG", Addresses.KG_Pickup_GoldCoinsBagUpTree, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag on Roof 1 - KG", Addresses.KG_Pickup_GoldCoinsBagOnRoof1, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag on Roof 2 - KG", Addresses.KG_Pickup_GoldCoinsBagOnRoof2, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag in Third Human Room - KG", Addresses.KG_Pickup_GoldCoinsBagInThirdHumanRoom, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Maze Chest - KG", Addresses.KG_Pickup_GoldCoinsHandMazeChest, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Maze Chest Reward 1 - KG", Addresses.KG_Pickup_GoldCoinsHandMazeChestReward1, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Maze Chest Reward 2- KG", Addresses.KG_Pickup_GoldCoinsHandMazeChestReward2, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Maze Chest Reward 3 - KG", Addresses.KG_Pickup_GoldCoinsHandMazeChestReward3, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Level Start - KG", Addresses.KG_Winston_LevelStart, "8", "346", LocationCheckType.UShort),
                new GenericItemsData("Winston: Infection - KG", Addresses.KG_Winston_Infection, "8", "390", LocationCheckType.UShort),
                new GenericItemsData("Chalice: Kew Gardens", Addresses.KG_Pickup_Chalice, "8", "704", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Kew Gardens", Addresses.GO_LevelStatus, "8", "9", LocationCheckType.Byte),
            };
            return gardenLocations;
        }

        private static List<GenericItemsData> GetDankensteinData()
        {
            List<GenericItemsData> dankensteinLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Bum - DK", Addresses.DK_Pickup_Bum, "5", "16", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Left Arm - DK", Addresses.DK_Pickup_LeftArm, "5", "1", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Left Leg - DK", Addresses.DK_Pickup_LeftLeg, "5", "4", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Right Arm - DK", Addresses.DK_Pickup_RightArm, "5", "2", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Right Leg - DK", Addresses.DK_Pickup_RightLeg, "5", "8", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Torso - DK", Addresses.DK_Pickup_Torso, "5", "32", LocationCheckType.UShort),
                new GenericItemsData("Life Bottle: Dankenstein", Addresses.DK_Pickup_LifeBottle, "5", "2752", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Silver shield - DK", Addresses.DK_Pickup_SilverShield, "5", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest Above Projector - DK", Addresses.DK_Pickup_GoldCoinsChestAboveProjector, "5", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Chest 1 - DK", Addresses.DK_Pickup_GoldCoinsHandAreaChest1, "5", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Hand Area Chest 2 - DK", Addresses.DK_Pickup_GoldCoinsHandAreaChest2, "5", "704", LocationCheckType.UShort),
                new GenericItemsData("Book: Reanimation - DK", Addresses.DK_Book_Reanimation, "5", "0", LocationCheckType.UShort),
                new GenericItemsData("Winston: Save Point - DK", Addresses.DK_Winston_SavePoint, "5", "404", LocationCheckType.UShort),
                new GenericItemsData("Chalice: Dankenstein - DK", Addresses.DK_Pickup_Chalice, "5", "704", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Dankenstein", Addresses.DK_LevelStatus, "5", "9", LocationCheckType.Byte),
            };
            return dankensteinLocations;
        }

        private static List<GenericItemsData> GetIronSluggerData()
        {
            List<GenericItemsData> ironSluggerLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Book: Dankenstein Manual - IS", Addresses.IS_Book_DankensteinManual, "4", "0", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Iron Slugger", Addresses.IS_LevelStatus, "5", "9", LocationCheckType.Byte),
            };
            return ironSluggerLocations;
        }

        private static List<GenericItemsData> GetWulfrumHallData()
        {
            List<GenericItemsData> wulfrumLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Front Door Key - WH", Addresses.WH_Pickup_FrontDoorKey, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Life Bottle: Wulfrum Hall", Addresses.WH_Pickup_LifeBottle, "17", "2752", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Silver Shield Close To Vampire Room 2 - WH", Addresses.WH_Pickup_SilverShieldCloseToVampireRoom2, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Near Kitchen Stairs - WH", Addresses.WH_Pickup_EnergyVialNearKitchenStairs, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Left Room Of Entrance - WH", Addresses.WH_Pickup_EnergyVialLeftRoomOfEntrance, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: In Front Of Hall's Staircase - WH", Addresses.WH_Pickup_EnergyVialInFrontOfHallsStaircase, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag Left Side Of Entrance Stairs - WH", Addresses.WH_Pickup_GoldCoinsBagLeftOfEntrance, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest Close To Vampire Room 1 - WH", Addresses.WH_Pickup_GoldCoinsChestCloseToVampireRoom1, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest in Vampire Room 3 - WH", Addresses.WH_Pickup_GoldCoinsChestInVampireRoom3, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag in Vampire Room 5 - WH", Addresses.WH_Pickup_GoldCoinsBagInVampireRoom5, "17", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Level Start - WH", Addresses.WH_Winston_LevelStart, "17", "306", LocationCheckType.UShort),
                new GenericItemsData("Winston: Vampires - WH", Addresses.WH_Winston_Vampires, "17", "376", LocationCheckType.UShort),
                new GenericItemsData("Chalice: Wulfrum Hall", Addresses.WH_Pickup_Chalice, "17", "2752", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Wulfrum Hall", Addresses.WH_LevelStatus, "17", "9", LocationCheckType.Byte),
            };
            return wulfrumLocations;
        }


        private static List<GenericItemsData> GetTheCountData()
        {
            List<GenericItemsData> countLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Winston: Level Start - TC", Addresses.TC_Winston_LevelStart, "16", "394", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Gold Chest - TC", Addresses.TC_Pickup_GoldChest, "16", "704", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Count", Addresses.TC_LevelStatus, "16", "9",LocationCheckType.Byte),
            };
            return countLocations;
        }

        private static List<GenericItemsData> GetWhitechapelData()
        {
            List<GenericItemsData> whitechapelLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Library Key in House Basement - WC", Addresses.WC_Pickup_LibraryKeyInHouseBasement, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Griffin Shield - WC", Addresses.WC_Pickup_GriffinShield, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Membership Card - WC", Addresses.WC_Pickup_MembershipCard, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Elegant Suit - WC", Addresses.WC_Pickup_ElegantSuite, "9", "2", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Unicorn Shield - WC", Addresses.WC_Pickup_UnicornShield, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Beard - WC", Addresses.WC_Pickup_Beard, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Life Bottle: Whitechapel", Addresses.WC_Pickup_LifeBottle, "9", "341", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Silver Shield in Library - WC", Addresses.WC_Pickup_SilverShieldInLibrary, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Flaming Crossbow Inside House Near Unicorn Shield - WC", Addresses.WC_Pickup_FlamingCrossbow, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag 1 in House Basement - WC", Addresses.WC_Pickup_GoldCoinsBag1InHouseBasement, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag 2 in House Basement - WC", Addresses.WC_Pickup_GoldCoinsBag2InHouseBasement, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag 1 Tailor Shop Basement -WC", Addresses.WC_Pickup_GoldCoinsBag1TailorShopBasement, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag 2 Tailor Shop Basement - WC", Addresses.WC_Pickup_GoldCoinsBag2TailorShopBasement, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag 3 Tailor Shop Basement - WC", Addresses.WC_Pickup_GoldCoinsBag3TailorShopBasement, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest Inside Club - WC", Addresses.WC_Pickup_GoldCoinsChestInsideClub, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Book: Isibod Brunel - WC", Addresses.WC_Book_IsibodBrunel, "9", "0", LocationCheckType.UShort),
                new GenericItemsData("Winston: Kiya Last Seen - WC", Addresses.WC_Winston_KiyaLastSeen, "9", "350", LocationCheckType.UShort),
                new GenericItemsData("Chalice: Whitechapel", Addresses.WC_Pickup_Chalice, "9", "704", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Whitechapel", Addresses.WC_LevelStatus, "16", "9", LocationCheckType.Byte),
            };
            return whitechapelLocations;
        }

        private static List<GenericItemsData> GetTheSewersData()
        {
            List<GenericItemsData> sewersLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Poster - TS", Addresses.TS_Pickup_Poster, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Life Bottle: Sewers", Addresses.TS_Pickup_LifeBottle, "14", "2752", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Gold Shield in Hub Area - TS", Addresses.TS_Pickup_GoldShieldInHubArea, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Event: Girls Freed - 1 - TS", Addresses.TS_GirlsFreed_1, "14", "1", LocationCheckType.UShort),
                new GenericItemsData("Event: Girls Freed - 2 - TS", Addresses.TS_GirlsFreed_2, "14", "2", LocationCheckType.UShort),
                new GenericItemsData("Event: Girls Freed - 3 - TS", Addresses.TS_GirlsFreed_3, "14", "3", LocationCheckType.UShort),
                new GenericItemsData("Event: Girls Freed - 4 - TS", Addresses.TS_GirlsFreed_4, "14", "4", LocationCheckType.UShort),
                new GenericItemsData("Event: Girls Freed - 5 - TS", Addresses.TS_GirlsFreed_5, "14", "5", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Vial 1 End Pipes Area - TS", Addresses.TS_Pickup_EnergyVial1EndPipesArea, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Vial 2 End Pipes Area - TS", Addresses.TS_Pickup_EnergyVial2EndPipesArea, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Vial 3 End Pipes Area - TS", Addresses.TS_Pickup_EnergyVial3EndPipesArea, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag at Pipes Area Start - TS", Addresses.TS_Pickup_GoldCoinsBagAtPipesAreaStart, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag 1 in Pipes Puzzle Room - TS", Addresses.TS_Pickup_GoldCoinsBag1InPipesPuzzleRoom, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag 2 in Pipes Puzzle Room - TS", Addresses.TS_Pickup_GoldCoinsBag2InPipesPuzzleRoom, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Reward Chest 1 - TS", Addresses.TS_Pickup_GoldCoinsRewardChest1, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Reward Chest 2 - TS", Addresses.TS_Pickup_GoldCoinsRewardChest2, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Save Point - TS", Addresses.TS_Winston_SavePoint, "14", "404", LocationCheckType.UShort),
                new GenericItemsData("Winston: Something Really Interesting - TS", Addresses.TS_Winston_SomethingReallyInteresting, "14", "648", LocationCheckType.UShort),
                new GenericItemsData("Chalice: Sewers", Addresses.TS_Pickup_Chalice, "14", "704", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Sewers", Addresses.TS_LevelStatus, "14", "9", LocationCheckType.Byte),
            };
            return sewersLocations;
        }

        private static List<GenericItemsData> GetTheTimeMachineData()
        {
            List<GenericItemsData> timeMachineLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Time Machine Piece - Planetarium - TTM", Addresses.TTM_Pickup_TimeMachinePiecePlanetarium, "15", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Time Machine Piece - Grammar Horn - TTM", Addresses.TTM_Pickup_TimeMachinePieceGrammarHorn, "15", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Time Machine Piece - Moon Exhibit - TTM", Addresses.TTM_Pickup_TimeMachinePieceMoonExhibit, "15", "704", LocationCheckType.UShort),
                new GenericItemsData("Book: Space Beacon - TTM", Addresses.TTM_Book_SpaceBeacon, "15", "0", LocationCheckType.UShort),
                new GenericItemsData("Book: Grammar Horn - TTM", Addresses.TTM_Book_GrammarHorn, "15", "0", LocationCheckType.UShort),
                new GenericItemsData("Book: Moon Exhibit - TTM", Addresses.TTM_Book_MoonExhibit, "15", "0", LocationCheckType.UShort),
                new GenericItemsData("Book: The Time Machine", Addresses.TTM_Book_TheTimeMachine, "15", "0", LocationCheckType.UShort),
                new GenericItemsData("Winston: Entrance - TTM", Addresses.TTM_Winston_Entrance, "15", "404", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Time Machine", Addresses.TTM_LevelStatus, "15", "9",LocationCheckType.Byte),
            };
            return timeMachineLocations;
        }

        private static List<GenericItemsData> GetTheTimeMachineSewersData()
        {
            List<GenericItemsData> timeMachineSewerLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Time Stone - Hut Trap - TTMTS", Addresses.TTMTS_Pickup_TimeStoneHutTrap, "28", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: King Mullocks Key - Downing King - TTMTS", Addresses.TTMTS_Pickup_KingMullocksKey, "28", "704", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Good Lightning - Changing Room - TTMTS", Addresses.TTMTS_Pickup_GoodLightning, "28", "257", LocationCheckType.UShort),
                new GenericItemsData("Winston: Entrance - TTMTS", Addresses.TTMTS_Winston_Entrance, "28", "404", LocationCheckType.UShort),
                new GenericItemsData("Winston: Kings Hat - TTMTS", Addresses.TTMTS_Winston_KingsHat, "28", "372", LocationCheckType.UShort),
                new GenericItemsData("Winston: Stealing Time Stone - TTMTS", Addresses.TTMTS_Winston_StealingTimeStone, "28", "381", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Time Machine - The Sewers", Addresses.TTMTS_LevelStatus, "28", "9", LocationCheckType.Byte),
            };
            return timeMachineSewerLocations;
        }

        private static List<GenericItemsData> GetTheRipperData()
        {
            List<GenericItemsData> ripperLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Winston: Entrance - TR", Addresses.TR_Winston_Entrance, "29", "404", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Ripper", Addresses.TR_LevelStatus, "29", "9",LocationCheckType.Byte),
            };
            return ripperLocations;
        }

        private static List<GenericItemsData> GetCathedralSpiresData()
        {
            List<GenericItemsData> spiresLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Lost Soul - Bottom left of Cathedral - CS", Addresses.CS_Pickup_LostSoul, "2", "1", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Top of Cathedral - CS", Addresses.CS_Pickup_LostSoul, "2", "2", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Left Chapel on the Roo - CS", Addresses.CS_Pickup_LostSoul, "2", "3", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Right Chapel on the Roo - CS", Addresses.CS_Pickup_LostSoul, "2", "4", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Next to Life Bottle - CS", Addresses.CS_Pickup_LostSoul, "2", "5", LocationCheckType.UShort),
                new GenericItemsData("Life Bottle: Cathedral Spires", Addresses.CS_Pickup_LifeBottle, "2", "2720", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Gold Shield in Chest at Start - CS", Addresses.CS_Pickup_GoldShieldInChestAtStart, "2", "33049", LocationCheckType.UShort),
                new GenericItemsData("Equipment: Silver Shield Chest near Gold Coins - CS", Addresses.CS_Pickup_SilverShieldChest, "2", "33049", LocationCheckType.UShort),
                new GenericItemsData("Energy Vial: Next to Spiv - CS", Addresses.CS_Pickup_EnergyVialNextToSpiv, "2", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bottom Right Near Spiv - CS", Addresses.CS_Pickup_GoldCoinsBottomRightNearSpiv, "2", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest After First Lost Soul - CS", Addresses.CS_Pickup_GoldCoinsChestAfterFirstLostSoul, "2", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest After First Flame Gargoyles - CS", Addresses.CS_Pickup_GoldCoinsChestAfterFirstFlameGargoyles, "2", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Chest Near Silver Shield Chest - CS", Addresses.CS_Pickup_GoldCoinsChestNearSilverShield, "2", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Bag at top of Spire - CS", Addresses.CS_Pickup_GoldCoinsBagAtTopOfSpire, "2", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Entrance - CS", Addresses.CS_Winston_Entrance, "2", "404", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Cathedral Spires", Addresses.CS_LevelStatus, "2", "9", LocationCheckType.Byte),
            };
            return spiresLocations;
        }

        private static List<GenericItemsData> GetCathedralSpiresTheDescentData()
        {
            List<GenericItemsData> descentLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Lost Soul - Left Jump at Chandelere - CSTD", Addresses.CSTD_Pickup_LostSoulLeftChandelier, "30", "6", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Right Jump at Chandelere - CSTD", Addresses.CSTD_Pickup_LostSoulRightChandelier, "30", "7", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Demon Statue Mausoleum - CSTD", Addresses.CSTD_Pickup_LostSoulDemonStatueMausoleum, "30", "8", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Entrance Room - CSTD", Addresses.CSTD_Pickup_LostSoulEntranceRoom, "30", "9", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Top of Pully Front - CSTD", Addresses.CSTD_Pickup_LostSoulTopOfPulleyFront, "30", "10", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Top of Pully Back - CSTD", Addresses.CSTD_Pickup_LostSoulTopOfPulleyBack, "30", "11", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Lost Soul - Pully Room Right - CSTD", Addresses.CSTD_Pickup_LostSoulPulleyRoomRight, "30", "12", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Golden Cog in Room - CSTD", Addresses.CSTD_Pickup_GoldenCogInRoom, "30", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Golden Cog in Hand Area - CSTD", Addresses.CSTD_Pickup_GoldenCogInHandArea, "30", "704", LocationCheckType.UShort),
                new GenericItemsData("Key Item: Spell Page - Demon Death - CSTD", Addresses.CSTD_Pickup_SpellPageDemonDeath, "30", "704", LocationCheckType.UShort),
                new GenericItemsData("Life Bottle: Cathedral Spires, The Decent", Addresses.CSTD_Pickup_LifeBottle, "30", "2752", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Golden Cog Room Entrance - CSTD", Addresses.CSTD_Pickup_GoldCoinsGoldenCogRoomEntrance, "30", "704", LocationCheckType.UShort),
                new GenericItemsData("Gold Coins: Golden Cog Room Bottom - CSTD", Addresses.CSTD_Pickup_GoldCoinsGoldenCogRoomBottom, "30", "704", LocationCheckType.UShort),
                new GenericItemsData("Winston: Entrance - CSTD", Addresses.CSTD_Winston_Entrance, "30", "404", LocationCheckType.UShort),
                new GenericItemsData("Cleared: Cathedral Spires - The Descent", Addresses.CSTD_LevelStatus, "30", "9", LocationCheckType.Byte),
            };
            return descentLocations;
        }

        private static List<GenericItemsData> GetTheDemonData()
        {
            List<GenericItemsData> demonLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Winston: Entrance - TD", Addresses.TD_Winston_Entrance, "3", "331", LocationCheckType.UShort),
                new GenericItemsData("Cleared: The Demon", Addresses.TD_LevelStatus, "3", "41",LocationCheckType.Byte),
            };
            return demonLocations;
        }

    }
}