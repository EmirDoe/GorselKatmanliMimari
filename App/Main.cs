using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADOX;

namespace App
{
    public partial class Main : Form
    {
        private DAL.DAL dal;
        private BL.BusinessLogic bl;
        private Form activeForm;


        public Main()
        {
            InitializeComponent();
            dal = new DAL.DAL();
            bl = new BL.BusinessLogic();

        }

        private void Main_Load(object sender, EventArgs e)
        {
            PopulateDB();
        }

        void PopulateDB()
        {
            try
            {
                string databasePath = "Database.accdb";

                if (!File.Exists(databasePath))
                {
                    Catalog catalog = new Catalog();
                    catalog.Create($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath};Jet OLEDB:Engine Type=5");

                    MessageBox.Show("Database created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string query = bl.MembersTable();
                    dal.ExecuteNonQuery(query);
                    query = bl.DuesTable();
                    dal.ExecuteNonQuery(query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(childForm);
            this.panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }



        private void home_button_Click(object sender, EventArgs e)
        {
            
            string sorgu = bl.AddMember("13180839070", "emirAksakal", DateTime.Now, "Erkek", "B Rh+", "5056921616", "emirdoe@gmail.com", "Bursa", "asdasd");

            try
            {
                int affectedRows = dal.ExecuteNonQuery(sorgu);

                if (affectedRows > 0)
                {
                    MessageBox.Show("Üye eklendi!");
                    
                }
                else
                {
                    MessageBox.Show("Üye eklenirken bir hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void users_button_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Users());
        }

        private void dues_button_Click(object sender, EventArgs e)
        {

        }

        private void graphs_button_Click(object sender, EventArgs e)
        {

        }
    }
}
