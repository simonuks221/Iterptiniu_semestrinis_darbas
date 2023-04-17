namespace PCKodas
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ConnectionStatusLabel = new System.Windows.Forms.Label();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.CompassBox = new System.Windows.Forms.GroupBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CompassHeadingLabel = new System.Windows.Forms.Label();
            this.SaveToFileButton = new System.Windows.Forms.Button();
            this.ConfigTextBox = new System.Windows.Forms.TextBox();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ExistingConfigLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(131, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 0;
            // 
            // ConnectionStatusLabel
            // 
            this.ConnectionStatusLabel.AutoSize = true;
            this.ConnectionStatusLabel.Location = new System.Drawing.Point(295, 31);
            this.ConnectionStatusLabel.Name = "ConnectionStatusLabel";
            this.ConnectionStatusLabel.Size = new System.Drawing.Size(67, 16);
            this.ConnectionStatusLabel.TabIndex = 1;
            this.ConnectionStatusLabel.Text = "Status text";
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // CompassBox
            // 
            this.CompassBox.Location = new System.Drawing.Point(12, 78);
            this.CompassBox.Name = "CompassBox";
            this.CompassBox.Size = new System.Drawing.Size(406, 322);
            this.CompassBox.TabIndex = 4;
            this.CompassBox.TabStop = false;
            this.CompassBox.Text = "Compass";
            this.CompassBox.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(6, 25);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(99, 28);
            this.ConnectButton.TabIndex = 5;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ConnectButton);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Location = new System.Drawing.Point(527, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 72);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connect to COM port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(440, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Compass heading:";
            // 
            // CompassHeadingLabel
            // 
            this.CompassHeadingLabel.AutoSize = true;
            this.CompassHeadingLabel.Location = new System.Drawing.Point(440, 109);
            this.CompassHeadingLabel.Name = "CompassHeadingLabel";
            this.CompassHeadingLabel.Size = new System.Drawing.Size(28, 16);
            this.CompassHeadingLabel.TabIndex = 8;
            this.CompassHeadingLabel.Text = "000";
            // 
            // SaveToFileButton
            // 
            this.SaveToFileButton.Location = new System.Drawing.Point(443, 160);
            this.SaveToFileButton.Name = "SaveToFileButton";
            this.SaveToFileButton.Size = new System.Drawing.Size(118, 23);
            this.SaveToFileButton.TabIndex = 9;
            this.SaveToFileButton.Text = "Save to file";
            this.SaveToFileButton.UseVisualStyleBackColor = true;
            this.SaveToFileButton.Click += new System.EventHandler(this.SaveToFileButton_Click);
            // 
            // ConfigTextBox
            // 
            this.ConfigTextBox.Location = new System.Drawing.Point(647, 322);
            this.ConfigTextBox.Name = "ConfigTextBox";
            this.ConfigTextBox.Size = new System.Drawing.Size(100, 22);
            this.ConfigTextBox.TabIndex = 10;
            // 
            // ConfigButton
            // 
            this.ConfigButton.Location = new System.Drawing.Point(647, 366);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(118, 23);
            this.ConfigButton.TabIndex = 11;
            this.ConfigButton.Text = "Send config";
            this.ConfigButton.UseVisualStyleBackColor = true;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(644, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Enter new config value:";
            // 
            // ExistingConfigLabel
            // 
            this.ExistingConfigLabel.AutoSize = true;
            this.ExistingConfigLabel.Enabled = false;
            this.ExistingConfigLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExistingConfigLabel.Location = new System.Drawing.Point(458, 291);
            this.ExistingConfigLabel.Name = "ExistingConfigLabel";
            this.ExistingConfigLabel.Size = new System.Drawing.Size(140, 17);
            this.ExistingConfigLabel.TabIndex = 13;
            this.ExistingConfigLabel.Text = "Existing config value:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ExistingConfigLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.ConfigTextBox);
            this.Controls.Add(this.SaveToFileButton);
            this.Controls.Add(this.CompassHeadingLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CompassBox);
            this.Controls.Add(this.ConnectionStatusLabel);
            this.Name = "Form1";
            this.Text = "Kompaso programa";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label ConnectionStatusLabel;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.GroupBox CompassBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label CompassHeadingLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveToFileButton;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.TextBox ConfigTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ExistingConfigLabel;
    }
}

