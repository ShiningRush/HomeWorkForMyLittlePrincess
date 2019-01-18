using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubwayTicketProblem.Win
{
    /// <summary>
    /// 地铁票计价问题
    /// 
    /// 业务场景
    /// 假设某个城市目前存在8个地铁站:国贸、老街、大剧院、科学馆、华强路、岗厦、购物中心、会展中心
    /// 各个站之间距离不同，本项目旨在通过计算乘客所经过的站点不同，算出合适的票价。
    /// 
    /// 要求如下：
    /// 1，起步价：2元、当乘客乘坐距离超过10公里时，超出部分按照 0.5元/公里 的价格收费，超出15公里部分按照 0.2元/公里 收费, 超出20公里部分按照0.1公里收费
    /// 2，在圣诞节(12.25)，时，对票价总体打8折
    /// 3，在计算正确票价与余额扣减时的同时，请保证对不同里程以及不同代码的计算代码位于不同class当中以保证代码隔离便于拓展
    /// 4，实现用户的充值
    /// 
    /// 用户与地铁站的定义均已实现好，放在SubwayTicketProblem.Library项目下
    /// 请实现地铁卡服务定义的接口
    /// 
    /// 姑娘可以使用SubwayTicketProblem.Win来校验自己的接口是否正确，也可以使用Host的控制台应用来调试你的服务接口，都没关系
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += Application_ThreadException;
            //处理非UI线程异常   
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("哎呀，系统出现了一些未处理的异常，要不然Debug看看？");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("哎呀，系统出现了一些未处理的异常，要不然Debug看看？");
        }
    }
}
