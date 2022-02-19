using CodeChallenge.Service;
using CodeChallenge.Service.Exceptions;
using CodeChallenge.Service.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Service
{
    [TestClass]
    public class DepthChartServiceTests
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void Add_Null_Position()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart(null, new Player("A", 1));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void Add_Empty_Position()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart(" ", new Player("A", 1));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPlayerException))]
        public void Add_Null_Player()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart("aa", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionDepthException))]
        public void Add_Invalid_PositionDepth()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart("aa", new Player("A", 1), -1);
        }

        [TestMethod]
        public void Add_New_Position_UpperCase()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart("aa", new Player("A", 1));
            var result = svc.GetFullDepthChart();
            Assert.AreEqual(1, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("AA"));
        }

        [TestMethod]
        public void Add_Player_To_New_Position()
        {
            var svc = new DepthChartService();
            var player = new Player("A", 1);
            svc.AddPlayerToDepthChart("AA", player);
            var result = svc.GetFullDepthChart();
            Assert.AreEqual(1, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("AA"));
            Assert.AreEqual(1, result["AA"].Count);
            Assert.AreEqual(player, result["AA"][0]);
        }

        [TestMethod]
        public void Add_Another_New_Position_With_Same_Player()
        {
            var svc = new DepthChartService();
            var player = new Player("A", 1);
            svc.AddPlayerToDepthChart("aa", player);
            svc.AddPlayerToDepthChart("Bb", player);
            var result = svc.GetFullDepthChart();
            Assert.AreEqual(2, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("AA"));
            Assert.IsTrue(result.ContainsKey("BB"));
            Assert.AreEqual(player, result["AA"][0]);
            Assert.AreEqual(player, result["BB"][0]);
        }

        [TestMethod]
        public void Add_Another_New_Position_With_Different_And_Same_Player()
        {
            var svc = new DepthChartService();
            var player = new Player("A", 1);
            var player2 = new Player("B", 2);
            svc.AddPlayerToDepthChart("aa", player);
            svc.AddPlayerToDepthChart("Bb", player2);
            svc.AddPlayerToDepthChart("CC", player);
            var result = svc.GetFullDepthChart();
            Assert.AreEqual(3, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("AA"));
            Assert.IsTrue(result.ContainsKey("BB"));
            Assert.IsTrue(result.ContainsKey("CC"));
            Assert.AreEqual(player, result["AA"][0]);
            Assert.AreEqual(player2, result["BB"][0]);
            Assert.AreEqual(player, result["CC"][0]);
        }

        [TestMethod]
        public void Add_Player_To_Existing_Position_With_Depth()
        {
            var svc = new DepthChartService();
            var playerA = new Player("A", 1);
            svc.AddPlayerToDepthChart("AA", playerA);
            var playerB = new Player("B", 2);
            svc.AddPlayerToDepthChart("AA", playerB, 0);
            var result = svc.GetFullDepthChart();
            Assert.AreEqual(1, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("AA"));
            Assert.AreEqual(2, result["AA"].Count);
            Assert.AreEqual(playerB, result["AA"][0]);
            Assert.AreEqual(playerA, result["AA"][1]);
        }

        [TestMethod]
        public void Add_Player_To_Existing_Position_With_Depth_Three_Players()
        {
            var svc = new DepthChartService();
            var playerA = new Player("A", 1);
            svc.AddPlayerToDepthChart("AA", playerA);
            var playerC = new Player("C", 3);
            svc.AddPlayerToDepthChart("AA", playerC, 0);
            var playerB = new Player("B", 2);
            svc.AddPlayerToDepthChart("AA", playerB, 1);
            var result = svc.GetFullDepthChart();
            Assert.AreEqual(1, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("AA"));
            Assert.AreEqual(3, result["AA"].Count);
            Assert.AreEqual(playerC, result["AA"][0]);
            Assert.AreEqual(playerB, result["AA"][1]);
            Assert.AreEqual(playerA, result["AA"][2]);
        }

        [TestMethod]
        public void Add_Player_To_Existing_Position_With_Depth_Five_Players()
        {
            var svc = new DepthChartService();
            var playerA = new Player("A", 1);
            svc.AddPlayerToDepthChart("AA", playerA);
            var playerC = new Player("C", 3);
            svc.AddPlayerToDepthChart("AA", playerC);
            var playerB = new Player("B", 2);
            svc.AddPlayerToDepthChart("AA", playerB);
            var playerD = new Player("D", 4);
            svc.AddPlayerToDepthChart("AA", playerD);
            var playerE = new Player("E", 5);
            svc.AddPlayerToDepthChart("AA", playerE, 1);
            var result = svc.GetFullDepthChart();
            Assert.AreEqual(1, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("AA"));
            Assert.AreEqual(5, result["AA"].Count);
            Assert.AreEqual(playerA, result["AA"][0]);
            Assert.AreEqual(playerE, result["AA"][1]);
            Assert.AreEqual(playerC, result["AA"][2]);
            Assert.AreEqual(playerB, result["AA"][3]);
            Assert.AreEqual(playerD, result["AA"][4]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void RemovePlayerFromDepthChart_Position_Null()
        {
            var svc = new DepthChartService();
            svc.RemovePlayerFromDepthChart(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void RemovePlayerFromDepthChart_Position_Empty()
        {
            var svc = new DepthChartService();
            svc.RemovePlayerFromDepthChart(" ", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPlayerException))]
        public void RemovePlayerFromDepthChart_Player_Null()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart("AA", new Player("Q", 1));
            svc.RemovePlayerFromDepthChart("AA", null);
        }

        [TestMethod]
        [ExpectedException(typeof(PositionDoesntExistException))]
        public void RemovePlayerFromDepthChart_Unknown_Position()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart("AA", new Player("Q", 1));
            svc.RemovePlayerFromDepthChart("BB", new Player("P", 2));
        }

        [TestMethod]
        public void RemovePlayerFromDepthChart_Valid_Player_Removed()
        {
            var svc = new DepthChartService();
            var player1 = new Player("1", 1);
            var player2 = new Player("2", 2);
            var player3 = new Player("3", 3);
            svc.AddPlayerToDepthChart("AA", player1);
            svc.AddPlayerToDepthChart("AA", player2);
            svc.AddPlayerToDepthChart("AA", player3);
            svc.RemovePlayerFromDepthChart("AA", player2);
            var result = svc.GetFullDepthChart();
            Assert.AreEqual(2, result["AA"].Count);
            Assert.AreEqual(player1, result["AA"][0]);
            Assert.AreEqual(player3, result["AA"][1]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void GetBackups_Position_Null()
        {
            var svc = new DepthChartService();
            svc.GetBackups(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void GetBackups_Position_Empty()
        {
            var svc = new DepthChartService();
            svc.GetBackups(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPlayerException))]
        public void GetBackups_Position_Player_Null()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart("QQ", new Player("Q", 1));
            svc.GetBackups("QQ", null);
        }

        [TestMethod]
        [ExpectedException(typeof(PositionDoesntExistException))]
        public void GetBackups_Position_Invalid()
        {
            var svc = new DepthChartService();
            svc.AddPlayerToDepthChart("QQ", new Player("Q", 1));
            svc.GetBackups("PP", new Player("Q", 1));
        }

        [TestMethod]
        public void GetBackups_Position_Valid_Player_Invalid()
        {
            var svc = new DepthChartService();
            var playerA = new Player("A", 1);
            svc.AddPlayerToDepthChart("AA", playerA);
            var playerC = new Player("C", 3);
            svc.AddPlayerToDepthChart("AA", playerC);
            var playerB = new Player("B", 2);
            svc.AddPlayerToDepthChart("AA", playerB);
            var playerD = new Player("D", 4);
            svc.AddPlayerToDepthChart("AA", playerD);
            var playerE = new Player("E", 5);
            svc.AddPlayerToDepthChart("AA", playerE, 1);
            var result = svc.GetBackups("AA", new Player("F", 6));
            Assert.AreEqual(0, result.Count);

        }

        [TestMethod]
        public void GetBackups_Position_Valid_Player_Valid()
        {
            var svc = new DepthChartService();
            var playerA = new Player("A", 1);
            svc.AddPlayerToDepthChart("AA", playerA);
            var playerC = new Player("C", 3);
            svc.AddPlayerToDepthChart("AA", playerC);
            var playerB = new Player("B", 2);
            svc.AddPlayerToDepthChart("AA", playerB);
            var playerD = new Player("D", 4);
            svc.AddPlayerToDepthChart("AA", playerD);
            var playerE = new Player("E", 5);
            svc.AddPlayerToDepthChart("AA", playerE, 1);
            var result = svc.GetBackups("AA", playerB);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(playerD, result[0]);
        }
    }
}
