using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using KiraBot.Services;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace KiraBot
{
    public class Program
    {
		private Logger _log;

		public static uint OkColor { get; } = 0x00ff00;
		public static uint ErrorColor { get; } = 0xff0000;

		static void Main(string[] args)
            => new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private IConfiguration _config;

		public async Task Start()
        {
			_log = LogManager.GetCurrentClassLogger();

			_log.Info("Starting KiraBot!");
            _client = new DiscordSocketClient();
            _config = BuildConfig();

            var services = ConfigureServices();
            services.GetRequiredService<LogService>();
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync(services);

            await _client.LoginAsync(TokenType.Bot, _config["token"]);
            await _client.StartAsync();

			_log.Info("Initialization Completed.");

            await Task.Delay(-1);
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

        private IConfiguration BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();
        }

		public async Task ModifyStatus()
		{
			await _client.SetStatusAsync(UserStatus.DoNotDisturb);
			await _client.SetGameAsync("with Wumpus!", "", StreamType.Twitch);
		}
	}
}