using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
        //1.feladat
    {
        string path = @"C:\Users\info\Documents\Tóth Zóltán\Tóth Zoltán\sütik.txt";

        if (!File.Exists(path))
        {
            Console.WriteLine("file nem talált.");
            return;
        }
        //2.feladat
        string[] lines = File.ReadAllLines(path);

        Console.Write("szorzó (példa: 1.5): ");
        if (!double.TryParse(Console.ReadLine(), out double multiplier))
        {
            Console.WriteLine("nincs ilyen szám.");
            return;
        }
        
        Console.WriteLine("\n--- szorzot recept ---\n");
        //3.feladat
        double minValue = double.MaxValue;
        double maxValue = double.MinValue;
        string minIngredient = "";
        string maxIngredient = "";

        foreach (string line in lines)
        {
            var match = Regex.Match(line, @"(\d+(\.\d+)?)\s*(\w+)");
            //4.feladat
            if (match.Success)
            {
                double originalNumber = double.Parse(match.Groups[1].Value);
                string unit = match.Groups[3].Value;

                double newNumber = originalNumber * multiplier;

                if (unit == "dkg" && newNumber >= 100)
                {
                    newNumber /= 100;
                    unit = "kg";
                }
                else if (unit == "dl" && newNumber >= 10)
                {
                    newNumber /= 10;
                    unit = "l";
                }

                string formattedNumber = newNumber % 1 == 0
                    ? newNumber.ToString("0")
                    : newNumber.ToString("0.##");

                string newLine = Regex.Replace(line, @"(\d+(\.\d+)?)\s*\w+",
                    formattedNumber + " " + unit);

                Console.WriteLine(newLine);

                if (newNumber < minValue)
                {
                    minValue = newNumber;
                    minIngredient = newLine;
                }

                if (newNumber > maxValue)
                {
                    maxValue = newNumber;
                    maxIngredient = newLine;
                }
            }
            else
            {
                Console.WriteLine(line);
            }
        }

        Console.WriteLine("\n");
        Console.WriteLine("legkeveseb érték: " + minIngredient);
        Console.WriteLine("legtöbb érték: " + maxIngredient);

    }
}
// Tóth Zoltán