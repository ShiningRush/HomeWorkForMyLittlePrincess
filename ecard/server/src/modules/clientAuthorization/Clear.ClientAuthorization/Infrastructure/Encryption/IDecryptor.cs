using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clear.ClientAuthorization.Infrastructure.Encryption
{
    public interface IDecryptor
    {
        string DecryptHttpSecret(string httpSecret, string privateKey);
    }
}
