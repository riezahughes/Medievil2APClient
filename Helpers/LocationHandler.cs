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
            "Greenwich Observatory ",
            "Greenwich, Naval Academy",
            "Kew Gardens",
            "Dankenstein",
            "Iron Slugger",
            "Wulfrum Hall",
            "The Count",
            "Whitechapel",
            "The Sewers",
            "The Time Machine",
            "The Time Machine, Sewers",
            "The Ripper",
            "Cathedral Spires",
            "Cathedral Spires, The Descent",
            "The Demon",
];

            List<ILocation> locations = new List<ILocation>();

            Dictionary<string, List<GenericItemsData>> allLevelLocations = new Dictionary<string, List<GenericItemsData>>();

            // Level Locations
            allLevelLocations.Add("The Museum", GetTheMuseumData());

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

                        //if (loc.Name.ToLower().Contains("in crystal"))
                        //{
                        //    {
                        //        List<ILocation> conditionalChoice = new List<ILocation>();

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Level Check",
                        //            Address = Addresses.CurrentLevel,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = loc.LevelId
                        //        });

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Crystal Check",
                        //            Address = loc.Address,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //        });

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Loading Check",
                        //            Address = Addresses.IsLoaded,
                        //            CheckType = LocationCheckType.UShort,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = "59580"
                        //        });

                        //        CompositeLocation location = new CompositeLocation()
                        //        {
                        //            Name = loc.Name,
                        //            Id = locationId,
                        //            CheckType = LocationCheckType.AND,
                        //            Conditions = conditionalChoice,
                        //        };

                        //        locations.Add(location);
                        //        location_index++;
                        //        continue;
                        //    };
                        //}

                        //if (loc.IsInChest) // if it's cleared and we don't have an option set 
                        //{
                        //    {
                        //        List<ILocation> conditionalChoice = new List<ILocation>();

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Level Check",
                        //            Address = Addresses.CurrentLevel,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = loc.LevelId
                        //        });

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Chest Check",
                        //            Address = loc.Address,
                        //            CheckType = LocationCheckType.Short,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = loc.LevelId == "22" ? "500" : "801" // set zarok chests to wierd numbers so they don't trigger
                        //        });

                        //        CompositeLocation location = new CompositeLocation()
                        //        {
                        //            Name = loc.Name,
                        //            Id = locationId,
                        //            CheckType = LocationCheckType.AND,
                        //            Conditions = conditionalChoice,
                        //        };

                        //        locations.Add(location);
                        //        location_index++;
                        //        continue;
                        //    };
                        //}

                        //if (loc.Name.Contains("Gauntlet Cleared:")) // if it's cleared and we don't have an option set 
                        //{
                        //    {
                        //        List<ILocation> conditionalChoice = new List<ILocation>();

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Level Check",
                        //            Address = Addresses.CurrentLevel,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = loc.LevelId
                        //        });

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Gauntlet Check",
                        //            Address = loc.Address,
                        //            CheckType = LocationCheckType.UShort,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = loc.Check
                        //        });

                        //        CompositeLocation location = new CompositeLocation()
                        //        {
                        //            Name = loc.Name,
                        //            Id = locationId,
                        //            CheckType = LocationCheckType.AND,
                        //            Conditions = conditionalChoice

                        //        };
                        //        locations.Add(location);
                        //        location_index++;
                        //        continue;
                        //    };
                        //}

                        //if (loc.Name.Contains("Cleared: Zaroks Lair")) // if it's cleared and we don't have an option set 
                        //{
                        //    {
                        //        List<ILocation> conditionalChoice = new List<ILocation>();

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Cleared: Zaroks Lair",
                        //            Address = loc.Address,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = "101"
                        //        });
                        //        CompositeLocation location = new CompositeLocation()
                        //        {
                        //            Name = loc.Name,
                        //            Id = locationId,
                        //            CheckType = LocationCheckType.AND,
                        //            Conditions = conditionalChoice
                        //        };

                        //        locations.Add(location);
                        //        location_index++;
                        //        continue;
                        //    }
                        //}

                        //if (loc.Name.Contains("Cleared: ")) // if it's cleared and we don't have an option set 
                        //{

                        //    {
                        //        List<ILocation> conditionalChoice = new List<ILocation>();

                        //        {
                        //            conditionalChoice.Add(new Location()
                        //            {
                        //                Id = -1,
                        //                Name = "Cleared Level",
                        //                Address = loc.Address,
                        //                CheckType = LocationCheckType.Bit,
                        //                AddressBit = 4
                        //            });
                        //        }



                        //        CompositeLocation location = new CompositeLocation()
                        //        {
                        //            Name = loc.Name,
                        //            Id = locationId,
                        //            CheckType = LocationCheckType.AND,
                        //            Conditions = conditionalChoice
                        //        };



                        //        locations.Add(location);
                        //        location_index++;
                        //        continue;
                        //    }
                        //}

                        //if (loc.Name == "Chalice: Ant Hill")
                        //{
                        //    {
                        //        List<ILocation> conditionalChoice = new List<ILocation>();

                        //        {
                        //            conditionalChoice.Add(new Location()
                        //            {
                        //                Id = -1,
                        //                Name = "Got Anthill Chalice",
                        //                Address = loc.Address,
                        //                CheckType = LocationCheckType.Bit,
                        //                AddressBit = 5
                        //            });
                        //        }

                        //        CompositeLocation location = new CompositeLocation()
                        //        {
                        //            Name = loc.Name,
                        //            Id = locationId,
                        //            CheckType = LocationCheckType.AND,
                        //            Conditions = conditionalChoice
                        //        };



                        //        locations.Add(location);
                        //        location_index++;
                        //        continue;
                        //    }
                        //}

                        //if (loc.Name.Contains("Gargoyle:"))
                        //{
                        //    if (gargoyleSanity == 1)
                        //    {
                        //        List<ILocation> conditionalChoice = new List<ILocation>();

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Level Check",
                        //            Address = Addresses.CurrentLevel,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = loc.LevelId
                        //        });

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Dan Frozen",
                        //            Address = Addresses.DanFrozen,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = "2"
                        //        });

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Gargoyle Check",
                        //            Address = loc.Address,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = "0"
                        //        });

                        //        CompositeLocation location = new CompositeLocation()
                        //        {
                        //            Name = loc.Name,
                        //            Id = locationId,
                        //            CheckType = LocationCheckType.AND,
                        //            Conditions = conditionalChoice
                        //        };

                        //        locations.Add(location);
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine($"Gargoylesanity not on. ignoring {loc.Name}, id: {loc.Id}");
                        //    }
                        //    location_index++;
                        //    continue;
                        //}

                        //if (loc.Name.Contains("Book:") && bookSanity == 1)
                        //{
                        //    if (bookSanity == 1)
                        //    {
                        //        List<ILocation> conditionalChoice = new List<ILocation>();

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Level Check",
                        //            Address = Addresses.CurrentLevel,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = loc.LevelId
                        //        });

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Dan Frozen",
                        //            Address = Addresses.DanFrozen,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = "2"
                        //        });

                        //        conditionalChoice.Add(new Location()
                        //        {
                        //            Id = -1,
                        //            Name = "Book Check",
                        //            Address = loc.Address,
                        //            CheckType = LocationCheckType.Byte,
                        //            CompareType = LocationCheckCompareType.Match,
                        //            CheckValue = "0"
                        //        });

                        //        CompositeLocation location = new CompositeLocation()
                        //        {
                        //            Name = loc.Name,
                        //            Id = locationId,
                        //            CheckType = LocationCheckType.AND,
                        //            Conditions = conditionalChoice
                        //        };

                        //        locations.Add(location);
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine($"BookSanity not on. ignoring {loc.Name}, id: {loc.Id}");
                        //    }
                        //    location_index++;
                        //    continue;
                        //}

                        if (loc.Name.Contains("Key Item:") || loc.Name.Contains("Chalice Reward") || loc.Name.Contains("Cleared:") || loc.Name.Contains("Chalice:") || loc.Name.Contains("Equipment:") || loc.Name.Contains("Gold Coins:") || loc.Name.Contains("Skill:") || loc.Name.Contains("Life Bottle:") || loc.Name.Contains("Energy Vial:") || loc.Name.Contains("Book:") || loc.Name.Contains("Winston:"))
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

            //Console.WriteLine($"Count is {locations.Count()}");
            //foreach (var location in locations)
            //{
            //    Console.WriteLine($"{location.Id}: {location.Name}");
            //}

            return locations;
        }

        //private static List<GenericItemsData> Hub()
        //{
        //    List<GenericItemsData> museumLocations = new List<GenericItemsData>()
        //    {

        //    }
        //}

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
                new GenericItemsData("Winston: Dans Room - TM", Addresses.TM_Winston_DansRoom, "10", "32797", LocationCheckType.UInt),
                new GenericItemsData("Winston: Pistol Room - TM", Addresses.TM_Winston_PistolRoom, "10", "32797", LocationCheckType.UInt),
                new GenericItemsData("Winston: Chest on Mausoleum Room 2nd Floor - TM", Addresses.TM_Winston_ChestOnMausoleumRoom2F, "10", "32797", LocationCheckType.UInt),
                new GenericItemsData("Winston: Gold Coins on Mausoleum Room 2nd Floor - TM", Addresses.TM_Winston_GoldCoinsOnMausoleumRoom2F, "10", "32797", LocationCheckType.UInt),
                new GenericItemsData("Winston: Staircase after buddah - TM", Addresses.TM_Winston_StaircaseAfterBuddah, "10", "32797", LocationCheckType.UInt),
                new GenericItemsData("Winston: Chalice - TM", Addresses.TM_Pickup_Chalice, "10", "32797", LocationCheckType.UInt),
                new GenericItemsData("Chalice: The Museum", Addresses.TM_Pickup_Chalice, "10", "704", LocationCheckType.UShort),
                new GenericItemsData("Chalice: The Museum", Addresses.TM_Pickup_Chalice, "10", "704", LocationCheckType.UShort),
            };

            return museumLocations;
        }


        //private static List<GenericItemsData> GetTheMuseumData()
        //{
        //    List<GenericItemsData> museumLocations = new List<GenericItemsData>()
        //    {

        //    }
        //}


    }
}