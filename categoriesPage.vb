Imports System.Data.SQLite
'imports nested namespaces for SQLite.

Public Class categoriesPage

    'A subroutine to dynamically create buttons for each category.
    Private Sub categoriesPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'A textbox to hold the name of the category.
        'It is hidden as the user does not need it.
        ItemTextBox.Hide()

        'Get number of rows in categories table
        Dim con As New SQLiteConnection("Data Source=C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        Dim da As New SQLiteDataAdapter("SELECT * FROM categories", con)
        Dim dt As New DataTable
        da.Fill(dt)

        'Variable to hold the number of rows.
        Dim numRows As Integer = dt.Rows.Count

        'A variable to hold the location of each dynamic button
        Dim intLeft As Integer = 1
        Dim intTop As Integer = 43

        'variable for width and height of each button
        Dim intWidth As Integer = 93 'px
        Dim intHeight As Integer = 50

        'loop through categories table to create a button for all categories
        For i As Integer = 0 To numRows - 1

            'If buttons widths are longer than form width then put buttons onto new line
            If intLeft >= Me.Width / 2 - intWidth Then

                intLeft = 0
                intTop += 60

            End If

            'create each button, positioning it correctly and giving each button its correct text.
            Dim btn As New Button
            btn.Text = dt.Rows(i)(1)

            btn.Left = intLeft
            btn.Top = intTop
            btn.Width = intWidth
            btn.Height = intHeight

            btn.BackColor = Color.White

            btn.Visible = True

            'add events for button
            AddHandler btn.Click, AddressOf OnBtn_Click

            'add button's controls
            Me.Controls.Add(btn)

            'Keep spacing between buttons
            intLeft += intWidth + 5

        Next

    End Sub

    'Creating the event for each button when they are clicked
    Private Sub OnBtn_Click(sender As Object, e As EventArgs)

        Dim mybtn As New Button
        'convert the object to the button type
        mybtn = CType(sender, Button)

        ItemTextBox.Text = mybtn.Text

        'When button is clicked show the form holding all the items of that category and hide this form.
        ItemsOfCatg.Show()
        Me.Hide()

    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        menuPage.Show()
        Me.Close()

    End Sub

    'If the void sale button is clicked, remove all items in the basket.
    Private Sub VoidSaleButton_Click(sender As Object, e As EventArgs) Handles VoidSaleButton.Click

        ItemsTextBox.Clear()
        QtyTextBox.Clear()
        PriceTextBox.Clear()
        TotalTextBox.Clear()

    End Sub

    'When PAY button is selected, increases purchases for each item by the number of purchases of that item.
    'Decrease the stock of that item by the number of purchase made.
    Private Sub PayButton_Click(sender As Object, e As EventArgs) Handles PayButton.Click

        'Get the number of items being bought in the multiline textbox holding the item names.
        Dim myEnd As Integer = ItemsTextBox.Lines.Length

        Dim i As Integer = 0

        'Iterate from the start of the multiline textbox until the end of the multiline textbox 
        'to increase the purchases of each item and decrease the stock of each item.
        While i < myEnd

            'Establish connection to database and open connection.
            Dim mycon As New SQLiteConnection("Data Source= C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
            mycon.Open()

            'Create command, attach its connection and set its SQL commmand
            'to update the purchases and stock column of each item.
            Dim mycmd As New SQLiteCommand
            mycmd.Connection = mycon

            mycmd.CommandText = "UPDATE items SET purchases = purchases + @purchases, stock = stock - @stock WHERE itemname = @itemname"
            mycmd.Parameters.AddWithValue("@purchases", QtyTextBox.Lines(i))
            mycmd.Parameters.AddWithValue("@stock", QtyTextBox.Lines(i))
            mycmd.Parameters.AddWithValue("@itemname", ItemsTextBox.Lines(i))

            mycmd.ExecuteNonQuery()

            'close connection
            mycon.Close()

            'increment 'i' by 1 so the next line in the multiline textbox can be updated.
            i += 1

        End While

        'Clear all multiline textboxes and the textbox holding the totala value of the sale.
        ItemsTextBox.Clear()
        QtyTextBox.Clear()
        PriceTextBox.Clear()
        TotalTextBox.Clear()

        'Display message to indicate that the sale has been completed.
        MsgBox("SALE COMPLETE", MsgBoxStyle.OkOnly, "Sale Completion")

    End Sub

End Class