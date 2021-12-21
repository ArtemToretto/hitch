using System;
using System.Collections.Generic;
using System.Text;

namespace hitch
{
    class Particle
    {
        public int radius;
        public float X;
        public float Y;
        public float direction; //направление
        public float speed;

        public static Random Random = new Random();

        public Particle()
        {
            radius = 2 + Random.Next(10);
            speed = 1 + Random.Next(10);
            direction = Random.Next(360);
        }
    }
}
