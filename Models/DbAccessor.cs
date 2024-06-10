using System.Collections.Generic;
using System.Data.SqlClient;

namespace MyApp.Models
{
    public class DbAccessor
    {
        private readonly string _connectionString;

        public DbAccessor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<AbonnementDispo> AbonnementDispos(int idClient)
        {
            var AbonnementDispos = new List<AbonnementDispo>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT vabcsmin.*, ab.description  from v_abonnements_chaine_signalMin vabcsmin JOIN abonnements ab on ab.id = vabcsmin.idAbonnement WHERE signalMin >= (SELECT t_reception_signal FROM v_client_region WHERE idClient = " + idClient + ")", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string idAbonnement = reader["idAbonnement"].ToString();
                        Abonnement ab = new Abonnement().getAbonnementById(idAbonnement);
                        var abDispo = new AbonnementDispo
                        (
                            ab,
                            (float)reader["signalMin"]
                        );

                        AbonnementDispos.Add(abDispo);
                    }
                }
            }

            return AbonnementDispos;
        }

        public List<ClientListe> GetClientDetails(int id)
        {
            var clientListes = new List<ClientListe>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT idClient, nom, idAbonnement, description, dateDeb, dateFin, situation FROM v_clientliste where idClient = " + id + " ORDER BY dateFin  DESC", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string idClient = reader["idClient"].ToString();
                        var client = new Client().getClientById(idClient);
                        //nom = reader["nom"].ToString();
                        string idAbonnement = reader["idAbonnement"].ToString();
                        var ab = new Abonnement().getAbonnementById(idAbonnement);
                        //description = reader["description"].ToString();
                        DateTime dateDeb = (DateTime)reader["dateDeb"];
                        DateTime dateFin = (DateTime)reader["dateFin"];
                        int situation = (int)reader["situation"];

                        ClientListe clientListe = new ClientListe(
                            client,
                            ab, 
                            dateDeb,
                            dateFin, 
                            situation
                        );
                        clientListes.Add(clientListe);
                    }
                }
            }

            return clientListes;
        }

        public List<ClientListe> GetClientListeABenCours()
        {
            var clientListes = new List<ClientListe>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT idClient, nom, idAbonnement, description, dateDeb, dateFin, situation FROM v_clientliste where situation = 0", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string idClient = reader["idClient"].ToString();
                        var client = new Client().getClientById(idClient);
                        //nom = reader["nom"].ToString();
                        string idAbonnement = reader["idAbonnement"].ToString();
                        var ab = new Abonnement().getAbonnementById(idAbonnement);
                        //description = reader["description"].ToString();
                        DateTime dateDeb = (DateTime)reader["dateDeb"];
                        DateTime dateFin = (DateTime)reader["dateFin"];
                        int situation = (int)reader["situation"];

                        ClientListe clientListe = new ClientListe(
                            client,
                            ab, 
                            dateDeb,
                            dateFin, 
                            situation
                        );
                        clientListes.Add(clientListe);

                    }
                }
            }

            return clientListes;
        }
    }

}
