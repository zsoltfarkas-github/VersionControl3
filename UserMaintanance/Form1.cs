using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            lblFullName.Text = Resource1.FullName;
            btnAdd.Text = Resource1.Add;
            btnIras.Text = Resource1.Write;     // hogy ne kelljen módosítani a kódot ha címkét cserélünk

            listUsers.DataSource = users; 
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = txtFullName.Text     
            };
            users.Add(u);                       // létrehozás, értékadás és hozzáadás
        }

        private void btnIras_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() != DialogResult.OK) return;
            
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8)) //    ha igaz és már létezik ilyen fájl akkor a végéhez fűzi a sorokat, ha hamis, akkor felülírja a létező fájlt
            {
                // végigmegyünk a users lista elemein
                foreach (var u in users)
                {
                    sw.Write(u.ID);
                    sw.Write(";");
                    sw.Write(u.FullName);
                    sw.WriteLine();
                }
            }
        }
    }
}
