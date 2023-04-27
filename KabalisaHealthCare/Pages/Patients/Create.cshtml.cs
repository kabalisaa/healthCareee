using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static KabalisaHealthCare.Pages.Patients.IndexModel;

namespace KabalisaHealthCare.Pages.Patients
{
    public class CreateModel : PageModel
    {
        public PatientInfo patientInfo = new PatientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() { 
            patientInfo.name = Request.Form["name"];
            patientInfo.email = Request.Form["email"];
            patientInfo.phone = Request.Form["phone"];
            patientInfo.address = Request.Form["address"];

            if(patientInfo.name.Length == 0 || patientInfo.email.Length == 0
               || patientInfo.phone.Length == 0 || patientInfo.address.Length == 0) 
                    {
                errorMessage = "all field are required!";
                return;
            }

            //save the date 
            try {
                String connectionString = "Data Source=KABALISA-PC\\KABALISA;Initial Catalog=kabalisaHealthCareDB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "INSERT INTO Patients(name, email, phone, address) VALUES(@name, @email, @phone, @address)"; 
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@name", patientInfo.name);
                        cmd.Parameters.AddWithValue("@email", patientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", patientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", patientInfo.address);
                        cmd.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception ex) {
                errorMessage =  ex.Message;
                return;
            }
            patientInfo.name = ""; patientInfo.email = "";
            patientInfo.phone = ""; patientInfo.address = "";
            successMessage = "New Patient added with Success";
            Response.Redirect("/Patients/Index");
        }
    }
}
