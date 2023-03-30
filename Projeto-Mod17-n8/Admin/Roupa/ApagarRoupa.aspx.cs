using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.Admin.Roupa
{
    public partial class ApagarRoupa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            try
            {
                //querystring nlivro
                int nroupa = int.Parse(Request["nroupa"].ToString());

                M17AB_TrabalhoModelo_202223.Models.Roupa lv = new M17AB_TrabalhoModelo_202223.Models.Roupa();
                DataTable dados = lv.devolveDadosRoupa(nroupa);
                if (dados == null || dados.Rows.Count == 0)
                {
                    //o nroupa não existe na tabela dos livros
                    throw new Exception("A peca de roupa selecionada não existe.");
                }
                //mostrar os dados da roupa
                lbNroupa.Text = dados.Rows[0]["nroupa"].ToString();
                lbTipoRoupa.Text = dados.Rows[0]["tiporoupa"].ToString();
                lbMarca.Text = dados.Rows[0]["marca"].ToString();
                lbModelo.Text = dados.Rows[0]["modelo"].ToString();

            }
            catch
            {
                Response.Redirect("~/Admin/Roupa/roupas.aspx");
            }
        }


        protected void btVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Roupa/roupas.aspx");
        }

        protected void btRemover_Click(object sender, EventArgs e)
        {
            try
            {
                int nroupa = int.Parse(Request["nroupa"].ToString());
                M17AB_TrabalhoModelo_202223.Models.Roupa lv = new M17AB_TrabalhoModelo_202223.Models.Roupa();
                lv.removerRoupa(nroupa);
                //apagar a capa
                string ficheiro = Server.MapPath(@"~\Public\Imagens\") + nroupa + ".jpg";
                if (File.Exists(ficheiro))
                    File.Delete(ficheiro);
                Response.Redirect("~/Admin/Roupa/roupas.aspx");
                lbErro.Text = "A peca de roupa foi removida com sucesso";
                //ScriptManager.RegisterStartupScript(this, typeof(Page),
                //    "Redirecionar", "returnMain('livros.aspx')", true);
            }
            catch
            {
                Response.Redirect("~/Admin/Roupa/roupas.aspx");

            }
        }
    }
}