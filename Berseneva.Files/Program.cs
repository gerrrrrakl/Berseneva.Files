using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Files
{
    class Program
    {
        static void Main(string[] args)
        {


            // Абсолютный путь.
            string path = @"D:\Учеба ИАТЭ\Berseneva.Files\Berseneva.Files\TextFile1.txt";
            // Относительный путь.
            string pathw = @"..\..\..\TextFile1.txt";
            FileInfo info = new FileInfo(path); 
            string Text = File.ReadAllText(path);
            string p = "";
            string[] textMass;
            StreamReader jk = new StreamReader(path);
            while (jk.EndOfStream != true)
            {
                p += jk.ReadLine();
            }
            textMass = p.Split(' ');
            jk.Close();

            var result = "result.txt";

            using (StreamWriter streamWriter = new StreamWriter(result))
            {
                streamWriter.WriteLine("Название файла " + info.Name);
                streamWriter.WriteLine("Абсолютный путь: " + info.FullName);
                streamWriter.WriteLine("Относительный путь: " + pathw);
                streamWriter.WriteLine("Время создания: " + info.CreationTime);
                streamWriter.WriteLine("Размер файла: " + info.Length);
                streamWriter.WriteLine("Общее количество слов: " + textMass.Length);
                streamWriter.WriteLine("Общее количество строк: " + File.ReadAllLines(path).Length);
                streamWriter.WriteLine("Присутствуют ли в тексте цифры: " + Regex.IsMatch(Text, @"[0-9]"));
                streamWriter.WriteLine("Eсть ли в тексте кириллица? " + Regex.IsMatch(Text, @"[А-я]"));
                streamWriter.WriteLine("Eсть ли в тексте латиница? " + Regex.IsMatch(Text, @"[A-z]"));
            }

        }

    }
}