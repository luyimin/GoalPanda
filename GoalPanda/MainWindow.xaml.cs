using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xceed.Wpf.Toolkit;


namespace GoalPanda
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summar
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public int[] df = new int[13];
        public int[] lf = new int[13];
        static MainWindow _instance;
        Button _Row1 = null;
        Button _Row2 = null;

        int _Row1_Value = 0;
        int _Row2_Value = 0;
        int _Row3_Value = 0;

        String lb_odds;
     
        #region 賠率
        double A_normal_odds_10 = 1;
        double B_normal_odds_10 = 2.5;
        double C_normal_odds_10 = 2.5;

        double A_normal_odds_9 = 1;
        double B_normal_odds_9 = 1.5;
        double C_normal_odds_9 = 2.5;

        double A_normal_odds_8 = 1.2;
        double B_normal_odds_8 = 1.5;
        double C_normal_odds_8 = 2.5;

        double A_normal_odds_7 = 1.5;
        double B_normal_odds_7 = 1.5;
        double C_normal_odds_7 = 2.5;

        double A_normal_odds_6 = 1.8;
        double B_normal_odds_6 = 1;
        double C_normal_odds_6 = 2.5;

        double A_normal_odds_5 = 2.5;
        double B_normal_odds_5 = 0.8;
        double C_normal_odds_5 = 2.5;

        double A_normal_odds_4 = 3;
        double B_normal_odds_4 = 0.6;
        double C_normal_odds_4 = 2.5;

        double A_normal_odds_3 = 4.5;
        double B_normal_odds_3 = 0.4;
        double C_normal_odds_3 = 2.5;

        double A_normal_odds_2 = 7;
        double B_normal_odds_2 = 0.3;
        double C_normal_odds_2 = 2.5;

        double A_normal_odds_1 = 10;
        double B_normal_odds_1 = 0.2;
        double C_normal_odds_1 = 2.5;

        double A_normal_odds_A7K = 2.5;
        double B_normal_odds_A7K = 1;
        double C_normal_odds_A7K = 2.5;

        #endregion

        public MainWindow()
        {
            
            _instance = this;
            this.InitializeComponent();
            UpdateDefaultCards(this);
            UpdateLeftCards(this);
            LastCardsNotShow(this);
        }

        private void Row1_Click(object sender, RoutedEventArgs e)
        {

            if (_Row1 == null)
            {
                Button btn = (Button)sender;
                double a = Convert.ToDouble(btn.Name.Split('t')[1]);
                //Debug.WriteLine(a);
                Btn_NotEnable(btn);

                _Row1 = btn;
                _Row1_Value = Convert.ToInt32(a);
                SequenceConfirm();
                ShowRate();
                odds_show();

            }
            else
            {

                Btn_Enable(_Row1);
                Button btn = (Button)sender;
                double a = Convert.ToDouble(btn.Name.Split('t')[1]);
                //Debug.WriteLine(a);
                Btn_NotEnable(btn);

                _Row1 = btn;
                _Row1_Value = Convert.ToInt32(a);
                SequenceConfirm();
                ShowRate();
                odds_show();

            }
        }

        private void Row2_Click(object sender, RoutedEventArgs e)
        {

            if (_Row2 == null)
            {
                Button btn = (Button)sender;

                double a = Convert.ToDouble(btn.Name.Split('t')[1]);
                //Debug.WriteLine(a);
                Btn_NotEnable(btn);

                _Row2 = btn;
                _Row2_Value = Convert.ToInt32(a);
                SequenceConfirm();
                ShowRate();
                odds_show();

            }
            else
            {
                Btn_Enable(_Row2);

                Button btn = (Button)sender;

                double a = Convert.ToDouble(btn.Name.Split('t')[1]);
                //Debug.WriteLine(a);
                Btn_NotEnable(btn);

                _Row2 = btn;
                _Row2_Value = Convert.ToInt32(a);
                SequenceConfirm();
                ShowRate();
                odds_show();


            }
        }

        private void Row3_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            double a = Convert.ToDouble(btn.Name.Split('t')[1]);
            _Row3_Value = Convert.ToInt32(a);
            if (_Row1_Value > 0 && _Row2_Value > 0)
            {
                lf[_Row1_Value - 1] -= 1;
                lf[_Row2_Value - 1] -= 1;
                lf[_Row3_Value - 1] -= 1;

                SubLeftCatdValue(_Row1_Value);
                SubLeftCatdValue(_Row3_Value);

                SubLeftCatdValue(_Row2_Value);

                Btn_Enable(_Row1);
                Btn_Enable(_Row2);
                ShowImage(last_1, _Row1_Value);
                ShowImage(last_3, _Row3_Value);

                ShowImage(last_2, _Row2_Value);
                _Row1_Value = 0;
                _Row2_Value = 0;
                TB_win.Visibility = Visibility.Collapsed;
                TB_middle.Visibility = Visibility.Collapsed;
                TB_EV_A.Visibility = Visibility.Collapsed;
                TB_EV_B.Visibility = Visibility.Collapsed;
                TB_EV_C.Visibility = Visibility.Collapsed;
                lb_odd.Visibility = Visibility.Collapsed;
                //DisplayDeleteFileDialog();

            }



        }





        #region 機率
        public double WinRate()
        {
            UpdateLeftCards(this);
            if (Math.Abs(_Row1_Value - _Row2_Value) == 1)
                return 0;

            int cnt = 0;
            int sum = lf.Sum() - 2;
            double winrate = 0;

            int min = minMax()[0];
            int max = minMax()[1];
           // Debug.WriteLine("min: " + min);
            //Debug.WriteLine("max: " + max);
            for (int i = min; i < max-1; i++)
            {
                //Debug.WriteLine("cntloop: " + cnt);
                cnt += lf[i];
            }

            // super7
            if (Math.Abs(_Row1_Value - _Row2_Value) >= 7 || (min < 7 && max > 7))
                cnt = cnt - lf[6];

            winrate = (double)cnt / sum;

          //  Debug.WriteLine("cnt: " + cnt);

            return winrate;
        }

        private double[] ExWinRate()
        {
            int cnt_top = 0;
            int cnt_bottom = 0;
            int sum = lf.Sum() - 2;
            double[] winrate = new double[2];
            int ex = _Row1_Value;
            for (int i = ex; i < 13; i++)
            {
                cnt_top += lf[i];
            }

            for (int i = ex - 2; i >= 0; i--)
            {
                cnt_bottom += lf[i];
            }
            // 超級7
            if (ex != 7 && ex < 7)
                cnt_top -= lf[6];
            if (ex != 7 && ex > 7)
                cnt_bottom -= lf[6];

            winrate[0] = (double)cnt_top / sum;
            winrate[1] = (double)cnt_bottom / sum;



            return winrate;
        }

        private double HitMiddleRate()
        {
            if (Math.Abs(_Row1_Value - _Row2_Value) == 1)
                return 0;

            int cnt = 0;
            int sum = lf.Sum() - 2;
            double hitmiddle = 0;
            cnt += lf[_Row1_Value - 1] - 1;
            cnt += lf[_Row2_Value - 1] - 1;


            hitmiddle = (double)cnt / sum;
            return hitmiddle;
        }

        private double ExHitMiddleRate()
        {
            int cnt = 0;
            int sum = lf.Sum() - 2;
            double hitmiddle = 0;
            int ex = _Row1_Value;
            cnt += lf[ex - 1] - 2;

            // super7
            if (ex != 7)
                cnt += lf[6];


            hitmiddle = (double)cnt / sum;
            return hitmiddle;
        }

        #endregion
        #region 期望值


        private double[] ExpectedValue(double winrate, double hitmiddle)
        {

            int min = minMax()[0];
            int max = minMax()[1];
           // Debug.WriteLine(min);
            //Debug.WriteLine(max);

       

            double A_expectedval = 0;
            double B_expectedval = 0;
            double C_expectedval = 0;


            double[] expectedval = new double[3];
            if (Math.Abs(_Row1_Value - _Row2_Value) == 1)
            {
                expectedval = new double[] { 0, 0, 0 };

            }
            int sub = Math.Abs(_Row1_Value - _Row2_Value) - 1;

            if (sub >= 7 || (min < 7 && max > 7))
                sub -= 1;


           // Debug.WriteLine(sub);


            if ((min != 1 || max != 7) && (min != 7 || max != 13))
            {
                
                switch (sub)
                {
                    case 10:
                        lb_odds = "A:" + A_normal_odds_10.ToString() + "\n"
                            + "B:" + B_normal_odds_10.ToString() + "\n"
                            + "C:" + C_normal_odds_10.ToString() ;
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_10);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_10);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_10);
                        break;

                    case 9:
                        lb_odds = "A:" + A_normal_odds_9.ToString() + "\n"
                            + "B:" + B_normal_odds_9.ToString() + "\n"
                            + "C:" + C_normal_odds_9.ToString() ;
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_9);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_9);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_9);
                        break;

                    case 8:
                        lb_odds = "A:" + A_normal_odds_8.ToString() + "\n"
                                + "B:" + B_normal_odds_8.ToString() + "\n"
                                + "C:" + C_normal_odds_8.ToString();
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_8);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_8);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_8);
                        break;
                    case 7:
                        lb_odds = "A:" + A_normal_odds_7.ToString() + "\n"
                                + "B:" + B_normal_odds_7.ToString() + "\n"
                                + "C:" + C_normal_odds_7.ToString();
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_7);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_7);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_7);
                        break;
                    case 6:
                        lb_odds = "A:" + A_normal_odds_6.ToString() + "\n"
                                + "B:" + B_normal_odds_6.ToString() + "\n"
                                + "C:" + C_normal_odds_6.ToString();
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_6);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_6);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_6);
                        break;
                    case 5:
                        lb_odds = "A:" + A_normal_odds_5.ToString() + "\n"
                                + "B:" + B_normal_odds_5.ToString() + "\n"
                                + "C:" + C_normal_odds_5.ToString();
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_5);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_5);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_5);
                        break;
                    case 4:
                        lb_odds = "A:" + A_normal_odds_4.ToString() + "\n"
                                + "B:" + B_normal_odds_4.ToString() + "\n"
                                + "C:" + C_normal_odds_4.ToString();
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_4);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_4);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_4);
                        break;
                    case 3:
                        lb_odds = "A:" + A_normal_odds_3.ToString() + "\n"
                                + "B:" + B_normal_odds_3.ToString() + "\n"
                                + "C:" + C_normal_odds_3.ToString();
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_3);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_3);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_3);
                        break;
                    case 2:
                        lb_odds = "A:" + A_normal_odds_2.ToString() + "\n"
                                + "B:" + B_normal_odds_2.ToString() + "\n"
                                + "C:" + C_normal_odds_2.ToString();
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_2);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_2);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_2);
                        break;
                    case 1:
                        lb_odds = "A:" + A_normal_odds_1.ToString() + "\n"
                                + "B:" + B_normal_odds_1.ToString() + "\n"
                                + "C:" + C_normal_odds_1.ToString();
                        A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_1);
                        B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_1);
                        C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_1);
                        break;

                }
            }
            else
            {
                lb_odds = "A:" + A_normal_odds_A7K.ToString() + "\n"
                        + "B:" + B_normal_odds_A7K.ToString() + "\n"
                        + "C:" + C_normal_odds_A7K.ToString();

                A_expectedval = CalculateEV('A', winrate, hitmiddle, A_normal_odds_A7K);
                B_expectedval = CalculateEV('B', winrate, hitmiddle, B_normal_odds_A7K);
                C_expectedval = CalculateEV('C', winrate, hitmiddle, C_normal_odds_A7K);
                Debug.WriteLine(A_expectedval);
            }

            expectedval[0] = A_expectedval;
            expectedval[1] = B_expectedval;
            expectedval[2] = C_expectedval;

            return expectedval;
        }

        private double[] ExExpectedValue(double[] winrate, double hitmiddle)
        {
            double money = 1;
            double[] expectedval = new double[3];
            switch (_Row1_Value)
            {
                case 1:
                    expectedval[0] =
                        winrate[0] * money * 1 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] = 
                        winrate[1] * money * 0 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);

                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 2:
                    expectedval[0] =
                        winrate[0] * money * 1 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 5 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 3:
                    expectedval[0] =
                        winrate[0] * money * 1 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 3 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 4:
                    expectedval[0] =
                        winrate[0] * money * 1 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 2 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 5:
                    expectedval[0] =
                        winrate[0] * money * 1 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1.5 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 6:
                    expectedval[0] =
                        winrate[0] * money * 1 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1.2 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 7:
                    expectedval[0] =
                        winrate[0] * money * 1 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 8:
                    expectedval[0] =
                        winrate[0] * money * 1.2 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 9:
                    expectedval[0] =
                        winrate[0] * money * 1.5 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 10:
                    expectedval[0] =
                        winrate[0] * money * 2 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 11:
                    expectedval[0] =
                        winrate[0] * money * 3 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 12:
                    expectedval[0] =
                        winrate[0] * money * 5 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
                case 13:
                    expectedval[0] =
                        winrate[0] * money * 0 +
                        hitmiddle * money * (-3) +
                        (1 - winrate[0] - hitmiddle) * money * (-1);
                    expectedval[1] =
                       winrate[1] * money * 1 +
                       hitmiddle * money * (-3) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    expectedval[2] =
                       winrate[1] * money * (-1) +
                       hitmiddle * money * (2.5) +
                       (1 - winrate[1] - hitmiddle) * money * (-1);
                    break;
            }
            

            return expectedval;
        }

        #endregion
        #region Textbox

        private void TB_Win_TextChange(TextBlock TB, double winrate, double middlerate, double loserate)
        {
            TB.Text = "A: " + string.Format("{0:0.00%}", winrate) + "\n";
            TB.Text += "B: " + string.Format("{0:0.00%}", loserate) + "\n";
            TB.Text += "C: " + string.Format("{0:0.00%}", middlerate);
            TB.Visibility = Visibility.Visible;
        }
        private void TB_Middle_TextChange(TextBlock TB, double rate)
        {
            TB.Text = string.Format("{0:0.00%}", rate);
            TB.Visibility = Visibility.Visible;
        }

        private void GreenORred(TextBlock tb, double rate)
        {
            if (rate > 0)
                tb.Foreground = new SolidColorBrush(Colors.Red);
            else
                tb.Foreground = new SolidColorBrush(Colors.Green);
        }

        private void TB_EV_TextChange( double[] rate)
        {
            

            String Aev = "A: " + Math.Round(rate[0], 2).ToString() ;
            String Bev = "B: " + Math.Round(rate[1], 2).ToString() ;
            String Cev = "C: " + Math.Round(rate[2], 2).ToString();
            TB_EV_A.Text = Aev;
            GreenORred(TB_EV_A, rate[0]);
            TB_EV_A.Visibility = Visibility.Visible;
           
            TB_EV_B.Text = Bev;
            GreenORred(TB_EV_B, rate[1]);
            TB_EV_B.Visibility = Visibility.Visible;

            TB_EV_C.Text = Cev;
            GreenORred(TB_EV_C, rate[2]);
            TB_EV_C.Visibility = Visibility.Visible;


        }
        private void TB_Ex_Win_Middle_TextChange(TextBlock TB, double[] rate)
        {
            TB.Text = string.Format("上{0:0.00%}", rate[0]) + "\n" +
                string.Format("下{0:0.00%}", rate[1]);
            TB.Visibility = Visibility.Visible;
        }

        private void TB_Ex_EV_TextChange( double[] rate)
        {
            String Upev = "上: " + Math.Round(rate[0], 2).ToString();
            String Downev = "下: " + Math.Round(rate[1], 2).ToString();
            String Middleev = "C: " + Math.Round(rate[2], 2).ToString();

            TB_EV_A.Text = Upev;
            GreenORred(TB_EV_A, rate[0]);
            TB_EV_A.Visibility = Visibility.Visible;

            TB_EV_B.Text = Downev;
            GreenORred(TB_EV_B, rate[1]);
            TB_EV_B.Visibility = Visibility.Visible;

            TB_EV_C.Text = Middleev;
            GreenORred(TB_EV_C, rate[2]);
            TB_EV_C.Visibility = Visibility.Visible;

        }
        #endregion
        #region Functions

        private double CalculateEV(char alphabet, double winrate, double hitmiddle, double odds)
        {
            double money = 1;

            switch (alphabet)
            {
                case 'A':
                    double A_expectedval = 0;


                    A_expectedval =
                               winrate * money * odds +
                               hitmiddle * money * (-2) +
                               (1 - winrate - hitmiddle) * money * (-1);

                    return A_expectedval;


                case 'B':
                    double B_expectedval = 0;
                    B_expectedval =
                              winrate * money * (-1) +
                              hitmiddle * money * (-1) +
                              (1 - winrate - hitmiddle) * money * odds;
                    return B_expectedval;

                case 'C':
                    double C_expectedval = 0;
                    C_expectedval =
                            winrate * money * (-1) +
                            hitmiddle * money * odds +
                            (1 - winrate - hitmiddle) * money * (-1);
                    return C_expectedval;

                default:
                    return 0;
            }

        }

        private void DisplayDeleteFileDialog()
        {
            string messageBoxText = "第一張牌與第二張牌連續，是否從剩餘牌數扣除？?";
            string caption = "是否從剩餘牌數扣除？";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.None;
            MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    lf[_Row1_Value - 1] -= 1;
                    lf[_Row2_Value - 1] -= 1;
                    SubLeftCatdValue(_Row1_Value);
                    SubLeftCatdValue(_Row2_Value);
                    Btn_Enable(_Row1);
                    Btn_Enable(_Row2);
                    _Row1_Value = 0;
                    _Row2_Value = 0;
                    break;
                case MessageBoxResult.No:
                    Btn_Enable(_Row1);
                    Btn_Enable(_Row2);
                    _Row1_Value = 0;
                    _Row2_Value = 0;
                    break;
            }
        }

        public void UpdateDefaultCards(MainWindow instance)
        {
            for (int i = 0; i < 13; i++)
            {
                String temp = "df_" + (i + 1);
                IntegerUpDown nmb = this.FindName(temp) as IntegerUpDown;

                df[i] = Convert.ToInt32(nmb.Value);
            }
            //Debug.WriteLine(string.Join(" ", df));
        }

        public void UpdateLeftCards(MainWindow instance)
        {
            for (int i = 0; i < 13; i++)
            {
                String temp = "lf_" + (i + 1);
                //Debug.WriteLine(temp);
                IntegerUpDown nmb = this.FindName(temp) as IntegerUpDown;
                lf[i] = Convert.ToInt32(nmb.Value);
            }
            //Debug.WriteLine(string.Join(" ", lf));
        }

        public void LastCardsNotShow(MainWindow instance)
        {
            last_1.Visibility = Visibility.Collapsed;
            last_2.Visibility = Visibility.Collapsed;
            last_3.Visibility = Visibility.Collapsed;
        }

        private void SequenceConfirm()
        {
            int sub = Math.Abs(_Row1_Value - _Row2_Value);
            int min = minMax()[0];
            int max = minMax()[1];
            if (_Row1_Value > 0 && _Row2_Value > 0 && sub == 1)
            {
                DisplayDeleteFileDialog();
            }
            else if (min == 6 && max == 8)
            {
                DisplayDeleteFileDialog();
            }
        }

        private void odds_show()
        {
            if (_Row1_Value > 0 && _Row1_Value > 0)
            {
                lb_odd.Visibility = Visibility.Visible;
                lb_odd.Content = lb_odds;
            }
        }

        private int[] minMax()
        {
            int[] arrminMax = new int[2];
            int min = _Row1_Value;
            int max = _Row2_Value;
            if (_Row1_Value > _Row2_Value)
            {
                max = _Row1_Value;
                min = _Row2_Value;
            }
            arrminMax[0] = min;
            arrminMax[1] = max;


            return arrminMax;
        }

        private void Btn_Enable(Button btn)
        {
            btn.IsEnabled = true;
            Image img = (Image)btn.Content;
            img.Opacity = 1;
        }

        private void Btn_NotEnable(Button btn)
        {
            btn.IsEnabled = false;
            Image img = (Image)btn.Content;
            img.Opacity = 0.1;
        }

        private void ShowImage(Image image, int value)
        {
            BitmapImage source = new BitmapImage(new Uri("/Assets/cards/" + value + ".jpg", UriKind.Relative));
            image.Source = source;
            image.Visibility = Visibility.Visible;
        }

        private void SubLeftCatdValue(int value)
        {
            String temp = "lf_" + value;
            IntegerUpDown nmb = _instance.FindName(temp) as IntegerUpDown;
            nmb.Value -= 1;
        }

        private void InitLeftCatdValue()
        {
            for (int value = 1; value <= 13; value++)
            {
                String temp = "lf_" + value;
                IntegerUpDown lfnmb = _instance.FindName(temp) as IntegerUpDown;
                String temp2 = "df_" + value;
                IntegerUpDown dfnmb = _instance.FindName(temp2) as IntegerUpDown;
                lfnmb.Value = dfnmb.Value;
            }
        }
        private void ShowRate()
        {

            if (_Row1_Value != 0 && _Row2_Value != 0)
            {
                if (_Row2_Value != _Row1_Value)
                {
                    double win = WinRate();
                    double hit = HitMiddleRate();
                    double[] ex = ExpectedValue(win, hit);

                    TB_Win_TextChange(TB_win, win, hit, 1 - win - hit);
                    TB_Middle_TextChange(TB_middle, hit);
                    TB_EV_TextChange(ex);
                }
                else
                {
                    double[] exwin = ExWinRate();

                    double hit = ExHitMiddleRate();
                    double[] exev = ExExpectedValue(exwin, hit);

                    TB_Ex_Win_Middle_TextChange(TB_win, exwin);
                    TB_Middle_TextChange(TB_middle, hit);
                    TB_Ex_EV_TextChange(exev);

                }
            }
            else
            {
                return;
            }
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            InitLeftCatdValue();
            UpdateDefaultCards(_instance);
            UpdateLeftCards(_instance);
            if (!(_Row1 is null))
                Btn_Enable(_Row1);
            if (!(_Row2 is null))
                Btn_Enable(_Row2);

            _Row1 = null;
            _Row2 = null;
            _Row1_Value = 0;
            _Row2_Value = 0;
            _Row3_Value = 0;

            last_1.Visibility = Visibility.Collapsed;
            last_2.Visibility = Visibility.Collapsed;
            last_3.Visibility = Visibility.Collapsed;
            TB_win.Visibility = Visibility.Collapsed;
            TB_middle.Visibility = Visibility.Collapsed;
            TB_EV_A.Visibility = Visibility.Collapsed;
            TB_EV_B.Visibility = Visibility.Collapsed;
            TB_EV_C.Visibility = Visibility.Collapsed;
            lb_odd.Visibility = Visibility.Collapsed;
        }
        #endregion


    }
}
