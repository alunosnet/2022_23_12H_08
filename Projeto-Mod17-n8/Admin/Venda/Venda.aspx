<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Venda.aspx.cs" Inherits="Projeto_Mod17_n8.Admin.Venda.Venda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Vendas</h2>
    <asp:GridView CssClass="table" runat="server" ID="gv_vendas"></asp:GridView>
    <h2>Registar nova venda</h2>
    Roupa: <asp:DropDownList CssClass="form-control" runat="server" ID="dd_roupa"></asp:DropDownList>
    <br />
    Consumidor:<asp:DropDownList CssClass="form-control" runat="server" ID="dd_consumidor"></asp:DropDownList>
    <br />
    Data venda:<asp:TextBox CssClass="form-control" runat="server" ID="tb_data" TextMode="Date"></asp:TextBox>
    <br />
    <asp:Button CssClass="btn btn-lg btn-danger" runat="server" ID="bt_registar" Text="Vender" OnClick="bt_registar_Click" />
    <br />
    <asp:Label runat="server" ID="lb_erro"></asp:Label>
</asp:Content>
