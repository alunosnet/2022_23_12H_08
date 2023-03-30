using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.User.Vendas
{
    public partial class user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "1") == false)
            {
                Response.Redirect("~/index.aspx");
            }

            if (!IsPostBack)
            {
                divEditar.Visible = false;
                MostrarPerfil();
            }

        }

        void MostrarPerfil()
        {
            int id = int.Parse(Session["id"].ToString());
            Utilizador utilizador = new Utilizador();
            DataTable dados = utilizador.devolveDadosUtilizador(id);
            if (divPerfil.Visible == true)
            {
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbEmail.Text = dados.Rows[0]["email"].ToString();
                lbnif.Text = dados.Rows[0]["nif"].ToString();
            }
            else
            {
                tbNome.Text = dados.Rows[0]["nome"].ToString();
                lbEmail.Text = dados.Rows[0]["email"].ToString();
                tbNif.Text = dados.Rows[0]["nif"].ToString();
            }
        }

        protected void btEditar_Click(object sender, EventArgs e)
        {
            divPerfil.Visible = false;
            divEditar.Visible = true;
            tbEmail.Visible = false;
            lbEmail.Visible = false;
            tbNif.Visible = false;
            MostrarPerfil();
        }

        protected void btAtualizar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Session["id"].ToString());
            string nome = tbNome.Text;
            string email = tbEmail.Text;
            string nif = tbNif.Text;
            //TODO: validar os dados
            Utilizador utilizador = new Utilizador();
            utilizador.nome = nome;
            utilizador.email = email;
            utilizador.nif = nif;
            utilizador.id = id;
            utilizador.atualizarUtilizador();
            btCancelar_Click(sender, e);
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            divPerfil.Visible = true;
            divEditar.Visible = false;
            tbEmail.Visible = true;
            lbEmail.Visible = true;
            tbNif.Visible=true;
            MostrarPerfil();
        }
    }
}