using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace hitch
{
    public class Gun : BaseObject
    {
        public Gun(int X, int Y, float Angle) : base(X, Y, Angle)
        {
        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Brown), 200, 200, 80, 40);
            g.FillEllipse(new SolidBrush(Color.Red), 180, 180, 20, 20);
            g.FillEllipse(new SolidBrush(Color.Red), 220, 220, 20, 20);
        }
    }
}
