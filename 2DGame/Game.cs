using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace _2DGame
{
    public partial class Game : UserControl
    {
        //Sounds
        SoundPlayer jumpSound = new SoundPlayer(Properties.Resources.jumpSound);
        SoundPlayer deathSound = new SoundPlayer(Properties.Resources.deathSound);
        
        //Create brushes
        SolidBrush groundBrush = new SolidBrush(Color.ForestGreen);
        SolidBrush playerBrush = new SolidBrush(Color.White);
        SolidBrush spikeBrush = new SolidBrush(Color.Red);

        //Ints 
        int jumpTime = 0;
        int leftX, topX, topY;             
        int rightX = 900;
        int spikeTime = 20;
        int spikeCount = 0;
        int speed = 3;
        int spaceMin = 60;
        int spaceMax = 90;
        

        //random to generate height and width of spike
        Random rnd = new Random();

        //Ints to check for key press
        Boolean leftPress, rightPress, spacePress;

        //List for spikes
        List<Spike> spikes = new List<Spike>();

        //create ground and player
        Rectangle ground = new Rectangle(0, 450, 900, 150);
        Rectangle player; 


        public Game()
        {

            InitializeComponent();    
            player = new Rectangle(100, 392, 50, 50);
            Form1.playerScore = 0;

        
        }



        private void playerJump()
        {

            

            if (jumpTime < 18 )
            {
                player.Y -= 5;
                jumpTime++;
            }
            
            else if (jumpTime == 18 && player.Y < 392)
            {
                player.Y += 5;                       
            }

            else if (player.Y == 392)
            {
                jumpTime = 0;
            }
            
        }

        private void playerForward()
        {
            if (player.X < (this.Width - 50))
            {
                player.X += 5;                
            }
            else if (player.X == (this.Width - 50))
            {
                
            }
            
        }

        private void playerBack()
        {
            if(player.X > 50)
            {
                player.X -= 5;
            }
            else if(player.X == 50)
            {

            }
        }

        private void newSpike()
        {
            leftX = rnd.Next(850, 875);
            topX = (leftX + rightX)/2;
            topY = rnd.Next(400, 425);                       
            Spike s = new Spike(leftX, topX, topY);
            spikes.Add(s);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //player controls
            if (spacePress == true)
            {
               
                playerJump();
                
            }

            if (spacePress == false && player.Y < 392)
            {
                player.Y += 5;                
            }

            if (spacePress == false && player.Y == 392)
            {
                jumpTime = 0;
            }
            

            if (rightPress == true)
            {
                playerForward();
            }

            if (leftPress == true)
            {
                playerBack();
            }

            //check collision
            foreach (Spike s in spikes)
            {
                if (s.Collision(player, s) == true)
                {
                    //stop the timer
                    gameTimer.Stop();

                    //Play the death sound
                    deathSound.Play();
                    
                    //Change to endscreen
                    Form f = this.FindForm();
                    EndScreen es = new EndScreen();
                    f.Controls.Add(es);
                    es.Location = new Point((f.Width - es.Width) / 2, (f.Height - es.Height) / 2);
                    f.Controls.Remove(this);
                    

                }
            }


            //Add a new spike 
            spikeCount++;
            
            if (spikeCount == spikeTime)
            {
                newSpike();
                spikeTime = rnd.Next(spaceMin, spaceMax);
                spikeCount = 0;
            }

            //Remove spikes when they go off screen
            if (spikes.Count > 0)
            {
                if (spikes[0].p2X < 0)
                {
                    spikes.RemoveAt(0);
                }
            }

            //Call move function from spike class
            foreach (Spike s in spikes)
            {
                s.move(speed);
            }

            //Count and display score
            Form1.playerScore++;
            score.Text = "Score: " + Form1.playerScore;

            //Increase speed by 1 for every 200 points 
            if (Form1.playerScore > 0 && Form1.playerScore % 300 == 0)
            {
                speed++;
                spaceMin -= 10;
                spaceMax -= 10;
            }

            Refresh();
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.FillRectangle(groundBrush, ground);
            //e.Graphics.FillEllipse(playerBrush, player);
            e.Graphics.DrawImage(Properties.Resources.chicken, player.X, player.Y, 60, 60);

            //Paint spikes
            foreach (Spike s in spikes)
            {
                PointF left = new PointF(s.p1X, 450);
                PointF right = new PointF(s.p2X, 450);
                PointF top = new PointF(s.p3X, s.p3Y);
                PointF[] triangle = { left, right, top }; 
                e.Graphics.FillPolygon(spikeBrush, triangle);
                e.Graphics.DrawRectangle(new Pen(Color.Red), s.p2X, s.p3Y, s.p1X - s.p2X, 450 - s.p3Y);
            }
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPress = false;
                    break;
                case Keys.Right:
                    rightPress = false;
                    break;
                case Keys.Space:
                    spacePress = false;
                    jumpSound.Play();
                    break;                
            }
        }

        private void Game_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPress = true;
                    break;
                case Keys.Right:
                    rightPress = true;
                    break;
                case Keys.Space:
                    spacePress = true;
                    
                    break;
            }
        }
    }
}
