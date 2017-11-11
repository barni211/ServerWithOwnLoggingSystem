using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger;
using Moq;
using System.IO;
using System.Linq;

namespace LoggerUnitTests
{
    [TestClass]
    public class ConsoleOutputterTest
    {
        private ConsoleOutputter cons;
        [TestInitialize]
        public void TestInit()
        {
            cons = new ConsoleOutputter();
        }

        [TestMethod]
        public void GetInstanceTest()
        {
            Assert.IsInstanceOfType(cons, typeof(ConsoleOutputter));
        }

        [TestMethod]
        public void GetOutputTypeTest()
        {
            Assert.IsInstanceOfType(cons.GetOutputType(), typeof(OutputType));
        }

        [TestMethod]
        public void WriteLogConsoleOutputterTest()
        {
            string expected = Level.INFO.ToString() + ": " + "unitTest" + " WHEN: " + DateTime.Now + "\r\n";
            string actual;

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            cons.WriteLog(Level.INFO, "unitTest");

            actual = stringWriter.ToString();


            //assert
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}
