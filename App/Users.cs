using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class Users : Form
    {
        private DAL.DAL dal;
        private BL.BusinessLogic bl;
        public Users()
        {
            InitializeComponent();
            dal = new DAL.DAL();
            bl = new BL.BusinessLogic();
        }
        private void Users_Load(object sender, EventArgs e)
        {
            city_combo.Items.AddRange(bl.cities);
            bloodtype_combo.Items.AddRange(bl.bloodTypes);
        }

        private void adduser_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tc_text.Text) || string.IsNullOrWhiteSpace(name_text.Text) || string.IsNullOrWhiteSpace(email_text.Text) || 
                string.IsNullOrWhiteSpace(city_combo.Text) || string.IsNullOrWhiteSpace(bloodtype_combo.Text) || string.IsNullOrWhiteSpace(text_addres.Text) ||
                string.IsNullOrWhiteSpace(phone_text.Text) || (!gender_male.Checked && !gender_female.Checked))
            {
                MessageBox.Show("Lütfen gerekli bütün alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string Tc = tc_text.Text;

            if (Tc.Length != 11)
            {
                MessageBox.Show("TC Kimlik numarası 11 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                int tekler = 0;
                int ciftler = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (i % 2 == 0)
                    {
                        tekler += Convert.ToInt32(Tc[i].ToString());
                    }
                    else
                    {
                        ciftler += Convert.ToInt32(Tc[i].ToString());
                    }
                }
                int onuncu = (tekler * 7 - ciftler) % 10;
                int onbirinci = (tekler + ciftler + onuncu) % 10;
                if (onuncu == Convert.ToInt32(Tc[9].ToString()) && onbirinci == Convert.ToInt32(Tc[10].ToString()))
                {
                    
                }
                else
                {
                    MessageBox.Show("TC Kimlik numarası yanlış.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            string gender = "Null";

            if (gender_male.Checked)
                {  gender = "Erkek"; }
            else if (gender_female.Checked)
                {  gender = "Kadın"; }

            string query = bl.AddMember(Tc, name_text.Text, birthdate_picker.Value, gender , bloodtype_combo.Text, phone_text.Text , email_text.Text, city_combo.Text, text_addres.Text);

            try
            {
                int affectedRows = dal.ExecuteNonQuery(query);
                if (affectedRows > 0)
                {
                    dal.ExecuteNonQuery(query);
                    MessageBox.Show("Üye başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        private void ClearForm()
        {
            //clear all textboxes and comboboxes and datepickers and radiobuttons and checkboxes
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).Text = "";
                }
                else if (c is DateTimePicker)
                {
                    ((DateTimePicker)c).Value = DateTime.Now;
                }
                else if (c is RadioButton)
                {
                    ((RadioButton)c).Checked = false;
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
            }
        }
    }
}
