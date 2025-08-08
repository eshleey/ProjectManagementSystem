using Newtonsoft.Json;
using PMSWPF.Models;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace PMSWPF.Controls
{
    public partial class ProjectDetailsControl : UserControl
    {
        private Project currentProject;

        public ProjectDetailsControl(Project project)
        {
            InitializeComponent();
            currentProject = project;
            LoadProjectDetailsFromApi();
        }

        private async void LoadProjectDetailsFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Projeye ait detayları çek
                    var response = await client.GetAsync($"http://localhost:5160/api/projects/{currentProject.Id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var detailedProject = JsonConvert.DeserializeObject<Project>(json);

                        if (detailedProject != null)
                        {
                            currentProject = detailedProject;
                            UpdateUI();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Proje verisi çekilemedi. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Proje detayları yüklenirken hata oluştu: " + ex.Message);
                }
            }
        }

        private void UpdateUI()
        {
            ProjectNameTextBlock.Text = currentProject.Name;
            ProjectDescriptionTextBlock.Text = currentProject.Description;

            // Görevler varsa ItemsControl'e bağla
            if (currentProject.Tasks != null)
            {
                TaskItemsControl.ItemsSource = currentProject.Tasks;
            }
        }


        private void TaskBox_Click(object sender, RoutedEventArgs e)
        {
            // Göreve tıklandığında, tıklanan görevin detaylarını gösteren sayfa açılacak.
        }
    }
}