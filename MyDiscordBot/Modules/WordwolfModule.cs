using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using MyDiscordBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiscordBot.Modules
{
    public class WordwolfModule : ModuleBase<SocketCommandContext>
    {
        // ↓DI
        public WordwolfService WordwolfService { get; set; }
        public IConfiguration Configuration { get; set; }
        public MyFirstBotDbContext DB { get; set; }
        // ↑DI

        [Command("wordwolf")]
        public async Task Wordwolf(params IUser[] users)
        {
            var status = await WordwolfService.GetStatus();
            if (status.IsNowPlaying)
            {
                await ReplyAsync("テーマは「" + status.Theme.A + "」と「" + status.Theme.B + "」でした。");
                await ReplyAsync("人狼は" + status.Wolf.Username + "でした。");
                await WordwolfService.EndGame();
            }
            else
            {
                var userList = users.Distinct().ToList();
                if (userList.Count < 1)// 多分3が適正
                {
                    await ReplyAsync("ワードウルフの最低プレイ人数は3人です！");
                    return;
                }
                var triple = await WordwolfService.StartGame(userList);

                await Task.WhenAll(userList.Select(async user =>
                {
                    if (user.Id == triple.Item1.Id)
                    {
                        await user.SendMessageAsync(triple.Item2);
                    }
                    else
                    {
                        await user.SendMessageAsync(triple.Item3);
                    }
                }));
            }
        }
    }
}
