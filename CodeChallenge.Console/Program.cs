using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeChallenge.Service;
using CodeChallenge.Service.Model;

namespace CodeChallenge.Console
{
    class Program
    {
        /// <summary>
        /// This would be the human testing app to verify the implementation
        /// for the requirement, i have not put in a lot of emphasis in this project
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var service = new DepthChartService();

            var tomBrady = new Player("Tom Brady", 12);
            var blaineGabbert = new Player("Blaine Gabbert", 11);
            var kyleTrask = new Player("Kyle Trask", 2);
            var mikeEvans = new Player("Mike Evans", 13);
            var jaelonDarden = new Player("Jaelon Darden", 1);
            var scottMiller = new Player("Scott Miller", 10);

            service.AddPlayerToDepthChart("QB", tomBrady, 0);
            service.AddPlayerToDepthChart("QB", blaineGabbert, 1);
            service.AddPlayerToDepthChart("QB", kyleTrask, 2);
            service.AddPlayerToDepthChart("LWR", mikeEvans, 0);
            service.AddPlayerToDepthChart("LWR", jaelonDarden, 1);
            service.AddPlayerToDepthChart("LWR", scottMiller, 2);

            //TEST 1
            var backups = service.GetBackups("QB", tomBrady).ToList();
            ShowOutputForBackups(backups);

            //TEST 2
            backups = service.GetBackups("QB", jaelonDarden).ToList();
            ShowOutputForBackups(backups);

            //TEST 3
            backups = service.GetBackups("QB", mikeEvans).ToList();
            ShowOutputForBackups(backups);

            //TEST 4
            backups = service.GetBackups("QB", blaineGabbert).ToList();
            ShowOutputForBackups(backups);

            //TEST 5
            backups = service.GetBackups("QB", kyleTrask).ToList();
            ShowOutputForBackups(backups);

            //TEST 6
            var depthChart = service.GetFullDepthChart();
            ShowFullDepthChart(depthChart);

            //TEST 7
            service.RemovePlayerFromDepthChart("LWR", mikeEvans);
            ShowRemovedPlayer(mikeEvans);
            
            //TEST 8
            depthChart = service.GetFullDepthChart();
            ShowFullDepthChart(depthChart);

            System.Console.Read();
        }

        private static void ShowRemovedPlayer(Player player)
        {
             System.Console.WriteLine($"#{player.Number} - {player.Name}");
            
            System.Console.WriteLine("---------------------------------");
        }

        private static void ShowOutputForBackups(List<Player> backups)
        {
            if(backups != null && backups.Count > 0)
                backups.ForEach(x => System.Console.WriteLine($"#{x.Number} - {x.Name}"));
            else
                System.Console.WriteLine("<NO LIST>");

            System.Console.WriteLine("---------------------------------");
        }

        private static void ShowFullDepthChart(IDictionary<string, IList<Player>> depthChart)
        {
            if (depthChart != null)
            {
                foreach (var item in depthChart)
                {
                    if (item.Value != null && item.Value.Count > 0)
                    {
                        var positionResultBuilder = new StringBuilder($"{item.Key} - ");
                        foreach (var player in item.Value)
                        {
                            positionResultBuilder.Append(
                                String.Format($"(#{player.Number}, {player.Name}), "));
                        }

                        var positionResultOutput = positionResultBuilder.ToString(0, positionResultBuilder.Length - 2);
                        System.Console.WriteLine(positionResultOutput);
                    }
                    else
                    {
                        System.Console.WriteLine("No players at position {0}", item.Key);
                    }
                }

                System.Console.WriteLine("---------------------------------");
            }

        }
    }
}
