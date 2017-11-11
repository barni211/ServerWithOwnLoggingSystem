using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class FileOutputter : Outputter
    {
        private string documentsPath;
        public FileOutputter()
        {
            documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public Outputter GetInstance()
        {
            return this;
        }

        public OutputType GetOutputType()
        {
            return OutputType.File;
        }

        public void WriteLog(Level lvl, string value)
        {
 
           using (FileStream fs = new FileStream(documentsPath + @"\LoggerOutputText.txt", FileMode.Open, FileSystemRights.AppendData,
           FileShare.Write, 4096, FileOptions.None))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(lvl.ToString()
           + ": " + value + " WHEN: " + DateTime.Now + Environment.NewLine);            
                }
            }

        }

    }
}
