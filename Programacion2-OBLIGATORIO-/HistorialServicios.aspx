<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialServicios.aspx.cs" Inherits="Programacion2_OBLIGATORIO_.CrearServicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Diseño/General.css" />
    <p>
        <br />
    </p>
    <div class="orden">
        <h3>Lista De Servicios</h3>
    </div>
    <p>&nbsp;</p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Fecha"></asp:Label>
    </p>
    <p>
        <asp:TextBox class="form-control" ID="txtFecha" runat="server" TextMode="Date"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="BUSCAR" BorderColor="#0000CC" BorderStyle="Solid" CssClass="btn-primary" Font-Bold="False" ForeColor="White" />
</p>
<p>
    <asp:Label ID="labelError" runat="server" ForeColor="Red"></asp:Label>
</p>
    <div class="orden">
        <asp:GridView class="table table-dark table-striped" ID="gridHistorial" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="IdServicio" HeaderText="Id Servicio" />
                <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="Localizacion" HeaderText="Localizacion" />
                <asp:BoundField DataField="precios" HeaderText="Precio" />
                <asp:BoundField DataField="Duracion" HeaderText="Duracion (Horas)" />
            </Columns>
        </asp:GridView>
    </div>
    <p>&nbsp;</p>
</asp:Content>
