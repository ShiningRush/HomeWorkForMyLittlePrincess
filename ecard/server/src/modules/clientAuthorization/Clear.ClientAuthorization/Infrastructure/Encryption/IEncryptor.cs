using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.Infrastructure.Encryption
{
    public interface IEncryptor
    {
        string EncryptSecret(string secret, string salt);
        string EncryptStringByRSA(string dealString, string publicKey);
        void GetPublicAndPrivateKey(out string publicKey, out string privateKey);
    }
}
