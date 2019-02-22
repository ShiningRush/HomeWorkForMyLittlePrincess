using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Clear.UserPermission.Domain.Authorization
{
    public static class HashHelper
    {
        /// <summary>
        /// Create random salt
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomSalt()
        {
            Byte[] saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public static string StringToBase64(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(data);
        }

        public static string Base64ToString(string base64)
        {
            byte[] data = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(data);
        }


        /// <summary>
        /// Compute salted password hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string ComputeSaltedHash(string password, string salt)
        {
            return InternalComputeSaltedHash(password, salt);
        }
        /// <summary>
        /// Compute salted password hash 以便混淆代码时不暴露外部
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string InternalComputeSaltedHash(string password, string salt)
        {
            // Create Byte array of password string
            UnicodeEncoding encoder = new UnicodeEncoding();
            Byte[] secretBytes = encoder.GetBytes(password);

            // Create a new salt
            Byte[] saltBytes = Convert.FromBase64String(salt);

            // append the two arrays
            Byte[] toHash = new Byte[secretBytes.Length + saltBytes.Length];
            Array.Copy(secretBytes, 0, toHash, 0, secretBytes.Length);
            Array.Copy(saltBytes, 0, toHash, secretBytes.Length, saltBytes.Length);

            SHA512 sha512 = SHA512.Create();
            Byte[] computedHash = sha512.ComputeHash(toHash);

            return Convert.ToBase64String(computedHash);
        }

        /// <summary>
        /// 方 法 名：CreateKey
        /// 方法说明：自动生成非对称公钥、私钥
        /// 创 建 人：刘国炯
        /// 创建时间：2017/2/21
        /// </summary>
        /// <param name="publicKey">返回的公钥</param>
        /// <param name="privateKey">返回的私钥</param>
        public static void CreateKey(out string publicKey, out string privateKey)
        {
            InternalCreateKey(out publicKey, out privateKey);
        }
        /// <summary>
        /// 内部自动生成非对称公钥、私钥 以便混淆代码时不暴露外部
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="privateKey"></param>
        private static void InternalCreateKey(out string publicKey, out string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            publicKey = rsa.ToXmlString(false);
            privateKey = rsa.ToXmlString(true);
        }




        /// <summary>
        /// 方 法 名：RSAEncrypt
        /// 方法说明：非对称加密方法
        /// 创 建 人：刘国炯
        /// 创建时间：2017/2/21
        /// </summary>
        /// <param name="strMsg">要加密的数据</param>
        /// <param name="publicKey">加密的公钥</param>
        /// <returns>返回加密后的byte[]数据</returns>
        public static string RSAEncrypt(string strMsg, string publicKey)
        {
            return InternalRSAEncrypt(strMsg, publicKey);
        }



        /// <summary>
        /// 方 法 名：InternalRSAEncrypt
        /// 方法说明：非对称加密方法  以便混淆代码时不暴露外部
        /// 创 建 人：刘国炯  
        /// 创建时间：2017/2/21
        /// </summary>
        /// <param name="strMsg">The STR MSG.</param>
        /// <param name="publicKey">The public key.</param>
        /// <returns></returns>
        private static string InternalRSAEncrypt(string strMsg, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            byte[] data = Encoding.UTF8.GetBytes(strMsg);
            //encryBuffer = rsa.Encrypt(buffers, false); //有长度问题
            int keySize = rsa.KeySize / 8;
            int bufferSize = keySize - 11;
            byte[] buffer = new byte[bufferSize];
            MemoryStream msInput = new MemoryStream(data);
            MemoryStream msOutput = new MemoryStream();
            int readLen = msInput.Read(buffer, 0, bufferSize);
            while (readLen > 0)
            {
                byte[] dataToEnc = new byte[readLen];
                Array.Copy(buffer, 0, dataToEnc, 0, readLen);
                byte[] encData = rsa.Encrypt(dataToEnc, false);
                msOutput.Write(encData, 0, encData.Length);
                readLen = msInput.Read(buffer, 0, bufferSize);
            }
            msInput.Close();
            byte[] result = msOutput.ToArray();    //得到加密结果
            msOutput.Close();
            rsa.Clear();
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// 方 法 名：RSADecrypt
        /// 方法说明：非对称解密方法  
        /// 创 建 人：刘国炯
        /// 创建时间：2017/2/21
        /// </summary>
        /// <param name="strMsg">需要解密的字符串</param>
        /// <param name="privateKey">解密的私钥</param>
        /// <returns>解密后的数据string类型</returns>
        public static string RSADecrypt(string strMsg, string privateKey)
        {
            return InternalRSADecrypt(strMsg, privateKey);
        }


        /// <summary>
        /// 方 法 名：InternalRSADecrypt
        /// 方法说明：非对称解密方法  以便混淆代码时不暴露外部
        /// 创 建 人：刘国炯
        /// 创建时间：2017/2/21
        /// </summary>
        /// <param name="strMsg">The STR MSG.</param>
        /// <param name="privateKey">The private key.</param>
        /// <returns></returns>
        private static string InternalRSADecrypt(string strMsg, string privateKey)
        {
            byte[] dataEnc = Convert.FromBase64String(strMsg);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            //byte[] decryptBuffers = rsa.Decrypt(dataEnc, false);
            int keySize = rsa.KeySize / 8;
            byte[] buffer = new byte[keySize];
            MemoryStream msInput = new MemoryStream(dataEnc);
            MemoryStream msOutput = new MemoryStream();
            int readLen = msInput.Read(buffer, 0, keySize);
            while (readLen > 0)
            {
                byte[] dataToDec = new byte[readLen];
                Array.Copy(buffer, 0, dataToDec, 0, readLen);
                byte[] decData = rsa.Decrypt(dataToDec, false);
                msOutput.Write(decData, 0, decData.Length);
                readLen = msInput.Read(buffer, 0, keySize);
            }
            msInput.Close();
            byte[] decryptBuffers = msOutput.ToArray();    //得到解密结果
            msOutput.Close();
            rsa.Clear();
            return Encoding.UTF8.GetString(decryptBuffers);
        }

        /// <summary>
        /// 获取MD5串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5Hash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            char[] temp = new char[res.Length];
            System.Array.Copy(res, temp, res.Length);
            return new String(temp);
        }
    }
}
