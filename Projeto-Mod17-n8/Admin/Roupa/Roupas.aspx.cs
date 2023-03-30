using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
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
    public partial class roupas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }
            ConfigurarGrid();

            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }

        private void ConfigurarGrid()
        {
            gvLivros.AllowPaging = true;
            gvLivros.PageSize = 3;
            gvLivros.PageIndexChanging += GvLivros_PageIndexChanging;
        }

        private void GvLivros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLivros.PageIndex = e.NewPageIndex;
            AtualizarGrid();
        }



        protected void bt_Click(object sender, EventArgs e)
        {
            try
            {
                string tiporoupa = tbTipoRoupa.Text;
                if (tiporoupa.Trim().Length < 3)
                {
                    throw new Exception("O nome do tipo de roupa é muito pequeno.");
                }
                string marca = tbMarca.Text;
                if (marca.Trim().Length < 3)
                {
                    throw new Exception("O nome da marca é muito pequena.");
                }
                string modelo = tbModelo.Text;
                if (modelo.Trim().Length < 0)
                {
                    throw new Exception("O modelo é muito pequeno.");
                }
                Decimal preco = Decimal.Parse(tbPreco.Text);
                if (preco < 0 )
                {
                    throw new Exception("O preço deve ser maior que 0");
                }
                int quantidade = int.Parse(tbQuantidade.Text);
                if(quantidade < 0)
                {
                    throw new Exception("A quantidade introduzida deve ser maior que 0");
                }

                string genero = dpGenero.SelectedValue;
                if (genero == "")
                {
                    throw new Exception("O genero não é válido");
                }
                M17AB_TrabalhoModelo_202223.Models.Roupa roupa = new M17AB_TrabalhoModelo_202223.Models.Roupa();
                roupa.tiporoupa = tiporoupa;
                roupa.marca = marca;
                roupa.modelo = modelo;
                roupa.genero = genero;
                roupa.preco = preco;
                roupa.quantidade = quantidade;

                int nroupa = roupa.Adicionar();

                if (fuCapa.HasFile)
                {
                    string ficheiro = Server.MapPath(@"~\Public\Imagens\");
                    ficheiro = ficheiro + nroupa + ".jpg";
                    fuCapa.SaveAs(ficheiro);
                }
            }
            catch (Exception ex)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + ex.Message;
                return;
            }
            //limpar form
            tbMarca.Text = "";
            tbModelo.Text = "";
            tbTipoRoupa.Text = "";
            tbPreco.Text = "";
            tbQuantidade.Text = "";
            dpGenero.SelectedIndex = 0;
            //atualizar grid
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            gvLivros.Columns.Clear();
            M17AB_TrabalhoModelo_202223.Models.Roupa lv = new M17AB_TrabalhoModelo_202223.Models.Roupa();
            DataTable dados = lv.ListaTodasRoupas();

            DataColumn dcAdicionar = new DataColumn();
            dcAdicionar.ColumnName = "Adicionar";
            dcAdicionar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcAdicionar);

            DataColumn dcEditar = new DataColumn();
            dcEditar.ColumnName = "Editar";
            dcEditar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcEditar);

            DataColumn dcApagar = new DataColumn();
            dcApagar.ColumnName = "Apagar";
            dcApagar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcApagar);

            //colunas da gridview
            gvLivros.DataSource = dados;
            gvLivros.AutoGenerateColumns = false;
            //Adicionar stock
            HyperLinkField hlAdicionar = new HyperLinkField();
            hlAdicionar.HeaderText = "Adicionar";
            hlAdicionar.DataTextField = "Adicionar";
            hlAdicionar.Text = "Adicionar...";
            hlAdicionar.DataNavigateUrlFormatString = "adicionar.aspx?nroupa={0}";
            hlAdicionar.DataNavigateUrlFields = new string[] { "nroupa" };
            gvLivros.Columns.Add(hlAdicionar);
            //Editar
            HyperLinkField hlEditar = new HyperLinkField();
            hlEditar.HeaderText = "Editar";
            hlEditar.DataTextField = "Editar";
            hlEditar.Text = "Editar...";
            hlEditar.DataNavigateUrlFormatString = "EditarRoupa.aspx?nroupa={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "nroupa" };
            gvLivros.Columns.Add(hlEditar);
            //Apagar
            HyperLinkField hlApagar = new HyperLinkField();
            hlApagar.HeaderText = "Apagar";
            hlApagar.DataTextField = "Apagar";
            hlApagar.Text = "Apagar...";
            hlApagar.DataNavigateUrlFormatString = "ApagarRoupa.aspx?nroupa={0}";
            hlApagar.DataNavigateUrlFields = new string[] { "nroupa" };
            gvLivros.Columns.Add(hlApagar);
            //nroupa
            BoundField bfnroupa = new BoundField();
            bfnroupa.HeaderText = "Nº Roupa";
            bfnroupa.DataField = "nroupa";
            gvLivros.Columns.Add(bfnroupa);
            //tiporoupa
            BoundField bftiporoupa = new BoundField();
            bftiporoupa.HeaderText = "TipoRoupa";
            bftiporoupa.DataField = "tiporoupa";
            gvLivros.Columns.Add(bftiporoupa);
            //modelo
            BoundField bfmodelo = new BoundField();
            bfmodelo.HeaderText = "Modelo";
            bfmodelo.DataField = "modelo";
            gvLivros.Columns.Add(bfmodelo);
            //marca
            BoundField bfmarca = new BoundField();
            bfmarca.HeaderText = "Marca";
            bfmarca.DataField = "marca";
            gvLivros.Columns.Add(bfmarca);
            //Preço
            BoundField bfpreco = new BoundField();
            bfpreco.HeaderText = "Preço";
            bfpreco.DataField = "preco";
            bfpreco.DataFormatString = "{0:C}";
            gvLivros.Columns.Add(bfpreco);
            //Quantidade
            BoundField bfquantidade = new BoundField();
            bfquantidade.HeaderText = "Quantidade";
            bfquantidade.DataField = "quantidade";
            gvLivros.Columns.Add(bfquantidade);
            //Generor
            BoundField bfgenero = new BoundField();
            bfgenero.HeaderText = "Genero";
            bfgenero.DataField = "genero";
            gvLivros.Columns.Add(bfgenero);
            //Capa
            ImageField ifcapa = new ImageField();
            ifcapa.HeaderText = "Capa";
            int aleatorio = new Random().Next(99999);
            ifcapa.DataImageUrlFormatString = "~/Public/Imagens/{0}.jpg?" + aleatorio;
            ifcapa.DataImageUrlField = "nroupa";
            ifcapa.ControlStyle.Height = 170;
            gvLivros.Columns.Add(ifcapa);

            gvLivros.DataBind();
        }
    }
}