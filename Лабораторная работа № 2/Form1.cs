using System;
using System.Windows.Forms;

namespace Лабораторная_работа___2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // открытие формы для заполнения данных о кредите
            Form2 form2 = new Form2();
            form2.ShowDialog();
            // вывод графика
            textBox1.Text = Form2.answer;
        }

        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
