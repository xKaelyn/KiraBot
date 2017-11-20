//System Requirements
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System.Collections.Concurrent;
using System.Linq;
using System.Collections.Immutable;
//Discord Requirements
using Discord;
using Discord.Commands;
using Discord.WebSocket;
//KiraBot Requirements
using KiraBot;
using KiraBot.Services;

//This file is mandatory for KiraBot.
//This file is the shard code initialization.
//Without this file, this bot would not be operational with just a few files filled with C# code doing absolute fuck all.
//Do not remove.

namespace KiraBot
{
    public class Program
    {
	    	//Initializing the logger for the entire program.
		private Logger _log;

		public static uint OkColor { get; } = 0x00ff00;
		public static uint ErrorColor { get; } = 0xff0000;

		public static CommandService CommandService { get; private set; }
		public static DiscordShardedClient Client { get; private set; }
		//Initializing the bot sequence.
		static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private IConfiguration _config;
		//Initializing bot tasks within Shard #1.
		public async Task Start()
        {
			_log = LogManager.GetCurrentClassLogger();
		//Logging the startup.
			_log.Info("Starting KiraBot!");
            _client = new DiscordSocketClient();
            _config = BuildConfig();

            var services = ConfigureServices();
            services.GetRequiredService<LogService>();
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync(services);

            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            await _client.StartAsync();
			//Initialization completed.
			_log.Info("Initialization Completed.");

            await Task.Delay(-1);
		}
		//Setting the Bot's Shard to display this as the game within the Discord client.
		public async Task SetGame()
		{
			await _client.SetGameAsync("with Wumpus!");
			await _client.SetStatusAsync(UserStatus.DoNotDisturb);
		}


		private IServiceProvider ConfigureServices()
        {
			return new ServiceCollection()
                // Base
                .AddSingleton(_client)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                // Logging
                .AddLogging()
                .AddSingleton<LogService>()
                // Extra
                .AddSingleton(_config)
                // Add additional services here...
                .BuildServiceProvider();
        }
	//Grabbing the configuration json file and embedding it within the project.
        private IConfiguration BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();
        }
	}
}

//Basic initialization completed.
