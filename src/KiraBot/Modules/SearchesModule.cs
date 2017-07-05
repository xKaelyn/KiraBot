// System Requirements
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Linq;using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// Discord Requirements
using Discord;
using Discord.Commands;
using Discord.WebSocket;
// KiraBot Requirements
using KiraBot;
using KiraBot.Extensions;

namespace KiraBot.Modules
{
	public class SearchesModule : ModuleBase<SocketCommandContext>
	{
		[Command("randomcat")]
		[Alias("rcat")]
		[Summary("Returns a random cat!")]
		public async Task RandomCat()
		{
			using (var http = new HttpClient())
			{
				var res = JObject.Parse(await http.GetStringAsync("http://www.random.cat/meow").ConfigureAwait(false));
				await Context.Channel.SendMessageAsync(Uri.EscapeUriString(res["file"].ToString())).ConfigureAwait(false);
			}
		}

		[Command("randomdog")]
		[Alias("rdog")]
		[Summary("Returns a random dog!")]
		public async Task RandomDog()
		{
			using (var http = new HttpClient())
			{
				await Context.Channel.SendMessageAsync("http://random.dog/" + await http.GetStringAsync("http://random.dog/woof")
							 .ConfigureAwait(false)).ConfigureAwait(false);
			}
		}

#if completed
		[Command("catfact")]
		[Alias("cfact")]
		[Summary("Returns a random cat fact!")]
		public async Task Catfact()
		{
			using (var http = new HttpClient())
			{
				var response = await http.GetStringAsync("http://catfacts-api.appspot.com/api/facts").ConfigureAwait(false);
				if (response == null)
					return;

				var fact = JObject.Parse(response)["facts"][0].ToString();
				await Context.Channel.SendMessageAsync("🐈" + GetText("catfact"), fact).ConfigureAwait(false);
			}
		}
#endif

		private static string GetUptime()
			=> (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
		private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
	}
}
