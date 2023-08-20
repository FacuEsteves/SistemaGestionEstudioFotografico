﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Fotografos.aspx.cs" Inherits="Programacion2_OBLIGATORIO_.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" type="text/css" href="Diseño/General.css" />

    <p>
        <br />
    </p>
    <div class="orden">
    <h3>FOTOGRAFOS</h3>
    </div>              
    <div class="division">
    <p>
        <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
        <asp:TextBox class="form-control" ID="txtNombre" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Apellido"></asp:Label>
        <asp:TextBox class="form-control" ID="txtApellido" runat="server"></asp:TextBox>
        <asp:Button ID="btnGuardarFoto" runat="server" Text="Guardar" OnClick="btnGuardarFoto_Click" CssClass="btn-success" />
        <asp:Label ID="labelError" runat="server" ForeColor="Red"></asp:Label>
    </p>
    </div>
    <div class="division">
    <p>
        <asp:Label ID="Label3" runat="server" Text="Precio"></asp:Label>
        <asp:TextBox class="form-control" ID="txtPrecio" runat="server"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="Guardar Precio" OnClick="Button2_Click" CssClass="btn-warning" />
        <asp:Label ID="labelError2" runat="server" ForeColor="Red"></asp:Label>
    </p>
    </div>
    <div class="orden">
    <p>
        <asp:GridView class="table table-striped table-dark table-hover"  ID="gridFotos" runat="server" AutoGenerateDeleteButton="True" OnRowDeleting="gridFotos_RowDeleting">
        </asp:GridView>
    </p>
    </div>
    <p>
    </p>
</asp:Content>
