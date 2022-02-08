using MarketLibrary;
using System;
using System.Collections.Generic;

namespace SupermarketConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataPath = @""; // указать полный путь к файлу в котором будут хранится данные
            var departmentsAmount = 7;
            var market = new Market(departmentsAmount);
            if (!market.Load(dataPath))
            {
                var product1 = new Product("Продукт1", 14);
                var product2 = new Product("Продукт2", 88);
                var product3 = new Product("Продукт3", 25);
                var product4 = new Product("Продукт4", 34);
                var product5 = new Product("Продукт5", 47);
                var product6 = new Product("Продукт6", 500);
                var product7 = new Product("Продукт7", 123);
                var product8 = new Product("Продукт8", 77);
                var product9 = new Product("Продукт9", 69);
                var product10 = new Product("Продукт10", 420);

                var department1 = new Department("Мясо");
                var department2 = new Department("Молоко");
                var department3 = new Department("Крупы");
                var department4 = new Department("Химия");
                var department5 = new Department("Рыба");
                var department6 = new Department("Сладости");
                var department7 = new Department("Хлеб");

                department1.AddProduct(product1);
                department1.AddProduct(product2);
                department2.AddProduct(product3);
                department2.AddProduct(product4);
                department3.AddProduct(product5);
                department3.AddProduct(product6);
                department4.AddProduct(product7);
                department4.AddProduct(product8);
                department5.AddProduct(product9);
                department5.AddProduct(product10);
                department6.AddProduct(product1);
                department6.AddProduct(product2);
                department7.AddProduct(product3);
                department7.AddProduct(product4);

                market.PushDepartment(department1);
                market.PushDepartment(department2);
                market.PushDepartment(department3);
                market.PushDepartment(department4);
                market.PushDepartment(department5);
                market.PushDepartment(department6);
                market.PushDepartment(department7);
            }

            for (int i = 0; i < market.DepartmentsCount; i++)
            {
                var currentDepartment = market[i];
                Console.WriteLine($"Отдел: {currentDepartment.Name}\nПродукты:");
                for (int j = 0; j < currentDepartment.CountOfProducts; j++)
                {
                    Console.WriteLine(currentDepartment[j].Product.Name);
                }
                Console.WriteLine();
            }

            market.Save(dataPath);
            Console.ReadKey();
        }
    }
}
