using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Android.Widget;
using Android.App;

namespace ClinicApp.Resources 
{
    public class Server : Activity
    {
        protected string host = "nextrun.mykeenetic.by";
        protected int port = 801;
        protected Android.Content.Context obj;

        public Server() 
        {
        }

        public Server(Android.Content.Context obj) 
        {
            this.obj = obj;
        }
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

        public void sendOrder(int ID, string name, string phone, string birthday, string orderDay, bool first) 
        {
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();
            
            string data = ID + ":" + name + ":" + phone + ":" + birthday + ":" + orderDay + ":" + (first ? "+" : "-") + ":";
            
            sendData(stream, "order");
            getData(stream);
            sendData(stream, Convert.ToString(Encoding.UTF8.GetBytes(data).Length));
            getData(stream);
            sendData(stream, data);

            stream.Close();
            client.Close();
        }

        public List<string> getOrderDate(int docId) 
        {
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            sendData(stream, "orderDate");
            getData(stream);
            sendData(stream, Convert.ToString(docId));
            int lenght = Convert.ToInt32(getData(stream));
            sendData(stream, "OK");
            string data = getData(stream, lenght), temp = "";

            stream.Close();
            client.Close();

            List<string> dates = new List<string>();

            for (int i = 0; i < data.Length; i++) 
            {
                if (data[i] == ',') 
                {
                    dates.Add(temp);
                    temp = "";
                    continue;
                }

                temp += data[i];
            }

            return dates;
        }

        public void sendNote(int ID, string number) 
        {
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            sendData(stream, "send");
            getData(stream);
            sendData(stream, ID + " " + number);

            stream.Close();
            client.Close();
        }

        public List<Data> GetDatas() 
        {
            TcpClient client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();

            sendData(stream, "get");
            int len = Convert.ToInt32(getData(stream));
            string data = getData(stream, len), temp = "";
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
        
        private string getData(NetworkStream stream, int lenght)
        {
            byte[] vs = new byte[lenght + 1];
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