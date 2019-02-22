using Abp.Dependency;
using HISP.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.Infrastructure.Encryption
{
    public class Encryptor : IEncryptor, ITransientDependency
    {
        public string EncryptSecret(string secret, string salt)
        {
            return HashHelper.ComputeSaltedHash(secret, salt);
        }

        public string EncryptStringByRSA(string dealString, string publicKey)
        {
            return HashHelper.RSAEncrypt(dealString, publicKey);
        }

        public void GetPublicAndPrivateKey(out string publicKey, out string privateKey)
        {
            HashHelper.CreateKey(out publicKey, out privateKey);
        }
    }
}
