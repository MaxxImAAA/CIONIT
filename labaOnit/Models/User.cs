namespace labaOnit.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }
        public string RecordBook { get; set; }
        public DateTime BirthdayDate { get; set; }
        public DateTime ReceiptDate { get; set; }
    }
}
