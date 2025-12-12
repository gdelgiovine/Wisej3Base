using System;
using System.Data;
using System.Data.SqlClient;
using Wisej.Web;

namespace Wisej3Base
{
    public partial class frmADONET : Form
    {
        string defaultconnectionString = "Data Source=(local);Initial Catalog=Pubs;Integrated Security=True";    
        SqlConnection connection = new SqlConnection();
        private Random random = new Random();
        public frmADONET()
        {
            InitializeComponent();
            this.txtConnectionString.Text = this.defaultconnectionString;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.ConnectionString = this.txtConnectionString.Text ;
                connection.Open();
                this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
                this.btnConnect .Text = "Connected";
                

            }
            else
            {
                this.btnConnect.BackColor = System.Drawing.Color.LightCoral;
                this.connection.Close();    
                this.btnConnect.Text = "Connect";   
            }   
        }


        private void LoadTitlesWithDataReader()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Connessione non aperta. Connettersi prima di eseguire la query.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "SELECT title_id, title, type, pub_id, price, advance, royalty, ytd_sales, notes, pubdate FROM titles";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                reader.Close();

               

                this.dgvTitles.DataSource = dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Errore durante l'esecuzione della query: " + ex.Message, "Errore SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore generico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTitlesWithDataSetSQLQuery()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Connessione non aperta. Connettersi prima di eseguire la query.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "SELECT title_id, title, type, pub_id, price, advance, royalty, ytd_sales, notes, pubdate " +
                    "FROM titles " +
                    "WHERE title LIKE @title";

                using SqlCommand command = new SqlCommand(query, connection);
                string titleFilter = string.IsNullOrWhiteSpace(this.txtTitleLike.Text) ? "%" : this.txtTitleLike.Text;
                command.Parameters.AddWithValue("@title", titleFilter);

                using SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet titlesDataSet = new DataSet();
                adapter.Fill(titlesDataSet, "titles");

                

                this.bindingSource1 .DataSource = titlesDataSet.Tables["titles"];
                this.dgvTitles.DataSource = this.bindingSource1.DataSource;
                this.bindingSource1.MoveFirst();
                this.panel1.Enabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Errore durante l'esecuzione della query: " + ex.Message, "Errore SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore generico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadTitlesWithDataAdapter()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Connessione non aperta. Connettersi prima di eseguire la query.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Usa il TableAdapter per caricare i dati nel pubsDataSet
                this.titlesTableAdapter.Fill(this.pubsDataSet.titles);

                this.dgvTitles.DataSource = this.bindingSource1;
                this.bindingSource1.MoveFirst();
                this.panel1.Enabled = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Errore durante l'esecuzione della query: " + ex.Message, "Errore SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore generico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSelectedTitleNotes()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Connessione non aperta. Connettersi prima di eseguire l'aggiornamento.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.dgvTitles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selezionare un titolo dalla griglia.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = this.dgvTitles.SelectedRows[0];
                string titleId = selectedRow.Cells["title_id"].Value.ToString();
                string currentNotes = selectedRow.Cells["notes"].Value?.ToString() ?? string.Empty;

                string updatedNotes = currentNotes + " - Aggiornato il " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                string updateQuery = "UPDATE titles SET notes = @notes WHERE title_id = @title_id";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@notes", updatedNotes);
                command.Parameters.AddWithValue("@title_id", titleId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Note aggiornate con successo.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ricarica i dati per visualizzare l'aggiornamento
                    if (this.dgvTitles.DataSource != null)
                    {
                        LoadTitlesWithDataAdapter();
                    }
                }
                else
                {
                    MessageBox.Show("Nessuna riga aggiornata.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Errore durante l'aggiornamento: " + ex.Message, "Errore SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore generico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void InsertRandomTitle()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Connessione non aperta. Connettersi prima di eseguire l'inserimento.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Genera un title_id casuale di 6 caratteri alfanumerici
                string titleId = GenerateRandomTitleId();

                // Tipi disponibili nel database Pubs
                string[] types = { "business", "mod_cook", "popular_comp", "psychology", "trad_cook", "UNDECIDED" };
                string type = types[random.Next(types.Length)];

                // Pub_id validi (esistenti nella tabella publishers)
                string[] pubIds = { "0736", "0877", "1389", "1622", "1756", "9901", "9952", "9999" };
                string pubId = pubIds[random.Next(pubIds.Length)];

                // Genera valori numerici casuali
                decimal price = Math.Round((decimal)(random.NextDouble() * 50 + 5), 2); // tra 5 e 55
                decimal advance = Math.Round((decimal)(random.NextDouble() * 10000), 2); // tra 0 e 10000
                int royalty = random.Next(10, 25); // tra 10 e 24
                int ytdSales = random.Next(0, 50000); // tra 0 e 50000

                // Genera titolo e note casuali
                string title = "Titolo Casuale " + titleId;
                string notes = "Inserito automaticamente il " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                DateTime pubDate = DateTime.Now.AddDays(-random.Next(0, 3650)); // data casuale negli ultimi 10 anni

                string insertQuery = @"INSERT INTO titles (title_id, title, type, pub_id, price, advance, royalty, ytd_sales, notes, pubdate) 
                                      VALUES (@title_id, @title, @type, @pub_id, @price, @advance, @royalty, @ytd_sales, @notes, @pubdate)";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@title_id", titleId);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@pub_id", pubId);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@advance", advance);
                command.Parameters.AddWithValue("@royalty", royalty);
                command.Parameters.AddWithValue("@ytd_sales", ytdSales);
                command.Parameters.AddWithValue("@notes", notes);
                command.Parameters.AddWithValue("@pubdate", pubDate);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Nuovo titolo inserito con successo. ID: " + titleId, "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ricarica i dati per visualizzare il nuovo record
                    if (this.dgvTitles.DataSource != null)
                    {
                        LoadTitlesWithDataAdapter();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Errore durante l'inserimento: " + ex.Message, "Errore SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore generico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedTitle()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Connessione non aperta. Connettersi prima di eseguire l'eliminazione.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.dgvTitles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selezionare un titolo dalla griglia.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = this.dgvTitles.SelectedRows[0];
                string titleId = selectedRow.Cells["title_id"].Value.ToString();
                string titleName = selectedRow.Cells["title"].Value?.ToString() ?? "N/A";

                // Conferma eliminazione
                DialogResult result = MessageBox.Show(
                    "Sei sicuro di voler eliminare il titolo:\n\n" +
                    "ID: " + titleId + "\n" +
                    "Titolo: " + titleName + "\n\n" +
                    "Questa operazione non può essere annullata.",
                    "Conferma eliminazione",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                string deleteQuery = "DELETE FROM titles WHERE title_id = @title_id";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@title_id", titleId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Titolo eliminato con successo.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ricarica i dati per aggiornare la griglia
                    if (this.dgvTitles.DataSource != null)
                    {
                        LoadTitlesWithDataAdapter();
                    }
                }
                else
                {
                    MessageBox.Show("Nessuna riga eliminata.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Errore durante l'eliminazione: " + ex.Message + "\n\nNota: Potrebbero esistere vincoli di integrità referenziale.", "Errore SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore generico: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateRandomTitleId(int maxlenght=6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] titleId = new char[maxlenght];

            for (int i = 0; i < maxlenght; i++)
            {
                titleId[i] = chars[random.Next(chars.Length)];
            }

            return new string(titleId);
        }
        private void btnQueryTitlesDataReader_Click(object sender, EventArgs e)
        {
            this.panel1.Enabled = false;
            LoadTitlesWithDataReader();
            
        }

        private void btnQueryTitlesDataAdapter_Click(object sender, EventArgs e)
        {
            LoadTitlesWithDataAdapter ();
            
        }

        private void btnUpdateCurrentTitle_Click(object sender, EventArgs e)
        {
            UpdateSelectedTitleNotes();
        }

        private void btnInsertNewTile_Click(object sender, EventArgs e)
        {
            InsertRandomTitle();
        }

        private void btnDeleteCurrentTitle_Click(object sender, EventArgs e)
        {
            DeleteSelectedTitle (); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
            try
            {
                // Termina l'editing nel BindingSource per applicare le modifiche
                bindingSource1.EndEdit();
                
                // Aggiorna il database usando il TableAdapter
                int rowsAffected = this.titlesTableAdapter.Update(this.pubsDataSet.titles);
                
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record aggiornato con successo nel database. Righe modificate: " + rowsAffected, "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Accetta le modifiche nel DataSet
                    this.pubsDataSet.titles.AcceptChanges();
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

       private void panel1_PanelCollapsed(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current == null)
            {
                MessageBox.Show("Nessun titolo selezionato.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRowView currentRow = (DataRowView)bindingSource1.Current;
            string titleId = currentRow["title_id"]?.ToString() ?? "<sconosciuto>";
            string titleName = currentRow["title"]?.ToString() ?? "N/D";

            DialogResult result = MessageBox.Show(
                $"Sei sicuro di voler eliminare il titolo:\n\nID: {titleId}\nTitolo: {titleName}\n\nQuesta operazione non può essere annullata.",
                "Conferma eliminazione",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bindingSource1.RemoveCurrent();
                int rowsAffected = this.titlesTableAdapter.Update(this.pubsDataSet.titles);

                if (rowsAffected > 0)
                {
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

        private void btnUndo_Click(object sender, EventArgs e)
        {
            bindingSource1.CancelEdit();
            if (bindingSource1.Current is DataRowView row)
            {
                row.Row.RejectChanges();
                bindingSource1.ResetCurrentItem();
            }
        }


        private void InsertViaBindingSource()
        {
            var newRow = (DataRowView)bindingSource1.AddNew();
            newRow["title_id"] = GenerateRandomTitleId();
            newRow["title"] = "Titolo generato " + newRow["title_id"];
            newRow["type"] = "UNDECIDED";
            newRow["pub_id"] = "0736";
            newRow["price"] = 9.99m;
            newRow["advance"] = 0m;
            newRow["royalty"] = 10;
            newRow["ytd_sales"] = 0;
            newRow["notes"] = "Inserito il " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            newRow["pubdate"] = DateTime.Now;
            bindingSource1.EndEdit();


            if (MessageBox .Show ("Salvare il nuovo titolo nel database?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                bindingSource1.RemoveCurrent();
                bindingSource1.CancelEdit();
                return;
            }   
            int rows = titlesTableAdapter.Update(pubsDataSet.titles);
            if (rows > 0)
                MessageBox.Show("Titolo inserito via BindingSource.", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            InsertViaBindingSource();
        }

        private void btmQueryTitlesSQLQuery_Click(object sender, EventArgs e)
        {
            LoadTitlesWithDataSetSQLQuery();
        }
    }
    
}
