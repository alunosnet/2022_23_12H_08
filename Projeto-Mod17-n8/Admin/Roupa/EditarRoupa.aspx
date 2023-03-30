<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditarRoupa.aspx.cs" Inherits="Projeto_Mod17_n8.Admin.Roupa.EditarRoupa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Editar roupa</h1>

    <div class="from-group">
        <label for="ContentPlaceHolder1_tbMarca">Marca</label>
        <asp:TextBox CssClass="form-control" ID="tbMarca" runat="server" MaxLength="100" Required placeholder="Marca da Roupa" /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbModelo">Modelo</label>
        <asp:TextBox CssClass="form-control" ID="tbModelo" runat="server" MaxLength="100" Required placeholder="Modelo da Roupa" /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbPreco">Preço:</label>
        <asp:TextBox ID="tbPreco" CssClass="form-control" runat="server" TextMode="Number" /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbQuantidade">Quantidade</label>
        <asp:TextBox ID="tbQuantidade" CssClass="form-control" runat="server" TextMode="Number" /><br />
    </div>
    </div>
    <asp:Image CssClass="figure-img" runat="server" ID="imgCapa" />
    <div class="form-group">
        <label for="ContentPlaceHolder1_fuCapa">Capa:</label>
        <asp:FileUpload ID="fuCapa" runat="server" CssClass="form-control" />
    </div>
    <br />
    <asp:Button runat="server" ID="btAtualizar" CssClass="btn btn-lg btn-success" Text="Atualizar" OnClick="btAtualizar_Click" />
    <asp:Button runat="server" ID="btVoltar" CssClass="btn btn-lg btn-info" Text="Voltar" PostBackUrl="~/Admin/Roupa/Roupas.aspx" />
    <br />
    <asp:Label ID="lbErro" runat="server"></asp:Label>
</asp:Content>
