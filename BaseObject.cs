using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace hitch
{
    public class BaseObject
    {
        public int X;
        public int Y;
        public float Angle;

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
    }
}
