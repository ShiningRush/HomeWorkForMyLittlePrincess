using Abp.Dependency;
using Castle.Core.Logging;
using HISP.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.Infrastructure.Encryption
{
    public class Decryptor : IDecryptor, ITransientDependency
    {
        public ILogger Logger { get; set; }

        public Decryptor()
        {
            Logger = NullLogger.Instance;
        }

        public string DecryptHttpSecret(string httpSecret, string privateKey)
        {
            try
            {
                return HashHelper.RSADecrypt(httpSecret, privateKey);
            }
            catch (Exception ex)
            {
                Logger.Error($"使用RSA算法解密secret时失败。httpSecret:{httpSecret}   privateKey:{privateKey}", ex);
                return string.Empty;
            }
        }
    }
}
