using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MarketLibrary
{
    public class Market
    {
        private Department[] departments; //Массив отделов - кольцевая очередь.
        private int first = -1; //Начало очереди
        private int last = -1; //Конец очереди
        private int departmentsCount = 0; //Количество отделов в очереди

        //Количество отделов
        public int DepartmentsCount
        {
            get { return departmentsCount; }
        }

        //Емкость массива отделов
        public int ArraySize
        {
            get { return departments.Length; }
        }

        //Пустая ли очередь отделов
        public bool IsDepartmentsEmpty
        {
            get { return DepartmentsCount == 0; }
        }

        //Полная ли очередь отеделов
        public bool IsDepartmentsFull
        {
            get { return DepartmentsCount == ArraySize; }
        }

        //Конструктор, возможно, с указанием размера очереди
        public Market(int arraySize = 10)
        {
            departments = new Department[arraySize];
        }

        //Конструктор с указанием пути к файлу с выгруженными данными
        public Market(string FileName, int Capacity = 10) : this(Capacity)
        {
            Load(FileName);
        }

        //Поместить в очередь. Возвращает false, если нет возможности добавить отдел
        public bool PushDepartment(Department department)
        {
            if (IsDepartmentsFull) //Проверка на свободное место
                return false;
            if (this[department.Name] != null) //Проверка на уникальное имя отдела
                return false;
            if (first < 0)
                first = 0;
            last = ++last % ArraySize;
            departments[last] = department;
            departmentsCount++;
            return true;
        }

        //Извелечние отдела из очереди 
        public Department PopDepartment()
        {
            Department department;
            if (IsDepartmentsEmpty)
                return null;
            else
            {
                department = departments[first];
                if (first == last)
                {
                    first = -1;
                    last = -1;
                }
                else
                    first = (first + 1) % ArraySize;
                departmentsCount--;
                return department;
            }
        }

        //Получить отдел по порядковому номеру в очереди
        public Department this[int index]
        {
            get
            {
                return departments[(first + index + ArraySize) % ArraySize];
            }
        }

        //Получить отдел по имени
        public Department this[string name]
        {
            get
            {
                if (departmentsCount == 0)
                    return null;
                for (int k = first < 0 ? 0 : ((first + ArraySize) % ArraySize); k != (last + 1) % ArraySize; k = ++k % ArraySize)
                {
                    if (departments[k].Name == name)
                        return departments[k];
                }
                return null;
            }
        }

        //Общая стоимость товаров со всех отделов
        public double TotalProductsCost
        {
            get
            {
                double result = 0;
                for (int i = 0; i < DepartmentsCount; i++)
                {
                    result += departments[i].TotalProductsCost;
                }
                return result;
            }
        }

        //Сохранение данных
        //В первой строке указывается количество отделов
        //Далее для каждого отдела указывается его имя и перечисляются продукты с их имена и ценой через строку
        public void Save(string fileName)
        {
            //Проверка на наличие файла с данными, при его отсутсвии создается новый в указанном пути
            if (!File.Exists(fileName))
            {
                var dataFile = File.Create(fileName);
                dataFile.Close();
            }
                
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(DepartmentsCount);
            for (int d = 0; d < DepartmentsCount; d++)
            {
                writer.WriteLine(departments[d].Name);
                writer.WriteLine(departments[d].CountOfProducts);
                for (int p = 0; p < departments[d].CountOfProducts; p++)
                {
                    writer.WriteLine(departments[d][p].Product.Name);
                    writer.WriteLine(departments[d][p].Product.Cost);
                }
            }
            writer.Close();
        }

        //Загрузить
        public bool Load(string fileName)
        {
            //Проверка на наличие файла с данными
            if (!File.Exists(fileName))
                return false;

            StreamReader reader = new StreamReader(fileName);
            //Получение количества отделов
            int departmentsCount = int.Parse(reader.ReadLine());
            for (int d = 0; d < departmentsCount; d++)
            {
                Department department = new Department(reader.ReadLine());
                //Получение количества продуктов в текущем отделе
                int productsAmount = int.Parse(reader.ReadLine());
                for (int p = 0; p < productsAmount; p++)
                {
                    Product product = new Product(reader.ReadLine(), double.Parse(reader.ReadLine()));
                    department.AddProduct(product);
                }
                PushDepartment(department);
            }
            reader.Close();
            return true;
        }
    }
}
