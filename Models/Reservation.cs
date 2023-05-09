namespace tuto.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime DateArrivee { get; set; }
        public DateTime DateDepart { get; set; }

        public int IdClient { get; set; } // pour creer une cle etrangere
        public Client? Client { get; set; } //  propriété de navigation : pour définir la relation entre Reservation et Client

        public int IdChambre { get; set; }
        public Chambre Chambre { get; set; }

        
        //public Facture? Facture { get; set; } // propriété pour faire référence à la facture associée
    }
}
