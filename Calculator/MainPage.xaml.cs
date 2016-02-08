using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        bool ifDot = false;
        List<object> temp = new List<object>();
        List<object> storage = new List<object>();
        Stack mark = new Stack();
        Stack num = new Stack();
        char tempMark;
        double temp1, temp2, result;

        int length = 0;


        void myAdd(Button x)
        {
            if (expression.Text == "0")
                expression.Text = x.Content.ToString();
            else
                expression.Text = expression.Text + x.Content.ToString();
        }//操作数
        void myAdd(char ch)
        {

            if (temp.Count!=0&& Equals(temp[temp.Count - 1].GetType().ToString(), "System.Char") && expression.Text == "" && temp.Count != 0)
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
            expression.Text = "";
        }//运算符
        #region 增加数字
        private void one_Click(object sender, RoutedEventArgs e)
        {
            myAdd(one);
        }
        private void two_Click(object sender, RoutedEventArgs e)
        {
            myAdd(two);
        }
        private void three_Click(object sender, RoutedEventArgs e)
        {
            myAdd(three);
        }
        private void four_Click(object sender, RoutedEventArgs e)
        {
            myAdd(four);
        }
        private void five_Click(object sender, RoutedEventArgs e)
        {
            myAdd(five);
        }
        private void six_Click(object sender, RoutedEventArgs e)
        {
            myAdd(six);
        }
        private void seven_Click(object sender, RoutedEventArgs e)
        {
            myAdd(seven);
        }
        private void eight_Click(object sender, RoutedEventArgs e)
        {
            myAdd(eight);
        }
        private void nine_Click(object sender, RoutedEventArgs e)
        {
            myAdd(nine);
        }
        private void zero_Click(object sender, RoutedEventArgs e)
        {
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

        private void count_Click(object sender, RoutedEventArgs e)
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
