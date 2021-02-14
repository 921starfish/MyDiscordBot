using Discord;
using MyDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiscordBot.Services
{
    public class WordwolfService
    {
        private Status status { get; set; }
        public List<IUser> Players { get; set; }
        public MyFirstBotDbContext DB { get; set; }

        public WordwolfService(MyFirstBotDbContext _db)
        {
            DB = _db;
            status = new Status()
            {
                IsNowPlaying = false,
                Wolf = null,
                Theme = null,
                Voted = null
            };
        }

        public async Task SetPlayers(List<IUser> users)
        {
            this.Players = users.Distinct().ToList();
        }

        public async Task<(IUser, string, string)> StartGame(bool latest = false)
        {
            status.IsNowPlaying = true;
            status.Voted = new Dictionary<IUser, IUser>();

            var random = new Random();
            status.Wolf = Players[random.Next(Players.Count)];

            if (latest)
            {
                status.Theme = await DB.WordwolfThemes.LastAsync();
            }
            else
            {
                var allThemes = await DB.WordwolfThemes.ToListAsync();
                status.Theme = allThemes[random.Next(allThemes.Count)];
            }
            var (jinroTheme, villagerTheme) = random.Next(2) == 0 ? (status.Theme.A, status.Theme.B) : (status.Theme.B, status.Theme.A);
            return (status.Wolf, jinroTheme, villagerTheme);
        }

        public async Task EndGame()
        {
            status.IsNowPlaying = false;
            status.Wolf = null;
            status.Theme = null;
            status.Voted = null;
        }

        public async Task<Status> GetStatus()
        {
            return status;
        }

        public async Task Add(string a, string b)
        {
            var theme = new WordwolfTheme(A: a, B: b);
            await DB.AddAsync(theme);
            await DB.SaveChangesAsync();
        }

        public class Status
        {
            public bool IsNowPlaying { get; set; }
            public IUser Wolf { get; set; }
            public WordwolfTheme Theme { get; set; }
            public Dictionary<IUser, IUser> Voted { get; set; }
        }

    }
}