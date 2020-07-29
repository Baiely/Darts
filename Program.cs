using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Globalization;

namespace _1hour
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime localDate = DateTime.Now;
            string fomattedtime = localDate.ToString("HH-mm_dd-MM-yy");
            Print(fomattedtime);
            int type = Convert.ToInt32(GetInfo("Welchen Spielmodus möchtest du spielen [1 = 301 | 2 = 501]: "));
            int ursprung;
            switch (type)
            {
                case 1:
                    ursprung = 301;
                    Print("Du spielst mit 301 Punkten!");
                    Print("");
                    Print("");
                    break;
                case 2:
                    ursprung = 501;
                    Print("Du spielst mit 501 Punkten!");
                    Print("");
                    Print("");
                    break;
                default:
                    ursprung = 301;
                    Print("Du hast irgendetwas anderes eingegeben, deshalb spielst du jetzt mit 301 Punkten!");
                    break;
            }
            Print("");
            Print("");
            Print("");
            int spieler = Convert.ToInt32(GetInfo("Wie viele Personen spielen mit: "));
            string[,] spielerliste = new string[spieler, 2];

            for (int i = 0; i < spieler; i++)
            {
                string name = GetInfo( String.Format("Wie heißt Spieler {0}: ", i + 1) );
                spielerliste[i, 0] = name;
                spielerliste[i, 1] = ToString(ursprung);
            }

            Print("");
            Print("");
            Print("Die Mitspieler");
            Print("");
            Playerlist(spielerliste);
            Print("");
            Print("");
            
            while (CheckWinner(spielerliste) == "")
            {
                int len = spielerliste.Length / 2;
                for (int i = 0; i < len; i++)
                {
                    int score = ToInt(spielerliste[i, 1]);
                    int backup = score;
                    string tempname = spielerliste[i, 0];
                    Print("");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(tempname);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ist jetzt dran!");
                    Print("");
                    Print("");
                    for (int num = 0; num < 3; num++)
                    {
                        int pt = ToInt(GetInfo(String.Format("Wie viele Punkte hat {0} in seinem {1}. Wurf erhalten: ", tempname, num + 1)));
                        score = score - pt;
                        if (score < 0)
                        {
                            Print("No Score! Du hast überworfen. Deine Runde ist beendet!");
                            break;
                        }
                        else if (score == 0)
                        {
                            break;
                        }
                    }
                    if (score < 0)
                    {
                        score = backup;
                    }
                    spielerliste[i, 1] = ToString(score);
                    if (score == 0)
                    {
                        break;
                    }
                    Print("");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Print("Der Zwischenstand lautet:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Playerlist(spielerliste);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Print("");
                    Print("");
                }
                //Print("Der Zwischenstand lautet:");
                //Playerlist(spielerliste);

            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Print( String.Format( "Der Gewinner ist {0}!", CheckWinner( spielerliste ) ) );
            Console.BackgroundColor = ConsoleColor.Black;
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "Baiely Darts");
            string path2 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), "Baiely Darts", fomattedtime + ".csv");
            if (Directory.Exists(path) != true)
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(path2, "Datum:," + localDate + "\n");
            File.AppendAllText(path2, "Spielmodus," + ursprung + "\n");
            File.AppendAllText(path2, "Gewinner:," + CheckWinner(spielerliste) + "\n");
            File.AppendAllText(path2, "\n\n");
            File.AppendAllText(path2, "Ergebnisse:\n");
            File.AppendAllText(path2, "\n\n");
            File.AppendAllText(path2, "Name,Punkte\n");

            for (int nummer = 0; nummer < spieler; nummer++)
            {
                File.AppendAllText(path2, spielerliste[nummer,0] + "," + spielerliste[nummer,1] + "\n");
            }
            //using (StreamWriter outfile = new StreamWriter(@""))
            Console.ReadKey();
        }


        static void Print(string str)
        {
            Console.WriteLine(str);
        }

        static string CheckWinner(string[,] tbl)
        {
            int len = tbl.Length / 2;
            string[] table = new string[2];

            for (int i = 0; i < len; i++)
            {
                if (ToInt(tbl[i,1]) == 0)
                {
                    return tbl[i,0];
                }
            }
            return "";
        }

        static string GetInfo(string str)
        {
            Console.Write(str);
            return Console.ReadLine();
        }

        static void Playerlist(string[,] tbl)
        {
            int wdh = tbl.Length / 2;
            for (int i = 0; i < wdh; i++)
                Console.WriteLine(" Name: {0} | Punkte: {1}", tbl[i, 0], tbl[i, 1]);
                Print("");
        }

        static int ToInt(string str)
        {
            return Convert.ToInt32(str);
        }

        static string ToString(int num)
        {
            return Convert.ToString(num);
        }
    }
}
