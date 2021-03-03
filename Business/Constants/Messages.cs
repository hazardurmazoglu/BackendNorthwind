using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz.";
        public static string MaintenanceTime = "Bakım Zamanı";
        public static string ProductsListed = "Ürünler listelendi.";
        public static string ProductDetailsListed="Ürün detayları listelendi.";
        public static string ProductCountOfCategoryError="Bir kategoride en fazla 10 ürün olabilir.";
        public static string ProductNameAlreadyExist="Bu isimde zaten başka bir ürün var.";
        public static string CategoryLimitExceeded="Kategori limiti aşıldığı için yeni ürün eklenemiyor.";
        public static string AuthorizationDenied="Yetkiniz yok.";
        public static string UserRegistered ="Kayıt Olundu.";
        public static string UserNotFound="Kullanıcı bulunamadı.";
        public static string PasswordError="Parola Hatası.";
        public static string SuccessfulLogin="Başarılı giriş.";
        public static string UserAlreadyExists="Kullanıcı mevcut.";
        public static string AccessTokenCreated="Token Oluşturuldu.";
    }
}
