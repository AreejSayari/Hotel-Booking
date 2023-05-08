namespace tuto.Models
{
    public class Client: LoginViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public Facture? Facture { get; set; } // propriété pour faire référence à la facture associée
    }
}
