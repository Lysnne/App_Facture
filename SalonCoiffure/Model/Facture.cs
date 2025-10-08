namespace SalonCoiffure.Model
{
    public class Facture
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double PrixTotal { get; set; }

        // FK
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Collection de services associés à cette facture
        public ICollection<Service> Services { get; set; } = new List<Service>();

        public Paiement Paiement { get; set; }
    }

}
