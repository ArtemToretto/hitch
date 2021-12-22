﻿using System;
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
        List<Emitter> emitters = new List<Emitter>();
        Gun gun;
        GunBase gunBase;
        List<BaseObject> objects = new List<BaseObject>();
        Brush brush = new TextureBrush(Properties.Resources.fon);
        public Form1()
        {
            InitializeComponent();
            gunBase = new GunBase(picDisplay.Width / 2, picDisplay.Height - 35, 0);
            gun = new Gun(picDisplay.Width / 2, picDisplay.Height - 65, 180);
            objects.Add(gunBase);
            objects.Add(gun);
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitters.Add(new HitchEmitter { Width = picDisplay.Width / 2 });
            objects.Add(new MrHitch(picDisplay.Width, 0, 0));
            objects.Add(new MrHitch(picDisplay.Width, 0, 0));
            gunBase.OverlapHitch += (obj) =>
            {
                objects.Add(new MrHitch(picDisplay.Width, 0, 0));
                objects.Remove(obj);
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_tick(object sender, EventArgs e)
        {
            updateMrHitch();
            foreach (Emitter emitter in emitters)
            {
                emitter.UpdateState();
            }
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.FillRectangle(brush, 0, 0, picDisplay.Width, picDisplay.Height);

                foreach (Emitter emitter in emitters)
                {
                    emitter.Render(g);
                }
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
            emitters[0].direction = -Angle+90;
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
        }


    }
}
