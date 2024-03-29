﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using MyDiscordBot.Services;

namespace MyDiscordBot.Modules
{
    // Modules must be public and inherit from an IModuleBase
    public class DevelopModule : ModuleBase<SocketCommandContext>
    {
        // Dependency Injection will fill this value in for us
        // public PictureService PictureService { get; set; }
        public IConfiguration Configuration { get; set; }
        public MyFirstBotDbContext db { get; set; }

#if DEBUG
        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");

        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;

            await ReplyAsync(user.ToString());
        }

        [RequireUserPermission(GuildPermission.ManageChannels, ErrorMessage = "チャンネルを作る権限が無いよ！")]
        [RequireBotPermission(GuildPermission.ManageChannels)]
        [Command("create")]
        public async Task CreateGroup()
        {
            var user = Context.User as IGuildUser;
            ulong.TryParse(Configuration["category:general"], out ulong categoryId);
            var a = await user.Guild.CreateTextChannelAsync(user.Username ?? "aaa", x => { x.CategoryId = categoryId; });
        }

        [Command("tell")]
        public async Task Tell(params IUser[] users)
        {
            foreach(var user in users)
            {
                await user.SendMessageAsync(db.WordwolfThemes.First().A);
            }
        }

        // [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
        [Command("echo")]
        public Task EchoAsync([Remainder] string text)
            // Insert a ZWSP before the text to prevent triggering other bots!
            => ReplyAsync('\u200B' + text);

        // 'params' will parse space-separated elements into a list
        [Command("list")]
        public Task ListAsync(params string[] objects)
            => ReplyAsync("You listed: " + string.Join("; ", objects));

        // Setting a custom ErrorMessage property will help clarify the precondition error
        [Command("guild_only")]
        [RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not a DM!")]
        public Task GuildOnlyCommand()
            => ReplyAsync("Nothing to see here!");

#endif
    }
}