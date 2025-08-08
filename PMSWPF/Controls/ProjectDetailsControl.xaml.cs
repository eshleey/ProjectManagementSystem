using Newtonsoft.Json;
using PMSWPF.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace PMSWPF.Controls
{
    public partial class ProjectDetailsControl : UserControl
    {
        public EventHandler<TaskModel> TaskSelected;
        public event EventHandler BackToListRequested;
        private ProjectModel currentProject;
        private ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
        private List<TaskModel> emptyList = new List<TaskModel>();
        private readonly string apiUrl = "http://localhost:5160/api/task";
        private readonly HttpClient httpClient = new HttpClient();

        public ProjectDetailsControl(ProjectModel project)
        {
            InitializeComponent();
            currentProject = project;
            LoadProjectDetailsFromApi();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackToListRequested?.Invoke(this, EventArgs.Empty);
        }

        private void TaskBox_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            if (button != null)
            {
                var task = button.DataContext as TaskModel;
                if (task != null)
                {
                    TaskSelected?.Invoke(this, task);
                }
            }
        }

        private async void LoadProjectDetailsFromApi()
        {
            tasks.Clear();

            var fetchedTasks = await GetTasksAsync();

            foreach(var task in fetchedTasks)
            {
                tasks.Add(task);
            }

            TaskItemsControl.ItemsSource = tasks;
        }

        private async Task<List<TaskModel>> GetTasksAsync()
        {
            try
            {
                var response = await httpClient.GetAsync($"{apiUrl}/byproject/{currentProject.Id}");

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"API request failed. Status code: {response.StatusCode} (Task)");
                    return emptyList;
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var taskList = JsonConvert.DeserializeObject<List<TaskModel>>(jsonString);

                if (taskList == null)
                {
                    MessageBox.Show("The data returned from the server could not be processed. (Task)");
                    return emptyList;
                }

                return taskList;
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"HTTP Error: {httpEx.Message} (Task)");
                return emptyList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error: {ex.Message} (Task)");
                return emptyList;
            }
        }
    }
}