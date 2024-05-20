using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using MailKit.Net.Smtp;
//using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace WebApplication2
{
    public partial class RecuperarContraseña : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnEnviarCodigo_Click(object sender, EventArgs e)
        {
            string btn1 = "Enviar código";
            string btn2 = "Comprobar código";
            string email = txtUsuario.Text.Trim();

            if (btnEnviarCodigo.Text == btn1)
            {
                //Creamos una conexion para verificar si el correo existe en la BD
                string conectar = DB.Conectando();//ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
                using (SqlConnection sqlConectar = new SqlConnection(conectar))
                {
                    using (SqlCommand cmd = new SqlCommand("VerificarMail", sqlConectar))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario", email);

                        // Agregar el parámetro de salida
                        SqlParameter existeParam = new SqlParameter("@Existe", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(existeParam);

                        sqlConectar.Open();
                        cmd.ExecuteNonQuery();
                         
                        // Obtener el valor del parámetro de salida
                        bool mailExist = (bool)existeParam.Value;

                        if (mailExist)
                        {
                            // Creamos un identificador unico(GUID) y solo tomamos los primeros 8 caracteres
                            string codigo = Guid.NewGuid().ToString().Substring(0, 8);
                            Session["codigoVerificacion"] = codigo;

                            enviarCorreo(codigo, email);

                            System.Diagnostics.Debug.WriteLine("CODIGO:",codigo);

                            lblCorreo.Visible = false;
                            txtUsuario.Visible = false;

                            mensajeCodigo.InnerText = "Se ha enviado un código de verificación a su correo";
                            lblIntoCode.Text = "Introduce el código enviado a tu correo electrónico*";
                            txtCodigoUser.Visible = true;
                            btnEnviarCodigo.Text = "Comprobar código";
                        }
                        else
                        {
                            mensajeCodigo.InnerText = "El correo no se encuentra registrado en el sistema";
                        }
                        sqlConectar.Close();
                    }
                }
            }
            else if (btnEnviarCodigo.Text == btn2)
            {
                string codigoUsuario = txtCodigoUser.Text.Trim();
                dynamic codigoGenerado = Session["codigoVerificacion"];

                if(codigoUsuario == codigoGenerado)
                {
                    //mensajeCodigo.InnerText = "Los codigos coinciden";
                    lblCorreo.Visible = false;
                    txtUsuario.Visible = false;
                    mensajeCodigo.Visible = false;
                    lblIntoCode.Visible = false;
                    txtCodigoUser.Visible = false;

                    lblContrasenia.Text = "Introduce tu nueva contraseña*";
                    lblConfirmContra.Text = "Confirma tu contraseña*";
                    txtContrasenia.Visible = true;
                    txtConfirContra.Visible = true;
                    btnEnviarCodigo.Text = "Guardar contraseña";
                }
                else
                {
                    mensajeCodigo.InnerText = "Los codigos NO coinciden";
                }
            }
            else
            {
                /*mensajeCodigo.InnerText = "Se guardo la contraseña";
                mensajeCodigo.Visible = true;*/
                string patron = "Hash";
                string contraseña = txtContrasenia.Text.Trim();

                string conectar = DB.Conectando();//ConfigurationManager.ConnectionStrings["stringConexion"].ConnectionString;
                SqlConnection sqlConectar = new SqlConnection(conectar);
                SqlCommand cmd = new SqlCommand("CambiarContraseña", sqlConectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Usuario", email);
                cmd.Parameters.AddWithValue("@Contrasenia_nueva", contraseña);
                cmd.Parameters.AddWithValue("@Patron", patron);

                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    string script = "<script>alert('Contraseña actualizada'); window.location.href = 'Login.aspx';</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }
                catch
                {
                    string script = "<script>alert('Error al cambiar la contraseña');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }

                cmd.Connection.Close();
            }
        }

        private void enviarCorreo(string codigoRecuperacion, string email)
        {
            try
            {
                // Configurar cliente SMTP
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("notificaciones@mccollect.com.mx", "fqgdogwxxalpsfpu");
                smtpClient.EnableSsl = true;

                // Crear mensaje de correo
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("notificaciones@mccollect.com.mx");
                mail.To.Add(email);
                mail.Subject = "Código de recuperación de contraseña";
                mail.Body = "Su código de recuperación de contraseña es: " + codigoRecuperacion;

                // Enviar correo
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de envío de correo electrónico aquí
                mensajeCodigo.InnerText = "Se ha producido un error al enviar el correo electrónico de recuperación: " + ex.Message;
            }

            /*try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Soporte", "mccollectsoporte@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Código de recuperación de contraseña";
                message.Body = new TextPart("plain")
                {
                    Text = "Su código de recuperación de contraseña es: " + codigoRecuperacion
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("mccollectsoporte@gmail.com", "McCollect220424");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de envío de correo electrónico aquí
                mensajeCodigo.InnerText = "Se ha producido un error al enviar el correo electrónico de recuperación: " + ex.Message;
            }*/

        }

        protected void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            string contraseña = txtContrasenia.Text.Trim();
            if (!IsValidPassword(contraseña))
            {
                errorContraseña.InnerText = "La contraseña no es segura, debe contener al menos: "
                                          + "1 letra mayuscula "
                                          + "1 letra minuscula "
                                          + "1 caracter especial "
                                          + "entre 8-12 caracteres (sin espacios)";
                errorContraseña.Visible = true;
                btnEnviarCodigo.Enabled = false;
            }
            else
            {
                errorContraseña.Visible = false;
                btnEnviarCodigo.Enabled = true;
            }
        }
        protected void txtConfirContra_TextChanged(object sender, EventArgs e)
        {
            string contra1 = txtContrasenia.Text.Trim();
            string contra2 = txtConfirContra.Text.Trim();
            if(contra1 != contra2 || !IsValidPassword(contra1))
            {
                errorConfirContra.InnerText = "Las contraseñas no coinciden";
                errorConfirContra.Visible = true;
                btnEnviarCodigo.Enabled = false; 
            }
            else
            {
                errorConfirContra.Visible = false;
                btnEnviarCodigo.Enabled = true;
            }
        }

        private bool IsValidPassword(string contraseña)
        {
            // Validación de contraseña *al menos 1 minuscula y una mayuscula, al menos 1 digito, al menos 1 caracter especial y long 8-12
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,12}$";
            return Regex.IsMatch(contraseña, pattern);
        }
    }
}