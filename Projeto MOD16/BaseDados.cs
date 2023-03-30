using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Projeto_MOD16
{
    public class BaseDados
    {
        string ligaBD;
        SqlConnection sqlConnection;
        string NomeBD;
        string caminhoBD;
        /*construtor*/
        public BaseDados(string NomeBD)
        {
            ligaBD = ConfigurationManager.ConnectionStrings["servidor"].ToString();
            this.NomeBD = NomeBD;
            string caminhoBD = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            caminhoBD += "\\M15_TrabalhoModelo\\";
            this.caminhoBD = caminhoBD + NomeBD + ".mdf";
            if (System.IO.Directory.Exists(caminhoBD) == false)
            {
                System.IO.Directory.CreateDirectory(caminhoBD);
            }
            if (System.IO.File.Exists(this.caminhoBD) == false)
            {
                CriarBD();
            }
            //ligação BD
            sqlConnection = new SqlConnection(ligaBD);
            sqlConnection.Open();
            sqlConnection.ChangeDatabase(NomeBD);

        }
        /*destrutor*/
        ~BaseDados()
        {
            try
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            catch
            {
                //Pode ocorrer erros 
            }
        }
        private void CriarBD()
        {
            //ligar ao servidor BD
            sqlConnection = new SqlConnection(ligaBD);
            sqlConnection.Open();
            //criar a BD
            string sql = $"CREATE DATABASE {NomeBD} ON PRIMARY (NAME={NomeBD},FILENAME='{caminhoBD}')";
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.ChangeDatabase(NomeBD);
            //criar as tabelas
            sql = @"create table roupas(
	                    nroupa int identity primary key,
	                    nome varchar(100) not null,
	                    genero varchar (1) not null check(genero in ('m','f')),
                        estado bit


                    );
                    create table clientes(
	                    ncliente int identity primary key,
	                    nome varchar(40),
                        genero varchar (1) not null check(genero in ('m','f')),
                        estado bit

                    );
                    create table vendas(
	                    nvenda int identity primary key,
	                    nroupa int references roupas(nroupa),
	                    ncliente int references clientes(ncliente),
	                    data_venda date,
                        estado bit

                    );
                    create table stock(
                        nstock int identity primary key,
	                    nroupa int references roupas(nroupa),
                        quantidade int not null,
                        preco money not null check(preco >= 0),
                        tamanho varchar (40) not null,
                        cor varchar (40) not null

                    );";

            sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            //fechar a ligação ao servidor BD
            sqlCommand.Dispose();
            sqlConnection.Close();
            sqlConnection.Dispose();
        }

        public void ExecutaSQL(string sql, List<SqlParameter> parametros = null)
        {

            SqlCommand comando = new SqlCommand(sql, sqlConnection);
            if (parametros != null)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        /// <summary>
        /// Executa uma consulta e devolve os dados da bd
        /// </summary>
        /// <returns>Um datatable com o resultado da consulta</returns>
        public DataTable DevolveSQL(string sql, List<SqlParameter> parametros = null)
        {
            //TODO: adicionar transações
            SqlCommand comando = new SqlCommand(sql, sqlConnection);
            if (parametros != null)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }
            DataTable dados = new DataTable();

            SqlDataReader registos = comando.ExecuteReader();
            dados.Load(registos);

            registos.Close();
            comando.Dispose();
            registos = null;
            comando = null;

            return dados;
        }
    }
}
