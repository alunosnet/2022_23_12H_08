using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_MOD16.Clientes
{
    public class cliente
    {
        public int Ncliente { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }

        public bool Estado { get; set; }
        public cliente()
        { }
            

        public cliente(int ncliente, string nome, string genero, bool estado)
        {
            Ncliente = ncliente;
            Nome = nome;
            Genero = genero;
            Estado = estado;



        }

        public void Guardar(BaseDados bd)
        {
            string sql = @"INSERT INTO Clientes(nome,genero,estado) VALUES 
                            (@nome,@genero,1)";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@nome",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.Nome
                },
                new SqlParameter()
                {
                    ParameterName="@genero",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.Genero
                },

                new SqlParameter()
                {
                    ParameterName="@ncliente",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.Ncliente
                }

            };
            bd.ExecutaSQL(sql, parametros);

        }

        public static DataTable ListarTodos(BaseDados bd)
        {
            string sql = "SELECT * From Clientes";
            return bd.DevolveSQL(sql);
        }


        public void ProcurarPorNrLeitor(BaseDados bd, int ncliente)
        {
            string sql = "SELECT * FROM Clientes WHERE NCliente=" + ncliente;
            DataTable dados = bd.DevolveSQL(sql);
            if (dados != null && dados.Rows.Count > 0)
            {
                this.Ncliente = int.Parse(dados.Rows[0]["ncliente"].ToString());
                this.Nome = dados.Rows[0]["nome"].ToString();
                this.Genero = dados.Rows[0]["genero"].ToString();


            }
        }

        internal static void Apagar(BaseDados bd, int ncliente_escolhido)
        {
            string sql = "DELETE FROM Clientes WHERE NCliente=" + ncliente_escolhido;
            bd.ExecutaSQL(sql);

        }


        public void Atualizar(BaseDados bd)
        {
            string sql = @"UPDATE Clientes SET nome=@nome,genero=@genero  
                                WHERE ncliente=@ncliente";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@nome",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.Nome
                },
                new SqlParameter()
                {
                    ParameterName="@genero",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=this.Genero
                },

                new SqlParameter()
                {
                    ParameterName="@ncliente",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.Ncliente
                }
            };
            bd.ExecutaSQL(sql, parametros);
        }

        public override string ToString()
        {
            return this.Nome;
        }



    }
}
