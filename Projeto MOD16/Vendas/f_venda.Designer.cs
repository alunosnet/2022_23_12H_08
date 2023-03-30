namespace Projeto_MOD16.Vendas
{
    partial class f_venda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dtDataVenda = new System.Windows.Forms.DateTimePicker();
            this.cbRoupas = new System.Windows.Forms.ComboBox();
            this.cbClientes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgVendas = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgVendas)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(98, 189);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 45);
            this.button1.TabIndex = 10;
            this.button1.Text = "Vender";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtDataVenda
            // 
            this.dtDataVenda.Location = new System.Drawing.Point(116, 120);
            this.dtDataVenda.Name = "dtDataVenda";
            this.dtDataVenda.Size = new System.Drawing.Size(147, 20);
            this.dtDataVenda.TabIndex = 9;
            // 
            // cbRoupas
            // 
            this.cbRoupas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRoupas.FormattingEnabled = true;
            this.cbRoupas.Location = new System.Drawing.Point(116, 80);
            this.cbRoupas.Name = "cbRoupas";
            this.cbRoupas.Size = new System.Drawing.Size(148, 21);
            this.cbRoupas.TabIndex = 7;
            // 
            // cbClientes
            // 
            this.cbClientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClientes.FormattingEnabled = true;
            this.cbClientes.Location = new System.Drawing.Point(116, 41);
            this.cbClientes.Name = "cbClientes";
            this.cbClientes.Size = new System.Drawing.Size(148, 21);
            this.cbClientes.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data Venda";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Roupa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Clientes";
            // 
            // dgVendas
            // 
            this.dgVendas.AllowUserToResizeColumns = false;
            this.dgVendas.AllowUserToResizeRows = false;
            this.dgVendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVendas.Location = new System.Drawing.Point(299, 41);
            this.dgVendas.Name = "dgVendas";
            this.dgVendas.Size = new System.Drawing.Size(270, 193);
            this.dgVendas.TabIndex = 11;
            this.dgVendas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgVendas_CellClick);
            this.dgVendas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(299, 240);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 41);
            this.button3.TabIndex = 16;
            this.button3.Text = "Apagar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(481, 240);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 41);
            this.button4.TabIndex = 17;
            this.button4.Text = "Atualizar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(481, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 27);
            this.button2.TabIndex = 18;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // f_venda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 316);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dgVendas);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtDataVenda);
            this.Controls.Add(this.cbRoupas);
            this.Controls.Add(this.cbClientes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "f_venda";
            this.Text = "f_venda";
            this.Load += new System.EventHandler(this.f_venda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgVendas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtDataVenda;
        private System.Windows.Forms.ComboBox cbRoupas;
        private System.Windows.Forms.ComboBox cbClientes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgVendas;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
    }
}