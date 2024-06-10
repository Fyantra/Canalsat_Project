using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace MyApp.Models
{
    public class Remise
    {
        public long id { get; set; }

        public float remise { get; set; }

        public DateTime  dateDeb { get; set; }
        public DateTime  dateFin { get; set; }

        public Remise(){
        }

        public Remise(long i, float r, DateTime db, DateTime df) {
            id = i;
            remise = r;
            dateDeb = db;
            dateFin = df;
        }
    }
}