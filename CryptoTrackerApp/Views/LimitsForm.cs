using System;
using System.Windows.Forms;

namespace CryptoTrackerApp.Views
{
    public partial class LimitsForm : Form
    {
        private string position;

        public LimitsForm(string position)
        {
            InitializeComponent();
            this.position = position;
            // Inicializar label y textbox con la posición inicial
            label1.Text = "Current Position: " + position;
            textBox1.Text = position;

            // Agregar el evento click para el botón
            button1.Click += new EventHandler(button1_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Actualizar la posición con el valor del textbox
            position = textBox1.Text;
            // Actualizar el label con la nueva posición
            label1.Text = "Current Position: " + position;
        }
    }
}
