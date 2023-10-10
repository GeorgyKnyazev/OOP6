using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace OOP6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Seller seller = new Seller(0);
            Bayer bayer = new Bayer(1000);

            seller.AddProduct(new Product("чай", 10));
            seller.AddProduct(new Product("кофе", 20));
            seller.AddProduct(new Product("хлеб", 30));
            seller.AddProduct(new Product("торт", 40));
            seller.AddProduct(new Product("яблоко", 50));
            seller.AddProduct(new Product("виноград", 60));
            seller.AddProduct(new Product("апельсин", 70));
            seller.AddProduct(new Product("лимон", 80));

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
                        bayer.AddProduct(seller.SellProduct());
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
        public Human(int money)
        {
            Money = money;
        }
        public int Money { get; private set; }
    }

    class Seller : Human
    {
        private List<Product> _products = new List<Product>();
        public Seller(int money) : base(money) { }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void ShowAllProduct()
        {
            Console.Clear();

            foreach (Product product in _products)
            {
                Console.WriteLine(product.Name + " " + product.Price);
            }

            Console.ReadKey();
        }

        public Product SellProduct()
        {
            Product product = null;

            Console.Write("Введите имя товара: ");
            string userInput = Console.ReadLine();

            bool isProductInList = FindProduct(userInput);

            if(isProductInList == true)
            {
                product = ReturnProduct(userInput);
                DeleteProduct(userInput);
            }

            return product;
        }
        private Product ReturnProduct(string userInput)
        {
            Product product = null;

            foreach (Product products in _products)
            {
                if (products.Name == userInput)
                {
                    product = products;
                }
            }
            return product;
        }
        private bool FindProduct(string userInput)
        {
            bool isFind = false;

            foreach (Product product in _products)
            {
                if (product.Name == userInput)
                {
                    isFind = true;
                }
            }

            return isFind;
        }

        private void DeleteProduct(string userInput)
        {
            Product product = null;

            foreach (Product products in _products)
            {
                if (products.Name == userInput)
                {
                    product = products; 
                }
            }

            _products.Remove(product);
        }
    }

    class Bayer : Human
    {
        private List<Product> _products = new List<Product>();

        public Bayer(int money) : base(money) { }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void ShowAllProduct()
        {
            Console.Clear();

            foreach (Product product in _products)
            {
                Console.WriteLine(product.Name + " " + product.Price);
            }

            Console.ReadKey();
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
