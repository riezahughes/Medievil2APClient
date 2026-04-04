using Archipelago.Core;
using Medievil2Archipelago.Models;

namespace MedievilArchipelago.Helpers
{
    internal class GoalConditionHandlers
    {
        private static bool CheckDemonCondition(ArchipelagoClient client)
        {
            if (client?.CurrentSession.Locations.AllLocations == null) return false;

            var demonLocation = LocationHandlers.BuildLocationList(client.Options)
                .Where(loc => loc.Name == ("Cleared: The Demon")).ToList();

            var checkedIds = new HashSet<long>(client.CurrentSession.Locations.AllLocationsChecked);

            List<string> matchingNames = demonLocation
                .Where(loc => checkedIds.Contains((long)loc.Id))
                .Select(loc => loc.Name)
                .ToList();


            if (matchingNames.Count() == 1)
            {
                Console.WriteLine("You've Defeated The Demon!");
                return true;
            }
            return false;
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
