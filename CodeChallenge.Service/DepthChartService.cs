using System;
using System.Collections.Generic;
using System.Linq;
using CodeChallenge.Service.Exceptions;
using CodeChallenge.Service.Interface;
using CodeChallenge.Service.Model;

namespace CodeChallenge.Service
{
    public class DepthChartService : IDepthChartService
    {
        private readonly IDictionary<string, IList<Player>> _depthChart = new Dictionary<string, IList<Player>>();

        public void AddPlayerToDepthChart(string position, Player player, int? positionDepth = null)
        {
            ValidateRecord(position, player, positionDepth);

            StandardisePosition(ref position);

            if (!_depthChart.Any(x => x.Key.Contains(position)))
            {
                AddPosition(position);
            }

            AddPlayer(position, positionDepth, player);
        }

        public void RemovePlayerFromDepthChart(string position, Player player)
        {
            ValidatePosition(position);

            ValidatePlayer(player);

            StandardisePosition(ref position);

            ValidatePositionInDepthChart(position);
            
            if (_depthChart[position].Any(x => x.Number == player.Number)) 
            {
                _depthChart[position].Remove(player);
            }
            else
            { 
                throw new PlayerDoesntExistException();
            }
        }

        public IList<Player> GetBackups(string position, Player player)
        {
            
            ValidatePosition(position);

            ValidatePlayer(player);

            StandardisePosition(ref position);

            ValidatePositionInDepthChart(position);

            try
            {
                ValidatePlayerAtPosition(position, player);
            }
            catch (PlayerDoesntExistException)
            {
                //If the player is not found then return a empty list
                return new List<Player>();
            }
            
            var index = _depthChart[position].IndexOf(player);

            var resultLength = _depthChart[position].Count - (index + 1);
            return _depthChart[position].ToList().GetRange(index + 1,resultLength);
        }

        public IDictionary<string, IList<Player>> GetFullDepthChart()
        {
            return _depthChart;
        }

        #region Validation Methods
        private void ValidateRecord(string position, Player player, int? positionDepth)
        {
            ValidatePosition(position);

            ValidatePositionDepth(positionDepth);

            ValidatePlayer(player);
        }

        private void ValidatePositionDepth(int? positionDepth)
        {
            if ((positionDepth ?? 0) < 0)
                throw new InvalidPositionDepthException();
        }

        private void ValidatePlayer(Player player)
        {
            if (player == null || player.Number <= 0)
                throw new InvalidPlayerException();
        }

        private void ValidatePosition(string position)
        {
            if (String.IsNullOrWhiteSpace(position))
                throw new InvalidPositionException();
        }

        private void ValidatePositionInDepthChart(string position)
        {
            if (!_depthChart.Keys.Contains(position))
                throw new PositionDoesntExistException();
        }

        private void ValidatePlayerAtPosition(string position, Player player)
        {
            if (!_depthChart[position].Contains(player))
            {
                throw new PlayerDoesntExistException();
            }
        }

        #endregion

        #region Common Method to standardise position
        private void StandardisePosition(ref string position)
        {
            position = !string.IsNullOrWhiteSpace(position) ? position.Trim().ToUpper() : String.Empty;
        }
        #endregion

        #region Methods to add position and player

        private void AddPlayer(string position, int? positionDepth, Player player)
        {
            if (_depthChart[position].Any(x => x.Number == player.Number))
            {
                throw new ApplicationException("Player already in this position");
            }

            if (positionDepth != null)
                _depthChart[position].Insert(positionDepth.Value, player);
            else
                _depthChart[position].Add(player);

        }

        private void AddPosition(string position)
        {
            _depthChart.Add(position, new List<Player>());
        }
        #endregion
    }
}
