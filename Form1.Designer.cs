
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
            this.btnConnectReader = new System.Windows.Forms.Button();
            this.btnDetectcard = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnReadfromCard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnectReader
            // 
            this.btnConnectReader.Location = new System.Drawing.Point(478, 20);
            this.btnConnectReader.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnConnectReader.Name = "btnConnectReader";
            this.btnConnectReader.Size = new System.Drawing.Size(211, 29);
            this.btnConnectReader.TabIndex = 0;
            this.btnConnectReader.Text = "Start";
            this.btnConnectReader.UseVisualStyleBackColor = true;
            this.btnConnectReader.Click += new System.EventHandler(this.btnConnectReader_Click);
            // 
            // btnDetectcard
            // 
            this.btnDetectcard.Location = new System.Drawing.Point(445, 403);
            this.btnDetectcard.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnDetectcard.Name = "btnDetectcard";
            this.btnDetectcard.Size = new System.Drawing.Size(159, 44);
            this.btnDetectcard.TabIndex = 0;
            this.btnDetectcard.Text = "Write To Card";
            this.btnDetectcard.UseVisualStyleBackColor = true;
            this.btnDetectcard.Click += new System.EventHandler(this.btnDetectcard_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(41, 89);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(734, 297);
            this.textBox1.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(627, 403);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(148, 44);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "List Of Readers";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(170, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(301, 23);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnReadfromCard
            // 
            this.btnReadfromCard.Location = new System.Drawing.Point(278, 403);
            this.btnReadfromCard.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnReadfromCard.Name = "btnReadfromCard";
            this.btnReadfromCard.Size = new System.Drawing.Size(159, 44);
            this.btnReadfromCard.TabIndex = 5;
            this.btnReadfromCard.Text = "Read from Card";
            this.btnReadfromCard.UseVisualStyleBackColor = true;
            this.btnReadfromCard.Click += new System.EventHandler(this.btnReadfromCard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 467);
            this.Controls.Add(this.btnReadfromCard);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnDetectcard);
            this.Controls.Add(this.btnConnectReader);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "Form1";
            this.Text = "Smart Card Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConnectReader;
        private System.Windows.Forms.Button btnDetectcard;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnReadfromCard;
    }
}

