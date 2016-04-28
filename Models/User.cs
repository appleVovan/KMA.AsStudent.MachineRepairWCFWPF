using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace MacineRepairTool.Models
{
    public enum UserType
    {
        Client = 0,
        Partner = 1
    }

    [Serializable]
    [DataContract(IsReference = true)]
    public class User : IEntityObject<User>
    {
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _login;
        [DataMember]
        private string _password;
        [DataMember]
        private string _name;
        [DataMember]
        private bool _isDeleted;
        [DataMember]
        private UserType _userType;
        

        public Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }

        public bool HasAssociation
        {
            get { return false; }
        }


        public string Login
        {
            get { return _login; }
            private set { _login = value; }
        }

        public string Password
        {
            get { return _password; }
            private set { _password = value; }
        }

        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        public UserType UserType
        {
            get { return _userType; }
            protected set { _userType = value; }
        }

        public User(string login, string password, string name)
        {
            _guid = Guid.NewGuid();
            _login = login;
            ChangePassword(password);
            _name = name;
            _isDeleted = false;
            _userType = UserType.Partner;
        }

        public void ChangePassword(string password)
        {
            _password = Encrypt(password);
        }

        public User()
        {
        }

        public class UserConfiguration : EntityTypeConfiguration<User>
        {
            public UserConfiguration()
            {
                ToTable("User");
                HasKey(p => p.Guid);
                Property(p => p.Guid).HasColumnName("Guid").IsRequired();
                Property(p => p.IsDeleted).HasColumnName("IsDeleted").IsRequired();
                Property(p => p.Login).HasColumnName("Login").IsRequired();
                Property(p => p.Name).HasColumnName("Name").IsRequired();
                Property(p => p.Password).HasColumnName("Password").IsRequired();
                Property(p => p.UserType).HasColumnName("UserType").IsRequired();
                Ignore(p => p.HasAssociation);
                Ignore(p => p.Type);
            }
        }

        public Expression<Func<User, bool>> ObjectGuid()
        {
            Expression<Func<User, bool>> exp = obj => obj.Guid == Guid;
            return exp;
        }

        public IQueryable<User> GetAssociaton(DbSet<User> dbSet)
        {
            throw new NotImplementedException();
        }

        public void DropAssociations()
        {
        }


        public static string Encrypt(string Data)
        {
            try
            {

                string passPhrase = "bananax97";
                string saltValue = "pepper";
                string hashAlgorithm = "MD5";
                int passwordIterations = 1;
                string initVector = "koxskfruvdslbsxu";
                int keySize = 128;

                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(Data);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm,
                    passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize/8);

                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();

                byte[] cipherTextBytes = memoryStream.ToArray();

                memoryStream.Close();
                cryptoStream.Close();

                string cipherText = Convert.ToBase64String(cipherTextBytes);

                return cipherText;

            }
            catch
            {
            }

            return "";
        }

        public static string Decrypt(string Data)
        {
            try
            {

                string passPhrase = "bananax97";
                string saltValue = "pepper";
                string hashAlgorithm = "MD5";
                int passwordIterations = 1;
                string initVector = "koxskfruvdslbsxu";
                int keySize = 128;

                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] cipherTextBytes = Convert.FromBase64String(Data);

                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm,
                    passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize/8);

                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                memoryStream.Close();
                cryptoStream.Close();

                string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                return plainText;

            }
            catch
            {
            }

            return "";
        }

        public ObjectType Type => ObjectType.User;
    }

}
