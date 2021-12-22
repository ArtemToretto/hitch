using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace hitch
{
    public class MrHitch : BaseObject
    {
        Brush HitchBrush = new TextureBrush(Properties.Resources.MrHitch);
        int health;
        public MrHitch(int X, int Y, float Angle) : base(X, Y, Angle)
        {
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(HitchBrush, 0, 0, 37, 37);
        }
    }
}
