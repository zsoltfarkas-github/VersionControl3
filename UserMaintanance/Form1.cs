﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintanance.Entities;  // megfelelő névtér behivatkozása mert a mappát alapból nem látja 

namespace UserMaintanance
{
    public partial class Form1 : Form    
    {

        BindingList<User> users = new BindingList<User>();  // 

        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource1.LastName;
            lblFirstName.Text = Resource1.FirstName;
            btnAdd.Text = Resource1.Add;                // hogy ne kelljen módosítani a kódot ha címkét cserélünk

            listUsers.DataSource = users; 
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text
            };
            users.Add(u);                       // létrehozás, értékadás és hozzáadás
        }
    }
}
