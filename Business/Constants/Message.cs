using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Message
    {
        public static string MaintenanceTime = "Bakım zamanı";
        public static string CarModelYear = "Düşük model araç";
        public static string CarAdded = "Araç eklendi";
        public static string CarListed = "Arabalar listelendi";
        public static string CarUpdated = "Araç güncellendi";
        public static string CarDelete = "Araç silindi";
        public static string CarNotAdded = "Aracınızın modeli düşük olduğundan eklenemedi";
        public static string RentalAdded = "Kiralanan araç eklendi.";
        public static string UserAdded = "Kullanıcı eklendi.";
        public static string CustomerAdded = "Müşteri eklendi.";
        public static string RentalErrorAdded = "Aracınız başka müşteride";
        public static string RentalGotById = "Seçilen araç";
        public static string UserListed = "Kullanıcılar listelendi.";
        public static string OverBrandCount = "En fazla 14 marka ekleyebilirsiniz";
        public static string OverCountOfCar = "Şirketimize 10 adet araçtan fazlası eklenememektedir.";
        public static string RentalNotAddedForReturnDate = "Aracımız şu an kirada";
    }
}
