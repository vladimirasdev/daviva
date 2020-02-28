using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace Uzduotis4
{
    class Program
    {
        static void Main(string[] args)
        {
            string url_1 = "https://backend.daviva.lt/API/GetBrandasFromRRR";
            string url_2 = "https://backend.daviva.lt/public/Markes";
            string json1 = new WebClient().DownloadString(url_1);
            string json2 = new WebClient().DownloadString(url_2);

            UrlModel dataUrl_1 = JsonConvert.DeserializeObject<UrlModel>(json1);
            dynamic dataUrl_2 = JsonConvert.DeserializeObject<dynamic>(json2);

            string[] url1List = new string[dataUrl_1.list.Length];
            string[] url2List = new string[dataUrl_2.Count];
            string[] brandsList = new string[] { };

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{dataUrl_1.list.Length}] [{url_1}]");
            Console.ResetColor();
            int i = 0;
            foreach (var item1 in dataUrl_1.list)
            {
                //Console.Write($"{item1.name}, ");
                url1List[i++] = item1.name.ToString();
                brandsList = brandsList.Concat(new string[] { $"{item1.name}" }).ToArray();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{dataUrl_2.Count}] [{url_2}]");
            Console.ResetColor();
            int j = 0;
            foreach (dynamic item2 in dataUrl_2)
            {
                //Console.Write($"{item2}, ");
                url2List[j++] = item2;
                brandsList = brandsList.Concat(new string[] { $"{item2}" }).ToArray();
            }

            infoMessage($"\n- Sąrašas 1: Aut. markių sąrašas, kurios rastos abiejuose sąrašuose.");
            int m = 0;
            brandsList = Array.ConvertAll(brandsList, d => d.ToLower());
            brandsList = brandsList.Distinct().ToArray();
            brandsList = Array.ConvertAll(brandsList, d => d.Length > 3 ? d = d.Substring(0, 1).ToUpper() + d.Substring(1) : d = d.ToUpper());

            foreach (var list in brandsList)
            {
                Console.Write($"{list}, ");
                m++;
            }
            infoMessage($"\n--- Išviso: [{m}] ---");
            infoMessage($"\n- Sąrašas 2: Aut. markių sąrašas, kur atrinktos markės kurios yra [{url_1}] bet nėra [{url_2}]");
            int k = 0;
            foreach (var s1 in url1List)
            {
                if(!url2List.Contains(s1))
                {
                    Console.Write($"{s1}, ");
                    k++;
                }
            }
            infoMessage($"\n--- Išviso: [{k}] ---");
            infoMessage($"\n- Sąrašas 3: Aut. markių sąrašas, kur atrinktos markės kurios yra [{url_2}] bet nėra [{url_1}]");
            int l = 0;
            foreach (var s2 in url2List)
            {
                if (!url1List.Contains(s2))
                {
                    Console.Write($"{s2}, ");
                    l++;
                }
            }
            infoMessage($"\n--- Išviso: [{l}] ---");

            Console.Read();
        }
        private static void infoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

