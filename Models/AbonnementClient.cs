using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace MyApp.Models
{
    public class AbonnementClient
    {
        public Client client { get; set; }

        public Abonnement abonnement { get; set; }

        public DateTime dateDeb { get; set; }
        public DateTime dateFin { get; set; }

        public AbonnementClient(){}

        public AbonnementClient(Client c, Abonnement ab, DateTime db, DateTime df) {
            client = c;
            abonnement = ab;
            dateDeb = db;
            dateFin = df;
        }

        public void Insert()
        {
            string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
            SqlConnection connection = new SqlConnection(connetionString);
            SqlCommand command = new SqlCommand("INSERT INTO abonnements_clients (idAbonnement, idClient, dateDeb, dateFin) VALUES (@idAbonnement, @idClient,  @dateDeb, @dateFin)", connection);
            command.Parameters.AddWithValue("@idAbonnement", this.abonnement.id);
            command.Parameters.AddWithValue("@idClient", this.client.id);
            command.Parameters.AddWithValue("@dateDeb", this.dateDeb);
            command.Parameters.AddWithValue("@dateFin", this.dateFin);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                command.Dispose();
                connection.Close();
            }
        }
    }
}