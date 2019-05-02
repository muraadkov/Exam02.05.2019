using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam02._05._2019
{
    public class InitCity
    {
        public void InitCitiesProps(City city)
        {
            Console.WriteLine("Введите название города: ");
            city.Name = Console.ReadLine();
            Console.WriteLine("Введите население города: ");
            city.Population = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите идентификатор страны: ");
            city.CountryId = int.Parse(Console.ReadLine());
        }
    }
}
