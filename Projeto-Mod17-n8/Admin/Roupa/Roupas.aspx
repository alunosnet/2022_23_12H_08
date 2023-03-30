<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Roupas.aspx.cs" Inherits="Projeto_Mod17_n8.Admin.Roupa.roupas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestão de Vestuario</h2>
    <asp:GridView ID="gvLivros" runat="server" CssClass="table" />
    <h2>Adicionar Vestuario</h2>
    <div class="from-group">
        <label for="ContentPlaceHolder1_tbTipoRoupa">Tipo de Roupa:</label>
        <asp:TextBox CssClass="form-control" ID="tbTipoRoupa" runat="server" MaxLength="100" Required placeholder="Tipo de Roupa" /><br />
        </div>
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
        <asp:TextBox ID="tbPreco" CssClass="form-control" runat="server"  /><br />
    </div>
    <div class="from-group">
        <label for="ContentPlaceHolder1_dpGenero">Genero</label>
        <asp:DropDownList CssClass="form-select" ID="dpGenero" runat="server">
            <asp:ListItem Text="Masculino" Value="m" />
            <asp:ListItem Text="Femenino" Value="f" />
         </asp:DropDownList>
    </div><br />

    <div class="from-group">
        <label for="ContentPlaceHolder1_tbQuantidade">Quantidade</label>
        <asp:TextBox ID="tbQuantidade" CssClass="form-control" runat="server" TextMode="Number" /><br />
    </div>
    <div class="form-group">
        <label for="ContentPlaceHolder1_fuCapa">Capa:</label>
        <asp:FileUpload ID="fuCapa" runat="server" CssClass="form-control" />
    </div> 
    <br />
    <asp:Button CssClass="btn btn-lg btn-success" runat="server" ID="bt" Text="Adicionar" OnClick="bt_Click" />

    <asp:Label runat="server" ID="lbErro" />
</asp:Content>
