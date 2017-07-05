// System Requirements
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq;
// Discord Requirements
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace KiraBot.Modules
{
	public class HelpModule : ModuleBase<SocketCommandContext>
	{
		private CommandService _service;

		// Create a constructor for the commandservice dependency
		public HelpModule(CommandService service)
		{
			_service = service;
		}

		[Command("help")]
		[Alias("listhelp", "commands")]
		[Summary("List all commands you are able to use with KiraBot!")]
		public async Task HelpAsync()
		{
			var application = await Context.Client.GetApplicationInfoAsync();
			var author = new EmbedAuthorBuilder()
				.WithName("KiraBot")
				.WithIconUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg");
			var footer = new EmbedFooterBuilder()
				.WithText("All commands have the ~ prefix.");
			var builder = new EmbedBuilder()
			{
				Author = author,
				Color = new Color(0, 255, 0),
				Description = "Here are the available commands!",
				Footer = footer
			};

			foreach (var module in _service.Modules)
			{
				string description = null;
				foreach (var cmd in module.Commands)
				{
					var result = await cmd.CheckPreconditionsAsync(Context);
					if (result.IsSuccess)
						description += $"{cmd.Aliases.First()}\n";
				}

				if (!string.IsNullOrWhiteSpace(description))
				{
					builder.AddField(x =>
					{
						x.Name = module.Name;
						x.Value = description;
						x.IsInline = false;
					});
				}
			}

			await ReplyAsync("", false, builder.Build());
		}

		[Command("chelp")]
		[Alias("commandhelp")]
		[Summary("Find out how to use a certain command | E.g: ~help info")]
		[Remarks("")]
		public async Task HelpAsync(string command)
		{
			var result = _service.Search(Context, command);

			if (!result.IsSuccess)
			{
				await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
				return;
			}

			var builder = new EmbedBuilder()
			{
				Color = new Color(114, 137, 218),
				Description = $"Here are some commands like **{command}**"
			};

			foreach (var match in result.Commands)
			{
				var cmd = match.Command;

				builder.AddField(x =>
				{
					x.Name = string.Join(", ", cmd.Aliases);
					x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" +
							  $"Summary: {cmd.Summary}";
					x.IsInline = false;
				});
			}

			await ReplyAsync("", false, builder.Build());
		}

// Unused command | Updated version is ~info //
#if unused
		{[Command("ok", RunMode = RunMode.Async)]
		public async Task ok()
		{
			var application = await Context.Client.GetApplicationInfoAsync();
			var footerbuilder = new EmbedFooterBuilder()
				.WithText("Bot is under active development.");
			var builder = new EmbedBuilder()
				.WithColor(new Color(0, 255, 0))
				.WithFooter(footerbuilder)
				.WithThumbnailUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg")
				.WithCurrentTimestamp()
				.WithTitle("Bot Information!")
				.WithDescription(
				$"{Format.Bold("Info")}\n" +
				$"- **Author**: xSklzx Dark | DarkEDM CEO#8330\n" +
				$"- **Library**: Discord.Net ({DiscordConfig.Version})\n" +
				$"- **Runtime**: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
				$"- **Uptime**: {GetUptime()}\n\n" +

				$"{Format.Bold("Stats")}\n" +
				$"- **Heap Size**: {GetHeapSize()} MB\n" +
				$"- **Guilds**: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
				$"- **Channels**: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}\n" +
				$"- **Users**: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}\n");

			await ReplyAsync("", embed: builder);
		}
#endif

		[Command("info", RunMode = RunMode.Async)]
		public async Task Info()
		{
			var footer = new EmbedFooterBuilder()
				.WithText("Run ~support for the support server invitation!")
				.WithIconUrl("https://pbs.twimg.com/media/DD92ejDW0AA-2Ik.png:large");
			var author = new EmbedAuthorBuilder()
				.WithName("KiraBot")
				.WithIconUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg");
			var builder = new EmbedBuilder()
			.WithTitle("KiraBot Stats")
			.WithColor(new Color(0, 255, 0))
			.WithCurrentTimestamp()
			.WithFooter(footer)
			.WithAuthor(author)
			.AddInlineField(":star: __Developer__ :star:", "       xSklzx Dark")
			.AddInlineField(":blue_book: __Library__ :blue_book: ", " Discord.Net 1.0")
			.AddInlineField(":runner: __Runtime__ :runner: ", "    .NET Core 4.6")
			.AddInlineField(":arrow_up: __Uptime__ :arrow_up:", $"  {GetUptime()}")
			.AddInlineField(":signal_strength: __Heap Size__ :signal_strength:", $"{GetHeapSize()} MB")
			.AddInlineField(":trophy: __Guilds__ :trophy: ", $"        {(Context.Client as DiscordSocketClient).Guilds.Count}")
			.AddInlineField(":tv: __Channels__ :tv:", $"        {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}\n")
			.AddInlineField(":desktop: __Users__ :desktop:", $"        {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}");

			await ReplyAsync("", embed: builder);
		}

		[Command("developer", RunMode = RunMode.Async)]
		[Alias("dev")]
		public async Task Developer()
		{
			var application = await Context.Client.GetApplicationInfoAsync();
			var authorbuilder = new EmbedAuthorBuilder()
				.WithName("KiraBot")
				.WithIconUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg");
			var footerbuilder = new EmbedFooterBuilder()
				.WithText("You can contact me by using the KiraBot Support server. Type ~support for the invitation!");
			var builder = new EmbedBuilder()
				.WithColor(new Color(0, 255, 0))
				.WithFooter(footerbuilder)
				.WithTitle("Who Developed Me?")
				.WithDescription(
				$"{Format.Bold("Well. You're about to find out!")}\n\n" +
				$"My creator is xSklzx Dark | Coded using the Discord.Net library\n" +
				$"Over the course of about 3 days.\n" +
				$"As you may or may not know, this bot is still in active development\n" +
				$"So for now, sit tight! More features are on the way!\n");

			await ReplyAsync("", embed: builder);
		}

		[Command("donators", RunMode = RunMode.Async)]
		public async Task Donators()
		{
			var application = await Context.Client.GetApplicationInfoAsync();
			var footerbuilder = new EmbedFooterBuilder()
				.WithText("Feeling generous enough to donate? Contact me personally!");
			var authorbuilder = new EmbedAuthorBuilder()
				.WithName("The following people have donated to this bot!");
			var builder = new EmbedBuilder()
				.WithColor(new Color(0, 255, 0))
				.WithFooter(footerbuilder)
				.WithAuthor(authorbuilder)
				.WithTitle("If you're on this list, thank you so much for your support")
				.WithDescription(":star: TBA :star:");

			await ReplyAsync("", embed: builder);
		}

		[Command("userinfo")]
		[Alias("user", "whois")]
		[Summary("Returns info about the current user, or the user parameter, if one passed.")]
		public async Task UserInfo([Summary("The (optional) user to get info for")] IUser user = null)
		{
			var userInfo = user ?? Context.Client.CurrentUser;
			await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
		}

		[Command("support")]
		[Summary("Returns the ")]

	//[Command("serverinfo")]
	//[Summary("Returns info about the server the command was triggered in")]
	//[Alias("sinfo")]
	//public async Task ServerInfo(string guildName = null)
	//{
	//		var channel = (ITextChannel)Context.Channel;
	//guildName = guildName?.ToUpperInvariant();
	//IGuild guild;
	//if (string.IsNullOrWhiteSpace(guildName))
	//guild = channel.Guild;
	//else
	//guild = NadekoBot.Client.GetGuilds().FirstOrDefault(g => g.Name.ToUpperInvariant() == guildName.ToUpperInvariant());
	//if (guild == null)
	//	return;
	//var ownername = await guild.GetUserAsync(guild.OwnerId);
	//var textchn = (await guild.GetTextChannelsAsync()).Count();
	//var voicechn = (await guild.GetVoiceChannelsAsync()).Count();

	//var createdAt = new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(guild.Id >> 22);
	//var users = await guild.GetUsersAsync().ConfigureAwait(false);
	//var features = string.Join("\n", guild.Features);
	//	if (string.IsNullOrWhiteSpace(features))
	//features = "-";
	//var builder = new EmbedBuilder()
	//.WithAuthor(eab => eab.WithName(GetText("server_info")))
	//.WithTitle(guild.Name)
	//.AddField(fb => fb.WithName(GetText("id")).WithValue(guild.Id.ToString()).WithIsInline(true))
	//.AddField(fb => fb.WithName(GetText("owner")).WithValue(ownername.ToString()).WithIsInline(true))
	//.AddField(fb => fb.WithName(GetText("members")).WithValue(users.Count.ToString()).WithIsInline(true))
	//.AddField(fb => fb.WithName(GetText("text_channels")).WithValue(textchn.ToString()).WithIsInline(true))
	//.AddField(fb => fb.WithName(GetText("voice_channels")).WithValue(voicechn.ToString()).WithIsInline(true))
	//.AddField(fb => fb.WithName(GetText("created_at")).WithValue($"{createdAt:dd.MM.yyyy HH:mm}").WithIsInline(true))
	//.AddField(fb => fb.WithName(GetText("region")).WithValue(guild.VoiceRegionId.ToString()).WithIsInline(true))
	//.AddField(fb => fb.WithName(GetText("roles")).WithValue((guild.Roles.Count - 1).ToString()).WithIsInline(true))
	//.AddField(fb => fb.WithName(GetText("features")).WithValue(features).WithIsInline(true))
	//.WithImageUrl(guild.IconUrl)
	//.WithColor(new Color(0, 255, 0));

	//await ReplyAsync("", embed: builder);
	//}

	private static string GetUptime()
			=> (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
		private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
	}
}
