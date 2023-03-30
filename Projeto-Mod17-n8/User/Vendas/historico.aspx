<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="historico.aspx.cs" Inherits="Projeto_Mod17_n8.User.Vendas.historico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Histórico</h2>
    <asp:GridView CssClass="table" EmptyDataText="Nenhuma compra foi efetuada nesta conta" ID="gvhistorico" runat="server"></asp:GridView>
</asp:Content>
