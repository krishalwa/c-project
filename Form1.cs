using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;





namespace vendingMachine
{
    public partial class Form : System.Windows.Forms.Form
    {

        // following variables maintain to access data from different methods
        // througout this programme
        // following variable are global
        int rw = 0;
        Employee[] EmployeeArray = new Employee[20];
        ArrayList coinsinserted = new ArrayList();
       
        int deserarray; // store current position of array to use in deserialize
        string dess = "false";// logic to decide data adding is fresh or after deserialize
        decimal totalpaid; // total coins paid 
        string productprice; // store select product price
        int totalqnty; // store available qnty on selected property
        string productselectedimage; // to display selected image on payment box
        int stock; // display current stock value 

        // Arraylist for avoiding icomparable null exception when using array[] objects
        //add array[] objects to this arraylist to sort usign icomparable
        ArrayList EmployeeList = new ArrayList();
        string itemdelete;

        public Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //generating grid view by code

            var gridcell = new DataGridViewTextBoxColumn();
            gridcell.HeaderText = "Product";
            gridcell.Name = "Product";
            gridcell.Width = 150;
            dataGridView1.Columns.Add(gridcell);


            gridcell = new DataGridViewTextBoxColumn();
            gridcell.HeaderText = "price";
            gridcell.Name = "Price";
            gridcell.Width = 150;
            
            dataGridView1.Columns.Add(gridcell);

            gridcell = new DataGridViewTextBoxColumn();
            gridcell.HeaderText = "qty_available";
            gridcell.Name = "qty_available";
            gridcell.Width = 150;
            dataGridView1.Columns.Add(gridcell);

            groupBox1.Visible = false;
            groupBox3.Visible = false;
            grpboxCoins.Visible = false;

            grpsorting.Visible = false;
            groupBox4.Visible = false;
            groupBoxSerializaiton.Visible = false;
            btnTotalQnty.Visible = false;
            lblTotalQnty.Visible = false;
            label5.Visible = false;
            btnnewtotal.Visible = false;





            //creating  20 employee object to store products
            for (int i = 0; i < 20; i++)
            {
                // initialise the array with numberOfEmployees Employee object
                EmployeeArray[i] = new Employee("  ");
            }
            //currencyManager = (CurrencyManager)dataGridView1.BindingContext[EmployeeArray];
            // dataGridView1.DataSource = EmployeeArray;

        }

        private void btnADDproduct_Click(object sender, EventArgs e)
        {
            //following code looking for existing item if found display message
            //otherwise add to products arraylist

            string itemsfound = ""; //variable to store existing item
            decimal d;
            int d1;
            int cellcount = 0;
            int rowIndex = -1;
            if (txtproductname.Text != "" && decimal.TryParse(txtProductprice.Text, out d) && Int32.TryParse(txtProductqty.Text, out d1))
            {


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    rowIndex = row.Index;
                    try
                    {
                        if (row.Cells[0].Value != null)
                        {
                            //to checking the product already in the product list
                            if (row.Cells[0].Value.ToString().Equals(txtproductname.Text) || row.Cells[0].Value.Equals(null))
                            {
                                itemsfound = row.Cells[0].Value.ToString();
                                MessageBox.Show("product already exist  " + row.Cells[0]);
                                btnCoke.Text = itemsfound;
                                break;

                            }
                        }
                    }

                    catch (Exception )
                    {

                        MessageBox.Show("product didnot added , only limtted to 20 products");
                        break;


                    }






                }

                // if product doesnot exist  adding products
                if (itemsfound == "")
                {

                    // the below if condition use to add products specially
                    //when deserialized and adding new item to avoid overriding exisiting items 
                    // in array[]
                     if (dess == "true")
                    {
                        deserarray = EmployeeArray[11].Arraynumber + 1;// storing current arraynumber  in array element 11
                     }
                    rw = deserarray;
                    if (rw <= 19)
                    {  
                        try
                        {
                            {
                                //adding product information to datagridview
                                dataGridView1.Rows.Add(txtproductname.Text, txtProductprice.Text, txtProductqty.Text);

                                //adding product information to product array
                                EmployeeArray[deserarray].P_name = this.txtproductname.Text;

                                EmployeeArray[deserarray].P_price = Convert.ToDouble(txtProductprice.Text);
                                EmployeeArray[deserarray].P_qnty = Convert.ToInt32(txtProductqty.Text);
                                EmployeeArray[deserarray].Arraynumber = deserarray;
                             
                                EmployeeArray[11].Arraynumber = deserarray; //storing current arraynumber
                                EmployeeList.Add(EmployeeArray[deserarray]);
                                deserarray++; // adding arraynumber
                                              //dataGridView1.Refresh();
                                rw++;
                                button3.Enabled = true;
                                

                            }
                            calctotalqnty(); //calling total qnty calculation method to calculate the qnty

                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("only limted to 10 products " + ex);
                        }
                    }
                    else
                    {
                        MessageBox.Show("limted to 20 products");
                    }

                }

                
                
            }
           
            
            else
            {
                // wrong data entry  on price and qty display this message
                MessageBox.Show("please check your price or qnty  entry");
            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //following code mainting and updating existing products
            int rowIndex = -1;

            foreach (DataGridViewRow row in dataGridView1.Rows)

            {
                rowIndex = row.Index;
                try
                {
                    if (row.Cells[0].Value.ToString().Equals(txtproductname.Text))
                    {

                        int stock = Convert.ToInt32(txtProductqty.Text);
                        if (stock > 0)
                        {
                            // dataGridView1.Rows.RemoveAt(row.Index);
                            EmployeeArray[rowIndex].P_name = this.txtproductname.Text;
                            EmployeeArray[rowIndex].P_price =Convert.ToDouble( this.txtProductprice.Text);
                            EmployeeArray[rowIndex].P_qnty =Convert.ToInt32(this.txtProductqty.Text);
                            // EmployeeArray[rw].P_price = Convert.ToInt32(this.txtProductprice.Text);
                            //  dataGridView1.Rows.Add(txtproductname);
                            row.Cells[0].Value = txtproductname.Text;
                            row.Cells[1].Value = txtProductprice.Text;
                            row.Cells[2].Value = txtProductqty.Text;

                            dataGridView1.Refresh();


                            break;
                        }
                        else
                        {


                            MessageBox.Show("product not found");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            calctotalqnty();// calling total qnty calculation method 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // following code remove item from datagridview only not update array[]
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
                itemdelete = item.Cells[0].Value.ToString();
            }

           

           

        }

        private void btnDataentryClose_Click(object sender, EventArgs e)
        {
            //hide groupbox1 which displays datagridview 1
            groupBox1.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // adming password hide and display 
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            { txtPassword.UseSystemPasswordChar = false; }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //veryfing admin password
            txtPassword.Text = "Admin";
            if (txtPassword.Text.ToString() == "Admin")
            {

                dataGridView1.Visible = true;
                groupBox1.Visible = true;

               
                grpsorting.Visible = true;
                groupBox4.Visible = true;
                groupBoxSerializaiton.Visible = true;
                btnTotalQnty.Visible = true;
                lblTotalQnty.Visible = true;
                label5.Visible = true;
                btnnewtotal.Visible = true;
            }
        }

        private void btnCoke_Click(object sender, EventArgs e)
        {
            //product coke selection
            productselection("coke");
            productselectedimage = "coke"; //storing product selection to in method
            button8.BackgroundImage = Properties.Resources.coke;// displya image on payment area


            // Specify the layout style of the background image. Tile is the default.
            button8.BackgroundImageLayout = ImageLayout.Stretch;



        }

        private void btnPepsi_Click(object sender, EventArgs e)
        {
            // product selection  cokebottle
            productselection("cokebottle");
            productselectedimage = "cokebottle"; //store selection to use in a mthod
            button8.BackgroundImage = Properties.Resources.coke500ml;

            // Specify the layout style of the background image. Tile is the default.
            button8.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnFanta_Click(object sender, EventArgs e)
        {
            productselection("fantabottle");
            productselectedimage = "fantabottle"; //storing product selection to in method
            button8.BackgroundImage = Properties.Resources.fanta1;// displya image on payment area


            // Specify the layout style of the background image. Tile is the default.
            button8.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn7up_Click(object sender, EventArgs e)
        {
            productselection("fanta");
            productselectedimage = "fanta"; //storing product selection to in method
            button8.BackgroundImage = Properties.Resources.fanta1;// displya image on payment area


            // Specify the layout style of the background image. Tile is the default.
            button8.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnSprite_Click(object sender, EventArgs e)
        {
            productselection("7up");
            productselectedimage = "7up"; //storing product selection to in method
            button8.BackgroundImage = Properties.Resources._7up;// displya image on payment area


            // Specify the layout style of the background image. Tile is the default.
            button8.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnDrpeper_Click(object sender, EventArgs e)
        {
            productselection("7upbottle");
            productselectedimage = "7upbottle"; //storing product selection to in method
            button8.BackgroundImage = Properties.Resources._7up;// displya image on payment area


            // Specify the layout style of the background image. Tile is the default.
            button8.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnTea_Click(object sender, EventArgs e)
        {
            productselection("pepsi");
            productselectedimage = "pepsi"; //storing product selection to in method
            button8.BackgroundImage = Properties.Resources.pepsi;// displya image on payment area


            // Specify the layout style of the background image. Tile is the default.
            button8.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btnMilk_Click(object sender, EventArgs e)
        {
            productselection("fantastrawberry");
            productselectedimage = "fantastrawberry"; //storing product selection to in method
            button8.BackgroundImage = Properties.Resources.fantastraw;// displya image on payment area


            // Specify the layout style of the background image. Tile is the default.
            button8.BackgroundImageLayout = ImageLayout.Stretch;
        }
       
        // method for availabe product qnty and displaying 
        private void productselection(string btn)
        {
            String searchValue = btn;
            int rowIndex = -1;

            foreach (DataGridViewRow row in dataGridView1.Rows)

            {
                try
                {
                    //verifying customer selection exist in the product 
                    if (row.Cells[0].Value.ToString().Equals(searchValue))
                    {
                        //store the qnty of the selected prduct 
                         stock = Convert.ToInt32(row.Cells[2].Value);
                        if (stock > 0) // if selected product qty >0 display price ,qty,product
                        {
                           // grpboxCoins.Visible = true;
                            groupBox3.Visible = true;
                            rowIndex = row.Index;
                            lblProdName.Text = "You have selected --> " + row.Cells[0].Value.ToString();
                            lblProdPrice.Text = "Price =" +string.Format("{0:f2}", row.Cells[1].Value);
                             productprice =row.Cells[1].Value.ToString();
                            lblProdQty.Text = "Availabel qty = " + " " + row.Cells[2].Value.ToString() + "Thank you very much";
                            groupBox3.Visible = true;
                            lblProdQty.Visible = true;
                            lblProdPrice.Visible = true;
                            lblProdName.Visible = true;
                            // cokeprice = (double)row.Cells[1].Value;






                            break;
                        }
                        else
                        {
                            //if selectted product <0 displays following messages
                            groupBox3.Visible = false; // if stock <0 hide payment buton
                            grpboxCoins.Visible = false;

                            label1.Text = "product out of stock";
                            MessageBox.Show("product out of stock please try another product");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    grpboxCoins.Visible = false;
                    groupBox3.Visible = false;
                    MessageBox.Show("product out of stock contact admin please"); }
                }




        }

        private void btnPrdcancel_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = false;

        }

        // without using ISerializable  just using [serilizable]
        // private const string FILENAME = @"C:\Users\thayalini\source\repos\vendingMachine\vendingMachine\bin\Debug\productsfile.xml";
        private const string FILENAME = @"productsfile.xml";

        // using SOAP method to serialize 
        private void button1_Click(object sender, EventArgs e)
        {
            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = new FileStream(FILENAME, FileMode.Create))
            {
                formatter.Serialize(fs, EmployeeArray);

            }
            button3.Enabled = true;
        }


        /// <summary>
        ///  SOAP DESERIALIZATION OF PRODUCT FILE
        /// </summary>
        int arnumber = 0;
         private void button3_Click_1(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(FILENAME, FileMode.Open))
            {

                SoapFormatter sf = new SoapFormatter();
                EmployeeArray = (Employee[])sf.Deserialize(fs);
                //currencyManager = (CurrencyManager)dataGridView1.BindingContext[EmployeeArray];
                // dataGridView1.DataSource = EmployeeArray;
                addingdatatoarraylist();
                string[] items = new string[3];
                string hk = null;
                char xc = ' ';

                // loop through each item in the product array and add to 
                // grdiview if product name is not empty 
                int rowin = 0;
                foreach (Employee abc in EmployeeArray)
                {
                    char[] splitchar = { ' ' };
                    if (abc.P_name != null)
                    {
                        string[] pr1 = new string[50];
                        // string prs = Convert.ToString(abc.P_name);
                        string prs = Convert.ToString(abc);
                        pr1[0] = abc.P_name;
                        pr1[1] = Convert.ToString(abc.P_price);
                        pr1[2] = Convert.ToString(abc.P_qnty);

                        //  pr1 = prs.Split(splitchar);
                        //  dataGridView1.Rows.Add(pr1[0]+pr1[0]+pr1[2]);
                        //dataGridView1.Rows.Add(pr1[1]);
                        // dataGridView1.Rows.Add(pr1[2]);
                        if (!string.IsNullOrWhiteSpace(pr1[0]))
                        {
                            if (deserarray < 20)
                            {
                                // dataGridView1.Rows.Add(pr1[0], pr1[1], pr1[2]);


                                this.dataGridView1.Rows.Add();
                                this.dataGridView1.Rows[rowin].Cells[0].Value = abc.P_name;
                                this.dataGridView1.Rows[rowin].Cells[1].Value = abc.P_price;
                                this.dataGridView1.Rows[rowin].Cells[2].Value = abc.P_qnty;
                                rowin++;






                               
                                deserarray++;
                                dess = "true";
                            }
                            EmployeeArray[11].Arraynumber = deserarray;

                        }
                        

                    }


                   
                }
                




            }
            calctotalqnty(); // calling total qnty callculation method to callculate total qty
        }

        private void btnSortbyqty_Click(object sender, EventArgs e)
        {
            // sorting product using icomparable and looping each item in the array
            // and adding to datgridview1
            EmployeeList.Sort();
            int rowin =0;
            try
            {
                foreach (Employee productss in EmployeeList)
                {
                    if (productss.P_name !=null)
                    {
                        //dataGridView1.DataSource = EmployeeList;
                        dataGridView1.Rows[rowin].Cells[0].Value = productss.P_name;
                        dataGridView1.Rows[rowin].Cells[1].Value = productss.P_price;
                        dataGridView1.Rows[rowin].Cells[2].Value = productss.P_qnty;
                        dataGridView1.Refresh();
                        rowin++;
                    }
                 }
                    
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message ,"   ///No products found upload products and try again"); }
        }

        private void btnBuyprd_Click(object sender, EventArgs e)
        {
            if (stock > 0)
            {
               
               // groupBox1.Visible = false;
                grpboxCoins.Visible = true;
               
            }
            else
            {
                MessageBox.Show("product out of stock");

            }
        



        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Close();
        }
        int k = 0;
        private void btnFivepence_Click(object sender, EventArgs e)
        {
            Coins cinsert = new Coins();
            cinsert.Fivep = 5;
            coinsinserted.Add(cinsert);
            coins.Items.Add("Five pence added");
        
            totalpaid = totalpaid +(decimal) .05;
            //btnTotal.Text = string.Format("{0:f2}",totalpaid);
            btnTotal.Text = string.Format("{0:C}", totalpaid);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Coins cinsert = new Coins();
            cinsert.Twntyp = 20;
            coinsinserted.Add(cinsert);
            coins.Items.Add("Tewenty pence added");

            totalpaid = totalpaid + (decimal).20;
           // btnTotal.Text = string.Format("{0:f2}", totalpaid);
            btnTotal.Text = string.Format("{0:C}", totalpaid);
        }

        // method for coins inserted
        


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnBInarySerialization_Click(object sender, EventArgs e)
        {
            Stream strem= File.Open("coins.dat", FileMode.Create);
            BinaryFormatter bformatter=new BinaryFormatter();
            bformatter.Serialize(strem, coinsinserted);
            strem.Close();
            MessageBox.Show("coins details saved ");
            


        }

        private void btnBinaryDeserialization_Click(object sender, EventArgs e)
        {
            try
            {
                Stream stream = File.Open("coins.dat", FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                coinsinserted = (ArrayList)bformatter.Deserialize(stream);
                stream.Close();
            }
            catch
            { MessageBox.Show("please check your binary file"); }
            // @"C:\Users\thayalini\source\repos\vendingMachine\vendingMachine\bin\Debug\productsfile.xml"
           
            foreach (Coins abc in coinsinserted)
           {
                if (abc!=null)
                {
                    if (abc.Fivep != 0)
                    {
                        totalpaid = totalpaid +(decimal) 0.05;
                        coins.Items.Add(abc.Fivep + "  " +"pence");
                        //btnTotal.Text = string.Format("{0:f2}",totalpaid);
                        btnTotal.Text = string.Format("{0:C}", totalpaid);
                    }
                    if (abc.Tenp!=0)
                    {
                        totalpaid = totalpaid + (decimal)0.05;
                        coins.Items.Add(abc.Fivep + "  " + "pence");
                       // btnTotal.Text = string.Format("{0:f2}", totalpaid);
                        btnTotal.Text = string.Format("{0:C}", totalpaid);
                    }

                    if(abc.Twntyp!=0)
                    {
                        totalpaid = totalpaid + (decimal)0.20;
                        coins.Items.Add(abc.Twntyp + "  " + "pence");
                       // btnTotal.Text = string.Format("{0:f2}", totalpaid);
                        btnTotal.Text = string.Format("{0:C}", totalpaid);
                    }
                    if (abc.Fiftyp != 0)
                    {
                        totalpaid = totalpaid + (decimal)0.50;
                        coins.Items.Add(abc.Fiftyp + "  " + "pence");
                       // btnTotal.Text = string.Format("{0:f2}", totalpaid);
                        btnTotal.Text = string.Format("{0:C}", totalpaid);
                    }
                    if (abc.Onepound != 0)
                    {
                        totalpaid = totalpaid + (decimal)1;
                        coins.Items.Add(abc.Onepound + "pence  " );
                       // btnTotal.Text = string.Format("{0:f2}", totalpaid);
                        btnTotal.Text = string.Format("{0:C}", totalpaid);
                    }


                }


           }

        }

        private void btnconfirmbuy_Click(object sender, EventArgs e)
        {
            decimal p_price = Convert.ToDecimal(productprice);
                if(p_price<=totalpaid)
                 {
                     MessageBox.Show("please collect your drink");
                 //    stockupdating("coke");
                        stockupdating(productselectedimage);
                    
                    //coinsinserted = null;
                         coins.Items.Clear();
                        btnTotal.Text=  (totalpaid = 0).ToString();
                      

                        calctotalqnty();
                         grpboxCoins.Visible = false;
                        groupBox3.Visible = false;


                    //Close();

                }
                else
                 {
                        DialogResult result = MessageBox.Show(this, "do you want to add more coins", "Insufficnet Money",MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign);

                        if (result == DialogResult.No)
                            {

                                     // popup message for insufficent money inserted for the product
                                        MessageBox.Show("coins return, please take your money");
                                        this.Close();

                            }


                            MessageBox.Show("insufficent payment-check total paid");

                }
        
        }
        // updating qnty
        public void stockupdating(string prdct)
        {
            for(int i=0;i< EmployeeArray.Length;i++)
            {
                try
                {
                    if (EmployeeArray[i].P_name.Equals(prdct))

                    {
                        MessageBox.Show(Convert.ToString(EmployeeArray[i].P_qnty));
                        EmployeeArray[i].P_qnty = EmployeeArray[i].P_qnty - 1;

                    }
                }
                catch(Exception ex)
                {
                   

                }
            }
            ///
            /// 
            /// 
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                rowIndex = row.Index;
                try
                {
                    if (row.Cells[0].Value.ToString().Equals(prdct))
                    {
                        int stock = Convert.ToInt32(row.Cells[2].Value);
                        if (stock > 0)
                        {

                            row.Cells[2].Value = stock - 1;

                            dataGridView1.Refresh();


                            break;
                        }
                        else
                        {


                            MessageBox.Show("product out of stock");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }

            /// 
            /// 
            /// 
            /// 
            ///
        }
        // serializing  products information
        private void btnBINARY_serialization_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("products_binary.dat", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, EmployeeArray);



            }
        }
        // deserializing product informations
        private void btn_BINARY_DESERILZ_Click(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream("products_binary.dat", FileMode.Open))
            {
             BinaryFormatter bf= new BinaryFormatter();
                EmployeeArray = (Employee[])bf.Deserialize(fs);
                addingdatatoarraylist();




                //
                int rowin = 0;
                for (int i = 0; i < EmployeeArray.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(EmployeeArray[i].P_name))
                    {
                        try
                             {
                            
                            this.dataGridView1.Rows.Add();
                            this.dataGridView1.Rows[rowin].Cells[0].Value = EmployeeArray[i].P_name;
                            this.dataGridView1.Rows[rowin].Cells[1].Value = EmployeeArray[i].P_price;
                            this.dataGridView1.Rows[rowin].Cells[2].Value = EmployeeArray[i].P_qnty;
                            rowin++;

                            //}
                        }
                        catch
                        {

                        }
                   }
                }
                dess = "true";

                //


            }
            calctotalqnty();

        }

        private void btnSortbyqty_Click_1(object sender, EventArgs e)
        {
             Array.Sort(EmployeeArray, new Employee());
            int rowin = 0;
            try
            {
                // foreach ( productss in EmployeeArray)
                for (int i = 0; i < EmployeeArray.Length; i++)
                {
                    string pname = EmployeeArray[i].P_name;
                    if (!string.IsNullOrWhiteSpace(pname))
                    {
                        
                        this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[rowin].Cells[0].Value = EmployeeArray[i].P_name;
                        this.dataGridView1.Rows[rowin].Cells[1].Value = EmployeeArray[i].P_price;
                        this.dataGridView1.Rows[rowin].Cells[2].Value = EmployeeArray[i].P_qnty;
                        rowin++;
                    }
                    
                }
                
            }
            catch (Exception ex)
            { }


        }


        int temptotalqnty = 0;
        int totalqnty1;
        int xe = 0;
        ArrayList ts = new ArrayList();
        
        public void calctotalqnty()
        {
            temptotalqnty = 0;
            totalqnty1 = 0;
            double stockvalue=0;
            foreach (Employee hs in EmployeeArray)
            {
                temptotalqnty = hs.P_qnty;
                stockvalue = stockvalue + (hs.P_price * hs.P_qnty);
              
                totalqnty1 = totalqnty1 + temptotalqnty;
                btnTotalQnty.Text = totalqnty1.ToString();
                btnnewtotal.Text =string.Format("{0:C}",stockvalue);
                temptotalqnty = 0;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void txtproductname_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(txtproductname.Text==string.Empty)
            {
                MessageBox.Show("This field cannot be empty", "product name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnnewtotal_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Coins cinsert = new Coins();
            cinsert.Fiftyp = 50;
            coinsinserted.Add(cinsert);
            coins.Items.Add("Fiftey pence added");

            totalpaid = totalpaid + (decimal).50;
            btnTotal.Text = string.Format("{0:f2}", totalpaid);
            //btnTotal.Text = string.Format("{ 0:C}", totalpaid);
             btnTotal.Text= string.Format("{0:C}",totalpaid);
        }

        private void btnOnepound_Click(object sender, EventArgs e)
        {
            Coins cinsert = new Coins();
            cinsert.Onepound = 100;
            coinsinserted.Add(cinsert);
            coins.Items.Add("One pound added");

            totalpaid = totalpaid + (decimal)1;
            //btnTotal.Text = string.Format("{0:f2}", totalpaid);
            btnTotal.Text = string.Format("{0:C}", totalpaid);
        }

        public void addingdatatoarraylist()
        {

            foreach (Employee productinfor in EmployeeArray)
            {
                if (!string.IsNullOrWhiteSpace(productinfor.P_name))
                {
                    EmployeeList.Add(productinfor);
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Array.Sort(EmployeeArray, new Employee());
            int rowin = 0;
            try
            {
                // foreach ( productss in EmployeeArray)
                for (int i = 0; i < EmployeeArray.Length; i++)
                {
                    string pname = EmployeeArray[i].P_name;
                    if (!string.IsNullOrWhiteSpace(pname))
                    {
                       
                        this.dataGridView2.Rows.Add();
                        this.dataGridView2.Rows[rowin].Cells[0].Value = EmployeeArray[i].P_name;
                        this.dataGridView2.Rows[rowin].Cells[1].Value = EmployeeArray[i].P_qnty;
                        rowin++;
                    }
                    //ataGridView1.Rows.Add(productss.P_name, productss.P_price, productss.P_qnty);
                }
                
            }
            catch (Exception ex)
            { }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        ///
    }

}
