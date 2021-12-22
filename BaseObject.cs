using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace hitch
{
    public class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;
        public int health;

        public BaseObject(int X, int Y, float Angle)
        {
            this.X = X;
            this.Y = Y;
            this.Angle = Angle;
        }

        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);
            return matrix;
        }

        public virtual void Render(Graphics g)
        { }
        public virtual void Update(Graphics g, MrHitch mrHitch) { }
        public virtual int getHealths()
        {
            return 0;
        }

        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }
    }
}
