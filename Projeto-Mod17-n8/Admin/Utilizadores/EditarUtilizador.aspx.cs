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
    public partial class EditarUtilizador : System.Web.UI.Page
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
                M17AB_TrabalhoModelo_202223.Models.Utilizador utilizador = new M17AB_TrabalhoModelo_202223.Models.Utilizador();
                DataTable dados = utilizador.devolveDadosUtilizador(id);
                if (dados == null || dados.Rows.Count != 1)
                {
                    throw new Exception("Utilizador não existe");
                }
                tbNome.Text = dados.Rows[0]["nome"].ToString();
                tbNif.Text = dados.Rows[0]["nif"].ToString();

            }
            catch
            {
                lbErro.Text = "O utilizador indicado não existe.";
                ScriptManager.RegisterStartupScript(this, typeof(Page),
                    "Redirecionar", "returnMain('/Admin/Utilizadores/Utilizadores.aspx')", true);
            }
        }

        protected void btEditar_Click(object sender, EventArgs e)
        {
            try
            {
                string nome = tbNome.Text.Trim();
                if (string.IsNullOrEmpty(nome) || nome.Length < 3)
                {
                    throw new Exception("O nome não é válido.");
                }
                string nif = tbNif.Text.Trim();
                if (string.IsNullOrEmpty(nif) || nif.Length != 9)
                {
                    throw new Exception("O nif não é válido.");
                }
                int id = int.Parse(Request["id"].ToString());
                M17AB_TrabalhoModelo_202223.Models.Utilizador utilizador = new M17AB_TrabalhoModelo_202223.Models.Utilizador();
                utilizador.id = id;
                utilizador.nome = nome;
                utilizador.nif = nif;
                utilizador.atualizarUtilizador();
            }
            catch (Exception error)
            {
                lbErro.Text = "Ocorreu um erro: " + error.Message;
                return;
            }
            lbErro.Text = "Utilizador atualizado com sucesso.";
            //ScriptManager.RegisterStartupScript(this, typeof(Page),
            //        "Redirecionar", "returnMain('/Admin/Utilizadores/Utilizadores.aspx')", true);
        }
    }
    
}