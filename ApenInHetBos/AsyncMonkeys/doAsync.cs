using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncMonkeys
{

    public class doAsync
    {
        public static int MAX = Int32.MaxValue;

        public async static Task VulhetBosTask(int aantalBomen, Bos bos)
        {
            Random r = new Random();
            int id = 0;
            int goede = 0;
            for (int i = 0; i < aantalBomen; i++)
            {
                while (goede != aantalBomen)
                {
                    
               
                Boom boom = new Boom();
                boom.Y = r.Next(bos.MinY, bos.MaxY);
                boom.X = r.Next(bos.MinX, bos.MaxX);
                
                if (!bos.bomenList.Contains(boom))
                {
                    goede++;
                    bos.bomenList.Add(boom);
                }

                }
            }

    }

        public async static Task laatDeApenOntsnappen(Bos bos)
        {
            //voor het log bestand stappen bijhouden
            int stappen = 0;
            bool alleApenZijnOntsnapt = false;

            while (!alleApenZijnOntsnapt)
            {
                stappen++;
                foreach (var aap in bos.apenList.Where(x => x.huidigeBoom != null))
                {
                    Boom resultaatBoom = KorsteBoomBijAap(bos, aap);
                    if (resultaatBoom != null)
                    {
                        aap.bezochteBomen.Add(resultaatBoom);
                        resultaatBoom.aapinDeBoom = aap;
                    }

                    aap.huidigeBoom.aapinDeBoom = null;
                    aap.huidigeBoom = resultaatBoom;
                }

                alleApenZijnOntsnapt = bos.apenList.All(x => x.huidigeBoom == null);
            }
            //log
        }

        public static Boom KorsteBoomBijAap(Bos bos, Aap aap)
        {
            var alleBomenWaarWeAankunnen = bos.bomenList.Where(x => aap.bezochteBomen.All(y => y.Id != x.Id)).Where(z => z.aapinDeBoom == null).ToList();
            double temp;
            Boom tempBoom = null;
            foreach (var boom in alleBomenWaarWeAankunnen)
            {
                //Console.Write(boom);
                var afstand = boom.afstandTotEenAndereBoom(aap.huidigeBoom);
                if (afstand < MAX)
                {
                    tempBoom = boom;

                    temp = afstand;
                    
                }

            }

            if (Rekenwerk.afstandNaarDeRand(bos, aap.huidigeBoom) <= MAX)
            {
                tempBoom = null;
            }

            return tempBoom;
        }

    }
}
