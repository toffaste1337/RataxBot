using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        #region Variabler
        
        NetworkStream serverStream = default(NetworkStream);
        string read_data = "";
        Thread chatThread;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            print("Initialized!");
            
        }

        private void print(string message)
        {
            int maxLines = 24;
            var lines = textBox1.Lines;
            var linesToSkip = lines.Length - maxLines;
            if (linesToSkip > 0)
            {
                var newLines = lines.Skip(linesToSkip);
                textBox1.Lines = newLines.ToArray();
            }
            textBox1.AppendText(message + "\r\n");
        }

        private string test_irc_connection()
        {
            string response = "";
            IrcClient irc = new IrcClient(textBox3.Text, textBox4.Text);
            return response;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)){
                test_irc_connection();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }


    class IrcClient
    {
        public TcpClient tcpClient;
        private StreamReader reader;
        private StreamWriter writer;

        public IrcClient(string username, string oauth)
        {
            string ip = "tmi.twitch.tv";
            int port = 6667;
            tcpClient = new TcpClient(ip, port);
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());

            writer.WriteLine("PASS " + oauth);
            writer.WriteLine("NICK " + username);
            writer.WriteLine("USER " + username + " 8 * :" + username);
            writer.WriteLine("CAP REQ :twitch.tv/membership");
            writer.WriteLine("CAP REQ :twitch.tv/commands");
            writer.Flush();
        }

        public void joinRoom(string channel)
        {
            writer.WriteLine("JOIN #" + channel);
            writer.Flush();
        }

        public void partRoom(string channel)
        {
            writer.WriteLine("PART #" + channel);
            writer.Close();
            reader.Close();
        }

        public void sendIrcMessage(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public void sendChatMessage(string channel, string message)
        {
            string formattedMessage = ":" + username + "!" + username + "@" + username + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message;
            sendIrcMessage(formattedMessage);
        }

        public void PingResponse()
        {
            sendIrcMessage("PONG tmi.twitch.tv\r\n");
        }

        public string[] readMessage()
        {
            string[] message = new string[0];
            message = reader.ReadLine().Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            return message;
        }
    }
}
