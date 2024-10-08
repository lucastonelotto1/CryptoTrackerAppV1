﻿using System.Windows.Forms.DataVisualization.Charting;
using CryptoTrackerApp;
using CryptoTrackerApp.Classes;
using CryptoTrackerApp.DTO;
using NLog;

namespace CryptoTracker.Views
{
    public partial class DetailsForm : Form
    {
        private string symbol;
        private string id;
        private readonly FacadeCT _facadeCT;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public DetailsForm(FacadeCT facadeCT, string symbol, string id)
        {
            LogManager.LoadConfiguration("nlog.config");
            Logger.Info("Details Initialized.");
            _facadeCT = facadeCT;
            this.symbol = symbol;
            this.id = id;
            InitializeComponent();
            LoadCryptoDetails();



        }

        private async void LoadCryptoDetails()
        {
            try
            {
                // Obtener detalles de la criptomoneda
                var cryptoDetails = await _facadeCT.GetCryptoDetailsAsync(symbol);


                string formattedPriceUsd = Math.Round(cryptoDetails.PriceUsd, 2).ToString("F2");
                string formattedChangePercent24Hr = Math.Round(cryptoDetails.ChangePercent24Hr, 2).ToString("F3");
                string formattedVolumeUsd24Hr = Math.Round(Convert.ToDecimal(cryptoDetails.VolumeUsd24Hr), 2).ToString("F2");
                string formattedVwap24Hr = Math.Round(Convert.ToDecimal(cryptoDetails.Vwap24Hr), 2).ToString("F2");
                string formattedMarketCapUsd = Math.Round(Convert.ToDecimal(cryptoDetails.MarketCapUsd), 2).ToString("F2");
                
                
                dataGridViewDetails.DataSource = new List<CryptoDTO> { cryptoDetails };

                // Obtener y mostrar los datos de la evolución del precio
                var historyData = await _facadeCT.GetCryptoHistoryAsync(id);

                if (historyData == null || !historyData.Any())
                {
                    MessageBox.Show("No se encontraron datos de historial para esta criptomoneda.");
                    return;
                }

                // Inicializar la serie del Chart
                var series = new Series
                {
                    Name = "PriceEvolution",
                    Color = System.Drawing.Color.FromArgb(56, 152, 213),
                    ChartType = SeriesChartType.Line,
                    XValueType = ChartValueType.DateTime // Configurar el eje X para usar fechas
                };
                chartPriceEvolution.Series.Clear();
                chartPriceEvolution.Series.Add(series);

                // Variables para calcular el precio mínimo
                decimal minPrice = decimal.MaxValue;

                // Llenar el Chart con los datos de la evolución del precio
                foreach (var price in historyData)
                {
                    DateTime date = price.Date;
                    decimal priceValue = price.PriceUsd;
                    if (date < DateTime.Now.AddMonths(-6)) continue; // Saltar fechas fuera de rango

                    minPrice = Math.Min(minPrice, priceValue); // Encontrar el precio mínimo
                    series.Points.AddXY(date, Math.Round(priceValue, 2));
                }

                // Ajustar el eje Y para que comience en el precio mínimo
                chartPriceEvolution.ChartAreas[0].AxisY.LabelStyle.Format = "C2"; // Formatear el eje Y
                chartPriceEvolution.ChartAreas[0].AxisY.Minimum = (double)minPrice; // Establecer el valor mínimo del eje Y
                chartPriceEvolution.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                chartPriceEvolution.ChartAreas[0].CursorY.IsUserEnabled = true;
                chartPriceEvolution.ChartAreas[0].CursorY.LineColor = System.Drawing.Color.Red;
                chartPriceEvolution.ChartAreas[0].CursorX.IsUserEnabled = true;
                chartPriceEvolution.ChartAreas[0].CursorX.LineColor = System.Drawing.Color.Red;
                chartPriceEvolution.ChartAreas[0].AxisX.LabelStyle.Format = "MM/yyyy";
                chartPriceEvolution.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Months;
                chartPriceEvolution.ChartAreas[0].AxisX.Interval = 1;
                chartPriceEvolution.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartPriceEvolution.Invalidate(); // Refrescar el Chart
            }
            catch (Exception ex)
            {
                Logger.Error("An error occurred while loading crypto details: " + ex.Message);
                return;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            // Aquí puedes definir la acción que se debe realizar cuando se hace clic en el botón Home.
            // Por ejemplo, podrías cerrar el formulario o regresar a una pantalla principal.
            this.Close(); // Como ejemplo, cerrar el formulario.
        }

        private void chartPriceEvolution_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult result = chartPriceEvolution.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint point = result.Series.Points[result.PointIndex];
                DateTime date = DateTime.FromOADate(point.XValue); // Convertir el valor X a una fecha
                string formattedDate = date.ToString("dd/MM/yyyy"); // Formatear la fecha
                string coordinates = $"Date: {formattedDate}        Price: {point.YValues[0]:C2}";
                txtCoordinates.Text = coordinates; // Mostrar las coordenadas en el cuadro de texto
            }
        }
    }
}
