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
	public class FuckupsModule : ModuleBase<SocketCommandContext>
	{
		[Command("lutfifuckup1")]
		[Summary("Lutfi's fuck up!")]
		public async Task LuftiFuckUp()
		{
			var author = new EmbedAuthorBuilder()
				.WithName("KiraBot")
				.WithIconUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg");
			var footer = new EmbedFooterBuilder()
				.WithText("Got a fuck up you want to show? Contact me personally!");
			var builder = new EmbedBuilder()
				.WithAuthor(author)
				.WithColor(new Color(0, 255, 0))
				.WithCurrentTimestamp()
				.WithTitle("LutfiWesker's Fuck Up #1")
				.WithImageUrl("https://cdn.discordapp.com/attachments/332194446147846145/332195662181564426/unknown.png")
				.WithFooter(footer);

			await ReplyAsync("", false, builder.Build());
		}

		private static string GetUptime()
			=> (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
		private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
	}
}
