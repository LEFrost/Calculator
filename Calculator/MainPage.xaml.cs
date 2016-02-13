using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static Calculator.BlankPage1;
//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Calculator
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            mark.Push('#');
        }
        /// <summary>
        /// 判断是否有小数点
        /// </summary>
        bool ifDot = false;
        /// <summary>
        /// 临时存储算式
        /// </summary>
        List<object> temp = new List<object>();//
        /// <summary>
        /// 存储器
        /// </summary>
        List<object> storage = new List<object>();
        Stack mark = new Stack();
        Stack num = new Stack();
        char tempMark;
        double temp1, temp2, result;
        int length = 0;

        void myAdd(Num x)
        {
            if (expression.Text == "0")
                expression.Text = x.GetNum.ToString();
            else
                expression.Text = expression.Text + x.GetNum.ToString();
        }//操作数
        void myAdd(char ch)
        {

            if (temp.Count != 0 && Equals(temp[temp.Count - 1].GetType().ToString(), "System.Char") && expression.Text == "0" && temp.Count != 0)
            {
                temp[temp.Count - 1] = ch;

                Input.Text = Input.Text.Remove(Input.Text.Count() - 1) + ch;
            }
            else
            {
                temp.Add(Convert.ToDouble(expression.Text));
                temp.Add(ch);
                if (Input.Text == "")
                    Input.Text = expression.Text + ch;
                else
                    Input.Text += expression.Text + ch;
            }
            expression.Text = "0";
        }//运算符

        public class Num
        {
            private int getNum;

            public int GetNum
            {
                get
                {
                    return getNum;
                }

                set
                {
                    getNum = value;
                }
            }

        }

        #region 增加数字
        private void one_Click(object sender, RoutedEventArgs e)
        {
            Num one = new Num();
            one.GetNum = 1;
            myAdd(one);
        }
        private void two_Click(object sender, RoutedEventArgs e)
        {
            Num two = new Num();
            two.GetNum = 2;
            myAdd(two);
        }
        private void three_Click(object sender, RoutedEventArgs e)
        {
            Num three = new Num();
            three.GetNum = 3;
            myAdd(three);
        }
        private void four_Click(object sender, RoutedEventArgs e)
        {
            Num four = new Num();
            four.GetNum = 4;
            myAdd(four);
        }
        private void five_Click(object sender, RoutedEventArgs e)
        {
            Num five = new Num();
            five.GetNum = 5;
            myAdd(five);
        }
        private void six_Click(object sender, RoutedEventArgs e)
        {
            Num six = new Num();
            six.GetNum = 6;
            myAdd(six);
        }
        private void seven_Click(object sender, RoutedEventArgs e)
        {
            Num seven = new Num();
            seven.GetNum = 7;
            myAdd(seven);
        }
        private void eight_Click(object sender, RoutedEventArgs e)
        {
            Num eight = new Num();
            eight.GetNum = 8;
            myAdd(eight);
        }
        private void nine_Click(object sender, RoutedEventArgs e)
        {
            Num nine = new Num();
            nine.GetNum = 9;
            myAdd(nine);
        }
        private void zero_Click(object sender, RoutedEventArgs e)
        {
            Num zero = new Num();
            zero.GetNum = 0;
            myAdd(zero);
        }
        #endregion


        private void dot_Click(object sender, RoutedEventArgs e)
        {
            if (ifDot == true)
                expression.Text = expression.Text;
            else
            {
                expression.Text = expression.Text + ".";
                ifDot = true;
            }

        }

        private void add_Click(object sender, RoutedEventArgs e)
        {

            myAdd('+');
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {

            myAdd('-');

        }

        private void mul_Click(object sender, RoutedEventArgs e)
        {
            myAdd('*');

        }


        private void div_Click(object sender, RoutedEventArgs e)
        {
            myAdd('/');

        }

        private void allDel_Click(object sender, RoutedEventArgs e)
        {
            Input.Text = "";
            expression.Text = "0";
            mark.Clear();
            mark.Push('#');
            num.Clear();
            temp.Clear();
            storage.Clear();
        }

        private void record_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1));
        }

        private void count_Click(object sender, RoutedEventArgs e)
        {
            if (expression.Text == "0" && Input.Text == "")
            {
            }
            else
            {

                Input.Text += expression.Text;
                temp.Add(Convert.ToDouble(expression.Text));
                length = temp.Count;
                #region 将数字和字符存入存储器storage,实现逆波兰式
                for (int L = 0; L < length; L++)
                {
                    if (Equals(temp[L].GetType().ToString(), "System.Double"))
                        storage.Add(temp[L]);
                    else if (mark.Count == 1)
                        mark.Push(temp[L]);
                    else if (mark.Count > 1)
                    {
                        tempMark = Convert.ToChar(mark.Peek());
                        if ((tempMark == '+' || tempMark == '-') && (Convert.ToChar(temp[L]) == '/' || Convert.ToChar(temp[L]) == '*'))
                        {
                            mark.Push(temp[L]);
                        }
                        else
                        {
                            while (true)
                            {

                                if (Convert.ToChar(mark.Peek()) == '#' || ((tempMark == '+' || tempMark == '-') && (Convert.ToChar(temp[L]) == '/' || Convert.ToChar(temp[L]) == '*')))
                                {

                                    mark.Push(temp[L]);
                                    break;
                                }
                                else
                                {
                                    storage.Add(mark.Pop());
                                    tempMark = Convert.ToChar(mark.Peek());

                                }
                            }
                        }
                    }
                }
                int j = mark.Count;
                for (int i = 0; i < j; i++)
                {
                    storage.Add(Convert.ToChar(mark.Pop()));
                }
                #endregion
                #region 计算逆波兰式
                int n = 0;
                while (!Equals(storage[n], '#'))
                {

                    if (Equals(storage[n].GetType().ToString(), "System.Double"))
                    {
                        num.Push(storage[n]);
                    }
                    else if (Equals(storage[n].GetType().ToString(), "System.Char"))
                    {
                        temp2 = Convert.ToDouble(num.Pop());
                        temp1 = Convert.ToDouble(num.Pop());
                        switch (Convert.ToChar(storage[n]))
                        {
                            case '+':
                                result = temp1 + temp2;
                                num.Push(result);
                                break;
                            case '-':
                                result = temp1 - temp2;
                                num.Push(result);
                                break;
                            case '*':
                                result = temp1 * temp2;
                                num.Push(result);
                                break;
                            case '/':
                                result = temp1 / temp2;
                                num.Push(result);
                                break;

                        }
                    }

                    n++;

                }
                #endregion
                expression.Text = num.Pop().ToString();

                Class1.moc.Add(new Record { record = Input.Text + "=" + expression.Text });
            }
        }
        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (expression.Text == "" || expression.Text.Count() == 1)
            {
                expression.Text = "0";
            }
            else
            {
                expression.Text = expression.Text.Remove(expression.Text.Count() - 1);
            }
        }
    }


}
