using BattleShipLite.BLL.Models;
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

        internal static void Core(PlayerInfoModel activePlayer, PlayerInfoModel opponent, PlayerInfoModel winner)
        {
            throw new NotImplementedException();
        }

        internal static void End(PlayerInfoModel winner)
        {
            throw new NotImplementedException();
        }
    }
}
