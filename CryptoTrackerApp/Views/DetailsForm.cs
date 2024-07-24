using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;
using CryptoTrackerApp.Classes;

namespace CryptoTracker.Views
{
    public partial class DetailsForm : Form
    {
        private string cryptoId;
        private static readonly CoinCapApiClient client = new CoinCapApiClient();

        public DetailsForm(string cryptoId)
        {
            InitializeComponent();
            this.cryptoId = cryptoId;
            LoadCryptoDetails();
        }

        private async void LoadCryptoDetails()
        {
            try
            {
                // Obtener detalles de la criptomoneda
                var cryptoDetails = await client.GetCryptoAssetByIdAsync(cryptoId);

                // Asignar los valores obtenidos al DataGridView
                dataGridViewDetails.DataSource = new List<CryptoAsset> { cryptoDetails };

                // Obtener y mostrar los datos de la evolución del precio
                var historyData = await client.GetCryptoAssetHistoryAsync(cryptoId);

                // Inicializar la serie del Chart
                var series = new Series
                {
                    Name = "PriceEvolution",
                    Color = System.Drawing.Color.Blue,
                    ChartType = SeriesChartType.Line
                };
                chartPriceEvolution.Series.Clear();
                chartPriceEvolution.Series.Add(series);

                // Llenar el Chart con los datos de la evolución del precio
                foreach (var price in historyData)
                {
                    DateTime date = DateTimeOffset.FromUnixTimeMilliseconds(price.Time).DateTime;
                    double priceValue = double.Parse(price.PriceUsd);

                    series.Points.AddXY(date, priceValue);
                }

                chartPriceEvolution.ChartAreas[0].AxisX.LabelStyle.Format = "MM/dd/yyyy";
                chartPriceEvolution.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                chartPriceEvolution.ChartAreas[0].AxisX.Interval = 1;
                chartPriceEvolution.Invalidate(); // Refrescar el Chart
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading crypto details: {ex.Message}");
            }
        }

        private void DetailsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
