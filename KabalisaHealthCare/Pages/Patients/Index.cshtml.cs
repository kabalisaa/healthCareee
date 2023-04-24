using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace KabalisaHealthCare.Pages.Patients
{
    public class IndexModel : PageModel
    {
        public List<PatientInfo> listPatients = new List<PatientInfo>();
        public void OnGet()
        {
            listPatients.Clear();
            try {
                String connectionString = "Data Source=KABALISA-PC\\KABALISA;Initial Catalog=kabalisaHealthCareDB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlquery = "SELECT * FROM Patients";
                    using(SqlCommand cmd = new SqlCommand(sqlquery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientInfo patientInfo = new PatientInfo();
                                patientInfo.id = "" + reader.GetInt32(0);
                                patientInfo.name = reader.GetString(1);
                                patientInfo.email = reader.GetString(2);
                                patientInfo.phone = reader.GetString(3);
                                patientInfo.address = reader.GetString(4);
                                patientInfo.createdAt = reader.GetDateTime(5).ToString();
                                listPatients.Add(patientInfo);

                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception" + ex.Message);
            }
        }

        public class PatientInfo
        {
            public string id;
            public string name;
            public string email;
            public string phone;
            public string address;
            public string createdAt;
        }
    }
}
