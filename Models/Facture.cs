namespace tuto.Models
{
    public class Facture
    {
        public int Id { get; set; }
        public float Montant { get; set; }
        public DateTime DateFacture { get; set; }

        //public int IdReservation { get; set; } // clé étrangère
        //public Reservation? Reservation { get; set; }

        public int IdClient { get; set; }
        public Client Client { get; set; }
    }
}
