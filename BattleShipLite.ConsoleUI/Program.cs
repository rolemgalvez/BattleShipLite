using BattleShipLite.BLL.Models;
using BattleShipLite.ConsoleUI;

Section.Welcome();

(PlayerInfoModel activePlayer, PlayerInfoModel opponent, PlayerInfoModel winner) = Section.Setup();

winner = Section.Core(activePlayer, opponent, winner);

Section.End(winner);
