using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CryptoTrackerApp.Views
{
    partial class AssetGridForm
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
        /// </summary> .
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            rank = new DataGridViewTextBoxColumn();
            symbol = new DataGridViewTextBoxColumn();
            name = new DataGridViewTextBoxColumn();
            supply = new DataGridViewTextBoxColumn();
            maxSupply = new DataGridViewTextBoxColumn();
            marketCapUsd = new DataGridViewTextBoxColumn();
            volumeUsd24Hr = new DataGridViewTextBoxColumn();
            priceUsd = new DataGridViewTextBoxColumn();
            changePercent24Hr = new DataGridViewTextBoxColumn();
            vwap24Hr = new DataGridViewTextBoxColumn();
            explorer = new DataGridViewTextBoxColumn();
            AddFavorite = new Button();
            searchTextBox = new TextBox();
            searchButton = new Button();
            btnHome = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.FromArgb(0, 18, 30);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(1, 29, 43);
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Sans Serif Collection", 8F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(1, 29, 43);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { rank, symbol, name, supply, maxSupply, marketCapUsd, volumeUsd24Hr, priceUsd, changePercent24Hr, vwap24Hr, explorer });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(1, 26, 43);
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Sans Serif Collection", 6F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(56, 152, 213);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(1, 26, 43);
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.GridColor = Color.FromArgb(1, 26, 43);
            dataGridView1.Location = new Point(26, 93);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1194, 480);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // rank
            // 
            rank.HeaderText = "Rank";
            rank.Name = "rank";
            rank.ReadOnly = true;
            // 
            // symbol
            // 
            symbol.HeaderText = "Symbol";
            symbol.Name = "symbol";
            symbol.ReadOnly = true;
            // 
            // name
            // 
            name.HeaderText = "Name";
            name.Name = "name";
            name.ReadOnly = true;
            // 
            // supply
            // 
            supply.HeaderText = "Supply";
            supply.Name = "supply";
            supply.ReadOnly = true;
            // 
            // maxSupply
            // 
            maxSupply.HeaderText = "Max Supply";
            maxSupply.Name = "maxSupply";
            maxSupply.ReadOnly = true;
            // 
            // marketCapUsd
            // 
            marketCapUsd.HeaderText = "Market Cap USD";
            marketCapUsd.Name = "marketCapUsd";
            marketCapUsd.ReadOnly = true;
            // 
            // volumeUsd24Hr
            // 
            volumeUsd24Hr.HeaderText = "Volume USD 24Hr";
            volumeUsd24Hr.Name = "volumeUsd24Hr";
            volumeUsd24Hr.ReadOnly = true;
            // 
            // priceUsd
            // 
            priceUsd.HeaderText = "Price USD";
            priceUsd.Name = "priceUsd";
            priceUsd.ReadOnly = true;
            // 
            // changePercent24Hr
            // 
            changePercent24Hr.HeaderText = "Change Percent 24Hr";
            changePercent24Hr.Name = "changePercent24Hr";
            changePercent24Hr.ReadOnly = true;
            // 
            // vwap24Hr
            // 
            vwap24Hr.HeaderText = "VWAP";
            vwap24Hr.Name = "vwap24Hr";
            vwap24Hr.ReadOnly = true;
            // 
            // explorer
            // 
            explorer.HeaderText = "Explorer";
            explorer.Name = "explorer";
            explorer.ReadOnly = true;
            // 

            // 
            // AddFavorite
            // 
            AddFavorite.BackColor = Color.FromArgb(64, 228, 175);
            AddFavorite.Cursor = Cursors.Hand;
            AddFavorite.Font = new System.Drawing.Font("Segoe UI", 12F);
            AddFavorite.Location = new Point(679, 581);
            AddFavorite.Margin = new Padding(3, 4, 3, 4);
            AddFavorite.Name = "AddFavorite";
            AddFavorite.Size = new Size(120, 57);
            AddFavorite.TabIndex = 1;
            AddFavorite.Text = "Add";
            AddFavorite.UseVisualStyleBackColor = false;
            AddFavorite.Click += btnAddCrypto_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(26, 37);
            searchTextBox.Margin = new Padding(3, 4, 3, 4);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(228, 27);
            searchTextBox.TabIndex = 2;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(262, 36);
            searchButton.Margin = new Padding(3, 4, 3, 4);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(86, 31);
            searchButton.TabIndex = 3;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // btnHome
            // 
            btnHome.BackColor = Color.FromArgb(64, 228, 175);
            btnHome.Cursor = Cursors.Hand;
            btnHome.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnHome.Location = new Point(36, 581);
            btnHome.Margin = new Padding(3, 4, 3, 4);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(120, 57);
            btnHome.TabIndex = 4;
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = false;
            btnHome.Click += btnHome_Click;
            // 
            // AssetGridForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 18, 30);
            ClientSize = new Size(1254, 681);
            Controls.Add(btnHome);
            Controls.Add(searchButton);
            Controls.Add(searchTextBox);
            Controls.Add(AddFavorite);
            Controls.Add(dataGridView1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AssetGridForm";
            Text = "AssetGrid";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn rank;
        private DataGridViewTextBoxColumn symbol;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn supply;
        private DataGridViewTextBoxColumn maxSupply;
        private DataGridViewTextBoxColumn marketCapUsd;
        private DataGridViewTextBoxColumn volumeUsd24Hr;
        private DataGridViewTextBoxColumn priceUsd;
        private DataGridViewTextBoxColumn changePercent24Hr;
        private DataGridViewTextBoxColumn vwap24Hr;
        private DataGridViewTextBoxColumn explorer;
        private Button AddFavorite;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button btnHome;
    }
}