<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ApagarRoupa.aspx.cs" Inherits="Projeto_Mod17_n8.Admin.Roupa.ApagarRoupa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Confirmar apagar livro</h1>
    Nº Roupa:<asp:Label runat="server" ID="lbNroupa" CssClass="form-control"></asp:Label>
    <br />Tipo de Roupa:<asp:Label runat="server" ID="lbTipoRoupa" CssClass="form-control"></asp:Label>
    <br />Modelo:<asp:Label runat="server" ID="lbModelo" CssClass="form-control"></asp:Label>
    <br />Marca:<asp:Label runat="server" ID="lbMarca" CssClass="form-control"></asp:Label>
    <%--<br />Modelo<asp:Image CssClass="figure-img" runat="server" ID="imgCapa" />--%>
    <br />
    <asp:Button CssClass="btn btn-lg btn-danger" runat="server" ID="btRemover" Text="Remover" OnClick="btRemover_Click" />
    <asp:Button CssClass="btn btn-lg btn-info" runat="server" ID="btVoltar" Text="Voltar" OnClick="btVoltar_Click" />
    <br /><asp:Label runat="server" ID="lbErro"></asp:Label>
</asp:Content>
