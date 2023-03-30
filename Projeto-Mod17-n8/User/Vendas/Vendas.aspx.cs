using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.User.Vendas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "1") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            ConfigurarGrid();

            if (!IsPostBack)
            {
                AtualizarGrid();
            }

        }

        private void AtualizarGrid()
        {

            gvlivros.Columns.Clear();
            gvlivros.DataSource = null;
            gvlivros.DataBind();

            M17AB_TrabalhoModelo_202223.Models.Roupa livro = new M17AB_TrabalhoModelo_202223.Models.Roupa();
            gvlivros.DataSource = livro.listaRoupasDisponiveis();

            //mostrar imagem da roupa
            ImageField ifcapa = new ImageField();
            ifcapa.HeaderText = "Capa";
            int aleatorio = new Random().Next(99999);
            ifcapa.DataImageUrlFormatString = "~/Public/Imagens/{0}.jpg?" + aleatorio;
            ifcapa.DataImageUrlField = "nroupa";
            ifcapa.ControlStyle.Height = 180;
            gvlivros.Columns.Add(ifcapa);

            //botão comprar

            ButtonField bt = new ButtonField();
            bt.HeaderText = "Comprar";
            bt.Text = "Comprar";
            bt.ButtonType = ButtonType.Button;
            bt.CommandName = "comprar";
            bt.ControlStyle.CssClass = "btn btn-danger";
            gvlivros.Columns.Add(bt);

            gvlivros.DataBind();
        }

        private void ConfigurarGrid()
        {

            gvlivros.AllowPaging = true;
            gvlivros.PageSize = 3;
            gvlivros.RowCommand += Gvlivros_RowCommand;
            gvlivros.PageIndexChanging += Gvlivros_PageIndexChanging;
        }

        private void Gvlivros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvlivros.PageIndex = e.NewPageIndex;
            AtualizarGrid();

        }



        private void Gvlivros_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;
            int linha = int.Parse(e.CommandArgument.ToString());
            int idroupa = int.Parse(gvlivros.Rows[linha].Cells[2].Text);
            int idutilizador = int.Parse(Session["id"].ToString());
            if (e.CommandName == "comprar")
            {
                M17AB_TrabalhoModelo_202223.Models.Venda emp = new M17AB_TrabalhoModelo_202223.Models.Venda();
                emp.adicionarVenda(idroupa, idutilizador, DateTime.Now.AddDays(7));
                AtualizarGrid();
                lbErro.Text = "A peca de roupa foi comprada com sucesso";
            }
            
        }

    }
}