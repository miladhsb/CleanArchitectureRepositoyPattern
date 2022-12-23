using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Utility
{
    public static class DigitToAlpha
    {
        public static string ToAlpha(this string x)
        {
            if (string.IsNullOrEmpty(x))
            {
                x = Convert.ToString(Double.Parse(x));
            }



            string text7;
            string text10;

            string[] textArray0_10 = new string[11];
            textArray0_10[0] = "صفر";
            textArray0_10[1] = "یک";
            textArray0_10[2] = "دو";
            textArray0_10[3] = "سه";
            textArray0_10[4] = "چهار";
            textArray0_10[5] = "پنج";
            textArray0_10[6] = "شش";
            textArray0_10[7] = "هفت";
            textArray0_10[8] = "هشت";
            textArray0_10[9] = "نه";

            string[] textArray10_19 = new string[11];
            textArray10_19[0] = "ده";
            textArray10_19[1] = "یازده";
            textArray10_19[2] = "دوازده";
            textArray10_19[3] = "سیزده";
            textArray10_19[4] = "چهارده";
            textArray10_19[5] = "پانزده";
            textArray10_19[6] = "شانزده";
            textArray10_19[7] = "هفده";
            textArray10_19[8] = "هجده";
            textArray10_19[9] = "نوزده";

            string[] textArray20_90 = new string[11];
            textArray20_90[2] = "بیست";
            textArray20_90[3] = "سی";
            textArray20_90[4] = "چهل";
            textArray20_90[5] = "پنجاه";
            textArray20_90[6] = "شصت";
            textArray20_90[7] = "هفتاد";
            textArray20_90[8] = "هشتاد";
            textArray20_90[9] = "نود";

            string[] textArray100_900 = new string[11];
            textArray100_900[1] = "یکصد";
            textArray100_900[2] = "دویست";
            textArray100_900[3] = "سیصد";
            textArray100_900[4] = "چهارصد";
            textArray100_900[5] = "پانصد";
            textArray100_900[6] = "ششصد";
            textArray100_900[7] = "هفتصد";
            textArray100_900[8] = "هشتصد";
            textArray100_900[9] = "نهصد";

            string strHezar = "هزار";
            string strHezar_ = "هزار" + " ";
            string strMeliun = "میلیون";
            string strMiliard = "میلیارد";
            string strMiliard_ = "میلیارد" + " ";
            string strTriliun = "تریلیون";
            string strTriliun_ = "تریلیون" + " ";

            string text4 = x;
            text7 = "";
            switch (text4.Length)
            {
                case 1:
                    if (x != "")
                    {
                        text7 = textArray0_10[Convert.ToInt32((string)x)];
                    }
                    break;

                case 2:
                    if ((Int32.Parse(text4.Substring(text4.Length - 1, 1)) > 0) & (Convert.ToDouble(text4.Substring(0, 1)) > 1))
                    {
                        text10 = Convert.ToString(Convert.ToDouble(text4.Substring(text4.Length - 1, 1)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    if (Convert.ToDouble(text4.Substring(0, 1)) > 1)
                    {
                        text7 = textArray20_90[Convert.ToInt32(text4.Substring(0, 1))] + text7;
                    }
                    if (Convert.ToDouble(text4.Substring(0, 1)) == 1)
                    {
                        text7 = textArray10_19[Convert.ToInt32(text4.Substring(text4.Length - 1, 1))];
                    }
                    break;

                case 3:
                    if (Convert.ToDouble(text4.Substring(text4.Length - 2, 2)) > 0)
                    {
                        text10 = Convert.ToString(Convert.ToDouble(text4.Substring(text4.Length - 2, 2)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    text7 = textArray100_900[(int)Math.Round(Convert.ToDouble(text4.Substring(0, 1)))] + text7;
                    break;

                case 4:
                    if (Convert.ToDouble(text4.Substring(text4.Length - 3, 3)) > 0)
                    {
                        text10 = Convert.ToString(Convert.ToDouble(text4.Substring(text4.Length - 3, 3)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 1)));
                    text7 = ToAlpha(text10) + strHezar + text7;
                    break;

                case 5:
                    if (Convert.ToDouble(text4.Substring(text4.Length - 3, 3)) > 0)
                    {
                        text10 = Convert.ToString(Convert.ToDouble(text4.Substring(text4.Length - 3, 3)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 2)));
                    text7 = ToAlpha(text10) + strHezar + text7;
                    break;

                case 6:
                    if (Convert.ToInt32(text4.Substring(text4.Length - 5, 5)) != 0)
                    {
                        if (Convert.ToInt32(text4.Substring(text4.Length - 3, 3)) > 0)
                        {
                            text10 = Convert.ToString(Convert.ToInt32(text4.Substring(text4.Length - 3, 3)));
                            text7 = " و " + ToAlpha(text10);
                        }
                        text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 3)));
                        text7 = ToAlpha(text10) + strHezar_ + text7;
                        break;
                    }
                    text7 = textArray100_900[(int)Math.Round(Convert.ToDouble(text4.Substring(0, 1)))] + strHezar_;
                    break;


                case 7:
                    if (Convert.ToInt32(text4.Substring(text4.Length - 6, 6)) != 0)
                    {
                        text10 = Convert.ToString(Convert.ToInt32(text4.Substring(text4.Length - 6, 6)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 1)));
                    text7 = ToAlpha(text10) + strMeliun + text7;
                    break;

                case 8:
                    if (Convert.ToInt32(text4.Substring(text4.Length - 6, 6)) > 0)
                    {
                        text10 = Convert.ToString(Convert.ToInt32(text4.Substring(text4.Length - 6, 6)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 2)));
                    text7 = ToAlpha(text10) + strMeliun + text7;
                    break;

                case 9:
                    if (Convert.ToInt32(text4.Substring(text4.Length - 6, 6)) > 0)
                    {
                        text10 = Convert.ToString(Convert.ToInt32(text4.Substring(text4.Length - 6, 6)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 3)));
                    text7 = ToAlpha(text10) + strMeliun + text7;
                    break;

                case 10:
                    if (Convert.ToInt32(text4.Substring(text4.Length - 9, 9)) > 0)
                    {
                        text10 = Convert.ToString(Convert.ToInt32(text4.Substring(text4.Length - 9, 9)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 1)));
                    text7 = ToAlpha(text10) + strMiliard + text7;
                    break;

                case 11:
                    if (Convert.ToInt32(text4.Substring(text4.Length - 9, 9)) > 0)
                    {
                        text10 = Convert.ToString(Convert.ToInt32(text4.Substring(text4.Length - 9, 9)));
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 2)));
                    text7 = ToAlpha(text10) + strMiliard + text7;
                    break;
                case 12:
                    if (Int32.Parse(text4.Substring(text4.Length - 9, 9)) > 0)
                    {
                        text10 = text4.Substring(text4.Length - 9, 9);
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 3)));
                    text7 = ToAlpha(text10) + strMiliard_ + text7;
                    break;

                case 13:
                    if (double.Parse(text4.Substring(text4.Length - 12, 12)) > 0)
                    {
                        text10 = text4.Substring(text4.Length - 12, 12);
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 1)));
                    text7 = ToAlpha(text10) + strTriliun + text7;

                    break;

                case 14:
                    if (double.Parse(text4.Substring(text4.Length - 12, 12)) > 0)
                    {
                        text10 = text4.Substring(text4.Length - 12, 12);
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 2)));
                    text7 = ToAlpha(text10) + strTriliun + text7;
                    break;

                case 15:
                    if (double.Parse(text4.Substring(text4.Length - 12, 12)) > 0)
                    {
                        text10 = text4.Substring(text4.Length - 12, 12);
                        text7 = " و " + ToAlpha(text10);
                    }
                    text10 = Convert.ToString(Convert.ToDouble(text4.Substring(0, 3)));
                    text7 = ToAlpha(text10) + strTriliun_ + text7;
                    break;
            }
            string text3 = " " + text7 + " ";

            return text3;
        }




    }
}
