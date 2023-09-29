Imports System.Data.SQLite
'imports nested namespaces for SQLite.

Public Class NewUser
    'When this form loads, carry out the subroutine LoadUserTable()
    Private Sub NewUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadUserTable()

    End Sub

    Private Sub LoadUserTable()
        'create connection to database
        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        'create command to be used to search database
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        cmd.CommandText = "SELECT * FROM login ORDER BY ID ASC"

        'create a datareader to read the values from the command into a datatable
        Dim reader As SQLiteDataReader = cmd.ExecuteReader
        Dim dt As New DataTable
        dt.Load(reader)

        'close reader and connection
        reader.Close()
        con.Close()

        'Make the datasource of the datagrid the datatable so database query result is shown
        NewUserDataGrid.DataSource = dt

    End Sub

    'When exiting from this form, open managementPage and close this form.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        managementPage.Show()
        Me.Close()

    End Sub

    'Procedure to save the new user entry into the database table "Login"
    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        'Check if any entry fields are empty. If they are show error message.
        If FirstNameTextBox.Text = "" Or LastNameTextBox.Text = "" Or CodeTextBox.Text = "" Or AccessLevelComboBox.Text Is Nothing Then

            MsgBox("One or more input boxes are empty", MsgBoxStyle.Exclamation, "Error")

        Else

            'If no errors, create a connection to the database
            Dim mycon As New SQLiteConnection("Data Source= C:\Users\Yasin\Desktop\DATABASE\SQLiteDB.db")
            mycon.Open()

            'create a command to be executed against the database
            Dim mycmd As New SQLiteCommand
            mycmd.Connection = mycon

            'Create the query for the command
            mycmd.CommandText = "INSERT INTO login (firstname,lastname,code,accesslevel) 
                                VALUES (@firstname,@lastname,@code,@accesslevel)"
            'Give each field that is being input into the table "login" the area from which to get its value.
            mycmd.Parameters.AddWithValue("@firstname", FirstNameTextBox.Text)
            mycmd.Parameters.AddWithValue("@lastname", LastNameTextBox.Text)
            'convert "code" and "accesslevel" to integer datatype as they are both integer values.
            mycmd.Parameters.AddWithValue("@code", CInt(CodeTextBox.Text))
            mycmd.Parameters.AddWithValue("@accesslevel", CInt(AccessLevelComboBox.Text))

            'execute the query against the table "login"
            mycmd.ExecuteNonQuery()

            'close connection
            mycon.Close()

            'clear input fields.
            FirstNameTextBox.Text = ""
            LastNameTextBox.Text = ""
            CodeTextBox.Text = ""
            AccessLevelComboBox.SelectedIndex = -1

            'Display the updated table "login" in the datagrid.
            LoadUserTable()

        End If

    End Sub

End Class