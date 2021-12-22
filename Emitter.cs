using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace hitch
{
    public class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int mousePositionX;
        public int mousePositionY;
        public float gravitationX=0;
        public float gravitationY=1;
        public int particleCount = 1000;

        public void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.life -= 1;
                if (particle.life < 0)
                {
                    resetParticle(particle);
                }
                else
                {
                    particle.speedX += gravitationX;
                    particle.speedY += gravitationY;
                    particle.X += particle.speedX;
                    particle.Y += particle.speedY;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (particles.Count < particleCount)
                {
                    var particle = new ColorfulParticle();
                    particle.fromColor = Color.Pink;
                    particle.toColor = Color.FromArgb(0, Color.Blue);
                    resetParticle(particle);
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }

        }

        public void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
        }

        public virtual void resetParticle(Particle particle)
        {
            particle.life = 20 + Particle.Random.Next(100);
            particle.X = mousePositionX;
            particle.Y = mousePositionY;
            var direction = (double)Particle.Random.Next(360);
            var speed = 1 + Particle.Random.Next(10);
            particle.speedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.speedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
            particle.radius = 2 + Particle.Random.Next(10);
        }
    }


    public class TopEmitter : Emitter
    {
        public int Width;
        public override void resetParticle(Particle particle)
        {
            base.resetParticle(particle);
            particle.X = Particle.Random.Next(Width);
            particle.Y = 0;
            particle.speedX = Particle.Random.Next(-2, 2);
        }
    }
}
