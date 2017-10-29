using PinnacleUniversity.ViewModels;
using System;

using System.Windows.Forms;

namespace PinnacleUniversity
{
    public partial class frmUniversityStudents : Form, IProgress<string>
    {
        UniversityStudentsViewModel _ViewModel;

        public frmUniversityStudents()
        {

            InitializeComponent();
        }

        private void frmUniversityStudents_Load(object sender, EventArgs e)
        {
            _ViewModel = new UniversityStudentsViewModel(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Report(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(()=> { Report(value); }));
                return;
            }
            switch (value)
            {
                case UniversityStudentsViewModel.PROGRESS_GETTING_DATA_FROM_API:
                    lblProgress.Text = value;
                    break;
                case UniversityStudentsViewModel.PROGRESS_LOADING_FROM_DATABASE:
                    lblProgress.Text = value;
                    break;
                case UniversityStudentsViewModel.PROGRESS_FINISHED:
                    lblProgress.Text = value;
                    LoadDataToView();
                    lblProgress.Visible = false;
                    progressBar.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void LoadDataToView()
        {
            var source = new BindingSource(_ViewModel.Students, null);

            dgvStudentsOverview.DataSource = source;
            dgvStudentsOverview.Columns["Id"].DisplayIndex = 0;
            dgvStudentsOverview.Columns["Name"].DisplayIndex = 1;
            dgvStudentsOverview.Columns["Email"].DisplayIndex = 2;
            dgvStudentsOverview.Columns["GPA"].DisplayIndex = 3;
            dgvStudentsOverview.Columns["EnrolledCredit"].DisplayIndex = 4;
        }

        private void btnAutoDrop_Click(object sender, EventArgs e)
        {
            _ViewModel.BeginDroppingCourses();
        }
    }
}
