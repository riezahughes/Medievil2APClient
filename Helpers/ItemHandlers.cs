﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Archipelago.Core.Models;
using Archipelago.Core.Util;
using SharpDX.Direct2D1.Effects;
using SharpDX.DirectWrite;

namespace MedievilArchipelago.Helpers
{


    internal class ItemHandlers
    {

        internal const int percentageMax = 20480;
        internal const int countMax = 32767;
        internal const int maxHealth = 300;
        internal const int maxLifeBottleValue = 3000;

        public static List<string> ListOfKeyItemStrings = new List<string>()
        {
            "Torch",
            "Poster",
            "Dan's Head",
            "Chalice of Souls",
            "Left Leg",
            "Right Leg",
            "Left Arm",
            "Right Arm",
            "Bum",
            "Torso",
            "Bellows",
            "Lost Soul",
            "Golden Cog 1",
            "Golden Cog 2",
            "Spell Page",
            "Griffin Shield",
            "Unicorn Shield",
            "Beard" ,
            "Library Key",
            "Club Membership Card",
            "Elephant Key 1",
            "Elephant Key 2",
            "Time Machine Piece (Contact Room)",
            "Time Machine Piece (Earth Room)",
            "Time Machine Piece (Space Room)",
            "King Mullocks Key",
            "Staff Of Anubis",
            "Scroll Of Sekhmet",
            "Tablet Of Horus",
            "Pocket Watch",
            "Town House Key",
            "Time Stone",
            "Antidote",
            "Pond Room Valve",
            "Hot House Valve",
            "Water Tank Valve",
            "Cannon Ball",
            "Front Door Key",
            "Potting Shed Key",
            "Depot Key",
            "Museum Key",
            "Dinosaur Key",
        };

        public static List<string> ListOfWeaponStrings = new List<string>()
        {
            "Small Sword",
            "Broad Sword",
            "Magic Sword",
            "Cane Stick",
            "Pistol",
            "Hammer",
            "Crossbow",
            "Flaming Crossbow",
            "Axe",
            "Gatling Gun",
            "Lightning",
            "Blunderbuss",
            "Bombs",
            "Chicken Drumsticks",
        };

        public static List<string> ListOfWeaponAmmoStrings = new List<string>()
        {
            "Pistol",
            "Hammer",
            "Crossbow",
            "Flaming Crossbow",
            "Axe",
            "Gatling Gun",
            "Lightning",
            "Blunderbuss",
            "Bombs",
            "Chicken Drumsticks",
            "Torch",
        };

        public static List<string> ListOfWeaponChargeStrings = new List<string>()
        {
            "Broadsword",
            "Lightning",
        };

        public static Dictionary<string, int> WeaponEquipDictionary = new Dictionary<string, int>
        {
            {"Equipment: Small Sword", 0},
            {"Equipment: Broadsword", 1},
            {"Equipment: Magic Sword", 2},
            {"Equipment: Cane Stick", 3},
            {"Equipment: Pistol", 4},
            {"Equipment: Hammer", 5},
            {"Equipment: Crossbow", 6},
            {"Equipment: Flaming Crossbow", 7},
            {"Equipment: Axe", 8},
            {"Equipment: Gatling Gun", 9},
            {"Equipment: Lightning",11 },
            {"Dans Arm", 12 },
            {"Equipment: Blunderbuss",13},
            {"Equipment: Bombs", 14},
            {"Equipment: Chicken Drumsticks", 15},
            {"Equipment: Torch", 16}
        };

        public static Dictionary<string, int> ShieldEquipDictionary = new Dictionary<string, int>
        {
            {"Equipment: Copper Shield", 0},
            {"Equipment: Silver Shield", 1},
            {"Equipment: Gold Shield", 2}
        };

        public static Dictionary<string, uint> FlattenedInventoryStrings()
        {
            Dictionary<string, Dictionary<string, uint>> currentDict = StatusAndInventoryAddressDictionary();
            Dictionary<string, uint> newDict = new Dictionary<string, uint>();

            List<string> validList = new List<string>
            {
                "Equipment",
                "Player Stats",
                "Key Items",
                "Skills",
                "Level Status"
            };


            foreach (KeyValuePair<string, Dictionary<string, uint>> location in currentDict)
            {
                string categoryName = location.Key;

                if (!validList.Contains(categoryName))
                {
                    continue;
                }

                Dictionary<string, uint> categoryItems = location.Value;


                foreach (KeyValuePair<string, uint> item in categoryItems)
                {
                    string prefix = location.Key == "Level Status" ? "Cleared: " : "";
                    string itemName = prefix + item.Key;
                    uint itemAddress = item.Value;
                    newDict.Add(itemName, itemAddress);
                }

            }

            return newDict;
        }

        public static Dictionary<string, int> AmmoAndChargeLimits()
        {
            return new Dictionary<string, int>
            {
                ["Pistol"] = 200,
                ["Blunderbuss"] = 50,
                ["Crossbow"] = 200,
                ["Flaming Crossbow"] = 200,
                ["Broad Sword"] = 4096,
                ["Bombs"] = 20,
                ["Chicken Drumsticks"] = 30,
                ["Gatling Gun"] = 999,
                ["Lightning"] = 4096
            };
        }


        public static Dictionary<string, Dictionary<string, uint>> StatusAndInventoryAddressDictionary()
        {
            return new Dictionary<string, Dictionary<string, uint>>
            {
                ["Equipment"] = new Dictionary<string, uint>
                {
                    ["Small Sword"] = Addresses.SmallSword,
                    ["Broadsword"] = Addresses.BroadSword,
                    ["Magic Sword"] = Addresses.MagicSword,
                    ["Cane Stick"] = Addresses.CaneStick,
                    ["Pistol"] = Addresses.Pistol,
                    ["Hammer"] = Addresses.Hammer,
                    ["Crossbow"] = Addresses.Crossbow,
                    ["Flaming Crossbow"] = Addresses.FlamingCrossbow,
                    ["Axe"] = Addresses.Axe,
                    ["Gatling Gun"] = Addresses.GatlingGun,
                    ["Good Lightning"] = Addresses.GoodLightning,
                    ["Lightning"] = Addresses.Lightning,
                    ["Blunderbuss"] = Addresses.Blunderbuss,
                    ["Bombs"] = Addresses.Bombs,
                    ["Chicken Drumsticks"] = Addresses.ChickenDrumsticks,
                    ["Copper Shield"] = Addresses.CopperShield,
                    ["Silver Shield"] = Addresses.SilverShield,
                    ["Gold Shield"] = Addresses.GoldShield,
                },

                ["Ammo"] = new Dictionary<string, uint>
                {
                    ["Pistol"] = Addresses.Pistol,
                    ["Broadsword"] = Addresses.BroadSword,
                    ["Crossbow"] = Addresses.Crossbow,
                    ["Flaming Crossbow"] = Addresses.FlamingCrossbow,
                    ["Gatling Gun"] = Addresses.GatlingGun,
                    ["Lightning"] = Addresses.Lightning,
                    ["Blunderbuss"] = Addresses.Blunderbuss,
                    ["Bombs"] = Addresses.Bombs,
                    ["Chicken Drumsticks"] = Addresses.ChickenDrumsticks,
                    ["Copper Shield"] = Addresses.CopperShield,
                    ["Silver Shield"] = Addresses.SilverShield,
                    ["Gold Shield"] = Addresses.GoldShield,
                    ["Dan's Armour"] = Addresses.GoldenArmour
                },

                ["Player Stats"] = new Dictionary<string, uint>
                {
                    ["Gold Coins"] = Addresses.DansCurrentGold,
                    ["Health"] = Addresses.DansCurrentEnergy,
                    ["Health Vial"] = Addresses.DansCurrentEnergy,
                    ["Life Bottle"] = Addresses.DansCurrentLifeBottles,
                    ["Energy"] = Addresses.DansCurrentEnergy,
                },
                ["Skills"] = new Dictionary<string, uint>
                {
                    ["Daring Dash"] = Addresses.DaringDashSkill,
                    ["Dan's Hand"] = Addresses.DansHandSkill

                },
                ["Key Items"] = new Dictionary<string, uint>
                {
                    ["Torch"] = Addresses.Torch,
                    ["Poster"] = Addresses.Poster,
                    ["Dan's Head"] = Addresses.DansHead,
                    ["Chalice of Souls"] = Addresses.ChaliceOfSouls,
                    ["Left Leg"] = Addresses.LeftLeg,
                    ["Right Leg"] = Addresses.RightLeg,
                    ["Left Arm"] = Addresses.LeftArm,
                    ["Right Arm"] = Addresses.RightArm,
                    ["Bum"] = Addresses.Bum,
                    ["Torso"] = Addresses.Torso,
                    ["Bellows"] = Addresses.Bellows,
                    ["Lost Soul"] = Addresses.LostSoul,
                    ["Golden Cog"] = Addresses.GoldenCog,
                    ["Spell Page"] = Addresses.SpellPage,
                    ["Griffin Shield"] = Addresses.GriffinShield,
                    ["Unicorn Shield"] = Addresses.UnicornShield,
                    ["Beard"] = Addresses.Beard,
                    ["Library Key"] = Addresses.LibraryKey,
                    ["Club Membership Card"] = Addresses.ClubMembershipCard,
                    ["Elephant Key 1"] = Addresses.ElephantKey1,
                    ["Elephant Key 2"] = Addresses.ElephantKey2,
                    ["Time Machine Piece (Contact Room)"] = Addresses.TimeMachinePieceContactRoom,
                    ["Time Machine Piece (Earth Room)"] = Addresses.TimeMachinePieceEarthRoom,
                    ["Time Machine Piece (Space Room)"] = Addresses.TimeMachinePieceSpaceRoom,
                    ["King Mullocks Key"] = Addresses.KingMullocksKey,
                    ["Staff Of Anubis"] = Addresses.StaffOfAnubis,
                    ["Scroll Of Sekhmet"] = Addresses.ScrollOfSekhmet,
                    ["Tablet Of Horus"] = Addresses.TabletOfHorus,
                    ["Pocket Watch"] = Addresses.PocketWatch,
                    ["Town House Key"] = Addresses.TownHouseKey,
                    ["Time Stone"] = Addresses.TimeStone,
                    ["Antidote"] = Addresses.Antidote,
                    ["Pond Room Valve"] = Addresses.PondRoomValve,
                    ["Hot House Valve"] = Addresses.HothouseValve,
                    ["Water Tank Valve"] = Addresses.WaterTankValve,
                    ["Cannon Ball"] = Addresses.CannonBall,
                    ["Front Door Key"] = Addresses.FrontDoorKey,
                    ["Potting Shed Key"] = Addresses.PottingShedKey,
                    ["The Depot Key"] = Addresses.TheDepotKey,
                    ["Museum Key"] = Addresses.MuseumKey,
                    ["Dinosaur Key"] = Addresses.DinosaurKey
                },
                ["Level Status"] = new Dictionary<string, uint>
                {

                    ["The Museum"] = Addresses.TM_LevelStatus,
                    ["Tyrannosaurus Wrecks"] = Addresses.TW_LevelStatus,
                    ["Kensington"] = Addresses.KT_LevelStatus,
                    ["The Tomb"] = Addresses.TT_LevelStatus,
                    ["The Freakshow"] = Addresses.TF_LevelStatus,
                    ["Greenwich Observatory"] = Addresses.GO_LevelStatus,
                    ["Greenwich Observatory, Naval Academy"] = Addresses.GONA_LevelStatus,
                    ["Kew Gardens"] = Addresses.KG_LevelStatus,
                    ["Dankenstein"] = Addresses.DK_LevelStatus,
                    ["Iron Slugger"] = Addresses.IS_LevelStatus,
                    ["Wulfrum Hall"] = Addresses.WH_LevelStatus,
                    ["The Count"] = Addresses.TC_LevelStatus,
                    ["Whitechapel"] = Addresses.WC_LevelStatus,
                    ["The Sewers"] = Addresses.TS_LevelStatus,
                    ["The Time Machine"] = Addresses.TTM_LevelStatus,
                    ["The Time Machine, The Sewers"] = Addresses.TTMTS_LevelStatus,
                    ["The Ripper"] = Addresses.TR_LevelStatus,
                    ["Cathedral Spires"] = Addresses.CS_LevelStatus,
                    ["Cathedral Spires, The Descent"] = Addresses.CSTD_LevelStatus,
                    ["The Demon"] = Addresses.TD_LevelStatus,
                },
            };
        }



        public static int SetItemMemoryValue(uint itemMemoryAddress, int itemUpdateValue, int maxCount)
        {
            int addition = Math.Min(itemUpdateValue, maxCount);

            byte[] byteArray = BitConverter.GetBytes(addition);

            Memory.WriteByteArray(itemMemoryAddress, byteArray);

            // Add more types as needed
            return itemUpdateValue;
        }

        public static void UpdateKeyItemValues(uint itemMemoryAddress)
        {
            SetItemMemoryValue(itemMemoryAddress, 1, 1);
        }

        public static void UpdateEquippedItemValues(uint itemMemoryAddress)
        {
            var currentWeaponValue = Memory.ReadShort(itemMemoryAddress);

            if (currentWeaponValue == -1)
            {
                SetItemMemoryValue(itemMemoryAddress, 1, 1);
            }
        }

        public static void UpdateLifeBottleValues()
        {
            var currentNumberAmount = Memory.ReadShort(Addresses.DansCurrentLifeBottles);
            SetItemMemoryValue(Addresses.DansCurrentLifeBottles, (currentNumberAmount + 300), maxLifeBottleValue);
        }

        public static void UpdateAmmoCount(string itemName, uint itemMemoryAddress, int updateValue, bool breakLimit)
        {
            var currentNumberAmount = Memory.ReadShort(itemMemoryAddress);
            var maxCountLimit = countMax;
            var limitDict = AmmoAndChargeLimits();

            if(!breakLimit)
            {
                maxCountLimit = limitDict[itemName];
            }

            var newUpdateValue = currentNumberAmount + updateValue;

            SetItemMemoryValue(itemMemoryAddress, newUpdateValue, maxCountLimit);
        }

        public static void UpdateChargeCount(string itemName, uint itemMemoryAddress, int updateValue, bool breakLimit)
        {
            var currentNumberAmount = Memory.ReadShort(itemMemoryAddress);
            var maxCountLimit = percentageMax;
            var dict = AmmoAndChargeLimits();

            if (!breakLimit)
            {
                maxCountLimit = dict[itemName];
            }

            var newUpdateValue = currentNumberAmount + updateValue;

            SetItemMemoryValue(itemMemoryAddress, newUpdateValue, maxCountLimit);
        }

        public static void UpdateGoldCount(int updateValue)
        {
            var currentGold = Memory.ReadShort(Addresses.DansCurrentGold);

            var newUpdateValue = currentGold + updateValue;

            SetItemMemoryValue(Addresses.DansCurrentGold, newUpdateValue, countMax);

        }

        public static void UpdateHealthCount(int updateValue)
        {
            var currentEnergy = Memory.ReadShort(Addresses.DansCurrentEnergy);
            var currentStoredEnergy = Memory.ReadShort(Addresses.DansCurrentStoredEnergy);
            var currentLifeBottles = Memory.ReadShort(Addresses.DansCurrentLifeBottles);

            bool useCurrentHealth = currentEnergy + updateValue <= 300;

            Console.WriteLine($"{currentEnergy}, {updateValue}, {currentStoredEnergy}, {currentLifeBottles}, {useCurrentHealth}");

            SetItemMemoryValue(useCurrentHealth ? Addresses.DansCurrentEnergy : Addresses.DansCurrentStoredEnergy, currentEnergy + updateValue, useCurrentHealth ? 300: currentLifeBottles * 300);

        }


        public static int ExtractBracketAmount(string itemName)
        {
            var match = Regex.Match(itemName, @"\((\d+)\)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int bracketAmount))
            {
                return bracketAmount;
            }
            return 0;
        }

        public static string ExtractDictName(string itemName)
        {
            // Use a more specific regex to handle the colon and parentheses in the new format.
            var colonParenthesesMatch = Regex.Match(itemName, @"^[^:]*:\s*(.*?)(?:\s*\(\d+\))?$");
            if (colonParenthesesMatch.Success)
            {
                return colonParenthesesMatch.Groups[1].Value.Trim();
            }

            // This is the fallback for strings with parentheses but no colon.
            var parenthesesMatch = Regex.Match(itemName, @"^(.*?)\s*\(");
            if (parenthesesMatch.Success)
            {
                return parenthesesMatch.Groups[1].Value.Trim();
            }

            // Fallback for simple strings or those that don't match other patterns.
            return itemName.Trim();
        }


        public static void ReceiveCountType(Item item, bool breakAmmoLimit)
        {
            var addressDict = StatusAndInventoryAddressDictionary();
            var amount = ExtractBracketAmount(item.Name);
            var name = ExtractDictName(item.Name);

            UpdateAmmoCount(name, addressDict["Ammo"][name], amount, breakAmmoLimit);
        }

        public static void ReceiveChargeType(Item item, bool breakChargeLimit)
        {
            var addressDict = StatusAndInventoryAddressDictionary();
            var amount = ExtractBracketAmount(item.Name);
            var name = ExtractDictName(item.Name);

            UpdateChargeCount(name, addressDict["Ammo"][name],  amount, breakChargeLimit);
        }

        public static void ReceiveEquipment(Item item)
        {
            var addressDict = StatusAndInventoryAddressDictionary();

            UpdateEquippedItemValues(addressDict["Equipment"][item.Name]);

        }


        public static void ReceiveKeyItem(Item item)
        {
            // commented out because i need to make a list of player data addresses to deal with this.
            var addressDict = StatusAndInventoryAddressDictionary();

            UpdateKeyItemValues(addressDict["Key Items"][item.Name]);

        }

        public static void ReceiveLifeBottle()
        {
            UpdateLifeBottleValues();
        }

        public static void ReceiveEnergy(Item item)
        {
            var amount = ExtractBracketAmount(item.Name);
            UpdateHealthCount(amount);
        }

        public static void ReceiveGold(Item item)
        {
            var amount = ExtractBracketAmount(item.Name);
            UpdateGoldCount(amount);
        }

        public static void ReceiveSkill(Item item)
        {
            // setting it here till i fix my ridiculous update function
            SetItemMemoryValue(Addresses.DansHandSkill, 1, 1);
        }

        public static void EquipWeapon(int value)
        {
            SetItemMemoryValue(Addresses.DansEquippedPrimaryWeapon, value, value);
        }

        public static void DefaultToArm()
        {
            SetItemMemoryValue(Addresses.DansCurrentEquipmentSlot, 0, 0);
            SetItemMemoryValue(Addresses.DansEquippedSecondaryWeapon, 12, 12);
            SetItemMemoryValue(Addresses.DansEquippedPrimaryWeapon, 12, 12);
        }
    }
}
