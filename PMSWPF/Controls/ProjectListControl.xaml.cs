using Newtonsoft.Json;
using PMSWPF.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace PMSWPF.Controls
{
    public partial class ProjectListControl : UserControl
    {
        public EventHandler<ProjectModel> ProjectSelected;
        private ObservableCollection<ProjectModel> projects = new ObservableCollection<ProjectModel>();
        private List<ProjectModel> emptyList = new List<ProjectModel>();
        private readonly string apiUrl = "http://localhost:5160/api/projects";
        private readonly HttpClient httpClient = new HttpClient();

        public ProjectListControl()
        {
            InitializeComponent();
            LoadProjectsFromApi();
        }

        private void ProjectBox_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            if (button != null)
            {
                var project = button.DataContext as ProjectModel;
                if (project != null)
                {
                    ProjectSelected?.Invoke(this, project);
                }
            }
        }

        private async void LoadProjectsFromApi()
        {
            projects.Clear();

            var fetchedProjects = await GetProjectsAsync();

            foreach(var project in fetchedProjects)
            {
                projects.Add(project);
            }

            ProjectItemsControl.ItemsSource = projects;
        }

        public async Task<List<ProjectModel>> GetProjectsAsync()
        {
            try
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"API request failed. Status code: {response.StatusCode} (Project)");
                    return emptyList;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var projectList = JsonConvert.DeserializeObject<List<ProjectModel>>(jsonString);

                if (projectList == null)
                {
                    MessageBox.Show("The data returned from the server could not be processed. (Project)");
                    return emptyList;
                }

                return projectList;
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"HTTP Error: {httpEx.Message} (Project)");
                return emptyList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error: {ex.Message} (Project)");
                return emptyList;
            }
        }
    }
}
