using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8
{
    public partial class detalhesroupa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AtualizaGrid();
            }
        }

        private void AtualizaGrid()
        {

            gvRoupas.Columns.Clear();
            M17AB_TrabalhoModelo_202223.Models.Roupa lv = new M17AB_TrabalhoModelo_202223.Models.Roupa();
            DataTable dados = lv.ListaTodasRoupas();

            //Comprar
            DataColumn dcComprar = new DataColumn();
            dcComprar.ColumnName = "Comprar";
            dcComprar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcComprar);

            HyperLinkField hlComprar = new HyperLinkField();
            hlComprar.HeaderText = "Comprar";
            hlComprar.DataTextField = "Comprar";
            hlComprar.Text = "Comprar...";
            hlComprar.DataNavigateUrlFormatString = "Index.aspx?nroupa={0}";
            hlComprar.DataNavigateUrlFields = new string[] { "nroupa" };
            gvRoupas.Columns.Add(hlComprar);


            //tipo roupa
            BoundField bftiporoupa = new BoundField();
            bftiporoupa.HeaderText = "TipoRoupa";
            bftiporoupa.DataField = "tiporoupa";
            gvRoupas.Columns.Add(bftiporoupa);
            //modelo
            BoundField bfmodelo = new BoundField();
            bfmodelo.HeaderText = "Modelo";
            bfmodelo.DataField = "modelo";
            gvRoupas.Columns.Add(bfmodelo);
            //marca
            BoundField bfmarca = new BoundField();
            bfmarca.HeaderText = "Marca";
            bfmarca.DataField = "marca";
            gvRoupas.Columns.Add(bfmarca);
            //Preço
            BoundField bfpreco = new BoundField();
            bfpreco.HeaderText = "Preço";
            bfpreco.DataField = "preco";
            bfpreco.DataFormatString = "{0:C}";
            gvRoupas.Columns.Add(bfpreco);
            //Quantidade
            BoundField bfquantidade = new BoundField();
            bfquantidade.HeaderText = "Quantidade";
            bfquantidade.DataField = "quantidade";
            gvRoupas.Columns.Add(bfquantidade);
            //Generor
            BoundField bfgenero = new BoundField();
            bfgenero.HeaderText = "Genero";
            bfgenero.DataField = "genero";
            gvRoupas.Columns.Add(bfgenero);

            gvRoupas.DataSource = dados;
            gvRoupas.AutoGenerateColumns = false;

            gvRoupas.DataBind();
        }
    }
}