using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archipelago.Core.Models;
using Archipelago.Core;
using Microsoft.Extensions.Options;
using Archipelago.Core.Util;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MedievilArchipelago.Helpers
{
    internal class LevelHandlers
    {
        public static void CheckPositionalLocations(ArchipelagoClient client, List<ILocation> builtLocations)
        {

            //

            // A large pile of custom pieces are here. These are mostly things that are in dynamic memory/are hard to work out. 
            // By doing it this way, it allows us to create a plane and custom logic from it, which, i'm really fucking greatful for.
            // Bit thanks to Arson for the GPS btw. Holy shit. What a godsend.

            //
            if (builtLocations?.Count == null)
            {
                return;
            }

            int gargoyleSanity = int.Parse(client.Options?.GetValueOrDefault("gargoylesanity", "0").ToString());

            //// starting gargoyles
            //if (client.GPSHandler.MapId == 6 && client.GPSHandler.X == 64189 && client.GPSHandler.Y == 0 && client.GPSHandler.Z == 16 && gargoyleSanity == 1)
            //{
            //    var location1 = builtLocations.FirstOrDefault(loc => loc.Name == "Gargoyle: Left - DC");
            //    var location2 = builtLocations.FirstOrDefault(loc => loc.Name == "Gargoyle: Right - DC");
            //    if (location1 != null && location2 != null)
            //    {
            //        {
            //            client.SendLocation(location1);
            //            client.SendLocation(location2);
            //        }
            //    }
            //}

        }
        public static string GetLevelNameFromId(byte levelId)
        {
            var dict = new Dictionary<byte, string>
            {
                [0x02] = "Cathedral Spires",
                [0x03] = "The Demon",
                [0x04] = "Iron Slugger",
                [0x05] = "Dankenstein",
                [0x06] = "The Freakshow",
                [0x07] = "Greenwich Observatory",
                [0x08] = "Kew Gardens",
                [0x09] = "Whitechapel",
                [0x0a] = "The Museum",
                [0x0b] = "Tyrannosaurus Wrecks",
                [0x0d] = "Hub",
                [0x0e] = "The Sewers",
                [0x0f] = "The Time Machine",
                [0x10] = "The Count",
                [0x11] = "Wulfrum Hall",
                [0x12] = "Kensington",
                [0x13] = "Main Menu",
                [0x1a] = "Kensington, The Tomb",
                [0x1b] = "Greenwich Naval Academy",
                [0x1c] = "Time Machine, Sewers",
                [0x1d] = "Time Machine, The Ripper",
                [0x1e] = "Cathedral Spires, the Descent",
            };

            return dict[levelId];

        }
    }
}
