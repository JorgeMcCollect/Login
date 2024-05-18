<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebApplication2.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Index</title>
    <link href ="Reursos/CSS/Index.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <asp:Label ID="lblInfo" runat="server"></asp:Label>
            <table class="table table-user-information">
                <tbody>
                    <tr>
                        <td>Correo electrónico:</td>
                        <td>
                            <asp:Label ID="lblCorreo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Edad:</td>
                        <td>
                            <asp:Label ID="lblEdad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Dirección:</td>
                        <td>
                            <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Código postal:</td>
                        <td>
                            <asp:Label ID="lblCP" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Promedio:</td>
                        <td>
                            <asp:Label ID="lblPromedio" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Fecha de nacimiento:</td>
                        <td>
                            <asp:Label ID="lblFN" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="form-group">
                <asp:Button ID="btnCita" runat="server" Text="Consultar cita" OnClick="btnCita_Click" CssClass="btn" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar sesión" OnClick="btnCerrar_Click" style="background: none; border: none; color: blue; text-decoration: underline; cursor: pointer; font-size: 18px;" />
            </div>
            <!--<a href ="Login.aspx">Cerrar sesión</a>-->
        </div>
    </form>
</body>
</html>
