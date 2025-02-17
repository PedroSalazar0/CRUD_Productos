namespace CRUD_ProductosxCategorias
{
    partial class FormVerInforme
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
            this.webViewInforme = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.webViewInforme)).BeginInit();
            this.SuspendLayout();
            // 
            // webViewInforme
            // 
            this.webViewInforme.AllowExternalDrop = true;
            this.webViewInforme.CreationProperties = null;
            this.webViewInforme.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webViewInforme.Location = new System.Drawing.Point(2, -5);
            this.webViewInforme.Name = "webViewInforme";
            this.webViewInforme.Size = new System.Drawing.Size(1086, 633);
            this.webViewInforme.TabIndex = 0;
            this.webViewInforme.ZoomFactor = 1D;
            this.webViewInforme.Click += new System.EventHandler(this.webViewInforme_Click);
            // 
            // FormVerInforme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 624);
            this.Controls.Add(this.webViewInforme);
            this.Name = "FormVerInforme";
            this.Text = "Informe de productos:";
            this.Load += new System.EventHandler(this.FormVerInforme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webViewInforme)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webViewInforme;
    }
}