using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Zero
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User
            {
                Username = usernameTextBox.Text,
                Highscore = 0
            };

            List<User> users = LoadUsers();

            var existingUser = users.FirstOrDefault(u => u.Username == newUser.Username);
            users.Add(newUser);

            try
            {
                SaveUsers(users);
                MainWindow childWindow = new MainWindow(newUser);
                childWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists("users.json"))
                return new List<User>();

            string json = File.ReadAllText("users.json");
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        private void SaveUsers(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("users.json", json);
        }

        public class User
        {
            public string Username { get; set; }
            public int Highscore { get; set; }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Text == "Ник")
                {
                    textBox.Text = string.Empty;
                }
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    if (textBox == usernameTextBox)
                    {
                        textBox.Text = "Ник";
                    }
                }
            }
        }


    }
}
