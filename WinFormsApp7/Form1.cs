using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        public List<string> comboBoxItems = new();
        private SqlConnection con;
        private DataSet dataSet;
        private SqlDataAdapter adapter;
        private SqlCommandBuilder cmd;
        string cs = @"Server = (localdb)\MSSQLLocalDB; 
Integrated Security = SSPI; 
Database = Library";
        public Form1()
        {
            InitializeComponent();
            con = new();
            con.ConnectionString = cs;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataSet = new();
            string? aut = comboBox1.SelectedItem as string;
            string? query = null;
            if (aut is not null)
            {
                query = @$"SELECT * FROM Books AS B WHERE B.Id_Author = {aut[0]}";
                dataGridView1.DataSource = null;
                adapter = new SqlDataAdapter(query, con);
                adapter.Fill(dataSet, "book");

                dataGridView1.DataSource = dataSet.Tables["book"];
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataSet = new();
            string querry = "SELECT * FROM [Authors]";
            adapter = new(querry, con);
            adapter.Fill(dataSet, "author");

            foreach (DataRow item in dataSet.Tables["author"].Rows)
            {
                comboBoxItems.Add(item["Id"].ToString() + " " + item["FirstName"].ToString() + " " + item["LastName"].ToString());

            }


            comboBox1.DataSource = comboBoxItems;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
        }
    }
}
