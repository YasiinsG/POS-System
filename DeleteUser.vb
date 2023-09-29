Imports System.Data.SQLite
'imports nested namespaces for SQLite.

Public Class DeleteUser

    'If exit button is clicked, return back to managementPage and close this form.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        managementPage.Show()
        Me.Close()

    End Sub

    'When this form loads call the subroutine LoadUserTable()
    Private Sub DeleteUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadUserTable()

    End Sub

    Private Sub LoadUserTable()

        'Create connection to database.
        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()

        'Create command
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        'Attach query to command
        cmd.CommandText = "SELECT * FROM login ORDER BY ID ASC"

        'Create reader to read the result of the query and then load it into the datatable.
        Dim reader As SQLiteDataReader = cmd.ExecuteReader
        Dim dt As New DataTable
        dt.Load(reader)

        'close reader and connection
        reader.Close()
        con.Close()

        'State the datasource of the datagrid.
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

            'put the value from the relevant cell of that row into the input field.
            NameTextBox.Text = selectedRow.Cells(1).Value.ToString()

        End If

    End Sub

    'Subroutine used to delete a user from the database table "login".
    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click

        'if they have not filled the input field then output an error message.
        If NameTextBox.Text = "" Then

            MsgBox("Input box is empty", MsgBoxStyle.Exclamation, "Error")

        Else

            'variable to get the "ID" column value of the row chosen.
            Dim thisID As Integer = UserDataGrid.SelectedRows(0).Cells("ID").Value
            Dim mycon As New SQLiteConnection("Data Source= C:\Users\Yasin\Desktop\DATABASE\SQLiteDB.db")
            mycon.Open()

            Dim mycmd As New SQLiteCommand
            mycmd.Connection = mycon

            'Create query where the user is removed from the table "login"
            'using the contents of the input field.
            mycmd.CommandText = "DELETE FROM login WHERE ID = @ID"

            mycmd.Parameters.AddWithValue("@ID", thisID)

            mycmd.ExecuteNonQuery()
            mycon.Close()

            'Clear input fields.
            NameTextBox.Text = ""

            'Show updated database table.
            LoadUserTable()

        End If

    End Sub

End Class