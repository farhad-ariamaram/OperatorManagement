using System.Collections.Generic;

namespace OperatorManagementBL.DTOs
{
    public class TransactionPageDTO
    {
        public List<TransactionDTO> TransactionsList { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int ResultCount { get; set; }
    }
}
