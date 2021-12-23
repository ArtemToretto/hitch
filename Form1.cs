using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hitch
{
    public partial class Form1 : Form
    {
        int score = 0;
        Emitter emitter;
        Emitter hitchEmitter;
        Gun gun;
        GunBase gunBase;
        List<BaseObject> objects = new List<BaseObject>();
        Brush brush = new TextureBrush(Properties.Resources.fon);
        public Form1()
        {
            InitializeComponent();
            hitchEmitter = new HitchBloodEmitter { gravitationY = 1, particleCount = 0 };
            gunBase = new GunBase(picDisplay.Width / 2, picDisplay.Height - 35, 0);
            gun = new Gun(picDisplay.Width / 2, picDisplay.Height - 60, 180);
            objects.Add(gunBase);
            objects.Add(gun);
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter = new HitchEmitter{ Width = picDisplay.Width / 2 };
            objects.Add(new MrHitch(picDisplay.Width, 0, 0));
            objects.Add(new MrHitch(picDisplay.Width, 0, 0));
            gun.OverlapHitch += (obj) =>
            {
                foreach (BaseObject o in objects.ToList())
                {
                    if (obj == o)
                    {
                        score -= o.health;
                    }
                }            
                objects.Add(new MrHitch(picDisplay.Width, 0, 0));
                objects.Remove(obj);
            };
            gunBase.OverlapHitch += (obj) =>
            {
                foreach (BaseObject o in objects.ToList())
                {
                    if (obj == o)
                    {
                        score -= o.health;
                    }
                }
                objects.Add(new MrHitch(picDisplay.Width, 0, 0));
                objects.Remove(obj);
            };
            emitter.OverlapWitchHitch += (obj) =>
            {
                foreach (BaseObject o in objects.ToList())
                {
                    if (o == obj)
                    {
                        if (o.health > 0)
                        {
                            o.health--;
                            score++;
                        }
                        else
                        {
                            hitchEmitter = new HitchBloodEmitter
                            {
                                gravitationY = 1,
                                particlePerTik = 5,
                                X = obj.X,
                                Y = obj.Y
                            };
                            objects.Add(new MrHitch(picDisplay.Width, 0, 0));
                            objects.Remove(obj);
                        }
                    }
                }
            };
            emitter.OverlapWithParticle += (p) =>
            {
                emitter.particles.Remove(p);
            };
        }
            

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_tick(object sender, EventArgs e)
        {
            updateMrHitch();
            emitter.UpdateState();
            hitchEmitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.FillRectangle(brush, 0, 0, picDisplay.Width, picDisplay.Height);
                emitter.Render(g);
                hitchEmitter.Render(g);
                foreach (BaseObject obj in objects.ToList())
                {
                    g.Transform = obj.GetTransform();
                    obj.Render(g);
                    foreach (BaseObject obj2 in objects.ToList())
                    {

                        if (obj!=obj2 && obj2 is MrHitch && obj.Overlaps(obj2,g))
                        {
                            obj.Overlap(obj2);
                        }

                        foreach (Particle particle in emitter.particles.ToList())
                        {
                            if (obj is MrHitch && emitter.Overlaps(obj,g,particle))
                            {
                                emitter.Overlap(obj);
                                emitter.OverlapParticle(particle);
                            }
                        }
                    }
                }
            }
            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            float X = e.X - gun.X;
            float Y=e.Y - gun.Y;
            float Angle= 180 - MathF.Atan2(X, Y) * 180 / MathF.PI;
            gun.Angle = Angle;
            emitter.direction = -Angle+90;
        }

        private void updateMrHitch()
        {
            foreach (BaseObject obj in objects)
            {
                if (obj is MrHitch)
                {
                    obj.Y += 0.7f;
                }
            }
            textBox1.Text = $"Счёт: {score}";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            emitter.speedMax = trackBar1.Value;
            emitter.speedMin = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            emitter.radiusMax = trackBar2.Value;
            emitter.radiusMin = trackBar2.Value;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            emitter.particlePerTik = trackBar3.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog()==DialogResult.OK)
            {
                emitter.particles.Clear();
                var color = colorDialog1.Color;
                emitter.colorFrom = color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                emitter.particles.Clear();
                var color = colorDialog1.Color;
                emitter.colorTo = Color.FromArgb(0,color);
            }
        }
    }
}
