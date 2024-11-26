using Middleware.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Client
{
    public partial class Form1 : Form
    {
        public IUserRPC _userRPC;
        public IProductRPC _productRPC;
        public ICategoryRPC _categoryRPC;
        public IRoleRPC _roleRPC;
        public int selectedId = 0;
        public Form1()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Form1_Resize);
            CenterPanel();
            TcpChannel chnl = new TcpChannel();
            ChannelServices.RegisterChannel(chnl, false);
            _userRPC = (IUserRPC)Activator.GetObject(typeof(IUserRPC), "tcp://localhost:1234/user");
            _productRPC = (IProductRPC)Activator.GetObject(typeof(IProductRPC), "tcp://localhost:1234/product");
            _categoryRPC = (ICategoryRPC)Activator.GetObject(typeof(ICategoryRPC), "tcp://localhost:1234/category");
            _roleRPC = (IRoleRPC)Activator.GetObject(typeof(IRoleRPC), "tcp://localhost:1234/role");
            ShowLoginPanel();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void CenterPanel()
        {

            loginPanel.Left = (this.ClientSize.Width - loginPanel.Width) / 2;
            loginPanel.Top = (this.ClientSize.Height - loginPanel.Height) / 2;
            registerPanel.Left = (this.ClientSize.Width - registerPanel.Width) / 2;
            registerPanel.Top = (this.ClientSize.Height - registerPanel.Height) / 2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool result = _userRPC.Register(registerEmail.Text, registerPassword.Text, registerName.Text, 1);
            MessageBox.Show(result.ToString());

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLoginPanel();

        }

        public void ShowLoginPanel()
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Hide();
            rolesPanel.Hide();
            categoryPanel.Hide();
            loginPanel.Show();
            addUsersPanel.Hide();
            usersPanel.Hide();
            updateCategoryPanel.Hide();


        }

        public void ShowRegisterPanel()
        {
            loginPanel.Hide();
            productsPanel.Hide();
            userPanel.Hide();
            usersPanel.Hide();
            addUsersPanel.Hide();
            rolesPanel.Hide();
            categoryPanel.Hide();
            addProductPanel.Hide();
            addRolePanel.Hide();
            updateCategoryPanel.Hide();

            registerPanel.Show();
        }

        public void ShowProductsPanel()
        {
            loginPanel.Hide();
            registerPanel.Hide();
            categoryPanel.Hide();
            addProductPanel.Hide();
            rolesPanel.Hide();
            addCategoryPanel.Hide();
            usersPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Hide();
            userPanel.Show();
            productsPanel.Show();
            updateRolePanel.Hide();
            updateCategoryPanel.Hide();

        }
        public void ShowUserPanel()
        {
            loginPanel.Hide();
            registerPanel.Hide();
            categoryPanel.Hide();
            rolesPanel.Hide();
            addRolePanel.Hide();
            updateCategoryPanel.Hide();

            productsPanel.Hide();
            addCategoryPanel.Hide();
            addUsersPanel.Hide();
            userPanel.Show();
        }
        public void ShowAddCategoryPanel()
        {
            loginPanel.Hide();
            registerPanel.Hide();
            categoryPanel.Hide();
            addProductPanel.Hide();
            productsPanel.Hide();
            addRolePanel.Hide();
            updateCategoryPanel.Hide();

            usersPanel.Hide();
            rolesPanel.Hide();
            userPanel.Show();
            addCategoryPanel.Show();
            addUsersPanel.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowRegisterPanel();
        }

        private void productsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {


        }

        private void panel1_Paint(object sender, PaintEventArgs e)        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            List<object> result= _userRPC.Login(loginEmail.Text,loginPassword.Text);
            if (!bool.Parse(result[0].ToString()))
                MessageBox.Show("Login Failed");
            if (bool.Parse(result[0].ToString())) {
                if (int.Parse(result[1].ToString())==1)
                {
                    ShowProductsPanel();
                    FillProductListView();
                    FillCategoryListView();
                    FillUserListView();
                    FillRoleListView();
                }
                else
                {
                    // code for user interface
                }
            }
        }

        public void LoadDataToProductListView()
        {
            List<Dictionary<string, object>> products = _productRPC.GetAllProducts();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void userPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void productsListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addProductPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void FillProductListView()
        {
            productsListView.Items.Clear();  

            
            List<Dictionary<string, object>> products = _productRPC.GetAllProducts();




            foreach (var product in products)
            {
                ListViewItem listViewItem = new ListViewItem(product["Id"].ToString());
                Dictionary<string,object> category =  _categoryRPC.GetCategoryById(int.Parse(product["CategoryId"].ToString()));
                listViewItem.SubItems.Add(product["Name"].ToString());
                listViewItem.SubItems.Add(product["Description"].ToString());
                listViewItem.SubItems.Add(Convert.ToDouble(product["Price"]).ToString("C")); 
                listViewItem.SubItems.Add(product["Qte"].ToString());
                listViewItem.SubItems.Add(category["Name"].ToString());

                productsListView.Items.Add(listViewItem);
            }


        }
        private void FillUserListView()
        {
            userListView.Items.Clear();  


            List<Dictionary<string, object>> users = _userRPC.getAllUsers();




            foreach (var user in users)
            {

                ListViewItem listViewItem = new ListViewItem(user["Id"].ToString());


                listViewItem.SubItems.Add(user["Name"].ToString());
                listViewItem.SubItems.Add(user["Email"].ToString());
                listViewItem.SubItems.Add(user["RoleId"].ToString());
                

                
                userListView.Items.Add(listViewItem);
            }


        }


        private void FillCategoryListView()
        {
            categoryListView.Items.Clear();  


            List<Dictionary<string, object>> categories = _categoryRPC.GetAllCategories();




            foreach (var category in categories)
            {

                ListViewItem listViewItem = new ListViewItem(category["Id"].ToString());


                listViewItem.SubItems.Add(category["Name"].ToString());


                categoryListView.Items.Add(listViewItem);
            }


        }

        private void FillRoleListView()
        {
            roleListView.Items.Clear();


            List<Dictionary<string, object>> roles = _roleRPC.GetAllRoles();




            foreach (var role in roles)
            {

                ListViewItem listViewItem = new ListViewItem(role["Id"].ToString());


                listViewItem.SubItems.Add(role["Name"].ToString());


                roleListView.Items.Add(listViewItem);
            }


        }

        private void addProductButton_Click(object sender, EventArgs e)
        {
            ShowAddProductPanel();
        }
        public void ShowAddProductPanel()
        {
            List<Dictionary<string, object>> categories = _categoryRPC.GetAllCategories();
            addProductListbox.Items.Clear();
            foreach (var category in categories)
            {
                addProductListbox.Items.Add(category["Name"].ToString());
            }

            productsPanel.Hide();
            userPanel.Show();
            addRolePanel.Hide();
            updateCategoryPanel.Hide();

            rolesPanel.Hide();
            addUsersPanel.Hide();
            addProductPanel.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            ShowProductsPanel();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowProductsPanel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (productsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            
            ListViewItem selectedItem = productsListView.SelectedItems[0];

            
            int productId = Convert.ToInt32(selectedItem.Text); // Text is the first column (Product Id)

            


            
            _productRPC.DeleteProduct(productId);
            FillProductListView();
        }

        private void addProductAddButton_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(addProductNameTextbox.Text) ||
                string.IsNullOrWhiteSpace(addProductDescriptionTextbox.Text) ||
                string.IsNullOrWhiteSpace(addProductPriceTextbox.Text) ||
                string.IsNullOrWhiteSpace(addProductQuantityTextbox.Text) 
                )
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {
                
                string name = addProductNameTextbox.Text;
                string description = addProductDescriptionTextbox.Text;
                double price = Convert.ToDouble(addProductPriceTextbox.Text);
                int quantity = Convert.ToInt32(addProductQuantityTextbox.Text);
                int categoryId = 1;

                
                _productRPC.AddProduct(name, description, price, quantity, categoryId);

                
                FillProductListView();

                
                ClearAddProductForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message);
            }
        }

        void ClearAddProductForm()
        {
            addProductNameTextbox.Clear();
            addProductDescriptionTextbox.Clear();
            addProductPriceTextbox.Clear();
            addProductQuantityTextbox.Clear();
        }

        private void addProductClearButton_Click(object sender, EventArgs e)
        {
            ClearAddProductForm();
        }

        private void addProductBacktolistButton_Click(object sender, EventArgs e)
        {
            ShowProductsPanel();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

       void ShowCategoryPanel()
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            loginPanel.Hide();
            addCategoryPanel.Hide();
            addRolePanel.Hide();
            updateCategoryPanel.Hide();

            usersPanel.Hide();
            rolesPanel.Hide();
            addUsersPanel.Hide();
            categoryPanel.Show();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowProductsPanel();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowCategoryPanel();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            addCategoryNameTextBox.Clear();
        }

        private void addCategoryNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowCategoryPanel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowAddCategoryPanel();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addCategoryNameTextBox.Text) 
                )
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {

                string name = addCategoryNameTextBox.Text;
               


                _categoryRPC.AddCategory(name);


                FillCategoryListView();


                addCategoryNameTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message);
            }
        }

        private void addCategoryPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            ShowAddUsersPanel();
        }

        public void ShowUsersPanel()
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            addUsersPanel.Hide();
            rolesPanel.Hide();
            loginPanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();
            updateCategoryPanel.Hide();

            usersPanel.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowUsersPanel();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            addUserEmailTextbox.Clear();
            addUserNameTextbox.Clear();
            addUserPasswordTextbox.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bool result = _userRPC.Register(addUserEmailTextbox.Text, addUserPasswordTextbox.Text, addUserNameTextbox.Text, 1);
            FillUserListView();
            MessageBox.Show(result.ToString());
        }
        private void ShowAddUsersPanel()
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            loginPanel.Hide();
            rolesPanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();
            updateCategoryPanel.Hide();

            usersPanel.Hide();
            addUsersPanel.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            loginPanel.Hide();
            updateCategoryPanel.Hide();

            addUsersPanel.Hide();
            rolesPanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();

            usersPanel.Show();
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (userListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }


            ListViewItem selectedItem = userListView.SelectedItems[0];


            int userId = Convert.ToInt32(selectedItem.Text); 





            _userRPC.ResetPassword(userId);
            FillProductListView();
            MessageBox.Show("The new password is test");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (userListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            ListViewItem selectedItem = userListView.SelectedItems[0];
            int userId = Convert.ToInt32(selectedItem.Text);
            _userRPC.DeleteUser(userId);
            FillUserListView();
            
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            loginPanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();
            updateCategoryPanel.Hide();


            addUsersPanel.Hide();
            usersPanel.Hide();
            rolesPanel.Show();
        }

        private void rolesPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_3(object sender, PaintEventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            updateCategoryPanel.Hide();

            categoryPanel.Hide();
            loginPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();
            usersPanel.Hide();
            rolesPanel.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            loginPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Show();
            updateCategoryPanel.Hide();

            usersPanel.Hide();
            rolesPanel.Hide();
            updateRolePanel.Hide();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addRoleTextbox.Text)
               )
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {

                string name = addRoleTextbox.Text;



                _roleRPC.AddRole(name);


                FillRoleListView();


                addRoleTextbox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding role: " + ex.Message);
            }
        }

        private void panel1_Paint_4(object sender, PaintEventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            updateRoleTextbox.Clear();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            loginPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();
            updateCategoryPanel.Hide();

            usersPanel.Hide();
            rolesPanel.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {



            _roleRPC.UpdateRole(selectedId,updateRoleTextbox.Text);
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            loginPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();
            updateCategoryPanel.Hide();
            usersPanel.Hide();
            rolesPanel.Show();
            FillRoleListView();
            
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (roleListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            ListViewItem selectedItem = roleListView.SelectedItems[0];
            int roleId = Convert.ToInt32(selectedItem.Text);
            selectedId = roleId;
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            loginPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Hide();
            addRolePanel.Hide();
            updateCategoryPanel.Hide();
            updateRolePanel.Show();
            usersPanel.Hide();
            rolesPanel.Hide();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (roleListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a role to delete.");
                return;
            }


            ListViewItem selectedItem = roleListView.SelectedItems[0];


            int roleId = Convert.ToInt32(selectedItem.Text); 





            _roleRPC.DeleteRole(roleId);
            FillRoleListView();
        }

        private void roleListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (categoryListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a category to delete.");
                return;
            }
            ListViewItem selectedItem = categoryListView.SelectedItems[0];
            int categoryId = Convert.ToInt32(selectedItem.Text);
            _categoryRPC.DeleteCategory(categoryId);
            FillCategoryListView();
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            updateCategoryTextbox.Clear();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Show();
            loginPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();
            usersPanel.Hide();
            rolesPanel.Hide();
            updateCategoryPanel.Hide();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (categoryListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a category to update.");
                return;
            }

            ListViewItem selectedItem = categoryListView.SelectedItems[0];
            int categoryId = Convert.ToInt32(selectedItem.Text);
            selectedId = categoryId;
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Hide();
            loginPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Hide();
            addRolePanel.Hide();
            updateCategoryPanel.Show();
            updateRolePanel.Hide();
            usersPanel.Hide();
            rolesPanel.Hide();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            _categoryRPC.UpdateCategory(selectedId, updateCategoryTextbox.Text);
            registerPanel.Hide();
            productsPanel.Hide();
            addProductPanel.Hide();
            userPanel.Show();
            categoryPanel.Show();
            loginPanel.Hide();
            addUsersPanel.Hide();
            addRolePanel.Hide();
            addRolePanel.Hide();
            updateRolePanel.Hide();
            updateCategoryPanel.Hide();
            usersPanel.Hide();
            rolesPanel.Hide();
            FillCategoryListView();
        }
    }
}
