using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Приложение_Магазин_интерфейс__3_
{

    public enum DevTyp
    {
        LAPTOP,
        TABLET,
        PHONE
    }
    public abstract class Device
    {
        public DevTyp DevTyp { get; set; }
        private string title;
        private string model;
        private string color;
        private int price;
        private int skidka;
        private int kolich;

        public Device(string title, string model, int kolich, int price, int skidka, string color)
        {
            Title = title;
            Model = model;
            Kolich = kolich;
            Price = price;
            Skidka = skidka;
            Color = color;
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public int Kolich
        {
            get { return kolich; }
            set { kolich = value; }
        }
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
        public int Skidka
        {
            get { return skidka; }
            set { skidka = value; }
        }
        public string Color 
        {
            get { return color; }
            set { color = value; }
        }
        public override string ToString()
        {
            return $"Title: {Title}\n" + $"Model: {Model}\n" + $"Kolich: {Kolich}\n" +
                $"Price: {Price}\n" + $"Disc: {Skidka}\n" + $"Color: {Color}\n";

        }
        public abstract void Info();
    }

    public class Laptop : Device
    {
        public string keyboard;

        public Laptop(string title, string model, int kolich, int price, int skidka, string color,string keyboard):base
            (title,model,kolich,price,skidka,color)
        {
            DevTyp=DevTyp.LAPTOP;
            KeyBoard = keyboard;

        }
        public string KeyBoard
        {
            get { return keyboard; }
            set { keyboard = value; }
        }
        public override string ToString()
        {
            return base.ToString() + $"Keyboard: {KeyBoard}\n";
        }
        public override void Info()
        {
            Console.WriteLine(this);
        }
    }
    public class Mobile : Device
    {
        public bool knopka;
        public Mobile(string title, string model, int kolich, int price, int skidka, string color,bool knopka = true) : base
            (title, model, kolich, price, skidka, color)
        {
            DevTyp = DevTyp.PHONE;
            Knopka = knopka;
        }
        public bool Knopka
        {
            get { return knopka; }
            set { knopka = value; }
        }
        public override string ToString()
        {
            if (knopka == true)
            {
                Console.WriteLine("Have buttons of this mobile!");
            }
            else
            {
                Console.WriteLine("Didnt have buttons");
            }
            return base.ToString() + $"Buttons: {Knopka}\n";
        }
        public override void Info()
        {
            Console.WriteLine(this);
        }
    }
    public class Tablet : Device
    {
        public bool supKey;
        public Tablet(string title, string model, int kolich, int price, int skidka, string color, bool supKey = false) : base
            (title, model, kolich, price, skidka, color)
        {
            DevTyp = DevTyp.TABLET;
            SupKey = supKey;
        }
        public bool SupKey
        {
            get { return supKey; }
            set { supKey = value; }
        }
        public override string ToString()
        {
            if (supKey == true)
            {
                Console.WriteLine("This tablet supports keyboards");
            }
            else
            {
                Console.WriteLine("Didnt support keyboards");
            }
            return base.ToString() + $"Support Keyboard: {SupKey}\n";
        }
        public override void Info()
        {
            Console.WriteLine(this);
        }
    }

    public class Shop
    {
        private List<Device> device1;
        private string title;
        private void ShopCop(Shop shop)
        {
            title = shop.title;
            device1 = shop.device1;
        }

        public Shop() : this("Title1")
        {

        }
        public Shop(string title)
        {
            this.title = title;
            device1 = new List<Device>();
        }
        public Shop(List<Device> device1)
        {
            this.device1 = device1;
        } 
        
        public Shop(Shop shop)
        {
            ShopCop(shop);
        }
        public Shop NDevice(Device device)
        {
            device1.Add(device);
            return this;
        }
        public void DelDevice(DevTyp t)
        {
           for(int i=0;i<device1.Count; i++) 
            {
                if (device1[i].DevTyp == t)
                {
                    device1.Remove(device1[i]);
                }
            }
        }
        public List<Device> MaxDevice(int max)
        {
            List<Device> list1 = new List<Device>();
            foreach (var dev in device1)
            {
                if (dev.Price <= max)
                {
                    list1.Add(dev);
                }
            }
            return list1;
        }

        public List<Device> this[DevTyp d]
        {
            get {
                List<Device> list1 = new List<Device>();
                foreach (var dev in device1)
                {
                    if (dev.DevTyp == d)
                    {
                        list1.Add(dev);
                    }
                }
                return list1;
            }
        }
        public override string ToString()
        {
            StringBuilder build1 = new StringBuilder();
            build1.Append($"Title of Shop: {title}\n");
            foreach(var dev in device1)
            {
                build1.Append(dev.ToString() + "\n");
            }
            return build1.ToString();
        }
    }

    internal class Program
    {
        static void Main(string[] args) 
        {

            Shop shop = new Shop("CoolBuy");
            Laptop lap = new Laptop("Asus", "A341", 2, 5000, 10, "Black", "AModelKeyboard");
            Mobile mob = new Mobile("Samsung", "B134", 5, 3000, 0, "Blue", true);
            Tablet tabl = new Tablet("Apple", "C369", 1, 4500, 20, "White", true);


            Console.WriteLine("---Добавлены девайсы---\n");


            shop.NDevice(lap);
            shop.NDevice(mob);
            shop.NDevice(tabl);

            List<Device> list1 = shop.MaxDevice(5000);
            foreach (var dev in list1) 
            {
                Console.WriteLine(dev);
            }
            Console.WriteLine("Все девайсы: \n");
            Console.WriteLine("Phones: \n");
            List<Device> listP = shop[DevTyp.PHONE];
            foreach (var p in listP)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine("Laptops: \n");
            List<Device> listL = shop[DevTyp.LAPTOP];
            foreach (var l in listL)
            {
                Console.WriteLine(l);
            Console.WriteLine("Tablets: \n");
            List<Device> listT = shop[DevTyp.TABLET];
            foreach (var t in listT)
            {
                Console.WriteLine(t);
            }
            }
            Console.WriteLine("Выберите что вы хотите удалить: \n1.Ноутбуки\n2.Телефоны\n3.Планшеты");
            int user = int.Parse(Console.ReadLine());
            if (user == 1)
                shop.DelDevice(DevTyp.LAPTOP);
            else if(user==2)
                shop.DelDevice(DevTyp.PHONE);
            else if(user==3)
                shop.DelDevice(DevTyp.TABLET);
            else
            {
                throw new Exception("Вы неверно ввели! Пункта меньше 1 или больше 3 не существует!\n");
            }
            Console.WriteLine("\n---Обновленный список девайсов---\n");
            Console.WriteLine(shop + "\n");
        }
    }
}
