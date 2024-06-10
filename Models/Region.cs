using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace MyApp.Models
{
    public class Region
    {
        public long id { get; set; }

        public string description { get; set; }

        public float t_reception_signal { get; set; }
    
    public Region(){}

    public Region(long i, string d, float t){
        this.id = i;
        this.description = d;
        this.t_reception_signal = t; 
    }

    public Region getRegionById() {
        return getRegionById(id.ToString());
    }

    public Region getRegionById(string id) {
        string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
        List<Region> liste=new List<Region>();
        SqlConnection connection = new SqlConnection(connetionString);
        connection.Open();       
        String sql = "SELECT * FROM regions where id = "+id;
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        try{
            reader.Read();
            Region r=new Region((long)reader.GetValue(0), (string)reader.GetValue(1), (float)reader.GetValue(2));
            return r;
        }finally{
            reader.Close();
            command.Dispose();
            connection.Close();
        }
    }

    public List<Region> getAllRegions(){
        string connetionString = "Server=p15A-80-Fyantra\\SQLEXPRESS;Database=canalsat;Trusted_connection=True;";
        List<Region> liste=new List<Region>();
        SqlConnection connection = new SqlConnection(connetionString);
        connection.Open();       
        String sql = "SELECT * FROM regions";
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader reader = command.ExecuteReader();
        try{
            while (reader.Read())
            {
                Region Region=new Region((long)reader.GetValue(0), (string)reader.GetValue(1), (float)reader.GetValue(2));
                liste.Add(Region);
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
