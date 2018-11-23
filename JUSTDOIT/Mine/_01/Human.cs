using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01
{
    class Human
    {
        //       3 pub 3 priv поля ,4 метода ,3 конструктора и вызываю
        // к 1 методу перегрузку
        public int Age;
        public bool Male;
        public string Name;
        private int Old;
        private int Long;
        private char Work;

        public int HowOld()
        {
            return 20;
        }

        public bool Race()
        {
            if (Male==true)

            {
                return true;
            }
            return false;
         }

        private char Longest()
        {
            if (Long>160)
            {
                return 'y';
            }
            else
            {
                return 'n';
            } 
        }
        public void MyName()
        {
            
            Console.WriteLine("Bag");
            
        }
        public void MyName(string Name)
        {
            Console.WriteLine("Имя:", Name);
        }
        public void MyName(string Name,string Familia)
        {
            Console.WriteLine("ФИО:", Name,Familia);
        }

        public Human(int age, bool male, string name)
        {
            Age = age;
            Male = male;
            Name = name;
        }

    }
}
