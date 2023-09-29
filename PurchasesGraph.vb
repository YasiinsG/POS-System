'import nested namespaces for SQLite.
Imports System.Data.SQLite
'import nested namespaces for chart.
Imports System.Windows.Forms.DataVisualization.Charting

Public Class PurchasesGraph

    'If exit button is selected, show managementPage And close this form.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click

        managementPage.Show()
        Me.Close()

    End Sub

    'When this form is opened, call the subroutine CreateChart().
    Private Sub PurchasesGraph_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CreateChart()

    End Sub

    'Subroutine to create and display chart to the user dynamically.
    'Chart is connected to database so it can fetch values required.
    Private Sub CreateChart()

        'Define connection string to connect to database.
        Dim strCon As String = "Data Source= C:\Users\Yasin\Desktop\DATABASE\SQLiteDB.db"
        'Create SQLiteconnection's object.
        Dim newCon As New SQLiteConnection(strCon)
        Dim items As String
        'Define SQL query string.
        items = "SELECT itemname,purchases FROM items ORDER BY purchases DESC LIMIT 5"

        'Execute the query and populate the result to dataset.
        Dim da As New SQLiteDataAdapter(items, newCon)
        Dim ds As New DataSet()
        da.Fill(ds, "items")

        'Create chart's objects.
        Dim ChartArea1 As ChartArea = New ChartArea()
        Dim legend1 As Legend = New Legend()
        Dim series1 As Series = New Series()
        Dim Chart1 = New Chart()
        'Add chart's object to the form.
        Me.Controls.Add(Chart1)

        'Set the properties of the chart (ChartArea, Legend and Series)
        ChartArea1.Name = "ChartArea1"
        Chart1.ChartAreas.Add(ChartArea1)
        legend1.Name = "Legend1"
        Chart1.Legends.Add(legend1)
        Chart1.Location = New Point(13, 13)
        Chart1.Name = "itemsChart"
        series1.ChartArea = "ChartArea1"
        series1.Legend = "Legend1"
        series1.Name = "Number Of Purchases"
        Chart1.Series.Add(series1)
        Chart1.Size = New Size(800, 400)
        Chart1.TabIndex = 0
        Chart1.Text = "Chart1"

        'Bind column 'itemname' to X-axis and column 'purchases' to Y-axis on the 'series1' chart.
        Chart1.Series("Number Of Purchases").XValueMember = "itemname"
        Chart1.Series("Number Of Purchases").YValueMembers = "purchases"

        'Set the data source of the chart to the datatable in the dataset's object.
        Chart1.DataSource = ds.Tables("items")

    End Sub

End Class