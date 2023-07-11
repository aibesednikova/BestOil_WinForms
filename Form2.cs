using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BestOil
{
    public partial class Form2 : Form
    {
        ObservableCollection<Good> list;
        
        public Form2(ObservableCollection<Good>list, bool isFuel)
        {
            InitializeComponent();
            this.list = list;
            foreach (var l in list)
            {
                comboBox1.Items.Add(l);
            }
            if (!isFuel)
            {
                Add.Enabled = false;
                Delete.Enabled = false;
                Copy.Enabled = false;
                Add.Visible = false;
                Delete.Visible = false;
                Copy.Visible = false;
            }


        }
        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            e.Handled = !Char.IsDigit(ch) && !Char.IsControl(ch) && ch != ',';
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            if (i != -1)
            {
                textBoxName.Text = list[i].Name;
                textBoxPrice.Text = list[i].Price.ToString();
                SelectedItem.Value = comboBox1.SelectedIndex;
            }
        }



        private void Add_Click(object sender, EventArgs e)
        {
            
            //isNew = true;
            Good good = new Good("Новое топливо", 0);
            comboBox1.Items.Add(good);
            list.Add(good);
            comboBox1.SelectedIndex = list.Count - 1;
            textBoxName.Text = "(введите название)";
            textBoxPrice.Text = "(введите цену)";
            IsChanged.Value = true;
            SelectedItem.Value = comboBox1.SelectedIndex;
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали товар для удаления!");
                return;
            }
            DialogResult result = MessageBox.Show("Подтверждаете удаление?", "Удаление товара", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int i = comboBox1.SelectedIndex;
                comboBox1.Items.RemoveAt(i);
                list.RemoveAt(i);
                comboBox1.SelectedIndex = i - 1;
                IsChanged.Value = true;
            }

        }

        private void Copy_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали товар для копирования!");
                return;
            }
            int i = comboBox1.SelectedIndex;
            Good copyGood = new Good(list[i].Name, list[i].Price);
            comboBox1.Items.Add(copyGood);
            list.Add(copyGood);
            comboBox1.SelectedIndex = list.Count - 1;
            IsChanged.Value = true;
            SelectedItem.Value = comboBox1.SelectedIndex;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            if (i != -1)
            {
                list[i].Name = textBoxName.Text;
                IsChanged.Value = true;
            }
        }

        private void textBoxPrice_TextChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            if (i != -1)
            {
                list[i].Price = !(textBoxPrice.Text == "" || textBoxPrice.Text == "(введите цену)" ) ? Convert.ToDouble(textBoxPrice.Text) : 0;
                IsChanged.Value = true;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            //this.list = list;
            comboBox1.Items.Clear();
            foreach (var l in list)
            {
                comboBox1.Items.Add(l);
            }
            comboBox1.SelectedIndex = (SelectedItem.Value < comboBox1.Items.Count) ? SelectedItem.Value : -1;
        }
        public ObservableCollection<Good> Data
        {
            get
            {
                return list;
            }
        }


    }
}
