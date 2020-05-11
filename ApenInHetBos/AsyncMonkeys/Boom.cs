using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncMonkeys
{
    public class Boom
    {


        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int Radius { get; set; } = 10;
        public Aap aapinDeBoom { get; set; }


        public bool Overlaps(Boom other)
        {
            double som = Radius + other.Radius;
            double mid = afstandTotEenAndereBoom(other);
            return (som >= mid);
        }

        public int CenterX()
        {
            var centerX = X + Radius / 2;
            return centerX;
        }

        public int CenterY()
        {
            var centerY = Y + Radius / 2;
            return centerY;
        }

        public double afstandTotEenAndereBoom(Boom other)
        {
            double lengteTussenTweePunten = CenterX() - other.CenterX();
            double hoogteTussenTweePunten = CenterY() - other.CenterY();
            double afstandMiddelpunten =
                Math.Sqrt(Math.Pow(lengteTussenTweePunten, 2) + Math.Pow(hoogteTussenTweePunten, 2));
            return afstandMiddelpunten;
        }


    }
}
