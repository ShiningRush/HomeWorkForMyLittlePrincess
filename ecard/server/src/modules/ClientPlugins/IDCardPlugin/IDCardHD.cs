using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace Clear.ECSIDCardPlugin
{
	public struct IDCardData
    {
        public string Name; //姓名   
        public string Sex;   //性别
        public string Nation; //国家
        public string Born; //出生日期
        public string Address; //住址
        public string IDCardNo; //身份证号
        public string GrantDept; //发证机关
        public string UserLifeBegin; // 有效开始日期
        public string UserLifeEnd;  // 有效截止日期
        public string PhotoFileName; // 照片路径
        public string IDCardID;   //身份证ID号
        public string IDFrontImgFileName; //身份证扫描正面文件名
        public string IDRearImgFileName;  //身份证扫描反面文件名
    }
    public class IDCardHD 
    {
        #region 动态库DLL
        [DllImport(@"HDstdapi.dll", EntryPoint = "HD_InitComm")]
        private static extern int HD_InitComm(int Port); //连接

        [DllImport(@"HDstdapi.dll", EntryPoint = "HD_CloseComm")]
        public static extern int HD_CloseComm(int Port);  //关闭

        [DllImport(@"HDstdapi.dll", EntryPoint = "HD_Authenticate")]
        private static extern int HD_Authenticate(); //卡认证

        [DllImport(@"HDstdapi.dll", EntryPoint = "HD_Read_BaseInfo")]
        private static extern int HD_Read_BaseInfo(
            string pBmpData,        //如果为NULL不生成照片，如果需要生成照片这传人生成照片的路径
            byte[] pName,           //姓名
            byte[] pSex,            //性别
            byte[] pNation,         //国籍
            byte[] pBirth,          //出生日期
            byte[] pAddress,        //住址
            byte[] pCertNo,         //公民身份证号码
            byte[] pDepartment,     //签发机关
            byte[] pEffectData,     //有效期开始日期
            byte[] pExpire          //有效期结束日期
            );  //获取基本信息

        [DllImport(@"HDstdapi.dll", EntryPoint = "HD_Read_BaseFPInfo")]
        private static extern int HD_Read_BaseFPInfo(
            ref string pFingerData,   //输出指纹信息
            string pBmpData,         //如果为NULL不生成照片，如果需要生成照片这传人生成照片的路径  
            byte[] pName,            //姓名
            byte[] pSex,             //性别
            byte[] pNation,          //国籍
            byte[] pBirth,           //出生日期
            byte[] pAddress,         //住址
            byte[] pCertNo,          //公民身份证号码
            byte[] pDepartment,      //签发机关
            byte[] pEffectData,      //有效期开始日期
            byte[] pExpire           //有效期结束日期
            );   //获取带指纹的身份证信息

        [DllImport(@"HDstdapi.dll", EntryPoint = "HD_Read_BaseMsg")]
        private static extern int HD_Read_BaseMsg(
            string pBmpFile,         //如果pBmpFile==NULL，则不生成照片，如需生成照片，这里请传入路径。例如：C:/zhaopian/zp.bmp
            byte[] pName,            //姓名
            byte[] pSex,             //性别
            byte[] pNation,          //国籍
            byte[] pBirth,           //出生日期
            byte[] pAddress,         //住址
            byte[] pCertNo,          //公民身份证号码
            byte[] pDepartment,      //签发机关
            byte[] pEffectData,      //有效期开始日期
            byte[] pExpire           //有效期结束日期
           );  //将身份证信息保存在文件
        

        #endregion

        #region  参数
        public int iPort = 0;
        #endregion

        #region  方法
        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Port">串口号 连接串口（COM1~COM16）或USB口(1001~1016) 默认从1001开始</param>
        /// <param name="Baud">波特率(不用指定)</param>
        /// <returns>成功返回0，失败返回-1</returns>
        public int Init(int Port)
        {
            int result = HD_InitComm(Port);
            iPort = Port;
            return result == 0 ? 0 : -1;
        }
        #endregion

        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns>成功返回0，失败返回-1</returns>
        public int Close()
        {
            int result = HD_CloseComm(iPort);
            return result == 0 ? 0 : -1;
        }
        #endregion

        #region 读身份证信息
        /// <summary>
        /// 读或扫描身份证信息(照片路径,身份证扫描正反面照片路径居必传)
        /// </summary>
        /// <param name="Data">身份证结构体</param>
        /// <returns>成功返回0，失败返回-1，未检测到身份证返回1</returns>
        public int IDCardRead(ref IDCardData Data)
        {
            //先认证卡，连接
            int result = HD_Authenticate();
            if (result == 0)
            {
                    //取数据
                    int flag=  FillData(ref Data);
                    return flag == 0 ? 0 : -1;
                    // break;
            }
            return result;
        }

        private int FillData(ref IDCardData info)
        {
            int result = -1;

            string pBmpFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"zp.bmp");
            byte[] pName = new byte[32];
            byte[] pSex = new byte[6];
            byte[] pNation = new byte[30];
            byte[] pBirth = new byte[18];
            byte[] pAddress = new byte[72];
            byte[] pCertNo = new byte[36];
            byte[] pDepartment = new byte[40];
            byte[] pEffectData = new byte[18];
            byte[] pExpire = new byte[18];
            result = HD_Read_BaseMsg(pBmpFile, pName, pSex, pNation, pBirth, pAddress, pCertNo, pDepartment, pEffectData, pExpire);

            if (result == 0)
            {
                info.Name = System.Text.Encoding.Default.GetString(pName).Replace("\0", "").Trim();
                info.Sex = System.Text.Encoding.Default.GetString(pSex).Replace("\0", "").Trim();
                info.Nation = System.Text.Encoding.Default.GetString(pNation).Replace("\0", "").Trim();
                info.Born = System.Text.Encoding.Default.GetString(pBirth).Replace("\0", "").Trim();
                info.Address = System.Text.Encoding.Default.GetString(pAddress).Replace("\0", "").Trim();
                info.IDCardNo = System.Text.Encoding.Default.GetString(pCertNo).Replace("\0", "").Trim();
                info.GrantDept = System.Text.Encoding.Default.GetString(pDepartment).Replace("\0", "").Trim();
                info.UserLifeBegin = System.Text.Encoding.Default.GetString(pEffectData).Replace("\0", "").Trim();
                info.UserLifeEnd = System.Text.Encoding.Default.GetString(pExpire).Replace("\0", "").Trim();
                info.IDFrontImgFileName = pBmpFile;
            }

            return result;
        }

        #endregion

        #endregion
    }
}
