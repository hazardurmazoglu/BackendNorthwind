using System;
using System.Collections.Generic;
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
    }
}
