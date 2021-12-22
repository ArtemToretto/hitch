using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace hitch
{
    public class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int X;
        public int Y;
        public float direction = 90;
        public int spreading = 5;
        public int speedMin = 10;
        public int speedMax = 10;
        public int radiusMin = 5;
        public int radiusMax = 10;
        public int lifeMin = 50;
        public int lifeMax = 100;
        public Color colorFrom = Color.Red;
        public Color colorTo = Color.FromArgb(0,Color.Yellow);
        public float gravitationX=0;
        public float gravitationY=0;
        public int particleCount = 10;
        public int particlePerTik = 1;

        public void UpdateState()
        {
            int particleToCreate = particlePerTik;
            foreach (var particle in particles)
            {
                particle.life -= 1;
                if (particle.life <= 0)
                {
                    if (particleToCreate>0)
                    {
                        particleToCreate -= 1;
                        resetParticle(particle);
                    }
                }
                else
                {
                    particle.speedX += gravitationX;
                    particle.speedY += gravitationY;
                    particle.X += particle.speedX;
                    particle.Y += particle.speedY;
                }
            }
            while (particleToCreate>=1)
            {
                    particleToCreate -= 1;
                    var particle = CreateParticle();
                    resetParticle(particle);
                    particles.Add(particle);
                
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
            particle.life = Particle.Random.Next(lifeMin,lifeMax);
            particle.X = X;
            particle.Y = Y;
            var Direction = direction + (double)Particle.Random.Next(spreading)-spreading/2;
            var speed = 1 + Particle.Random.Next(speedMin,speedMax);
            particle.speedX = (float)(Math.Cos(Direction / 180 * Math.PI) * speed);
            particle.speedY = -(float)(Math.Sin(Direction / 180 * Math.PI) * speed);
            particle.radius = Particle.Random.Next(radiusMin, radiusMax);
        }

        public virtual Particle CreateParticle()
        {
            var particle = new ColorfulParticle();
            particle.fromColor = colorFrom;
            particle.toColor = colorTo;
            return particle;
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

    public class HitchEmitter : Emitter
    {
        public int Width;
        public override void resetParticle(Particle particle)
        {
            base.resetParticle(particle);
            particle.X = Width;
            particle.Y = 370;
            
        }
    }
}
