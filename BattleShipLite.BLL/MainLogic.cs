using BattleShipLite.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipLite.BLL
{
    public static class MainLogic
    {
        public static void InitializeGrid(PlayerInfoModel model)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }
            }
        }

        private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };

            model.ShotGrid.Add(spot);
        }

        public static bool PlaceShip(PlayerInfoModel model, string? location)
        {
            bool output = false;
            (string row, int column) = SplitShotIntoRowAndColumn(location);

            bool isValidLocation = ValidateGridLocation(model, row, column);
            bool isSpotOpen = ValidateShipLocation(model, row, column);

            if (isValidLocation && isSpotOpen)
            {
                model.ShipLocations.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });

                output = true;
            }

            return output;
        }

        private static bool ValidateShipLocation(PlayerInfoModel model, string row, int column)
        {
            bool output = true;

            foreach (var ship in model.ShipLocations)
            {
                if (MatchedToAvailableLocation(ship, row, column))
                {
                    output = false;
                }
            }

            return output;
        }

        private static bool MatchedToAvailableLocation(GridSpotModel ship, string row, int column)
        {
            bool output = false;

            bool letterMatched = (ship.SpotLetter == row.ToUpper());
            bool numberMatched = (ship.SpotNumber == column);

            if (letterMatched && numberMatched)
            {
                output = true;
            }

            return output;
        }

        private static bool ValidateGridLocation(PlayerInfoModel model, string row, int column)
        {
            bool output = false;

            foreach (var ship in model.ShotGrid)
            {
                if (MatchedToAvailableLocation(ship, row, column))
                {
                    output = true;
                }
            }

            return output;
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            string row = "";
            int column = 0;

            if (shot.Length != 2)
            {
                throw new ArgumentException("This was an invalid shot type.", "shot");
            }

            char[] shotArray = shot.ToArray();

            row = shotArray[0].ToString();
            column = int.Parse(shotArray[1].ToString());

            return (row, column);
        }

        public static bool ValidateShot(PlayerInfoModel player, string row, int column)
        {
            bool output = false;

            foreach (var gridSpot in player.ShotGrid)
            {
                if (MatchedToAvailableLocation(gridSpot, row, column))
                {
                    if (gridSpot.Status == GridSpotStatus.Empty)
                    {
                        output = true;
                    }
                }
            }

            return output;
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            bool output = false;

            foreach (var ship in opponent.ShipLocations)
            {
                if (MatchedToAvailableLocation(ship, row, column))
                {
                    output = true;
                    ship.Status = GridSpotStatus.Sunk;
                }
            }

            return output;
        }

        public static void MarkShotResult(PlayerInfoModel player, string row, int column, bool isAHit)
        {
            foreach (var gridSpot in player.ShotGrid)
            {
                if (MatchedToAvailableLocation(gridSpot, row, column))
                {
                    if (isAHit)
                    {
                        gridSpot.Status = GridSpotStatus.Hit;
                    }
                    else
                    {
                        gridSpot.Status = GridSpotStatus.Miss;
                    }
                }
            }
        }

        public static bool PlayerStillActive(PlayerInfoModel player)
        {
            bool output = false;

            foreach (var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    output = true;
                }
            }

            return output;
        }

        public static int GetShotCount(PlayerInfoModel player)
        {
            int output = 0;

            foreach (var shot in player.ShotGrid)
            {
                if (shot.Status != GridSpotStatus.Empty)
                {
                    output += 1;
                }
            }

            return output;
        }
    }
}
