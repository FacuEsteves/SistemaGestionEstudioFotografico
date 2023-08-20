<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Programacion2_OBLIGATORIO_.Clientes" %>

<script runat="server">


    protected void btn_guardar_Click(object sender, EventArgs e)
    {

    }
</script>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Diseño/General.css" />
    <p>
        <br />
    </p>
    <div class="orden">
        <h3>GESTION DE CLIENTES:</h3>
    </div>
    <div class="espaciado">
    </div>
        <p>
            Nombre:
            <asp:TextBox class="form-control" ID="txt_nombre" runat="server"></asp:TextBox>
        </p>
        <p>
            Apellido: <asp:TextBox class="form-control" ID="txt_apellido" runat="server"></asp:TextBox>
        </p>
        <p>
            Telefono:
            <asp:TextBox class="form-control" ID="txt_telefono" runat="server"></asp:TextBox>
        </p>
        <p>
            Tipo cliente:
            <asp:DropDownList class="form-select" ID="drop_tipo_cliente" runat="server" OnSelectedIndexChanged="drop_tipo_cliente_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>-Seleccione-</asp:ListItem>
                <asp:ListItem Value="Particular">Particular</asp:ListItem>
                <asp:ListItem Value="Empresa">Empresa</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            &nbsp;<asp:Label ID="lblTipo" runat="server" Text="Tipo:" Visible="False"></asp:Label>
            <asp:DropDownList class="form-select" ID="drop_cliente_particular" runat="server" Visible="False">
                <asp:ListItem>-Seleccione-</asp:ListItem>
                <asp:ListItem>Comun</asp:ListItem>
                <asp:ListItem>VIP</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            &nbsp;<asp:Label ID="lblRut" runat="server" Text="RUT:" Visible="False"></asp:Label>
            <asp:TextBox class="form-control" ID="txt_rut" runat="server" Visible="False"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="lblDesc" runat="server" Text="Descuento para empresas en (%):" Visible="False"></asp:Label>
    &nbsp;<asp:TextBox class="form-control" ID="txt_descuento" runat="server" Visible="False"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="lbl_aviso" runat="server" ForeColor="#CC0000"></asp:Label>
        </p>
        <p>
            <asp:Button ID="Btn_Save" runat="server" OnClick="Btn_Save_Click" Text="Guardar" CssClass="btn-success" />
        </p>
        <p>
            &nbsp;</p>
</asp:Content>
