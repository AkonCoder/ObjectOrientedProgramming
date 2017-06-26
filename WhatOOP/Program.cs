using System;
using System.Collections.Generic;
using System.Linq;
using Baidu.Aip.Face;
using Baidu.Aip.Nlp;
using MySoft.Logger;
using Newtonsoft.Json;

namespace WhatOOP
{
    internal class Program
    {
        public static readonly string AppKey = "8SdnNXeQ90gUDHQL3OeRu38k";

        public static readonly string AppSecret = "VNChhyxX6EqNaxlpKNiT86ALSed9QhFL ";

        private static void Main(string[] args)
        {
            //SentimentClassify();
            //var result = GenericFunction.Get<string>(2, "2");
            //Console.WriteLine(result.GetType());
            //var liupengSon = new Son();
            //liupengSon.UserName = "刘鹏";
            //liupengSon.BuildHouse();
            //liupengSon.ShowSkill();

            //int num = 1;
            //long numLong = 2;
            //string str = "";
            //float numFloat = 1.5f;
            //double numDouble = 1.3;
            //decimal numDecimal = 2.5M;
            //object obj  = "";
            //bool numBool = true;


            //var userName = "liupeng";
            //运算符+、-、*、/，

            var numOne = 2;
            var numThree = numOne --;
            numThree += numOne;
            numThree = numThree + numOne;
            numThree -= numOne;
            numThree = numThree - numOne;
            //Console.WriteLine(numTwo);
            Console.WriteLine(numOne);
            Console.WriteLine(numThree);


            

            Console.ReadKey();
        }

        public static void SentimentClassify()
        {
            var nlp = new Nlp(AppKey, AppSecret);
            const string sqlStr = "SELECT  id,accountid,t_mk,vm_id FROM  dbo.Sys_TaskDaily";
            var taskDailyList = DapperHelper.Query<TaskDailyEntity>(sqlStr, null);
            foreach (
                var result in
                    from item in taskDailyList
                    where !string.IsNullOrWhiteSpace(item.T_mk)
                    select nlp.SentimentClassify(item.T_mk))
            {
                SimpleLog.Instance.WriteLogForFile("用户反馈情感分析", JsonConvert.SerializeObject(result));
            }

            Console.WriteLine("执行完毕！");
        }
    }


    public class Father
    {
        public string UserName;

        public void BuildHouse()
        {
            Console.WriteLine("我能建房子！，我的名字是：" + UserName);
        }
    }


    public class Son : Father
    {
        public void ShowSkill()
        {
            Console.WriteLine("我可以打dota");
        }
    }
}