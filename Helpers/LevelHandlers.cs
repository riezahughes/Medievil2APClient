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

        public static readonly Dictionary<uint, Dictionary<string, uint?>> LevelStatusMap = new()
        {
            // Cathedral Spires
            [0x02] = new() { ["Locked"] = 0x20, ["Unlocked"] = 0x21, ["CompleteNoChalice"] = 0x00, ["CompleteWithChalice"] = null, ["Address"] = Addresses.CS_LevelStatus },
            // The Demon
            [0x03] = new() { ["Locked"] = 0x20, ["Unlocked"] = 0x21, ["CompleteNoChalice"] = 0x29, ["CompleteWithChalice"] = null, ["Address"] = Addresses.TD_LevelStatus },
            // Iron Slugger                                                                       
            [0x04] = new() { ["Locked"] = 0x20, ["Unlocked"] = 0x21, ["CompleteNoChalice"] = 0x29, ["CompleteWithChalice"] = null, ["Address"] = Addresses.IS_LevelStatus },
            // Dankenstein                                                                 
            [0x05] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.DK_LevelStatus },
            // The Freakshow
            [0x06] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.TF_LevelStatus },
            // Greenwhish Observatory
            [0x07] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.GO_LevelStatus },
            // Kew Gardens
            [0x08] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.KG_LevelStatus },
            // Whitechapel
            [0x09] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.WC_LevelStatus },
            // The Museum
            [0x0a] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.TM_LevelStatus },
            // Tyrannosaurus Wrecks
            [0x0b] = new() { ["Locked"] = 0x20, ["Unlocked"] = 0x21, ["CompleteNoChalice"] = 0x29, ["CompleteWithChalice"] = null, ["Address"] = Addresses.TW_LevelStatus },
            // Lab (Hub)
            [0x0d] = new() { ["Locked"] = null, ["Unlocked"] = null, ["CompleteNoChalice"] = null, ["CompleteWithChalice"] = null, ["Address"] = null },
            // The Sewer
            [0x0e] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.TS_LevelStatus },
            // The Time Machine
            [0x0f] = new() { ["Locked"] = 0x20, ["Unlocked"] = 0x21, ["CompleteNoChalice"] = 0x29, ["CompleteWithChalice"] = null, ["Address"] = Addresses.TTM_LevelStatus },
            // The Count
            [0x10] = new() { ["Locked"] = 0x20, ["Unlocked"] = 0x21, ["CompleteNoChalice"] = 0x29, ["CompleteWithChalice"] = null, ["Address"] = Addresses.TC_LevelStatus },
            // Wulfum Hall
            [0x11] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.WH_LevelStatus },
            // Kensington
            [0x12] = new() { ["Locked"] = 0x00, ["Unlocked"] = 0x01, ["CompleteNoChalice"] = 0x09, ["CompleteWithChalice"] = 0x19, ["Address"] = Addresses.KT_LevelStatus },
            // Main Menu
            [0x13] = new() { ["Locked"] = 0x01, ["Unlocked"] = 0x00, ["CompleteNoChalice"] = 0x00, ["CompleteWithChalice"] = 0x00, ["Address"] = null },
            // Kensington, The Tomb
            [0x1a] = new() { ["Locked"] = 0x22, ["Unlocked"] = 0x23, ["CompleteNoChalice"] = 0x2B, ["CompleteWithChalice"] = null, ["Address"] = Addresses.TT_LevelStatus },
            // Greenwich Naval Academy
            [0x1b] = new() { ["Locked"] = 0x22, ["Unlocked"] = 0x23, ["CompleteNoChalice"] = 0x2B, ["CompleteWithChalice"] = null, ["Address"] = Addresses.GONA_LevelStatus },
            // Time Machine, Sewers
            [0x1c] = new() { ["Locked"] = 0x22, ["Unlocked"] = 0x23, ["CompleteNoChalice"] = 0x2B, ["CompleteWithChalice"] = null, ["Address"] = Addresses.TTMTS_LevelStatus },
            // Time Machine, The Ripper
            [0x1d] = new() { ["Locked"] = 0x02, ["Unlocked"] = 0x03, ["CompleteNoChalice"] = 0x0b, ["CompleteWithChalice"] = null, ["Address"] = Addresses.TR_LevelStatus },
            // Cathedral Spires, The Descent
            [0x1e] = new() { ["Locked"] = 0x22, ["Unlocked"] = 0x23, ["CompleteNoChalice"] = 0x2B, ["CompleteWithChalice"] = null, ["Address"] = Addresses.CSTD_LevelStatus },
        };

        public static Dictionary<string, uint?> GetLevelStatuses(byte currentLevel)
        {
            if (LevelStatusMap.TryGetValue(currentLevel, out var statuses))
            {
                return statuses;
            }

            // Return null or an empty dictionary if the level isn't found
            return null;
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

        // this is just for the locations. These don't really line up anywhere else but here.
        public static byte GetLevelIdFromName(string levelName)
        {
            var dict = new Dictionary<string, byte>
            {
                ["Cathedral Spires"] = 0x02,
                ["The Demon"] = 0x03,
                ["Iron Slugger"] = 0x04,
                ["Dankenstein"] = 0x05,
                ["The Freakshow"] = 0x06,
                ["Greenwich Observatory"] = 0x07,
                ["Kew Gardens"] = 0x08,
                ["Whitechapel"] = 0x09,
                ["The Museum"] = 0x0a,
                ["Tyrannosaurus Wrecks"] = 0x0b,
                ["Hub"] = 0x0d,
                ["The Sewers"] = 0x0e,
                ["The Time Machine"] = 0x0f,
                ["The Count"] = 0x10,
                ["Wulfrum Hall"] = 0x11,
                ["Kensington"] = 0x12,
                ["Main Menu"] = 0x13,
                ["The Tomb"] = 0x1a,
                ["Naval Academy"] = 0x1b,
                ["The Time Machine - The Sewers"] = 0x1c,
                ["The Ripper"] = 0x1d,
                ["Cathedral Spires - The Descent"] = 0x1e,
            };

            return dict[levelName]; 
        }
    }
}
