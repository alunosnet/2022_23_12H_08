using M17AB_TrabalhoModelo_202223.Classes;
using M17AB_TrabalhoModelo_202223.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Projeto_Mod17_n8.Admin
{
    /// <summary>
    /// Descrição resumida de servicos1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class servicos1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string ListaRoupas()
        {
            M17AB_TrabalhoModelo_202223.Models.Roupa livro = new M17AB_TrabalhoModelo_202223.Models.Roupa();
            DataTable dados = livro.ListaTodasRoupas();
            List<M17AB_TrabalhoModelo_202223.Models.Roupa> Roupas = new List<M17AB_TrabalhoModelo_202223.Models.Roupa>();
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                M17AB_TrabalhoModelo_202223.Models.Roupa novo = new M17AB_TrabalhoModelo_202223.Models.Roupa();
                novo.nroupa = int.Parse(dados.Rows[i]["nroupa"].ToString());
                novo.modelo = dados.Rows[i]["modelo"].ToString();
                novo.preco = Decimal.Parse(dados.Rows[i]["preco"].ToString());
                Roupas.Add(novo);
            }
            return new JavaScriptSerializer().Serialize(Roupas);
        }
        public class Dados
        {
            public string perfil;
            public int contagem;
        }
        [WebMethod(EnableSession = true)]
        public List<Dados> DadosUtilizadores()
        {
            if (Session["perfil"] == null || Session["perfil"].ToString() != "0")
                return null;
            List<Dados> devolver = new List<Dados>();
            BaseDados bd = new BaseDados();
            DataTable dados = bd.devolveSQL(@"SELECT count(*), case 
                                                   when perfil=0 then 'Admin'
                                                   when perfil=1 then 'User'
                                                end as [perfil]
                                                FROM utilizadores GROUP BY perfil");
            for (int i = 0; i < dados.Rows.Count; i++)
            {
                Dados novo = new Dados();
                novo.perfil = dados.Rows[i][1].ToString();
                novo.contagem = int.Parse(dados.Rows[i][0].ToString());
                devolver.Add(novo);
            }
            return devolver;
        }
    }
}
