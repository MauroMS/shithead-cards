﻿using System.Collections.Generic;
using System.Linq;

namespace ShitheadCardsApi.Models
{
    public class GameResponse
    {
        public GameResponse(Game game, string playerId)
        {
            Name = game.Name;
            Status = game.Status;
            DeckCount = game.CardsInDeck.Count;
            LastBurnedCard = game.LastBurnedCard;
            BurnedCardsCount = game.BurnedCardsCount;
            TableCards = game.TableCards;
            PlayerNameTurn = game.PlayerNameTurn;


            //Players = GameHelper.GetOtherPlayers(game.Players, 0, 0, 0, new List<Player>() { }).ConvertAll(op => new PlayerOtherResponse(op)); //game.Players.FindAll(gp => gp.Id != playerId).ConvertAll(op => new PlayerOtherResponse(op));
            MySelf = new PlayerMyselfResponse(game.Players.FirstOrDefault(gp => gp.Id == playerId));
        }

        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public List<PlayerOtherResponse> Players { get; set; }
        public PlayerMyselfResponse MySelf { get; set; }
        public List<string> TableCards { get; set; }
        public string LastBurnedCard { get; set; }
        public int BurnedCardsCount { get; set; }
        public int DeckCount { get; set; }
        public string PlayerNameTurn { get; set; }

        //public List<PlayerOtherResponse> GetOtherPlayers(List<Player> players, string playerId)
        //{
        //    var otherPlayers = new L
        //    var otherPlayersCount = players.Count - 3;
        //    var playerIndex = players.FindIndex(p => p.Id == playerId);

        //    for (int i = 2; i > 0; i--)
        //    {
        //        var idx = playerIndex - i;
        //        if (idx < 0)
        //            players[players.Count - idx];

        //        players[i]
        //    }

        //    return null;
        //}
    }

    public class PlayerOtherResponse
    {
        public PlayerOtherResponse (Player player)
        {
            Name = player.Name;
            HandCount = player.InHandCards.Count;
            DownCount = player.DownCards.Count;
            OpenCards = player.OpenCards;
            Status = player.Status;
        }
        public string Name { get; set; }
        public int HandCount { get; set; }
        public int DownCount { get; set; }
        public List<string> OpenCards { get; set; }
        public StatusEnum Status { get; set; }
    }

    public class PlayerMyselfResponse
    {
        public PlayerMyselfResponse(Player player)
        {
            if (player == null)
                return;

            Name = player.Name;
            HandCards = player.InHandCards.OrderBy(card => ShitheadService.GetNumericValue(ShitheadService.GetCardNumber(card))).ToList();
            DownCount = player.DownCards.Count;
            OpenCards = player.OpenCards;
            Status = player.Status;
        }

        public string Name { get; set; }
        public int DownCount { get; set; }
        public List<string> HandCards { get; set; }
        public List<string> OpenCards { get; set; }
        public StatusEnum Status { get; set; }
    }
}
