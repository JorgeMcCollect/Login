<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cita.aspx.cs" Inherits="WebApplication2.Cita" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Cita</title>
    <link href ="Reursos/CSS/Cita.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
             <h2>A continuación se muestra la información de tu cita</h2>
            <div class="form-group">
                <asp:Label ID="lblCita" runat="server" style="color: #fff;  font-weight: bold; font-size: 18px"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Button ID="btnDescargar" runat="server" Text="Descargar comprobante" OnClick="btnDescargar_Click" CssClass="btn" />
            </div>
            <div class="form-group">
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar sesión" OnClick="btnCerrar_Click" CssClass="btn2" style="background: none; border: none; color: blue; text-decoration: underline; cursor: pointer; font-size: 18px;" />
            </div>
            <!--<a href ="Login.aspx">Cerrar sesión</a>-->
        </div>
    </form>
</body>
</html>
