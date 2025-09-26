using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archipelago.Core.Models;
using Archipelago.Core.Util;
using Archipelago.Core;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Kokuban;
using System.Threading;

namespace MedievilArchipelago.Helpers
{
    internal class PlayerStateHandler
    {
        internal static DateTime lastDeathTime = default(DateTime);
        internal static Task _deathlinkMonitorTask = null;
        internal static bool gameCleared = false;
        internal static bool playerStateUpdating = false;

        public static bool isInTheGame()
        {
            ulong currentGold = Memory.ReadUInt(Addresses.DansCurrentGold);
            ulong currentLevel = Memory.ReadByte(Addresses.CurrentLevel);



            if ((currentLevel <= 0x02 && currentLevel >= 0x1e && currentLevel == 0x13) || currentGold == 0x82a4)
            {
                return false;
            }
            return true;

        }

        //    public static void KillPlayer()
        //    {
        //        Memory.WriteByte(Addresses.GameGlobalScene, 0x06);
        //    }

        //    public static void StartDeathlinkMonitor(DeathLinkService deathLink, ArchipelagoClient client)
        //    {

        //        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        //        CancellationToken cancellationToken = _cancellationTokenSource.Token;

        //        _deathlinkMonitorTask = Task.Run(async () =>
        //        {
        //            // This is a continuous loop that runs until the task is canceled.
        //            while (!cancellationToken.IsCancellationRequested)
        //            {
        //                // Read the memory address.
        //                int currentValue = Memory.ReadUShort(Addresses.CurrentEnergy);

        //                // Check your condition.
        //                if (currentValue == 0)
        //                {
        //                    // Execute the action.
        //                    IsTrulyDead(deathLink, client);
        //                }

        //                // Await a short delay to prevent high CPU usage.
        //                await Task.Delay(100, cancellationToken);
        //            }
        //        }, cancellationToken);
        //    }

        //    public static void IsTrulyDead(DeathLinkService deathlink, ArchipelagoClient client)
        //    {
        //        var rnd = new Random();

        //        List<string> deathResponse = new List<string>
        //        {
        //            "Everyone disliked that.",
        //            "We're in danger!",
        //            "You hate to see it.",
        //            "Press F to pay respects.",
        //            "This is fine.",
        //            "Dan's dissapointment: Immeasurable.",
        //            "We're going down swimming.",
        //            "Lock, Stock and... we're all dead."
        //        };

        //        int listChoice = rnd.Next(deathResponse.Count);


        //        if (DateTime.Now - lastDeathTime >= TimeSpan.FromSeconds(30))
        //        {
        //            ushort bottleEnergy = Memory.ReadUShort(Addresses.CurrentStoredEnergy);
        //            ushort currentLevel = Memory.ReadByte(Addresses.CurrentLevel);

        //            Kokuban.AnsiEscape.AnsiStyle bg = Chalk.BgCyan;
        //            Kokuban.AnsiEscape.AnsiStyle fg = Chalk.Black;

        //            if (bottleEnergy == 0 && currentLevel != 0 && isInTheGame())
        //            {
        //                Console.WriteLine(bg + (fg + "[   ☠️💀 Deathlink Sent. " + deathResponse[listChoice] + " 💀☠️   ]"));
        //                deathlink.SendDeathLink(new DeathLink(client.CurrentSession.Players.ActivePlayer.Name));
        //                lastDeathTime = DateTime.Now;
        //            }
        //        }
        //    }

        public static void UpdatePlayerState(ArchipelagoClient client, bool gameCleared)
        {
            if (playerStateUpdating == true) { return; }

            playerStateUpdating = true;

            int keyItemSanityOption = Int32.Parse(client.Options?.GetValueOrDefault("keyitemsanity", "0").ToString());

            // get a list of all locatoins
            Dictionary<string, uint> all_items = ItemHandlers.FlattenedInventoryStrings();


            System.Collections.ObjectModel.ReadOnlyCollection<ItemInfo> itemsCollected = client.CurrentSession.Items.AllItemsReceived;
            // get a list of used locations
            var usedItems = new List<string>();

            short currentPrimaryWeapon = Memory.ReadShort(Addresses.DansEquippedPrimaryWeapon);
            short currentSecondaryWeapon = Memory.ReadShort(Addresses.DansEquippedSecondaryWeapon);
            byte currentLevel = Memory.ReadByte(Addresses.CurrentLevel);

            ItemHandlers.SetItemMemoryValue(Addresses.DansCurrentLifeBottles, 0, 0);

            if (keyItemSanityOption == 1)
            {
                ItemHandlers.SetItemMemoryValue(Addresses.GoldenCog, 0, 0);
                ItemHandlers.SetItemMemoryValue(Addresses.LostSoul, 0, 0);
            }

            // for each location that's coming in
            bool hasEquipableWeapon = false;

            Console.WriteLine("Updating player state...");

            foreach (ItemInfo itemInf in itemsCollected)
            {
                Item itm = new Item();
                itm.Name = itemInf.ItemName;

                switch (itm)
                {
                    // Update memory
                    case var x when x.Name.ContainsAny("Ammo"):
                        // no plans yet
                        break;
                    case var x when x.Name.ContainsAny("Charge"):
                        // no plans yet
                        break;
                    case var x when x.Name.Contains("Skill"): ItemHandlers.ReceiveSkill(x); break;
                    case var x when x.Name.ContainsAny(ItemHandlers.ListOfWeaponStrings):
                        ItemHandlers.ReceiveEquipment(x);
                        if (!x.Name.Contains("Shield"))
                        {
                            hasEquipableWeapon = true;
                        }
                        break;
                    case var x when x.Name.Contains("Life Bottle"): ItemHandlers.ReceiveLifeBottle(); break;
                    // these two will need to be adjusted, functions don't exist yet.
                    //case var x when x.Name.Contains("Golden Cog"): ItemHandlers.ReceiveDragonGem(); break;
                    //case var x when x.Name.Contains("Lost Soul"): ItemHandlers.ReceiveAmber(); break;
                    case var x when x.Name.Contains("Torch") && keyItemSanityOption == 0: ItemHandlers.ReceiveKeyItem(x); break;
                    case var x when x.Name.ContainsAny(ItemHandlers.ListOfKeyItemStrings) && keyItemSanityOption == 1: ItemHandlers.ReceiveKeyItem(x); break;
                }
                usedItems.Add(itm.Name);
            }

            Dictionary<string, uint> remainingItemsDict = all_items
                .Where(kvp => !usedItems.Contains(kvp.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (KeyValuePair<string, uint> item in remainingItemsDict)
            {

                string itemName = item.Key;
                uint itemAddress = item.Value;

                if (itemName.ContainsAny("Lost Soul", "Golden Cog") && keyItemSanityOption == 1)
                {
                    continue;
                }

                // reset any other values
                if (itemName.ContainsAny("Skill"))
                {
                    ItemHandlers.SetItemMemoryValue(itemAddress, 0, 0);
                    continue;
                }
                else if (itemName.ContainsAny(ItemHandlers.ListOfWeaponStrings) && !itemName.Contains("Ammo"))
                {
                    ItemHandlers.SetItemMemoryValue(itemAddress, 65535, 65535); // Assuming 65535 is "reset/max" for equipment
                    continue;
                }
                else if (itemName.ContainsAny("Complete"))
                {
                    ItemHandlers.SetItemMemoryValue(itemAddress, 0, 0);
                    continue;

                }
                else if (itemName.ContainsAny(ItemHandlers.ListOfKeyItemStrings) && keyItemSanityOption == 1)
                {
                    ItemHandlers.SetItemMemoryValue(itemAddress, 65535, 65535);
                    continue;

                }

            }

            if (!hasEquipableWeapon)
            {
                ItemHandlers.DefaultToArm();
            }
            else
            {
                ItemHandlers.EquipWeapon(currentPrimaryWeapon); 
                ItemHandlers.EquipWeapon(currentSecondaryWeapon);
            }


            if (GoalConditionHandlers.CheckGoalCondition(client) && gameCleared == false)
            {
                gameCleared = true;
                Console.WriteLine("No need for player state update. You've cleared!");
                return;
            };

            playerStateUpdating = false;
        }
    }
}
