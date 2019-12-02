namespace TP.Pom
{
    public class BookingRecord
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Price { get; set; }
        public string Deposit { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }

        public bool Equals(BookingRecord other)
        {
            return other != null &&
                FirstName != null && FirstName.Equals(other.FirstName) &&
                Surname != null && Surname.Equals(other.Surname) &&
                Price != null && Price.Equals(other.Price) &&
                Deposit != null && Deposit.Equals(other.Deposit) &&
                CheckIn != null && CheckIn.Equals(other.CheckIn) &&
                CheckIn != null && CheckIn.Equals(other.CheckIn);
        }
    }
}
