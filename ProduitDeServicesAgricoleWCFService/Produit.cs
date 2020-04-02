using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProduitDeServicesAgricoleWCFService
{
    [DataContract]
    public class Produit
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nom { get; set; }
        [DataMember]
        public int Qty { get; set; }
        [DataMember]
        public double Prix { get; set; }

        public Produit()
        {

        }
    }
}
