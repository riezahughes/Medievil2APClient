using Archipelago.Core;
using Archipelago.Core.Util;
using Medievil2Archipelago.Models;

namespace MedievilArchipelago.Helpers
{
    internal class ThreadHandlers
    {
        static internal void SetCheatMenu(ArchipelagoClient client)
        {
            if (client.Options == null)
            {
                return;
            }

            int cheatMenu = Int32.Parse(client.Options?.GetValueOrDefault("cheat_menu", "0").ToString());
            int cheatMenuValue = Memory.ReadByte(Addresses.CheatMenu);

            if (cheatMenuValue != 0x68)
            {
                return;
            }

            switch (cheatMenu)
            {
                case 0:
                    break;
                case 1:
                    Memory.WriteByte(Addresses.CheatMenu, 0x69);
                    break;
            }
            return;
        }

        static internal void SetChestContents(byte currentLevel, int keyItemsInPool)
        {
            var chestLocations = ItemHandlers.ListOfChestLocations;

            var offset = 0x18;

            foreach (var chest in chestLocations[currentLevel])
            {
                // these are the only two chests in the game that have a key item in them.
                if (keyItemsInPool == KeyItemSanityOptions.ON && chest == (Addresses.KT_Pickup_Pocketwatch - offset) || chest == (Addresses.TM_Pickup_Cannonball - offset))
                {
                    continue;
                }
                Memory.WriteByte(chest, 0x01);
            }

        }

        static internal void SetOpenWorld(ArchipelagoClient client)
        {
            // for every level, set its byte to unlocked. Only do this if it's set to 1.
            foreach (var (levelId, Statuses) in LevelHandlers.LevelStatusMap)
            {
                if (Statuses["Address"] is not null && levelId != 0x03)
                {
                    Memory.WriteByte((ulong)Statuses["Address"], (byte)Statuses["Unlocked"]);
                }

                if (levelId == 0x03)
                {
                    int currentSouls = ItemHandlers.CountCurrentSouls(client);
                    int currentCogs = ItemHandlers.CountCurrentCogs(client);
                    if (currentSouls == 12 && currentCogs == 2)
                    {
                        Memory.WriteByte((ulong)Statuses["Address"], (byte)Statuses["Unlocked"]);
                    }
                    else
                    {
                        Memory.WriteByte((ulong)Statuses["Address"], (byte)Statuses["Locked"]);
                    }
                }
            }
        }

    }
}
