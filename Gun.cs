using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            g.FillRectangle(new SolidBrush(Color.SaddleBrown), -1000, 32, 2000, 50);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-40, -20, 80, 40));
            path.AddEllipse(-40, 15, 20, 20);
            path.AddEllipse(20, 15, 20, 20);
            path.AddRectangle(new Rectangle(-1000,32,2000,50));
            return path;
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
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-30, -15, 60, 30));
            return path;
        }
    }
}
