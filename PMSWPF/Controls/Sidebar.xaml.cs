using System.Windows;
using System.Windows.Controls;

namespace PMSWPF.Controls
{
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
        }

        private void SidebarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string? content = button.Content.ToString();

                switch (content)
                {
                    case "Dashboard":
                        // new DashboardWindow().Show();
                        break;
                    case "Projects":
                        // new ProjectsWindow().Show();
                        break;
                    case "Tasks":
                        // new TasksWindow().Show();
                        break;
                    case "Calendar":
                        // new CalendarWindow().Show();
                        break;
                    case "Settings":
                        // new SettingsWindow().Show();
                        break;
                }

                Window.GetWindow(this)?.Close();
            }
        }
    }
}
