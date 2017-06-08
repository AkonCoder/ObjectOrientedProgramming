using System.Runtime.InteropServices;

namespace WhatOOP
{
    /// <summary>
    /// 泛型
    /// </summary>
    public class GenericFunction
    {
        /// <summary>
        /// 限制T的类型为class类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Get<T>(int id, T t)
            where T : class
        {
            return default(T);
        }

        /// <summary>
        /// 限制T的类型为struct类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Get<T>(string id, T t)
            where T : struct
        {
            return default(T);
        }
    }
}