using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;

namespace WebApplication2
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Autenticado"] == null || !(bool)Session["Autenticado"])
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            //List<string> errores = new List<string>();
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string email = txtEmail.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();
            string edad = txtEdad.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string cP = txtCodigoPostal.Text.Trim();
            string fechaNacimiento = txtFechaNacimiento.Text.Trim();
            string promedio = txtPromedio.Text.Trim();
            string patron = "Hash";

            //Conexion a base de datos
            string conectar = ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
            SqlConnection sqlConectar = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand("UserRegister", sqlConectar)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Usuario", email);
            cmd.Parameters.AddWithValue("@Contrasenia", contraseña);
            cmd.Parameters.AddWithValue("@Patron", patron);
            cmd.Parameters.AddWithValue("@Nombre", nombreUsuario);
            cmd.Parameters.AddWithValue("@Edad", int.Parse(edad));
            cmd.Parameters.AddWithValue("@Direccion", direccion);
            cmd.Parameters.AddWithValue("@Cp", cP);
            cmd.Parameters.Add("@Promedio", SqlDbType.Float).Value = promedio;
            cmd.Parameters.AddWithValue("@Nacimiento", DateTime.Parse(fechaNacimiento));
            
            cmd.Connection.Open();
            try
            {
                cmd.ExecuteNonQuery();
                string script = "<script>alert('Usuario Registrado'); window.location.href = 'Registro.aspx';</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }
            catch
            {
                string script = "<script>alert('Este correo ya esta asociado a otro usuario');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }

            cmd.Connection.Close();
        }

        protected void txtNombreUsuario_TextChanged(object sender, EventArgs e)
        {
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            if (!IsValidName(nombreUsuario))
            {
                errorNombre.InnerText = "El nombre de usuario solo puede contener letras";
                errorNombre.Visible = true;
            }
            else
            {
                errorNombre.Visible = false;
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (!IsValidEmail(email))
            {
                errorEmail.InnerText = "El correo electrónico no es válido";
                errorEmail.Visible = true;
            }
            else
            {
                errorEmail.Visible = false;
            }
        }

        protected void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            string contraseña = txtContraseña.Text.Trim();
            if (!IsValidPassword(contraseña))
            {
                errorContraseña.InnerText = "La contraseña no es segura, debe contener al menos: "
                                          + "1 letra mayuscula "
                                          + "1 letra minuscula "
                                          + "1 caracter especial "
                                          + "entre 8-12 caracteres (sin espacios)";
                errorContraseña.Visible = true;
            }
            else
            {
                errorContraseña.Visible = false;
            }
        }

        protected void txtFechaNacimiento_TextChanged(object sender, EventArgs e)
        {
            string fechaNacimiento = txtFechaNacimiento.Text.Trim();
            if (!IsValidDate(fechaNacimiento))
            {
                errorFecha.InnerText = "La fecha de nacimiento no es válida";
                errorFecha.Visible = true;
            }
            else
            {
                errorFecha.Visible = false;
            }
        }

        protected void txtEdad_TextChanged(object sender, EventArgs e)
        {
            string edad = txtEdad.Text.Trim();
            string fechaNacimiento = txtFechaNacimiento.Text.Trim();
            if (!IsValidAge(edad, fechaNacimiento))
            {
                errorEdad.InnerText = "La edad no es válida o no coincide con tu fecha de nacimiento";
                errorEdad.Visible = true;
            }
            else
            {
                errorEdad.Visible = false;
            }
        }

        protected void txtCodigoPostal_TextChanged(object sender, EventArgs e)
        {
            string cP = txtCodigoPostal.Text.Trim();
            if (!IsValidCP(cP))
            {
                errorCP.InnerText = "Él código postal no es válido";
                errorCP.Visible = true;
            }
            else
            {
                errorCP.Visible = false;
            }
        }

        protected void txtPromedio_TextChanged(object sender, EventArgs e)
        {
            string promedio = txtPromedio.Text.Trim();
            if (!IsValidPromedio(promedio))
            {
                errorProm.InnerText = "El promedio no es válido";
                errorProm.Visible = true;
            }
            else
            {
                errorProm.Visible = false;
            }
        }

        private bool IsValidName(string nombreUsuario)
        {
            // Expresión regular que verifica si el nombre contiene solo letras
            string pattern = @"^[a-zA-ZñÑ\s]+$";
            return Regex.IsMatch(nombreUsuario, pattern);
        }
        private bool IsValidEmail(string email)
        {
            // Validación de correo electrónico mediante expresión regular
            string pattern = @"^[a-zA-ZñÑ0-9._%+-]+@[a-zA-ZñÑ0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPassword(string contraseña)
        {
            // Validación de contraseña *al menos 1 minuscula y una mayuscula, al menos 1 digito, al menos 1 caracter especial y long 8-12
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,12}$";
            return Regex.IsMatch(contraseña, pattern);
        }

        private bool IsValidDate(string fecha)
        {
            int calcEdad;
            // Validación de fecha de nacimiento (formato YYYY-MM-DD)
            DateTime date;
            if(DateTime.TryParseExact(fecha, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out date))
            {
                if (date.Year <= (DateTime.Today.Year - 18))
                {
                    calcEdad = DateTime.Today.Year - date.Year;
                    if (date.Month > DateTime.Today.Month)
                    {
                        --calcEdad;
                        txtEdad.Text = calcEdad.ToString();
                        //txtEdad.Enabled = false;
                    }
                    return true;
                }
            }
            return false;
        }

        private bool IsValidPromedio(string promedio)
        {
            // Validación de promedio (numérico)
            float result;
            if(float.TryParse(promedio, out result))
            {
                if (result >= 0 && result <= 10)
                    return true;
            }
            return false;
        }

        private bool IsValidAge(string edad, string fecha)
        {
            // Validación de edad en valor entero
            int age, calcEdad;
            DateTime date1;
            DateTime.TryParseExact(fecha, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out date1);
            if (int.TryParse(edad, out age))
            {
                // Verificar si la edad es positiva y mayor que cero
                if (age > 0 && age < 100)
                {
                    //Verificar que la edad coincida con la fecha de nacimiento
                    calcEdad = DateTime.Today.Year - date1.Year;
                    if(date1.Month > DateTime.Today.Month)
                    {
                        --calcEdad;
                        if(calcEdad == age)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool IsValidCP(string cP)
        {
            // Validación de codigo postal en valor entero
            int CP;
            if (int.TryParse(cP, out CP))
            {
                // Verificar si el CP es mayor que cero
                if (CP >= 1000 && CP < 99998)
                {
                    return true;
                }
            }
            return false;
        }

    }
}