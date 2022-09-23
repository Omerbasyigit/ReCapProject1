using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string Added = "Eklendi";
        public static string Deleted = "Silindi";
        public static string Modified = "Güncellendi";
        public static string Listed = "Listelendi";
        public static string NameInvalid = "HATA";
        public static string Detail = "Detay getirildi";
        public static string GetById = "Id ye göre getirildi";
        public static string RentalCheck = "Kiralanan araba teslim edilmedi";
        public static string Available = "Müsait";
        public static string System = "Sistem Bakımda";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError="Şifre hatası";
        public static string SuccessfulLogin="Giriş başarılı";
        public static string UserAlreadyExist="Kullanıcı kayıtlı";
        public static string CarNotFound = "Araba bulunamadı";

        public static string UserRegistered = "Kayıt Başarılı";
        public static string AccessTokenCreated="Access token oluşturuldu";
        public static string AuthorizationDenied="Yetkin yok";
        public static string ColorNotFound = "Renk Bulunamadı";
        public static string BrandNotFound = "Marka bulunamadı";
    }
}
