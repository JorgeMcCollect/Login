using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WebApplication2
{
    public partial class Cita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Autenticado"] == null || !(bool)Session["Autenticado"])
            {
                Response.Redirect("Login.aspx");
            }
            //dynamic datosCita = Session["datosCita"];
            dynamic email = Session["usuario"];

            string conectarC = ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
            SqlConnection sqlConectarC = new SqlConnection(conectarC);
            SqlCommand cmdDatosC = new SqlCommand("MostrarDatos", sqlConectarC)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmdDatosC.Connection.Open();
            cmdDatosC.Parameters.AddWithValue("@Usuario", email);

            SqlDataReader drDatosC = cmdDatosC.ExecuteReader();

            if (drDatosC.Read())
            {
                lblCita.Text = "Nombre:" +
                drDatosC.GetString(drDatosC.GetOrdinal("Nombre")) + "<br/>"
                + "Email: "
                + email + "<br/>"
                + "Edad: "
                + drDatosC.GetInt32(drDatosC.GetOrdinal("Edad")) + "<br/>"
                + "Direcion: "
                + drDatosC.GetString(drDatosC.GetOrdinal("Direccion")) + "<br/>"
                + "CP: "
                + drDatosC.GetString(drDatosC.GetOrdinal("Cp")) + "<br/>"
                + "Promedio: "
                + drDatosC.GetDouble(drDatosC.GetOrdinal("Promedio")) + "<br/>"
                + "Fecha de nacimiento: "
                + drDatosC.GetDateTime(drDatosC.GetOrdinal("Nacimiento")).ToShortDateString() + "<br/>"
                +"Cita programada para el: " 
                + drDatosC.GetDateTime(drDatosC.GetOrdinal("FechaCita")).ToShortDateString();
               
                drDatosC.Close();
                cmdDatosC.Connection.Close();

                string[] lineas = lblCita.Text.Split(new string[] { "<br/>" }, StringSplitOptions.None);

                Document docCita = new Document();
                string rutaPDF = @"C:\LOGIN_Jorge\pdf\";
                if (!Directory.Exists(rutaPDF))
                {
                    Directory.CreateDirectory(rutaPDF); 
                }
                string ruta = rutaPDF + email + ".pdf";

                PdfWriter.GetInstance(docCita, new FileStream(ruta, FileMode.Create));
                docCita.Open();

                foreach (string linea in lineas)
                {
                    // Dividir cada línea en partes separadas utilizando ':'
                    string[] partes = linea.Split(':');

                    // Añadir al documento PDF las partes relevantes de la línea
                    if (partes.Length == 2)
                    {
                        string etiqueta = partes[0].Trim(); // Obtener la etiqueta (nombre del dato)
                        string valor = partes[1].Trim(); // Obtener el valor del dato

                        // Agregar la etiqueta y el valor como párrafos al documento PDF
                        docCita.Add(new Paragraph(etiqueta + ": " + valor));
                    }
                }
                docCita.Close();
            }
        }
        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            dynamic email = Session["usuario"];
            string rutaPDF = @"C:\LOGIN_Jorge\pdf\";
            /*if (!Directory.Exists(rutaPDF))
            {
                Directory.CreateDirectory(rutaPDF);
            }*/
            string ruta = rutaPDF + email + ".pdf";
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + (email + ".pdf"));
            Response.TransmitFile(ruta);
            Response.End();
        }
    }
}