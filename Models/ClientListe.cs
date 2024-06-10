using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class ClientListe
    {
        public Client client { get; set; }

        public Abonnement abonnement { get; set; }

        public DateTime dateDeb { get; set; }
        public DateTime dateFin { get; set; }

        public int situation { get; set; }

        public ClientListe() { }

        public ClientListe(Client c, Abonnement ab, DateTime db, DateTime df, int s)
        {
            client = c;
            abonnement = ab;
            dateDeb = db;
            dateFin = df; 
            situation = s;
        }

    }
}
