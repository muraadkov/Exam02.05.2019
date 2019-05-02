using DbUp;
using Exam.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exam02._05._2019
{
    public class Menu
    {
        InitCity initCity = new InitCity();
        InitCountry initCountry = new InitCountry();
        InitStreet initStreet = new InitStreet();
        CityRepository cityRepository = new CityRepository();
        CountryRepository countryRepository = new CountryRepository();
        StreetRepository streetRepository = new StreetRepository();
        Country country = new Country();
        City city = new City();
        Street street = new Street();
        public void MenuOfDapper()
        {
            var connectionStringConfiguration = ConfigurationManager.ConnectionStrings["appConnection"];
            var connectionString = connectionStringConfiguration.ConnectionString;

            #region Migration
            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

            var result = upgrader.PerformUpgrade();
            if (!result.Successful) throw new Exception("failed");
            #endregion
            while (true)
            {
                Console.WriteLine(
                    "1 - Добавить в таблицу значение" +
                    "\n2 - Обновить в таблице значение" +
                    "\n3 - Удалить в таблице значение" +
                    "\n4 - Выборка");
                if (int.TryParse(Console.ReadLine(), out int resultOfEntryChoose))
                {
                    switch (resultOfEntryChoose)
                    {
                        case 1:
                            Console.WriteLine("В какую таблицу?" +
                                "\n1 - Страна" +
                                "\n2 - Город" +
                                "\n3 - Улица");
                            if(int.TryParse(Console.ReadLine(), out int resultOfChooseTableForAdd))
                            {
                                switch (resultOfChooseTableForAdd)
                                {
                                    case 1:
                                        initCountry.InitCountriesProps(country);
                                        countryRepository.AddTheCountry(connectionString, "insert into Country values(@Name, @Capital, @Population)", country);
                                        break;
                                    case 2:
                                        initCity.InitCitiesProps(city);
                                        cityRepository.AddTheCity(connectionString, "insert into City values(@Name, @Population, @CountryId)", city);
                                        break;
                                    case 3:
                                        initStreet.InitStreetsProps(street);
                                        streetRepository.AddTheStreet(connectionString, "insert into Street values(@Name, @CityId)", street);
                                        break;
                                }
                            }
                            break;
                        case 2:
                            Console.WriteLine("В какой таблице: " +
                                "\n1 - Страна" +
                                "\n2 - Город" +
                                "\n3 - Улица");
                            if(int.TryParse(Console.ReadLine(), out int resultOfChooseTableForUpdate))
                            {
                                switch (resultOfChooseTableForUpdate)
                                {
                                    case 1:
                                        var allCountries = countryRepository.GetAllCountries(connectionString, "select * from Countries");
                                        foreach (var countries in allCountries)
                                        {
                                            Console.WriteLine($"{countries.Id}.{country.Name} - {country.Population} , столица - {country.Capital}");
                                        }
                                        Console.WriteLine("Введите номер страны, которую хотите изменить: ");
                                        if (int.TryParse(Console.ReadLine(), out int idOfCountryByUpdate)) {
                                        Console.WriteLine("Что вы хотите изменить: " +
                                            "\n1 - Название" +
                                            "\n2 - Численность" +
                                            "\n3 - Столицу");
                                        if(int.TryParse(Console.ReadLine(), out int resultOfUpdateCountry))
                                        {
                                            switch (resultOfUpdateCountry)
                                            {
                                                   case 1:
                                                        Console.WriteLine("Введите новое название страны: ");
                                                        string newNameOfCountry = Console.ReadLine();
                                                        countryRepository.UpdateCountries(connectionString, $"update Country SET Name = {newNameOfCountry}"); break;
                                                    case 2:
                                                        Console.WriteLine("Введите новое количество численности: ");
                                                        int newPopulationOfCountry = int.Parse(Console.ReadLine());
                                                        countryRepository.UpdateCountries(connectionString, $"update Country SET Population = {newPopulationOfCountry}"); break;
                                                    case 3:
                                                        Console.WriteLine("Введите новое название столицы этой страны: ");
                                                        string newCapitalOfCountry = Console.ReadLine();
                                                        countryRepository.UpdateCountries(connectionString, $"update Country SET Capital = {newCapitalOfCountry}"); break;
                                                }
                                        }
                                        }
                                        break;
                                    case 2:
                                        var allCities = cityRepository.GetAllCities(connectionString, "select * from City");
                                        foreach (var cities in allCities)
                                        {
                                            Console.WriteLine($"{cities.Id}. {cities.Name} - \nЧисленность: {cities.Population}");
                                        }
                                        Console.WriteLine("Введите номер страны, которую хотите изменить: ");
                                        if (int.TryParse(Console.ReadLine(), out int idOfCityByUpdate))
                                        {
                                            Console.WriteLine("Что вы хотите изменить: " +
                                                "\n1 - Название" +
                                                "\n2 - Численность" +
                                                "\n3 - Страну города");
                                            if (int.TryParse(Console.ReadLine(), out int resultOfUpdateCity))
                                            {
                                                switch (resultOfUpdateCity)
                                                {
                                                    case 1:
                                                        Console.WriteLine("Введите новое название города: ");
                                                        string newNameOfCity = Console.ReadLine();
                                                        cityRepository.UpdateCity(connectionString, $"update City SET Name = {newNameOfCity}"); break;
                                                    case 2:
                                                        Console.WriteLine("Введите новое количество численности: ");
                                                        int newPopulationOfCity = int.Parse(Console.ReadLine());
                                                        cityRepository.UpdateCity(connectionString, $"update City SET Population = {newPopulationOfCity}"); break;
                                                    case 3:
                                                        var allCountriesToCity = countryRepository.GetAllCountries(connectionString, "select * from Countries");
                                                        foreach (var countries in allCountriesToCity)
                                                        {
                                                            Console.WriteLine($"{countries.Id}.{country.Name} - {country.Population} , столица - {country.Capital}");
                                                        }
                                                        Console.WriteLine("Введите идентификатор страны: ");
                                                        int idOfNewCountry = int.Parse(Console.ReadLine());
                                                        cityRepository.UpdateCity(connectionString, $"update City SET CountryId = {idOfNewCountry}");break;
                                                }
                                            }
                                        }
                                            break;
                                    case 3:
                                        var allStreets = streetRepository.GetAllStreets(connectionString, "select * from Street");
                                        foreach(var streets in allStreets)
                                        {
                                            Console.WriteLine($"{streets.Id}. {streets.Name} \nИдентификатор города: {streets.CityId}");
                                        }
                                        Console.WriteLine("Введите номер страны, которую хотите изменить: ");
                                        if (int.TryParse(Console.ReadLine(), out int idOfStreetForUpdate))
                                        {
                                            Console.WriteLine("Что вы хотите изменить: " +
                                                "\n1 - Название" +
                                                "\n2 - Город улицы");
                                            switch (idOfStreetForUpdate)
                                            {
                                                case 1:
                                                    Console.WriteLine("Введите новое название города: ");
                                                    string newNameOfStreet = Console.ReadLine();
                                                    streetRepository.UpdateStreet(connectionString, $"update Street SET Name = {newNameOfStreet}"); break;
                                                case 2:
                                                    var allCitiesToStreet = cityRepository.GetAllCities(connectionString, "select * from City");
                                                    foreach (var cities in allCitiesToStreet)
                                                    {
                                                        Console.WriteLine($"{cities.Id}.{country.Name} \nЧисленность населения: {cities.Population} \nИдентификатор страны: {cities.CountryId}");
                                                    }
                                                    Console.WriteLine("Введите идентификатор города: ");
                                                    int idOfNewCity = int.Parse(Console.ReadLine());
                                                    streetRepository.UpdateStreet(connectionString, $"update Street SET CityId = {idOfNewCity}"); break;
                                            }
                                        }

                                            break;
                                }

                            }
                            break;
                        case 3:
                            Console.WriteLine("В какой таблице: " +
                                "\n1 - Страна" +
                                "\n2 - Город" +
                                "\n3 - Улица");
                            if (int.TryParse(Console.ReadLine(), out int resultOfChooseTableForDelete))
                            {
                                switch (resultOfChooseTableForDelete)
                                {
                                    case 1:
                                        var allCountries = countryRepository.GetAllCountries(connectionString, "select * from Country");
                                        foreach (var countries in allCountries)
                                        {
                                            Console.WriteLine($"{countries.Id}.{country.Name} - {country.Population} , столица - {country.Capital}");
                                        }
                                        Console.WriteLine("Введите идентификатор страны: ");
                                        if(int.TryParse(Console.ReadLine(), out int idOfDeletedCountry))
                                        {
                                            countryRepository.DeleteAllCountries(connectionString, $"delete from Country where Id = {idOfDeletedCountry}");
                                            cityRepository.DeleteAllCities(connectionString, $"delete from City where CountryId = {idOfDeletedCountry}");
                                        }
                                        break;
                                    case 2:
                                        var allCities = cityRepository.GetAllCities(connectionString, "select * from Street");
                                        foreach (var cities in allCities)
                                        {
                                            Console.WriteLine($"{cities.Id}. {cities.Name} - \nЧисленность: {cities.Population}");
                                        }
                                        Console.WriteLine("Введите идентификатор города: ");
                                        if (int.TryParse(Console.ReadLine(), out int idOfDeletedCity))
                                        {
                                            cityRepository.DeleteAllCities(connectionString, $"delete from City where Id = {idOfDeletedCity}");
                                            streetRepository.DeleteAllStreets(connectionString, $"delete from Street where CityId = {idOfDeletedCity}");
                                        }
                                        break;
                                    case 3:
                                        var allStreets = streetRepository.GetAllStreets(connectionString, "select * from Street");
                                        foreach (var streets in allStreets)
                                        {
                                            Console.WriteLine($"{streets.Id}. {streets.Name} \nИдентификатор города: {streets.CityId}");
                                        }
                                        Console.WriteLine("Введите идентификатор улицы: ");
                                        if(int.TryParse(Console.ReadLine(), out int idOfDeletedStreet))
                                        {
                                            streetRepository.DeleteAllStreets(connectionString, $"delete from Street where Id = {idOfDeletedStreet}");
                                        }
                                        break;
                                }
                            }
                            break;
                        case 4:
                            Console.WriteLine("В какой таблице: " +
                                "\n1 - Страна" +
                                "\n2 - Город" +
                                "\n3 - Улица");
                            if (int.TryParse(Console.ReadLine(), out int resultOfChooseTableForSelect))
                            {
                                switch (resultOfChooseTableForSelect)
                                {
                                    case 1:
                                        Console.WriteLine("1 - Вывести все страны");
                                        if(int.TryParse(Console.ReadLine(), out int resultOfChooseSelect)){
                                            var allCountries = countryRepository.GetAllCountries(connectionString, "select * from Country");
                                            foreach (var countries in allCountries)
                                            {
                                                Console.WriteLine($"Id: {countries.Id} \nНазвание: {country.Name} \nЧисленность населения: {country.Population} \nСтолица: {country.Capital}");
                                            }
                                        }
                                        break;
                                    case 2:
                                        Console.WriteLine("1 - Вывести все города." +
                                            "\n2 - Вывести города из какой-либо страны");
                                        if(int.TryParse(Console.ReadLine(), out int resultOfChooseSelectFromCities))
                                        {
                                            switch (resultOfChooseSelectFromCities)
                                            {
                                                case 1:
                                                    var allCities = cityRepository.GetAllCities(connectionString, "select * from Street");
                                                    foreach (var cities in allCities)
                                                    {
                                                        Console.WriteLine($"Id: {cities.Id} Название: {cities.Name}\nЧисленность: {cities.Population}\nИдентификатор страны: {cities.CountryId}");
                                                    }
                                                    break;
                                                case 2:
                                                    var allCountries = countryRepository.GetAllCountries(connectionString, "select * from Country");
                                                    foreach (var countries in allCountries)
                                                    {
                                                        Console.WriteLine($"Id: {countries.Id} \nНазвание: {country.Name} \nЧисленность населения: {country.Population} \nСтолица: {country.Capital}");
                                                    }
                                                    Console.WriteLine("Из какой страны?(Id)");
                                                    if(int.TryParse(Console.ReadLine(), out int idCountry))
                                                    {
                                                        var allCitiesByCountry = cityRepository.GetAllCities(connectionString, $"select * from City where CountryId = {idCountry}");
                                                        foreach (var cities in allCitiesByCountry)
                                                        {
                                                            Console.WriteLine($"Id: {cities.Id}\nНазвание: {cities.Name}\nЧисленность населения: {cities.Population} \nИдентификатор страны: {cities.CountryId}");
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                    case 3:
                                        Console.WriteLine("1 - Вывести все улицы." +
                                           "\n2 - Вывести улицу из какого-либо города");
                                        if(int.TryParse(Console.ReadLine(), out int resultOfSelectByStreet))
                                        {
                                            switch (resultOfSelectByStreet)
                                            {
                                                case 1:
                                                    var allStreets = streetRepository.GetAllStreets(connectionString, "select * from Street");
                                                    foreach (var streets in allStreets)
                                                    {
                                                        Console.WriteLine($"Id: {streets.Id} \nНазвание: {streets.Name} \nИдентификатор города: {streets.CityId}");
                                                    }
                                                    break;
                                                case 2:
                                                    var allCities = cityRepository.GetAllCities(connectionString, "select * from City");
                                                    foreach (var cities in allCities)
                                                    {
                                                        Console.WriteLine($"Id: {cities.Id} \nНазвание: {cities.Name} \nЧисленность: {cities.Population} \nИдентификатор страны: {cities.CountryId}");
                                                    }
                                                    Console.WriteLine("Введите идентификатор города(Id):");
                                                    if(int.TryParse(Console.ReadLine(), out int idCity))
                                                    {
                                                        var allStreetsByCity = streetRepository.GetAllStreets(connectionString, $"select * from Street where CityId = {idCity}");
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }

            }
        }
    }
}
