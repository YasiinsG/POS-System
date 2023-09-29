Imports System.Data.SQLite
'imports nested namespaces for SQLite.

Public Class ModifyUser

    'When exit button is clicked open the management page and close this page.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        managementPage.Show()
        Me.Close()

    End Sub

    'When this page loads, call the subroutine LoadUserTable()
    Private Sub ModifyUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadUserTable()

    End Sub

    Private Sub LoadUserTable()

        'Create connection to database
        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()

        'create command for query that is to be executed against database.
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        'get all rows from the table login
        cmd.CommandText = "SELECT * FROM login ORDER BY ID ASC"

        'read all the database rows into the datatable.
        Dim reader As SQLiteDataReader = cmd.ExecuteReader
        Dim dt As New DataTable
        dt.Load(reader)

        'close connection and reader
        reader.Close()
        con.Close()

        'State the source of the data for the datagrid.
        UserDataGrid.DataSource = dt

    End Sub

    'When a cell is selected in the datagrid, uploads all values of that row into the correct input field.
    Private Sub UserDataGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles UserDataGrid.CellClick

        'Create a variable which holds the index of the row selected in the datatable.
        Dim index As Integer
        index = e.RowIndex

        'If the row is not a row which holds values of the database then show an error message.
        If index < 0 Then
            MsgBox("Select a cell!", MsgBoxStyle.Exclamation)

        Else

            'Create variable to hold all values of row chosen.
            Dim selectedRow As DataGridViewRow
            selectedRow = UserDataGrid.Rows(index)

            'put the values from the relevant cells of that row into the input fields.
            NameTextBox.Text = selectedRow.Cells(1).Value.ToString()
            NewFirstNameTextBox.Text = selectedRow.Cells(1).Value.ToString()
            NewLastNameTextBox.Text = selectedRow.Cells(2).Value.ToString()
            CodeTextBox.Text = selectedRow.Cells(3).Value.ToString()
            AccessLevelTextBox.Text = selectedRow.Cells(4).Value.ToString()

        End If

    End Sub

    'Subroutine used to make changes to the database table "login".
    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        'if they have not filled all input fields then output an error message.
        If NewFirstNameTextBox.Text = "" Or CodeTextBox.Text = "" Or (AccessLevelTextBox.Text <> 1 And AccessLevelTextBox.Text <> 2) Then

            MsgBox("One or more input boxes are empty or have invalid entries", MsgBoxStyle.Exclamation, "Error")

        Else

            'variable to get the "ID" column value of the row chosen.
            Dim thisID As Integer = UserDataGrid.SelectedRows(0).Cells("ID").Value
            Dim mycon As New SQLiteConnection("Data Source= C:\Users\Yasin\Desktop\DATABASE\SQLiteDB.db")
            mycon.Open()

            Dim mycmd As New SQLiteCommand
            mycmd.Connection = mycon

            'Create query where firstname, lastname, code and access level are updated
            'using the contents of the input fields.
            mycmd.CommandText = "UPDATE login SET firstname = @firstname, lastname = @lastname,
                                code = @code, accesslevel = @accesslevel WHERE ID = @ID"

            mycmd.Parameters.AddWithValue("@firstname", NewFirstNameTextBox.Text)
            mycmd.Parameters.AddWithValue("@lastname", NewLastNameTextBox.Text)
            mycmd.Parameters.AddWithValue("@code", CInt(CodeTextBox.Text))
            mycmd.Parameters.AddWithValue("@accesslevel", CInt(AccessLevelTextBox.Text))
            mycmd.Parameters.AddWithValue("@ID", thisID)

            mycmd.ExecuteNonQuery()
            mycon.Close()

            'Clear all input fields so another query can be executed if necessary.
            NameTextBox.Clear()
            NewFirstNameTextBox.Clear()
            NewLastNameTextBox.Clear()
            CodeTextBox.Clear()
            AccessLevelTextBox.Clear()

            LoadUserTable()

        End If

    End Sub

End Class