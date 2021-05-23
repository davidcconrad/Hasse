﻿using System;
using Ardalis.GuardClauses;
using Hasse.Core.GameAggregate.Player;
using Hasse.SharedKernel;

namespace Hasse.Core.GameAggregate.Team
{
    public sealed class TeamBuilder : LazyBuilder<Team, TeamBuilder>, ITeamBuilder
    {
        private readonly IPlayerBuilder _playerBuilder;
        private (Player.Player, Player.Player) _players;

        public TeamBuilder(string name, IPlayerBuilder playerBuilder)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            Do(t => t.Name = name);

            _playerBuilder = playerBuilder;
        }

        public TeamBuilder WithPlayer(Action<IPlayerBuilder> builder)
        {
            builder(_playerBuilder);

            var player = _playerBuilder.Build();

            AddPlayer(player);

            return this;
        }

        private void AddPlayer(Player.Player player)
        {
            if (_players.Item1 is null)
                _players.Item1 = player;
            else if (_players.Item2 is null)
                _players.Item2 = player;
        }

        protected override Team Construct()
        {
            return new(_players);
        }
    }

    public interface ITeamBuilderFactory
    {
        ITeamBuilder GetTeamBuilder(string name);
    }

    public class TeamBuilderFactory : ITeamBuilderFactory
    {
        private readonly IPlayerBuilder _playerBuilder;

        public TeamBuilderFactory(IPlayerBuilder playerBuilder)
        {
            _playerBuilder = playerBuilder;
        }

        public ITeamBuilder GetTeamBuilder(string name)
        {
            return new TeamBuilder(name, _playerBuilder);
        }
    }
}