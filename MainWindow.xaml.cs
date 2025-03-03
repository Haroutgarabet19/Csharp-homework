using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace WPFDataStorageApp
{
    public partial class MainWindow : Window
    {
        struct Record
        {
            public int Id;
            public string Name;
            public int Age;
            public string Address;
        }

        private Record[] records = new Record[100];
        private int count = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            if (count >= 100)
            {
                MessageBox.Show("Storage is full!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtAge.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Invalid age.");
                return;
            }

            records[count].Id = count + 1;
            records[count].Name = txtName.Text;
            records[count].Age = age;
            records[count].Address = txtAddress.Text;
            count++;

            txtName.Clear();
            txtAge.Clear();
            txtAddress.Clear();
        }

        private void DisplayAll_Click(object sender, RoutedEventArgs e)
        {
            lstRecords.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                lstRecords.Items.Add($"{records[i].Id}. {records[i].Name}, Age: {records[i].Age}, Address: {records[i].Address}");
            }
        }

        private void SortByAge_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(records, 0, count, Comparer<Record>.Create((a, b) => a.Age.CompareTo(b.Age)));
            DisplayAll_Click(sender, e);
        }

        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(records, 0, count, Comparer<Record>.Create((a, b) => a.Name.CompareTo(b.Name)));
            DisplayAll_Click(sender, e);
        }

        private void SearchByAge_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtAge.Text, out int age))
            {
                lstRecords.Items.Clear();
                foreach (var record in records)
                {
                    if (record.Age == age)
                    {
                        lstRecords.Items.Add($"{record.Id}. {record.Name}, Age: {record.Age}, Address: {record.Address}");
                    }
                }
            }
        }

        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.ToLower();
            lstRecords.Items.Clear();
            foreach (var record in records)
            {
                if (record.Name != null && record.Name.ToLower() == name)
                {
                    lstRecords.Items.Add($"{record.Id}. {record.Name}, Age: {record.Age}, Address: {record.Address}");
                }
            }
        }

        private void RemoveByAge_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtAge.Text, out int age))
            {
                for (int i = 0; i < count; i++)
                {
                    if (records[i].Age == age)
                    {
                        for (int j = i; j < count - 1; j++)
                        {
                            records[j] = records[j + 1];
                        }
                        count--;
                        break;
                    }
                }
                DisplayAll_Click(sender, e);
            }
        }

        private void RemoveByName_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.ToLower();
            for (int i = 0; i < count; i++)
            {
                if (records[i].Name != null && records[i].Name.ToLower() == name)
                {
                    for (int j = i; j < count - 1; j++)
                    {
                        records[j] = records[j + 1];
                    }
                    count--;
                    break;
                }
            }
            DisplayAll_Click(sender, e);
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Enter Name" || textBox.Text == "Enter Age" || textBox.Text == "Enter Address")
            {
                textBox.Text = "";
                textBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "txtName") textBox.Text = "Enter Name";
                if (textBox.Name == "txtAge") textBox.Text = "Enter Age";
                if (textBox.Name == "txtAddress") textBox.Text = "Enter Address";
                textBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
    }
}
