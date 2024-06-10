using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace MyApp.Models
{
    public class Chaine
    {
        public long id { get; set; }

        public string description { get; set; }

        public float prix { get; set; }

        public float signal { get; set; }


            public Chaine(){}

    public Chaine(long i, string d, float p,float s){
        this.id = i;
        this.description = d;
        this.prix = p; 
        this.signal = s; 
    }

    public Chaine getChaineById() {
        return getChaineById(id.ToString());
    }

    public Chaine getChaineById(string id) {
        string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
        List<Chaine> liste=new List<Chaine>();
        SqlConnection connection = new SqlConnection(connetionString);
        connection.Open();       
        String sql = "SELECT * FROM Chaines where id = "+id;
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        try{
            reader.Read();
            Chaine c=new Chaine((long)reader.GetValue(0), (string)reader.GetValue(1), (float) reader.GetValue(2), (float) reader.GetValue(3));
            return c;
        }finally{
            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }

    public List<Chaine> getAllChaines(){
        string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
        List<Chaine> liste=new List<Chaine>();
        SqlConnection connection = new SqlConnection(connetionString);
        connection.Open();       
        String sql = "SELECT * FROM Chaines";
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        try{
            while (reader.Read())
            {
                Chaine c=new Chaine((long)reader.GetValue(0), (string)reader.GetValue(1), (float) reader.GetValue(2), (float) reader.GetValue(3));
                liste.Add(c);
            }
            return liste;
        }finally{
            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }

    }
}