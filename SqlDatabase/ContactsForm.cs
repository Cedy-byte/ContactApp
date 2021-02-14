using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlDatabase
{
    public partial class ContactsForm : Form
    {
        ContactsEntities db = new ContactsEntities();
        List <Contact> contacts = new List<Contact>();
        Contact selectedContact = new Contact();
        string username;
        public ContactsForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

      

        private void ContactsForm_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnSave.Enabled = false;

            refreshLocalList();
            refreshUI();

            //try
            //{
            //    SqlConnection cnn = new SqlConnection(Properties.Settings.Default.ContactconnectionString);
            //    cnn.Open();
            //    string sqlQuery = "Select * from Contacts where Username ='" + username + "'";
            //    SqlCommand command = new SqlCommand(sqlQuery, cnn);
            //    SqlDataReader dataReader = command.ExecuteReader();

            //    while (dataReader.Read())
            //    {
            //        Contact temp = new Contact(dataReader.GetInt32(0), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(),
            //            dataReader.GetValue(3).ToString(), dataReader.GetValue(4).ToString());
            //        contacts.Add(temp);
            //    }
            //    dataReader.Close();
            //    command.Dispose();
            //    cnn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}


        }

        private void lstContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;

            if (lstContacts.SelectedItem != null)
            {
                string selectedName = lstContacts.SelectedItem.ToString();
                foreach (Contact contact in contacts)
                {
                    if ((contact.FirstName+ " " + contact.Surname).Equals(selectedName))
                    {
                        selectedContact = contact;
                        break;
                    }
                }

                List<string> contactInfo = new List<string>();
                contactInfo.Add("Details");
                contactInfo.Add("---------------------------------------------");
                contactInfo.Add("ID : " + selectedContact.ID);
                contactInfo.Add("Name : " + selectedContact.FirstName);
                contactInfo.Add("Surame : " + selectedContact.Surname);
                contactInfo.Add("Cell : " + selectedContact.Telephone);
                contactInfo.Add("Email : " + selectedContact.Email);

                txtContact.Lines = contactInfo.ToArray<string>();
            }

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtSurname.Text == "" || txtEmail.Text == "" || txtCell.Text == "")
            {
                MessageBox.Show("Error values Missing");
            }
            else
            {
                Contact newContact = new Contact();
                newContact.FirstName = txtName.Text;
                newContact.Surname = txtSurname.Text;
                newContact.Email = txtEmail.Text;
                newContact.Telephone = txtCell.Text;
                newContact.Username = username;

                db.Contacts.Add(newContact);
                db.SaveChanges();

                refreshLocalList();
                refreshUI();
            }

            //else
            //{
            //    Contact newContact = new Contact(0, txtName.Text, txtSurname.Text, txtEmail.Text, txtCell.Text);

            //    try
            //    {
            //        SqlConnection cnn = new SqlConnection(Properties.Settings.Default.ContactconnectionString);
            //        cnn.Open();

            //        string sqlQuery = "Insert into Contacts (Firstname,Surname,Email,Telephone,Username) VALUES('"
            //            + newContact.Name + "','" + newContact.Surname + "','" + newContact.Email + "','"
            //            + newContact.CellPhone + "','" + username + "');SELECT SCOPE_IDENTITY();";

            //        SqlCommand command = new SqlCommand(sqlQuery, cnn);
            //        SqlDataAdapter adapter = new SqlDataAdapter();
            //        adapter.InsertCommand = command;

            //        int id = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());

            //        newContact.Id = id;
            //        contacts.Add(newContact);

            //        adapter.Dispose();
            //        command.Dispose();
            //        cnn.Close();
            //        MessageBox.Show("Contact Added Successfully");

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            
               
            //}
        }

        private void refreshUI()
        {
            txtName.Clear();
            txtSurname.Clear();
            txtEmail.Clear();
            txtCell.Clear();
            txtContact.Clear();

            contacts.Sort();
            lstContacts.Items.Clear();
            foreach (Contact contact in contacts)
            {
                lstContacts.Items.Add(contact.FirstName + " " + contact.Surname);
            }
        }

        private void refreshLocalList()
        {
            contacts = db.Contacts.Where(u => u.Username.Equals(username)).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtSurname.Text == "" || txtEmail.Text == "" || txtCell.Text == "")
            {
                MessageBox.Show("Error - values Missing!");
            }
            else
            {
                Contact newContact = new Contact();
                newContact.FirstName = txtName.Text;
                newContact.Surname = txtSurname.Text;
                newContact.Email = txtEmail.Text;
                newContact.Telephone = txtCell.Text;
                newContact.Username = username;
                newContact.ID = selectedContact.ID;

                db.Contacts.AddOrUpdate(newContact);
                db.SaveChanges();

                refreshLocalList();
                refreshUI();

            }

            //else
            //{
            //    Contact newContact = new Contact(selectedContact.Id, txtName.Text, txtSurname.Text, txtEmail.Text, txtCell.Text);
            //    try
            //    {
            //        SqlConnection cnn = new SqlConnection(Properties.Settings.Default.ContactconnectionString);
            //        cnn.Open();

            //        string sqlQuery = "Update Contacts set Firstname='" + newContact.Name
            //            + "',Surname='" + newContact.Surname + "',Email='" + newContact.Email
            //            + "',Telephone='" + newContact.CellPhone + "'WHERE ID=" + newContact.Id;

            //        SqlCommand command = new SqlCommand(sqlQuery, cnn);
            //        SqlDataAdapter adapter = new SqlDataAdapter();
            //        adapter.UpdateCommand = command;
            //        adapter.UpdateCommand.ExecuteNonQuery();

            //        MessageBox.Show("Updated Successfully");

            //        contacts.Remove(selectedContact);
            //        contacts.Add(newContact);

            //        adapter.Dispose();
            //        command.Dispose();
            //        cnn.Close();

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            refreshUI();
            //}
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            refreshUI();
            btnAddNew.Enabled = true;
            btnEdit.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnAddNew.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            txtContact.Clear();

            string selectedName = lstContacts.SelectedItem.ToString();
            Contact selectedContact = new Contact();

            foreach (Contact contact in contacts)
            {
                if ((contact.FirstName + " " + contact.Surname).Equals(selectedName))
                {
                    txtName.Text = contact.FirstName;
                    txtSurname.Text = contact.Surname;
                    txtEmail.Text = contact.Email;
                    txtCell.Text = contact.Telephone;
                    selectedContact = contact;
                    break;
                }
            }
            btnEdit.Enabled = false;
        }

    }
}
