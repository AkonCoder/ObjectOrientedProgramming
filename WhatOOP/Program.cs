using System;
using System.Collections.Generic;
using System.Linq;
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
            SentimentClassify();
            Console.ReadKey();
        }

        public static void SentimentClassify()
        {
            var nlp = new Nlp(AppKey, AppSecret);
            const string sqlStr = "SELECT  id,accountid,t_mk,vm_id FROM  dbo.Sys_TaskDaily";
            var taskDailyList = DapperHelper.Query<TaskDailyEntity>(sqlStr, null);
            foreach (var result in from item in taskDailyList where !string.IsNullOrWhiteSpace(item.T_mk) select nlp.SentimentClassify(item.T_mk))
            {
                SimpleLog.Instance.WriteLogForFile("用户反馈情感分析", JsonConvert.SerializeObject(result));
            }
            
            Console.WriteLine("执行完毕！");
        }
    }
}