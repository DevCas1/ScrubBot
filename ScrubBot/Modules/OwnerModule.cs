﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ScrubBot.Modules
{
    [RequireOwner]
    class OwnerModule : ModuleBase<SocketCommandContext>
    {
        [Command("ChangeGame")]
        public async Task ChangeGame([Remainder]string newGame)
        {
            await ReplyAsync("Received command ChangeGame");
            EmbedBuilder embed = new EmbedBuilder
            {
                Color = Color.Magenta,
                Title = "Game changed"
            };
            embed.AddInlineField(String.Empty, $"Changed active game from {Context.Client.CurrentUser.Game} to {newGame}");
            await Context.Client.SetGameAsync(newGame);
            await ReplyAsync("", false, embed.Build());
            await ReplyAsync("Job's done");
        }
    }
}