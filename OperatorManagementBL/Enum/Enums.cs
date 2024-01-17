namespace OperatorManagementBL.Enum
{
    public enum TransactionTypeEnum
    {
        sms = 1,
        call = 2
    }

    public enum SimTypeEnum
    {
        permanent = 1,
        credit = 2
    }

    public enum CallFailedEnum
    {
        ok = 0,
        insuffienceBalance = 1,
        inactiveFromSimcard = 2,
        inactiveToSimcard = 3
    }

    public enum SortTypeEnum
    {
        Newest = 0,
        Oldest = 1,
        Longest = 2
    }
}
