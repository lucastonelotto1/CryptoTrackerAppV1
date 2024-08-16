using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CryptoTracker.Views
{
    partial class DetailsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewDetails;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPriceEvolution;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.TextBox txtCoordinates;
        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewDetails = new System.Windows.Forms.DataGridView();
            this.chartPriceEvolution = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnHome = new System.Windows.Forms.Button();
            this.txtCoordinates = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPriceEvolution)).BeginInit();
            this.SuspendLayout();

            // 
            // dataGridViewDetails
            // 
            this.dataGridViewDetails.AllowUserToAddRows = false;
            this.dataGridViewDetails.AllowUserToResizeRows = false;
            this.dataGridViewDetails.BorderStyle = 0; // Sin borde
            this.dataGridViewDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDetails.BackgroundColor = Color.FromArgb(0, 18, 30);
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(1, 29, 43),
                Font = new Font("Sans Serif Collection", 8F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(1, 29, 43),
                SelectionForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            this.dataGridViewDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(1, 26, 43),
                Font = new Font("Sans Serif Collection", 6F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(56, 152, 213),
                SelectionBackColor = Color.FromArgb(1, 26, 43),
                SelectionForeColor = SystemColors.HighlightText,
                WrapMode = DataGridViewTriState.False
            };
            this.dataGridViewDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewDetails.GridColor = Color.FromArgb(1, 26, 43);
            this.dataGridViewDetails.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewDetails.Margin = new Padding(3, 7, 3, 3);
            this.dataGridViewDetails.MultiSelect = false;
            this.dataGridViewDetails.Name = "dataGridViewDetails";
            this.dataGridViewDetails.ReadOnly = true;
            this.dataGridViewDetails.RowHeadersVisible = false;
            this.dataGridViewDetails.RowTemplate.Height = 35;
            this.dataGridViewDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDetails.Size = new System.Drawing.Size(960, 100);
            this.dataGridViewDetails.TabIndex = 0;

            // 
            // chartPriceEvolution
            // 
            ChartArea chartArea1 = new ChartArea
            {
                BackColor = Color.FromArgb(0, 18, 30), // Fondo del área del gráfico
                BorderColor = Color.FromArgb(0, 18, 30), // Color del borde del área del gráfico
                BorderWidth = 0, // Sin borde
                BorderDashStyle = ChartDashStyle.Solid,

                // Eliminar las líneas de fondo
                AxisX = new Axis
                {
                    MajorGrid = new Grid { LineColor = Color.FromArgb(0, 18, 30) },
                    LineColor = Color.White,
                    LabelStyle = new LabelStyle { ForeColor = Color.White },
                    Interval = 1 // Mostrar menos etiquetas en el eje X
                },
                AxisY = new Axis
                {
                    MajorGrid = new Grid { LineColor = Color.FromArgb(0, 18, 30) },
                    LineColor = Color.White,
                    LabelStyle = new LabelStyle { ForeColor = Color.White },
                    Minimum = 0, // Ajustar el valor mínimo si es necesario
                }
            };
            this.chartPriceEvolution.ChartAreas.Add(chartArea1);

            Series series = new Series
            {
                ChartType = SeriesChartType.Line,
                Name = "PriceSeries",
                Color = Color.White, // Color de la línea
                BorderDashStyle = ChartDashStyle.Solid, // Estilo de línea
                BorderWidth = 2 // Ancho de la línea
            };

            // Habilitar los ToolTips para mostrar los valores
            this.chartPriceEvolution.GetToolTipText += new EventHandler<ToolTipEventArgs>(chartPriceEvolution_GetToolTipText);

            this.chartPriceEvolution.Location = new System.Drawing.Point(12, 130);
            this.chartPriceEvolution.Name = "chartPriceEvolution";
            this.chartPriceEvolution.Size = new System.Drawing.Size(960, 500);
            this.chartPriceEvolution.TabIndex = 1;
            this.chartPriceEvolution.Text = "chartPriceEvolution";
            this.chartPriceEvolution.BackColor = Color.FromArgb(0, 18, 30); // Fondo del gráfico
            this.chartPriceEvolution.BorderlineColor = Color.FromArgb(0, 18, 30); // Color del borde del gráfico
            this.chartPriceEvolution.BorderlineWidth = 0; // Sin borde
            this.chartPriceEvolution.BorderlineDashStyle = ChartDashStyle.Solid; // Estilo de borde

            // Añadir el evento de clic al gráfico
            this.chartPriceEvolution.MouseClick += new MouseEventHandler(chartPriceEvolution_MouseClick);

            // 
            // btnHome
            // 
            btnHome.BackColor = Color.FromArgb(64, 228, 175);
            btnHome.Cursor = Cursors.Hand;
            btnHome.Location = new Point(12, 640);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(100, 40);
            btnHome.TabIndex = 2;
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = false;
            btnHome.Click += btnHome_Click;
            // 
            // txtCoordinates
            // 
            txtCoordinates.BackColor = Color.FromArgb(0, 26, 43);
            txtCoordinates.BorderStyle = BorderStyle.None;
            txtCoordinates.Cursor = Cursors.IBeam;
            txtCoordinates.Font = new Font("Segoe UI", 14F);
            txtCoordinates.ForeColor = SystemColors.Window;
            txtCoordinates.Location = new Point(128, 641);
            txtCoordinates.Name = "txtCoordinates";
            txtCoordinates.ReadOnly = true;
            txtCoordinates.Size = new Size(340, 32);
            txtCoordinates.TabIndex = 3;

            // 
            // DetailsForm
            // 
            this.BackColor = Color.FromArgb(0, 18, 30);
            this.ClientSize = new System.Drawing.Size(1000, 700); // Aumentar el tamaño del formulario para incluir el botón y el cuadro de texto
            this.Controls.Add(this.txtCoordinates);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.chartPriceEvolution);
            this.Controls.Add(this.dataGridViewDetails);
            this.Name = "DetailsForm";
            this.Text = "Crypto Details";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPriceEvolution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private void chartPriceEvolution_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                var point = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                e.Text = $"{point.YValues[0]:C2}";
            }
        }


    }
}
