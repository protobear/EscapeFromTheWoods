using System;

using System.Collections.Generic;
using System.Text;

namespace AsyncMonkeys
{
    public class Bos
    {
        public int id { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public List<Boom> bomenList { get; set; } = new List<Boom>();
        public List<Aap> apenList { get; set; } = new List<Aap>();


    }
}
