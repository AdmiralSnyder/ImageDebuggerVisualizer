namespace ImageVisualizer
{
    partial class ImageForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ImageDisplayTC = new System.Windows.Forms.TabControl();
            this.GdiTP = new System.Windows.Forms.TabPage();
            this.WpfTP = new System.Windows.Forms.TabPage();
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.imageControl = new ImageVisualizer.ImageControl();
            this.UwpTP = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.ImageDisplayTC.SuspendLayout();
            this.WpfTP.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(495, 594);
            this.btnClose.Margin = new System.Windows.Forms.Padding(6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(187, 50);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ImageDisplayTC);
            this.panel1.Location = new System.Drawing.Point(22, 118);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 464);
            this.panel1.TabIndex = 2;
            // 
            // ImageDisplayTC
            // 
            this.ImageDisplayTC.Controls.Add(this.GdiTP);
            this.ImageDisplayTC.Controls.Add(this.WpfTP);
            this.ImageDisplayTC.Controls.Add(this.UwpTP);
            this.ImageDisplayTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageDisplayTC.Location = new System.Drawing.Point(0, 0);
            this.ImageDisplayTC.Name = "ImageDisplayTC";
            this.ImageDisplayTC.SelectedIndex = 0;
            this.ImageDisplayTC.Size = new System.Drawing.Size(656, 462);
            this.ImageDisplayTC.TabIndex = 7;
            // 
            // GdiTP
            // 
            this.GdiTP.Location = new System.Drawing.Point(4, 33);
            this.GdiTP.Name = "GdiTP";
            this.GdiTP.Padding = new System.Windows.Forms.Padding(3);
            this.GdiTP.Size = new System.Drawing.Size(648, 425);
            this.GdiTP.TabIndex = 0;
            this.GdiTP.Text = "GDI";
            this.GdiTP.UseVisualStyleBackColor = true;
            // 
            // WpfTP
            // 
            this.WpfTP.Controls.Add(this.elementHost);
            this.WpfTP.Location = new System.Drawing.Point(4, 33);
            this.WpfTP.Name = "WpfTP";
            this.WpfTP.Padding = new System.Windows.Forms.Padding(3);
            this.WpfTP.Size = new System.Drawing.Size(648, 425);
            this.WpfTP.TabIndex = 1;
            this.WpfTP.Text = "WPF";
            this.WpfTP.UseVisualStyleBackColor = true;
            // 
            // elementHost
            // 
            this.elementHost.BackColorTransparent = true;
            this.elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost.Location = new System.Drawing.Point(3, 3);
            this.elementHost.Margin = new System.Windows.Forms.Padding(6);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(642, 419);
            this.elementHost.TabIndex = 1;
            this.elementHost.Text = "elementHost";
            this.elementHost.Child = this.imageControl;
            // 
            // UwpTP
            // 
            this.UwpTP.Location = new System.Drawing.Point(4, 33);
            this.UwpTP.Name = "UwpTP";
            this.UwpTP.Padding = new System.Windows.Forms.Padding(3);
            this.UwpTP.Size = new System.Drawing.Size(648, 425);
            this.UwpTP.TabIndex = 2;
            this.UwpTP.Text = "UWP";
            this.UwpTP.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "&Expression:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "&Value:";
            // 
            // txtExpression
            // 
            this.txtExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpression.BackColor = System.Drawing.SystemColors.Control;
            this.txtExpression.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpression.Location = new System.Drawing.Point(222, 26);
            this.txtExpression.Margin = new System.Windows.Forms.Padding(6);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.ReadOnly = true;
            this.txtExpression.Size = new System.Drawing.Size(459, 29);
            this.txtExpression.TabIndex = 6;
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 666);
            this.Controls.Add(this.txtExpression);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(713, 684);
            this.Name = "ImageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Image Visualizer";
            this.panel1.ResumeLayout(false);
            this.ImageDisplayTC.ResumeLayout(false);
            this.WpfTP.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.TabControl ImageDisplayTC;
        private System.Windows.Forms.TabPage GdiTP;
        private System.Windows.Forms.TabPage WpfTP;
        private System.Windows.Forms.Integration.ElementHost elementHost;
        private ImageVisualizer.ImageControl imageControl;
        private System.Windows.Forms.TabPage UwpTP;
    }
}