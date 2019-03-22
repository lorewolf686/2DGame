using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGame
{
    public partial class Form1 : Form
    {
        //Int for the player's y position 
        
        public static int playerScore;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Menu m = new Menu();
            this.Controls.Add(m);
            m.Location = new Point((this.Width - m.Width) / 2, (this.Height - m.Height) / 2);
        }
    }
}
