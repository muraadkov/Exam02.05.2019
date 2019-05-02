using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam02._05._2019
{
    public class InitCountry
    {
        public void InitCountriesProps(Country country)
        {
            Console.WriteLine("Введите название страны: ");
            country.Name = Console.ReadLine();
            Console.WriteLine("Введите столицу: ");
            country.Capital = Console.ReadLine();
            Console.WriteLine("Введите количество населения: ");
            country.Population = int.Parse(Console.ReadLine());
        }
    }
}
