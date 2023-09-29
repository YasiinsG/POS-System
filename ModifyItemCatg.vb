Imports System.Data.SQLite
'import nested namespaces for SQLite.

Public Class ModifyItemCatg

    'If exit button is selected, show managementPage and close this form.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        managementPage.Show()
        Me.Close()

    End Sub

    'When form loads, hide all input fields and their labels.
    Private Sub ModifyItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        NewCatgIDTextBox.Hide()
        NewItemCatgNameLabel.Hide()
        NewItemValueLabel.Hide()
        NewItemCategoryLabel.Hide()
        ItemCatgNameLabel.Hide()
        NameTextBox.Hide()
        NewCatgComboBox.Hide()
        NewNameTextBox.Hide()
        NewValueTextBox.Hide()
        CatgNameLabel.Hide()
        CatgComboBox.Hide()

    End Sub

    Private Sub ChoiceComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChoiceComboBox.SelectedIndexChanged
        'If the user choose to modify a category show relevant fields and their respective labels, keep all other fields and labels hidden.
        If ChoiceComboBox.Text = "CATEGORY" Then

            'change label text to highlight the purpose of the input field
            ItemCatgNameLabel.Text = "CATEGORY NAME:"
            NewItemCatgNameLabel.Text = "NEW CATEGORY NAME:"

            NewItemValueLabel.Hide()
            NewItemCategoryLabel.Hide()
            NewCatgComboBox.Hide()
            NewValueTextBox.Hide()
            CatgNameLabel.Hide()
            CatgComboBox.Hide()

            'If the user choose to modify an item show relevant fields and their respective labels, keep all other fields and labels hidden.
        ElseIf ChoiceComboBox.Text = "ITEM" Then

            'change label text to highlight the purpose of the input field
            ItemCatgNameLabel.Text = "ITEM NAME:"
            NewItemCatgNameLabel.Text = "NEW ITEM NAME:"

            NewItemValueLabel.Show()
            NewItemCategoryLabel.Show()
            NewCatgComboBox.Show()
            NewValueTextBox.Show()
            CatgNameLabel.Show()
            CatgComboBox.Show()

            'create database connection and query to fill datatable
            Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
            con.Open()
            Dim da As New SQLiteDataAdapter("SELECT categoryID,categoryname FROM categories ORDER BY categoryID ASC", con)
            Dim dt As New DataTable
            da.Fill(dt)

            'State what will be displayed from the query result in the list of categories.
            CatgComboBox.DisplayMember = "categoryname"
            CatgComboBox.ValueMember = "categoryID"

            'State the source of the query result
            CatgComboBox.DataSource = dt

            NewCatgComboBox.DisplayMember = "categoryname"
            NewCatgComboBox.ValueMember = "categoryID"

            'Use the same datasource for the new categories' list.
            'New DataView allows two controls to be bound to the same datatable but each control is independent of one-another.
            NewCatgComboBox.DataSource = New DataView(dt)

            con.Close()

            'Make the comboboxes have no value visible and selected as default.
            CatgComboBox.SelectedIndex = -1
            NewCatgComboBox.SelectedIndex = -1

        End If

        'Clear all input fields so another query can be executed if necessary.

        NameTextBox.Text = ""
        NewNameTextBox.Text = ""
        NewValueTextBox.Text = ""

        NewItemCatgNameLabel.Show()
        NewNameTextBox.Show()
        ItemCatgNameLabel.Show()
        NameTextBox.Show()

        LoadItemCatgTable()

    End Sub

    'Subroutine to load and show on the datagrid, dependent on user choice, the "categories" table or the "items" table.
    Private Sub LoadItemCatgTable()

        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        'If the user chooses to modify a category then show the "categories" table.
        If ChoiceComboBox.Text = "CATEGORY" Then

            cmd.CommandText = "SELECT * FROM categories ORDER BY categoryID ASC"

            'If user chooses to modify an item then show the "items" table.
        Else

            cmd.CommandText = "SELECT * FROM items ORDER BY itemID ASC"

        End If

        Dim reader As SQLiteDataReader = cmd.ExecuteReader
        Dim dt As New DataTable
        dt.Load(reader)
        reader.Close()
        con.Close()

        ItemCatgDataGrid.DataSource = dt

        'Clear input fields.
        CatgComboBox.SelectedIndex = -1
        NewCatgComboBox.SelectedIndex = -1
        NameTextBox.Text = ""
        NewNameTextBox.Text = ""
        NewValueTextBox.Text = ""

    End Sub

    'If the user then inputs a specific category to choose an item from,
    'then only items of that category are displayed in the datatable.
    Private Sub LoadSpecificCatgItems()

        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        'Create query where only items with the same categoryname as the one chosen by the user in CatgComboBox are shown.
        cmd.CommandText = "SELECT * FROM items WHERE categoryname = @categoryname ORDER BY itemID ASC"
        cmd.Parameters.AddWithValue("@categoryname", CatgComboBox.Text)
        cmd.ExecuteNonQuery()

        Dim reader As SQLiteDataReader = cmd.ExecuteReader
        Dim dt As New DataTable
        dt.Load(reader)
        reader.Close()
        con.Close()

        ItemCatgDataGrid.DataSource = dt

        'Clear input fields.
        NewCatgComboBox.SelectedIndex = -1
        NameTextBox.Text = ""
        NewNameTextBox.Text = ""
        NewValueTextBox.Text = ""

    End Sub

    'When CatgComboBox is not blank (a category has been chosen) then show items of that category specifically
    'This is done by calling LoadSpecificCatgItems()
    Private Sub CatgComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CatgComboBox.SelectedIndexChanged

        If CatgComboBox.Text <> "" Then

            LoadSpecificCatgItems()

        End If

    End Sub

    'When a cell is selected manually by the user in the datagrid
    'then fill the corresponding input field with the item's or category's row values.
    Private Sub ItemCatgDataGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ItemCatgDataGrid.CellClick

        'Create a variable which holds the index of the row selected in the datatable.
        Dim index As Integer
        index = e.RowIndex

        'If the row is not a row which holds values of the database table then show an error message.
        If index < 0 Then

            MsgBox("Select a cell!", MsgBoxStyle.Exclamation)

        Else

            'Create variable to hold all values of row chosen.
            Dim selectedRow As DataGridViewRow
            selectedRow = ItemCatgDataGrid.Rows(index)

            'If the user wants to modify a category then put the values from the relevant cells of that row into the input fields.
            If ChoiceComboBox.Text = "CATEGORY" Then

                NameTextBox.Text = selectedRow.Cells(1).Value.ToString()
                NewNameTextBox.Text = selectedRow.Cells(1).Value.ToString()

            Else

                'If the user wants to modify an item then put the values from the relevant cells of that row into the input fields.
                NameTextBox.Text = selectedRow.Cells(1).Value.ToString()
                NewNameTextBox.Text = selectedRow.Cells(1).Value.ToString()
                NewValueTextBox.Text = selectedRow.Cells(2).Value.ToString()
                NewCatgComboBox.Text = selectedRow.Cells(3).Value.ToString()

            End If

        End If

    End Sub

    'If the user chooses to save their modification, if they have not chosen between item or category then give an error message.
    'If they have chosen an item/category then call the subroutine ModifyTable()
    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        If ChoiceComboBox.Text = "" Then

            MsgBox("Choose an item/category", MsgBoxStyle.Exclamation, "Error")

        ElseIf ChoiceComboBox.Text <> "" Then

            ModifyTable()

        Else

            MsgBox("Choose an option from ITEM or CATEGORY", MsgBoxStyle.Exclamation, "Error")

        End If

    End Sub

    'Subroutine used to make changes to the database depending on selection of either category or item.
    Private Sub ModifyTable()

        Dim mycon As New SQLiteConnection("Data Source= C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        mycon.Open()

        'create two commands, each with a query,
        'as both the "items" table and the "category" table would have to be updated if "CATEGORY" is chosen.
        Dim mycmd As New SQLiteCommand
        Dim mycmd2 As New SQLiteCommand
        mycmd.Connection = mycon
        mycmd2.Connection = mycon

        If ChoiceComboBox.Text = "CATEGORY" Then

            'variable to get the "categoryID" column value of the row chosen.
            Dim thiscategoryID As Integer = ItemCatgDataGrid.SelectedRows(0).Cells("categoryID").Value

            mycmd.CommandText = "UPDATE categories SET categoryname = @categoryname WHERE categoryID = @categoryID"
            mycmd.Parameters.AddWithValue("@categoryname", NewNameTextBox.Text)
            mycmd.Parameters.AddWithValue("@categoryID", thiscategoryID)

            mycmd2.CommandText = "UPDATE items SET categoryname = @categoryname WHERE categoryID = @categoryID"
            mycmd2.Parameters.AddWithValue("@categoryname", NewNameTextBox.Text)
            mycmd2.Parameters.AddWithValue("@categoryID", thiscategoryID)

            mycmd2.ExecuteNonQuery()

        Else

            Dim thisitemID2 As Integer = ItemCatgDataGrid.SelectedRows(0).Cells("itemID").Value
            Dim catgID As Integer = NewCatgIDTextBox.Text
            mycmd.CommandText = "UPDATE items SET itemname = @itemname, itemvalue = @itemvalue,
                                categoryname = @categoryname, categoryID = @categoryID WHERE itemID = @itemID"

            mycmd.Parameters.AddWithValue("@itemname", NewNameTextBox.Text)
            mycmd.Parameters.AddWithValue("@itemvalue", NewValueTextBox.Text)
            mycmd.Parameters.AddWithValue("@categoryname", NewCatgComboBox.Text)
            mycmd.Parameters.AddWithValue("@itemID", thisitemID2)
            mycmd.Parameters.AddWithValue("@categoryID", catgID)

        End If

        mycmd.ExecuteNonQuery()
        mycon.Close()

        'Clear input fields.
        CatgComboBox.SelectedIndex = -1
        NewCatgComboBox.SelectedIndex = -1
        NameTextBox.Text = ""
        NewNameTextBox.Text = ""
        NewValueTextBox.Text = ""
        NewCatgIDTextBox.Text = ""

        'Show updated table.
        LoadItemCatgTable()

    End Sub

    Private Sub NewCatgComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles NewCatgComboBox.SelectedIndexChanged

        'If the user has chosen a new category for the item then get the categoryID of that category.
        If NewCatgComboBox.Text <> "" Then

            'Establish connection to database.
            Dim con1 As String = "Data Source= C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db"

            Using con As SQLiteConnection = New SQLiteConnection(con1)

                'Create command for querying.
                'Command will get categoryID of the category chosen.
                Using cmd As SQLiteCommand = New SQLiteCommand("SELECT categoryID FROM categories WHERE categoryname = '" & NewCatgComboBox.Text & "'")
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con
                    con.Open()

                    'create data reader and read the result of the query.
                    Using sdr As SQLiteDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        'NewCatgIDTextBox is a hidden textbox which will hold the 'categoryID' value that is returned
                        NewCatgIDTextBox.Text = sdr("categoryID").ToString()
                    End Using

                    'close connection.
                    con.Close()

                End Using

            End Using

        End If

    End Sub

End Class