Imports System.Data.SQLite
'imports nested namespace for SQLite.

Public Class StockManagement

    'When the exit button is clicked, show the managementPage and close this form.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        managementPage.Show()
        Me.Close()
    End Sub

    'Call the subroutine LoadUserTable().
    Private Sub StockManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadTable()

    End Sub
    Private Sub LoadTable()

        'create connection to database
        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        'create command to be used to search database and get itemID, itemname and stock from the table items.
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        cmd.CommandText = "SELECT itemID,itemname,stock FROM items ORDER BY stock ASC"

        'create a datareader to read the values from the command into a datatable
        Dim reader As SQLiteDataReader = cmd.ExecuteReader
        Dim dt As New DataTable
        dt.Load(reader)

        'close reader and connection.
        reader.Close()
        con.Close()

        'Make the datasource of the datagrid the datatable so database query result is shown.
        StockDataGrid.DataSource = dt

        'Create variable to hold the datagrid table as a datagridview.
        Dim colouredDT As DataGridView = StockDataGrid
        'The table is in an ascending order so the item with the highest stock is the last row.
        'Store the 'stock' value of the last row in a variable.
        Dim highestStock As Integer = colouredDT.Rows(colouredDT.RowCount - 1).Cells(2).Value

        'Iterate through the table to go through each row.
        For x As Integer = 0 To colouredDT.Rows.Count - 1

            'If the stock value of that row is 1/3rd of the value of highestStock or lower
            'then colour the background of that cell red (To indicate low stock level).
            If colouredDT.Rows(x).Cells(2).Value <= (1 / 3) * highestStock Then

                colouredDT.Rows(x).Cells(2).Style.BackColor = Color.Red

                'If the stock value of that row is 2/3rd of the value of highestStock but higher than 1/3rd
                'then colour the background of that cell orange (To indicate moderate stock level).
            ElseIf colouredDT.Rows(x).Cells(2).Value <= (2 / 3) * highestStock And
                colouredDT.Rows(x).Cells(2).Value > (1 / 3) * highestStock Then

                colouredDT.Rows(x).Cells(2).Style.BackColor = Color.Orange

            Else
                'Otherwise colour the background green (To indicate high stock level).
                colouredDT.Rows(x).Cells(2).Style.BackColor = Color.Green


            End If

        Next

        'For each column in the table, make it so that the cells in the column cannot be reordered.
        For Each DataGridViewColumn In StockDataGrid.Columns

            DataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable

        Next

    End Sub

    'When a cell is selected manually by the user in the datagrid
    'then fill the corresponding input field with the item's row values.
    Private Sub StockDataGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles StockDataGrid.CellClick

        'Create a variable which holds the index of the row selected in the datatable.
        Dim index As Integer
        index = e.RowIndex

        'If the row is not a row which holds values of the database then show an error message.
        If index < 0 Then
            MsgBox("Select a cell!", MsgBoxStyle.Exclamation)

        Else

            'Create variable to hold all values of row chosen.
            Dim selectedRow As DataGridViewRow
            selectedRow = StockDataGrid.Rows(index)

            'put the values from the relevant cells of that row into the input fields.
            ItemTextBox.Text = selectedRow.Cells(1).Value.ToString()
            CurrentStockTextBox.Text = selectedRow.Cells(2).Value.ToString()
            UpdatedStockTextBox.Text = selectedRow.Cells(2).Value.ToString()

        End If

    End Sub

    'Subroutine used to make changes to the database table "items".
    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        'if they have not filled all input fields then output an error message.
        If ItemTextBox.Text = "" Or CurrentStockTextBox.Text = "" Or UpdatedStockTextBox.Text = "" Then

            MsgBox("One or more input boxes are empty", MsgBoxStyle.Exclamation, "Error")

        Else

            'variable to get the "itemID" column value of the row chosen.
            Dim thisID As Integer = StockDataGrid.SelectedRows(0).Cells("itemID").Value
            Dim mycon As New SQLiteConnection("Data Source= C:\Users\Yasin\Desktop\DATABASE\SQLiteDB.db")
            mycon.Open()

            Dim mycmd As New SQLiteCommand
            mycmd.Connection = mycon

            'Create query where stock level is updated using the contents of the input field for the item selected.
            mycmd.CommandText = "UPDATE items SET stock = @stock WHERE itemID = @itemID"

            mycmd.Parameters.AddWithValue("@stock", CInt(UpdatedStockTextBox.Text))
            mycmd.Parameters.AddWithValue("@itemID", thisID)

            mycmd.ExecuteNonQuery()
            mycon.Close()

            'Clear all input fields so another query can be executed if necessary.
            ItemTextBox.Clear()
            CurrentStockTextBox.Clear()
            UpdatedStockTextBox.Clear()

            'Show updated table.
            LoadTable()

        End If

    End Sub

End Class