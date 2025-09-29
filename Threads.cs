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
            }, cts.Token);

        }
    }
}
