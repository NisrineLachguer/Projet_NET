namespace WpfApp1.Models
{
    public class Payment
    {
        // Constructor
        public Payment(int id, Reservation reservation, decimal amount, DateTime paymentDate, string paymentType, PaymentState? paymentState = null)
        {
            Id = id;
            Reservation = reservation;
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentType = paymentType;
            PaymentState = paymentState ?? PaymentState.Pending;  
        }

       
        public int Id { get; set; }
        public Reservation Reservation { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public PaymentState PaymentState { get; set; }
    }
}