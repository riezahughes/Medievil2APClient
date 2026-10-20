using Archipelago.Core;
using Archipelago.Core.Util;
using Medievil2Archipelago.Models;

namespace MedievilArchipelago.Helpers
{
    internal class GoalConditionHandlers
    {
        private static bool CheckDemonCondition(ArchipelagoClient client)
        {
            // "Cleared: The Demon" is a goal event in the APWorld (no location id), so it can never appear in
            // the server's checked locations. Detect it from memory: in The Demon's level (3) with the
            // ending cutscene status (41) set.
            byte currentLevel = Memory.ReadByte(Addresses.CurrentLevel);
            if (currentLevel != 3) return false;

            byte levelStatus = Memory.ReadByte(Addresses.TD_LevelStatus);
            if (levelStatus != 41) return false;

            Console.WriteLine("You've Defeated The Demon!");
            return true;
        }

        //private static bool CheckChaliceCondition(ArchipelagoClient client)
        //{
        //    int antOption = Int32.Parse(client.Options?.GetValueOrDefault("include_ant_hill_in_checks", "0").ToString());
        //    int maxChaliceCount = antOption == 1 ? 20 : 19;
        //    int currentCount = 0;
        //    if (client?.GameState == null || client.CurrentSession == null) return false;

        //    foreach (CompositeLocation loc in client.GameState.CompletedLocations.Distinct())
        //    {
        //        if (loc.Name.Contains("Chalice: "))
        //        {
        //            currentCount++;
        //        }
        //    }

        //    if (currentCount == maxChaliceCount)
        //    {
        //        client.SendGoalCompletion();
        //        Console.WriteLine("You got all the chalices!");
        //        return true;
        //    }
        //    return false;
        //}



        public static bool CheckGoalCondition(ArchipelagoClient client)
        {

            if (client?.CurrentSession.Locations.AllLocations == null)
            {
                return false;
            }

            if (client?.Options == null) { return false; }

            int goalCondition = Int32.Parse(client.Options?.GetValueOrDefault("goal", "0").ToString());

            if (goalCondition == PlayerGoals.DEFEAT_DEMON)
            {
                bool goal = CheckDemonCondition(client);

                if (goal)
                {
                    client.SendGoalCompletion();
                    Console.WriteLine("Goaled!!");
                    return true;
                }
            }

            return false;
        }
    }
}
