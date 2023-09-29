Imports System.Data.SQLite
'imports nested namespaces for SQLite.

Public Class ItemsOfCatg

    'A subroutine to dynamically create buttons for each item in the specific category.
    Private Sub ItemsOfCatg_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'A textbox to hold the name of the item.
        'It is hidden as the user does not need it.
        ItemChoiceTextBox.Hide()

        'Get number of items in that specific category from the items table
        Dim con As New SQLiteConnection("Data Source=C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        Dim da As New SQLiteDataAdapter("SELECT * FROM items WHERE categoryname = '" & categoriesPage.ItemTextBox.Text & "'", con)
        Dim dt As New DataTable
        da.Fill(dt)

        'Variable to hold the number of rows.
        Dim numRows As Integer = dt.Rows.Count

        'A variable to hold the location of each dynamic button
        Dim intLeft As Integer = 0
        Dim intTop As Integer = 0

        'variable for width and height of each button
        Dim intWidth As Integer = 100 'px
        Dim intHeight As Integer = 50

        'loop through items table to create a button for all items
        For i As Integer = 0 To numRows - 1

            'If buttons widths are longer than form width then put buttons onto new line
            If intLeft >= Me.Width - intWidth Then

                intLeft = 0
                intTop += 60

            End If

            'create each button, positioning it correctly and giving each button its correct text.
            Dim button As New Button
            button.Text = dt.Rows(i)(1)

            button.Left = intLeft
            button.Top = intTop
            button.Width = intWidth
            button.Height = intHeight

            button.BackColor = Color.White

            button.Visible = True

            'add events for button
            AddHandler button.Click, AddressOf OnButton_Click

            'add button's controls
            Me.Controls.Add(button)

            'Keep spacing between buttons
            intLeft += intWidth + 5

        Next

    End Sub

    'When one of the dynamically created buttons is selected, add the button's text(name), quantity and price to the basket.
    Private Sub OnButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim button As New Button

        'convert the object to the button type
        button = CType(sender, Button)

        ItemChoiceTextBox.Text = button.Text
        Dim qtyOfItem As Integer = 1

        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        Dim cmd As New SQLiteCommand("SELECT * FROM items WHERE itemname = '" & ItemChoiceTextBox.Text & "' 
                                    AND categoryname = '" & categoriesPage.ItemTextBox.Text & "'", con)
        Dim dr As SQLiteDataReader
        dr = cmd.ExecuteReader
        Dim j As Integer = 0

        Dim found As Boolean = False
        Dim thisItemPriceQty As Integer = 0

        'An iteration to check if the selected item is already in the basket.
        'If the item is already in the basket then the quantity for that item is increased by one,
        'rather than creating a new entry in the basket.
        While dr.Read()

            With categoriesPage.ItemsTextBox

                Dim item() As String = .Lines

                While found = False And j < item.Length

                    If item(j) = dr(1) Then

                        found = True

                    Else

                        j += 1

                    End If

                End While

            End With

            'If item is found, increase the qty of the item on that line in the multiline textbox.
            If found = True Then

                With categoriesPage.QtyTextBox

                    Dim qty() As String = .Lines
                    qty(j) += 1
                    thisItemPriceQty = qty(j)
                    .Lines = qty

                End With

                'Also increase the price of the item as the quantity has now increased.
                With categoriesPage.PriceTextBox

                    Dim itemPrice() As String = .Lines
                    'Multiply price by the quantity of that item.
                    itemPrice(j) = itemPrice(j) * thisItemPriceQty
                    .Lines = itemPrice

                End With

            Else

                'In the case the item is not found in the basket, create a new entry in the basket.
                categoriesPage.ItemsTextBox.Text = categoriesPage.ItemsTextBox.Text & dr(1) & Environment.NewLine
                categoriesPage.QtyTextBox.Text = categoriesPage.QtyTextBox.Text & qtyOfItem & Environment.NewLine
                categoriesPage.PriceTextBox.Text = categoriesPage.PriceTextBox.Text & dr(2) & Environment.NewLine

            End If

        End While

        'Variable to hold the value of all the items
        Dim total As Decimal = 0
        'Variable to hold the value of the current item in the iteration.
        Dim value As Decimal = 0

        With categoriesPage.PriceTextBox
            Dim price() As String = .Lines

            For Each line As String In categoriesPage.PriceTextBox.Lines
                'Convert the value found in the line in PriceTextBox to decimal so it can be used in the total.
                Decimal.TryParse(line, value)
                total += value

            Next line

        End With

        'Place the updated total into TotalTextBox
        categoriesPage.TotalTextBox.Text = vbCrLf & total

        'Show categoriesPage and close this form.
        categoriesPage.Show()
        Me.Close()

    End Sub

    'If the user would like to go back/choose a different category then show categoriesPage and close this page.
    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click

        categoriesPage.Show()
        Me.Close()

    End Sub

End Class