using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.Admin.Utilizadores
{
    public partial class bloquearutilizador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
                Response.Redirect("~/index.aspx");
            try
            {
                int id = int.Parse(Request["id"].ToString());
                M17AB_TrabalhoModelo_202223.Models.Utilizador utilizador = new M17AB_TrabalhoModelo_202223.Models.Utilizador();
                utilizador.ativarDesativarUtilizador(id);
                Response.Redirect("~/Admin/Utilizadores/Utilizadores.aspx");

            }
            catch
            {

                Response.Redirect("~/Admin/Utilizadores/Utilizadores.aspx");
            }
        }
    }
}