using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace textura
{
    public partial class Form2 : Form
    {
        string connectionString = "server=localhost;port=3306;database=sisima;user=root;password=;";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //string connectionString = "tu_cadena_de_conexión"; // Reemplaza con tu cadena de conexión a MySQL

            string query = "SELECT * FROM data"; // Reemplaza con tu consulta SQL y el nombre de tu tabla

            // Crear la conexión a la base de datos
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Crear el adaptador de datos y el DataSet
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();

                // Llenar el DataSet con los resultados de la consulta
                adapter.Fill(dataSet);

                // Vincular los datos al control DataGridView
                dataGridView1.DataSource = dataSet.Tables[0];
            }
        }

    }
}