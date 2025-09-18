namespace muzLibHelper
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
            inputTextBox = new TextBox();
            searchButton = new Button();
            searchResultGridView = new DataGridView();
            TrackName = new DataGridViewTextBoxColumn();
            TrackAutors = new DataGridViewTextBoxColumn();
            TrackFile = new DataGridViewTextBoxColumn();
            TrackGroup = new DataGridViewTextBoxColumn();
            addBtton = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button1 = new Button();
            operationResultLabel = new Label();
            label1 = new Label();
            tabPage2 = new TabPage();
            tabPage4 = new TabPage();
            massAddReasultLabel = new Label();
            massAddButton = new Button();
            massAddRichTextBox = new RichTextBox();
            tabPage3 = new TabPage();
            helpRichTextBox = new RichTextBox();
            tabPage5 = new TabPage();
            logRichTextBox = new RichTextBox();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)searchResultGridView).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage5.SuspendLayout();
            SuspendLayout();
            // 
            // inputTextBox
            // 
            inputTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inputTextBox.Location = new Point(14, 13);
            inputTextBox.Margin = new Padding(6, 7, 6, 7);
            inputTextBox.Name = "inputTextBox";
            inputTextBox.Size = new Size(1139, 43);
            inputTextBox.TabIndex = 0;
            // 
            // searchButton
            // 
            searchButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchButton.Location = new Point(1169, 7);
            searchButton.Margin = new Padding(6, 7, 6, 7);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(161, 57);
            searchButton.TabIndex = 1;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // searchResultGridView
            // 
            searchResultGridView.AllowUserToAddRows = false;
            searchResultGridView.AllowUserToDeleteRows = false;
            searchResultGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            searchResultGridView.BackgroundColor = SystemColors.ButtonHighlight;
            searchResultGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            searchResultGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            searchResultGridView.Columns.AddRange(new DataGridViewColumn[] { TrackName, TrackAutors, TrackFile, TrackGroup });
            searchResultGridView.Location = new Point(9, 10);
            searchResultGridView.Margin = new Padding(6, 7, 6, 7);
            searchResultGridView.Name = "searchResultGridView";
            searchResultGridView.ReadOnly = true;
            searchResultGridView.RowHeadersVisible = false;
            searchResultGridView.RowHeadersWidth = 92;
            searchResultGridView.Size = new Size(1327, 125);
            searchResultGridView.TabIndex = 3;
            // 
            // TrackName
            // 
            TrackName.HeaderText = "Name";
            TrackName.MinimumWidth = 11;
            TrackName.Name = "TrackName";
            TrackName.ReadOnly = true;
            TrackName.Width = 225;
            // 
            // TrackAutors
            // 
            TrackAutors.HeaderText = "Autors";
            TrackAutors.MinimumWidth = 11;
            TrackAutors.Name = "TrackAutors";
            TrackAutors.ReadOnly = true;
            TrackAutors.Width = 225;
            // 
            // TrackFile
            // 
            TrackFile.HeaderText = "File";
            TrackFile.MinimumWidth = 11;
            TrackFile.Name = "TrackFile";
            TrackFile.ReadOnly = true;
            TrackFile.Width = 225;
            // 
            // TrackGroup
            // 
            TrackGroup.HeaderText = "Group";
            TrackGroup.MinimumWidth = 11;
            TrackGroup.Name = "TrackGroup";
            TrackGroup.ReadOnly = true;
            TrackGroup.Width = 225;
            // 
            // addBtton
            // 
            addBtton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addBtton.Location = new Point(1168, 78);
            addBtton.Margin = new Padding(6, 7, 6, 7);
            addBtton.Name = "addBtton";
            addBtton.Size = new Size(161, 57);
            addBtton.TabIndex = 6;
            addBtton.Text = "Add";
            addBtton.UseVisualStyleBackColor = true;
            addBtton.Click += addButton_Click;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Location = new Point(12, 6);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1365, 210);
            tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(operationResultLabel);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(inputTextBox);
            tabPage1.Controls.Add(addBtton);
            tabPage1.Controls.Add(searchButton);
            tabPage1.Location = new Point(10, 55);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1345, 145);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Main";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(790, 78);
            button1.Name = "button1";
            button1.Size = new Size(123, 50);
            button1.TabIndex = 9;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // operationResultLabel
            // 
            operationResultLabel.AutoSize = true;
            operationResultLabel.Location = new Point(127, 78);
            operationResultLabel.Margin = new Padding(6, 0, 6, 0);
            operationResultLabel.Name = "operationResultLabel";
            operationResultLabel.Size = new Size(77, 37);
            operationResultLabel.TabIndex = 8;
            operationResultLabel.Text = "none";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 78);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(113, 37);
            label1.TabIndex = 7;
            label1.Text = "Result =";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(searchResultGridView);
            tabPage2.Location = new Point(10, 55);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1345, 145);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Search";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(massAddReasultLabel);
            tabPage4.Controls.Add(massAddButton);
            tabPage4.Controls.Add(massAddRichTextBox);
            tabPage4.Location = new Point(10, 55);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(1345, 145);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Mass add";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // massAddReasultLabel
            // 
            massAddReasultLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            massAddReasultLabel.AutoSize = true;
            massAddReasultLabel.Location = new Point(1202, 85);
            massAddReasultLabel.Name = "massAddReasultLabel";
            massAddReasultLabel.Size = new Size(88, 37);
            massAddReasultLabel.TabIndex = 2;
            massAddReasultLabel.Text = "Result";
            // 
            // massAddButton
            // 
            massAddButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            massAddButton.Location = new Point(1163, 14);
            massAddButton.Name = "massAddButton";
            massAddButton.Size = new Size(169, 52);
            massAddButton.TabIndex = 1;
            massAddButton.Text = "Add";
            massAddButton.UseVisualStyleBackColor = true;
            massAddButton.Click += massAddButton_Click;
            // 
            // massAddRichTextBox
            // 
            massAddRichTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            massAddRichTextBox.Location = new Point(3, 3);
            massAddRichTextBox.Name = "massAddRichTextBox";
            massAddRichTextBox.Size = new Size(1143, 139);
            massAddRichTextBox.TabIndex = 0;
            massAddRichTextBox.Text = "";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(helpRichTextBox);
            tabPage3.Location = new Point(10, 55);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1345, 145);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Help";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // helpRichTextBox
            // 
            helpRichTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            helpRichTextBox.Location = new Point(3, 3);
            helpRichTextBox.Name = "helpRichTextBox";
            helpRichTextBox.ReadOnly = true;
            helpRichTextBox.Size = new Size(1339, 139);
            helpRichTextBox.TabIndex = 0;
            helpRichTextBox.Text = "";
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(logRichTextBox);
            tabPage5.Location = new Point(10, 55);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(1345, 145);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Log";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // logRichTextBox
            // 
            logRichTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logRichTextBox.Location = new Point(3, 3);
            logRichTextBox.Name = "logRichTextBox";
            logRichTextBox.ReadOnly = true;
            logRichTextBox.Size = new Size(1339, 142);
            logRichTextBox.TabIndex = 0;
            logRichTextBox.Text = "";
            // 
            // button2
            // 
            button2.Location = new Point(941, 78);
            button2.Name = "button2";
            button2.Size = new Size(123, 50);
            button2.TabIndex = 10;
            button2.Text = "Undo";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1382, 223);
            Controls.Add(tabControl1);
            Margin = new Padding(6, 7, 6, 7);
            Name = "Form1";
            Text = "muzLibHelper v1.1";
            ((System.ComponentModel.ISupportInitialize)searchResultGridView).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox inputTextBox;
        private Button searchButton;
        private DataGridView searchResultGridView;
        private Button addBtton;
        private DataGridViewTextBoxColumn TrackName;
        private DataGridViewTextBoxColumn TrackAutors;
        private DataGridViewTextBoxColumn TrackFile;
        private DataGridViewTextBoxColumn TrackGroup;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label operationResultLabel;
        private Label label1;
        private TabPage tabPage3;
        private RichTextBox helpRichTextBox;
        private TabPage tabPage4;
        private RichTextBox massAddRichTextBox;
        private Label massAddReasultLabel;
        private Button massAddButton;
        private Button button1;
        private TabPage tabPage5;
        private RichTextBox logRichTextBox;
        private Button button2;
    }
}
