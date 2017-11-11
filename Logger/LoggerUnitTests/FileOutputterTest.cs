using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerUnitTests
{
    [TestClass]
    public class FileOutputterTest
    {
        private FileOutputter output;
        [TestInitialize]
        public void TestInit()
        {
            output = new FileOutputter();
        }

        [TestMethod]
        public void GetInstanceTest()
        {
            Assert.IsInstanceOfType(output, typeof(FileOutputter));
        }

        [TestMethod]
        public void GetOutputTypeTest()
        {
            Assert.IsInstanceOfType(output.GetOutputType(), typeof(OutputType));
        }

        [TestMethod]
        public void WriteLogFileOutputterTest()
        {
            //string expected = Level.INFO.ToString() + ": " + "unitTest" + " WHEN: " + DateTime.Now + "\r\n";
            //string actual;

            //FileStream fs = new FileStream("MovieList.txt", FileMode.Append, FileAccess.Write);
            //StreamWriter textWriter = new StreamWriter(fs);


            //output.WriteLog(Level.INFO, "unitTest");

            //actual = textWriter.ToString();

            //Assert.IsTrue(expected.SequenceEqual(actual));
        }


    }
    
            
    
}
