using System.Collections.Generic;

namespace OperatorManagementBL.DTOs
{
    //DTO استفاده در صفحه لیست تراکنش ها
    public class TransactionPageDTO
    {
        public List<TransactionDTO> TransactionsList { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int ResultCount { get; set; }
    }
}
