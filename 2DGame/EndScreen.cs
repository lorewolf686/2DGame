using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGame
{
    public partial class EndScreen : UserControl
    {
        public EndScreen()
        {
            InitializeComponent();
            scoreLabel.Text = "Your Score: " + Form1.playerScore;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();
            Game g = new Game();            
            f.Controls.Add(g);
            f.Controls.Remove(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();
            f.Dispose();
        }
    }
}
