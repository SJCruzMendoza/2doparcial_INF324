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
using System.Diagnostics;
namespace textura
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        int pR, pG, pB;
        string connectionString = "server=localhost;port=3306;database=sisima;user=root;password=;";
        public Color customColor;
        public Form1()
        {
            InitializeComponent();
           // bool xamppRunning = IsXAMPPRunning();

          //  if (xamppRunning)
           // {
             //   MessageBox.Show("XAMPP está en ejecución.");
            //}
            //else
            //{
              //  MessageBox.Show("XAMPP no está en ejecución. FAVOR DE INICIALIZAR");
            //}
        }
        public void desabilitar() { 
        
        }

        bool IsXAMPPRunning()
        {
            // Nombre del proceso de XAMPP
            string processName = "xampp-control.exe";

            // Obtener todos los procesos en ejecución
            Process[] processes = Process.GetProcesses();

            // Verificar si el proceso de XAMPP está en ejecución
            foreach (Process process in processes)
            {
                if (process.ProcessName.Equals(processName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        public void TruncarTabla(string nombreTabla)
        {
            //string connectionString = "Server=nombreServidor;Database=nombreBaseDeDatos;Uid=nombreUsuario;Pwd=contraseña;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "TRUNCATE " + nombreTabla ;

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        //command.Parameters.AddWithValue("@tabla", nombreTabla);
                        command.ExecuteNonQuery();
                    }
                }

              //  MessageBox.Show("Tabla truncada exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al truncar la tabla: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "imagen(*.JPG)|*.jpg|*.png|*.gif";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bmp;
                TruncarTabla("data");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Color c = new Color();
            c = bmp.GetPixel(15, 15);
            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
        }
          private void InsertarValoresBD(string nombre, int valor1, int valor2, int valor3)
        {
            // Cadena de conexión a la base de datos MySQL
            //string connectionString = "Server=nombre_servidor;Database=sisima;Uid=nombre_usuario;Pwd=contraseña;";
            //string connectionString = "server=localhost;port=3306;database=sisima;user=root;password=;";
            // Crear una instancia de MySqlConnection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Crear un comando SQL para insertar los valores en la base de datos            
                string query = "INSERT INTO data (nombre, r, g, b) VALUES (@nombre, @r, @g, @b)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Asignar los valores de los parámetros en el comando
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@r", valor1);
                    command.Parameters.AddWithValue("@g", valor2);
                    command.Parameters.AddWithValue("@b", valor3);

                    // Ejecutar el comando SQL
                    command.ExecuteNonQuery();
                }
            }
        }
    

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
               Color c = new Color();
            //c = bmp.GetPixel(e.X, e.Y);
            //textBox1.Text = c.R.ToString();
            //textBox2.Text = c.G.ToString();
            //textBox3.Text = c.B.ToString();
            //pR = c.R;
            //pG = c.G;
            //pB = c.B;
            pR = 0; pG = 0; pB = 0;
            //mR = 0; mG = 0; mB = 0;
            for (int ki = e.X; ki < e.X + 10; ki++)
            {
                for (int kj = e.Y; kj < e.Y + 10; kj++)
                {
                    c = bmp.GetPixel(ki, kj);
                    pR = pR + c.R;
                    pG = pG + c.G;
                    pB = pB + c.B;
                }
            }
            pR = pR / 100;
            pG = pG / 100;
            pB = pB / 100;
            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void llamar()
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                int rr,gg,bb;
                if (int.TryParse(txtR.Text, out rr) &&
                    int.TryParse(txtG.Text, out gg) &&
                    int.TryParse(txtB.Text, out bb))
                {
                    customColor = Color.FromArgb(rr, gg, bb);

                    //bmpR.SetPixel(ki, kj, customColor);
                }
                //Color data = Color.colorConverter.ConvertFromString("Color." + valcolor); 
                int cantidad = 0;
                int mR = 0, mG = 0, mB = 0;
                int cR = 0, cG = 0, cB = 0;
                Color c = new Color();
                Bitmap bmpR = new Bitmap(bmp.Width, bmp.Height);
                for (int i = 0; i < bmp.Width - 10; i = i + 10)
                {
                    for (int j = 0; j < bmp.Height - 10; j = j + 10)
                    {
                        mR = 0; mG = 0; mB = 0;
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                c = bmp.GetPixel(ki, kj);
                                mR = mR + c.R;
                                mG = mG + c.G;
                                mB = mB + c.B;
                            }
                        }
                        mR = mR / 100;
                        mG = mG / 100;
                        mB = mB / 100;
                        if (cantidad < 20)
                            {
                                InsertarValoresBD(txtNombre.Text, mR, mG, mB);
                              cantidad += 1;
                            }

                        

                        c = bmp.GetPixel(i, j);
                        if ((pR - 10 <= mR && mR <= pR + 10) && (pG - 10 <= mG && mG <= pG + 10) && (pB - 10 <= mB && mB <= pB + 10))
                        {
                            for (int ki = i; ki < i + 10; ki++)
                            {
                                for (int kj = j; kj < j + 10; kj++)
                                {
                                    //bmpR.SetPixel(ki, kj, data);
                                    //bmpR.SetPixel(ki, kj, Color.Fuchsia);
                                    //bmpR.SetPixel(ki, kj, customColor);
                                    bmpR.SetPixel(ki, kj, customColor);
                                    // Color.FromArgb(c.R, c.G, c.B));
                                    cR = c.R;
                                    cG = c.G;
                                    cB = c.B;
                                   // if (cantidad < 10)
                                    //{
                                     //   InsertarValoresBD("textura", cR, cG, cB);
                                      //  cantidad += 1;
                                    //}


                                }
                            }
                            //bmpR.SetPixel(i, j, Color.Fuchsia);
                        }
                        else
                        {
                            for (int ki = i; ki < i + 10; ki++)
                            {
                                for (int kj = j; kj < j + 10; kj++)
                                {
                                    c = bmp.GetPixel(ki, kj);
                                    bmpR.SetPixel(ki, kj, Color.FromArgb(c.R, c.G, c.B));
                                    //cR = c.R;
                                    //cG = c.G;
                                    //cB = c.B;
                                    //MessageBox.Show(cR.ToString(),cG.ToString());
                                    // MessageBox.Show(Color.FromArgb(c.R,c.G,c.B).ToString());
                                    // InsertarValoresBD("textura",cR,cG,cB);
                                    //if (cantidad < 150)
                                    //{
                                      //  InsertarValoresBD(txtNombre.Text, cR, cG, cB);
                                        //cantidad += 1;
                                    //}
                                }
                            }
                            //bmpR.SetPixel(i, j, Color.Fuchsia);
                            //bmpR.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));

                        }


                    }

                }
                pictureBox2.Image = bmpR;
            }
            else {
                MessageBox.Show("Por favor ingrese un nombre para el color");
            }
        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            
            int mR = 0, mG = 0, mB = 0;
            Color c = new Color();
            Bitmap bmpR = new Bitmap(bmp.Width, bmp.Height);
            for (int i = 0; i < bmp.Width - 10; i = i + 10)
            {
                for (int j = 0; j < bmp.Height - 10; j = j + 10)
                {
                    mR = 0; mG = 0; mB = 0;
                    for (int ki = i; ki < i + 10; ki++)
                    {
                        for (int kj = j; kj < j + 10; kj++)
                        {
                            c = bmp.GetPixel(ki, kj);
                            mR = mR + c.R;
                            mG = mG + c.G;
                            mB = mB + c.B;
                        }
                    }
                    mR = mR / 100;
                    mG = mG / 100;
                    mB = mB / 100;



                    c = bmp.GetPixel(i, j);
                    if ((pR - 10 <= mR && mR <= pR + 10) && (pG - 10 <= mG && mG <= pG + 10) && (pB - 10 <= mB && mB <= pB + 10))
                    {
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                bmpR.SetPixel(ki, kj, Color.Fuchsia);
                            }
                        }
                        //bmpR.SetPixel(i, j, Color.Fuchsia);
                    }
                    else
                    {
                        for (int ki = i; ki < i + 10; ki++)
                        {
                            for (int kj = j; kj < j + 10; kj++)
                            {
                                c = bmp.GetPixel(ki, kj);
                                bmpR.SetPixel(ki, kj, Color.FromArgb(c.R, c.G, c.B));

                            }
                        }
                        //bmpR.SetPixel(i, j, Color.Fuchsia);
                        //bmpR.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));

                    }


                }

            }
            pictureBox2.Image = bmpR;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            llamar();
            txtNombre.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            llamar();
            txtNombre.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            llamar();
            txtNombre.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();

            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button8.BackColor = colorDialog1.Color;
                int r = colorDialog1.Color.R; // Componente Rojo
                int g = colorDialog1.Color.G; // Componente Verde
                int b = colorDialog1.Color.B; // Componente Azul

                txtR.Text = r.ToString();
                txtG.Text = g.ToString();
                txtB.Text = b.ToString();

             //   string mensaje = "Componentes RGB seleccionados:\n" +
             //            "R: " + r.ToString() + "\n" +
            //             "G: " + g.ToString() + "\n" +
            //             "B: " + b.ToString();

      //  MessageBox.Show(mensaje, "Valores RGB", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
               
            }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = saveFileDialog1.FileName;
                GuardarImagen(rutaArchivo);

                // Continúa utilizando la imagen guardada...
            }
        }

        private void GuardarImagen(string ruta)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap imagen = new Bitmap(pictureBox2.Image);
                imagen.Save(ruta);
                imagen.Dispose();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button9.BackColor = colorDialog1.Color;
                int r = colorDialog1.Color.R; // Componente Rojo
                int g = colorDialog1.Color.G; // Componente Verde
                int b = colorDialog1.Color.B; // Componente Azul

                txtR.Text = r.ToString();
                txtG.Text = g.ToString();
                txtB.Text = b.ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button10.BackColor = colorDialog1.Color;
                int r = colorDialog1.Color.R; // Componente Rojo
                int g = colorDialog1.Color.G; // Componente Verde
                int b = colorDialog1.Color.B; // Componente Azul

                txtR.Text = r.ToString();
                txtG.Text = g.ToString();
                txtB.Text = b.ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

       

        }
        }


