namespace OperatorManagementBL.DTOs
{
    public class WalletDTO
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int SimTypeId { get; set; }
        public string Person { get; set; }
        public string Number { get; set; }
    }
}
