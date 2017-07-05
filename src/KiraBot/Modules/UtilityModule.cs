// System Requirements
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
// Discord Requirements
using Discord;
using Discord.Commands;
using Discord.WebSocket;
// KiraBot Requirements
using KiraBot;
using KiraBot.Extensions;

namespace KiraBot.Modules
{
	public class UtilityModule : ModuleBase<SocketCommandContext>
	{
		[Command("manualwelcome")]
		[Summary("Returns info about the current user, or the user parameter, if one passed.")]
		[Alias("user", "whois")]
		public async Task ManualWelcome(IUser user = null)
		{
			var userWelcome = user ?? Context.Client.CurrentUser;
			var author = new EmbedAuthorBuilder()
				.WithName("KiraBot")
				.WithIconUrl("https://pbs.twimg.com/media/DD1pCKuWAAEwgtL.jpg");
			var footer = new EmbedFooterBuilder()
				.WithText($"Command executed by {MessageSource.User}.");
			var builder = new EmbedBuilder()
				.WithAuthor(author)
				.WithFooter(footer)
				.WithColor(new Color(0, 255, 0))
				.WithCurrentTimestamp()
				.WithDescription($"Welcome to the server, {userWelcome.Mention}! We hope you enjoy your stay!");

			await ReplyAsync("", false, builder.Build());
		}

#if completed
		[Command("av")]
		[Summary("Returns the direct picture link of the mentioned user's avatar.")]
		[Alias("avatar")]
		public async Task Avatar([Remainder] IUser usr = null)
		{
			if (usr == null)
				usr = Context.User;

			var avatarUrl = usr.RealAvatarUrl();
			var shortenedAvatarUrl = await NadekoBot.Google.ShortenUrl(avatarUrl).ConfigureAwait(false);
			await Context.Channel.EmbedAsync(new EmbedBuilder().WithOkColor()
				.AddField(efb => efb.WithName("Username").WithValue(usr.ToString()).WithIsInline(false))
				.AddField(efb => efb.WithName("Avatar Url").WithValue(shortenedAvatarUrl).WithIsInline(false))
				//.AddField(efb => efb.WithName("Avatar Id").WithValue(usr.AvatarId).WithIsInline(false))
				.WithThumbnailUrl(avatarUrl), Context.User.Mention).ConfigureAwait(false);
		}
#endif
		private static string GetUptime()
				=> (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
		private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
	}
}
