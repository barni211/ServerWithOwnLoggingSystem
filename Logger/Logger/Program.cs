using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleOutputter cons = new ConsoleOutputter();
            FileOutputter file = new FileOutputter();
            Logger log = new Logger();
            log.SetLoggerLevel(Level.DEBUG);
            log.SetLoggerOutput(cons);
            log.SetLoggerOutput(cons);
            log.SetLoggerOutput(cons);
            log.SetLoggerOutput(file);

            log.WriteLog(Level.INFO, "Logowanie ");
            log.WriteLog(Level.DEBUG, "Logowanie2 ");

            log.DeleteLoggerOutput(cons);

            log.WriteLog(Level.INFO, "Logowanie po usunięciu ");
            log.WriteLog(Level.DEBUG, "Logowanie2 po usunięciu ");

            Console.Read();
        }
    }
}
