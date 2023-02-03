namespace Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email{ get; set; }
        public string Address { get; set; }
        public string Country{ get; set; }
        public string City{ get; set; }
        public DateTime RegistrationDate{ get; set; }
        public DateTime BirthDate{ get; set; }

        public Customer(Guid id, string firstName, string lastName, string? email, string address, string country, string city, DateTime registrationDate, DateTime birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            Country = country;
            City = city;
            RegistrationDate = registrationDate;
            BirthDate = birthDate;
        }
    }
}