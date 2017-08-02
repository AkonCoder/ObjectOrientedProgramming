using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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

            //var numOne = 2;
            //var numThree = numOne --;
            //numThree += numOne;
            //numThree = numThree + numOne;
            //numThree -= numOne;
            //numThree = numThree - numOne;
            ////Console.WriteLine(numTwo);
            //Console.WriteLine(numOne);
            //Console.WriteLine(numThree);


            //var startDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddDays(-7);
            //var endDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            //Console.WriteLine(string.Format("开始日期是：{0}；结束日期是：{1}", startDate, endDate));

            //Image img1 = Image.FromFile(@"D:\Pictures\1.jpg");
            ////Image img2 = Image.FromFile(@"D:\Pictures\2.jpg");
            //Image img = ImageMergeHelper.ImgMerge(620, 600, 10, null,
            //    new Image[] { img1 });
            //img.Save(@"D:\Pictures\allinone.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            WaterMark waterMark = WaterMarkImage();
            DIVWaterMark(waterMark);
            Console.WriteLine("合成图片成功!");
            Console.ReadKey();
        }

        #region 水印预设

        /// <summary>
        /// 水印文字预设
        /// </summary>
        /// <returns></returns>
        private static WaterMark WaterMarkFont()
        {
            WaterMark waterMark = new WaterMark();
            waterMark.WaterMarkType = WaterMarkTypeEnum.Text;
            waterMark.Transparency = 0.7f;
            waterMark.Text = "dunitian.cnblogs.com";
            waterMark.FontStyle = System.Drawing.FontStyle.Bold;
            waterMark.FontFamily = "Consolas";
            waterMark.FontSize = 20f;
            waterMark.BrushesColor = System.Drawing.Brushes.Black;
            waterMark.WaterMarkLocation = WaterMarkLocationEnum.CenterCenter;
            return waterMark;
        }

        /// <summary>
        /// 图片水印预设
        /// </summary>
        /// <returns></returns>
        private static WaterMark WaterMarkImage()
        {
            var waterMark = new WaterMark
            {
                WaterMarkType = WaterMarkTypeEnum.Image,
                ImgPath = @"D:\Pictures\1.jpg",
                WaterMarkLocation = WaterMarkLocationEnum.CenterCenter,
                Transparency = 1f
            };
            return waterMark;
        }

        #endregion

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

        #region 水印操作

        /// <summary>
        /// 单个水印操作
        /// </summary>
        /// <param name="waterMark"></param>
        private static void DIVWaterMark(WaterMark waterMark)
        {
            #region 必须参数获取

            //图片路径
            const string filePath = @"D:\Pictures\2.jpg";
            //文件名
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            //图片所处目录
            string dirPath = Path.GetDirectoryName(filePath);
            //存放目录
            string savePath = dirPath + "\\DNTWaterMark";
            //是否存在，不存在就创建
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            #endregion

            #region 水印操作

            Image successImage = WaterMarkHelper.SetWaterMark(filePath, waterMark);
            if (successImage != null)
            {
                //保存图片（不管打不打开都保存）
                successImage.Save(savePath + "\\" + fileName + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                Console.WriteLine("水印失败！请检查原图和水印图！");
            }

            #endregion
        }

        #endregion
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