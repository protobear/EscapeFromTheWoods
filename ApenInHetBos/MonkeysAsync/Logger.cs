using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncMonkeys;

namespace MonkeysAsync
{
    public class Logger
    {
        public static StringBuilder GStringBuilder = new StringBuilder();

        public static void NaarBestand(int id, string msg)
        {
            using (StreamWriter bestandStreamWriter = new StreamWriter($@"{id}_logFile.txt", true))
            {
                bestandStreamWriter.WriteLine(msg);
            }
        }

        public static void LogLijn(string message)
        {
            GStringBuilder.AppendLine(message);
        }

        public static void SaveLog(Bos mijnBos)
        {
            string path = $@"OutputLog.txt";
            using (StreamWriter write = new StreamWriter(path))
            {
                write.WriteLine(GStringBuilder.ToString());
            }
        }
    
      public async static Task TekenBos(Bos bos)
        {
            LogLijn($"write bitmap routes wood : {bos.id} - start");
            var pad = GeneerBitMap(bos);
            pad.Save($@"{bos.id}_routes.jpg");


        }
        public static Bitmap GeneerBitMap(Bos Bos)
        {
            Bitmap bm = new Bitmap((Bos.MaxX - Bos.MinX), (Bos.MaxY - Bos.MinY));
            Graphics g = Graphics.FromImage(bm);
            Pen p = new Pen(Color.DarkGreen, 2);
            foreach (var boom in Bos.bomenList)
            {
           //     Console.WriteLine($"teken: {boom.X} {boom.Y}");
                g.DrawEllipse(p, boom.X, boom.Y, 2, 3);
            }
            foreach (var aap in Bos.apenList){


                Pen p1 = new Pen(aap.Kleur, 2);

            for (int i = 0; i < aap.bezochteBomen.Count - 1; i++)
            {

                    g.DrawLine(p1, aap.bezochteBomen[i].X, aap.bezochteBomen[i].Y, aap.bezochteBomen[i + 1].X, aap.bezochteBomen[i + 1].Y);
          


            }
        }
            return bm;
        }


    }
}
