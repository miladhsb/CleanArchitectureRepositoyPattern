using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Utility
{
    public static class ConvertDate
    {

        public static string ToShamsi(this DateTime value)
        {

            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");

        }


        public static DateTime ToMiladi(this string value)
        {

            string[] persianDate = value.Split("/");

            PersianCalendar pc = new PersianCalendar();
            return new DateTime(Convert.ToInt32(persianDate[0]), Convert.ToInt32(persianDate[1]),
                Convert.ToInt32(persianDate[2]), pc);
        }



        public static string ToShamsiAlpha(this string value)
        {
            string[] persianDate = value.Split("/");
            string Year = "";
            Year = persianDate[0].Replace("_", "0").ToAlpha();
            string month = "";
            switch (persianDate[1])
            {
                case "01":
                    month = "فروردین ماه";
                    break;
                case "02":
                    month = "اردیبهشت ماه";
                    break;
                case "03":
                    month = "خرداد ماه";
                    break;
                case "04":
                    month = "تیر ماه";
                    break;
                case "05":
                    month = "مرداد ماه";
                    break;
                case "06":
                    month = "شهریور ماه";
                    break;
                case "07":
                    month = "مهر ماه";
                    break;
                case "08":
                    month = "آبان ماه";
                    break;
                case "09":
                    month = "آذر ماه";
                    break;
                case "10":
                    month = "دی ماه";
                    break;
                case "11":
                    month = "بهمن ماه";
                    break;
                case "12":
                    month = "اسفند ماه";
                    break;

            }

            string Day = "";
            int DayNum = int.Parse((persianDate[2]).Replace("_", "0"));
            if (DayNum != 0)
            {
                Day = DayNum.ToString().ToAlpha().Trim() + "م";

                if (DayNum == 3)
                {
                    Day = "سوم";
                }
                if (DayNum == 23)
                {
                    Day = "بیست و سوم";
                }
                if (DayNum == 30)
                {
                    Day = "سی ام";
                }
            }



            return Day + " " + month + " " + Year;

        }
    }
}
