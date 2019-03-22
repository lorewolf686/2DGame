using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _2DGame
{
    class Spike
    {

        public int p2X, p3X, p3Y;
        public int p1X = 900;        
                
        public Spike(int _p2X, int _p3X, int _p3Y)
        {
            p2X = _p2X;
            p3X = _p3X;
            p3Y = _p3Y;
        }

        public void move(int speed)
        {
            p1X -= speed;
            p2X -= speed;
            p3X -= speed;
        }

        public bool Collision (Rectangle player, Spike s)
        {
            Rectangle spike = new Rectangle(s.p2X, s.p3Y, s.p1X - p2X, 450 - p3Y);

                        
            if (spike.IntersectsWith(player))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
