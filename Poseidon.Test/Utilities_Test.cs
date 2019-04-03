using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poseidon.Misc;

namespace Poseidon.Test
{
    [TestClass]
    public class Utilities_Test
    {
        [TestMethod]
        public void CheckNetworkConnectionTest()
        {
            Assert.IsTrue(Utilities.CheckNetworkConnection());
        }

        [TestMethod]
        public void CheckGenerateGuid()
        {
            int num = 10000;
            Dictionary<string, int> checker = new Dictionary<string, int>();
            List<string> ids = new List<string>();

            for (int i = 0; i < num; i++)
            {
                string id = Utilities.GenerateGUID();
                ids.Add(id);
            }

            foreach (var id in ids)
            {
                if (checker.ContainsKey(id)) Assert.Fail();
                checker.Add(id, 1);
            }
        }
    }
}