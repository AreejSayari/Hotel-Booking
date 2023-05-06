namespace tuto.Models
{
    public class Chambre
    {
        public int Id { get; set; }
        public int NumeroChambre { get; set; }
        public string Type { get; set; }
        public float Prix { get; set; }
        public ICollection<Reservation> Reservations { get; set; }


    }
}
