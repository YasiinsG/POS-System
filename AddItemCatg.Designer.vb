<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddItemCatg
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ValueLabel = New System.Windows.Forms.Label()
        Me.CatgChoiceLabel = New System.Windows.Forms.Label()
        Me.ValueBox = New System.Windows.Forms.TextBox()
        Me.CatgComboBox = New System.Windows.Forms.ComboBox()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.ItemCatgHeader = New System.Windows.Forms.Label()
        Me.ChoiceComboBox = New System.Windows.Forms.ComboBox()
        Me.NameBox = New System.Windows.Forms.TextBox()
        Me.ItemCatgLabel = New System.Windows.Forms.Label()
        Me.ItemCatgGrid = New System.Windows.Forms.DataGridView()
        Me.CatgIDTextBox = New System.Windows.Forms.TextBox()
        CType(Me.ItemCatgGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.SaveButton.Location = New System.Drawing.Point(71, 230)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(4)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(283, 82)
        Me.SaveButton.TabIndex = 0
        Me.SaveButton.Text = "SAVE"
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'ValueLabel
        '
        Me.ValueLabel.AutoSize = True
        Me.ValueLabel.Location = New System.Drawing.Point(31, 132)
        Me.ValueLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ValueLabel.Name = "ValueLabel"
        Me.ValueLabel.Size = New System.Drawing.Size(93, 17)
        Me.ValueLabel.TabIndex = 2
        Me.ValueLabel.Text = "ITEM VALUE:"
        '
        'CatgChoiceLabel
        '
        Me.CatgChoiceLabel.AutoSize = True
        Me.CatgChoiceLabel.Location = New System.Drawing.Point(31, 180)
        Me.CatgChoiceLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.CatgChoiceLabel.Name = "CatgChoiceLabel"
        Me.CatgChoiceLabel.Size = New System.Drawing.Size(125, 17)
        Me.CatgChoiceLabel.TabIndex = 3
        Me.CatgChoiceLabel.Text = "ITEM CATEGORY:"
        '
        'ValueBox
        '
        Me.ValueBox.Location = New System.Drawing.Point(169, 128)
        Me.ValueBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ValueBox.Name = "ValueBox"
        Me.ValueBox.Size = New System.Drawing.Size(201, 22)
        Me.ValueBox.TabIndex = 5
        '
        'CatgComboBox
        '
        Me.CatgComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CatgComboBox.FormattingEnabled = True
        Me.CatgComboBox.Location = New System.Drawing.Point(169, 176)
        Me.CatgComboBox.Margin = New System.Windows.Forms.Padding(4)
        Me.CatgComboBox.Name = "CatgComboBox"
        Me.CatgComboBox.Size = New System.Drawing.Size(201, 24)
        Me.CatgComboBox.TabIndex = 6
        '
        'ExitButton
        '
        Me.ExitButton.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ExitButton.Location = New System.Drawing.Point(71, 320)
        Me.ExitButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(283, 89)
        Me.ExitButton.TabIndex = 7
        Me.ExitButton.Text = "EXIT"
        Me.ExitButton.UseVisualStyleBackColor = False
        '
        'ItemCatgHeader
        '
        Me.ItemCatgHeader.AutoSize = True
        Me.ItemCatgHeader.Location = New System.Drawing.Point(68, 25)
        Me.ItemCatgHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ItemCatgHeader.Name = "ItemCatgHeader"
        Me.ItemCatgHeader.Size = New System.Drawing.Size(237, 17)
        Me.ItemCatgHeader.TabIndex = 23
        Me.ItemCatgHeader.Text = "WHAT WOULD YOU LIKE TO ADD?"
        '
        'ChoiceComboBox
        '
        Me.ChoiceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ChoiceComboBox.FormattingEnabled = True
        Me.ChoiceComboBox.Items.AddRange(New Object() {"CATEGORY", "ITEM"})
        Me.ChoiceComboBox.Location = New System.Drawing.Point(103, 44)
        Me.ChoiceComboBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ChoiceComboBox.Name = "ChoiceComboBox"
        Me.ChoiceComboBox.Size = New System.Drawing.Size(176, 24)
        Me.ChoiceComboBox.Sorted = True
        Me.ChoiceComboBox.TabIndex = 24
        '
        'NameBox
        '
        Me.NameBox.Location = New System.Drawing.Point(169, 84)
        Me.NameBox.Margin = New System.Windows.Forms.Padding(4)
        Me.NameBox.Name = "NameBox"
        Me.NameBox.Size = New System.Drawing.Size(201, 22)
        Me.NameBox.TabIndex = 25
        '
        'ItemCatgLabel
        '
        Me.ItemCatgLabel.AutoSize = True
        Me.ItemCatgLabel.Location = New System.Drawing.Point(31, 87)
        Me.ItemCatgLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ItemCatgLabel.Name = "ItemCatgLabel"
        Me.ItemCatgLabel.Size = New System.Drawing.Size(132, 17)
        Me.ItemCatgLabel.TabIndex = 26
        Me.ItemCatgLabel.Text = "CATEGORY NAME:"
        '
        'ItemCatgGrid
        '
        Me.ItemCatgGrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.ItemCatgGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ItemCatgGrid.Location = New System.Drawing.Point(394, 20)
        Me.ItemCatgGrid.Margin = New System.Windows.Forms.Padding(4)
        Me.ItemCatgGrid.Name = "ItemCatgGrid"
        Me.ItemCatgGrid.RowHeadersWidth = 51
        Me.ItemCatgGrid.Size = New System.Drawing.Size(661, 389)
        Me.ItemCatgGrid.TabIndex = 27
        '
        'CatgIDTextBox
        '
        Me.CatgIDTextBox.Location = New System.Drawing.Point(324, 176)
        Me.CatgIDTextBox.Margin = New System.Windows.Forms.Padding(4)
        Me.CatgIDTextBox.Name = "CatgIDTextBox"
        Me.CatgIDTextBox.ReadOnly = True
        Me.CatgIDTextBox.Size = New System.Drawing.Size(47, 22)
        Me.CatgIDTextBox.TabIndex = 28
        '
        'AddItemCatg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GrayText
        Me.ClientSize = New System.Drawing.Size(1083, 425)
        Me.ControlBox = False
        Me.Controls.Add(Me.CatgComboBox)
        Me.Controls.Add(Me.CatgIDTextBox)
        Me.Controls.Add(Me.ItemCatgGrid)
        Me.Controls.Add(Me.ItemCatgLabel)
        Me.Controls.Add(Me.NameBox)
        Me.Controls.Add(Me.ChoiceComboBox)
        Me.Controls.Add(Me.ItemCatgHeader)
        Me.Controls.Add(Me.ExitButton)
        Me.Controls.Add(Me.ValueBox)
        Me.Controls.Add(Me.CatgChoiceLabel)
        Me.Controls.Add(Me.ValueLabel)
        Me.Controls.Add(Me.SaveButton)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "AddItemCatg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AddItemCatg"
        CType(Me.ItemCatgGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SaveButton As Button
    Friend WithEvents ValueLabel As Label
    Friend WithEvents CatgChoiceLabel As Label
    Friend WithEvents ValueBox As TextBox
    Friend WithEvents CatgComboBox As ComboBox
    Friend WithEvents ExitButton As Button
    Friend WithEvents ItemCatgHeader As Label
    Friend WithEvents ChoiceComboBox As ComboBox
    Friend WithEvents NameBox As TextBox
    Friend WithEvents ItemCatgLabel As Label
    Friend WithEvents ItemCatgGrid As DataGridView
    Friend WithEvents CatgIDTextBox As TextBox
End Class
