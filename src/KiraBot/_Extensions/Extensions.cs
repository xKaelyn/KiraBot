using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ImageSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KiraBot.Extensions
{
	public static class Extensions
	{
		public static string RealAvatarUrl(this IUser usr)
		{
			return usr.AvatarId.StartsWith("a_")
					? $"{DiscordConfig.CDNUrl}avatars/{usr.Id}/{usr.AvatarId}.gif"
					: usr.GetAvatarUrl();
		}
	}
}
