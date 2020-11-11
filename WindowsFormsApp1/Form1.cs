using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace FlyDown
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        long timecount = 0;
        public Form1()
        {
            InitializeComponent();

            timer.Enabled = false;
            timer.Interval = 1000;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            using (WebClient wc = new WebClient())
            {

                if (textBox1.Text.Trim() == "")
                    MessageBox.Show("请输入下载路径");
                else if (textBox2.Text.Trim() == "")
                    MessageBox.Show("请输入文件存储名");
                else
                {

                    try
                    {
                        wc.Proxy = null;
                        Uri address = new Uri(textBox1.Text.ToString());
                        //调用DownloadFile方法下载文件
                        // wc.DownloadFile(textBox1.Text.ToString(), textBox2.Text.ToString());

                        //调用DownloadFileAsync异步下载文件
                        wc.DownloadFileAsync(address, textBox2.Text.ToString());
                        wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                        //下载完成的响应事件
                        wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);

                        //if(File .Exists (textBox2 .Text .ToString ()))
                        //    {
                        //        TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                        //        TimeSpan ts=ts2.Subtract (ts1 ).Duration();

                        //        MessageBox.Show("文件下载完成.下载用时：" + ts.Hours.ToString() + "小时" +
                        //            ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒");
                        //    }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
        private void timer_Tick(object o, EventArgs e)
        {
            timecount++;
        }
        //提示下载完成，并显示下载用时
        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            MessageBox.Show("下载完成，下载用时：" + timecount + "秒");
        }
        //定义进度条响应事件
        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBox2.Text.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Application.ExecutablePath+"+"+Directory .GetCurrentDirectory ()+"+"+Application .StartupPath);
            System.Diagnostics.Process.Start(Application.StartupPath);
        }

       
    }
}