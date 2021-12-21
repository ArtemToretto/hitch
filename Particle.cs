using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace hitch
{
    class Particle
    {
        public float life;
        public int radius;
        public float X;
        public float Y;
        public float direction; //направление
        public float speed;

        public static Random Random = new Random();

        public Particle()
        {
            radius = 2 + Random.Next(10);
            speed = 1 + Random.Next(10);
            direction = Random.Next(360);
            life = 20 + Random.Next(100);
        }
        public void Draw(Graphics g)
        {
            float k = Math.Min(1f, life / 100);
            int alpha = (int)(k * 255);
            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);
            g.FillEllipse(b, X - radius, Y - radius, radius * 2, radius * 2);
            b.Dispose();
        }
    }
}
