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
        SqlConnection con = new SqlConnection(@"server = NLTI151\SQLEXPRESS; database = Empdata; integrated security =  true");
        public salesRec()
        {
            InitializeComponent();
            txtDisplay.Visible= false;
            outgrid1.Visible=false;
        }
        EmpDet Detail = new EmpDet();

        private void lblHead_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
              outgrid1.Visible = true;
            //SqlDataAdapter da = new SqlDataAdapter("Select * From Employees where status = 'Active'",con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //string sr = "EmployeeID | EmployeeName | Salary | Designation\r\n";

            //string sd = "";
            //int i = 0;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (i == 0)
            //    {
            //        sd += sr;
            //        sd += dr[0].ToString() + "|" + dr[1].ToString() + "|" + dr[2].ToString() + "|" + dr[3].ToString() + "\r\n";
            //        i++;
            //    }
            //    else
            //    {
            //        sd += dr[0].ToString() + "|" + dr[1].ToString() + "|" + dr[2].ToString() + "|" + dr[3].ToString() +
            //             "\r\n";

            //    }
            //}

            //txtDisplay.Text = "Hello World1\nhow are u \r\n hope u r doing Good";
            if (display())
            {
                MessageBox.Show("Employee Details Successfully Displayed");
            }
            else 
            {
                MessageBox.Show("Employee details Not Found");
            }
        }

        private void salesRec_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'empdataDataSet.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.empdataDataSet.Employees);

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // inserting the below mwntioned Values into a Table 
            //insert into Employees(Emp_Id,Emp_Name,Emp_Salary,Designation,Status)
            
            int id, salary;
            string name, des, status;
            int.TryParse(txtID.Text,out id);
            int.TryParse(txtSalary.Text, out salary);
            name = txtName.Text;
            des = txtDes.Text;
            status = txtStat.Text;
            
            try
            {
                if (txtStat.Text != "")
                {

                    SqlCommand cmd = new SqlCommand($"insert into Employees(Emp_Id,Emp_Name,Emp_Salary,Designation,Status)\nvalues('{id.ToString()}','{name}','{salary}','{des}','{status}');", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Data SuccessFully Created");
                }
                else 
                {
                    SqlCommand cmd = new SqlCommand($"insert into Employees(Emp_Id,Emp_Name,Emp_Salary,Designation)\nvalues('{id.ToString()}','{name}','{salary}','{des}');", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Data SuccessFully Created");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Employee Data Not Created Enter Valid Input" + ex.Message);
            }
            finally
            {

                con.Close();
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id, salary;
            string name, des, status;
            int.TryParse(txtID.Text, out id);
            int.TryParse(txtSalary.Text, out salary);
            name = txtName.Text;
            des = txtDes.Text;
            status = txtStat.Text;

            //Emp_Id,Emp_Name,Emp_Salary,Designation
            SqlDataAdapter ds1 = new SqlDataAdapter("Select * from employees", con);
            SqlDataAdapter ds = new SqlDataAdapter($"update employees set Emp_Salary = {salary},Emp_ID = {id},Emp_Name = {name},des = {des},status ={status} where Emp_ID = {id}", con);
            DataTable dt = new DataTable();
            ds1.Fill(dt);
            bool isSuccess = false;
            bool isUpdated = false;
            foreach (DataRow Dr in dt.Rows) 
            {
                if (Dr[0].ToString() == id.ToString()) 
                {
                    ds.Fill(dt);
                    isSuccess = true;
                    break;
           
                } 
                
            }
            if (isSuccess) 
            {
                MessageBox.Show("Employee data has Been Successfullu Updated");
            }
            else 
            {
                MessageBox.Show($"Your EmployeeId {id} didn't Exist ");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id, salary;
            string name, des, status;
            int.TryParse(txtID.Text, out id);
            int.TryParse(txtSalary.Text, out salary);
            name = txtName.Text;
            des = txtDes.Text;
            status = txtStat.Text;


            SqlDataAdapter ds1 = new SqlDataAdapter("Select * from employees", con);
            SqlDataAdapter ds = new SqlDataAdapter($"update employees set Status = 'In Active' where Emp_ID = {id}", con);
            DataTable dt = new DataTable();
            ds1.Fill(dt);
            
            bool isSuccess = false;
            foreach (DataRow Dr in dt.Rows)
            {
                if (Dr[0].ToString() == id.ToString())
                {
                    if (Dr[4].ToString().ToLower() == "in active" )
                    {

                        isSuccess = false;
                        break;
                    }
                    else 
                    {
                        ds.Fill(dt);
                        isSuccess = true;
                        break;
                    }

                }

            }
            if (isSuccess)
            {
                MessageBox.Show("Employee data has Been Successfully deleted");
            }
            else
            {
                MessageBox.Show($"Your EmployeeId {id} didn't Exist ");
            }

        }

        private void outgrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        public bool display() 
        {
            bool isSuccess = false;
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("Select * From Employees Where Status = 'Active'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                outgrid1.DataSource = dt;
                isSuccess= true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show (ex.Message);

            }
            return isSuccess;
        }
    }
}
