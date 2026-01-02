using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archipelago.Core.Util;
using Archipelago.Core;
using Archipelago.MultiClient.Net.Models;

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

        static internal void SetChestContents(byte currentLevel)
        {
            var chestLocations = ItemHandlers.ListOfChestLocations;

            foreach(var chest in chestLocations[currentLevel])
            {
                Memory.WriteByte(chest, 0x01);
            }

        }

    }
}
