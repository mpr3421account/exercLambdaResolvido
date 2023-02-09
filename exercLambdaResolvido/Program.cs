/*Fazer um programa para ler um conjunto de produtos a partir de um
arquivo em formato .csv (suponha que exista pelo menos um produto).
Em seguida mostrar o preço médio dos produtos. Depois, mostrar os
nomes, em ordem decrescente, dos produtos que possuem preço
inferior ao preço médio.*/
using System.Globalization;
using exercLambdaResolvido.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter full file path: ");
        string path = Console.ReadLine();

        List<Product> list = new List<Product>();

        using (StreamReader sr = File.OpenText(path))
        {
            while (!sr.EndOfStream)
            {
                string[] fields = sr.ReadLine().Split(',');
                string name = fields[0];
                double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                list.Add(new Product(name, price));
            }
        }
        var avg = list.Select(p => p.Price).DefaultIfEmpty(0.0).Average();
        Console.WriteLine("Average price: " + avg.ToString("F2", CultureInfo.InvariantCulture));
        Console.WriteLine();

        var names = list.Where(p => p.Price < avg).OrderByDescending(p=>p.Name).Select(p=>p.Name);
        foreach(string name in names)
        {
            Console.WriteLine(name);
        }
        
    }
}