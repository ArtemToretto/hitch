using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace hitch
{
    public class Particle
    {
        public float life;
        public int radius;
        public float X;
        public float Y;
        public float speedX;
        public float speedY;

        public static Random Random = new Random();

        public Particle()
        {
            radius = 2 + Random.Next(10);
            var speed = 1 + Random.Next(10);
            var direction = Random.Next(360);
            speedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            speedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
            life = 20 + Random.Next(100);
        }
        public virtual void Draw(Graphics g)
        {
            float k = Math.Min(1f, life / 100);
            int alpha = (int)(k * 255);
            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);
            g.FillEllipse(b, X - radius, Y - radius, radius * 2, radius * 2);
            b.Dispose();
        }

        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }
    }

    public class ColorfulParticle : Particle
    {
        public Color fromColor;
        public Color toColor;
        
        public static Color MixColor(Color c1,Color c2, float k)
        {
            
            return Color.FromArgb(
                (int)(c2.A * k + c1.A * (1 - k)),
                (int)(c2.R * k + c1.R * (1 - k)),
                (int)(c2.G * k + c1.G * (1 - k)),
                (int)(c2.B * k + c1.B * (1 - k)));
        }
        public override void Draw(Graphics g)
        {
            float k;
            if (life / 100 > 0)
            {
                k = Math.Min(1f, life / 100);
            }
            else
            {
                k = 0;
            }
            var color = MixColor(toColor, fromColor, k);
            var b = new SolidBrush(color);
            g.FillEllipse(b, X - radius, Y - radius, radius * 2, radius * 2);
            b.Dispose();
        }
    }
}
