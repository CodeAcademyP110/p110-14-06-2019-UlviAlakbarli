using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Garanuy_Motors
{
    public partial class GaranuyMotors : Form
    {
        public GaranuyMotors()
        {
            InitializeComponent();
        }

        private void GaranuyMotors_Load(object sender, EventArgs e)
        {
            string constrng = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            using (SqlConnection Conn = new SqlConnection(constrng))
            {
               
                string coman = "Select Name from Brand";
                using(SqlCommand comando=new SqlCommand(coman, Conn))
                {
                    Conn.Open();
                    SqlDataReader read = comando.ExecuteReader();
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            cmbBrands.Items.Add(read[0].ToString());
                        }
                        cmbBrands.SelectedIndex = 0;
                    }
                }
            }
        }

        private void CmbBrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cost = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            using(SqlConnection con=new SqlConnection(cost))
            {
                string comst="select Model.Name from Model "+
                               "join Brand on Brand.Id = Model.BrandId "+
                              $"where Brand.Name = '{cmbBrands.SelectedItem.ToString()}'";
                using(SqlCommand combo=new SqlCommand(comst, con))
                {
                    con.Open();
                    SqlDataReader redr = combo.ExecuteReader();
                    if (redr.HasRows)
                    {
                        cmbModels.Items.Clear();
                        while (redr.Read())
                        {
                            cmbModels.Items.Add(redr[0].ToString());
                        }
                        cmbModels.SelectedItem = 0;
                    }
                    else
                    {
                        MessageBox.Show("Data is not found");
                    }
                }
            }
            cmbModels.SelectedIndex = 0;
        }
    }
}
