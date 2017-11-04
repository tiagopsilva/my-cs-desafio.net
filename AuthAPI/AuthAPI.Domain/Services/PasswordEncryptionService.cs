using System.Security.Cryptography;
using System.Text;

namespace AuthAPI.Domain.Services
{
    public class PasswordEncryptionService
    {
        private const string SALT = "|4FA15E7D-E83B-461D-B30B-DFE4AF93EAB3";
        private readonly SHA256 _sha256 = SHA256.Create();

        public string EncryptPassword(string value)
        {
            value += SALT;
            var data = _sha256.ComputeHash(Encoding.Default.GetBytes(value));
            var sbString = new StringBuilder();
            foreach (var _char in data)
                sbString.Append(_char.ToString("x2"));
            return sbString.ToString();
        }

        public bool ComparePasswords(string password, string passwordHash)
        {
            return passwordHash == EncryptPassword(password);
        }
    }
}