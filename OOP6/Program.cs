using System;
using System.Collections.Generic;

namespace OOP6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Seller seller = new Seller(0);
            Bayer bayer = new Bayer(70);

            seller.ProductsAdd();

            const ConsoleKey ShowSellerProduktInMenu = ConsoleKey.D1;
            const ConsoleKey ShowBayerProduktInMenu = ConsoleKey.D2;
            const ConsoleKey BayProduktInMenu = ConsoleKey.D3;
            const ConsoleKey ExitInMenu = ConsoleKey.D4;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"Что бы посмотреть что есть у продавца нажмите: {ShowSellerProduktInMenu}");
                Console.WriteLine($"Что бы посмотреть что есть у покупателя нажмите: {ShowBayerProduktInMenu}");
                Console.WriteLine($"Для покупки товара нажмите: {BayProduktInMenu}");
                Console.WriteLine($"Для выхода нажмите: {ExitInMenu}");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                switch ( consoleKeyInfo.Key)
                {
                    case ShowSellerProduktInMenu:
                        seller.ShowAllProduct();
                        break;
                    case ShowBayerProduktInMenu:
                        bayer.ShowAllProduct();
                        break;
                    case BayProduktInMenu:
                        seller.SellProduct(bayer);
                        break;
                    case ExitInMenu:
                        isWork = false;
                        break;
                }
            }
        }
    }

    class Human
    {
        protected List<Product> _products = new List<Product>();
        protected int _money;

        public Human(int money)
        {
            _money = money;
        }
        public void ShowAllProduct()
        {
            Console.Clear();
            Console.WriteLine("В кошельке:" + _money + "рублей");

            foreach (Product product in _products)
            {
                Console.WriteLine(product.Name + " " + product.Price);
            }

            Console.ReadKey();
        }

        protected void AddProduct(Product product)
        {
            _products.Add(product);
        }

    }

    class Seller : Human
    {
        public Seller(int money) : base(money) { }

        public void SellProduct(Bayer bayer)
        {
            Product product;

            bool isProductOnList = TryGetProduct(out product);

            if(isProductOnList == true)
            {
                if (bayer.CheckSolvency(product.Price))
                {
                    bayer.Bay(product);
                    Sell(product);
                    Console.WriteLine("Товар куплен");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("У клиента не достаточно средств");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Такого продукта в списке нет");
                Console.ReadKey();
            }
        }

        private bool TryGetProduct(out Product product)
        {
            bool isFind = false;
            product = null;

            Console.Write("Введите имя товара: ");
            string userInput = Console.ReadLine();

            foreach(Product products in _products)
            {
                if (products.Name == userInput)
                {
                    product = products;
                    isFind = true;
                }
            }

            return isFind;
        }

        public Product Sell(Product product)
        {
            _money += product.Price;
            _products.Remove(product);
            return product;
        }

        public void ProductsAdd()
        {
            _products.Add(new Product("чай", 10));
            _products.Add(new Product("кофе", 20));
            _products.Add(new Product("хлеб", 30));
            _products.Add(new Product("торт", 40));
            _products.Add(new Product("яблоко", 50));
            _products.Add(new Product("виноград", 60));
            _products.Add(new Product("апельсин", 70));
            _products.Add(new Product("лимон", 80));
        }
    }

    class Bayer : Human
    {
        public Bayer(int money) : base(money) { }

        public bool CheckSolvency(int prise)
        {
            return _money >= prise;
        }

        public void Bay(Product product)
        {
            _money = _money - product.Price;
            _products.Add(product);
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set;}
    }
}
