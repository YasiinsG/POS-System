Public Class menuPage

    'When user chooses to logout, make sure that they do want to logout by using a messagebox to check
    'If result of messagebox is yes then logout.
    Private Sub logoutButton_Click(sender As Object, e As EventArgs) Handles logoutButton.Click

        Dim notice As DialogResult = MessageBox.Show("Are you sure you want to log out?", "System Message", MessageBoxButtons.YesNo)

        If notice = DialogResult.Yes Then
            loginPage.Show()
            Me.Close()
        End If

    End Sub

    'If management button is clicked, show managementPage and close this page
    Private Sub managementButton_Click(sender As Object, e As EventArgs) Handles managementButton.Click

        managementPage.Show()
        Me.Close()

    End Sub

    'If categories button is clicked, show categoriesPage and close this page
    Private Sub categoriesButton_Click(sender As Object, e As EventArgs) Handles categoriesButton.Click

        categoriesPage.Show()
        Me.Close()

    End Sub

End Class