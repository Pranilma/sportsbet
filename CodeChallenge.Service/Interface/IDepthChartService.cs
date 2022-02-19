using System.Collections.Generic;
using CodeChallenge.Service.Model;

namespace CodeChallenge.Service.Interface
{
    interface IDepthChartService
    {
        void AddPlayerToDepthChart(string position, Player player, int? positionDepth);

        void RemovePlayerFromDepthChart(string position, Player player);

        IList<Player> GetBackups(string position, Player player);

        IDictionary<string, IList<Player>> GetFullDepthChart();
    }
}
