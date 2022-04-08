using System;
using System.Text;
using System.Text.Encodings;
using System.Text.Encodings.Web;
using System.IO;
using System.Text.Json;



namespace Files
{
    class Program
    {
        static void Main(string[] args)
        {

            var path = @"D:\Учеба ИАТЭ\Berseneva.Files\Berseneva.Files.Exercise2\Table.csv";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding(1251);


            var lines = File.ReadAllLines(path, encoding);
            var persons = new Person[lines.Length - 1];
            for (var i = 1; i < lines.Length; i++)
            {
                var splits = lines[i].Split(';');
                var person = new Person();
                person.Номер = Convert.ToDouble(splits[0]);
                person.Фамилия = splits[1];
                person.Оплата_в_час = Convert.ToDouble(splits[2]);
                person.Отработанное_время = Convert.ToDouble(splits[3]);
                persons[i - 1] = person;
                Console.WriteLine(person);
            }

            var result = "result.csv";
            using StreamWriter streamWriter = new StreamWriter(result, false, encoding); 
            {
                streamWriter.WriteLine($"Фамилия;Номер;Оплата_в_час;Отработанное_время;Зарплата");
                for (int i = 0; i < persons.Length; i++)
                {
                    string stroka = persons[i].ToExcel();
                    streamWriter.WriteLine(stroka);
                }
            }

            var jsonOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Default
            };

            var json = JsonSerializer.Serialize(persons, jsonOptions);
            File.WriteAllText("result.json", json);

            var stringJson = File.ReadAllText("result.json");
            var array = JsonSerializer.Deserialize<Person[]>(stringJson);
        }
    }
    public class Person
    {
        public double Номер { get; set; }
        public string Фамилия { get; set; }
        public double Оплата_в_час { get; set; }
        public double Отработанное_время { get; set; }



        public double Зарплата { get => Оплата_в_час * Отработанное_время; }

        public override string ToString()
        {
            return $"Номер: {Номер} Фамилия: {Фамилия}  Оплата в час: {Оплата_в_час}  Отработанное время:{Отработанное_время}  Зарплата:{Зарплата}";
        }

        public string ToExcel()
        {
            return $"{Номер};{Фамилия};{Оплата_в_час};{Отработанное_время};{Зарплата}";
        }

    }
}
