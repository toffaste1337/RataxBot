using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
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
    }


    class IrcClient
    {
        
    }
}
