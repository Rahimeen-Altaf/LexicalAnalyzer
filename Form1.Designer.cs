namespace LexicalAnalyzer
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
            this.path = new System.Windows.Forms.TextBox();
            this.find = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.analyzed = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.clearBtn = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tokenCountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // path
            // 
            this.path.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(235)))));
            this.path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.path.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.path.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(54)))), ((int)(((byte)(40)))));
            this.path.Location = new System.Drawing.Point(27, 62);
            this.path.Margin = new System.Windows.Forms.Padding(4);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(693, 27);
            this.path.TabIndex = 0;
            // 
            // find
            // 
            this.find.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(90)))), ((int)(((byte)(60)))));
            this.find.FlatAppearance.BorderSize = 0;
            this.find.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.find.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.find.ForeColor = System.Drawing.Color.White;
            this.find.Location = new System.Drawing.Point(728, 59);
            this.find.Margin = new System.Windows.Forms.Padding(4);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(133, 34);
            this.find.TabIndex = 1;
            this.find.Text = "Browse File";
            this.find.UseVisualStyleBackColor = false;
            this.find.Click += new System.EventHandler(this.find_Click);
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(235)))));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.textBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(54)))), ((int)(((byte)(40)))));
            this.textBox.Location = new System.Drawing.Point(27, 135);
            this.textBox.Margin = new System.Windows.Forms.Padding(4);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(639, 467);
            this.textBox.TabIndex = 2;
            this.textBox.Text = "";
            // 
            // analyzed
            // 
            this.analyzed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(33)))));
            this.analyzed.FlatAppearance.BorderSize = 0;
            this.analyzed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analyzed.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.analyzed.ForeColor = System.Drawing.Color.White;
            this.analyzed.Location = new System.Drawing.Point(869, 59);
            this.analyzed.Margin = new System.Windows.Forms.Padding(4);
            this.analyzed.Name = "analyzed";
            this.analyzed.Size = new System.Drawing.Size(138, 34);
            this.analyzed.TabIndex = 4;
            this.analyzed.Text = "Analyze";
            this.analyzed.UseVisualStyleBackColor = false;
            this.analyzed.Click += new System.EventHandler(this.analyzed_Click);
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(235)))));
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputBox.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.outputBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(54)))), ((int)(((byte)(40)))));
            this.outputBox.Location = new System.Drawing.Point(693, 135);
            this.outputBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(594, 467);
            this.outputBox.TabIndex = 5;
            this.outputBox.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text Files|*.txt|C# Files|*.cs|Java Files|*.java|Python Files|*.py|JavaScript Fil" +
    "es|*.js|C++ Files|*.cpp|All Files|*.*";
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(90)))), ((int)(((byte)(60)))));
            this.clearBtn.FlatAppearance.BorderSize = 0;
            this.clearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.clearBtn.ForeColor = System.Drawing.Color.White;
            this.clearBtn.Location = new System.Drawing.Point(1015, 59);
            this.clearBtn.Margin = new System.Windows.Forms.Padding(4);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(133, 34);
            this.clearBtn.TabIndex = 6;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = false;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // exportBtn
            // 
            this.exportBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(90)))), ((int)(((byte)(60)))));
            this.exportBtn.FlatAppearance.BorderSize = 0;
            this.exportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.exportBtn.ForeColor = System.Drawing.Color.White;
            this.exportBtn.Location = new System.Drawing.Point(1156, 59);
            this.exportBtn.Margin = new System.Windows.Forms.Padding(4);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(133, 34);
            this.exportBtn.TabIndex = 11;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = false;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(33)))));
            this.label1.Location = new System.Drawing.Point(27, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "File Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(33)))));
            this.label2.Location = new System.Drawing.Point(27, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Source Code:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(33)))));
            this.label3.Location = new System.Drawing.Point(693, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tokens:";
            // 
            // tokenCountLabel
            // 
            this.tokenCountLabel.AutoSize = true;
            this.tokenCountLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tokenCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(67)))), ((int)(((byte)(33)))));
            this.tokenCountLabel.Location = new System.Drawing.Point(1033, 97);
            this.tokenCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tokenCountLabel.Name = "tokenCountLabel";
            this.tokenCountLabel.Padding = new System.Windows.Forms.Padding(13, 6, 13, 6);
            this.tokenCountLabel.Size = new System.Drawing.Size(154, 35);
            this.tokenCountLabel.TabIndex = 10;
            this.tokenCountLabel.Text = "Total Tokens: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(220)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(1317, 628);
            this.Controls.Add(this.tokenCountLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.analyzed);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.find);
            this.Controls.Add(this.path);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lexical Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button find;
        private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.Button analyzed;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label tokenCountLabel;
    }
}

