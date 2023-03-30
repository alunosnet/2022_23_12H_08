using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Projeto_Mod17_n8.Admin.Roupa
{
    public partial class EditarRoupa : System.Web.UI.Page
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
                tbMarca.Text = dados.Rows[0]["marca"].ToString();
                tbModelo.Text = dados.Rows[0]["modelo"].ToString();
                tbPreco.Text = dados.Rows[0]["preco"].ToString();
                tbQuantidade.Text = dados.Rows[0]["quantidade"].ToString();
                Random rnd = new Random();
                imgCapa.ImageUrl = @"~\Public\Imagens\" + nroupa + ".jpg?" + rnd.Next(9999);
                imgCapa.Width = 300;
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
                string marca = tbMarca.Text;
                if (marca.Trim().Length < 0)
                {
                    throw new Exception("O nome da marca de roupa é muito pequeno.");
                }

                string modelo = tbModelo.Text;
                if (modelo.Trim().Length < 0)
                {
                    throw new Exception("O modelo é muito pequeno.");
                }
                Decimal preco = Decimal.Parse(tbPreco.Text);
                if (preco < 0)
                {
                    throw new Exception("O preço deve ser maior que 0");
                }
                int quantidade = int.Parse(tbQuantidade.Text);
                if (quantidade < 0)
                {
                    throw new Exception("A quantidade introduzida deve ser maior que 0");
                }
                M17AB_TrabalhoModelo_202223.Models.Roupa roupa = new M17AB_TrabalhoModelo_202223.Models.Roupa();
                roupa.marca = marca;
                roupa.modelo = modelo;
                roupa.preco = preco;
                roupa.quantidade = quantidade;
                roupa.nroupa = int.Parse(Request["nroupa"].ToString());
                roupa.atualizaRoupa();

                

                if (fuCapa.HasFile)
                {
                    string ficheiro = Server.MapPath(@"~\Public\Imagens\");
                    ficheiro = ficheiro + roupa.nroupa + ".jpg";
                    fuCapa.SaveAs(ficheiro);
                }

                lbErro.Text = "A peça roupa foi atualizada com sucesso";
                ScriptManager.RegisterStartupScript(this, typeof(Page),
                    "Redirecionar", "returnMain('roupas.aspx')", true);
            }
            catch (Exception ex)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + ex.Message;
                return;
            }

        }
    }
}