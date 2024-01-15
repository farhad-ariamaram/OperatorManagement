using System;

namespace OperatorManagementBL.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FromSimNumber { get; set; }
        public string ToSimNumber { get; set; }
        public string TransactionType { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
