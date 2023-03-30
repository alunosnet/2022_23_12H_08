using Projeto_MOD16.Cliente;
using Projeto_MOD16.Vendas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_MOD16
{
    public partial class Form1 : Form
    {
        BaseDados bd = new BaseDados("M15_Loja");
        public Form1()
        {
            InitializeComponent();
            TopVendas();
        }

        public void TopVendas()
        {
            string sql = @"SELECT Nome,count(*) as [Nr Compras] FROM Clientes 
                            INNER JOIN Vendas ON 
                            Clientes.ncliente=Vendas.ncliente
                        GROUP By Vendas.ncliente, Nome
                        ORDER BY count(*) DESC";
            
            dataGridView1.DataSource = bd.DevolveSQL(sql);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lojaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            f_roupa f_Roupa = new f_roupa(bd);
            f_Roupa.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
 
            Application.Exit();
        
        }

        private void lojaToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_venda f_Venda = new f_venda(bd);
            f_Venda.Show();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            f_cliente f_Cliente = new f_cliente(bd);
            f_Cliente.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
