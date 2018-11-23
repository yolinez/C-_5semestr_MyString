using System;
using _01;

class Program
{
   static void Main(string[] args)
   {
        //вызываем метод у объекта s1 класса
     Human test = new Human(43, false, "Вася");
     
        test.Name = "Алеша";

        Console.WriteLine("Вы мужчина?:"+ test.Race()+","+ test.HowOld());
        Console.WriteLine("Вас зовут:"+ test.Name);
        //MyString ToChar = new MyString("Hollywood");
        //ToChar.str = 'g'; недоступен из-за его уровня защиты
        MyString s1 = new MyString("Holly");
        MyString s2 = new MyString("wood");
        Console.WriteLine(s1.IndexOf('o'));
        Console.WriteLine(s2 > s1);
        Console.WriteLine(s1.Equals(s2));
        Console.WriteLine(s1+s2);
        Console.ReadKey();
   }
}

