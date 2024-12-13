using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Messages
{
    public enum ErrorCode
    {
        VAL1001, // Zorunlu alan eksik veya null
        VAL1002, // Gönderilen değer izin verilen alanların dışında. (Negatif veya sınır aşımı.) 
        VAL1003, // Format hatası (e-mail)
        VAL1004, // Karakter sınır aşımı.

        AUTH2001, // Kullanıcı doğrulama hatası
        AUTH2002, // Yetkisiz erişim denemesi
        AUTH2003, // Kullanıcı hesabı dondurulmuş
        AUTH2004, // Şifre hatalı
        AUTH2005, // Şifre deneme sınırı aşıldı
        AUTH2010, // Token süresi dolmuş 
        AUTH2011, // Geçersiz token


        DB3001, // Veritabanına ulaşılamıyor.

        BUS4001, // İlgili veri zaten mevcut
    }
}
