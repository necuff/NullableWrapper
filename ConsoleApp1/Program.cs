using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace NullableWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            int? a1 = 2;
            Nullable<int> a2 = 2;
            Nullable<int> a3 = new Nullable<int>(2);

            N2<int> b1 = new N2<int>(2);
            N2<int> b2 = 2; //!!1 ЮХУ!                                    

            var t = b1 + b2;
            var t2 = b1 - b2;
            var t3 = b1 * b2;

            N2<String> c1 = "Алеша";
            N2<String> c2 = "Попович";

            var c = c1 + " " + c2;
            var d = c1 * c2;

            Console.WriteLine(b2);          //implict
            Console.WriteLine((int)b2);     //explict
            Console.WriteLine(t);
            Console.WriteLine(t2);
            Console.WriteLine(t3);
            Console.WriteLine(c);
            Console.WriteLine(d);
            Console.ReadKey();
        }                

    }         
        
    public struct N2<T> //where T : struct
    {
        public T Value { get; set; }

        public N2(T val)
        {
            Value = val;
        }
        
        //Неявные типы
        public static implicit operator N2<T>(T value) 
        {
            return new N2<T>
            {
                Value = value
            };
        }
        public static explicit operator T(N2<T> value) {
            return value.Value;
        }

        //переопределения операторов
        public static N2<T> operator + (N2<T> a, N2<T> b)
        {            
            return (dynamic)a.Value + (dynamic)b.Value; //без dynamic'а ну вообще никак...
        }

        public static N2<T> operator - (N2<T> a, N2<T> b)
        {
            return (dynamic)a.Value - (dynamic)b.Value;
        }

        public static N2<T> operator * (N2<T> a, N2<T> b)
        {            
            if(IsNumeric())
                return (dynamic)a.Value * (dynamic)b.Value;
            else
                return new N2<T> { Value = default(T) };
        }

        public static N2<T> operator /(N2<T> a, N2<T> b)
        {
            if (IsNumeric())
                return (dynamic)a.Value / (dynamic)b.Value;
            else
                return new N2<T> { Value = default(T) };
        }

        public override string ToString()
        {
            if (!(Value is null))
                return Value.ToString();
            else
                return base.ToString();
        }

        //Проверка на числовые типы данных (стандартной проверки нету?)
        private static bool IsNumeric()
        {
            if(typeof(T) == typeof(int) ||
                typeof(T) == typeof(long) ||
                typeof(T) == typeof(float) ||
                typeof(T) == typeof(double))
                return true;
            else return false;
        }
    }        
}
