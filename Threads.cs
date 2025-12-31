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

                while (!cts.Token.IsCancellationRequested)
                {
                    //byte currentLevel = Memory.ReadByte(Addresses.CurrentLevel);
                    //byte currentgold = Memory.ReadByte(Addresses.DansCurrentGold);

                    //Console.WriteLine($"Current Level: {currentLevel} and current gold is {currentgold}");
                    try
                    {
                        ThreadHandlers.SetCheatMenu(client);
                        Thread.Sleep(500);
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
