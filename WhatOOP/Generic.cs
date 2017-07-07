using System;
using System.Collections.Generic;

namespace WhatOOP
{
    /// <summary>
    /// 泛型类
    /// </summary>
    public class Generic
    {
        public class Test<T, TS>
        {
            private T _name;
            private TS _age;


            public Test(T name, TS age)
            {
                this._name = name;
                this._age = age;
            }

            public void ShowInfo()
            {
                Console.WriteLine(string.Format("当前的用户名是:{0}；年龄是：{1}", _name, _age));
            }
        }

        //正确
        public class ChildTest<T, TS> : Test<T, TS>
        {
            public ChildTest(T name, TS age) : base(name, age)
            {
            }
        }

        //如果这样写的话，显然会报找不到类型T,S的错误  
        //public class TestChild : Test<T, S> { }
        ////正确的写法应该是  
        //public class TestChild : Test<string, int> { }
        //public class TestChild<T, S> : Test<T, S> { }
        //public class TestChild<T, S> : Test<String, int> { } 

        public interface IList<T>
        {
            T[] GetElements();
        }

        public interface IDictionary<K, V>
        {
            void Add(K key, V value);
        }

        // 泛型接口的类型参数要么已实例化  
        // 要么来源于实现类声明的类型参数  
        private class Name<T> : IList<T>, IDictionary<int, T>
        {
            public T[] GetElements()
            {
                return null;
            }

            public void Add(int index, T value)
            {
            }
        }
    }
}