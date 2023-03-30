using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.Admin.Roupa
{
    public partial class adicionar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            try
            {
                if (IsPostBack) return;
                //querystring nlivro
                int nroupa = int.Parse(Request["nroupa"].ToString());

                M17AB_TrabalhoModelo_202223.Models.Roupa lv = new M17AB_TrabalhoModelo_202223.Models.Roupa();
                DataTable dados = lv.devolveDadosRoupa(nroupa);
                if (dados == null || dados.Rows.Count == 0)
                {
                    //o nroupa não existe na tabela das roupas
                    throw new Exception("Roupa não existe.");
                }
                //mostrar os dados livro

                tbQuantidade.Text = dados.Rows[0]["quantidade"].ToString();
                Random rnd = new Random();

            }
            catch
            {
                Response.Redirect("~/Admin/Roupa/roupas.aspx");
            }
        }

        protected void btAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                
                int quantidade = int.Parse(tbQuantidade.Text);
                if (quantidade < 0)
                {
                    throw new Exception("A quantidade introduzida deve ser maior que 0");
                }
                M17AB_TrabalhoModelo_202223.Models.Roupa roupa = new M17AB_TrabalhoModelo_202223.Models.Roupa();


                roupa.quantidade = quantidade;
                roupa.nroupa = int.Parse(Request["nroupa"].ToString());
                roupa.adicionaRoupa();




                lbErro.Text = "A quantidade foi atualizada com sucesso";
                //ScriptManager.RegisterStartupScript(this, typeof(Page),
                //"Redirecionar", "returnMain('roupaslivros.aspx')", true);
            }
            catch (Exception ex)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + ex.Message;
                return;
            }
        }
    }
}