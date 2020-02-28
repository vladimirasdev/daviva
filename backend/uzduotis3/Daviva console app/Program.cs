using System;

namespace Daviva_console_app
{
    class Program
    {
        static void Main(string[] args)
        {
            DBOldSchool mydb = new DBOldSchool();

            do
            {
                Console.WriteLine("Rasti atitikmenis pagal įvestą tekstą iš kelių žodžių \b(pvz: 'Audi A4 2003' arba 'A4 2005')." +
                                        "\nAtskirus paieškos žodžius kableliu (pvz: 'Audi black, VW diesel') galima ieskoti kelias užklausas.\n" +
                                        "\nĮveskite ką norite surąsti: \n");

                string search = Console.ReadLine();

                if (!string.IsNullOrEmpty(search))
                {

                    var list = mydb.SearchData(search);

                    Console.WriteLine($" --------------- Rezultatai: [ { search } ] išviso surasta: [ { list.Length } ] --------------- \n");

                    foreach (var item in list)
                    {
                        Console.WriteLine($"{item}");
                    }

                    Console.WriteLine($"\n --------------- Rezultatai: [ { search } ] išviso surasta: [ { list.Length } ] --------------- \n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Klaida! Neįvedete ką norite surasti.");
                    Console.ResetColor();
                }

                Console.Write("\n Spauskite (Esc) mygtuką, jei norite užbaigti darbą. (Enter) mygtuką, jei pakartoti \n ");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            //Console.ReadKey();
        }

    }
}
