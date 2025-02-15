namespace FootballCrazeSweepstakes
{
    partial class Dashboard
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
            uploadETickets = new Button();
            sweepstakeYear = new Label();
            ticketsAvailableForSale = new Label();
            label1 = new Label();
            label2 = new Label();
            testEmail = new Button();
            label3 = new Label();
            emailAddress = new TextBox();
            label4 = new Label();
            uploadGemPurchases = new Button();
            SuspendLayout();
            // 
            // uploadETickets
            // 
            uploadETickets.Location = new Point(386, 128);
            uploadETickets.Name = "uploadETickets";
            uploadETickets.Size = new Size(319, 58);
            uploadETickets.TabIndex = 1;
            uploadETickets.Text = "Upload E-Tickets";
            uploadETickets.UseVisualStyleBackColor = true;
            uploadETickets.Click += uploadETickets_Click;
            // 
            // sweepstakeYear
            // 
            sweepstakeYear.AutoSize = true;
            sweepstakeYear.Location = new Point(25, 28);
            sweepstakeYear.Name = "sweepstakeYear";
            sweepstakeYear.Size = new Size(73, 41);
            sweepstakeYear.TabIndex = 2;
            sweepstakeYear.Text = "Year";
            // 
            // ticketsAvailableForSale
            // 
            ticketsAvailableForSale.AutoSize = true;
            ticketsAvailableForSale.Location = new Point(25, 69);
            ticketsAvailableForSale.Name = "ticketsAvailableForSale";
            ticketsAvailableForSale.Size = new Size(234, 41);
            ticketsAvailableForSale.TabIndex = 3;
            ticketsAvailableForSale.Text = "Available Tickets";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 128);
            label1.Name = "label1";
            label1.Size = new Size(333, 41);
            label1.TabIndex = 4;
            label1.Text = "Step 1: Upload E-tickets";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 233);
            label2.Name = "label2";
            label2.Size = new Size(412, 41);
            label2.TabIndex = 5;
            label2.Text = "Step 2: Verify Email Is working";
            // 
            // testEmail
            // 
            testEmail.Location = new Point(1102, 224);
            testEmail.Name = "testEmail";
            testEmail.Size = new Size(188, 58);
            testEmail.TabIndex = 6;
            testEmail.Text = "Test Email";
            testEmail.UseVisualStyleBackColor = true;
            testEmail.Click += testEmail_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(456, 230);
            label3.Name = "label3";
            label3.Size = new Size(171, 41);
            label3.TabIndex = 7;
            label3.Text = "Enter Email:";
            // 
            // emailAddress
            // 
            emailAddress.Location = new Point(633, 230);
            emailAddress.Name = "emailAddress";
            emailAddress.Size = new Size(418, 47);
            emailAddress.TabIndex = 8;
            emailAddress.TextChanged += emailAddress_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 310);
            label4.Name = "label4";
            label4.Size = new Size(549, 41);
            label4.TabIndex = 9;
            label4.Text = "Step 3: Upload Weekly Gems purchases:";
            // 
            // uploadGemPurchases
            // 
            uploadGemPurchases.Location = new Point(601, 301);
            uploadGemPurchases.Name = "uploadGemPurchases";
            uploadGemPurchases.Size = new Size(450, 58);
            uploadGemPurchases.TabIndex = 10;
            uploadGemPurchases.Text = "Upload Gems Up Purchases";
            uploadGemPurchases.UseVisualStyleBackColor = true;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 583);
            Controls.Add(uploadGemPurchases);
            Controls.Add(label4);
            Controls.Add(emailAddress);
            Controls.Add(label3);
            Controls.Add(testEmail);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ticketsAvailableForSale);
            Controls.Add(sweepstakeYear);
            Controls.Add(uploadETickets);
            Name = "Dashboard";
            Text = "Football Craze Sweepstakes Dashboard";
            Load += Dashboard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button uploadETickets;
        private Label sweepstakeYear;
        private Label ticketsAvailableForSale;
        private Label label1;
        private Label label2;
        private Button testEmail;
        private Label label3;
        private TextBox emailAddress;
        private Label label4;
        private Button uploadGemPurchases;
    }
}
