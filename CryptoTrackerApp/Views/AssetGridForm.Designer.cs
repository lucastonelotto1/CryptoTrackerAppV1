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
        /// </summary>
        private void InitializeComponent()
        {
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
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
           //SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { rank, symbol, name, supply, maxSupply, marketCapUsd, volumeUsd24Hr, priceUsd, changePercent24Hr, vwap24Hr, explorer });
            dataGridView1.Location = new Point(0, -2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(743, 397);
            dataGridView1.TabIndex = 0;
           // dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // rank
            // 
            rank.HeaderText = "Rank";
            rank.Name = "rank";
            // 
            // symbol
            // 
            symbol.HeaderText = "Symbol";
            symbol.Name = "symbol";
            // 
            // name
            // 
            name.HeaderText = "Name";
            name.Name = "name";
            // 
            // supply
            // 
            supply.HeaderText = "Supply";
            supply.Name = "supply";
            // 
            // maxSupply
            // 
            maxSupply.HeaderText = "maxSupply";
            maxSupply.Name = "maxSupply";
            // 
            // marketCapUsd
            // 
            marketCapUsd.HeaderText = "Market Cap USD";
            marketCapUsd.Name = "marketCapUsd";
            // 
            // volumeUsd24Hr
            // 
            volumeUsd24Hr.HeaderText = "Volume USD 24Hr";
            volumeUsd24Hr.Name = "volumeUsd24Hr";
            // 
            // priceUsd
            // 
            priceUsd.HeaderText = "Price USD";
            priceUsd.Name = "priceUsd";
            // 
            // changePercent24Hr
            // 
            changePercent24Hr.HeaderText = "Change Percent 24Hr";
            changePercent24Hr.Name = "changePercent24Hr";
            // 
            // vwap24Hr
            // 
            vwap24Hr.HeaderText = "Volume Weighted Average Price";
            vwap24Hr.Name = "vwap24Hr";
            // 
            // explorer
            // 
            explorer.HeaderText = "Explorer";
            explorer.Name = "explorer";
            // 
            // AddFavorite
            // 
            AddFavorite.Location = new Point(330, 415);
            AddFavorite.Name = "AddFavorite";
            AddFavorite.Size = new Size(75, 23);
            AddFavorite.TabIndex = 1;
            AddFavorite.Text = "Add ";
            AddFavorite.UseVisualStyleBackColor = true;
            //AddFavorite.Click += button1_Click;
            // 
            // AssetGrid
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(743, 466);
            Controls.Add(AddFavorite);
            Controls.Add(dataGridView1);
            Name = "AssetGrid";
            Text = "AssetGrid";
          //  Load += AssetGrid_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
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
    }
}