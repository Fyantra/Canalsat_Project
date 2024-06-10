using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class AbonnementDispo 
    {
        public Abonnement abonnement { get; set; }

        public float signalMin { get; set; }

        public AbonnementDispo(){}
        public AbonnementDispo(Abonnement a, float f) {
            abonnement = a;
            signalMin = f;
        }
    }
}
