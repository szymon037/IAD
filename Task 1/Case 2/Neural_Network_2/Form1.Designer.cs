namespace Neural_Network_2
{
    partial class Form1
    {
        private OxyPlot.WindowsForms.PlotView pv;
        private OxyPlot.WindowsForms.PlotView pv2;
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.pv = new OxyPlot.WindowsForms.PlotView();
            this.pv2 = new OxyPlot.WindowsForms.PlotView();
            this.SuspendLayout();
            // 
            // pv
            // 
            this.pv.Location = new System.Drawing.Point(0, 0);
            this.pv.Name = "pv";
            this.pv.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.pv.Size = new System.Drawing.Size(900, 500);
            this.pv.TabIndex = 0;
            this.pv.Text = "plot1";
            this.pv.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.pv.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pv.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // pv2
            // 
            this.pv2.Location = new System.Drawing.Point(0, 500);
            this.pv2.Name = "pv2";
            this.pv2.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.pv2.Size = new System.Drawing.Size(900, 500);
            this.pv2.TabIndex = 0;
            this.pv2.Text = "plot2";
            this.pv2.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.pv2.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pv2.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 1042);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

