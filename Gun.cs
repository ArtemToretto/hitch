using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace hitch
{
    public class GunBase : BaseObject
    {
        public GunBase(int X, int Y, float Angle) : base(X, Y, Angle)
        {
        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Brown), -40,-20, 80, 40);
            g.FillEllipse(new SolidBrush(Color.Black), -40, 15, 20, 20);
            g.FillEllipse(new SolidBrush(Color.Black), 20, 15, 20, 20);
        }
    }

    public class Gun : BaseObject
    {
        public Gun(int X, int Y, float Angle) : base(X, Y, Angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), -15, -30, 30, 60);
        }
    }
}
