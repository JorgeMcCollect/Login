<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="WebApplication2.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Registro</title>
    <link href ="Reursos/CSS/Registro.css" rel="stylesheet" />
</head>
<body>
    <div id="container">
        <h2>Registro de usuarios</h2>
        <form id="form1" runat="server">
            <div class="form-group">
                <p>Nombre de usuario*</p>
                <asp:TextBox ID="txtNombreUsuario" runat="server" placeholder="Nombre de usuario" MaxLength="50"  required="" OnTextChanged="txtNombreUsuario_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <div id="errorNombre" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
            <div class="form-group">
                <p>Correo electrónico*</p>
                <asp:TextBox ID="txtEmail" runat="server" placeholder="Correo electrónico" MaxLength="50" required="" OnTextChanged="txtEmail_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <div id="errorEmail" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
            <div class="form-group">
                <p>Contraseña*</p>
                <asp:TextBox ID="txtContraseña" type="password" runat="server" placeholder="Contraseña" MinLength="8" MaxLength="12" required="" OnTextChanged="txtContraseña_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <div id="errorContraseña" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
            <div class="form-group">
                <p>Fecha de nacimiento (DD-MM-YYYY)*</p>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" placeholder="Fecha de nacimiento (DD-MM-YYYY)" MaxLength="10" required="" OnTextChanged="txtFechaNacimiento_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <div id="errorFecha" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
            <div class="form-group">
                <p>Edad (años)*</p>
                <asp:TextBox ID="txtEdad" runat="server" placeholder="Edad" MaxLength="2" required=""></asp:TextBox>
            </div>
            <div id="errorEdad" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
            <div class="form-group">
                <p>Dirección*</p>
                <asp:TextBox ID="txtDireccion" runat="server" placeholder="Dirección" required="" MaxLength="50"></asp:TextBox>
            </div>
            <div id="errorDir" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
            <div class="form-group">
                <p>Código postal*</p>
                <asp:TextBox ID="txtCodigoPostal" runat="server" placeholder="Código postal" MaxLength="5" required="" OnTextChanged="txtCodigoPostal_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <div id="errorCP" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
            <div class="form-group">
                <p>Promedio*</p>
                <asp:TextBox ID="txtPromedio" runat="server" placeholder="Promedio" MaxLength="10" required="" OnTextChanged="txtPromedio_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
            <div id="errorProm" runat="server" class="mensaje-error" style="color: #FF9C9C; font-weight: bold;"> </div>
             <div class="form-group">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar usuario" OnClick="btnRegistrar_Click" CssClass="btn" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar sesión" OnClick="btnCerrar_Click" style="background: none; border: none; color: blue; text-decoration: underline; cursor: pointer; font-size: 18px;" UseSubmitBehavior="false" />
            </div>
            <%--<a href ="Login.aspx" id="lnkCerrarSesion" runat="server" onclick="lnkCerrarSesion_Click">Cerrar sesión</a>--%>
        </form>
    </div>
</body>
</html>
