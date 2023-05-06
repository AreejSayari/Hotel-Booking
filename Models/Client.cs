namespace tuto.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public Facture? Facture { get; set; } // propriété pour faire référence à la facture associée
    }
}
