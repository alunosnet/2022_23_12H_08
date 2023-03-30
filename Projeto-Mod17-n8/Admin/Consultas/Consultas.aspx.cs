using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Projeto_Mod17_n8.Admin.Consultas
{
    public partial class Consultas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserLogin.ValidarSessao(Session, Request, "0") == false)
                Response.Redirect("~/index.aspx");

            AtualizaGrelhaConsulta();
        }
        protected void ddConsultas_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaGrelhaConsulta();
        }
        private void AtualizaGrelhaConsulta()
        {
            gvConsultas.Columns.Clear();
            int iconsulta = int.Parse(ddConsultas.SelectedValue);
            DataTable dados;
            string sql = "";
            switch (iconsulta)
            {
                //Top de livros mais requisitados
                case 0:
                    sql = @"SELECT Marca,count(Vendas.nroupa) as [Nº de roupas] FROM Roupas 
                            INNER JOIN Vendas ON Roupas.nroupa=Vendas.nroupa 
                            GROUP BY Roupas.nroupa,Roupas.Marca
                            ORDER BY count(Vendas.nroupa) DESC";
                    break;
                //Top de leitores
                    case 1:
                    sql = @"SELECT Nome,count(idutilizador) as [Nº de vendas] FROM Utilizadores 
                                INNER JOIN Vendas ON Utilizadores.id=Vendas.idutilizador 
                                GROUP BY Utilizadores.id,Utilizadores.Nome
                                ORDER BY count(idutilizador) DESC";
                    break;
                //Top de marcas mais requisitadas do último mês
                case 2:
                    sql = @"SELECT TOP 3 marca AS [Marca], COUNT(vendas.nroupa) AS [Nº de Requisições] 
                                FROM Roupas, vendas
                                WHERE Roupas.nroupa = vendas.nroupa 
                                    AND DATEDIFF(DAY, vendas.data_venda, GETDATE()) < 30
                                GROUP BY Roupas.nroupa,Roupas.marca
                                ORDER BY COUNT(vendas.nroupa) DESC";
                    break;
                

                //Nº de livros por autor
                case 3:
                    sql = @"SELECT marca, count(nroupa) as [Nrº de roupas] 
                                FROM Roupas 
                                GROUP BY marca";
                    break;
                //Nº de consumidores
                case 4:
                    sql = @"SELECT  count(*) as [Nº de consumidores] 
                                FROM Utilizadores
                                WHERE perfil = 1";
                    break;
                //Nº de utilizadores bloqueados
                case 5:
                    sql = @"SELECT count(*) as [Nº de utilizadores bloqueados] 
                                FROM Utilizadores
                                WHERE estado = 0";
                    break;
                
                //Nº de empréstimos por mês
                case 6:
                    sql = @"SELECT MONTH(data_venda) as [Mês],Count(nvenda) as [Nº de venda] 
                                FROM vendas
                                GROUP BY MONTH(data_venda)";
                    break;
                //Lista dos utilizadores que requisitaram o livro mais caro
                case 7:
                    sql = @"SELECT DISTINCT Utilizadores.nome 
                                FROM utilizadores
                                INNER JOIN vendas on vendas.idutilizador = utilizadores.id
                                WHERE vendas.nroupa = (SELECT TOP 1 roupas.nroupa FROM roupas ORDER BY preco DESC)";
                    break;
                
            }
            BaseDados bd = new BaseDados();
            dados = bd.devolveSQL(sql);
            gvConsultas.DataSource = dados;
            gvConsultas.DataBind();
        }
    }
}