using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickBreaker
{
    public class PowerUps 
    {
        public int x;
        public int y;
        public string name;

        public PowerUps(int _x, int _y, string _name)
        {
            x = _x;
            y = _y;
            name = _name;
        }

        public void Move ()
        {
            y += 5;
        }

        public bool Collision (Paddle p)
        {
            Rectangle a = new Rectangle(x, y, 40, 40);
            Rectangle b = new Rectangle(p.x, p.y, p.width, p.height);

            if (a.IntersectsWith(b))
            {
                return true;
            }
            return false;

        }
     
    }
    
}
