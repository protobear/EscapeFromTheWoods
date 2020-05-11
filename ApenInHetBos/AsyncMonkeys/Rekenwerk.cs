using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsyncMonkeys
{
    public class Rekenwerk
    {

        public static double afstandNaarDeRand(Bos bos, Boom boom)
        {
            return (new List<double>()
            {
                bos.MaxX - boom.Y,
                bos.MaxX - boom.X,
                boom.Y - bos.MinY,
                boom.X - bos.MinX
            }).Min();

        }
    }
}
