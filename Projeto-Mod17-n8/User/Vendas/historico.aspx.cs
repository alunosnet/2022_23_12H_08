using M17AB_TrabalhoModelo_202223.Classes;
using Projeto_Mod17_n8.Admin.Utilizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.User.Vendas
{
    public partial class historico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "1") == false)
            {
                Response.Redirect("~/index.aspx");
            }

            ConfigurarGrid();

            if (!IsPostBack)
                AtualizarGrid();
        }

        private void ConfigurarGrid()
        {
            gvhistorico.AllowPaging = true;
            gvhistorico.PageSize = 6;
            gvhistorico.RowCommand += Gvlivros_RowCommand;
            gvhistorico.PageIndexChanging += Gvhistorico_PageIndexChanging;
        }

        private void Gvlivros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;
            int linha = int.Parse(e.CommandArgument.ToString());
        }

        private void Gvhistorico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvhistorico.PageIndex = e.NewPageIndex;
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            gvhistorico.Columns.Clear();
            gvhistorico.DataSource = null;
            gvhistorico.DataBind();

            int idutilizador = int.Parse(Session["id"].ToString());
            M17AB_TrabalhoModelo_202223.Models.Venda emp = new M17AB_TrabalhoModelo_202223.Models.Venda();
            gvhistorico.DataSource = emp.listaTodosVendasComNomes(idutilizador);
            gvhistorico.DataBind();
        }
    }
}