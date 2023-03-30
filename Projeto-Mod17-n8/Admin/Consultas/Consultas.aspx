<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Consultas.aspx.cs" Inherits="Projeto_Mod17_n8.Admin.Consultas.Consultas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Consultas</h2>
    <asp:DropDownList ID="ddConsultas" CssClass="form-control" AutoPostBack="true" 
        OnSelectedIndexChanged="ddConsultas_SelectedIndexChanged" runat="server">
        <asp:ListItem Value="0">Top de marcas mais vendidas</asp:ListItem>
        <asp:ListItem Value="1">Top de consumidores</asp:ListItem>
        <asp:ListItem Value="2">Top marcas mais vendidas do último mês</asp:ListItem>
        <asp:ListItem Value="3">Nº de roupas por marca</asp:ListItem>
        <asp:ListItem Value="4">Nº de consumidores</asp:ListItem>
        <asp:ListItem Value="5">Nº de utilizadores bloqueados</asp:ListItem>
        <asp:ListItem Value="6">Nº de vendas por mês</asp:ListItem>
        <asp:ListItem Value="7">Lista dos utilizadores que compraram a roupa mais cara</asp:ListItem>

    </asp:DropDownList>
    <asp:GridView CssClass="table" ID="gvConsultas" runat="server"></asp:GridView>
</asp:Content>
