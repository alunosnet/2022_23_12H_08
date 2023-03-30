using Projeto_MOD16.Clientes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Projeto_MOD16.Cliente
{
    public partial class f_cliente : Form
    {

        BaseDados bd;
        int ncliente_escolhido;

        public f_cliente(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            AtualizarGrelha();
        }

        private void f_cliente_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cliente Cliente = new cliente(0, tbNome.Text, cbGenero.Text, true);
            //Guardar na bd
            Cliente.Guardar(bd);
            //limpar form
            LimparForm();
            //atualizar grid
            AtualizarGrelha();
        }
        private void AtualizarGrelha()
        {
            dgClientes.DataSource = cliente.ListarTodos(bd);
        }

        private void LimparForm()
        {
            cbGenero.SelectedIndex = -1;
            tbNome.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cliente.Apagar(bd, ncliente_escolhido);
            AtualizarGrelha();
            button5_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            cliente Cliente = new cliente();
            Cliente.Ncliente = ncliente_escolhido;
            Cliente.Nome = tbNome.Text;
            Cliente.Genero = cbGenero.Text;
           
            Cliente.Atualizar(bd);
            button5_Click(sender, e);
            AtualizarGrelha();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LimparForm();
        }

        private void dgClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha = dgClientes.CurrentCell.RowIndex;
            if (linha == -1)
            {
                return;
            }
            int ncliente = int.Parse(dgClientes.Rows[linha].Cells[0].Value.ToString());
            cliente Cliente = new cliente();
            Cliente.ProcurarPorNrLeitor(bd, ncliente);
            tbNome.Text = Cliente.Nome;
            cbGenero.Text = Cliente.Genero;

            //Guardar o nleitor
            ncliente_escolhido = ncliente;

        }

        private void tbNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
