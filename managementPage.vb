Public Class managementPage

    'For each button that is clicked, shows its corresponding form page and close this form page.

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        menuPage.Show()
        Me.Close()

    End Sub

    Private Sub AddItemCatgButton_Click(sender As Object, e As EventArgs) Handles AddItemCatgButton.Click

        AddItemCatg.Show()
        Me.Close()

    End Sub

    Private Sub ModifyItemCatgButton_Click(sender As Object, e As EventArgs) Handles ModifyItemCatgButton.Click

        ModifyItemCatg.Show()
        Me.Close()

    End Sub

    Private Sub DeleteItemCatgButton_Click(sender As Object, e As EventArgs) Handles DeleteItemCatgButton.Click

        DeleteItmCatg.Show()
        Me.Close()

    End Sub

    Private Sub NewUserButton_Click(sender As Object, e As EventArgs) Handles NewUserButton.Click

        NewUser.Show()
        Me.Close()

    End Sub

    Private Sub ModifyUserButton_Click(sender As Object, e As EventArgs) Handles ModifyUserButton.Click

        ModifyUser.Show()
        Me.Close()

    End Sub

    Private Sub DeleteUserButton_Click(sender As Object, e As EventArgs) Handles DeleteUserButton.Click

        DeleteUser.Show()
        Me.Close()

    End Sub

    Private Sub PurchasesGraphButton_Click(sender As Object, e As EventArgs) Handles PurchasesGraphButton.Click

        PurchasesGraph.Show()
        Me.Close()

    End Sub

    Private Sub StockLevelButton_Click(sender As Object, e As EventArgs) Handles StockLevelButton.Click

        StockManagement.Show()
        Me.Close()

    End Sub

End Class