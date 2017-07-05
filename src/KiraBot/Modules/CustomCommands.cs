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
		public static Color OkColor { get; }
		public static Color ErrorColor { get; }

		[Command("ocr", RunMode = RunMode.Async)]
		public async Task OCR()
		{
			var application = await Context.Client.GetApplicationInfoAsync();
			var footerbuilder = new EmbedFooterBuilder()
				.WithText("Want your own Custom Command? Contact me personally!");
			var authorbuilder = new EmbedAuthorBuilder()
				.WithName("KiraBot")
				.WithIconUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg");
			var builder = new EmbedBuilder()
				.WithColor(new Color(0, 255, 0))
				.WithFooter(footerbuilder)
				.WithAuthor(authorbuilder)
				.WithTitle("Check out Owen Chua Railways on ROBLOX!")
				.WithDescription(":star: Click [here](https://www.roblox.com/games/693019234/OCR-Autonomous-Rail-Original) to visit! :star:");

			await ReplyAsync("", embed: builder);
		}

		private static string GetUptime()
				=> (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
		private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
	}
}
