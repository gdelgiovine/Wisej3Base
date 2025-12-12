using Dapper;
using Passero.Framework;    
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using Wisej.Web;

namespace Wisej3Base
{
    public partial class frmPasseroFramework : Form
    {
        private readonly string defaultConnectionString = "Data Source=(local);Initial Catalog=Pubs;Integrated Security=True";
        private SqlConnection connection = new SqlConnection();
        //private Passero.Framework.ViewModel<PasseroModel_Titles> vmTitles = new ViewModel<PasseroModel_Titles>();  
        private vmTitles vmTitles = new vmTitles();    
        public frmPasseroFramework()
        {
            InitializeComponent();
            this.txtConnectionString .Text = defaultConnectionString;       

        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(this.txtConnectionString.Text);
        }


        private void Init()
        {
            try
            {
                connection = CreateConnection();
                connection.Open();
                this.vmTitles.Init(connection);
                this.vmTitles.DataBindingMode = DataBindingMode.BindingSource;
                this.vmTitles.BindingSource = this.bindingSource1;

                this.dataNavigator1.AddViewModel(this.vmTitles , "Titles", null , null);
                this.dataNavigator1.SetActiveViewModel(this.vmTitles );

                this.dataNavigator1.ManageNavigation = true;
                this.dataNavigator1.ManageChanges = true;

                //this.dataNavigator1.UseUpdateEx = true;
                this.dataNavigator1.Init(true);
                
                this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
                this.btnConnect.Text = "Connected";




            }
            catch (Exception ex)
            {
                this.btnConnect.BackColor = System.Drawing.Color.LightCoral;
                this.btnConnect.Text = "Connect";
                MessageBox.Show("Errore durante la connessione: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void btnConnect_Click(object sender, EventArgs e)
        {

            this.Init();
        }

        private void frmPasseroFramework_Load(object sender, EventArgs e)
        {

        }

        private void btnQueryTitlesDataGetAllItems_Click(object sender, EventArgs e)
        {
            this.vmTitles.GetAllItems ();
        }

        private void btnQueryTitlesGetItems_Click(object sender, EventArgs e)
        {


            //this.GetItemsByTitle(this.txtTitleLike.Text);
            this.GetItemsByTitleViewModel(this.txtTitleLike.Text);
            this.panel1 .Enabled = true;        


        }

        private void GetItemsByTitle(string titleFilter)
        {
            // non ottimale perchp nella view non dovrebbe esistere la logica di query  
           //
        }


        private void GetItemsByTitleViewModel(string titleFilter)
        {
            this.vmTitles  .GetItemsByTitle(titleFilter);
        }


        private string GenerateRandomTitleId(int maxLength = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(System.Linq.Enumerable.Range(0, maxLength).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }

        private PasseroModel_Titles CreateRandomTitle()
        {
            var titleId = GenerateRandomTitleId();
            var random = new Random();
            string[] types = { "business", "mod_cook", "popular_comp", "psychology", "trad_cook", "UNDECIDED" };
            string[] pubIds = { "0736", "0877", "1389", "1622", "1756", "9901", "9952", "9999" };

            return new PasseroModel_Titles
            {
                Title_Id = titleId,
                Title = "Titolo Casuale " + titleId,
                Type = types[random.Next(types.Length)],
                Pub_Id = pubIds[random.Next(pubIds.Length)],
                Price = Math.Round((decimal)(random.NextDouble() * 50 + 5), 2),
                Advance = Math.Round((decimal)(random.NextDouble() * 10000), 2),
                Royalty = random.Next(10, 25),
                Ytd_Sales = random.Next(0, 50000),
                Notes = "Inserito automaticamente il " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                PubDate = DateTime.Now.AddDays(-random.Next(0, 3650))
            };
        }
        private void dataNavigator1_eAfterAddNewRequest(ref bool Cancel)
        {
            PasseroModel_Titles newTitle = CreateRandomTitle();
            this.txtTitles_TitleId.Text = newTitle.Title_Id;
            this.txtTitles_Title.Text = newTitle.Title;
            this.txtTitles_Notes.Text = newTitle.Notes;
            

        }

        private void dataNavigator1_eAddNewRequest(ref bool Cancel)
        {
            

        }

        private void AddToBinding(PasseroModel_Titles title)
        {
            if (this.bindingSource1.DataSource is BindingList<PasseroModel_Titles> list)
            {
                list.Add(title);
                bindingSource1.MoveLast();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            this.dataNavigator1 .MoveFirst();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            this.dataNavigator1 .MoveLast();    
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            this.dataNavigator1 .MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.dataNavigator1 .MoveNext();    
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.dataNavigator1.Update();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.dataNavigator1.Delete();   
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            this.dataNavigator1.Undo();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
           this.dataNavigator1.AddNew();
        }
    }
}
