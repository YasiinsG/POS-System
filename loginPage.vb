Imports System.Data.SQLite
'imports nested namespaces for SQLite.

Public Class loginPage

    ' declare connection to SQLite database
    Dim connect As SQLiteConnection
    'declare "command" to execute commands against SQLite database
    Dim command As SQLiteCommand
    'Declare global variable to hold correct database row position
    Dim pos As Integer

    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click

        'When login button is clicked, initiate dbSearch() to check if code entered exists
        dbSearch()

    End Sub

    Private Sub loginPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Check if connection is achieved on opening the application
        connection()

    End Sub

    Private Sub accessLevelSearch()

        'declare variable to hold value of accesslevel from SQLite database
        Dim userAccessLevel As Integer

        'connect to the database sqliteDB.db
        Using con As SQLiteConnection = New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")

            'Execute command to select all fields from the table "login"
            Using cmd As SQLiteCommand = New SQLiteCommand("SELECT * FROM login", con)
                Using da As New SQLiteDataAdapter
                    da.SelectCommand = cmd

                    'store the data in memory and populate DataTable
                    Using dt As New DataTable
                        da.Fill(dt)

                        'If a row exists in the table:
                        'If value in column 5 (accesslevel) of row is 1 hide management page button.
                        If dt.Rows.Count > 0 Then
                            userAccessLevel = dt.Rows(pos)(4)

                            If userAccessLevel = 1 Then
                                menuPage.managementButton.Hide()
                            End If

                            'close connection to database
                            'close current page and open menuPage.
                            con.Close()
                            menuPage.Show()
                            Me.Close()

                        End If

                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub dbSearch()

        'variable to hold value of code from database as string.
        Dim checkCode As String

        'connect to database sqliteDB.db.
        Using con As SQLiteConnection = New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")

            'Execute command to select all fields from the table "login"
            Using cmd As SQLiteCommand = New SQLiteCommand("SELECT * FROM login", con)
                Using da As New SQLiteDataAdapter
                    da.SelectCommand = cmd

                    'store the data in memory and populate DataTable
                    Using dt As New DataTable
                        da.Fill(dt)

                        'check each row from row 0 to row n - 1
                        'store value from that row's 4th column (code) in variable checkCode.
                        For i = 0 To dt.Rows.Count() - 1
                            checkCode = dt.Rows(i).Item(3)

                            'Compare checkCode to value in LoginTextBox
                            'if they are the same change StatusLabel colour and text
                            'save correct row index (position i) in variable "pos"
                            If checkCode = LoginTextBox.Text Then
                                StatusLabel.Text = "Valid Entry"
                                StatusLabel.BackColor = Color.Green
                                StatusLabel.ForeColor = Color.White
                                pos = i

                                'close connection to database and accessLevelSearch() to find accesslevel
                                con.Close()
                                accessLevelSearch()
                            End If

                        Next

                        'If code cannot be found in any row:
                        'change StatusLabel to indicate error using colour red and text "Invalid input".
                        StatusLabel.BackColor = Color.Red
                        StatusLabel.ForeColor = Color.White
                        StatusLabel.Text = "Invalid input"

                        'Close connection to database.
                        con.Close()

                    End Using
                End Using
            End Using
        End Using

    End Sub

    'subroutine to check connection, when testing, on starting the application.
    Sub connection()

        Try
            'connect to database ("sqliteDB.db")
            connect = New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")

            'If the connection to database is currently closed, open the connection
            'Output messagebox to indicate connection success
            If connect.State = ConnectionState.Closed Then
                connect.Open()
                MsgBox("Connection Success!", MsgBoxStyle.Information, "Informations")

                'close connection.
                connect.Close()

            End If

            'If an error occurs in trying to connect, output messagebox indicating failed connection
        Catch ex As Exception
            MsgBox("Failed to connect to SQLite Database", MsgBoxStyle.Information, "Warning")

        End Try

    End Sub

    'When "CLEAR" button clicked, change contents of LoginTextBox to empty.
    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click

        LoginTextBox.Text = ""

    End Sub

    Private Sub Btn1_Click(sender As Object, e As EventArgs) Handles Btn1.Click

        'Add "1" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn1.Text

    End Sub

    Private Sub Btn2_Click(sender As Object, e As EventArgs) Handles Btn2.Click

        'Add "2" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn2.Text

    End Sub

    Private Sub Btn3_Click(sender As Object, e As EventArgs) Handles Btn3.Click

        'Add "3" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn3.Text

    End Sub

    Private Sub Btn4_Click(sender As Object, e As EventArgs) Handles Btn4.Click

        'Add "4" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn4.Text

    End Sub

    Private Sub Btn5_Click(sender As Object, e As EventArgs) Handles Btn5.Click

        'Add "5" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn5.Text

    End Sub

    Private Sub Btn6_Click(sender As Object, e As EventArgs) Handles Btn6.Click

        'Add "6" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn6.Text

    End Sub

    Private Sub Btn7_Click(sender As Object, e As EventArgs) Handles Btn7.Click

        'Add "7" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn7.Text

    End Sub

    Private Sub Btn8_Click(sender As Object, e As EventArgs) Handles Btn8.Click

        'Add "8" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn8.Text

    End Sub

    Private Sub Btn9_Click(sender As Object, e As EventArgs) Handles Btn9.Click

        'Add "9" to end of the text in LoginTextBox
        LoginTextBox.Text = LoginTextBox.Text & Btn9.Text

    End Sub

    'if "EXIT" button is clicked, close the application.
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click

        Me.Close()

    End Sub

    Private Sub LoginTextBox_TextChanged(sender As Object, e As EventArgs) Handles LoginTextBox.TextChanged

        'align text of LoginTextBox towards the centre.
        LoginTextBox.TextAlign = HorizontalAlignment.Center

    End Sub

End Class
