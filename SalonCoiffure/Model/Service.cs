using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonCoiffure.Model
{
    public class Service
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }

        // Relation many-to-many, sans table explicite de jointure
        public ICollection<Facture> Factures { get; set; } = new List<Facture>();
    }

}
