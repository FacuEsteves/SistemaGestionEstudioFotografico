﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsignarServicios.aspx.cs" Inherits="Programacion2_OBLIGATORIO_.AsignarServicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Diseño/General.css" />
    <p style="margin-top: 0px">
        <br />
    </p>
    <div class="orden">
        <h3>SOLICITAR SERVICIOS FOTOGRAFICOS</h3>
    </div>
    <p>
        &nbsp;</p>
    <p>
        Codigo de servicio:
        <asp:TextBox class="form-control" ID="txt_codigo_servicio" runat="server"></asp:TextBox>
    </p>
    <p>
        Fecha:<asp:TextBox class="form-control" ID="txt_fecha" runat="server" TextMode="Date"></asp:TextBox>
    </p>
    <p>
        Duracion Sesion fotografica(horas):<asp:TextBox class="form-control" ID="Txt_Duracion" runat="server" TextMode="DateTime"></asp:TextBox>
    </p>
    <p>
        Localizacion:<asp:DropDownList class="form-select" ID="DD_Localidad" runat="server">
            <asp:ListItem>- Seleccionar -</asp:ListItem>
            <asp:ListItem>Estudio</asp:ListItem>
            <asp:ListItem>Externo</asp:ListItem>
        </asp:DropDownList>
    &nbsp;</p>
    <p>
        Cliente:
        <asp:DropDownList class="form-select" ID="drop_clientes" runat="server" AppendDataBoundItems="True">
            <asp:ListItem>- Seleccionar -</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        &nbsp;</p>
    <div class="espaciado">
    <h1>Fotografos a Solicitar:</h1><h1>Fotografos Asignados:</h1>
    </div>
    <br/>
    <div class="divisionUsuario">    
        <asp:GridView class="table table-striped table-hover" ID="grid_fotografos" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="grid_fotografos_SelectedIndexChanged" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellido" HeaderText="Apellido" />
            </Columns>
        </asp:GridView>
        <asp:GridView class="table table-striped table-hover" ID="grid_Fseleccionados" runat="server" AutoGenerateDeleteButton="True" OnRowDeleting="grid_Fseleccionados_RowDeleting">
        </asp:GridView>
    </div>
    <p>
        <asp:Label ID="lbl_mostrar" runat="server" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large" Font-Strikeout="False" Font-Underline="False" ForeColor="Red"></asp:Label>
    </p>
    <p>
        <asp:Button ID="btn_ingresar" runat="server" Height="35px" Text="INGRESAR" Width="95px" OnClick="btn_ingresar_Click" CssClass="btn-success" />
&nbsp;<asp:Button ID="btn_verificar" runat="server" Height="34px" Text="VERIFICAR" Width="94px" OnClick="btn_verificar_Click" CssClass="btn-primary"  />
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Ordenar:"></asp:Label>
    </p>
            <asp:DropDownList class="form-select" ID="ddOrden" runat="server" OnSelectedIndexChanged="ddOrden_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>-Seleccionar-</asp:ListItem>
                <asp:ListItem>Por Nombre A-Z</asp:ListItem>
                <asp:ListItem>Por Fecha Descendente</asp:ListItem>
            </asp:DropDownList>
    <br />
    <div class="orden">
        <asp:GridView class="table table-striped table-hover table-primary" ID="gridServicios" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="idservicio" HeaderText="Id Servicio" />
                <asp:BoundField DataField="nombreCli" HeaderText="Nombre Cliente" />
                <asp:BoundField DataField="fechaServ" HeaderText="Fecha realizado" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
