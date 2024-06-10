using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace MyApp.Models
{
    public class Client
    {
        public long id { get; set; }

        public string nom { get; set; }

        public Region region { get; set; }
    
        public Client(){}

    public Client(long i, string d, Region r){
        this.id = i;
        this.nom = d;
        this.region = r; 
    }

    public List<Chaine> GetChainesDispo(List<Chaine> lc) {
        return GetChainesDispo(id.ToString(), lc);
    }

    public List<Chaine> GetChainesDispo(string idClient, List<Chaine> lc) {
        List<Chaine> rep = new List<Chaine>();
        Client client = getClientById(idClient);
        foreach (Chaine chaine in lc)
        {
            if(client.region.t_reception_signal <= chaine.signal) {
                rep.Add(chaine);
            }
        }
        return rep;
    }   

    public AbonnementClient GetLastAbonnement() {
        string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
        List<Client> liste=new List<Client>();
        SqlConnection connection = new SqlConnection(connetionString);
        connection.Open();       
        String sql = "SELECT idClient, nom, idAbonnement, description, dateDeb, dateFin, situation FROM v_clientliste where idClient = "+id+" and dateFin = (select MAX(dateFin) from v_ClientListe where idClient = "+id+")";
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        try{
            if(reader.HasRows == true) {
                reader.Read();
                string idAbonnement = reader["idAbonnement"].ToString();
                Abonnement ab = new Abonnement().getAbonnementById(idAbonnement);
                AbonnementClient abc = new AbonnementClient(
                    this, 
                    ab,
                    Convert.ToDateTime(reader["dateDeb"]),
                    Convert.ToDateTime(reader["dateFin"])
                );
                return abc;
            }
            return null;
        }finally{
            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }

    public bool isThereAbEncours() {
        if(GetAbonnementEncours() == null) {
            return false;
        }
        return true;
    }

    public Abonnement GetAbonnementEncours() {
        return GetAbonnementEncours(id.ToString());
    }

    public Abonnement GetAbonnementEncours(string idClient) {
        string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
        List<Client> liste=new List<Client>();
        SqlConnection connection = new SqlConnection(connetionString);
        connection.Open();       
        String sql = "SELECT idClient, nom, idAbonnement, description, dateDeb, dateFin, situation FROM v_clientliste where situation = 0 AND idClient = "+idClient;
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        try{
            if(reader.HasRows == true) {
                reader.Read();
                string idAbonnement = reader["idAbonnement"].ToString();
                Abonnement ab = new Abonnement().getAbonnementById(idAbonnement);
                return ab;
            }
            return null;
        }finally{
            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }

    public Client getClientById() {
        return getClientById(id.ToString());
    }

    public Client getClientById(string id) {
        string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
        List<Client> liste=new List<Client>();
        SqlConnection connection = new SqlConnection(connetionString);
        connection.Open();       
        String sql = "SELECT * FROM clients where id = "+id;
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        try{
            reader.Read();
            Region r = new Region();
            r = r.getRegionById(reader["id"].ToString());
            Client c=new Client((long)reader.GetValue(0), (string)reader.GetValue(1), r);
            return c;
        }finally{
            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }

    public List<Client> getAllClients(){
        string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
        List<Client> liste=new List<Client>();
        SqlConnection connection = new SqlConnection(connetionString);
        connection.Open();       
        String sql = "SELECT * FROM clients";
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        try{
            while (reader.Read())
            {
                Region r = new Region();
                r = r.getRegionById(reader["id"].ToString());
                Client c=new Client((long)reader.GetValue(0), (string)reader.GetValue(1), r);
                liste.Add(c);
            }
            return liste;
        }finally{
            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }

    /*public void Insert()
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
        }*/
    }
}
