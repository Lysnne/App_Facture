using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonCoiffure.Model
{
    public class Paiement
    {
        

        public int Id { get; set; }
        public double Montant { get; set; }
        public DateTime DatePaiement { get; set; }
        public string MoyenPaiement { get; set; }
        public Facture Facture { get; set; }
        public int FactureId { get; set; }
      
    }
}
