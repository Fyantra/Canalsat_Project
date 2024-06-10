namespace MyApp.Models
{
    public class MyUtil
    {
        public MyUtil() { }

        public AbonnementClient GetNewAbClient(Client client, Abonnement ab)
        {
            Abonnement abEnCours = client.GetAbonnementEncours();
            DateTime dateDeb = DateTime.Now;
            DateTime dateFin = dateDeb.AddDays(30);
            AbonnementClient newAB = new AbonnementClient(
                client,
                ab,
                dateDeb,
                dateFin
            );

            if (abEnCours != null)
            {
                AbonnementClient lastAbonnement = client.GetLastAbonnement();
                dateDeb = lastAbonnement.dateFin.AddDays(1); //ajout d'1 jour de la fin du last pour le début
                dateFin = dateDeb.AddDays(30);
                newAB = new AbonnementClient(
                    client,
                    ab,
                    dateDeb,
                    dateFin
                );
            }

            return newAB;
        }
        public List<Chaine> toListeChaines(string[] chaineId_strs)
        {
            List<Chaine> rep = new List<Chaine>();
            Chaine c = new Chaine();
            foreach (string chaineId in chaineId_strs)
            {
                c = new Chaine().getChaineById(chaineId);
                rep.Add(c);
            }
            return rep;
        }
    }
}