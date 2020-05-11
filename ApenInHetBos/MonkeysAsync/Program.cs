using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using MonkeysAsync;

namespace AsyncMonkeys
{
    class Program
    {
        // Globaale configuratie
        public static int max = 1000;
        public static int min = 0;
        public static List<Bos> AlleBossen = new List<Bos>();
        static async Task Main(string[] args)
        {
            doAsync handlerAsync = new doAsync();
            Thread t1 = new Thread(() => LaatHetSpelMaarBeginnen(1,500, handlerAsync));
            t1.Start();
            Thread t2 = new Thread(() => LaatHetSpelMaarBeginnen(2, 499, handlerAsync));
            t2.Start();

            t2.Join();
            t1.Join();

            await AllesOpslaan(AlleBossen);

        }

        public static async void Begin(int _id, int maxAantalBomen,doAsync handlerAsync)
        {

            var r = new Random();
            string[] NamenVanDeAapen = {"Alexis", "Eric", "Xeno", "Louie", "Lucas"};
            Bos NieuwBos = new Bos();
            NieuwBos.id = _id;
            NieuwBos.MinY = min;
            NieuwBos.MaxX = max;
            NieuwBos.MaxY = max;
            NieuwBos.MinX = min;
            var ApenLijst = new List<Aap>();

            var HetGoedeBos = await handlerAsync.GeneertMijnVeld(maxAantalBomen,NieuwBos);

            for (int i = 0; i < 5;i++)
            {
                Aap MijnAap = new Aap();
                MijnAap.id = i;
                MijnAap.Naam = NamenVanDeAapen[i];
                MijnAap.Kleur = (Color.FromArgb(r.Next(256), r.Next(256), r.Next(256)));
                MijnAap.huidigeBoom = HetGoedeBos.bomenList[(r.Next(1, maxAantalBomen))];
                Console.WriteLine($"mijnAap zit op {MijnAap.huidigeBoom.X} {MijnAap.huidigeBoom.Y}");
                ApenLijst.Add(MijnAap);
            }

            Console.WriteLine("oké");

            HetGoedeBos.apenList = ApenLijst;
            AlleBossen.Add(HetGoedeBos);
        }

        public static void LaatHetSpelMaarBeginnen(int id, int maxAantalBomen,doAsync handle)
        {
            var Generatie = Task.Run(() => Begin(id, maxAantalBomen,handle));
            Task.WaitAll(Generatie);

        }
        public async static Task AllesOpslaan(List<Bos> AlleBossen)
        {
            foreach (var bos in AlleBossen)
            {
                await doAsync.laatDeApenOntsnappen(bos);
                await Logger.TekenBos(bos);
            }

            Console.Write("hit 74");
            Console.ReadLine();

        }
    }
}
