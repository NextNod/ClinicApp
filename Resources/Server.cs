using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Android.Widget;

namespace ClinicApp.Resources
{
    public class Server
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
                string data = getData(stream);

                client.Close();
                stream.Close();

                return Convert.ToInt32(data);
            }
        }

        public void sendNote(int ID) 
        {
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            sendData(stream, "send: " + ID);

            stream.Close();
            client.Close();
        }

        public List<Data> GetDatas() 
        {
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            sendData(stream, "get");
            string data = getData(stream), temp = "";
            int i = 0;
            List<Data> result = new List<Data>();

            client.Close();
            stream.Close();

            while (true) 
            {
                if (data[i] == ' ')
                {
                    Data tempD = new Data();
                    tempD.ID = Convert.ToInt32(temp);
                    temp = "";
                    i++;

                    while (data[i] != ':')
                    {
                        temp += data[i];
                        i++;
                    }

                    tempD.name = temp;
                    temp = "";
                    i++;

                    while (data[i] != ';')
                    {
                        temp += data[i];
                        i++;
                    }

                    tempD.discription = temp;
                    temp = "";
                    result.Add(tempD);
                    i++;
                }
                
                if (data[i] == '.')
                {
                    break;
                }

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
            byte[] vs = new byte[255];
            stream.Read(vs, 0, vs.Length);
            string data = Encoding.UTF8.GetString(vs), rez = "";

            for (int i = 0; data[i] != '\0'; i++) 
            {
                rez += data[i];
            }
            
            return rez;
        }
    }
}