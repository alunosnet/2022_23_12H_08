using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.Admin.Utilizadores
{
    public partial class historicoutilizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
                Response.Redirect("~/index.aspx");
            try
            {
                atualizarGrid();
            }
            catch
            {
                lbErro.Text = "O utilizador indicado não existe";
                lbErro.CssClass = "alert alert-danger";
                Redirecionar();
            }
        }
        private void atualizarGrid()
        {
            gvHistorico.Columns.Clear();
            gvHistorico.DataSource = null;
            gvHistorico.DataBind();

            int id = int.Parse(Request["id"].ToString());
            M17AB_TrabalhoModelo_202223.Models.Venda venda = new M17AB_TrabalhoModelo_202223.Models.Venda();
            gvHistorico.DataSource = venda.listaTodasVendasComNomes(id);
            gvHistorico.DataBind();
        }

        private void Redirecionar()
        {
            //redirecionar
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Redirecionar",
                "returnMain('/Admin/Utilizadores/Utilizadores.aspx');", true);
        }
    }
}