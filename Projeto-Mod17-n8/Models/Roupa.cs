using M17AB_TrabalhoModelo_202223.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace M17AB_TrabalhoModelo_202223.Models
{
    public class Roupa
    {
        public int nroupa;
        public string tiporoupa;
        public string marca;
        public string modelo;
        public string genero;
        public decimal preco;
        public int quantidade;

        BaseDados bd;

        public Roupa()
        {
            bd = new BaseDados();
        }

        public int Adicionar()
        {
            string sql = @"INSERT INTO Roupas(tiporoupa,marca,modelo,genero,preco,quantidade)
                    VALUES (@tiporoupa,@marca,@modelo,@genero,@preco,@quantidade);
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@tiporoupa",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.tiporoupa
                },
                new SqlParameter()
                {
                    ParameterName="@marca",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.marca
                },
                new SqlParameter()
                {
                    ParameterName="@modelo",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.modelo
                },
                new SqlParameter()
                {
                    ParameterName="@genero",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.genero
                },
                new SqlParameter()
                {
                    ParameterName="@preco",
                    SqlDbType=System.Data.SqlDbType.Decimal,
                    Value=this.preco
                },

                new SqlParameter()
                {
                    ParameterName="@quantidade",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.quantidade
                },
            };
            return bd.executaEDevolveSQL(sql, parametros);
        }

        internal DataTable ListaTodasRoupas()
        {
            string sql = @"SELECT nroupa,tiporoupa,marca,modelo,genero,
                    preco, quantidade
                    FROM Roupas";
            return bd.devolveSQL(sql);
        }

        public DataTable devolveDadosRoupa(int nroupa)
        {
            string sql = "SELECT * FROM Roupas WHERE nroupa=@nroupa";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nroupa",SqlDbType=SqlDbType.Int,Value=nroupa }
            };
            return bd.devolveSQL(sql, parametros);
        }
        public void removerRoupa(int nroupa)
        {
            string sql = "DELETE FROM Roupas WHERE nroupa=@nroupa";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName="@nroupa",SqlDbType=SqlDbType.Int,Value=nroupa }
            };
            bd.executaSQL(sql, parametros);
        }
        public void atualizaRoupa()
        {
            string sql = "UPDATE Roupas SET marca=@marca,modelo=@modelo,preco=@preco,quantidade=@quantidade";
            sql += " WHERE nroupa=@nroupa;";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                
                new SqlParameter() {ParameterName="@marca",SqlDbType=SqlDbType.VarChar,Value= marca},
                new SqlParameter() {ParameterName="@modelo",SqlDbType=SqlDbType.VarChar,Value= modelo},
                new SqlParameter() {ParameterName="@preco",SqlDbType=SqlDbType.Decimal,Value= preco},
                new SqlParameter() {ParameterName="@quantidade",SqlDbType=SqlDbType.Int,Value=quantidade},
                new SqlParameter() {ParameterName="@nroupa",SqlDbType=SqlDbType.Int,Value=nroupa}
            };
            bd.executaSQL(sql, parametros);
        }

        public void adicionaRoupa()
        {
            string sql = "UPDATE Roupas SET quantidade=@quantidade";
            sql += " WHERE nroupa=@nroupa;";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {

                
                new SqlParameter() {ParameterName="@quantidade",SqlDbType=SqlDbType.Int,Value=quantidade},
                new SqlParameter() {ParameterName="@nroupa",SqlDbType=SqlDbType.Int,Value=nroupa}


            };
            bd.executaSQL(sql, parametros);
        }

        public DataTable listaRoupasDisponiveis(int? ordena = null)
        {
            string sql = "SELECT nroupa,modelo, marca, tiporoupa ,preco, genero FROM Roupas WHERE quantidade>0";
            if (ordena != null && ordena == 1)
            {
                sql += " order by preco";
            }
            if (ordena != null && ordena == 2)
            {
                sql += " order by genero";
            }
            return bd.devolveSQL(sql);
        }

        internal DataTable listaLivrosDaMarca(string pesquisa)
        {
            string sql = "SELECT * FROM Roupas WHERE quantidade>0 and tiporoupa like @nome";

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {
                    ParameterName ="@nome",
                    SqlDbType =SqlDbType.VarChar,
                    Value = "%"+pesquisa+"%"},
            };
            return bd.devolveSQL(sql, parametros);
        }

        internal DataTable listaRoupasDisponiveis(string pesquisa, int? ordena = null)
        {
            string sql = "SELECT * FROM Roupas WHERE quantidade>0 and tiporoupa like @tipo";
            if (ordena != null && ordena == 1)
            {
                sql += " order by preco";
            }
            if (ordena != null && ordena == 2)
            {
                sql += " order by marca";
            }

            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter() {
                    ParameterName ="@tipo",
                    SqlDbType =SqlDbType.VarChar,
                    Value = "%"+pesquisa+"%"},
            };
            return bd.devolveSQL(sql, parametros);
        }


    }
}