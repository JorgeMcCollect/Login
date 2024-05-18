using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Autenticado"] != null && (bool)Session["Autenticado"])
            {
                if (Session["esAdmin"] != null && (bool)Session["esAdmin"])
                {
                    Response.Redirect("Registro.aspx");
                }
                else
                {
                    dynamic usuario = Session["usuario"];
                    dynamic datos = Session["datos"];
                    lblInfo.Text = "Bienvenido " + datos.Nombre + " verifica tus datos: ";
                    lblCorreo.Text = usuario;
                    lblEdad.Text = datos.Edad.ToString();
                    lblDireccion.Text = datos.Direccion;
                    lblCP.Text = datos.Cp;
                    lblPromedio.Text = datos.Promedio.ToString();
                    lblFN.Text = datos.Nacimiento;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void btnCita_Click(object sender, EventArgs e)
        {
            dynamic datosCita = Session["datosCita"];
            int idUsuario = datosCita.idUsuario;
            double promedio = datosCita.Promedio;

            if (datosCita.Cita.ToString() == "01/01/1900 12:00:00 a. m.")
            {
                string conectar = DB.Conectando();//ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
                SqlConnection sqlConectar = new SqlConnection(conectar);
                SqlCommand cmdCita = new SqlCommand("GenerarCita", sqlConectar)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmdCita.Connection.Open();
                cmdCita.Parameters.AddWithValue("@UserId", idUsuario);
                cmdCita.Parameters.AddWithValue("@Promedio", promedio);

                SqlDataReader drCita = cmdCita.ExecuteReader();

                if (drCita.Read())
                {
                    drCita.Close();
                    cmdCita.Connection.Close();
                    Response.Redirect("Cita.aspx");
                }
                else
                {
                    cmdCita.Connection.Close();
                    string script = "<script>alert('Algo salio mal');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }
            }
            else
            {
                Response.Redirect("Cita.aspx");
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Session["Autenticado"] = false;
            Response.Redirect("Login.aspx");
        }
    }
}