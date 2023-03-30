using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.Admin.Utilizadores
{
    public partial class ApagarUtilizadore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            if (IsPostBack) return;

            try
            {
                int id = int.Parse(Request["id"].ToString());
                M17AB_TrabalhoModelo_202223.Models.Utilizador uti = new M17AB_TrabalhoModelo_202223.Models.Utilizador();

                DataTable dados = uti.devolveDadosUtilizador(id);

                if (dados == null || dados.Rows.Count != 1)
                {
                    throw new Exception("O utilizador não existe");
                }

                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbId.Text = dados.Rows[0]["id"].ToString();
            }

            catch
            {
                Response.Redirect("~/Admin/Utilizadores/utilizadores.aspx");
            }
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request["id"].ToString());
            M17AB_TrabalhoModelo_202223.Models.Utilizador uti = new M17AB_TrabalhoModelo_202223.Models.Utilizador();

            uti.removerUtilizador(id);
            Response.Redirect("~/Admin/Utilizadores/utilizadores.aspx");
        }
    }
}