using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace hitch
{
    public class Emitter
    {
        public List<Particle> particles = new List<Particle>();
        public float X;
        public float Y;
        public float direction = 90;
        public int spreading = 5;
        public int speedMin = 4;
        public int speedMax = 4;
        public int radiusMin = 5;
        public int radiusMax = 5;
        public int lifeMin = 50;
        public int lifeMax = 100;
        public Color colorFrom = Color.Red;
        public Color colorTo = Color.FromArgb(0, Color.Yellow);
        public float gravitationX = 0;
        public float gravitationY = 0;
        public int particleCount = 10;
        public int particlePerTik = 1;

        public virtual void UpdateState()
        {
            int particleToCreate = particlePerTik;
            foreach (var particle in particles)
            {
                particle.life -= 1;
                if (particle.life <= 0)
                {
                    if (particleToCreate > 0)
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
            while (particleToCreate >= 1)
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

        public Action<BaseObject> OverlapWitchHitch;
        public Action<Particle> OverlapWithParticle;

        public void OverlapParticle(Particle particle)
        {
            if (this.OverlapWithParticle != null)
            {
                this.OverlapWithParticle(particle);
            }
        }
        public void Overlap(BaseObject obj)
        {
            if (this.OverlapWitchHitch != null)
            {
                this.OverlapWitchHitch(obj);
            }
        }
        public virtual bool Overlaps(BaseObject obj, Graphics g, Particle particle)
        {
            var path1 = GetGraphicsPath(particle);
            var path2 = obj.GetGraphicsPath();
            path1.Transform(GetTransform(particle));
            path2.Transform(obj.GetTransform());
            var region = new Region(path1);
            region.Intersect(path2);
            return !region.IsEmpty(g);
        }
        public Matrix GetTransform(Particle particle)
        {
            var matrix = new Matrix();
            matrix.Translate(particle.X, particle.Y);
            return matrix;
        }
        public GraphicsPath GetGraphicsPath(Particle particle)
        {
            var path = new GraphicsPath();
            path.AddEllipse(0, 0, particle.radius * 2, particle.radius * 2);
            return path;
        }

        public virtual void resetParticle(Particle particle)
        {
            particle.life = Particle.Random.Next(lifeMin, lifeMax);
            particle.X = X;
            particle.Y = Y;
            var Direction = direction + (double)Particle.Random.Next(spreading) - spreading / 2;
            var speed = 1 + Particle.Random.Next(speedMin, speedMax);
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



    public class HitchBloodEmitter : Emitter
    {
        public override void resetParticle(Particle particle)
        {
            base.resetParticle(particle);
            particle.X = X;
            particle.Y = Y;
            particle.speedX = Particle.Random.Next(-2, 2);
            colorFrom = Color.Black;
            colorTo = Color.FromArgb(0, Color.DarkGray);
        }
        public override void UpdateState()
        {
            for (int i = 0; i < particleCount; i++)
            {
                var particle = CreateParticle();
                resetParticle(particle);
                particles.Add(particle);
            }
            particleCount = 0;
            foreach (var particle in particles)
            {
                particle.life -= 1;
                if (particle.life > 0)
                {
                    particle.speedX += gravitationX;
                    particle.speedY += gravitationY;
                    particle.X += particle.speedX;
                    particle.Y += particle.speedY;
                }
            }
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
