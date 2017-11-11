using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Logger;

namespace ServerWithOwnLogger
{
    class Program
    {
        static TcpListener tcpListener = new TcpListener(10);
        static TcpListener listenerForLogger = new TcpListener(1800);
        static Logger.Logger log;
        static void Main(string[] args)
        {
            tcpListener.Start();
            listenerForLogger.Start();
            createLogger();

            Thread threadForLogger = new Thread(changeLoggerLevel);
            threadForLogger.Start();

            log.WriteLog(Level.INFO,"************This is Server program************");
            log.WriteLog(Level.INFO,"************        Welcome!      ************");
            for (;;)
            {
                Socket socketForClient = tcpListener.AcceptSocket();
                Thread thread = new Thread(() => ConnectedClient(socketForClient));
                thread.Start();
            }

        }

        static void changeLoggerLevel()
        {
            for (;;)
            {
                Socket socketForClient = listenerForLogger.AcceptSocket();
                Thread thread = new Thread(() => ConnectedClientToChangeLogger(socketForClient));
                thread.Start();
            }
        }

        static void ConnectedClientToChangeLogger(Socket socketForClient)
        {
            if (socketForClient.Connected)
            {
                log.WriteLog(Level.DEBUG, "Client:" + socketForClient.RemoteEndPoint + " available to change Logger level.");
                NetworkStream networkStream = new NetworkStream(socketForClient);
                System.IO.StreamWriter streamWriter =
                new System.IO.StreamWriter(networkStream);
                System.IO.StreamReader streamReader =
                new System.IO.StreamReader(networkStream);

                //here we recieve client's text if any.
                while (true)
                {
                    string theString = streamReader.ReadLine();
                    switch(theString)
                    {
                        case "INFO":
                            log.SetLoggerLevel(Level.INFO);
                            break;
                        case "DEBUG":
                            log.SetLoggerLevel(Level.DEBUG);
                            break;
                        case "WARN":
                            log.SetLoggerLevel(Level.WARN);
                            break;
                        case "ALERT":
                            log.SetLoggerLevel(Level.ALERT);
                            break;
                        case "ERROR":
                            log.SetLoggerLevel(Level.ERROR);
                            break;
                        case "FATAL":
                            log.SetLoggerLevel(Level.FATAL);
                            break;
                    }
                    log.WriteLog(Level.INFO, "Changed logging level to: " + theString);
                    if (theString == "exit")
                        break;
                }
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
            }
            socketForClient.Close();
            log.WriteLog(Level.INFO, "Press any key to exit from server program");
            Console.ReadKey();
        }

        static void createLogger()
        {
            log = new Logger.Logger();
            Logger.ConsoleOutputter cons = new ConsoleOutputter();
            Logger.FileOutputter file = new FileOutputter();
            log.SetLoggerLevel(Level.FATAL);
            log.SetLoggerOutput(cons);
            log.SetLoggerOutput(file);
        }


        static void ConnectedClient(Socket socketForClient)
        {
            if (socketForClient.Connected)
            {
                log.WriteLog(Level.DEBUG, "Client:" + socketForClient.RemoteEndPoint + " now connected to server.");
                NetworkStream networkStream = new NetworkStream(socketForClient);
                System.IO.StreamWriter streamWriter =
                new System.IO.StreamWriter(networkStream);
                System.IO.StreamReader streamReader =
                new System.IO.StreamReader(networkStream);

                //here we recieve client's text if any.
                while (true)
                {
                    string theString = streamReader.ReadLine();
                    if (theString.Length > 10)
                    {
                        log.WriteLog(Level.ALERT, "Message recieved by client: " + socketForClient.RemoteEndPoint + ": " + theString);
                    }
                    else
                    {
                        log.WriteLog(Level.DEBUG, "Message recieved by client: " + socketForClient.RemoteEndPoint + ": " + theString);
                    }
                    if (theString == "exit")
                        break;
                }
                streamReader.Close();
                networkStream.Close();
                streamWriter.Close();
            }
            socketForClient.Close();
            log.WriteLog(Level.INFO, "Press any key to exit from server program");
            Console.ReadKey();
        }
    }
}
