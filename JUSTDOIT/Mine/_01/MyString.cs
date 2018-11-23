using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _01
{
    public  class MyString
    {
        /* сначала записываем поля, затем конструкторы, а затем методы
         * переменная это штука опред типа, а каждый класс это тип
         * все поля класса это переменные который хранят в себе значение какого-либо типа
         * в классах можно создавать методы которые : принимают значение, что-то делают с переменными и возвращают или не возвращают (если void)
         * у метода есть модификатор доступа, тип который возвращает метод, в ретурн вызываем только тип метода, после ретурн метод прекращает работу
         * пространство имен ограничивает одни классы от других, чтобы к нему обратиться нужно подключитсья к пространству
          экземпляр тип класса, название, ключ слово, конструктор класса(который задан неявно, те его в коде нет), если без скобок то это тип
          все поля пусты-> обращаемся через . к переменной хран экземпляр 
          нужны значения, инициаоизируем поля, в конструкторе мы производим то что должно исполняться во время создания класса
             */
      
        private char[] str;
        public int Len => str.Length; // длина строки
        //конструкторы:
        public MyString(char[] arr)
        {
            str = new char[arr.Length];
            arr.CopyTo(str, 0);
        }
        public MyString(string arr)
        {
            str = arr.ToCharArray();
        }


        //методы:
      
        public string Compare(MyString input)//ввожу в метод объект класса
        {
            int max = Math.Max(str.Length, input.str.Length);

            for (int i = 0; i < max; i++)
            {
                if (str[i] < input.str[i])
                {
                    return ("Меньше");
                }

                if (str[i] > input.str[i])
                {
                    return ("Больше");
                }
            }

            if (str.Length < input.str.Length)
            {
                return ("Меньше");
            }
            return ("Больше");
        }
        public static string Compare(MyString input, string s2) // сравнение
        {
            int max = Math.Max(s2.Length, input.str.Length);

            for (int i = 0; i < max; i++)
            {
                if (s2[i] < input.str[i])
                {
                    return ("Меньше");
                }

                if (s2[i] > input.str[i])
                {
                    return ("Больше");
                }
            }

            if (s2.Length < input.str.Length)
            {
                return ("Меньше");
            }
            return ("Больше");
        }
        public string Equals(MyString s)
        {
            if (str.Length != s.Len)
            {
                return ("не равны");
            }

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != s.str[i])
                {
                    return ("не равны");
                }
            }
            return ("равны");
        }
        public string Equals(MyString s,string s1)
        {
            if (s1.Length != s.Len)
            {
                return ("не равны");
            }

            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s.str[i])
                {
                    return ("не равны");
                }
            }
            return ("равны");
        }
        public static MyString Concat(MyString s1, MyString s2) // объединение строк
        {
            int _char = 0;
            char[] sum = new char[s1.Len + s2.Len];

            for (int i = 0; i < s1.Len; i++)
            {
                sum[_char] = s1.str[i];
                _char++;
            }

            for (int i = 0; i < s2.Len; i++)
            {
                sum[_char] = s2.str[i];
                _char++;
            }

            return new MyString(sum);
        }
        public static MyString Concat(MyString s1, string s2) // объединение строк
        {
            
            int _char = 0;
            char[] sum = new char[s1.Len + s2.Length];

            for (int i = 0; i < s1.Len; i++)
            {
                sum[_char] = s1.str[i];
                _char++;
            }

            for (int i = 0; i < s2.Length; i++)
            {
                sum[_char] = s2[i];
                _char++;
            }

            return new MyString(sum);
        }
   
        public static MyString Sub(MyString _str, int _a) // извлекает из строки подстроку
        {
            int len = _str.Len - _a;

            char[] result = new char[len];
            
            int j = 0;
            for (int i = _a; i < _str.Len; i++)
            {
                result[j] = _str.str[i];
                j++;
            }
            
            return new MyString(result);
        }
        public static MyString Sub(MyString _str, int a, int b) // извлекает из строки подстроку
        {
            int len = _str.Len;

            if (b > 0)
            {
                if (a + b < len)
                    len = a + b;
            }
            else
                len = len + b;

            char[] result = new char[_str.Len - a + 1];
            int j = 0;
            for (int i = a; i < _str.Len; i++)
            {
                result[j] = _str.str[i];
                j++;
            }
            return new MyString(result);
        }
        /*в этом и суть. Метод мне должен возвращать число, но что если мне нужно при каких то обстоятельствах возвратить просто нулл, то есть как здесь например, 
         * метод сначала ищет символ в массиве, если он его находит - возвращает индекс элемента. 
         * А если не находит? Что делать? Если я верну 0, это может восприниматься как индекс, потому что индексация начинается с нуля.
         * Поэтому я добавил оператор ?, который позволяет методу не объектного значения возвращать null*/
        //использую Nullable<T> или int? , т.к. они взаимозаменяемы
       public int? IndexOf(char symb)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == symb) return i;
            }

            return null;
        }
        
        public int? IndexOf(char symb,int startNumber)
        {
            for (int i = startNumber; i < str.Length; i++)
            {
                if (str[i] == symb) return i;
            }

            return null;
        }
        //перегрузки операторов

        public static string operator ==(MyString s1, MyString s2)
        {
            return (s1.Equals(s2));
        }

        public static string operator !=(MyString s1, MyString s2)
        {
            return (s1.Equals(s2));
        }

        public static string operator <(MyString s1, MyString s2)
        {
            return Compare(s1, s2);
        }
        public static bool operator <(MyString s1, char[] s2) => s1.ToInt32() < s2.Length;
        public static string operator >(MyString s1, MyString s2)
        {
            return Compare(s1, s2);
        }
        public static bool operator >(MyString s1, char[] s2) => s1.ToInt32() > s2.Length;
        public int ToInt32() => str.Length;
        public static MyString operator +(MyString s1, MyString s2)
        {
            s1 = MyString.Concat(s1, s2);
            return s1;
        }
        public static MyString operator +(MyString s1, string s2)
        {
            MyString.Concat(s1, s2);
            return s1;
        }


        public static implicit operator string (MyString owner)//владелец;
        {
      
            return new string(owner.str);
        }
        public static explicit operator int(MyString owner)

        {
            return owner.ToInt32();
        }

    }
}


