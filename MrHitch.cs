﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace hitch
{
    public class MrHitch : BaseObject
    {
        Brush HitchBrush = new TextureBrush(Properties.Resources.MrHitch);
        Random Random = new Random();
        public MrHitch(int X, int Y, float Angle) : base(X, Y, Angle)
        {
            this.Y = 0;
            this.X = Random.Next(40, X-40);
            this.Angle = 0;
            health = Random.Next(100, 500);
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(HitchBrush, 0, 0, 75, 75);
            g.DrawString(
            $"{health}",
            new Font("Verdana", 10),
            new SolidBrush(Color.Black),
            22,77);
        }
        public override void Update(Graphics g, MrHitch mrHitch)
        {
            mrHitch.X=Random.Next(40, (int)X - 40);
            mrHitch.Y = 0;
            mrHitch.health = Random.Next(100, 500);
        }
    }
}
