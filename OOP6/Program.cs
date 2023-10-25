using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OOP6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();

            shop.Work();
        }
    }

    class Shop
    {
        private Seller _seller = new Seller(0);
        private Bayer _bayer = new Bayer(70);

        public void Work()
        {
            const ConsoleKey ShowSellerProduktComand = ConsoleKey.D1;
            const ConsoleKey ShowBayerProduktComand = ConsoleKey.D2;
            const ConsoleKey BayProduktComand = ConsoleKey.D3;
            const ConsoleKey ExitoComand = ConsoleKey.D4;

            Start();

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"Что бы посмотреть что есть у продавца нажмите: {ShowSellerProduktComand}");
                Console.WriteLine($"Что бы посмотреть что есть у покупателя нажмите: {ShowBayerProduktComand}");
                Console.WriteLine($"Для покупки товара нажмите: {BayProduktComand}");
                Console.WriteLine($"Для выхода нажмите: {ExitoComand}");
                                
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                switch (consoleKeyInfo.Key)
                {
                    case ShowSellerProduktComand:
                        SellerShowStats();
                        break;

                    case ShowBayerProduktComand:
                        BayerShowStats();
                        break;

                    case BayProduktComand:
                        SellProduct();
                        break;

                    case ExitoComand:
                        isWork = false;
                        break;
                }
            }
        }

        private void Start()
        {
            _seller.ProductsAdd();
        }

        private void BayerShowStats()
        {
            _bayer.ShowAllProduct();
        }

        private void SellerShowStats()
        {
            _seller.ShowAllProduct();
        }

        private void SellProduct()
        {
            Product product;

            bool isProductOnList = _seller.TryGetProduct(out product);

            if (isProductOnList == true)
            {
                if (_bayer.IsEnoughtMoney(product.Price))
                {
                    _bayer.Bay(product);
                    _seller.RemoveProduct(product);
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
    }

    class Human
    {
        protected List<Product> Products = new List<Product>();
        protected int Money;

        public Human(int money)
        {
            Money = money;
        }
        public void ShowAllProduct()
        {
            Console.Clear();
            Console.WriteLine("В кошельке:" + Money + "рублей");

            foreach (Product product in Products)
            {
                Console.WriteLine(product.Name + " " + product.Price);
            }

            Console.ReadKey();
        }    
    }

    class Seller : Human
    {
        public Seller(int money) : base(money) { }

        public bool TryGetProduct(out Product product)
        {
            bool isFind = false;
            product = null;

            Console.Write("Введите имя товара: ");
            string userInput = Console.ReadLine();

            foreach(Product products in Products)
            {
                if (products.Name == userInput)
                {
                    product = products;
                    isFind = true;
                }
            }

            return isFind;
        }

        public void RemoveProduct(Product product)
        {
            Money += product.Price;
            Products.Remove(product);
        }

        public void ProductsAdd()
        {
            Products.Add(new Product("чай", 10));
            Products.Add(new Product("кофе", 20));
            Products.Add(new Product("хлеб", 30));
            Products.Add(new Product("торт", 40));
            Products.Add(new Product("яблоко", 50));
            Products.Add(new Product("виноград", 60));
            Products.Add(new Product("апельсин", 70));
            Products.Add(new Product("лимон", 80));
        }
    }

    class Bayer : Human
    {
        public Bayer(int money) : base(money) { }

        public bool IsEnoughtMoney(int prise)
        {
            return Money >= prise;
        }

        public void Bay(Product product)
        {
            Money -= product.Price;
            AddProduct(product);
        }

        private void AddProduct(Product product)
        {
            Products.Add(product);
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
