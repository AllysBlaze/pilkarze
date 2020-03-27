using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Piłkarze
{
    class Zapis
    {
        public static void DoPliku(string plik,Footballer[] f)
        {
            using (StreamWriter stream = new StreamWriter(plik))
            {
                foreach (var i in f)
                    stream.WriteLine(i.ToFileFormat());
                stream.Close();
            }
        }

        public static Footballer[] ZPliku(string plik)
        {
            Footballer[] f = null;
            if(File.Exists(plik))
            {
                var foot = File.ReadAllLines(plik);
                var dl = foot.Length;
                if (dl>0)
                {
                    f = new Footballer[dl];
                    for (int i = 0; i < dl; i++)
                        f[i] = Footballer.CreateFromString(foot[i]);
                    return f;
                }
                
            }
            return f;
        }
    }

}
