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
            instructions = new LinkLabel();
            linkToLog = new LinkLabel();
            label5 = new Label();
            sendYearEndPurchasedTickets = new Button();
            SuspendLayout();
            // 
            // uploadETickets
            // 
            uploadETickets.Location = new Point(386, 128);
            uploadETickets.Margin = new Padding(2, 3, 2, 3);
            uploadETickets.Name = "uploadETickets";
            uploadETickets.Size = new Size(318, 57);
            uploadETickets.TabIndex = 1;
            uploadETickets.Text = "Upload E-Tickets";
            uploadETickets.UseVisualStyleBackColor = true;
            uploadETickets.Click += uploadETickets_Click;
            // 
            // sweepstakeYear
            // 
            sweepstakeYear.AutoSize = true;
            sweepstakeYear.Location = new Point(24, 27);
            sweepstakeYear.Margin = new Padding(2, 0, 2, 0);
            sweepstakeYear.Name = "sweepstakeYear";
            sweepstakeYear.Size = new Size(73, 41);
            sweepstakeYear.TabIndex = 2;
            sweepstakeYear.Text = "Year";
            // 
            // ticketsAvailableForSale
            // 
            ticketsAvailableForSale.AutoSize = true;
            ticketsAvailableForSale.Location = new Point(24, 68);
            ticketsAvailableForSale.Margin = new Padding(2, 0, 2, 0);
            ticketsAvailableForSale.Name = "ticketsAvailableForSale";
            ticketsAvailableForSale.Size = new Size(234, 41);
            ticketsAvailableForSale.TabIndex = 3;
            ticketsAvailableForSale.Text = "Available Tickets";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 128);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(333, 41);
            label1.TabIndex = 4;
            label1.Text = "Step 1: Upload E-tickets";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 232);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(412, 41);
            label2.TabIndex = 5;
            label2.Text = "Step 2: Verify Email Is working";
            // 
            // testEmail
            // 
            testEmail.Location = new Point(1103, 224);
            testEmail.Margin = new Padding(2, 3, 2, 3);
            testEmail.Name = "testEmail";
            testEmail.Size = new Size(187, 57);
            testEmail.TabIndex = 6;
            testEmail.Text = "Test Email";
            testEmail.UseVisualStyleBackColor = true;
            testEmail.Click += testEmail_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(457, 230);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(171, 41);
            label3.TabIndex = 7;
            label3.Text = "Enter Email:";
            // 
            // emailAddress
            // 
            emailAddress.Location = new Point(634, 230);
            emailAddress.Margin = new Padding(2, 3, 2, 3);
            emailAddress.Name = "emailAddress";
            emailAddress.Size = new Size(417, 47);
            emailAddress.TabIndex = 8;
            emailAddress.TextChanged += emailAddress_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 309);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(654, 41);
            label4.TabIndex = 9;
            label4.Text = "Step 3: Send Newly/weekly Purchased E-Tickets:";
            label4.Click += label4_Click;
            // 
            // uploadGemPurchases
            // 
            uploadGemPurchases.Location = new Point(670, 301);
            uploadGemPurchases.Margin = new Padding(2, 3, 2, 3);
            uploadGemPurchases.Name = "uploadGemPurchases";
            uploadGemPurchases.Size = new Size(449, 57);
            uploadGemPurchases.TabIndex = 10;
            uploadGemPurchases.Text = "Send Purchased E-Tickets";
            uploadGemPurchases.UseVisualStyleBackColor = true;
            uploadGemPurchases.Click += uploadGemPurchases_Click;
            // 
            // instructions
            // 
            instructions.AutoSize = true;
            instructions.Location = new Point(515, 644);
            instructions.Margin = new Padding(7, 0, 7, 0);
            instructions.Name = "instructions";
            instructions.Size = new Size(172, 41);
            instructions.TabIndex = 11;
            instructions.TabStop = true;
            instructions.Text = "Instructions";
            instructions.LinkClicked += instructionsClicked;
            // 
            // linkToLog
            // 
            linkToLog.AutoSize = true;
            linkToLog.Location = new Point(515, 430);
            linkToLog.Name = "linkToLog";
            linkToLog.Size = new Size(211, 41);
            linkToLog.TabIndex = 12;
            linkToLog.TabStop = true;
            linkToLog.Text = "View Error Log";
            linkToLog.LinkClicked += linkToLog_LinkClicked;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 389);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(748, 41);
            label5.TabIndex = 13;
            label5.Text = "Step 4: Send Year-End Purchased E-Tickets information:";
            label5.Click += label5_Click;
            // 
            // sendYearEndPurchasedTickets
            // 
            sendYearEndPurchasedTickets.Location = new Point(753, 381);
            sendYearEndPurchasedTickets.Margin = new Padding(2, 3, 2, 3);
            sendYearEndPurchasedTickets.Name = "sendYearEndPurchasedTickets";
            sendYearEndPurchasedTickets.Size = new Size(575, 57);
            sendYearEndPurchasedTickets.TabIndex = 14;
            sendYearEndPurchasedTickets.Text = "Send Year-End Purchased E-tickets Info";
            sendYearEndPurchasedTickets.UseVisualStyleBackColor = true;
            sendYearEndPurchasedTickets.Click += sendYearEndPurchasedTickets_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 748);
            Controls.Add(sendYearEndPurchasedTickets);
            Controls.Add(label5);
            Controls.Add(linkToLog);
            Controls.Add(instructions);
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
            Margin = new Padding(2, 3, 2, 3);
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
        private LinkLabel instructions;
        private LinkLabel linkToLog;
        private Label label5;
        private Button sendYearEndPurchasedTickets;
    }
}
