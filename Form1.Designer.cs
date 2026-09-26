namespace MidiVolume
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbMidiDevices = new ComboBox();
            bRefresh = new Button();
            SuspendLayout();
            // 
            // cbMidiDevices
            // 
            cbMidiDevices.FormattingEnabled = true;
            cbMidiDevices.Location = new Point(12, 12);
            cbMidiDevices.Name = "cbMidiDevices";
            cbMidiDevices.Size = new Size(147, 23);
            cbMidiDevices.TabIndex = 0;
            // 
            // bRefresh
            // 
            bRefresh.Location = new Point(165, 12);
            bRefresh.Name = "bRefresh";
            bRefresh.Size = new Size(61, 23);
            bRefresh.TabIndex = 1;
            bRefresh.Text = "Refresh";
            bRefresh.UseVisualStyleBackColor = true;
            bRefresh.Click += bRefresh_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(409, 336);
            Controls.Add(bRefresh);
            Controls.Add(cbMidiDevices);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cbMidiDevices;
        private Button bRefresh;
    }
}
