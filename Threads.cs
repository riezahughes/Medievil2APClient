using Archipelago.Core;
using Archipelago.Core.Util;
using Serilog;
using MedievilArchipelago.Helpers;
using MedievilArchipelago.Models;
using Medievil2Archipelago.Models;
namespace MedievilArchipelago
{
    public class MemoryCheckThreads
    {

        async public static Task PassiveLogicChecks(ArchipelagoClient client, CancellationTokenSource cts)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Starting Background Tasks...");

                byte currentLocation = Memory.ReadByte(Addresses.CurrentLevel);
                int openWorld = Int32.Parse(client.Options?.GetValueOrDefault("progression_option", "0").ToString());
                int keyitems = Int32.Parse(client.Options?.GetValueOrDefault("keyitemsanity", "0").ToString());

                // set to listen to "new game" so it'll load straight into the professors lab.
                if (openWorld == ProgressionOptions.OPENWORLD)
                {
                    var task = Memory.MonitorAddressForAction<byte>(
                        Addresses.CurrentLevel,
                        () =>
                        {

                            var val = Memory.Read<byte>(Addresses.CurrentLevel, Enums.Endianness.Big);
                            Memory.WriteByte(Addresses.CurrentLevel, 0x0d); // 
                        },
                        value => value == 10);
                }


                while (!cts.Token.IsCancellationRequested)
                {
                    try
                    {

                        byte currentLevel = Memory.ReadByte(Addresses.CurrentLevel);

                            Thread.Sleep(3000);
                            if (currentLocation != currentLevel && PlayerStateHandler.isInTheGame() && currentLevel != 0x13)
                            {
                                PlayerStateHandler.UpdatePlayerState(client, false);
                            }

                            currentLocation = currentLevel;

                            GoalConditionHandlers.CheckGoalCondition(client);

                            if (currentLocation != 0x13)
                            {
                                ThreadHandlers.SetCheatMenu(client);
                                ThreadHandlers.SetChestContents(currentLocation, keyitems);
                            }

                            if (openWorld == ProgressionOptions.OPENWORLD && currentLocation != 0x13)
                            {
                                ThreadHandlers.SetOpenWorld();
                            }
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error in PassiveLogicChecks: {ex.Message}");
                    }
                    
                }
                }, cts.Token);

        }
    }
}
