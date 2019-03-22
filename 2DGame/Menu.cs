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
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Form f = this.FindForm();
            Game g = new Game();
            f.Controls.Remove(this);
            f.Controls.Add(g);
            g.Location = new Point((f.Width - g.Width) / 2, (f.Height - g.Height) / 2);
            this.Dispose();
        }
    }
}
