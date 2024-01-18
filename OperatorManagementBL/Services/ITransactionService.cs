
using OperatorManagementBL.DTOs;
using System;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface ITransactionService
    {
        TransactionPageDTO GetTransactions(int pageId = 1, long fromDate = 0, long toDate = 0, int fromSimId = 0, int toSimId = 0, int fromPersonId = 0, int toPersonId = 0, int durationLessThan = 0, int durationMoreThan = 0, int typeId = 0, int sortType = 0, string search = "");

        int MakeCall(int from, int to, int type, int duration);
        int SendSMS(int from, int to, int type);
    }
}
