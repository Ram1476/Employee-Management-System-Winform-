using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_Record
{
    public partial class salesRec : Form
    {
        
        public salesRec()
        {
            InitializeComponent();
        }
        EmpDet Detail = new EmpDet();

        private void lblHead_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void salesRec_Load(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // inserting the below mwntioned Values into a Table 
            //insert into Employees(Emp_Id,Emp_Name,Emp_Salary,Designation,Status)
            SqlConnection con = new SqlConnection("server = NLTI151\\SQLEXPRESS; database = Empdata; integrated security =  true");
            int id, salary;
            string name, des, status;
            int.TryParse(txtID.Text,out id);
            int.TryParse(txtSalary.Text, out salary);
            name = txtName.Text;
            des = txtDes.Text;

            if (txtStat.Text.ToLower() == "active")
            {
                status = "ACTIVE";
            }
            else 
            {
                status = "IN ACTIVE";
            }


          
            SqlCommand cmd = new SqlCommand($"insert into Employees(Emp_Id,Emp_Name,Emp_Salary,Designation,Status)\nvalues('{id.ToString()}','{name}','{salary}','{des}','{status}');",con);

            con.Open();
            try
            { 
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
    }
}
