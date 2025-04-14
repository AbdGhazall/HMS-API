namespace HMS.API.Abstraction.Entities.Billing
{
    public class BillingRequest
    {
        public int PatientId { get; set; }
        public string BillingStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}