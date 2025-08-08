using Newtonsoft.Json;
using PMSWPF.Controls;
using PMSWPF.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace PMSWPF.Windows
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Project> projects = new ObservableCollection<Project>();
        private readonly string apiUrl = "http://localhost:5160/api/projects";

        public MainWindow()
        {
            InitializeComponent();
            LoadProjectsFromApi();
        }

        private void ProjectBox_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;

            if (button != null)
            {
                var project = button.DataContext as Project;

                if (project != null)
                {
                    var detailsControl = new ProjectDetailsControl(project);
                    MainContent.Content = detailsControl;
                }
            } 
        }

        private void SidebarButton_Click(object sender, RoutedEventArgs e)
        {
            // Sol taraftaki sütundan bir özelliğe tıklandığında, sağ taraftaki sütunda; tıklanan özelliğin sayfası açılacak.
        }

        private async void LoadProjectsFromApi()
        {
            projects.Clear();

            var fetchedProjects = await GetProjectsAsync();

            foreach (var project in fetchedProjects)
            {
                projects.Add(project);
            }

            projects.Add(new Project());

            ProjectItemsControl.ItemsSource = projects;
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(apiUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"API request failed. Status code: {response.StatusCode}");
                        return new List<Project>();
                    }

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var projectList = JsonConvert.DeserializeObject<List<Project>>(jsonString);

                    if (projectList ==  null)
                    {
                        MessageBox.Show("The data returned from the server could not be processed.");
                        return new List<Project>();
                    }

                    return projectList;
                }
                catch (HttpRequestException httpEx)
                {
                    MessageBox.Show($"HTTP Error: {httpEx.Message}");
                    return new List<Project>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected Error: {ex.Message}");
                    return new List<Project>();
                }
            }
        }
    }
}