using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace RJUY
{
    internal class Program
    {
        static void Main(string[] args)
        {

            double a = 0;
            var prov = false;
            var curs = 25;
            var Blrub1 = 0d;
            double proc = 0.2, Blrubproc = 0;
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .Enrich.WithProperty("Курс белорусского рубля:", curs)
                        .WriteTo.Seq("http://localhost:5341/", apiKey: "PPxXsleGPUhRB6BHz72B")
                        .CreateLogger();
            while (!prov || a < 1)
            {
                Console.Write("Напишите сумму перевода из рублей в белорусские рубли: ");
                var dol = Console.ReadLine();
                prov = double.TryParse(dol, out a);
                if (!prov || a < 1)
                {
                    Log.Error("Введено некорректное значение");
                }
            }
            Log.Information($"Введено верное значение: {a}");
            Blrub1 = a * curs;
            if (a <= 500)
            {
                Console.WriteLine("Сумма перевода меньше 500Р ");
                Console.WriteLine("Комиссия 25 б.р.");
                Console.WriteLine("Итог перевода:" + (Blrub1 - 25));
            }
            else
            {
                Blrubproc = Blrub1 * (proc / 100);
                Console.WriteLine("Сумма перевода больше 500Р ");
                Console.WriteLine($"Процент комиссии : {proc} % =  {Blrubproc}");
                Console.Write("Итог при переводе в белорусских рублях :");
                Blrub1 = Blrub1 - Blrubproc;
                Console.WriteLine(Blrub1);
            }
            Log.Information($"Перевод в белорусские рубли прошел успешно, выдано:{Blrub1}");
            Log.CloseAndFlush();
            Console.ReadKey();
        }
    }
}