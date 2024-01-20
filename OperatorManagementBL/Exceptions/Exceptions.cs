using System;

namespace OperatorManagementBL.Exceptions
{
    [Serializable]
    public class DuplicateNationCodeException : Exception
    {
        public DuplicateNationCodeException(): base(String.Format("کد ملی تکراری است")){}
    }

    [Serializable]
    public class DuplicateSimNumberException : Exception
    {
        public DuplicateSimNumberException() : base(String.Format("شماره سیم‌کارت تکراری است")) { }
    }
}
