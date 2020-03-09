using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Лабораторная_работа___2
{
    public partial class Form2 : Form
    {
        // ответ
        public static string answer { get; set; }
        // Массив месяцев для перечисления
        public static string[] month = new string[12]
        {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"
        };

        public Form2()
        {
            InitializeComponent();
        }

        // Дифференцированные выплаты
        public string Diff(decimal sumOfCredit, decimal repayment, decimal percent, DateTime date, string shortcurr)
        {
            // расчитывается выплата по формуле
            answer = "";
            decimal Sostat = 1;
            bool firtsMonth = true;
            // номер текущего месяца
            int numOfMonth = date.Month;
            int year = date.Year;
            // Округление для лучшего восприятия
            decimal Sosn = Math.Round((decimal)(sumOfCredit / repayment), 2);
            while (Sostat > 0)
            {
                //В первом месяце 𝑆ост = Сумме кредита
                if (firtsMonth)
                {
                    Sostat = sumOfCredit;
                    firtsMonth = false;
                }
                // Округление для лучшего восприятия
                decimal Sproc = Math.Round((decimal)Sostat * (percent / 12), 2);
                // Выплата в текущем месяце
                decimal x = Sosn + Sproc;
                // Остаток на текущий меясяц
                Sostat = Sostat - Sosn;
                // график выплат
                answer += month[numOfMonth - 1] + " " + year + ": ваша выплата составляет " + x + " " + shortcurr + "\r\n";
                // смена месяцев и года
                if (numOfMonth == 12)
                {
                    year++;
                    numOfMonth = 0;
                }
                numOfMonth++;
            }
            return answer;
        }

        // Аннуитетная выплата
        public string Ann(double sumOfCredit, double repayment, double percent, string shortcurr)
        {
            // В данном методе используется тип double так как для decimal нет метода возведения в степень
            // расчитывается выплата по формуле
            answer = "";
            // метод Math.Pow используется для возведения в степень
            double K = percent / 12 * Math.Pow(1 + (percent / 12), repayment) / (Math.Pow(1 + (percent / 12), repayment) - 1);
            // Округление для лучшего восприятия
            double A = Math.Round(K * sumOfCredit, 2);
            answer = "Ежемесячная аннуитетная выплата составляет " + A + " " + shortcurr;
            return answer;
        }

        // Метод определение валюты и процентной ставки
        public void ChoiceCurrent(ref decimal percent, ref string shortcurr)
        {
            if (comboBox2.Text == "Рубли")
            {
                //12%
                percent = 0.12M;
                shortcurr = "руб.";
            }
            else if (comboBox2.Text == "Доллары")
            {
                //5%
                percent = 0.5M;
                shortcurr = "долл.";
            }
            else
            {
                //4%
                percent = 0.4M;
                shortcurr = "евро";
            }
        }

        public void FileWriter()
        {
            // Файл создается в папке с .sln файлом
            // создание пути для файла 
            string tempPath = Assembly.GetExecutingAssembly().Location;
            string path = tempPath.Substring(0, tempPath.LastIndexOf("Лабораторная работа № 2\\")) + "График выплат.txt";
            // проверка создал файл или нет
            if (!File.Exists(path))
            {
                // создание файла
                var myFile = File.Create(path);
                myFile.Close();
            }
            // запись графика в файл
            StreamWriter writer = new StreamWriter(path);
            writer.Write(answer);
            writer.Close();
        }

        public bool Exeprions(decimal sumOfCredit, decimal repayment)
        {
            // Проверка ошибок ввода
            // Неправильный ввод суммы кредита
            if (sumOfCredit <= 0)
            {
                MessageBox.Show("Сумма кредита не может быть отрицательной или равна 0", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox1.Focus();
                return false;
            }
            // Неправильный ввод срока погашения
            else if (repayment <= 0)
            {
                MessageBox.Show("Срок погашения не может быть отрицательным или равен 0", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                textBox2.Focus();
                return false;
            }
            // Неправильный ввод срока погашения
            else if (comboBox3.Text != "Месяц" && comboBox3.Text != "Год")
            {
                MessageBox.Show("Срок погашения может быть только в месяцах или годах.\r\nВыберите из списка.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox3.Text = "";
                comboBox3.Focus();
                return false;
            }
            // Неправильный ввод типа выплат
            else if (comboBox1.Text != "Аннуитетный" && comboBox1.Text != "Дифференцированный")
            {
                MessageBox.Show("Тип выплат может быть только аннуитетный и дифференцированный.\r\nВыберите из списка.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Text = "";
                comboBox1.Focus();
                return false;
            }
            // Неправильный ввод валюты займа
            else if (comboBox2.Text != "Рубли" && comboBox2.Text != "Доллары" && comboBox2.Text != "Евро")
            {
                MessageBox.Show("Валюта займа может быть только рубли, доллары и евро.\r\nВыберите из списка.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox2.Text = "";
                comboBox2.Focus();
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // В программе для рассчета используется тип decimal
            // ИСпользуется decimal, так как он продставляет число с плавающей точкой и оно является десятичным
            // Это делает результаты более точными
            bool notExeption = true;
            answer = "";
            // сокрашенное название валюты
            string shortcurr = "";
            // процентная ставка
            decimal percent = 0;
            // сумма кредита
            decimal sumOfCredit = 0;
            // срок погашения
            decimal repayment = 0;
            // Переменная даты начала займа
            DateTime dateOfCredit = dateTimePicker1.Value;

            // Проверка суммы кредита на ввод символов
            try
            {
                sumOfCredit = Convert.ToDecimal(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Сумма кредита должна быть числом", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox1.Focus();
                notExeption = false;
            }
            // Проверка суммы кредита на ввод символов
            try
            {
                repayment = Convert.ToDecimal(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Срок погашения должен быть числом", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Text = "";
                textBox2.Focus();
                notExeption = false;
            }

            if (notExeption) 
            {
                // Проверка на правильность ввода
                notExeption = Exeprions(sumOfCredit, repayment);
            }

            // Проверка на наличие ошибок
            if (notExeption) {
                // Определение количества месяцев если срок погашения задан в годах
                if (comboBox3.Text == "Год")
                {
                    repayment = repayment * 12;
                }

                // Определение валюты и процентной ставки
                ChoiceCurrent(ref percent, ref shortcurr);

                // Определение типа расчета выплат
                if (comboBox1.Text == "Дифференцированный")
                {
                    // Дифференцированные выплаты
                    answer = Diff(sumOfCredit, repayment, percent, dateOfCredit, shortcurr);
                }
                else if (comboBox1.Text == "Аннуитетный")
                {
                    // Аннуитетная выплата
                    answer = Ann((double)sumOfCredit, (double)repayment, (double)percent, shortcurr);
                }

                // Запись в файл
                if (radioButton1.Checked)
                {
                    FileWriter();
                }

                this.Visible = false;
            }
        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
