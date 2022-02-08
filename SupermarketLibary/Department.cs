namespace MarketLibrary
{
    public class Department
    {
        private string name; //Название отдела
        private int countOfProducts = 0; //Количество товаров в отделе
        private ListElement root = new ListElement(new Product("Корень", 0)); //Заголовок списка.  Именно сюда добавляются новые товары

        //Название отдела
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //Количество товаров в отделе
        public int CountOfProducts
        {
            get { return countOfProducts; }
        }

        //Общая стоимость продуктов в отделе
        public double TotalProductsCost
        {
            get
            {
                double result = 0;
                for (int i = 0; i < countOfProducts; i++)
                {
                    result += this[i].Product.Cost;
                }
                return result;
            }
        }

        //Конструктор отдела с указанием названия
        public Department(string name)
        {
            Name = name;
            root.Previous = root;
            root.Next = root;
        }

        //Добавление товара в конец списка
        public bool AddProduct(Product product)
        {
            return InsertProduct(countOfProducts, product);
        }

        //Удаление товара по индексу
        public bool RemoveProduct(int index)
        {
            ListElement productToRemove = this[index];
            if (productToRemove == null)
                return false;
            productToRemove.Previous.Next = productToRemove.Next;
            productToRemove.Next.Previous = productToRemove.Previous;
            countOfProducts--;
            return true;
        }

        //Удаление товара по названию 
        public bool RemoveProduct(string name)
        {
            ListElement productToRemove = this[name];
            if (productToRemove == null)
                return false;
            productToRemove.Previous.Next = productToRemove.Next;
            productToRemove.Next.Previous = productToRemove.Previous;
            countOfProducts--;
            return true;
        }

        //Вставка продукта перед указанным индексом
        public bool InsertProduct(int index, Product product)
        {
            if (IndexOfProduct(product) >= 0) //Проверка на уникальность имени товара
                return false;

            ListElement newProduct = new ListElement(product);
            ListElement targetProduct = this[index];

            if (targetProduct == null)
                targetProduct = root;

            newProduct.Next = targetProduct;
            newProduct.Previous = targetProduct.Previous;
            newProduct.Next.Previous = newProduct;
            newProduct.Previous.Next = newProduct;
            countOfProducts++;
            return true;
        }

        //Вставить после или до товара с указанным именем
        //insertAfter - признак того, что надо вставить продукт "после"
        public bool InsertProduct(string productName, Product product, bool insertAfter = false)
        {
            int index = IndexOfProduct(productName); //Поиск по имени
            if (index < 0)
                return false; //Анализ результата поиска
            if (insertAfter)
                index++;
            return InsertProduct(index, product);
        }

        //Получение индекса товара по названию
        public int IndexOfProduct(string name)
        {
            ListElement currentProduct = root.Next;
            for (int index = 0; index < countOfProducts; index++)
            {
                if (currentProduct.Product.Name == name)
                    return index;
                index++;
                currentProduct = currentProduct.Next;
            }
            return -1;
        }

        //Получение индекса товара
        public int IndexOfProduct(Product product)
        {
            return IndexOfProduct(product.Name);
        }

        //Получение товара по его индексу
        public ListElement this[int index]
        {
            get
            {
                //Исправление возможной некорректности вызова
                if (countOfProducts == 0)
                    return null;

                if (index < 0)
                    index = 0;

                if (index >= countOfProducts)
                    index = countOfProducts;

                //Нахождение элемента
                ListElement currentProduct = root.Next;
                for (int i = 0; i < index; i++)
                {
                    currentProduct = currentProduct.Next;
                }
                return currentProduct;
            }
        }

        //Получение товара по названию
        public ListElement this[string name]
        {
            get
            {
                ListElement currentProduct = root;
                for (int i = 0; i < countOfProducts; i++)
                {
                    currentProduct = currentProduct.Next;
                    if (currentProduct.Product.Name == name)
                        return currentProduct;
                }
                return null; 
            }
        }
    }
}
