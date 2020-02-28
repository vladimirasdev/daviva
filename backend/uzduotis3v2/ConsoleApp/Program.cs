using BetterConsoleTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleApp.Models
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new appDbContext())
            {
                do
                {
                    Console.WriteLine("Rasti atitikmenis pagal įvestą tekstą iš kelių žodžių \b(pvz: 'Audi A4 2003' arba 'A4 2005')." +
                                        "\nAtskirus paieškos žodžius kableliu (pvz: 'Audi black, VW diesel') galima ieskoti kelias užklausas.\n" +
                                        "\nĮveskite ką norite surąsti: \n");

                    string search = Console.ReadLine();

                    if (!string.IsNullOrEmpty(search))
                    {
                        string queryLine = SearchData(search);
                        var list = context.Automobilis
                            .FromSqlRaw(queryLine)
                            .OrderBy(a => a.ID)
                            .ToList();

                        Console.WriteLine($" --------------- Paieška: [ { search } ] išviso surasta: [ { list.Count } ] --------------- \n");
                        Table table = new Table("#", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S");
                        table.Config = TableConfiguration.Markdown();

                        int num = 1;
                        foreach (var item in list)
                        {
                            table.AddRow(num, item.ID, item.AutoKategorija, item.Spalva, item.Serija, item.VariklioDarbinisTuris, item.Kurotipas, item.AutomobilioPagaminimoMetai, item.KebuloTipas, item.GreiciuDezesTipas, item.PavaruSkaicius, item.PavaruDezesKodas, item.VariklioGalia, item.VaromejiRatai, item.Marke, item.Modelis, item.Variantas, item.MetuIntervalas, item.Tipas);
                            //Console.WriteLine($"{item.ID} {item.AutoKategorija} {item.Spalva} {item.Serija} {item.VariklioDarbinisTuris} {item.Kurotipas} {item.AutomobilioPagaminimoMetai} {item.KebuloTipas} {item.GreiciuDezesTipas} {item.PavaruSkaicius} {item.PavaruDezesKodas} {item.VariklioGalia} {item.VaromejiRatai} {item.Marke} {item.Modelis} {item.Variantas} {item.MetuIntervalas} {item.Tipas}");
                            num++;
                        }
                        Console.Write(table.ToString());
                        Console.WriteLine($"\n --------------- Paieška: [ { search } ] išviso surasta: [ { list.Count } ] ---------------");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Klaida! Neįvedete ką norite surasti.");
                        Console.ResetColor();
                    }

                    Console.Write("\n Spauskite (Esc) mygtuką, jei norite užbaigti darbą. (Enter) mygtuką, jei pakartoti \n ");
                } while (Console.ReadKey().Key != ConsoleKey.Escape);
            }

            string SearchData(string search)
            {
                string[] cols = { "ID", "AutoKategorija", "Spalva", "Serija", "VariklioDarbinisTuris", "Kurotipas","AutomobilioPagaminimoMetai","KebuloTipas", "GreiciuDezesTipas", "PavaruSkaicius", "PavaruDezesKodas","VariklioGalia", "VaromejiRatai", "Marke", "Modelis","Variantas","MetuIntervalas", "Tipas" };

                string colsList = "";
                string whereList = "";
                string[] separate = search.Split(',', '.', ':');
                string[] words = separate[0].Split(' ', '\t');

                for (int i = 0; i < cols.Length; i++)
                {
                    colsList += cols[i] + (i != (cols.Length - 1) ? ", " : "");
                }

                whereList += ($" (CONCAT({ colsList }) LIKE '%{ words[0] }%')");
                if (words.Length > 1)
                {
                    for (int m = 1; m < words.Length; m++)
                    {
                        whereList += ($" AND (CONCAT({ colsList }) LIKE '%{ words[m] }%') ");
                    }
                }

                if (separate.Length > 1)
                {
                    for (int s = 1; s < separate.Length; s++)
                    {
                        string[] sepwords = separate[s].Split(' ', '\t');
                        whereList += ($" OR (CONCAT({ colsList }) LIKE '%{ sepwords[0] }%')");
                        if (sepwords.Length > 1)
                        {
                            for (int sw = 1; sw < sepwords.Length; sw++)
                            {
                                whereList += ($" AND (CONCAT({ colsList }) LIKE '%{ sepwords[sw] }%') ");
                            }
                        }
                    }
                }

                string query = $"SELECT {colsList} FROM `Automobilis` WHERE ( {whereList} )";

                return query;
            }
        }
    }
}
