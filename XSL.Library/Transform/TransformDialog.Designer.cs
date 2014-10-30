namespace XSL.Library
{
    partial class TransformDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxPromptParams = new System.Windows.Forms.CheckBox();
            this.checkBoxAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.upDownTransparency = new System.Windows.Forms.NumericUpDown();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxXmlInput = new System.Windows.Forms.GroupBox();
            this.textBoxXmlFilename = new System.Windows.Forms.TextBox();
            this.groupBoxXml = new System.Windows.Forms.GroupBox();
            this.webBrowserInput = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openXmlToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.launchXmlToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.reloadXmlToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.transformToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.transformPlainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformHtmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveOutputToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.groupBoxOutputContainer = new System.Windows.Forms.GroupBox();
            this.textBoxXsltOutputFilename = new System.Windows.Forms.TextBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.webBrowserOutput = new System.Windows.Forms.WebBrowser();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownTransparency)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxXmlInput.SuspendLayout();
            this.groupBoxXml.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBoxOutputContainer.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxPromptParams);
            this.panel1.Controls.Add(this.checkBoxAlwaysOnTop);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.upDownTransparency);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 34);
            this.panel1.TabIndex = 0;
            // 
            // checkBoxPromptParams
            // 
            this.checkBoxPromptParams.AutoSize = true;
            this.checkBoxPromptParams.Checked = true;
            this.checkBoxPromptParams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPromptParams.Location = new System.Drawing.Point(250, 12);
            this.checkBoxPromptParams.Name = "checkBoxPromptParams";
            this.checkBoxPromptParams.Size = new System.Drawing.Size(135, 17);
            this.checkBoxPromptParams.TabIndex = 3;
            this.checkBoxPromptParams.Text = "Prompt for XSL Params";
            this.checkBoxPromptParams.UseVisualStyleBackColor = true;
            // 
            // checkBoxAlwaysOnTop
            // 
            this.checkBoxAlwaysOnTop.AutoSize = true;
            this.checkBoxAlwaysOnTop.Location = new System.Drawing.Point(142, 12);
            this.checkBoxAlwaysOnTop.Name = "checkBoxAlwaysOnTop";
            this.checkBoxAlwaysOnTop.Size = new System.Drawing.Size(92, 17);
            this.checkBoxAlwaysOnTop.TabIndex = 2;
            this.checkBoxAlwaysOnTop.Text = "Always on top";
            this.checkBoxAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Transparency";
            // 
            // upDownTransparency
            // 
            this.upDownTransparency.Location = new System.Drawing.Point(84, 9);
            this.upDownTransparency.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.upDownTransparency.Name = "upDownTransparency";
            this.upDownTransparency.Size = new System.Drawing.Size(42, 20);
            this.upDownTransparency.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxXmlInput);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1MinSize = 15;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxOutputContainer);
            this.splitContainer1.Size = new System.Drawing.Size(523, 557);
            this.splitContainer1.SplitterDistance = 229;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBoxXmlInput
            // 
            this.groupBoxXmlInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxXmlInput.Controls.Add(this.textBoxXmlFilename);
            this.groupBoxXmlInput.Controls.Add(this.groupBoxXml);
            this.groupBoxXmlInput.Location = new System.Drawing.Point(3, 28);
            this.groupBoxXmlInput.Name = "groupBoxXmlInput";
            this.groupBoxXmlInput.Size = new System.Drawing.Size(514, 198);
            this.groupBoxXmlInput.TabIndex = 15;
            this.groupBoxXmlInput.TabStop = false;
            this.groupBoxXmlInput.Text = "XML Input (Editable)";
            // 
            // textBoxXmlFilename
            // 
            this.textBoxXmlFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXmlFilename.Location = new System.Drawing.Point(9, 19);
            this.textBoxXmlFilename.Name = "textBoxXmlFilename";
            this.textBoxXmlFilename.ReadOnly = true;
            this.textBoxXmlFilename.Size = new System.Drawing.Size(499, 20);
            this.textBoxXmlFilename.TabIndex = 7;
            // 
            // groupBoxXml
            // 
            this.groupBoxXml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxXml.Controls.Add(this.webBrowserInput);
            this.groupBoxXml.Location = new System.Drawing.Point(9, 45);
            this.groupBoxXml.Name = "groupBoxXml";
            this.groupBoxXml.Size = new System.Drawing.Size(499, 147);
            this.groupBoxXml.TabIndex = 6;
            this.groupBoxXml.TabStop = false;
            // 
            // webBrowserInput
            // 
            this.webBrowserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserInput.Location = new System.Drawing.Point(3, 16);
            this.webBrowserInput.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserInput.Name = "webBrowserInput";
            this.webBrowserInput.Size = new System.Drawing.Size(493, 128);
            this.webBrowserInput.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openXmlToolStripButton,
            this.launchXmlToolStripButton,
            this.reloadXmlToolStripButton,
            this.toolStripSeparator1,
            this.transformToolStripSplitButton,
            this.toolStripSeparator2,
            this.saveOutputToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(523, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openXmlToolStripButton
            // 
            this.openXmlToolStripButton.Image = global::XSL.Library.Properties.Resources.open1;
            this.openXmlToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openXmlToolStripButton.Name = "openXmlToolStripButton";
            this.openXmlToolStripButton.Size = new System.Drawing.Size(75, 22);
            this.openXmlToolStripButton.Text = "Open XML";
            // 
            // launchXmlToolStripButton
            // 
            this.launchXmlToolStripButton.Enabled = false;
            this.launchXmlToolStripButton.Image = global::XSL.Library.Properties.Resources.pen;
            this.launchXmlToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.launchXmlToolStripButton.Name = "launchXmlToolStripButton";
            this.launchXmlToolStripButton.Size = new System.Drawing.Size(111, 22);
            this.launchXmlToolStripButton.Text = "Launch / Edit XML";
            // 
            // reloadXmlToolStripButton
            // 
            this.reloadXmlToolStripButton.Enabled = false;
            this.reloadXmlToolStripButton.Image = global::XSL.Library.Properties.Resources.refresh;
            this.reloadXmlToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadXmlToolStripButton.Name = "reloadXmlToolStripButton";
            this.reloadXmlToolStripButton.Size = new System.Drawing.Size(82, 22);
            this.reloadXmlToolStripButton.Text = "Reload XML";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // transformToolStripSplitButton
            // 
            this.transformToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transformPlainToolStripMenuItem,
            this.transformHtmlToolStripMenuItem,
            this.transformXmlToolStripMenuItem});
            this.transformToolStripSplitButton.Enabled = false;
            this.transformToolStripSplitButton.Image = global::XSL.Library.Properties.Resources.xsl2;
            this.transformToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.transformToolStripSplitButton.Name = "transformToolStripSplitButton";
            this.transformToolStripSplitButton.Size = new System.Drawing.Size(88, 22);
            this.transformToolStripSplitButton.Text = "Transform";
            this.transformToolStripSplitButton.ToolTipText = "Transform (text/plain)";
            // 
            // transformPlainToolStripMenuItem
            // 
            this.transformPlainToolStripMenuItem.Checked = true;
            this.transformPlainToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.transformPlainToolStripMenuItem.Name = "transformPlainToolStripMenuItem";
            this.transformPlainToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.transformPlainToolStripMenuItem.Tag = "0";
            this.transformPlainToolStripMenuItem.Text = "text/plain";
            // 
            // transformHtmlToolStripMenuItem
            // 
            this.transformHtmlToolStripMenuItem.Name = "transformHtmlToolStripMenuItem";
            this.transformHtmlToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.transformHtmlToolStripMenuItem.Tag = "1";
            this.transformHtmlToolStripMenuItem.Text = "text/html";
            // 
            // transformXmlToolStripMenuItem
            // 
            this.transformXmlToolStripMenuItem.Name = "transformXmlToolStripMenuItem";
            this.transformXmlToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.transformXmlToolStripMenuItem.Tag = "2";
            this.transformXmlToolStripMenuItem.Text = "text/xml";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // saveOutputToolStripButton
            // 
            this.saveOutputToolStripButton.Enabled = false;
            this.saveOutputToolStripButton.Image = global::XSL.Library.Properties.Resources.save;
            this.saveOutputToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveOutputToolStripButton.Name = "saveOutputToolStripButton";
            this.saveOutputToolStripButton.Size = new System.Drawing.Size(88, 22);
            this.saveOutputToolStripButton.Text = "Save Output";
            // 
            // groupBoxOutputContainer
            // 
            this.groupBoxOutputContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOutputContainer.Controls.Add(this.textBoxXsltOutputFilename);
            this.groupBoxOutputContainer.Controls.Add(this.groupBoxOutput);
            this.groupBoxOutputContainer.Location = new System.Drawing.Point(3, 0);
            this.groupBoxOutputContainer.Name = "groupBoxOutputContainer";
            this.groupBoxOutputContainer.Size = new System.Drawing.Size(514, 321);
            this.groupBoxOutputContainer.TabIndex = 0;
            this.groupBoxOutputContainer.TabStop = false;
            this.groupBoxOutputContainer.Text = "XSLT Output";
            // 
            // textBoxXsltOutputFilename
            // 
            this.textBoxXsltOutputFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXsltOutputFilename.Location = new System.Drawing.Point(9, 19);
            this.textBoxXsltOutputFilename.Name = "textBoxXsltOutputFilename";
            this.textBoxXsltOutputFilename.ReadOnly = true;
            this.textBoxXsltOutputFilename.Size = new System.Drawing.Size(499, 20);
            this.textBoxXsltOutputFilename.TabIndex = 10;
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOutput.Controls.Add(this.richTextBoxOutput);
            this.groupBoxOutput.Controls.Add(this.webBrowserOutput);
            this.groupBoxOutput.Location = new System.Drawing.Point(9, 40);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(499, 269);
            this.groupBoxOutput.TabIndex = 9;
            this.groupBoxOutput.TabStop = false;
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxOutput.Location = new System.Drawing.Point(3, 16);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.Size = new System.Drawing.Size(493, 250);
            this.richTextBoxOutput.TabIndex = 10;
            this.richTextBoxOutput.Text = "";
            // 
            // webBrowserOutput
            // 
            this.webBrowserOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserOutput.Location = new System.Drawing.Point(3, 16);
            this.webBrowserOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserOutput.Name = "webBrowserOutput";
            this.webBrowserOutput.Size = new System.Drawing.Size(493, 250);
            this.webBrowserOutput.TabIndex = 11;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.Filter = "XML Files (*.xml)|*.xml|XAML Files (*.xaml)|*.xaml|All Files (*.*)|*.*";
            this.openFileDialog.Title = "Select XML Data Source File for Transformation";
            // 
            // TransformDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 591);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "TransformDialog";
            this.Text = "Transform";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownTransparency)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxXmlInput.ResumeLayout(false);
            this.groupBoxXmlInput.PerformLayout();
            this.groupBoxXml.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxOutputContainer.ResumeLayout(false);
            this.groupBoxOutputContainer.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown upDownTransparency;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAlwaysOnTop;
        private System.Windows.Forms.GroupBox groupBoxOutputContainer;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBoxPromptParams;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openXmlToolStripButton;
        private System.Windows.Forms.ToolStripButton launchXmlToolStripButton;
        private System.Windows.Forms.ToolStripButton reloadXmlToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton transformToolStripSplitButton;
        private System.Windows.Forms.ToolStripMenuItem transformPlainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformHtmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformXmlToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxXmlInput;
        private System.Windows.Forms.TextBox textBoxXmlFilename;
        private System.Windows.Forms.GroupBox groupBoxXml;
        private System.Windows.Forms.WebBrowser webBrowserInput;
        private System.Windows.Forms.ToolStripButton saveOutputToolStripButton;
        private System.Windows.Forms.TextBox textBoxXsltOutputFilename;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.WebBrowser webBrowserOutput;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

    }
}