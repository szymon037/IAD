namespace pictures2
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.draw_button = new System.Windows.Forms.Button();
            this.epochs_text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LR_init = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LR_fin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RANGE_fin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.RANGE_init = new System.Windows.Forms.TextBox();
            this.color = new System.Windows.Forms.Button();
            this.chromo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.size_of_frame_text = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nn_height_text = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nn_width_text = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(801, 548);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // draw_button
            // 
            this.draw_button.Location = new System.Drawing.Point(810, 502);
            this.draw_button.Name = "draw_button";
            this.draw_button.Size = new System.Drawing.Size(100, 23);
            this.draw_button.TabIndex = 1;
            this.draw_button.Text = "Compress";
            this.draw_button.UseVisualStyleBackColor = true;
            this.draw_button.Click += new System.EventHandler(this.draw_button_Click);
            // 
            // epochs_text
            // 
            this.epochs_text.Location = new System.Drawing.Point(810, 27);
            this.epochs_text.Name = "epochs_text";
            this.epochs_text.Size = new System.Drawing.Size(100, 20);
            this.epochs_text.TabIndex = 2;
            this.epochs_text.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(807, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ilość epok";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(807, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "LR init";
            // 
            // LR_init
            // 
            this.LR_init.Location = new System.Drawing.Point(810, 237);
            this.LR_init.Name = "LR_init";
            this.LR_init.Size = new System.Drawing.Size(100, 20);
            this.LR_init.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(807, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "LR fin";
            // 
            // LR_fin
            // 
            this.LR_fin.Location = new System.Drawing.Point(810, 279);
            this.LR_fin.Name = "LR_fin";
            this.LR_fin.Size = new System.Drawing.Size(100, 20);
            this.LR_fin.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(807, 347);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Range fin";
            // 
            // RANGE_fin
            // 
            this.RANGE_fin.Location = new System.Drawing.Point(810, 363);
            this.RANGE_fin.Name = "RANGE_fin";
            this.RANGE_fin.Size = new System.Drawing.Size(100, 20);
            this.RANGE_fin.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(807, 305);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Range init";
            // 
            // RANGE_init
            // 
            this.RANGE_init.Location = new System.Drawing.Point(810, 321);
            this.RANGE_init.Name = "RANGE_init";
            this.RANGE_init.Size = new System.Drawing.Size(100, 20);
            this.RANGE_init.TabIndex = 8;
            // 
            // color
            // 
            this.color.Location = new System.Drawing.Point(823, 389);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(75, 23);
            this.color.TabIndex = 12;
            this.color.Text = "Color";
            this.color.UseVisualStyleBackColor = true;
            this.color.Click += new System.EventHandler(this.color_Click);
            // 
            // chromo
            // 
            this.chromo.Location = new System.Drawing.Point(823, 418);
            this.chromo.Name = "chromo";
            this.chromo.Size = new System.Drawing.Size(75, 23);
            this.chromo.TabIndex = 13;
            this.chromo.Text = "Chromo";
            this.chromo.UseVisualStyleBackColor = true;
            this.chromo.Click += new System.EventHandler(this.chromo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(807, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Size of frame";
            // 
            // size_of_frame_text
            // 
            this.size_of_frame_text.Location = new System.Drawing.Point(810, 66);
            this.size_of_frame_text.Name = "size_of_frame_text";
            this.size_of_frame_text.Size = new System.Drawing.Size(100, 20);
            this.size_of_frame_text.TabIndex = 14;
            this.size_of_frame_text.Text = "3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(807, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Height";
            // 
            // nn_height_text
            // 
            this.nn_height_text.Location = new System.Drawing.Point(810, 140);
            this.nn_height_text.Name = "nn_height_text";
            this.nn_height_text.Size = new System.Drawing.Size(100, 20);
            this.nn_height_text.TabIndex = 16;
            this.nn_height_text.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(807, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Width";
            // 
            // nn_width_text
            // 
            this.nn_width_text.Location = new System.Drawing.Point(810, 179);
            this.nn_width_text.Name = "nn_width_text";
            this.nn_width_text.Size = new System.Drawing.Size(100, 20);
            this.nn_width_text.TabIndex = 18;
            this.nn_width_text.Text = "10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(807, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 15);
            this.label9.TabIndex = 20;
            this.label9.Text = "Size of network";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(810, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Choose image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 560);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nn_width_text);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nn_height_text);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.size_of_frame_text);
            this.Controls.Add(this.chromo);
            this.Controls.Add(this.color);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RANGE_fin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RANGE_init);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LR_fin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LR_init);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.epochs_text);
            this.Controls.Add(this.draw_button);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button draw_button;
        private System.Windows.Forms.TextBox epochs_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LR_init;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox LR_fin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox RANGE_fin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox RANGE_init;
        private System.Windows.Forms.Button color;
        private System.Windows.Forms.Button chromo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox size_of_frame_text;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox nn_height_text;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox nn_width_text;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
    }
}

