using PinnacleUniversity.ViewModels;
using System;

using System.Windows.Forms;

namespace PinnacleUniversity
{
    public partial class frmUniversityStudents : Form, IProgress<string>
    {
        #region Constructor and ViewModel

        private UniversityStudentsViewModel _ViewModel;

        public frmUniversityStudents()
        {
            InitializeComponent();
        }

        #endregion Constructor and ViewModel

        #region IProgress Implementation

        /// <summary>
        /// Report Progress
        /// </summary>
        /// <param name="value"></param>
        public void Report(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { Report(value); }));
                return;
            }
            switch (value)
            {
                case UniversityStudentsViewModel.PROGRESS_GETTING_DATA_FROM_API:
                    lblProgress.Text = value;
                    btnAutoDrop.Enabled = false;
                    break;

                case UniversityStudentsViewModel.PROGRESS_LOADING_FROM_DATABASE:
                    lblProgress.Text = value;
                    btnAutoDrop.Enabled = false;
                    break;

                case UniversityStudentsViewModel.PROGRESS_FINISHED:
                    LoadDataToView();
                    lblProgress.Text = "Success";
                    btnAutoDrop.Enabled = true;
                    progressBar.Style = ProgressBarStyle.Blocks;
                    break;

                case UniversityStudentsViewModel.PROGRESS_DROPPING_STUDENTS:
                    lblProgress.Text = value;
                    btnAutoDrop.Enabled = false;
                    progressBar.Style = ProgressBarStyle.Marquee;
                    break;

                default:
                    lblProgress.Text = value;
                    break;
            }
        }

        #endregion IProgress Implementation

        #region Private Methods

        /// <summary>
        /// Will bind the list from the ViewModel to the ViewModel List.
        /// </summary>
        private void LoadDataToView()
        {
            var source = new BindingSource(_ViewModel.Students, null);

            dgvStudentsOverview.DataSource = source;
            dgvStudentsOverview.Columns["Id"].DisplayIndex = 0;
            dgvStudentsOverview.Columns["Name"].DisplayIndex = 1;
            dgvStudentsOverview.Columns["Email"].DisplayIndex = 2;
            dgvStudentsOverview.Columns["GPA"].DisplayIndex = 3;
            dgvStudentsOverview.Columns["EnrolledCredit"].DisplayIndex = 4;

            dgvStudentsOverview.Refresh();
        }

        #endregion Private Methods

        #region Events

        /// <summary>
        /// Constructor start up the View Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUniversityStudents_Load(object sender, EventArgs e)
        {
            _ViewModel = new UniversityStudentsViewModel(this);
        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Initiate the AutoDrop for Activity 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoDrop_Click(object sender, EventArgs e)
        {
            _ViewModel.BeginDroppingCourses();
        }

        #endregion Events
    }
}