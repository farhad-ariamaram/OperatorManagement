using System;

namespace OperatorManagementBL.Exceptions
{

    //Exception کاربر یافت نشد
    [Serializable]
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException() : base(String.Format("شخص یافت نشد")) { }
    }

    //Exception سیمکارت یافت نشد
    [Serializable]
    public class SimcardNotFoundException : Exception
    {
        public SimcardNotFoundException() : base(String.Format("سیم‌کارت یافت نشد")) { }
    }


    //Exception تکراری بودن کد ملی
    [Serializable]
    public class DuplicateNationCodeException : Exception
    {
        public DuplicateNationCodeException() : base(String.Format("کد ملی تکراری است")) { }
    }

    //Exception تکراری بودن شماره سیم‌کارت
    [Serializable]
    public class DuplicateSimNumberException : Exception
    {
        public DuplicateSimNumberException() : base(String.Format("شماره سیم‌کارت تکراری است")) { }
    }

    //Exception حذف بودن مالک سیمکارت در بازگردانی سیمکارت
    [Serializable]
    public class SimcardOwnerIsDeletedException : Exception
    {
        public SimcardOwnerIsDeletedException() : base(String.Format("مالک سیمکارت در وضعیت حذف شده قرار دارد")) { }
    }

    //Exception داشتن بدهی در تبدیل دائمی به اعتباری
    [Serializable]
    public class BillNotPaiedException : Exception
    {
        public BillNotPaiedException() : base(String.Format("سیمکارت موردنظر بدهی دارد، برای تبدیل به اعتباری ابتدا صورت‌حساب را پرداخت کنید")) { }
    }
}
