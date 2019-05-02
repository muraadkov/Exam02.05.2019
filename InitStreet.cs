using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam02._05._2019
{
    public class InitStreet
    {
        public void InitStreetsProps(Street street)
        {
            Console.WriteLine("Введите название улицы: ");
            street.Name = Console.ReadLine();
            Console.WriteLine("Введите идентификатор города: ");
            street.CityId = int.Parse(Console.ReadLine());
        }
    }
}
