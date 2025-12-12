using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.ColumnMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Wisej.Web;

namespace Wisej3Base
{
    public partial class frmDapper : Form
    {
        private readonly string defaultConnectionString = "Data Source=(local);Initial Catalog=Pubs;Integrated Security=True";
        
        private SqlConnection connection = new SqlConnection();
        
        public frmDapper()
        {
         
            InitializeComponent();
            this.txtConnectionString.Text = this.defaultConnectionString;
            this.dgvTitles.DataSource = this.bindingSource1;
         
        }

        
      

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(this.txtConnectionString.Text);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                connection = CreateConnection();
                connection.Open();
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

        private void btnQueryTitlesDataReader_Click(object sender, EventArgs e)
        {
            LoadTitlesWithQuery();
        }

        private void btnQueryTitlesDataAdapter_Click(object sender, EventArgs e)
        {
            LoadTitlesWithGetAll();
        }

        private void LoadTitlesWithQuery()
        {
            try
            {


                string TitlesQuery = "SELECT title_id, title, type, pub_id, price, advance, royalty, ytd_sales, notes, pubdate " +
                    "FROM titles " +
                    "WHERE title like(@title)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@title",this.txtTitleLike .Text);    

                var titles = connection.Query<Model_Titles>(TitlesQuery,parameters ).ToList();
                
                BindTitles(titles);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento dei titoli: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTitlesWithGetAll()
        {
            try
            {
               
                var titles = connection.GetAll<Model_Titles>().ToList();
                BindTitles(titles);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento dei titoli: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindTitles(IEnumerable<Model_Titles> titles)
        {
            this.bindingSource1.DataSource = new BindingList<Model_Titles>(titles.ToList());
            this.panel1.Enabled = true;
        }

        private void btnUpdateCurrentTitle_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is not Model_Titles current)
            {
                MessageBox.Show("Nessun titolo selezionato.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                current.Notes = (current.Notes ?? string.Empty) + " - Aggiornato il " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

               
                bool updated = connection.Update(current);

                if (updated)
                {
                    MessageBox.Show("Note aggiornate con successo.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.ResetCurrentItem();
                }
                else
                {
                    MessageBox.Show("Nessuna riga aggiornata.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'aggiornamento: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInsertNewTile_Click(object sender, EventArgs e)
        {
            InsertRandomTitle();
        }

        private void InsertRandomTitle()
        {
            try
            {
                var newTitle = CreateRandomTitle();
                bindingSource1.EndEdit();
                AddToBinding(newTitle);
                if (MessageBox.Show(
                        $"Confermi l'inserimento del nuovo titolo con i seguenti dati?\n\n" +
                        $"ID: {newTitle.Title_Id}\nTitolo: {newTitle.Title}\nTipo: {newTitle.Type}\n" +
                        $"Pub_Id: {newTitle.Pub_Id}\nPrezzo: {newTitle.Price:C}\nData Pubblicazione: {newTitle.PubDate:dd/MM/yyyy}",
                        "Conferma inserimento",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    bindingSource1.RemoveCurrent();
                    bindingSource1.CancelEdit();
                    return;
                }

                
                connection .Insert(newTitle);

                MessageBox.Show("Nuovo titolo inserito con successo. ID: " + newTitle.Title_Id, "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'inserimento: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddToBinding(Model_Titles title)
        {
            if (this.bindingSource1.DataSource is BindingList<Model_Titles> list)
            {
                list.Add(title);
                bindingSource1.MoveLast();
            }
        }

        private void btnDeleteCurrentTitle_Click(object sender, EventArgs e)
        {
            DeleteCurrentTitle();
        }

        private void DeleteCurrentTitle()
        {
            if (bindingSource1.Current is not Model_Titles current)
            {
                MessageBox.Show("Nessun titolo selezionato.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                $"Sei sicuro di voler eliminare il titolo:\n\nID: {current.Title_Id}\nTitolo: {current.Title}\n\nQuesta operazione non può essere annullata.",
                "Conferma eliminazione",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                connection = CreateConnection();
                connection.Open();
                bool deleted = connection.Delete(current);

                if (deleted)
                {
                    bindingSource1.RemoveCurrent();
                    MessageBox.Show("Titolo eliminato con successo.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nessuna riga eliminata.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'eliminazione: " + ex.Message + "\n\nNota: potrebbero esistere vincoli referenziali.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Model_Titles CreateRandomTitle()
        {
            var titleId = GenerateRandomTitleId();
            var random = new Random();
            string[] types = { "business", "mod_cook", "popular_comp", "psychology", "trad_cook", "UNDECIDED" };
            string[] pubIds = { "0736", "0877", "1389", "1622", "1756", "9901", "9952", "9999" };

            return new Model_Titles
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

        private string GenerateRandomTitleId(int maxLength = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, maxLength).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is not Model_Titles current)
            {
                MessageBox.Show("Nessun titolo selezionato.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                current .Notes = (current.Notes ?? string.Empty) + " - Aggiornato il " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                bindingSource1.EndEdit();
                using var connection = CreateConnection();
                connection.Open();

                bool updated = connection.Update(current);

                if (updated)
                {
                    MessageBox.Show("Record aggiornato con successo nel database.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bindingSource1.ResetCurrentItem();
                }
                else
                {
                    MessageBox.Show("Nessuna modifica da salvare.", "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'aggiornamento: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            InsertRandomTitle();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            bindingSource1.ResetCurrentItem();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCurrentTitle();
        }

        private void btnFirst_Click_1(object sender, EventArgs e)
        {
            bindingSource1 .MoveFirst();    
        }
    }
}
