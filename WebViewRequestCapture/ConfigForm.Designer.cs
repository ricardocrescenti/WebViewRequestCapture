namespace WebViewRequestCapture
{
    partial class ConfigForm
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
            textAPPTitle = new TextBox();
            textAPIKey = new TextBox();
            textStartPage = new TextBox();
            textMimeTypeFilter = new TextBox();
            textURLFilter = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnSalvar = new Button();
            label7 = new Label();
            btnCarregarIcone = new Button();
            numericAPIPort = new NumericUpDown();
            textIconPath = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numericAPIPort).BeginInit();
            SuspendLayout();
            // 
            // textAPPTitle
            // 
            textAPPTitle.Location = new Point(12, 27);
            textAPPTitle.Name = "textAPPTitle";
            textAPPTitle.Size = new Size(300, 23);
            textAPPTitle.TabIndex = 0;
            // 
            // textAPIKey
            // 
            textAPIKey.Location = new Point(12, 71);
            textAPIKey.Name = "textAPIKey";
            textAPIKey.Size = new Size(300, 23);
            textAPIKey.TabIndex = 1;
            // 
            // textStartPage
            // 
            textStartPage.Location = new Point(12, 159);
            textStartPage.Name = "textStartPage";
            textStartPage.Size = new Size(300, 23);
            textStartPage.TabIndex = 3;
            // 
            // textMimeTypeFilter
            // 
            textMimeTypeFilter.Location = new Point(12, 247);
            textMimeTypeFilter.Name = "textMimeTypeFilter";
            textMimeTypeFilter.Size = new Size(300, 23);
            textMimeTypeFilter.TabIndex = 4;
            textMimeTypeFilter.Text = "application/json";
            // 
            // textURLFilter
            // 
            textURLFilter.Location = new Point(12, 203);
            textURLFilter.Name = "textURLFilter";
            textURLFilter.Size = new Size(300, 23);
            textURLFilter.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 6;
            label1.Text = "Titulo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 53);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 7;
            label2.Text = "Chave da API";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 97);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 8;
            label3.Text = "Porta da API";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 141);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 9;
            label4.Text = "Pagina Inicial";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 185);
            label5.Name = "label5";
            label5.Size = new Size(74, 15);
            label5.TabIndex = 10;
            label5.Text = "Filtro de URL";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 229);
            label6.Name = "label6";
            label6.Size = new Size(93, 15);
            label6.TabIndex = 11;
            label6.Text = "Filtro MimeType";
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(12, 329);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(300, 25);
            btnSalvar.TabIndex = 12;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnGerar_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 273);
            label7.Name = "label7";
            label7.Size = new Size(36, 15);
            label7.TabIndex = 14;
            label7.Text = "Icone";
            // 
            // btnCarregarIcone
            // 
            btnCarregarIcone.Location = new Point(287, 290);
            btnCarregarIcone.Name = "btnCarregarIcone";
            btnCarregarIcone.Size = new Size(25, 23);
            btnCarregarIcone.TabIndex = 15;
            btnCarregarIcone.Text = "...";
            btnCarregarIcone.UseVisualStyleBackColor = true;
            btnCarregarIcone.Click += btnCarregarIcone_Click;
            // 
            // numericAPIPort
            // 
            numericAPIPort.InterceptArrowKeys = false;
            numericAPIPort.Location = new Point(12, 115);
            numericAPIPort.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numericAPIPort.Name = "numericAPIPort";
            numericAPIPort.Size = new Size(300, 23);
            numericAPIPort.TabIndex = 16;
            numericAPIPort.Value = new decimal(new int[] { 8001, 0, 0, 0 });
            // 
            // textIconPath
            // 
            textIconPath.Location = new Point(12, 291);
            textIconPath.Name = "textIconPath";
            textIconPath.Size = new Size(269, 23);
            textIconPath.TabIndex = 18;
            // 
            // CreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(329, 366);
            Controls.Add(textIconPath);
            Controls.Add(numericAPIPort);
            Controls.Add(btnCarregarIcone);
            Controls.Add(label7);
            Controls.Add(btnSalvar);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textURLFilter);
            Controls.Add(textMimeTypeFilter);
            Controls.Add(textStartPage);
            Controls.Add(textAPIKey);
            Controls.Add(textAPPTitle);
            MaximizeBox = false;
            MaximumSize = new Size(345, 405);
            MinimumSize = new Size(345, 405);
            Name = "CreateForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WebView Request Capture (Config)";
            ((System.ComponentModel.ISupportInitialize)numericAPIPort).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textAPPTitle;
        private TextBox textAPIKey;
        private TextBox textStartPage;
        private TextBox textMimeTypeFilter;
        private TextBox textURLFilter;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnSalvar;
        private Label label7;
        private Button btnCarregarIcone;
        private NumericUpDown numericAPIPort;
        private TextBox textIconPath;
    }
}