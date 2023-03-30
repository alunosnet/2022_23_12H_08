<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Vendas.aspx.cs" Inherits="Projeto_Mod17_n8.User.Vendas.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Loja</h1>
    <h3>Comprar Roupa</h3>
    <asp:GridView CssClass="table" EmptyDataText="Não existem pecas de roupa disponíveis para venda" runat="server" ID="gvlivros"></asp:GridView>
    <br /><asp:Label runat="server" ID="lbErro"></asp:Label>
</asp:Content>
