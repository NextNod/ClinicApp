using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ClinicApp.Resources
{
    class Server
    {
        protected string host = "nextrun.mykeenetic.by";
        protected int port = 801;
        public int ver
        {
            get
            {
                TcpClient client = new TcpClient(host, port);
                NetworkStream stream = client.GetStream();

                sendData(stream, "ver");
                return Convert.ToInt32(getData(stream));
            }
        }

        public List<Data> GetDatas() 
        {
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            sendData(stream, "get");
            string data = getData(stream), temp = "";
            int i = 0;
            List<Data> result = new List<Data>();
            
            while (true) 
            {
                if (data[i] == ':') 
                {
                    Data tempD = new Data();
                    tempD.name = temp;
                    temp = "";
                    i++;

                    while (data[i] == ';' || data[i] == '.') 
                    {
                        temp += data[i];
                        i++;
                    }

                    tempD.discription = temp;
                    temp = "";
                    result.Add(tempD);
                }

                if (data[i] == ';')
                    i++;
                else if (data[i] == '.')
                    break;


                temp += data[i];
                i++;
            }

            return result;
        }

        private void sendData(NetworkStream stream, string data)
        {
            byte[] vs = Encoding.UTF8.GetBytes(data);
            stream.Write(vs, 0, vs.Length);
        }

        private string getData(NetworkStream stream)
        {
            Span<byte> vs = new Span<byte>();
            stream.Read(vs);
            return Encoding.UTF8.GetString(vs.ToArray());
        }
    }
}