using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17AB_TrabalhoModelo_202223.Models
{
    public class Venda
    {
        BaseDados bd;

        public Venda()
        {
            this.bd = new BaseDados();
        }

        public void adicionarVenda(int nroupa, int nconsumidor, DateTime dataVenda)
        {
            string sql = "SELECT * FROM roupas WHERE nroupa=@nroupa";
            List<SqlParameter> parametrosBloquear = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nroupa",SqlDbType=SqlDbType.Int,Value=nroupa }
            };
            //iniciar transação
            SqlTransaction transacao = bd.iniciarTransacao(IsolationLevel.Serializable);
            DataTable dados = bd.devolveSQL(sql, parametrosBloquear, transacao);

            try
            {
                //verificar disponibilidade da roupa
                if (dados.Rows[0]["quantidade"].ToString() == "0")
                    throw new Exception("Roupa fora de stock");
                //alterar o stock da roupa
                sql = "UPDATE Roupas SET quantidade=quantidade-1 WHERE nroupa=@nroupa";
                List<SqlParameter> parametrosUpdate = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@nroupa",SqlDbType=SqlDbType.Int,Value=nroupa },
                    
                };
                bd.executaSQL(sql, parametrosUpdate, transacao);
                //registar empréstimo
                sql = @"INSERT INTO Vendas(nroupa,idutilizador,data_venda) 
                            VALUES (@nroupa,@idutilizador,@data_venda)";
                List<SqlParameter> parametrosInsert = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName="@nroupa",SqlDbType=SqlDbType.Int,Value=nroupa },
                    new SqlParameter() {ParameterName="@idutilizador",SqlDbType=SqlDbType.Int,Value=nconsumidor },
                    new SqlParameter() {ParameterName="@data_venda",SqlDbType=SqlDbType.Date,Value=DateTime.Now.Date},

                };
                bd.executaSQL(sql, parametrosInsert, transacao);
                //concluir transação
                transacao.Commit();
            }
            catch
            {
                transacao.Rollback();
            }
            dados.Dispose();
        }



        public DataTable listaTodosVendasComNomes(int nconsumidor)
        {
            string sql = @"SELECT roupas.tiporoupa as PecaRoupa,roupas.marca as Marca, roupas.modelo as Modelo, utilizadores.nome as nomeConsumidor, roupas.preco as Preco, data_venda
                        FROM Vendas inner join roupas on vendas.nroupa=roupas.nroupa
                        inner join utilizadores on vendas.idutilizador=utilizadores.id Where idutilizador=@idutilizador";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@idutilizador",SqlDbType=SqlDbType.Int,Value=nconsumidor }
            };
            return bd.devolveSQL(sql, parametros);
        }

        public DataTable listaTodasVendasComNomes(int ncomprador)
        {
            string sql = @"SELECT nvenda,roupas.modelo as modelo,utilizadores.nome as nomeComprador,data_venda,roupas.preco as Preco,
                        FROM Vendas inner join roupas on emprestimos.nroupa=roupas.nroupa
                        inner join utilizadores on vendas.idutilizador=utilizadores.id Where idutilizador=@idutilizador";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@idutilizador",SqlDbType=SqlDbType.Int,Value=ncomprador }
            };
            return bd.devolveSQL(sql, parametros);
        }

        public DataTable listaTodasVendasComNomes()
        {
            string sql = @"SELECT nvenda,roupas.marca as nomeRoupa,utilizadores.nome as nomeConsumidor,data_venda
                        FROM Vendas inner join roupas on vendas.nroupa=roupas.nroupa
                        inner join utilizadores on vendas.idutilizador=utilizadores.id";
            return bd.devolveSQL(sql);
        }


        
        public DataTable devolveDadosVenda(int nvenda)
        {
            string sql = "SELECT * FROM venda WHERE nvenda=@nvenda";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nvenda",SqlDbType=SqlDbType.Int,Value=nvenda }
            };
            return bd.devolveSQL(sql, parametros);
        }
    }
}