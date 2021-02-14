using Discord.Commands;
using Microsoft.Extensions.Configuration;
using MyDiscordBot.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyDiscordBot.Modules
{
    public class WordwolfModule : ModuleBase<SocketCommandContext>
    {
        // ↓DI
        public WordwolfService wordwolfService { get; set; }
        public IConfiguration Configuration { get; set; }
        public MyFirstBotDbContext db { get; set; }
        // ↑DI

        [Command("wordwolf")]
        public Task Wordwolf()
        {
            return ReplyAsync("wordwolf");
        }
    }
}
