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
        Gun gun;
        List<BaseObject> objects = new List<BaseObject>();
        Emitter emitter;
        Brush brush = new TextureBrush(Properties.Resources.fon);
        public Form1()
        {
            InitializeComponent();
            gun = new Gun(picDisplay.Width / 2, picDisplay.Height - 65, 180);
            objects.Add(new GunBase(picDisplay.Width/2, picDisplay.Height-35, 0));
            objects.Add(gun);
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter = new HitchEmitter { Width = picDisplay.Width / 2 };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.FillRectangle(brush, 0, 0, picDisplay.Width, picDisplay.Height);
                emitter.Render(g);
                foreach (BaseObject obj in objects)
                {
                    g.Transform = obj.GetTransform();
                    obj.Render(g);
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
    }
}
