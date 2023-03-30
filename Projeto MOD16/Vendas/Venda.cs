using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_MOD16.Vendas
{
    public class Venda
    {

        public int nvenda;
        public int ncliente;
        public int nroupa;
        public DateTime data_venda;
        public bool estado;

        public Venda() { }

        public Venda(int ncliente, int nroupa, DateTime data_venda)
        {
            this.ncliente = ncliente;
            this.nroupa = nroupa;
            this.data_venda = data_venda;


        }

        public static DataTable ListarTodos(BaseDados bd)
        {
            string sql = "SELECT * From Vendas";
            return bd.DevolveSQL(sql);
        }



        public static void ApagarVenda(int idvenda_escolhido, BaseDados bd)
        {
            string sql = "DELETE FROM vendas WHERE nvenda=" + idvenda_escolhido;
            bd.ExecutaSQL(sql);
        }

        public DataTable Procurar(int idVenda, BaseDados bd)
        {
            string sql = "SELECT * FROM vendas WHERE nvenda=" + idVenda;
            DataTable temp = bd.DevolveSQL(sql);

            if (temp != null && temp.Rows.Count > 0)
            {
                this.nvenda = int.Parse(temp.Rows[0]["nvenda"].ToString());
                this.nroupa = int.Parse(temp.Rows[0]["nroupa"].ToString());
                this.ncliente = int.Parse(temp.Rows[0]["nroupa"].ToString());
                this.estado = bool.Parse(temp.Rows[0]["estado"].ToString());
                this.data_venda = DateTime.Parse(temp.Rows[0]["data_venda"].ToString());



            }

            return temp;
        }


        public void Adicionar(BaseDados bd)
        {
            //sql com insert
            string sql = $@"insert into vendas(nroupa,ncliente,
                            data_venda,estado) values 
                            (@nroupa,@ncliente,@data_venda,1
                            )";
            //parametros
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@nroupa",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.nroupa
                },
                new SqlParameter()
                {
                    ParameterName="@ncliente",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.ncliente
                },
                new SqlParameter()
                {
                    ParameterName="@data_venda",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.data_venda
                },


            };

            bd.ExecutaSQL(sql, parametros);
            sql = "UPDATE roupas SET estado=0 WHERE nroupa=" + nroupa;
            bd.ExecutaSQL(sql);
        }


        internal static void Vender(BaseDados bd, int nroupa)
        {
            //alterar o estado do emprestimo
            string sql = @"update vendas set estado=0 
                            where estado=1 and nroupa=" + nroupa;
            bd.ExecutaSQL(sql);
            //alterar o estado do livro
            sql = "update roupas set estado=1 where nroupa=" + nroupa;
            bd.ExecutaSQL(sql);

        }

        internal static void Apagar(BaseDados bd, int nvenda_escolhido)
        {
            string sql = "DELETE FROM Vendas WHERE ncliente=" + nvenda_escolhido;
            bd.ExecutaSQL(sql);
        }

        internal void Atualizar(BaseDados bd, int nvenda_esc)
        {
            string sql = @"UPDATE Vendas SET data_venda=@data_venda WHERE nvenda="+nvenda_esc;


            List<SqlParameter> parametros = new List<SqlParameter>()
            {

                new SqlParameter()
                {
                    ParameterName="@data_venda",
                    SqlDbType=System.Data.SqlDbType.Date,
                    Value=this.data_venda.Date
                },
            };
           
            bd.ExecutaSQL(sql, parametros);
        }



    }
}
