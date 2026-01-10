using Archipelago.Core;
using Archipelago.Core.Util;
using Serilog;
using MedievilArchipelago.Helpers;
using Medievil2Archipelago.Models;
namespace MedievilArchipelago
{
    public class MemoryCheckThreads
    {
        static bool withinValidLevel = false;
        async public static Task PassiveLogicChecks(ArchipelagoClient client, CancellationTokenSource cts)
        {
            await Task.Run(() =>
            {

                Console.WriteLine("Starting Background Tasks...");

                void SetupLabStateMonitor()
                {
                    // Don't re-setup if the app is closing
                    if (cts.Token.IsCancellationRequested) return;

                    Memory.MonitorAddressForAction<byte>(
                        Addresses.CurrentLevel,
                        () =>
                        {

                            Memory.Write(Addresses.LabState, 0x0000000c);
#if DEBUG
                            Console.WriteLine("----Lab State Set");
#endif
                        },
                        value => value == 13);
                }

                void SetupNavalStateMonitor() {
                    if (cts.Token.IsCancellationRequested) return;

                    Memory.MonitorAddressForAction<byte>(
                        Addresses.CurrentLevel,
                        () =>
                        {
                            Memory.Write(Addresses.LabState, 0x00000010);
#if DEBUG
                            Console.WriteLine("----Naval State Set");
#endif
                            SetupNavalStateMonitor();
                        },
                        value => value == 27);
                }

                void SetupOpenWorldMonitor()
                {
                    if (cts.Token.IsCancellationRequested) return;

                    Memory.MonitorAddressForAction<byte>(
                        Addresses.CurrentLevel,
                        () =>
                        {
                            Memory.WriteByte(Addresses.CurrentLevel, 0x0d);
#if DEBUG
                            Console.WriteLine("----Open World State Set");
#endif
                        },
                        value => value == 10);
                }

                void SetupExitLevelMonitor()
                {
                    Memory.MonitorAddressForAction<byte>(
                        Addresses.CurrentLevel,
                        () =>
                        {
                            if (withinValidLevel == false)
                            {
#if DEBUG
                                Console.WriteLine("----Exit World State Set");
#endif
                                Memory.WriteByte(Addresses.ExitLevel, 0x00);
                                withinValidLevel = true;
                            }
                            SetupExitLevelMonitor();
                        },
                        value => value == 10 || value == 11);
                }

                byte currentLocation = Memory.ReadByte(Addresses.CurrentLevel);
                int openWorld = Int32.Parse(client.Options?.GetValueOrDefault("progression_option", "0").ToString());
                int keyitems = Int32.Parse(client.Options?.GetValueOrDefault("keyitemsanity", "0").ToString());

                // set to listen to "new game" so it'll load straight into the professors lab.
                if (openWorld == ProgressionOptions.OPENWORLD)
                {

                    SetupOpenWorldMonitor();
                    SetupLabStateMonitor();
                    SetupNavalStateMonitor();
                    //SetupExitLevelMonitor();
                }


                while (!cts.Token.IsCancellationRequested)
                {
                    try
                    {

                        byte currentLevel = Memory.ReadByte(Addresses.CurrentLevel);

                        if(openWorld == ProgressionOptions.OPENWORLD && currentLevel == 0x13)
                        {
                            SetupOpenWorldMonitor();
                        }

                        if (openWorld == ProgressionOptions.OPENWORLD && currentLocation != 0x13 && PlayerStateHandler.isInTheGame())
                        {
                            ThreadHandlers.SetOpenWorld();

                        }

                        if (currentLevel == 0x0a || currentLevel == 0x0b && openWorld == ProgressionOptions.OPENWORLD)
                        {
                            Memory.WriteByte(Addresses.ExitLevel, 0x00);
                        }

                        if (currentLocation != currentLevel && openWorld == ProgressionOptions.OPENWORLD)
                        {
                            SetupLabStateMonitor();
                        }

                        if (currentLocation != currentLevel && PlayerStateHandler.isInTheGame())
                        {
                            Thread.Sleep(8000);
                            PlayerStateHandler.UpdatePlayerState(client, false);
                        }

                        currentLocation = currentLevel;


                        if (currentLocation != 0x13 && PlayerStateHandler.isInTheGame())
                        {
                            ThreadHandlers.SetCheatMenu(client);
                            ThreadHandlers.SetChestContents(currentLocation, keyitems);
                        }

                        GoalConditionHandlers.CheckGoalCondition(client);

                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error in PassiveLogicChecks: {ex.Message}");
                    }
                    Thread.Sleep(3000);
                }
                }, cts.Token);

        }
    }
}
