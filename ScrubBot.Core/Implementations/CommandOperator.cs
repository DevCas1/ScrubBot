﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;

using ScrubBot.Managers;
using ScrubBot.Tools;

using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ScrubBot.Core
{
    internal class CommandOperator : ICommandOperator
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly IPrefixManager _prefixManager;

        public CommandOperator(DiscordSocketClient client)
        {
            _client = client;
            _prefixManager = Container.Get<PrefixManager>();

            _commandService = new CommandService(new CommandServiceConfig()
                {
                    DefaultRunMode = RunMode.Async,
                    CaseSensitiveCommands = false,
                    LogLevel = LogSeverity.Debug
                }
            );

            _commandService.AddModulesAsync(Assembly.GetAssembly(typeof(Modules.Module))).Wait();
            _commandService.Log += Logger.Log;

            Container.Add(_commandService);
        }

        public async Task ExecuteAsync(SocketMessage socketMessage)
        {
            SocketUserMessage message = socketMessage as SocketUserMessage;

            if (IsMessageValid(message, out int argPos))
            {
                SocketCommandContext context = new SocketCommandContext(_client, message);
                IResult result = await _commandService.ExecuteAsync(context, argPos, Container.GetServiceProvider());

                if (!result.IsSuccess)
                {
                    Console.WriteLine(new LogMessage(LogSeverity.Error, "Command", result.ErrorReason));
                }
            }
        }

        private bool IsMessageValid(SocketUserMessage message, out int argPos)
        {
            argPos = 0;

            if (message is null || message.Author.IsBot)
                return false;

            string prefix = _prefixManager.Get((message.Channel as SocketGuildChannel).Guild.Id);

            bool hasPrefix = message.HasStringPrefix(prefix, ref argPos);
            bool isMentioned = message.HasMentionPrefix(_client.CurrentUser, ref argPos);

            if (!hasPrefix && !isMentioned)
                return false;

            return true;
        }
    }
}
