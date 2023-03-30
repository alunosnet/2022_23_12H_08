<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="historicoutilizador.aspx.cs" Inherits="Projeto_Mod17_n8.Admin.Utilizadores.historicoutilizador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Histórico</h2>
    <asp:GridView EmptyDataText="Nenhuma compra foi efetuada por este utilizador!!!" CssClass="table" ID="gvHistorico" runat="server"></asp:GridView>
    <br /><asp:Label ID="lbErro" runat="server" Text=""></asp:Label>
    <br /><asp:Button CssClass="btn btn-info" ID="Button1" runat="server" Text="Voltar" PostBackUrl="~/Admin/Utilizadores/Utilizadores.aspx" />
</asp:Content>
