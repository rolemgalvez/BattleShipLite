using BattleShipLite.BLL;
using BattleShipLite.BLL.Models;
using System;
using System.ComponentModel;
using System.Reflection;

namespace BattleShipLite.ConsoleUI
{
    public static class Section
    {
        public static void Welcome()
        {
            Console.WriteLine("Battleship Lite");
            Console.WriteLine("---------------");
            Console.WriteLine();
        }

        public static (PlayerInfoModel activePlayer, PlayerInfoModel opponent, PlayerInfoModel winner) Setup()
        {
            PlayerInfoModel activePlayer = Part.CreatePlayer("Player 1");
            PlayerInfoModel opponent = Part.CreatePlayer("Player 2");
            PlayerInfoModel winner = null;

            return (activePlayer, opponent, winner);
        }

        public static void Core(PlayerInfoModel activePlayer, PlayerInfoModel opponent, PlayerInfoModel winner)
        {
            do
            {
                Part.DisplayShotGrid(activePlayer);
                Part.RecordPlayerShot(activePlayer, opponent);

                bool continueGame = MainLogic.PlayerStillActive(opponent);

                if (continueGame)
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    winner = activePlayer;
                }

            } while (winner == null);
        }

        public static void End(PlayerInfoModel winner)
        {
            Part.IdentifyWinner(winner);

            Console.WriteLine();
            Console.WriteLine("Game over.");
            Console.ReadLine();
        }
    }
}
