using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class SocketOutputter : Outputter
    {
        private Socket socket;
        private IPAddress ipAdd;
        private IPEndPoint remoteEP;
        public SocketOutputter(string m_ip, int m_port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipAdd = System.Net.IPAddress.Parse(m_ip);
            remoteEP = new IPEndPoint(ipAdd, m_port);
        }

        public Outputter GetInstance()
        {
            return this;
        }

        public OutputType GetOutputType()
        {
            return OutputType.Socket;
        }

        public void WriteLog(Level lvl, string value)
        {
            socket.Connect(remoteEP);

            byte[] byData = System.Text.Encoding.ASCII.GetBytes(lvl.ToString() + ": " + value + " WHEN: " + DateTime.Now);
            socket.Send(byData);

            socket.Disconnect(false);
            socket.Close();
        }
    }
}
