Imports System.Data.SQLite
'Imports nested namespaces for SQLite.

Public Class AddItemCatg

    'When they click the save button, check if all input fields are filled
    'If they are not filled output an error message.
    'Otherwise execute updateTable()
    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        If ChoiceComboBox.Text = "CATEGORY" Then

            If NameBox.Text = "" Then

                MsgBox("Input a name for your category", MsgBoxStyle.Exclamation, "Error")

            Else

                updateTable()

            End If

        ElseIf ChoiceComboBox.Text = "ITEM" Then

            If NameBox.Text = "" Or ValueBox.Text = "" Or CatgComboBox.Text = "" Then

                MsgBox("One or more input boxes are empty", MsgBoxStyle.Exclamation, "Error")

            Else

                updateTable()

            End If

        Else

            MsgBox("Choose an option from ITEM or CATEGORY", MsgBoxStyle.Exclamation, "Error")

        End If

    End Sub

    'If the exit button is clicked, show managementPage and close this form.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        managementPage.Show()
        Me.Close()

    End Sub

    Private Sub AddItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'When this form loads hide all input fields
        ValueLabel.Hide()
        ValueBox.Hide()
        CatgChoiceLabel.Hide()
        CatgComboBox.Hide()
        ItemCatgLabel.Hide()
        NameBox.Hide()
        CatgIDTextBox.Hide()

        'Create connection to database
        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()

        'Create command for query
        Dim cmd As New SQLiteCommand
        cmd.Connection = con
        cmd.CommandText = "SELECT categoryID,categoryname FROM categories ORDER BY categoryID ASC"

        'Fill datatable
        Dim dt As New DataTable
        dt.Load(cmd.ExecuteReader)

        'State the datasource of CatgComboBox
        CatgComboBox.DataSource = dt

        'State which values to display in CatgComboBox
        CatgComboBox.DisplayMember = "categoryname"
        CatgComboBox.SelectedIndex = -1

        con.Close()

    End Sub

    'Subroutine to load either the "items" or "categories" table from the database
    Private Sub LoadItemCatgTable()

        Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        con.Open()
        Dim cmd As New SQLiteCommand
        cmd.Connection = con

        'If they want to add a category then show the "categories" table
        'Othewise show the "items" table.
        If ChoiceComboBox.Text = "CATEGORY" Then

            cmd.CommandText = "SELECT * FROM categories ORDER BY categoryID ASC"

        Else

            cmd.CommandText = "SELECT * FROM items ORDER BY itemID ASC"

        End If

        Dim reader As SQLiteDataReader = cmd.ExecuteReader
        Dim dt As New DataTable
        dt.Load(reader)
        reader.Close()
        con.Close()

        ItemCatgGrid.DataSource = dt

    End Sub

    'Subroutine used to make additions to either the table "items" or the table "category".
    Private Sub updateTable()

        'If the input field is not empty then execute query
        If CatgComboBox.Text <> "" Then

            Dim con As New SQLiteConnection("Data Source = C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
            con.Open()

            'Retrieve all rows in the table "categories" which is the same as the users's selection.
            Dim cmd As New SQLiteCommand("SELECT * FROM categories WHERE categoryname = '" & CatgComboBox.Text & "'", con)
            Dim dr As SQLiteDataReader
            dr = cmd.ExecuteReader()
            dr.Read()

            'If the table is not empty (meaning a category exists) then attach the "categoryID" to CatgIDTextBox
            If dr.HasRows Then

                CatgIDTextBox.Text = dr("categoryID")

            End If

            dr.Close()
            con.Close()

        End If

        'Create new connection to database.
        Dim mycon As New SQLiteConnection("Data Source= C:\Users\Yasin\Desktop\DATABASE\sqliteDB.db")
        mycon.Open()

        Dim mycmd As New SQLiteCommand
        mycmd.Connection = mycon

        'If the user wants to add a category then update "categories" table.
        If ChoiceComboBox.Text = "CATEGORY" Then

            mycmd.CommandText = "INSERT INTO categories (categoryname) VALUES (@categoryname)"
            mycmd.Parameters.AddWithValue("@categoryname", NameBox.Text)

        Else
            'If the user wants to add an item then update the "items" table.
            mycmd.CommandText = "INSERT INTO items (itemname,itemvalue,categoryname,categoryID,purchases,stock)
                                VALUES (@itemname,@itemvalue,@categoryname,@categoryID,0,0)"
            mycmd.Parameters.AddWithValue("@itemname", NameBox.Text)
            'Convert @itemvalue to decimal, so the value of the item can be used in calculations
            mycmd.Parameters.AddWithValue("@itemvalue", CDec(ValueBox.Text))
            mycmd.Parameters.AddWithValue("@categoryname", CatgComboBox.Text)
            mycmd.Parameters.AddWithValue("@categoryID", CInt(CatgIDTextBox.Text))

        End If

        mycmd.ExecuteNonQuery()
        mycon.Close()

        LoadItemCatgTable()

        'Clear input fields, so user can enter another item/category if they wish to.
        NameBox.Text = ""
        ValueBox.Text = ""
        CatgComboBox.SelectedIndex = -1
        ChoiceComboBox.SelectedIndex = -1

    End Sub

    Private Sub ChoiceComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChoiceComboBox.SelectedIndexChanged

        'If the user choose to add a category show relevant fields and their respective labels, keep all other fields and labels hidden.
        If ChoiceComboBox.Text = "CATEGORY" Then

            'change label text to highlight the purpose of the input field
            ItemCatgLabel.Text = "CATEGORY NAME:"
            ItemCatgLabel.Show()
            NameBox.Show()
            ValueLabel.Hide()
            ValueBox.Hide()
            CatgChoiceLabel.Hide()
            CatgComboBox.Hide()

            LoadItemCatgTable()

            'If the user choose to add an item show relevant fields and their respective labels, keep all other fields and labels hidden.
        ElseIf ChoiceComboBox.Text = "ITEM" Then

            'change label text to highlight the purpose of the input field
            ItemCatgLabel.Text = "ITEM NAME:"
            ItemCatgLabel.Show()
            NameBox.Show()
            ValueLabel.Show()
            ValueBox.Show()
            CatgChoiceLabel.Show()
            CatgComboBox.Show()

            'Load the table "items" or "categories", depending on user selection.
            LoadItemCatgTable()

        End If

    End Sub

End Class