Imports System.Data.SQLite
'imports nested namespaces for SQLite.

Public Class DeleteItmCatg

    'When this form loads, hide all input fields.
    Private Sub DeleteItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CatgNameComboBox.Hide()
        ItemCatgNameTextBox.Hide()
        ItemCatgLabel.Hide()
        ItemCatgNameLabel.Hide()

    End Sub

    Private Sub LoadSpecificCatgItems()

        'Create connection
        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        'create command
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        'create query to show items of the category chosen by the user specifically.
        cmd.CommandText = "SELECT * FROM items WHERE categoryname = @categoryname ORDER BY itemID ASC"
        cmd.Parameters.AddWithValue("@categoryname", CatgNameComboBox.Text)
        cmd.ExecuteNonQuery()

        'create datatable and read into datatable.
        Dim reader As SQLiteDataReader = cmd.ExecuteReader
        Dim dt As New DataTable
        dt.Load(reader)
        reader.Close()
        con.Close()

        'State datasource of datagrid.
        ItemCatgDataGrid.DataSource = dt

        'clear input fields
        ItemCatgNameTextBox.Text = ""

    End Sub

    Private Sub LoadItemCatgTable()

        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        'If the user chooses to delete a category then show "categories" table
        If ItemCatgComboBox.Text = "CATEGORY" Then

            cmd.CommandText = "SELECT * FROM categories ORDER BY categoryID ASC"

            'otherwise show the "items" table
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
        ItemCatgNameTextBox.Text = ""
        CatgNameComboBox.SelectedIndex = -1

    End Sub

    Private Sub ItemCatgComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ItemCatgComboBox.SelectedIndexChanged

        'If the user choose to delete a category show relevant fields and their respective labels, keep all other fields and labels hidden.
        If ItemCatgComboBox.Text = "CATEGORY" Then

            'change label text to highlight the purpose of the input field
            ItemCatgNameLabel.Text = "CATEGORY NAME:"
            ItemCatgLabel.Hide()
            CatgNameComboBox.Hide()

            'If the user choose to delete an item show relevant fields and their respective labels, keep all other fields and labels hidden.
        ElseIf ItemCatgComboBox.Text = "ITEM" Then

            'change label text to highlight the purpose of the input field
            ItemCatgNameLabel.Text = "ITEM NAME:"
            ItemCatgLabel.Show()
            CatgNameComboBox.Show()

            Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
            con.Open()
            'Create data adapter to contact my connection object, and then execute a query.
            Dim da As New SQLiteDataAdapter("SELECT categoryID,categoryname FROM categories ORDER BY categoryID ASC", con)
            Dim dt As New DataTable
            da.Fill(dt)
            CatgNameComboBox.DisplayMember = "categoryname"
            CatgNameComboBox.DataSource = dt
            con.Close()

            'Make the input field have no value selected by default.
            CatgNameComboBox.SelectedIndex = -1

        End If

        'Clear input fields
        ItemCatgNameTextBox.Text = ""

        'Show required fields, and their labels, for user
        ItemCatgNameLabel.Show()
        ItemCatgNameTextBox.Show()

        'Load the "items" or "categories" table, depending on user choice.
        LoadItemCatgTable()

    End Sub

    'When user clicks exit button, shoe managementPage and close this form.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        managementPage.Show()
        Me.Close()

    End Sub

    'When delete button is clicked, Check if relevant input fields are filled in.
    'If input fields are empty then show error message.
    'Otherwise, execute DeleteFromTable()
    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click

        If ItemCatgComboBox.Text = "CATEGORY" Then

            If ItemCatgNameTextBox.Text = "" Then

                MsgBox("Choose a category", MsgBoxStyle.Exclamation, "Error")

            Else

                'A message will be displayed to the user, if they wish to delete a category, to confirm this.
                'This is because all items in that category will also be deleted.
                MsgBox("Deleting a category will delete all items in the category. Are you sure?", MsgBoxStyle.YesNo, "Warning")

                If MsgBoxResult.Yes Then

                    DeleteFromTable()

                End If

            End If

        ElseIf ItemCatgComboBox.Text = "ITEM" Then

            If ItemCatgNameTextBox.Text = "" Or CatgNameComboBox.Text = "" Then

                MsgBox("Choose an item/category", MsgBoxStyle.Exclamation, "Error")

            Else

                DeleteFromTable()

            End If

        Else

            MsgBox("Choose an option from ITEM or CATEGORY", MsgBoxStyle.Exclamation, "Error")

        End If

    End Sub

    'Subroutine to delete either the item from the "item" table or the category from the "category" table.
    Private Sub DeleteFromTable()

        'Create connection to database
        Dim mycon As New SQLiteConnection("Data Source= C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        mycon.Open()

        'Create commands for querying
        Dim mycmd As New SQLiteCommand
        Dim mycmd2 As New SQLiteCommand
        mycmd.Connection = mycon
        mycmd2.Connection = mycon

        'If the user wants to delete a category then get "categoryID" for that category.
        'Delete the category at which has the same "categoryID"
        If ItemCatgComboBox.Text = "CATEGORY" Then

            Dim thisID As Integer = ItemCatgDataGrid.SelectedRows(0).Cells("categoryID").Value
            Dim thisCategory As String = ItemCatgDataGrid.SelectedRows(0).Cells("categoryname").Value
            mycmd.CommandText = "DELETE FROM categories WHERE categoryID = @categoryID"
            mycmd.Parameters.AddWithValue("@categoryID", thisID)

            'A second query is needed to delete all items that were in that category
            mycmd2.CommandText = "DELETE FROM items WHERE categoryname = @categoryname"
            mycmd2.Parameters.AddWithValue("@categoryname", thisCategory)
            mycmd2.ExecuteNonQuery()

        Else

            'A query to delete the item
            Dim thisID2 As Integer = ItemCatgDataGrid.SelectedRows(0).Cells("itemID").Value
            mycmd.CommandText = "DELETE FROM items WHERE itemID = @itemID"

            mycmd.Parameters.AddWithValue("@itemID", thisID2)

        End If

        mycmd.ExecuteNonQuery()
        mycon.Close()

        'Clear input fields
        ItemCatgNameTextBox.Text = ""
        CatgNameComboBox.SelectedIndex = -1

        'Load the "item" or "category" table.
        LoadItemCatgTable()

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

            'If the user wants to delete a category then put the values from the relevant cells of that row into the input fields.
            If ItemCatgComboBox.Text = "CATEGORY" Then

                ItemCatgNameTextBox.Text = selectedRow.Cells(1).Value.ToString()

                'If the user wants to delete an item then put the values from the relevant cells of that row into the input fields.
            Else

                ItemCatgNameTextBox.Text = selectedRow.Cells(1).Value.ToString()

            End If

        End If

    End Sub

    'If the user specifies, when choosing an item to delete, the category the item is in,
    'then load onto the datagrid items of that category specifically by calling LoadSpecificCatgItems.
    Private Sub CatgNameComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CatgNameComboBox.SelectedIndexChanged

        If CatgNameComboBox.Text <> "" Then

            LoadSpecificCatgItems()

        End If

    End Sub

End Class