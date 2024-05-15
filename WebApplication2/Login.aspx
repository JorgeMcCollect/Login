<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication2.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login</title>
    <link href ="Reursos/CSS/Login.css" rel="stylesheet" />
    <script src="Reursos/CSS/timer.js"></script>
</head>
<body>
    <div id="container">
        <img src="Reursos/CSS/logo.jpg" alt="Imagen" class="logo-image"/>
        <div class="form-container">
            <form id="form" runat="server">
                <h2>Iniciar sesión</h2>
                <div class="form-group">
                    <p>Usuario*</p>
                    <asp:TextBox ID="txtUsuario" runat="server" placeholder="Correo electrónico" MaxLength="50" required=""></asp:TextBox>
                </div>
                <div class="form-group">
                    <p>Contraseña*</p>
                    <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" placeholder="Contraseña" MaxLength="12" required=""></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar sesión" OnClick="btnIniciarSesion_Click" CssClass="btn" />
                </div>
                <div id="mensajeError" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
                <a href ="RecuperarContraseña.aspx">¿Olvidaste tu contraseña?</a>
            </form>
        </div>
    </div>
</body>
</html>
