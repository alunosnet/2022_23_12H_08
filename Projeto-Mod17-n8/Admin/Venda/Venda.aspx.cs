using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.Admin.Venda
{
    public partial class Venda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
            {
                Response.Redirect("~/index.aspx");
            }


            ConfigurarGrid();

            if (IsPostBack) return;

            AtualizarGrid();
            AtualizarDDRoupas();
            AtualizarDDConsumidores();
        }

        private void ConfigurarGrid()
        {
            //paginação
            gv_vendas.AllowPaging = true;
            gv_vendas.PageSize = 5;
            gv_vendas.PageIndexChanging += Gv_vendas_PageIndexChanging;
            //botões de comando
            gv_vendas.RowCommand += Gv_vendas_RowCommand;

        }


        private void Gv_vendas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //mudar de página
            if (e.CommandName == "Page") return;

            //linha
            int linha = int.Parse(e.CommandArgument.ToString());

            //id do empréstimo
            int idemprestimo = int.Parse(gv_vendas.Rows[linha].Cells[2].Text);
            M17AB_TrabalhoModelo_202223.Models.Venda emp = new M17AB_TrabalhoModelo_202223.Models.Venda();

            if (e.CommandName == "email")
            {
                string email = ConfigurationManager.AppSettings["MeuEmail"];
                string password = ConfigurationManager.AppSettings["MinhaPassword"];
                string assunto = "Empréstimo de livro fora de prazo";
                string texto = "Caro leitor deve devolver o livro que tem emprestado.";
                DataTable dados = emp.devolveDadosVenda(idemprestimo);
                int idleitor = int.Parse(dados.Rows[0]["idutilizador"].ToString());
                DataTable dadosLeitor = new M17AB_TrabalhoModelo_202223.Models.Utilizador().devolveDadosUtilizador(idleitor);
                string emailConsumidor = dadosLeitor.Rows[0]["email"].ToString();
                Helper.enviarMail(email, password, emailConsumidor, assunto, texto);
                AtualizarDDConsumidores();
                AtualizarDDRoupas();
                AtualizarGrid();
            }
        }
        private void Gv_vendas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_vendas.PageIndex = e.NewPageIndex;
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            M17AB_TrabalhoModelo_202223.Models.Venda emp = new M17AB_TrabalhoModelo_202223.Models.Venda();

            DataTable dados;

            dados = emp.listaTodasVendasComNomes();

            gv_vendas.Columns.Clear();
            gv_vendas.DataSource = null;
            gv_vendas.DataBind();
            if (dados == null || dados.Rows.Count == 0) return;
            //botões de comando
           


            gv_vendas.DataSource = dados;
            gv_vendas.AutoGenerateColumns = true;
            gv_vendas.DataBind();
        }

        private void AtualizarDDRoupas()
        {
            M17AB_TrabalhoModelo_202223.Models.Roupa lv = new M17AB_TrabalhoModelo_202223.Models.Roupa();
            dd_roupa.Items.Clear();
            DataTable dados = lv.listaRoupasDisponiveis();
            foreach (DataRow linha in dados.Rows)
                dd_roupa.Items.Add(
                    new ListItem(linha["tiporoupa"].ToString(), linha["nroupa"].ToString())
                );
        }

        private void AtualizarDDConsumidores()
        {
            M17AB_TrabalhoModelo_202223.Models.Utilizador u = new M17AB_TrabalhoModelo_202223.Models.Utilizador();
            dd_consumidor.Items.Clear();
            DataTable dados = u.listaTodosUtilizadoresDisponiveis();
            foreach (DataRow linha in dados.Rows)
                dd_consumidor.Items.Add(
                    new ListItem(linha["nome"].ToString(), linha["id"].ToString())
                );
        }

        protected void bt_registar_Click(object sender, EventArgs e)
        {
            try
            {
                M17AB_TrabalhoModelo_202223.Models.Venda emp = new M17AB_TrabalhoModelo_202223.Models.Venda();
                int nroupa = int.Parse(dd_roupa.SelectedValue);
                int nconsumidor = int.Parse(dd_consumidor.SelectedValue);
                DateTime data = DateTime.Parse(tb_data.Text);
                emp.adicionarVenda(nroupa, nconsumidor,data);

                lb_erro.Text = "A venda foi registada com sucesso.";
                lb_erro.CssClass = "";
                AtualizarGrid();
            }
            catch (Exception erro)
            {
                lb_erro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lb_erro.CssClass = "alert alert-danger";

            }
            AtualizarDDConsumidores();
            AtualizarDDRoupas();
            AtualizarGrid();

            
        }
    }
}