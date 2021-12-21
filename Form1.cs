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
        List<Particle> particles = new List<Particle>();

        public Form1()
        {
            InitializeComponent();

            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.life -= 1;
                if (particle.life < 0)
                {
                    particle.life = 20 + Particle.Random.Next(100);
                    particle.X = mousePositionX;
                    particle.Y = mousePositionY;
                    particle.direction = Particle.Random.Next(360);
                    particle.speed = 1 + Particle.Random.Next(10);
                    particle.radius = 2 + Particle.Random.Next(10);
                }
                else
                {
                    var directionInRadians = particle.direction / 180 * Math.PI;
                    particle.X += (float)(particle.speed * Math.Cos(directionInRadians));
                    particle.Y -= (float)(particle.speed * Math.Sin(directionInRadians));
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (particles.Count < 500)
                {
                    var particle = new ColorfulParticle();
                    particle.fromColor = Color.Pink;
                    particle.toColor = Color.FromArgb(0, Color.AliceBlue);
                    particle.X = mousePositionX;
                    particle.Y = mousePositionY;
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
            
        }

        private void Render (Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
        }

        private void timer1_tick(object sender, EventArgs e)
        {
            UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.White);
                Render(g);
            }
            picDisplay.Invalidate();
        }
        private int mousePositionX = 0;
        private int mousePositionY = 0;
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            mousePositionX = e.X;
            mousePositionY = e.Y;
        }
    }
}
