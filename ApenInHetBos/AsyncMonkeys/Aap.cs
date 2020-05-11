using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AsyncMonkeys
{
    public class Aap
    {
        public int id { get; set; }
        public string Naam { get; set; }
        public Color Kleur { get; set; }
        public Boom huidigeBoom { get; set; }
        public List<Boom> bezochteBomen { get; set; } = new List<Boom>();
    }
}
