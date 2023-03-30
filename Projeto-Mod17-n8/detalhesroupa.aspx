<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="detalhesroupa.aspx.cs" Inherits="Projeto_Mod17_n8.detalhesroupa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Roupas</h1>
    <asp:GridView ID="gvRoupas" runat="server" CssClass="table" />
</asp:Content>
