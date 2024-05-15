using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Login : System.Web.UI.Page
    {
        private static int intentos = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string email = txtUsuario.Text.Trim();
            string patron = "Hash";
            string conectar = ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
            SqlConnection sqlConectar = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand("UserLogin", sqlConectar)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Connection.Open();
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = txtUsuario.Text;
            cmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar, 50).Value = txtContraseña.Text;
            cmd.Parameters.Add("@Patron", SqlDbType.VarChar, 50).Value = patron;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Session["Autenticado"] = true;
                dr.Close();
                SqlCommand cmdAdmin = new SqlCommand("VerificarAdmin", sqlConectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmdAdmin.Parameters.AddWithValue("@Usuario", email); ;

                SqlDataReader drAdmin = cmdAdmin.ExecuteReader();

                if (drAdmin.Read())
                {
                    bool isAdmin = drAdmin.GetBoolean(drAdmin.GetOrdinal("EsAdministrador"));
                    drAdmin.Close();

                    if (isAdmin)
                    {
                        Session["esAdmin"] = true;
                        Response.Redirect("Registro.aspx");
                    }
                    else
                    {
                        SqlCommand cmdDatos = new SqlCommand("MostrarDatos", sqlConectar)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        cmdDatos.Parameters.AddWithValue("@Usuario", email);

                        SqlDataReader drDatos = cmdDatos.ExecuteReader();

                        if (drDatos.Read())
                        {
                            Session["usuario"] = txtUsuario.Text;
                            Session["datos"] = new
                            {
                                UserId = drDatos.GetInt32(drDatos.GetOrdinal("user_id")),
                                Nombre = drDatos.GetString(drDatos.GetOrdinal("Nombre")),
                                Edad = drDatos.GetInt32(drDatos.GetOrdinal("Edad")),
                                Direccion = drDatos.GetString(drDatos.GetOrdinal("Direccion")),
                                Cp = drDatos.GetString(drDatos.GetOrdinal("Cp")),
                                Promedio = drDatos.GetDouble(drDatos.GetOrdinal("Promedio")),
                                Nacimiento = drDatos.GetDateTime(drDatos.GetOrdinal("Nacimiento")).ToShortDateString(),
                            };
                            Session["datosCita"] = new
                            {
                                idUsuario = drDatos.GetInt32(drDatos.GetOrdinal("user_id")),
                                Promedio = drDatos.GetDouble(drDatos.GetOrdinal("Promedio")),
                                Cita = drDatos.GetDateTime(drDatos.GetOrdinal("FechaCita"))
                            };
                            drDatos.Close();
                            Response.Redirect("Index.aspx");
                        }
                    }
                }
            }
            else
            {
                intentos += 1;
                if (intentos == 5)
                {
                    mensajeError.InnerText = "Por favor espera 30 segundos para volver a intentarlo";
                    string script = "ocultarBoton();";
                    ClientScript.RegisterStartupScript(this.GetType(), "ocultarBoton", script, true);
                    intentos = 0;
                }
                else
                {
                    mensajeError.InnerText = "Credenciales incorrectas";
                }
            }

            cmd.Connection.Close();
        }
    }
}