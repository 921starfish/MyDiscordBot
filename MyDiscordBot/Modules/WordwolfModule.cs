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
        [Alias("ww")]
        public async Task Wordwolf(params IUser[] users)
        {
            var status = await WordwolfService.GetStatus();
            if (status.IsNowPlaying)
            {
                await ReplyAsync("テーマは「" + status.Theme.A + "」と「" + status.Theme.B + "」でした。");
                var result = new Dictionary<IUser, int>();
                foreach (var voted in status.Voted)
                {
                    if (result.ContainsKey(voted.Value))
                    {
                        result[voted.Value]++;
                    }
                    else
                    {
                        result.Add(voted.Value, 1);
                    }
                }
                await ReplyAsync("投票結果は以下の通りです。");
                await Task.WhenAll(result.Select(async res =>
                {
                    await ReplyAsync(res.Key.Username + "：" + res.Value + "票");
                }).ToArray());


                await ReplyAsync("人狼は" + status.Wolf.Username + "でした。");
                await WordwolfService.EndGame();
            }
            else
            {
                var userList = users.Distinct().ToList();
                if (userList.Count < 3)
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
                }).ToArray());
            }
        }

        [Command("vote")]
        public async Task Vote(IUser user = null)
        {
            var status = await WordwolfService.GetStatus();
            if (status.IsNowPlaying)
            {
                if (user == null)
                {
                    await ReplyAsync("投票する人を指定してください。");
                    return;
                }

                try
                {
                    status.Voted.Add(Context.User, user);
                }
                catch (ArgumentException)
                {
                    await ReplyAsync("既にあなたは投票済みです。");
                }
            }
            else
            {
                await ReplyAsync("まだゲームが始まっていません。");
            }
        }

        [Command("add")]
        public async Task Add(string a, string b)
        {
            await WordwolfService.Add(a, b);
            await ReplyAsync("正常に追加されました。");
        }

        [Command("start_latest")]
        [Alias("sl")]
        public async Task StartLatest(params IUser[] users)
        {
            var status = await WordwolfService.GetStatus();
            if (status.IsNowPlaying)
            {
                await ReplyAsync("別のゲームが進行中です。");
            }
            else
            {
                var userList = users.Distinct().ToList();
                if (userList.Count < 3)
                {
                    await ReplyAsync("ワードウルフの最低プレイ人数は3人です！");
                    return;
                }
                var triple = await WordwolfService.StartLatest(userList);

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
                }).ToArray());
            }
        }
    }
}
