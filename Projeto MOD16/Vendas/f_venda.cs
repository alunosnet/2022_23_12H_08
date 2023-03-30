using Projeto_MOD16.Clientes;
using Projeto_MOD16.Roupas;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto_MOD16.Vendas
{
    public partial class f_venda : Form
    {
        

        int nvenda_escolhido;
        BaseDados bd;
        public f_venda(BaseDados bd)
        {
            InitializeComponent();
            this.bd = bd;
            AtualizaCBClientes();
            AtualizaCBRoupas();
            AtualizarGrelha();
        }



        private void f_venda_Load(object sender, EventArgs e)
        {
            
        }

        private void AtualizaCBRoupas()
        {
            cbRoupas.Items.Clear();
            DataTable dados = Roupa.ListarDisponiveis(bd);
            foreach (DataRow dr in dados.Rows)
            {
                Roupa roupa = new Roupa();
                roupa.Nroupa = int.Parse(dr["nroupa"].ToString());
                roupa.Nome = dr["nome"].ToString();
                cbRoupas.Items.Add(roupa);
            }
        }

        private void AtualizaCBClientes()
        {
            cbClientes.Items.Clear();
            DataTable dados = cliente.ListarTodos(bd);
            foreach (DataRow dr in dados.Rows)
            {
                cliente Cliente = new cliente();
                Cliente.Ncliente = int.Parse(dr["ncliente"].ToString());
                Cliente.Nome = dr["nome"].ToString();
                cbClientes.Items.Add(Cliente);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Escolha um cliente");
                return;
            }
            if (cbRoupas.SelectedIndex == -1)
            {
                MessageBox.Show("Escolha uma roupa");
                return;
            }


            cliente Cliente = cbClientes.SelectedItem as cliente;
            Roupa roupa = cbRoupas.SelectedItem as Roupa;
            Venda venda = new Venda(Cliente.Ncliente, roupa.Nroupa, dtDataVenda.Value);
            
            venda.Adicionar(bd);
            LimparForm();
            AtualizaCBRoupas();
            AtualizarGrelha();
        }

        private void AtualizarGrelha()
        {
            dgVendas.DataSource = Venda.ListarTodos(bd);
            dgVendas.AllowUserToAddRows = false;
            dgVendas.AllowUserToDeleteRows = false;
            dgVendas.ReadOnly = true;
        }

        private void LimparForm()
        {

            cbClientes.SelectedIndex = -1;
            cbRoupas.SelectedIndex = -1;
        }

        private void tbPreco_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {   

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void dgVendas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            label1.Visible = false;
            label2.Visible = false;
            cbClientes.Visible = false;
            cbRoupas.Visible = false;

            int linha = dgVendas.CurrentCell.RowIndex;
            if (linha == -1)
            {
                return;
            }

            int nVenda = int.Parse(dgVendas.Rows[linha].Cells[0].Value.ToString());
            Venda venda = new Venda();
            
            venda.Procurar(nVenda, bd);

            cbClientes.Text = venda.ncliente.ToString();
            cbRoupas.Text = venda.nroupa.ToString();
            dtDataVenda.Value = venda.data_venda;
            nvenda_escolhido = nVenda;





        }


    

        void ApagarVendas()
        {
            if (nvenda_escolhido < 1)
            {
                MessageBox.Show("Tem de selecionar uma compra primeiro. Clique com o botão esquerdo.");
                return;
            }

            if (MessageBox.Show(
                "Tem a certeza que pretende cancelar essa venda?",
                "Cancela",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                Venda.ApagarVenda(nvenda_escolhido, bd);
                AtualizarGrelha();

            }

            LimparForm();

            AtualizarGrelha();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            ApagarVendas();
            AtualizarGrelha();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DateTime data_Venda = dtDataVenda.Value;
            if (data_Venda > DateTime.Now)
            {
                MessageBox.Show("A data de aquisição tem de ser inferior ou igual à data atual.");
                dtDataVenda.Focus();
                return;
            }

            
           

            Venda venda = new Venda();
            venda.data_venda = data_Venda;

            venda.Atualizar(bd, nvenda_escolhido);

            AtualizarGrelha();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            cbClientes.Visible = true;
            cbRoupas.Visible = true;

            LimparForm();
        }
    }
}
