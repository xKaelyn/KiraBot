// System Requirements
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq;
using KiraBot;
// Discord Requirements
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace KiraBot.Modules
{
	public class CustomCommands : ModuleBase<SocketCommandContext>
	{
		[Command("ael", RunMode = RunMode.Async)]
		[Alias("tgm")]
		[Summary("Returns the custom command advertising TGM & AEL")]
		public async Task AEL()
		{
			var footerbuilder = new EmbedFooterBuilder()
				.WithText("Want to own Custom Command? Contact me personally!");
			var authorbuilder = new EmbedAuthorBuilder()
				.WithName("KiraBot")
				.WithIconUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg");
			var builder = new EmbedBuilder()
				.WithColor(new Color(221, 160, 221))
				.WithFooter(footerbuilder)
				.WithAuthor(authorbuilder)
				.WithTitle(":rose: Check out TGM Group and the AEL on ROBLOX! :rose:")
				.WithDescription("Click [here](https://www.roblox.com/My/Groups.aspx?gid=1043427) to visit the group page \n **&** \n Click [here](https://www.roblox.com/games/76470734/Subland-City-THE-TRUE-ORIGINAL) to visit Subland City \n **&** \n Click [here](https://discord.gg/YtvHJ4F) to visit the Discord!");

			await ReplyAsync("", embed: builder);
		}
		[Command("thejojonetwork", RunMode = RunMode.Async)]
		public async Task TheJojoNetwork()
		{
			var application = await Context.Client.GetApplicationInfoAsync();
			var footerbuilder = new EmbedFooterBuilder()
				.WithText("Want your own Custom Command? Contact me personally!");
			var authorbuilder = new EmbedAuthorBuilder()
				.WithName("KiraBot")
				.WithIconUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg");
			var builder = new EmbedBuilder()
				.WithColor(new Color(132, 0, 255))
				.WithFooter(footerbuilder)
				.WithAuthor(authorbuilder)
				.WithTitle(":heart: Check out TheJojoNetwork's channel & server! :heart:")
				.WithDescription("Click [here](https://www.youtube.com/channel/UChQ4PiJhSBEr4xHjGRaCM7A) to visit his channel \n **&** \n Click [here](https://discord.gg/bfRUFQz) to visit his discord server!");

			await ReplyAsync("", embed: builder);
		}

		private static string GetUptime()
				=> (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
		private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
	}
}
