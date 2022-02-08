using System;
using System.Collections.Generic;
using System.Text;

namespace MarketLibrary
{
    public class Product
    {
        string name; //Название
        double cost; //Стоимость

        //Название
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //Стоимость
        public double Cost
        {
            get { return cost; }
            set
            {
                if (value >= 0) //Проверка на положительное число
                    cost = value;
            }
        }

        //Конструктор продукта c указанием имени и цены
        public Product(string name, double cost)
        {
            Name = name;
            Cost = cost;
        }

        //Конструктор без параметров
        public Product() : this("", 0)
        {

        }

        //Операторы сравнения двух продуктов по имени
        public static bool operator ==(Product product1, Product product2)
        {
            return product1.Name == product2.Name;
        }

        public static bool operator !=(Product product1, Product product2)
        {
            return product1.Name != product2.Name;
        }

        //Необходимы для реализации операторов
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
