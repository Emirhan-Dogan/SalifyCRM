using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public static class HashingHelper
    {
        // Image hash oluşturma metodunu string olarak döndürmek için
        public static string CreateImageHash(string path)
        {
            using var hmac = new HMACSHA256();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(path));

            // Byte dizisini Base64 string'e çevir
            return Convert.ToBase64String(passwordHash);
        }

        // Şifre hash'ini ve salt'ı oluşturup, byte dizisi yerine string döndürmek
        public static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = Convert.ToBase64String(hmac.Key);  // Salt'ı Base64 string'e çevir
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));  // Hash'i Base64 string'e çevir
        }

        // Şifre doğrulama metodunda, byte dizileri yerine Base64 string'leri kullanmak
        public static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrEmpty(storedSalt) || string.IsNullOrEmpty(storedHash))
            {
                return false;
            }

            try
            {
                // Salt'ı Base64 string'den byte dizisine çevir
                byte[] saltBytes = Convert.FromBase64String(storedSalt);
                // Hash'i Base64 string'den byte dizisine çevir
                byte[] hashBytes = Convert.FromBase64String(storedHash);

                using (var hmac = new HMACSHA256(saltBytes)) // Salt ile HMACSHA256 kullanarak hash hesapla
                {
                    byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // Hash'lerin karşılaştırılması
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        if (hashBytes[i] != computedHash[i])
                        {
                            return false;
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                // Base64 formatında hata durumunda bir hata mesajı ver
                Console.WriteLine("Base64 string invalid: " + ex.Message);
                return false;
            }

            return true;
        }
    }
}
