using System;
using System.Collections.Generic;
using System.Text;

namespace MarketLibrary
{
    public class ListElement
    {
        Product product; //Значение элемента списка
        ListElement previous; //Значение стоящее до текущего элемента
        ListElement next; //Значение стоящее после текущего элемента

        public ListElement()
        {
            previous = next = this;
            product = null;
        }

        //Конструктор с указанием товара
        public ListElement(Product product) : this()
        {
            this.product = product;
        }

        //Значение элемента
        public Product Product
        {
            get { return product; }
        }

        //Значение стоящее до текущего элемента
        public ListElement Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        //Значение стоящее после текущего элемента
        public ListElement Next
        {
            get { return next; }
            set { next = value; }
        }
    }
}
