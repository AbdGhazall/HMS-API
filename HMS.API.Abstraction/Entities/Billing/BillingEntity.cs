namespace HMS.API.Abstraction.Entities.Billing
{
    public class BillingEntity
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int BillingStatusId { get; set; }
        public decimal Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}