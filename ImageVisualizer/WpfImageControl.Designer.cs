namespace ImageVisualizer
{
    partial class WpfImageControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ElementHost = new System.Windows.Forms.Integration.ElementHost();
            this.imageControl1 = new ImageVisualizer.ImageControlWpf();
            this.SuspendLayout();
            // 
            // ElementHost
            // 
            this.ElementHost.BackColorTransparent = true;
            this.ElementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementHost.Location = new System.Drawing.Point(0, 0);
            this.ElementHost.Name = "ElementHost";
            this.ElementHost.Size = new System.Drawing.Size(150, 150);
            this.ElementHost.TabIndex = 2;
            this.ElementHost.Text = "elementHost";
            this.ElementHost.Child = this.imageControl1;
            // 
            // WpfImageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ElementHost);
            this.Name = "WpfImageControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost ElementHost;
        private ImageControlWpf imageControl1;
    }
}
