using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class ChatController : Controller
    {
        private readonly string connectionString =
            "Data Source=KF-PC-71\\SQLEXPRESS;Initial Catalog=ZABC;Integrated Security=True;TrustServerCertificate=True;";

        [HttpPost]
        public JsonResult GetAnswer(string message)
        {
            string question = message.ToLower();
            string answer = "I can only answer about Wareesha Ali.";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                if (question.Contains("skill"))
                {
                    var cmd = new SqlCommand("SELECT SkillName FROM Skills", conn);
                    var reader = cmd.ExecuteReader();
                    var skills = new List<string>();
                    while (reader.Read())
                    {
                        skills.Add(reader["SkillName"].ToString());
                    }
                    reader.Close();
                    answer = "My skills are: " + string.Join(", ", skills);
                }
                else if (question.Contains("experience"))
                {
                    var cmd = new SqlCommand("SELECT Description FROM Experience", conn);
                    var reader = cmd.ExecuteReader();
                    var exp = new List<string>();
                    while (reader.Read())
                    {
                        exp.Add(reader["Description"].ToString());
                    }
                    reader.Close();
                    answer = "My experience includes: " + string.Join("; ", exp);
                }
                else if (question.Contains("education"))
                {
                    var cmd = new SqlCommand("SELECT Education FROM PersonalInfo", conn);
                    answer = (string)cmd.ExecuteScalar();
                }
                else if (question.Contains("contact"))
                {
                    var cmd = new SqlCommand("SELECT Email, LinkedIn, GitHub FROM Contact", conn);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        answer = $"Email: {reader["Email"]}, LinkedIn: {reader["LinkedIn"]}, GitHub: {reader["GitHub"]}";
                    }
                    reader.Close();
                }
                else if (question.Contains("about wareesha") ||
         question.Contains("tell me about wareesha") ||
         question.Contains("something about wareesha") ||
         question.Contains("tell me something about wareesha") ||   // Add this
         question.Contains("who is wareesha") ||
         question.Contains("wareesha"))
                {
                    // ----- Skills -----
                    var skills = new List<string>();
                    var skillCmd = new SqlCommand("SELECT SkillName FROM Skills", conn);
                    var skillReader = skillCmd.ExecuteReader();
                    while (skillReader.Read())
                    {
                        skills.Add(skillReader["SkillName"].ToString());
                    }
                    skillReader.Close();

                    // ----- Experience -----
                    var exp = new List<string>();
                    var expCmd = new SqlCommand("SELECT Description FROM Experience", conn);
                    var expReader = expCmd.ExecuteReader();
                    while (expReader.Read())
                    {
                        exp.Add(expReader["Description"].ToString());
                    }
                    expReader.Close();

                    // ----- Education -----
                    var eduCmd = new SqlCommand("SELECT Education FROM PersonalInfo", conn);
                    string education = (string)eduCmd.ExecuteScalar();

                    // ----- Contact -----
                    string contactInfo = "";
                    var contactCmd = new SqlCommand("SELECT Email, LinkedIn, GitHub FROM Contact", conn);
                    var contactReader = contactCmd.ExecuteReader();
                    if (contactReader.Read())
                    {
                        contactInfo = $"Email: {contactReader["Email"]}, LinkedIn: {contactReader["LinkedIn"]}, GitHub: {contactReader["GitHub"]}";
                    }
                    contactReader.Close();

                    // Final Answer
                    answer = $"Here is about Wareesha Ali:\n" +
                             $"- Skills: {string.Join(", ", skills)}\n" +
                             $"- Experience: {string.Join("; ", exp)}\n" +
                             $"- Education: {education}\n" +
                             $"- Contact: {contactInfo}";
                }
            }

            return Json(new { reply = answer }, JsonRequestBehavior.AllowGet);
        }
    }
}
