namespace Wisej3Base
{
    partial class frmPasseroFramework
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Wisej.NET Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtConnectionString = new Wisej.Web.TextBox();
            this.btnConnect = new Wisej.Web.Button();
            this.btnQueryTitlesGetAllItems = new Wisej.Web.Button();
            this.btnQueryTitlesGetItems = new Wisej.Web.Button();
            this.dataNavigator1 = new Passero.Framework.Controls.DataNavigator();
            this.panel1 = new Wisej.Web.Panel();
            this.btnNew = new Wisej.Web.Button();
            this.btnUndo = new Wisej.Web.Button();
            this.btnDelete = new Wisej.Web.Button();
            this.txtTitles_TitleId = new Wisej.Web.TextBox();
            this.btnUpdate = new Wisej.Web.Button();
            this.txtTitles_Title = new Wisej.Web.TextBox();
            this.btnLast = new Wisej.Web.Button();
            this.txtTitles_Notes = new Wisej.Web.TextBox();
            this.btnNext = new Wisej.Web.Button();
            this.btnFirst = new Wisej.Web.Button();
            this.btnPrev = new Wisej.Web.Button();
            this.txtTitleLike = new Wisej.Web.TextBox();
            this.bindingSource1 = new Wisej.Web.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.LabelText = "ConnectionString";
            this.txtConnectionString.Location = new System.Drawing.Point(7, 3);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(568, 53);
            this.txtConnectionString.TabIndex = 21;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(581, 27);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(74, 29);
            this.btnConnect.TabIndex = 20;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnQueryTitlesGetAllItems
            // 
            this.btnQueryTitlesGetAllItems.Location = new System.Drawing.Point(471, 62);
            this.btnQueryTitlesGetAllItems.Name = "btnQueryTitlesGetAllItems";
            this.btnQueryTitlesGetAllItems.Size = new System.Drawing.Size(184, 29);
            this.btnQueryTitlesGetAllItems.TabIndex = 24;
            this.btnQueryTitlesGetAllItems.Text = "Query Titles with GetAllItems";
            this.btnQueryTitlesGetAllItems.Click += new System.EventHandler(this.btnQueryTitlesDataGetAllItems_Click);
            // 
            // btnQueryTitlesGetItems
            // 
            this.btnQueryTitlesGetItems.Location = new System.Drawing.Point(7, 62);
            this.btnQueryTitlesGetItems.Name = "btnQueryTitlesGetItems";
            this.btnQueryTitlesGetItems.Size = new System.Drawing.Size(184, 29);
            this.btnQueryTitlesGetItems.TabIndex = 23;
            this.btnQueryTitlesGetItems.Text = "Query Titles with GetItems";
            this.btnQueryTitlesGetItems.Click += new System.EventHandler(this.btnQueryTitlesGetItems_Click);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Dock = Wisej.Web.DockStyle.Bottom;
            this.dataNavigator1.Location = new System.Drawing.Point(0, 370);
            this.dataNavigator1.Name = "dataNavigator1";
            this.dataNavigator1.Size = new System.Drawing.Size(754, 62);
            this.dataNavigator1.TabIndex = 25;
            this.dataNavigator1.eAddNewRequest += new Passero.Framework.Controls.DataNavigator.eAddNewRequestEventHandler(this.dataNavigator1_eAddNewRequest);
            this.dataNavigator1.eAfterAddNewRequest += new Passero.Framework.Controls.DataNavigator.eAfterAddNewEventHandler(this.dataNavigator1_eAfterAddNewRequest);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = Wisej.Web.BorderStyle.Solid;
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnUndo);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.txtTitles_TitleId);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.txtTitles_Title);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.txtTitles_Notes);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnFirst);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(7, 97);
            this.panel1.Name = "panel1";
            this.panel1.ShowHeader = true;
            this.panel1.Size = new System.Drawing.Size(679, 264);
            this.panel1.TabIndex = 26;
            this.panel1.Text = "BindingSource";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(423, 195);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(53, 37);
            this.btnNew.TabIndex = 18;
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(364, 195);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(53, 37);
            this.btnUndo.TabIndex = 17;
            this.btnUndo.Text = "Undo";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(305, 195);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(53, 37);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtTitles_TitleId
            // 
            this.txtTitles_TitleId.DataBindings.Add(new Wisej.Web.Binding("Text", this.bindingSource1, "Title_Id", true, Wisej.Web.DataSourceUpdateMode.OnValidation, null, ""));
            this.txtTitles_TitleId.LabelText = "Title Id";
            this.txtTitles_TitleId.Location = new System.Drawing.Point(3, 12);
            this.txtTitles_TitleId.Name = "txtTitles_TitleId";
            this.txtTitles_TitleId.Size = new System.Drawing.Size(100, 53);
            this.txtTitles_TitleId.TabIndex = 8;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(246, 195);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(53, 37);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtTitles_Title
            // 
            this.txtTitles_Title.DataBindings.Add(new Wisej.Web.Binding("Text", this.bindingSource1, "Title", true, Wisej.Web.DataSourceUpdateMode.OnValidation, null, ""));
            this.txtTitles_Title.LabelText = "Title";
            this.txtTitles_Title.Location = new System.Drawing.Point(109, 12);
            this.txtTitles_Title.Name = "txtTitles_Title";
            this.txtTitles_Title.Size = new System.Drawing.Size(462, 53);
            this.txtTitles_Title.TabIndex = 9;
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(185, 195);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(53, 37);
            this.btnLast.TabIndex = 14;
            this.btnLast.Text = "Last";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // txtTitles_Notes
            // 
            this.txtTitles_Notes.DataBindings.Add(new Wisej.Web.Binding("Text", this.bindingSource1, "Notes", true, Wisej.Web.DataSourceUpdateMode.OnValidation, null, ""));
            this.txtTitles_Notes.LabelText = "Notes";
            this.txtTitles_Notes.Location = new System.Drawing.Point(3, 71);
            this.txtTitles_Notes.Multiline = true;
            this.txtTitles_Notes.Name = "txtTitles_Notes";
            this.txtTitles_Notes.Size = new System.Drawing.Size(568, 118);
            this.txtTitles_Notes.TabIndex = 10;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(124, 195);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(53, 37);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(3, 195);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(55, 37);
            this.btnFirst.TabIndex = 11;
            this.btnFirst.Text = "First";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(64, 195);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(52, 37);
            this.btnPrev.TabIndex = 12;
            this.btnPrev.Text = "Prev";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // txtTitleLike
            // 
            this.txtTitleLike.Label.Position = Wisej.Web.LabelPosition.Left;
            this.txtTitleLike.LabelText = "Title Like ";
            this.txtTitleLike.Location = new System.Drawing.Point(197, 61);
            this.txtTitleLike.Name = "txtTitleLike";
            this.txtTitleLike.Size = new System.Drawing.Size(243, 30);
            this.txtTitleLike.TabIndex = 27;
            this.txtTitleLike.Text = "%";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(Wisej3Base.PasseroModel_Titles);
            // 
            // frmPasseroFramework
            // 
            this.ClientSize = new System.Drawing.Size(754, 432);
            this.Controls.Add(this.txtTitleLike);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataNavigator1);
            this.Controls.Add(this.btnQueryTitlesGetAllItems);
            this.Controls.Add(this.btnQueryTitlesGetItems);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.btnConnect);
            this.Name = "frmPasseroFramework";
            this.Text = "Passero.Framework";
            this.Load += new System.EventHandler(this.frmPasseroFramework_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Wisej.Web.TextBox txtConnectionString;
        private Wisej.Web.Button btnConnect;
        private Wisej.Web.Button btnQueryTitlesGetAllItems;
        private Wisej.Web.Button btnQueryTitlesGetItems;
        private Passero.Framework.Controls.DataNavigator dataNavigator1;
        private Wisej.Web.Panel panel1;
        private Wisej.Web.Button btnNew;
        private Wisej.Web.Button btnUndo;
        private Wisej.Web.Button btnDelete;
        private Wisej.Web.TextBox txtTitles_TitleId;
        private Wisej.Web.Button btnUpdate;
        private Wisej.Web.TextBox txtTitles_Title;
        private Wisej.Web.Button btnLast;
        private Wisej.Web.TextBox txtTitles_Notes;
        private Wisej.Web.Button btnNext;
        private Wisej.Web.Button btnFirst;
        private Wisej.Web.Button btnPrev;
        private Wisej.Web.BindingSource bindingSource1;
        private Wisej.Web.TextBox txtTitleLike;
    }
}