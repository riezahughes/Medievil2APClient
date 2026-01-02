using Archipelago.Core;
using Archipelago.Core.Util;
using Serilog;
using MedievilArchipelago.Helpers;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Hosting;
using System.Threading;


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

                while (!cts.Token.IsCancellationRequested)
                {
                    try
                    {

                        byte currentLevel = Memory.ReadByte(Addresses.CurrentLevel);
                        //byte currentgold = Memory.ReadByte(Addresses.DansCurrentGold);

                        //Console.WriteLine($"Current Level: {currentLevel} and current gold is {currentgold}");


                        if (currentLocation != currentLevel && PlayerStateHandler.isInTheGame() && currentLevel != 0x13)
                        {
                            PlayerStateHandler.UpdatePlayerState(client, false);
                        }

                        currentLocation = currentLevel;

                        GoalConditionHandlers.CheckGoalCondition(client);

                        if (currentLocation != 0x13)
                        {
                            ThreadHandlers.SetCheatMenu(client);
                            ThreadHandlers.SetChestContents(currentLocation);
                        }

                        Thread.Sleep(5000);
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
