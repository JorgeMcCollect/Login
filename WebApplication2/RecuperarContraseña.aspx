<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarContraseña.aspx.cs" Inherits="WebApplication2.RecuperarContraseña" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>RecuperarContraseña</title>
    <link href ="Reursos/CSS/RecuperarContraseña.css" rel="stylesheet" />
</head>
<body>
    <div id="container">
        <div class="form-container">
            <form id="form" runat="server">
                <h2 style="color:#fff; font-size: 36px; font-weight: bold;">Recuperar contraseña</h2>
                <div class="form-group">
                    <asp:Label ID="lblCorreo" runat="server" style="color: #fff; font-weight: bold;">Introduce tu correo electrónico*</asp:Label> 
                    <asp:TextBox ID="txtUsuario" runat="server" placeholder="Correo electrónico" MaxLength="50" required=""></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label  ID="lblIntoCode" runat="server" style="color: #fff; font-weight: bold;"></asp:Label>
                    <div>
                        <asp:TextBox ID="txtCodigoUser" runat="server" placeholder="Código de verificación" MaxLength="8" required="" Visible="false"></asp:TextBox>
                    </div> 
                </div>
                <div class="form-group">
                    <asp:Label  ID="lblContrasenia" runat="server" style="color: #fff; font-weight: bold;"></asp:Label>
                    <asp:TextBox ID="txtContrasenia" type="password" runat="server" placeholder="Nueva contraseña" MinLength="8" MaxLength="12" required="" Visible="false" OnTextChanged="txtContraseña_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                <div id="errorContraseña" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
                <div class="form-group">
                    <asp:Label  ID="lblConfirmContra" runat="server" style="color: #fff; font-weight: bold;"></asp:Label>
                    <asp:TextBox ID="txtConfirContra" type="password" runat="server" placeholder="Vuelve a escribir tu contraseña" MinLength="8" MaxLength="12" required="" Visible="false" OnTextChanged="txtConfirContra_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                <div id="errorConfirContra" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
                <div class="form-group">
                    <asp:Button ID="btnEnviarCodigo" runat="server" Text="Enviar código" OnClick="btnEnviarCodigo_Click" CssClass="btn" />
                </div>
                <div id="mensajeCodigo" runat="server" class="mensaje-codigo" style="color: #FF9C9C; font-weight: bold;"> </div>
            </form>
        </div>
    </div> 
</body>
</html>
