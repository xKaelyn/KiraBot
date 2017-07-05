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
	public class ShitpostingModule : ModuleBase<SocketCommandContext>
	{
		[Command("fuckoffkomori", RunMode = RunMode.Async)]
		public async Task FuckOffKomori()
		{
			var application = await Context.Client.GetApplicationInfoAsync();
			var footerbuilder = new EmbedFooterBuilder()
				.WithText("save me before im murdered");
			var builder = new EmbedBuilder()
				.WithColor(new Color(148, 0, 211))
				.WithFooter(footerbuilder)
				.WithTitle("guess what komori")
				.WithDescription(":middle_finger: fuck youuuuuu :middle_finger:");

			await ReplyAsync("", embed: builder);
		}

		private static string GetUptime()
				=> (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
		private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
	}
}
