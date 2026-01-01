// See https://aka.ms/new-console-template for more information

using Archipelago.Core;
using Archipelago.Core.GameClients;
using Archipelago.Core.Models;
using Archipelago.Core.Traps;
using Archipelago.Core.Util;
using Archipelago.Core.Util.GPS;
using MedievilArchipelago;
using Helpers = MedievilArchipelago.Helpers;
using Archipelago.Core.Util.Overlay;
using Archipelago.MultiClient.Net.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using MedievilArchipelago.Helpers;

public class Program
{
    public static List<ItemReceivedEventArgs> delayedItems = new List<ItemReceivedEventArgs>();

    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "Medievil 2 Archipelago Client";

        // set values
        const byte US_OFFSET = 0x38; // this is ADDED to addresses to get their US location
        const byte JP_OFFSET = 0; // could add more offsfets here



        bool playerStateUpdating = false;

        // Connection details
        string url;
        string port;
        string slot = "";
        string password;

        bool firstRun = true;
        


        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        List<ILocation> GameLocations = null;

        ////////////////////////////
        //
        // Main Program Flow
        //
        ////////////////////////////

        // Make sure the connect is initialised


        DuckstationClient gameClient = null;
        bool clientInitializedAndConnected = false; // Renamed for clarity
        int retryAttempt = 0;

        while (!clientInitializedAndConnected)
        {
            Console.Clear();
            retryAttempt++;
            Console.WriteLine($"\nAttempt #{retryAttempt}:");

            try
            {
                gameClient = new DuckstationClient();
                clientInitializedAndConnected = true;
            }
            catch (Exception ex)
            {
                // Catch any exception thrown during the DuckstationClient constructor call
                // or any other unexpected error during the try block.
                Console.WriteLine($"Could not find Duckstation open.");

                // Wait for 5 seconds before the next retry
                Thread.Sleep(5000); // 5000 milliseconds = 5 seconds
            }
        }

        #if DEBUG
        #else
            Console.Clear();
        #endif

        bool connected = gameClient.Connect();

        var archipelagoClient = new ArchipelagoClient(gameClient);

        // Register event handlers
        archipelagoClient.Connected += (sender, args) => Helpers.APHandlers.OnConnected(sender, args, archipelagoClient);
        archipelagoClient.Disconnected += (sender, args) => Helpers.APHandlers.OnDisconnected(sender, args, archipelagoClient, firstRun);
        archipelagoClient.ItemReceived += (sender, args) => Helpers.APHandlers.ItemReceived(sender, args, archipelagoClient);
        archipelagoClient.MessageReceived += (sender, args) => Helpers.APHandlers.Client_MessageReceived(sender, args, archipelagoClient, slot);
        archipelagoClient.LocationCompleted += (sender, args) => Helpers.APHandlers.Client_LocationCompleted(sender, args, archipelagoClient);
        archipelagoClient.EnableLocationsCondition = () => Helpers.PlayerStateHandler.isInTheGame();

        Console.WriteLine("Successfully connected to Duckstation.");

        // get the duckstation offset
        try
        {
            Memory.GlobalOffset = Memory.GetDuckstationOffset();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred while getting Duckstation memory offset: {ex.Message}");
            Console.WriteLine(ex); // Print full exception for debugging
        }


        #if DEBUG
            // auto logs in with Local.json settings if it's set to dev (because laziness)
            var configuration = new ConfigurationBuilder()
                // Add the default appsettings.json file
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                .Build();

            Console.WriteLine("Logging in using settings in appsettings.Local.json");
            Console.WriteLine(configuration["port"]);
            Console.WriteLine(configuration["slot"]);
            Console.WriteLine(configuration["pass"]);
            url = "wss://archipelago.gg";
            port = configuration["port"];
            slot = configuration["slot"];
            password = configuration["pass"];

        #else
            // start AP Login

            Console.WriteLine("Enter AP Domain: (archipelago.gg)");
            string lineUrl = Console.ReadLine();

            url = string.IsNullOrWhiteSpace(lineUrl) ? "archipelago.gg" : lineUrl;

            Console.WriteLine("Enter Port: eg, 80001");
            port = Console.ReadLine();

            Console.WriteLine("Enter Slot Name:");
            slot = Console.ReadLine();

            Console.WriteLine("Room Password:");
            string linePassword = Console.ReadLine();
            password = string.IsNullOrWhiteSpace(linePassword) ? null : linePassword;

            Console.WriteLine("Details:");
            Console.WriteLine($"URL:{url}:{port}");
            Console.WriteLine($"Slot: {slot}");
            Console.WriteLine($"Password: {password}");

            if (string.IsNullOrWhiteSpace(slot))
            {
                Console.WriteLine("Slot name cannot be empty. Please provide a valid slot name.");
                return;
            }
        #endif


        Console.WriteLine("Got the details! Attempting to connect to Archipelagos main server");


        try
        {

            

            await archipelagoClient.Connect(url + ":" + port, "Medievil 2");

            Thread.Sleep(1000);

            await archipelagoClient?.Login(slot, password);

            
            int retryCount = 0;
            Console.WriteLine("Waiting for connection...");
            while (archipelagoClient.IsLoggedIn == false)
            {

                if(retryCount >= 10)
                {
                    throw new Exception("The Medievil Client was unable to log into Archipelago. Please make sure your room is running, that you are putting in the correct details and that you are online.");

                }
                await archipelagoClient?.Login(slot, password);
                retryCount++;
                Console.Write(".");
                Thread.Sleep(1000);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred while connecting to Archipelago: {ex.Message}");
            #if DEBUG
                Console.WriteLine(ex); // Print full exception for debugging
            #endif
            Console.ReadKey();
            Environment.Exit(1);

        }

        try { 




        var overlayOptions = new OverlayOptions();

            overlayOptions.XOffset = 50;
            overlayOptions.YOffset = 500;
            overlayOptions.FontSize = 12;
            overlayOptions.DefaultTextColor = Archipelago.Core.Util.Overlay.Color.Yellow;

            var gameOverlay = new WindowsOverlayService(overlayOptions);

            //var fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "MediEvilFont.ttf");
            //Console.WriteLine(fontPath)
            //gameOverlay.CreateFont(fontPath, 12);

            // overlay is turned off till version update of AP Client Library
            //archipelagoClient.IntializeOverlayService(gameOverlay);

            while (archipelagoClient.CurrentSession == null)
            {
                Console.WriteLine("Waiting for current session");
                Thread.Sleep(1000);
            }

            archipelagoClient.CurrentSession.Locations.CheckedLocationsUpdated += Helpers.APHandlers.Locations_CheckedLocationsUpdated;

            // listen for the memory shift happening

            GameLocations = LocationHandlers.BuildLocationList(archipelagoClient.Options);

            // Check for when ripper dies and shifts memory

            var task = Memory.MonitorAddressForAction<byte>(
                Addresses.TR_LevelStatus,
                () =>
                {
                    // Read as a single byte to avoid grabbing neighboring data
                    var val = Memory.Read<byte>(Addresses.TR_LevelStatus, Enums.Endianness.Big);
#if DEBUG
                    Console.WriteLine("!!!!!  MEMORY SHIFT IS HAPPENING !!!!!");
                    LocationHandlers.MemoryShiftAfterRipperClear(archipelagoClient, archipelagoClient.Options);
                    Console.WriteLine("!!!!!  MEMORY SHIFT DONE !!!!!");
#endif
                },
                value => value >= 9);


            // Set up GPS
            //archipelagoClient.GPSHandler = Helpers.APHandlers.Client_GPSHandler();
            //archipelagoClient.GPSHandler.SetInterval(100);
            //archipelagoClient.GPSHandler.PositionChanged += (sender, args) => Helpers.APHandlers.Client_GPSPositionChanged(archipelagoClient, GameLocations);
            //archipelagoClient.GPSHandler.Start();


#if DEBUG

            //foreach (var opt in archipelagoClient.Options)
            //{
            //    Console.WriteLine($"Option: {opt.Key} - {opt.Value}");
            //}

#else
            Console.Clear();
#endif

            //foreach (var location in GameLocations)
            //{
            //    Console.WriteLine($"ID: {location.Id} - {location.Name}");
            //}

            firstRun = false;

            _ = archipelagoClient.MonitorLocations(GameLocations);
            _ = MemoryCheckThreads.PassiveLogicChecks(archipelagoClient, _cancellationTokenSource);

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    var input = Console.ReadLine();
                    if (input?.Trim().ToLower() == "exit")
                    {
                        _cancellationTokenSource.Cancel();
                        break;
                    }
                    else if (input?.Trim().ToLower().Contains("hint") == true)
                    {

                        string hintString = input?.Trim().ToLower() == "hint" ? "!hint" : $"!hint {input.Substring(5).Trim()}";
                        archipelagoClient.SendMessage(hintString);
                    }
                    else if (input?.Trim().ToLower() == "update")
                    {
                        if (archipelagoClient.LocationState.CompletedLocations != null)
                        {
                            Helpers.PlayerStateHandler.UpdatePlayerState(archipelagoClient, false);
                            Console.WriteLine($"Player state updated. Total Count: {archipelagoClient.CurrentSession.Items.AllItemsReceived.Count}");

#if DEBUG
                            foreach (ItemInfo item in archipelagoClient.CurrentSession.Items.AllItemsReceived)
                            {
                                Console.WriteLine($"id: {item.ItemId} - {item.ItemName}");
                            }
#endif
                        }
                        else
                        {
                            Console.WriteLine("Cannot update player state: GameState or CompletedLocations is null.");
                        }
                    }
                    else if (input?.Trim().ToLower() == "items")
                    {
                        var items = from item in archipelagoClient.CurrentSession.Items.AllItemsReceived where item.ItemName.ContainsAny(ItemHandlers.ListOfKeyItemStrings) select item;

                        var bottles = from item in archipelagoClient.CurrentSession.Items.AllItemsReceived where item.ItemName.Contains("Bottle") select item;

                        var equipment = from item in archipelagoClient.CurrentSession.Items.AllItemsReceived where item.ItemName.Contains("Equipment") select item;

                        Console.WriteLine("Current Equipment: ");
                        foreach (var weapon in equipment.OrderBy(item => item.ItemName))
                        {
                            Console.WriteLine(weapon.ItemName);
                        }

                        Console.WriteLine("Current Key Items: ");
                        foreach (var item in items.OrderBy(item => item.ItemName))
                        {
                            Console.WriteLine(item.ItemName);
                        }

                        Console.WriteLine("Current Bottles: ");
                        foreach (var bottle in bottles.OrderBy(item => item.ItemName))
                        {
                            Console.WriteLine(bottle.ItemName);
                        }


                    }
                    // allow manually handling traps if you're in dev mode (for testing)
#if DEBUG
                        else if (input?.Trim().ToLower() == "heavytrap")
                        {
                            Helpers.TrapHandlers.HeavyDanTrap();
                        }
                        else if (input?.Trim().ToLower() == "lighttrap")
                        {
                            Helpers.TrapHandlers.LightDanTrap();
                        }
                        //else if (input?.Trim().ToLower() == "darknesstrap")
                        //{
                        //    Helpers.TrapHandlers.DarknessTrap(0x01);
                        //}
                        //else if (input?.Trim().ToLower() == "hudtrap")
                        //{
                        //    Helpers.TrapHandlers.HudlessTrap();
                        //}
#endif
                    else if (!string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine($"Unknown command: '{input}'");
                    }
                }
                catch (Exception ex)
                {
                    _cancellationTokenSource.Cancel();
                    Console.WriteLine("The system has crashed. (Probably broken connection");
                }
            }

            Console.WriteLine("Shutting down due to connection issues.");
        }
        catch (Exception ex)
        {
            _cancellationTokenSource.Cancel();
            Console.WriteLine($"An unexpected error occurred while connecting to Archipelago: {ex.Message}");
            Console.WriteLine(ex); // Print full exception for debugging
        }
        finally
        {
            // Perform any necessary cleanup here
            Console.WriteLine("Shutting down...");
        }
    }
}