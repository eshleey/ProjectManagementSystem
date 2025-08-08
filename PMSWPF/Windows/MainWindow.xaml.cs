using PMSWPF.Controls;
using PMSWPF.Models;
using System.Windows;

namespace PMSWPF.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowProjectList();
        }

        private void SidebarButton_Click(object sender, RoutedEventArgs e)
        {
            // Sol taraftaki sütundan bir özelliğe tıklandığında, sağ taraftaki sütunda; tıklanan özelliğin sayfası açılacak.
        }

        private void ShowProjectList()
        {
            var projectListControl = new ProjectListControl();
            projectListControl.ProjectSelected += ProjectListControl_ProjectSelected;
            MainContentControl.Content = projectListControl;
        }

        private void ProjectListControl_ProjectSelected(object sender, ProjectModel selectedProject)
        {
            var detailsControl = new ProjectDetailsControl(selectedProject);
            detailsControl.BackToListRequested += DetailsControl_BackToListRequested;
            MainContentControl.Content = detailsControl;
        }

        private void DetailsControl_BackToListRequested(object sender, EventArgs e)
        {
            ShowProjectList();
        }
    }
}