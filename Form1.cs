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
        Emitter emitter;

        public Form1()
        {
            InitializeComponent();

            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter = new TopEmitter { Width = picDisplay.Width, gravitationY = 0.25f };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
            }
            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.mousePositionX = e.X;
            emitter.mousePositionY = e.Y;
        }
    }
}
