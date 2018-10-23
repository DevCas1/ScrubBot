﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ScrubBot.Handlers
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly PrefixHandler _prefixHandler;

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;

            _commandService = new CommandService(new CommandServiceConfig()
            {
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Debug
            });

            _commandService.AddModulesAsync(Assembly.GetEntryAssembly()).Wait();
            _commandService.Log += CommandServiceLog;

            Container.Add(_commandService);

            _prefixHandler = Container.Get<PrefixHandler>();
        }

        private Task CommandServiceLog(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());
            return Task.CompletedTask;
        }

        public async Task OnMessageRecievedAsync(SocketMessage msg)
        {
            SocketUserMessage message = msg as SocketUserMessage;

            if (message is null || message.Author.IsBot)
                return;

            string stringPrefix = _prefixHandler.Get((message.Channel as SocketGuildChannel).Guild.Id);
            int argPos = 0;

            bool hasStringPrefix = message.HasStringPrefix(stringPrefix, ref argPos);
            bool isMentioned = message.HasMentionPrefix(_client.CurrentUser, ref argPos);

            if (!hasStringPrefix && !isMentioned)
                return;

            SocketCommandContext context = new SocketCommandContext(_client, message);
            IResult result = await _commandService.ExecuteAsync(context, argPos, Container.GetServiceProvider());

            if (!result.IsSuccess)
            {
                Console.WriteLine(new LogMessage(LogSeverity.Error, "Command", result.ErrorReason));
            }
        }
    }
}
