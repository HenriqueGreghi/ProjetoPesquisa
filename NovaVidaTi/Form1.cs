using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace NovaVidaTi
{
    public partial class Form1 : Form
    {

        string Link = "https://www.kabum.com.br/lancamentos";
        string Xpath = "/html/body/div[1]/main/article/section/div[2]/div/main";
        //              /html/body/div[1]/main/article/section/div[2]/div/main/div[position()>0]
        //              /html/body/div[1]/main/article/section/div[2]/div/main/div[1]/a/div/div[1]/h2
        //              /html/body/div[1]/main/article/section/div[2]/div/main/div[1]/a/div[2]/div[2]/div[1]/div/span[2]
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadProduct();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        void LoadProduct()
        {
            var Products = new List<Product>();

            var Web = new HtmlWeb();

            var Dock = Web.Load(Link);

            var Nodes = Dock.DocumentNode.SelectNodes(Xpath);

            foreach(var Node in Nodes)
            {
                try
                {
                    var Product = new Product
                    {
                        Name = Node.SelectSingleNode("div[1]/a/div/div[1]/h2").InnerText,
                        Comment = Node.SelectSingleNode("div[1]/a/div[2]/div[2]/div[1]/div/span[2]").InnerText
                    };

                    Products.Add(Product);
                }
                catch { }
                

            }

            foreach (var Product in Products)
            {
                dataGridView1.Rows.Add(Product.Name, Product.Comment);
            }
        }

        class Product
        {
            public string Name { get; set; }

            public string Comment { get; set; }

            internal void Add(Product product)
            {
                throw new NotImplementedException();
            }
        }
    }


}
