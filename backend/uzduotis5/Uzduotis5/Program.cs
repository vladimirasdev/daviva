using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Uzduotis5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Paspauskite bet kuri mygtuką, kad pradėti darbą:");

            string url_1 = "https://backend.daviva.lt/public/Markes";
            string url_2 = "https://backend.daviva.lt/API/GetBrandasFromRRR";
            string url_3 = "https://backend.daviva.lt/public/Modeliai?Name=";
            string url_4 = "https://backend.daviva.lt/API/GetCarModelsFromRRR?BrandID=";

            do
            {
            Start:
                Console.WriteLine("Įveskite ką norite surąsti: \n");
                string search = Console.ReadLine();

                if (!string.IsNullOrEmpty(search))
                {
                    Console.Write($"Įvesta: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{search}\n");
                    Console.ResetColor();
                    
                    stepMessage("Tikriname ar yra 1 sąraše.");

                    string json1 = new WebClient().DownloadString(url_1);
                    dynamic dataUrl_1 = JsonConvert.DeserializeObject<dynamic>(json1);
                    string[] url1List = new string[dataUrl_1.Count];
                    
                    int i = 0;
                    foreach (dynamic item1 in dataUrl_1)
                    {
                        url1List[i++] = item1;
                    }
                    url1List = Array.ConvertAll(url1List, d => d.ToLower());
                    if (!url1List.Contains(search.ToLower())) //Tikrinam ar yra 1 sąraše
                    {
                        errorMessage("Deja, 1 sąraše nieko neradome, patikslinkite paiešką.\n");
                        goto Start;

                    }
                    stepMessage("Tikriname ar yra 2 sąraše.");
                    string[] foundItem = new string[2]; ;
                    bool inList = false;
                    string json2 = new WebClient().DownloadString(url_2);
                    UrlModel dataUrl_2 = JsonConvert.DeserializeObject<UrlModel>(json2);
                    //string[] url2List = new string[dataUrl_2.list.Length];

                    foreach (var item in dataUrl_2.list)
                    {
                        if (search.ToLower() == item.name?.ToString()?.ToLower())  //Tikrinam ar yra 2 sąraše
                        {
                            foundItem[0] = item.id;
                            foundItem[1] = item.name;
                            inList = true;
                        }
                    }
                    if (inList == true)
                    {
                        stepMessage("Paimam markes ID.");
                        stepMessage($"Markes ID: {foundItem[0]}.");

                        string json3 = new WebClient().DownloadString(url_4 + foundItem[0]); // sarasas is API
                        UrlModel2 dataUrl_3 = JsonConvert.DeserializeObject<UrlModel2>(json3);
                        string[] url3List = new string[dataUrl_3.list.Count];
                        string[] comparedList = new string[] { };
                        string[] apiList = new string[] { };
                        string[] publicList = new string[] { };

                        infoMessage($"\n- API list [{dataUrl_3.list.Count}]");
                        int k = 0;
                        foreach (var item2 in dataUrl_3.list)
                        {
                            Console.Write($"{item2.name} | ");
                            url3List[k++] = item2.name.ToString();
                        }

                        string json4 = new WebClient().DownloadString(url_3 + foundItem[1]); // sarasas is Public
                        dynamic dataUrl_4 = JsonConvert.DeserializeObject<dynamic>(json4);
                        string[] url4List = new string[dataUrl_4.Count];

                        infoMessage($"\n- Public list [{dataUrl_4.Count}]");
                        int l = 0;
                        foreach (var item in dataUrl_4)
                        {
                            Console.Write($"{item} | ");
                            url4List[l++] = item;
                        }

                        string url4ListString = String.Join(",", url4List.Select(p => p.ToString()).ToArray());
                        foreach (var s1 in url3List)
                        {
                            string[] split = s1.Split(',');
                            foreach (var sp in split)
                            {
                                if (!url4List.Contains(sp))
                                {
                                    publicList = publicList.Concat(new string[] { $"{sp}" }).ToArray();
                                }
                                else
                                {
                                    comparedList = comparedList.Concat(new string[] { $"{s1}" }).ToArray();
                                }
                            }
                        }

                        string url3ListString = String.Join(",", url3List.Select(p => p.ToString()).ToArray());
                        foreach (var s2 in url4List)
                        {
                            if (!url3ListString.Contains(s2))
                            {
                                apiList = apiList.Concat(new string[] { $"{s2}" }).ToArray();
                            }
                            else
                            {
                                comparedList = comparedList.Concat(new string[] { $"{s2}" }).ToArray();
                            }
                        }

                        infoMessage($"\n\n- Sąrašas 1: Atitinkačių markių sąrašas");
                        comparedList = comparedList.Distinct().ToArray();
                        int m = 0;
                        foreach (var item in comparedList)
                        {
                            Console.Write($"{item} | ");
                            m++;
                        }
                        infoMessage($"\n--- Išviso: [{m}] ---");
                        infoMessage($"\n\n- Sąrašas 2: Markių sąrašas kurių nėra: [{url_4 + foundItem[0]}], bet yra [{url_3 + foundItem[1]}]");

                        int n = 0;
                        //apiList = apiList.Distinct().ToArray();
                        foreach (dynamic item in apiList)
                        {
                            Console.Write($"{item} | ");
                            n++;
                        }
                        infoMessage($"\n--- Išviso: [{n}] ---");
                        infoMessage($"\n- Sąrašas 3: Markių sąrašas kurių nėra: [{url_3 + foundItem[1]}], bet yra [{url_4 + foundItem[0]}]");;

                        int o = 0;
                        //publicList = publicList.Distinct().ToArray();
                        foreach (dynamic item in publicList)
                        {
                            Console.Write($"{item} | ");
                            o++;
                        }
                        
                        infoMessage($"\n--- Išviso: [{o}] ---");
                    }
                    else
                    {
                        errorMessage("Deja, 2 sąraše nieko neradome, patikslinkite paiešką.\n");
                        goto Start;
                    }
                }
                else
                {
                    errorMessage("Klaida! Neįvedete ką norite surasti.\n");
                    goto Start;
                }

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        private static void errorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void stepMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void infoMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
