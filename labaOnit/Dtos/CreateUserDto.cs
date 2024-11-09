namespace labaOnit.Dtos
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }
        public string RecordBook { get; set; }

        public string BirthdayDate { get; set; }
        public string ReceiptDate { get; set; }
    }
}
