using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonkeysAsync;

namespace AsyncMonkeys
{

    public class doAsync
    {
        public static int MAX = Int32.MaxValue;

        public async Task<Bos> GeneertMijnVeld(int aantalBomen, Bos bos)
        {
            Random r = new Random();
            int id = 0;
            int goede = 0;

                while (goede != aantalBomen)
                {
                    
                Boom boom = new Boom();
                boom.Id = goede;
                boom.Y = r.Next(bos.MinY, bos.MaxY);
                boom.X = r.Next(bos.MinX, bos.MaxX);
                
                if (!bos.bomenList.Contains(boom))
                {
                    goede++;
                    bos.bomenList.Add(boom);
                }

                }
            Console.WriteLine($"GeneertMijnVeld: {bos.bomenList.Count}");
            return bos;

        }

        public async static Task laatDeApenOntsnappen(Bos bos)
        {
            //voor het log bestand stappen bijhouden
            int stap = 0;
            bool alleApenZijnOntsnapt = false;

            Console.WriteLine("3");
            while (alleApenZijnOntsnapt == false)
            {
                Console.WriteLine("4");
                stap++;
                foreach (var aap in bos.apenList.Where(x => x.huidigeBoom != null))
                { 
                    Console.WriteLine($"{aap.Naam} HIER");
                    Boom resultaatBoom = KorsteBoomBijAap(bos, aap);
                    if (resultaatBoom != null)
                    {
                        aap.bezochteBomen.Add(resultaatBoom);
                        resultaatBoom.aapinDeBoom = aap;
                    }
                    Console.WriteLine($"map:{bos.id} {aap.Naam} step: {stap} jumped from tree {aap.huidigeBoom.Id}");
                    Logger.NaarBestand(bos.id, $"step:{stap} aap={aap.Naam} is nu in '{aap.huidigeBoom.X} {aap.huidigeBoom.Y}");
                    if (aap.huidigeBoom != null)
                    {
                        Logger.LogLijn($"{bos.id} {aap.Naam} is ontsnapt in {stap}");
                    }
                    else
                    {
                        Logger.LogLijn($"{bos.id} {aap.Naam} is gesprogen naar  {aap.huidigeBoom.X} {aap.huidigeBoom.Y}");
                    }
                    aap.huidigeBoom.aapinDeBoom = null;
                    aap.huidigeBoom = resultaatBoom;
                }

                alleApenZijnOntsnapt = bos.apenList.All(x => x.huidigeBoom == null);
            }
            //log
            Console.WriteLine($"iedereen op {bos.id} is ontsnapt");
        }

        public static Boom KorsteBoomBijAap(Bos bos, Aap aap)
        {
            Console.WriteLine("5");
            var alleBomenWaarWeAankunnen = bos.bomenList.Where(x => aap.bezochteBomen.All(y => y.Id != x.Id))
                .Where(z => z.aapinDeBoom == null).ToList();
            double temp;
            Boom tempBoom = null;
            foreach (var boom in alleBomenWaarWeAankunnen)
            {
                Console.WriteLine("lane");
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
