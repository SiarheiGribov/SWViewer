using System;
using System.Collections.Generic;
using System.Windows;

namespace swViewer
{
    public partial class WindowWhitelist : Window
    {
        public event EventHandler userAddEvent;
        protected void OnuserAddEvent() { this.userAddEvent?.Invoke(this, EventArgs.Empty); }
        public event EventHandler userDeleteEvent;
        protected void OnuserDeleteEvent() { this.userDeleteEvent?.Invoke(this, EventArgs.Empty); }
        public event EventHandler projectAddEvent;
        protected void OnprojectAddEvent() { this.projectAddEvent?.Invoke(this, EventArgs.Empty); }
        public event EventHandler projectDeleteEvent;
        protected void OnprojectDeleteEvent() { this.projectDeleteEvent?.Invoke(this, EventArgs.Empty); }
        List<string> wUList = new List<string>();
        List<string> wPList = new List<string>();
        public List<string> WUList { get => wUList; set => wUList = value; }
        public List<string> WPList { get => wPList; set => wPList = value; }

        public WindowWhitelist()
        {
            InitializeComponent();
            Users_List.SelectionMode = System.Windows.Controls.SelectionMode.Single;
            Projects_List.SelectionMode = System.Windows.Controls.SelectionMode.Single;
            this.Closing += WindowWhitelist_Closing1;

            ex_users.Content = "Example:" + Environment.NewLine + "Jimbo" + Environment.NewLine + "127.0.0.1";
            ex_projects.Content = "Example" + Environment.NewLine + "metawiki" + Environment.NewLine + "ruwikinews";
        }

        private void WindowWhitelist_Closing1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }


        private void Add_User(object sender, RoutedEventArgs e)
        {
            if (usertList.Text.ToString() != null && usertList.Text.ToString() != "")
            {
                this.OnuserAddEvent();
                WUList.Add(usertList.Text.ToString());
                Users_List.Items.Add(usertList.Text.ToString());
            }
            usertList.Text = "";
        }

        private void Delete_User(object sender, RoutedEventArgs e)
        {
            if (Users_List.SelectedItem != null)
            {
                this.OnuserDeleteEvent();
                WUList.Remove(Users_List.SelectedItem.ToString());
                Users_List.Items.Remove(Users_List.SelectedItem);
            }
        }

        private void Add_Project(object sender, RoutedEventArgs e)
        {
            if (projectList.Text.ToString() != null && projectList.Text.ToString() != "")
            {
                this.OnprojectAddEvent();
                WPList.Add(projectList.Text.ToString());
                Projects_List.Items.Add(projectList.Text.ToString());
            }
            projectList.Text = "";
        }

        private void Delete_Project(object sender, RoutedEventArgs e)
        {
            if (Projects_List.SelectedItem != null)
            {
                this.OnprojectDeleteEvent();
                WPList.Remove(Projects_List.SelectedItem.ToString());
                Projects_List.Items.Remove(Projects_List.SelectedItem);
            }
        }


        private void WhiteListcancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
