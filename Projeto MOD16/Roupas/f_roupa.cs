using Projeto_MOD16.Roupas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Projeto_MOD16
{
    public partial class f_roupa : Form
    {

        int NrRegistosPorPagina = 5;
        int nroupa_escolhido;
        BaseDados bd;
        int nrlinhas, nrpagina;

        public f_roupa(BaseDados bd)
        {
            this.bd = bd;
            InitializeComponent();
            AtualizaNrPaginas();
            AtualizaGrelha();

        }
        void AtualizaGrelha()
        {
            dgRoupas.AllowUserToAddRows = false;
            dgRoupas.AllowUserToDeleteRows = false;
            dgRoupas.ReadOnly = true;

            if (cbPagina.SelectedIndex == -1)
                dgRoupas.DataSource = Roupa.ListarTodos(bd);
            else
            {
                //Paginação
                int nrpagina = cbPagina.SelectedIndex + 1;
                int primeiroregisto = (nrpagina - 1) * NrRegistosPorPagina + 1;
                int ultimoregisto = primeiroregisto + NrRegistosPorPagina - 1;
                dgRoupas.DataSource = Roupa.ListarTodos(bd, primeiroregisto, ultimoregisto);
            }

        }


        private void f_roupa_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string nome = tbNome.Text;
            if (nome == "" || nome.Length < 3)
            {
                MessageBox.Show("Nome tem de ter pelo menos 3 letras.");
                tbNome.Focus();
                return;
            }
            


            if (cbGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Escolha um genero");
                return;
            }
            //Criar um objeto roupa
            Roupa roupa = new Roupa();
            //Preencher as propriedades
            roupa.Nome = nome;  
            roupa.Genero = cbGenero.Text;
            //Guardar na BD
            roupa.Guardar(bd);
            //Limpar o form
            LimparForm();
            AtualizaGrelha();
            AtualizaNrPaginas();    

        }
        private void LimparForm()
        {
            cbGenero.SelectedIndex = -1;
            tbNome.Text = "";



        }
        

        private void cbGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void cbPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizaGrelha();
        }
        void AtualizaNrPaginas()
        {
            cbPagina.Items.Clear();
            int nrroupas = Roupa.NrRegistos(bd);
            int nrPaginas = (int)Math.Ceiling(nrroupas / (float)NrRegistosPorPagina);
            for (int i = 1; i <= nrPaginas; i++)
                cbPagina.Items.Add(i);

            if (cbPagina.Items.Count == 0)
                cbPagina.Items.Add(1);
            cbPagina.SelectedIndex = 0;
        }


        void ApagarRegisto()
        {
            if (nroupa_escolhido < 1)
            {
                MessageBox.Show("Tem de selecionar um livro primeiro. Clique com o botão esquerdo.");
                return;
            }
            //confirmar
            if (MessageBox.Show(
                "Tem a certeza que pretende retirar de stock a roupa escolhida?",
                "Confirmar",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //apagar da bd  
                Roupa.ApagarRoupa(nroupa_escolhido, bd);


            }
            //limpar form
            LimparForm();
            //trocar botões

            //Atualizar a grelha
            AtualizaGrelha();

            AtualizaNrPaginas();
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = false;

        }


        private void button3_Click(object sender, EventArgs e)
        {
         
            ApagarRegisto();
            button3.Visible = true;
            button4.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
        }

        private void dgRoupas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            button3.Visible = true;
            button4.Visible = true;
            button1.Visible = true;
            button2.Visible = true;

            int linha = dgRoupas.CurrentCell.RowIndex;
            if (linha == -1)
            {
                return;
            }
            int nlivro = int.Parse(dgRoupas.Rows[linha].Cells[0].Value.ToString());
            Roupa escolhido = new Roupa();
            escolhido.Procurar(nlivro, bd);
            tbNome.Text = escolhido.Nome;
            cbGenero.Text = escolhido.Genero;
            nroupa_escolhido = escolhido.Nroupa;

            button1.Visible = false;

        }

        private void dgRoupas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nome = tbNome.Text;
            if (nome == "" || nome.Length < 3)
            {
                MessageBox.Show("Nome tem de ter pelo menos 3 letras.");
                tbNome.Focus();
                return;
            }
            string genero = cbGenero.Text;

            /*A capa é opcional*/

            
            //Criar um objeto roupa
            Roupa roupa = new Roupa();
            //Preencher as propriedades
            roupa.Nome = nome;
            roupa.Genero = genero;
            roupa.Nroupa = nroupa_escolhido;
            //Guardar na BD
            roupa.Atualizar(bd);
            //Limpar o form
            LimparForm();
            AtualizaGrelha();
            //TODO: manter selecionada a linha do último livro editado
        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
            printPreviewDialog1.ShowDialog();
        }

        private void imprimeGrelha(System.Drawing.Printing.PrintPageEventArgs e, DataGridView grelha)
        {
            Graphics impressora = e.Graphics;
            Font tipoLetra = new Font("Arial", 10);
            Font tipoLetraMaior = new Font("Arial", 12, FontStyle.Bold);
            Brush cor = Brushes.Black;
            float mesquerda, mdireita, msuperior, minferior, linha, largura;
            Pen caneta = new Pen(cor, 2);

            //margens
            mesquerda = printDocument1.DefaultPageSettings.Margins.Left;
            mdireita = printDocument1.DefaultPageSettings.Bounds.Right - mesquerda;
            msuperior = printDocument1.DefaultPageSettings.Margins.Top;
            minferior = printDocument1.DefaultPageSettings.Bounds.Height - msuperior;
            largura = mdireita - mesquerda;
            //calcular as colunas da grelha
            float[] colunas = new float[grelha.Columns.Count];
            float lgrelha = 0;
            for (int i = 0; i < grelha.Columns.Count; i++)
                lgrelha += grelha.Columns[i].Width;
            colunas[0] = mesquerda;
            float total = mesquerda, larguraColuna;
            for (int i = 0; i < grelha.Columns.Count - 1; i++)
            {
                larguraColuna = grelha.Columns[i].Width / lgrelha;
                colunas[i + 1] = larguraColuna * largura + total;
                total = colunas[i + 1];
            }
            //cabeçalhos
            for (int i = 0; i < grelha.Columns.Count; i++)
            {
                impressora.DrawString(grelha.Columns[i].HeaderText, tipoLetraMaior, cor, colunas[i], msuperior);
            }
            linha = msuperior + tipoLetraMaior.Height;
            //ciclo para percorrer a grelha
            int l;
            for (l = nrlinhas; l < grelha.Rows.Count; l++)
            {
                //desenhar linha
                impressora.DrawLine(caneta, mesquerda, linha, mdireita, linha);
                //escrever uma linha
                for (int c = 0; c < grelha.Columns.Count; c++)
                {
                    impressora.DrawString(grelha.Rows[l].Cells[c].Value.ToString(),
                        tipoLetra, cor, colunas[c], linha);
                }
                //avançar para linha seguinte
                linha = linha + tipoLetra.Height;
                //verificar se o papel acabou
                if (linha + tipoLetra.Height > minferior)
                    break;
            }
            //tem mais páginas?
            if (l < grelha.Rows.Count)
            {
                nrlinhas = l + 1;
                e.HasMorePages = true;
            }
            //rodapé
            impressora.DrawString("12ºH - Página " + nrpagina.ToString(), tipoLetra, cor, mesquerda, minferior);
            //nr página
            nrpagina++;
            //linhas
            //linha superior
            impressora.DrawLine(caneta, mesquerda, msuperior, mdireita, msuperior);
            //linha inferior
            impressora.DrawLine(caneta, mesquerda, linha, mdireita, linha);
            //colunas
            for (int c = 0; c < colunas.Length; c++)
            {
                impressora.DrawLine(caneta, colunas[c], msuperior, colunas[c], linha);
            }
            //linha lado direito
            impressora.DrawLine(caneta, mdireita, msuperior, mdireita, linha);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;


            LimparForm();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dgRoupas.DataSource = Roupa.PesquisaPorNome(bd, textBox1.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgRoupas.DataSource = Roupa.PesquisaPorGenero(bd, cmbGenero.Text);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            imprimeGrelha(e, dgRoupas);
        }
    }
}
