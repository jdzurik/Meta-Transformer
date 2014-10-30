namespace GenForm
{
  partial class ExtPropSetUC
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.dgvExtProp = new System.Windows.Forms.DataGridView();
      this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dasExtProp = new System.Windows.Forms.BindingSource(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.dgvExtProp)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dasExtProp)).BeginInit();
      this.SuspendLayout();
      // 
      // dgvExtProp
      // 
      this.dgvExtProp.AutoGenerateColumns = false;
      this.dgvExtProp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvExtProp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
      this.dgvExtProp.DataSource = this.dasExtProp;
      this.dgvExtProp.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvExtProp.Location = new System.Drawing.Point(0, 0);
      this.dgvExtProp.Name = "dgvExtProp";
      this.dgvExtProp.Size = new System.Drawing.Size(363, 446);
      this.dgvExtProp.TabIndex = 1;
      this.dgvExtProp.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExtProp_CellEnter);
      this.dgvExtProp.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExtProp_CellValueChanged);
      this.dgvExtProp.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvExtProp_EditingControlShowing);
      this.dgvExtProp.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExtProp_RowEnter);
      // 
      // nameDataGridViewTextBoxColumn
      // 
      this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
      this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
      this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
      // 
      // valueDataGridViewTextBoxColumn
      // 
      this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
      this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
      this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
      this.valueDataGridViewTextBoxColumn.Width = 200;
      // 
      // dasExtProp
      // 
      this.dasExtProp.DataSource = typeof(Structure.ExtPropSet);
      // 
      // ExtPropSetUC
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.dgvExtProp);
      this.Name = "ExtPropSetUC";
      this.Size = new System.Drawing.Size(363, 446);
      ((System.ComponentModel.ISupportInitialize)(this.dgvExtProp)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dasExtProp)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dgvExtProp;
    private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
    public System.Windows.Forms.BindingSource dasExtProp;
  }
}
