using CodeChallenge.Service.Exceptions;
using CodeChallenge.Service.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Model
{
    [TestClass]
    public class PlayerTests
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidPlayerException))]
        public void Add_Invalid_Player_Bad_Number()
        {
            var player = new Player("A", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPlayerException))]
        public void Add_Invalid_Player_Null_Name()
        {
            var player = new Player(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPlayerException))]
        public void Add_Invalid_Player_Empty_Name()
        {
            var player = new Player(" ", 1);
        }
    }
}
