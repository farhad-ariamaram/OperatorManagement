using System;

namespace OperatorManagementBL.Exceptions
{

    //Exception تکراری بودن کد ملی
    [Serializable]
    public class DuplicateNationCodeException : Exception
    {
        public DuplicateNationCodeException(): base(String.Format("کد ملی تکراری است")){}
    }

    //Exception تکراری بودن شماره سیم‌کارت
    [Serializable]
    public class DuplicateSimNumberException : Exception
    {
        public DuplicateSimNumberException() : base(String.Format("شماره سیم‌کارت تکراری است")) { }
    }
}
