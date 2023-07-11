using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BestOil
{
    public partial class Form1 : Form
    {
        public bool IsLabel11_TextChange { get; private set; }
        Good fuel;
        Good[] food = new Good[4]
            {
                new Good { Name = "Чай", Price = 80 },
                new Good { Name = "Кофе", Price = 100 },
                new Good { Name = "Лимонад", Price = 60 },
                new Good { Name = "Булочка", Price = 55 }
            };
        ObservableCollection<Good> fuels = new ObservableCollection<Good>
            {
                new Good { Name="Бензин АИ-80 (А-76)", Price = 43.9},
                new Good { Name="Бензин АИ-92", Price = 46.56},
                new Good { Name="Бензин АИ-95", Price = 50.34},
                new Good { Name="Бензин АИ-98", Price = 61.7},
                new Good { Name="Pulsar-100", Price = 62.64},
                new Good { Name="Дизельное топливо", Price = 56.52},
                new Good { Name="Газ", Price = 19.24},
                new Good { Name="Газ (метан)", Price = 19.79},
                new Good { Name="Авиационное топливо", Price = 0}
            };
        double food_sum = 0;
        
        // Дневная выручка
        static double proceeds = 0;
        string s = "Дневная выручка: 0,00.";

        // Суммы в чеке
        double d = 0; //общая
        double dFuel = 0; //только топливо
        
        // Количество товаров мини-кафе
        int[] qt = { 0, 0, 0, 0 };

        //путь к файлу, из которого загружается список товаров по умолчанию
        //если файла нет, используется стандартный набор
        string filePath = "start.xml";

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = fuels;
            comboBox1.DisplayMember = "Name";
            comboBox1.SelectedIndex = 1;

            checkBox1.Text = food[0].Name;
            checkBox2.Text = food[1].Name;
            checkBox3.Text = food[2].Name;
            checkBox4.Text = food[3].Name;
            textBox4.Text = food[0].Price.ToString("n2");
            textBox6.Text = food[1].Price.ToString("n2");
            textBox8.Text = food[2].Price.ToString("n2");
            textBox10.Text = food[3].Price.ToString("n2");

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    ObservableCollection<Good> f2 = new ObservableCollection<Good>();

                    ObservableCollection<Good> list;
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Good>));
                    list = (ObservableCollection<Good>)xmlFormat.Deserialize(fs);

                    for (int i = 1; i <= 4; i++)
                    {
                        food[i - 1] = list[i];
                    }
                    for (int i = 5; i < list.Count; i++)
                    {
                        f2.Add(list[i]);
                    }
                    comboBox1.DataSource = f2;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.SelectedIndex = Convert.ToInt32(list[0].Price);
                }
            }
            catch
            {
                
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            e.Handled = !Char.IsDigit(ch) && !Char.IsControl(ch);
        }

        private void ShowPrice_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                fuel = (Good)comboBox1.SelectedItem;
                textBox1.Text = fuel.Price.ToString("n2");
                if (radioButton1.Checked == true)
                {
                    textBox2_TextChanged(this, e);
                }
                else
                {
                    textBox3_TextChanged(this, e);
                }
                return;
            }
            else
            {
                textBox1.Text = "0";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            textBox2.Text = "";
            textBox3.ReadOnly = false;
            groupBox5.Text = "К выдаче: ";
            label5.Text = "0,000";
            label6.Text = "л";
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = true;
            textBox3.Text = "";
            groupBox5.Text = "К оплате: ";
            label5.Text = "0,00";
            label6.Text = "руб.";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                    label5.Text = (Convert.ToDouble(textBox1.Text) * Convert.ToDouble(textBox2.Text)).ToString("n2");
            }
            else
            {
                label5.Text = "0,00";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox3.Text != "" && Convert.ToDouble(textBox1.Text) != 0)
            {
                label5.Text = Math.Round((decimal)(Convert.ToDouble(textBox3.Text) / Convert.ToDouble(textBox1.Text)), 3).ToString("n3");
            }
            else
            {
                label5.Text = "0,00";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox5.ReadOnly = false;
                textBox5.Text = "1"; 
                textBox5.Focus();
            }
            else
            {
                textBox5.ReadOnly = true;
                textBox5.Text = "";
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox7.ReadOnly = false;
                textBox7.Text = "1"; 
                textBox7.Focus();
            }
            else
            {
                textBox7.ReadOnly = true;
                textBox7.Text = "";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox9.ReadOnly = false;
                textBox9.Text = "1";
                textBox9.Focus();
            }
            else
            {
                textBox9.ReadOnly = true;
                textBox9.Text = "";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox11.ReadOnly = false;
                textBox11.Text = "1";
                textBox11.Focus();
            }
            else
            {
                textBox11.ReadOnly = true;
                textBox11.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            qt[0] = Convert.ToInt32(textBox5.Text!=""? textBox5.Text:"0");
            qt[1] = Convert.ToInt32(textBox7.Text != "" ? textBox7.Text : "0");
            qt[2] = Convert.ToInt32(textBox9.Text != "" ? textBox9.Text : "0");
            qt[3] = Convert.ToInt32(textBox11.Text != "" ? textBox11.Text : "0");
            food_sum = 0;
            for(int i=0;i<4;i++)
            {
                food_sum += qt[i]* food[i].Price;
            }
            label10.Text = food_sum.ToString("n2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked == true) 
            {
                dFuel = Convert.ToDouble(label5.Text);
            }
            else
            {
                dFuel = Convert.ToDouble(textBox3.Text);
            }
            d = dFuel + Convert.ToDouble(label10.Text);
            label11.Text = d.ToString("n2");
            IsLabel11_TextChange = true;
            proceeds += d;
            s = "Дневная выручка: " + string.Format("{0:f}", proceeds) + ".";
            if (timer1.Enabled == false)
            {
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            DialogResult result = MessageBox.Show($"Сумма к оплате: {label11.Text}. \nОчистить форму? ", "Ожидание следующего покупателя", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                timer1.Start();
            }
            else
            {
                radioButton1.Checked = true;
                textBox2.Text = "";
                foreach (var ch in groupBox2.Controls.OfType<CheckBox>())
                {
                    ch.Checked = false;
                }
                foreach (var txtbx in groupBox7.Controls.OfType<TextBox>())
                {
                    txtbx.ReadOnly = true;
                    txtbx.Text = "";
                }
                d = 0;
                label11.Text = d.ToString("n2");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChanged.Value == true)
            {
                DialogResult isChangedResult = MessageBox.Show("Список товаров был изменён. \nСохранить изменения?", "ПРЕДУПРЕЖДЕНИЕ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (isChangedResult == DialogResult.Yes)
                {
                    Save_MenuItem_Click(this, e);
                }
            }
            //s = "Дневная выручка: " + string.Format("{0:f}", proceeds) + ".";
            DialogResult result = MessageBox.Show(s, "Завершение работы", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void Load_MenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.FilterIndex = 1;
            if (open.ShowDialog() == DialogResult.OK)
            {
                ObservableCollection<Good> f2 = new ObservableCollection<Good>();

                ObservableCollection<Good> list;
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Good>));
                using (FileStream fs = File.OpenRead(open.FileName))
                {
                    list = (ObservableCollection<Good>)xmlFormat.Deserialize(fs);
                }
                //string s = list[0].Price.ToString();
                //DialogResult result = MessageBox.Show(s, "проверка", MessageBoxButtons.OK, MessageBoxIcon.Question);
                
                for (int i = 1; i <= 4; i++)
                {
                    food[i-1] = list[i];
                }
                for (int i = 5; i < list.Count; i++)
                {
                    f2.Add(list[i]);

                }
                
                comboBox1.DataSource = f2;
                comboBox1.DisplayMember = "Name";
                comboBox1.SelectedIndex = Convert.ToInt32(list[0].Price);
            }
        }

        private void Save_MenuItem_Click(object sender, EventArgs e)
        {
            //double ind = 0 + comboBox1.SelectedIndex;
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text Files(*.xml)|*.xml";
            if (save.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(ObservableCollection<Good>));
                ObservableCollection<Good> list = new ObservableCollection<Good>{
                new Good { Name="", Price = Convert.ToDouble(comboBox1.SelectedIndex)} };
                foreach(var item in food)
                {
                    list.Add(item);
                }
                foreach (var item in comboBox1.Items)
                {
                    list.Add(item as Good);
                }
                using (FileStream fs = File.Create(save.FileName))
                {
                    xmlFormat.Serialize(fs, list);
                }
            }
        }

        private void Edit_MenuItem_Click(object sender, EventArgs e)
        {
            ObservableCollection<Good> list = new ObservableCollection<Good> { };
            foreach (var item in comboBox1.Items)
            {
                list.Add(item as Good);
            }
            Form2 openForm2 = new Form2(list, true);
            openForm2.ShowDialog();
            fuels = openForm2.Data;
            comboBox1.DataSource = fuels;
            comboBox1.SelectedIndex = SelectedItem.Value;
        }

        private void EditCafe_MenuItem_Click(object sender, EventArgs e)
        {
            ObservableCollection<Good> list = new ObservableCollection<Good> { };
            foreach (var item in food)
            {
                list.Add(item);
            }
            Form2 openForm2 = new Form2(list, false);
            openForm2.ShowDialog();
            //list = openForm2.Data;
            for(int i = 0; i < 4; i++)
            {
                food[i] = list[i];
            }
            checkBox1.Text = food[0].Name;
            checkBox2.Text = food[1].Name;
            checkBox3.Text = food[2].Name;
            checkBox4.Text = food[3].Name;
            textBox4.Text = food[0].Price.ToString("n2");
            textBox6.Text = food[1].Price.ToString("n2");
            textBox8.Text = food[2].Price.ToString("n2");
            textBox10.Text = food[3].Price.ToString("n2");
        }

        private void Quit_MenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DailyProceeds_MenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(s, "Дневная выручка", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void PrintCheck_MenuItem_Click(object sender, EventArgs e)
        {
            string check = "";
            if (dFuel != 0)
            {
                check += comboBox1.SelectedItem.ToString() + ": " + textBox1.Text + " руб./л x ";
                if (textBox3.Text == "")
                {
                    check += textBox2.Text;
                }
                else
                {
                    check += label5.Text;
                }
                check += " л = " + dFuel + " руб.\n";
            }
            for (int i = 0; i < 4; i++)
            {
                if (qt[i] != 0)
                {
                    check += food[i].Name + ": " + food[i].Price + " руб./шт. x " + qt[i] + " шт. = " + food[i].Price * qt[i] + " руб.\n";
                }
            }
            check += "ИТОГ: " + string.Format("{0:f}", d) + " руб.";
            MessageBox.Show(check, "Чек по операции", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Какие товары будем редактировать?", "Выбор группы товаров", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
