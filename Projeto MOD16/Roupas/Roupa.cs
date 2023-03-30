using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_MOD16.Roupas
{
    public class Roupa
    {
        public int Nroupa { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public bool Estado { get; set; }


        public void Guardar(BaseDados bd)
        {
            string sql = @"INSERT INTO Roupas(nome,genero,estado) VALUES 
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
                    ParameterName="@nroupa",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.Nroupa
                }

            };
            bd.ExecutaSQL(sql, parametros);

        }
        public void Atualizar(BaseDados bd)
        {
            string sql = @"UPDATE Roupas SET nome=@nome,genero=@genero  
                                WHERE nroupa=@nroupa";
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
                    ParameterName="@nroupa",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=this.Nroupa
                }
            };
            bd.ExecutaSQL(sql, parametros);
        }
        public DataTable Procurar(int nroupa, BaseDados bd)
        {
            string sql = "SELECT * FROM Roupas WHERE nroupa=" + nroupa;
            DataTable temp = bd.DevolveSQL(sql);

            if (temp != null && temp.Rows.Count > 0)
            {

                this.Nroupa = int.Parse(temp.Rows[0]["nroupa"].ToString());
                this.Nome = temp.Rows[0]["nome"].ToString();
                this.Genero = temp.Rows[0]["genero"].ToString();
                this.Estado = bool.Parse(temp.Rows[0]["estado"].ToString());


            }

            return temp;
        }

        public static int NrRegistos(BaseDados bd)
        {
            string sql = "SELECT count(*) as NrRegistos from Roupas";
            DataTable dados = bd.DevolveSQL(sql);
            int nr = int.Parse(dados.Rows[0][0].ToString());
            dados.Dispose();
            return nr;
        }

        public static DataTable ListarTodos(BaseDados bd)
        {
            string sql = "SELECT * FROM Roupas";
            return bd.DevolveSQL(sql);
        }

        public static DataTable ListarTodos(BaseDados bd, int primeiroregisto, int ultimoregisto)
        {
            string sql = $@"SELECT nroupa,nome,genero,Estado FROM
                        (SELECT row_number() over (order by nroupa) as Num,* FROM Roupas) as T
                        WHERE Num>={primeiroregisto} AND Num<={ultimoregisto}";
            return bd.DevolveSQL(sql);
        }

        public static DataTable ListarDisponiveis(BaseDados bd)
        {

            string sql = "SELECT * FROM Roupas WHERE estado=1";
            return bd.DevolveSQL(sql);
        }



        public static void ApagarRoupa(int nroupa, BaseDados bd)
        {
            string sql = "DELETE FROM Roupas WHERE nroupa=" + nroupa;
            bd.ExecutaSQL(sql);
        }

        public static DataTable PesquisaPorNome(BaseDados bd, string nome)
        {
            string sql = @"SELECT * FROM Roupas WHERE nome LIKE @nome";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@nome",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value="%"+nome+"%"
                },
            };
            return bd.DevolveSQL(sql, parametros);
        }

        public static DataTable PesquisaPorGenero(BaseDados bd, string genero)
        {
            string sql = @"SELECT * FROM Roupas WHERE genero LIKE @genero";
            List<SqlParameter> parametros = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName="@genero",
                    SqlDbType=System.Data.SqlDbType.Text,
                    Value="%"+genero+"%"
                },
            };
            return bd.DevolveSQL(sql, parametros);
        }

        public override string ToString()
        {
            return this.Nome;
        }

    }
}
