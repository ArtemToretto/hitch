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
        public int healthPoint;

        public Action<BaseObject> OverlapHitch;

        public virtual void Overlap(BaseObject obj)
        {
            if (this.OverlapHitch!=null)
            {
                this.OverlapHitch(obj);
            }
        }
        public virtual void Boom(Particle particle) { }
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

        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());
            var region = new Region(path1);
            region.Intersect(path2);
            return !region.IsEmpty(g);
        }

        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }
    }
}
