namespace OperatorManagementBL.Enum
{
    //Enum انواع تراکنش
    //جدول TransactionType
    //جدول Cost
    public enum TransactionTypeEnum
    {
        sms = 1,
        call = 2
    }

    //Enum نوع سیم‌کارت
    //جدول SimType
    public enum SimTypeEnum
    {
        permanent = 1,
        credit = 2
    }

    //Enum انواع وضعیت تماس
    //در دو تابع زیر استفاده شده جهت برگرداندن وضعیت تماس/پیامک انجام شده
    //SendSMS و MakeCall
    public enum CallFailedEnum
    {
        ok = 0,
        insuffienceBalance = 1,
        inactiveFromSimcard = 2,
        inactiveToSimcard = 3,
        unknown = 4
    }

    //Enum انواع وضعیت مرتب سازی جدول تراکنش ها
    public enum SortTypeEnum
    {
        Newest = 0,
        Oldest = 1,
        Longest = 2
    }
}
