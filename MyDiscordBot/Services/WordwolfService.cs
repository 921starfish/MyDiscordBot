﻿using Discord;
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

        public async Task<(IUser, string, string)> StartGame(List<IUser> users)
        {
            status.IsNowPlaying = true;
            status.Voted = new Dictionary<IUser, IUser>();

            var random = new Random();
            status.Wolf = users[random.Next(users.Count - 1)];

            var allThemes = await DB.WordwolfThemes.ToListAsync();
            status.Theme = allThemes[random.Next(allThemes.Count - 1)];
            var (jinroTheme, villagerTheme) = random.Next(1) == 1 ? (status.Theme.A, status.Theme.B) : (status.Theme.B, status.Theme.A);
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

        public class Status
        {
            public bool IsNowPlaying { get; set; }
            public IUser Wolf { get; set; }
            public WordwolfTheme Theme { get; set; }
            public Dictionary<IUser, IUser> Voted { get; set; }
        }

    }
}