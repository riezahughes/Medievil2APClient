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
            "Cathedral Spires: The Descent",
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


                        if (loc.Name.Contains("Key Item:") || loc.Name.Contains("Chalice Reward") || loc.Name.Contains("Cleared:") || loc.Name.Contains("Chalice:") || loc.Name.Contains("Rune:") || loc.Name.Contains("Equipment:") || loc.Name.Contains("Gold Coins:") || loc.Name.Contains("Skill:") || loc.Name.Contains("Life Bottle:") || loc.Name.Contains("Energy Vial:") || loc.Name.Contains("Fairy") || loc.Name.Contains("Egg Drop"))
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
                                    CheckType = LocationCheckType.UInt,
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

        private static List<GenericItemsData> GetTheMuseumData()
        {
            List<GenericItemsData> museumLocations = new List<GenericItemsData>()
            {
                new GenericItemsData("Key Item: Museum Key", Addresses.TM_Pickup_MuseumKey, "10", "704"),
                new GenericItemsData("Key Item: Dinosaur Key", Addresses.TM_Pickup_MuseumKey, "10", "704"), // these aren't working
                new GenericItemsData("Key Item: Cannonball", Addresses.TM_Pickup_CannonBall, "10", "704"),
                new GenericItemsData("Key Item: Torch", Addresses.TM_Pickup_Torch, "10", "704"),
                new GenericItemsData("Equipment: Short Sword", Addresses.TM_Pickup_Shortsword, "10", "704"),
                new GenericItemsData("Equipment: Pistol", Addresses.TM_Pickup_Pistol, "10", "704"),
                new GenericItemsData("Energy Vial: Pistol Room", Addresses.TM_Pickup_EnergyInPistolRoom, "10", "704"),
                new GenericItemsData("Energy Vial: Mausoleum Room 2nd Floor", Addresses.TM_Pickup_EnergyInMausoleumRoom2F, "10", "704"),
                new GenericItemsData("Gold Coins: Mausoleum Room 2nd Floor 1", Addresses.TM_Pickup_GoldCoinsInMausoleumRoom2F1, "10", "704"),
                new GenericItemsData("Gold Coins: Mausoleum Room 2nd Floor 2", Addresses.TM_Pickup_GoldCoinsInMausoleumRoom2F2, "10", "704"),
                new GenericItemsData("Gold Coins: Mausoleum Room 2nd Floor 3", Addresses.TM_Pickup_GoldCoinsInMausoleumRoom2F3, "10", "704"),
                new GenericItemsData("Gold Coins: Buddah Statue Staircase", Addresses.TM_Pickup_GoldCoinsBuddahStaircase, "10", "704"),
                new GenericItemsData("Gold Coins: Zarok Room Rafters 1", Addresses.TM_Pickup_GoldCoinsBuddahStaircase, "10", "704"), // these aren't working
                new GenericItemsData("Gold Coins: Zarok Room Rafters 2", Addresses.TM_Pickup_GoldCoinsBuddahStaircase, "10", "704"), // these aren't working
                new GenericItemsData("Chalice: The Museum", Addresses.TM_Pickup_Chalice, "10", "704"),
                new GenericItemsData("Cleared: The Museum", Addresses.TM_LevelStatus, "10", "25"),
            };

            return museumLocations;
        }


    }
}