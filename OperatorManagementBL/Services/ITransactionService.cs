
using OperatorManagementBL.DTOs;
using System.Collections.Generic;

namespace OperatorManagementBL.Services
{
    public interface ITransactionService
    {
        List<TransactionDTO> GetTransactions();

        int MakeCall(int from, int to, int type, int duration);
    }
}
