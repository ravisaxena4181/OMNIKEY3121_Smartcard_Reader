
namespace SCReader
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
            this.btnAllDevices = new System.Windows.Forms.Button();
            this.btnConnectReader = new System.Windows.Forms.Button();
            this.btnDetectcard = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAllDevices
            // 
            this.btnAllDevices.Location = new System.Drawing.Point(28, 46);
            this.btnAllDevices.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAllDevices.Name = "btnAllDevices";
            this.btnAllDevices.Size = new System.Drawing.Size(141, 28);
            this.btnAllDevices.TabIndex = 0;
            this.btnAllDevices.Text = "List All Devices";
            this.btnAllDevices.UseVisualStyleBackColor = true;
            this.btnAllDevices.Click += new System.EventHandler(this.btnAllDevices_Click);
            // 
            // btnConnectReader
            // 
            this.btnConnectReader.Location = new System.Drawing.Point(212, 46);
            this.btnConnectReader.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConnectReader.Name = "btnConnectReader";
            this.btnConnectReader.Size = new System.Drawing.Size(141, 28);
            this.btnConnectReader.TabIndex = 0;
            this.btnConnectReader.Text = "Connect Reader";
            this.btnConnectReader.UseVisualStyleBackColor = true;
            this.btnConnectReader.Click += new System.EventHandler(this.btnConnectReader_Click);
            // 
            // btnDetectcard
            // 
            this.btnDetectcard.Location = new System.Drawing.Point(376, 46);
            this.btnDetectcard.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDetectcard.Name = "btnDetectcard";
            this.btnDetectcard.Size = new System.Drawing.Size(141, 28);
            this.btnDetectcard.TabIndex = 0;
            this.btnDetectcard.Text = "Detect Card";
            this.btnDetectcard.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(28, 137);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(491, 196);
            this.textBox1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(418, 370);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(99, 28);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 408);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnDetectcard);
            this.Controls.Add(this.btnConnectReader);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAllDevices);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Smart Card Reader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAllDevices;
        private System.Windows.Forms.Button btnConnectReader;
        private System.Windows.Forms.Button btnDetectcard;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnExit;
    }
}

