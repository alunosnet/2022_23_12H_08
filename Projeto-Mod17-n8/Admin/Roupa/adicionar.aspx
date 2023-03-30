<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="adicionar.aspx.cs" Inherits="Projeto_Mod17_n8.Admin.Roupa.adicionar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Gestao STOCK</h1>
    <h3>Adicionar quantidade</h3><br />
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbQuantidade">Quantidade</label>
        <asp:TextBox ID="tbQuantidade" CssClass="form-control" runat="server" TextMode="Number" /><br />
    </div>
    <asp:Button runat="server" ID="btAtualizar" CssClass="btn btn-lg btn-success" Text="Atualizar" OnClick="btAtualizar_Click" />
    <asp:Button runat="server" ID="btVoltar" CssClass="btn btn-lg btn-info" Text="Voltar" PostBackUrl="~/Admin/Roupa/Roupas.aspx" />
    <br />
    <asp:Label ID="lbErro" runat="server"></asp:Label>
</asp:Content>
