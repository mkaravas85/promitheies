Public Class Form4
    Dim tmimaid As Integer = 0
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowIcon = False
        Me.Text = "SigNet S.A                                                                                                           Login"
        'Dim task As String = Environment.GetEnvironmentVariable("TASK").ToString
        'tmimaid = returnsinglevaluequery("SELECT tmima FROM promusers WHERE task='" & task & "' AND task is not null")
        ''If tmimaid <> 0 Then
        ''runquerycombo("SELECT description FROM tmimata WHERE tmimaid='" & tmimaid & "'", ComboBox1, "description")
        'If task = "077" Then
        '        tmimaid = 1
        '        'runquerycombo("SELECT description FROM tmimata WHERE tmimaid='" & tmimaid & "'", ComboBox1, "description")
        '        'ComboBox1.SelectedIndex = 0
        '    ElseIf task = "" Then
        '        tmimaid = 0
        '        'runquerycombo("SELECT description FROM tmimata WHERE tmimaid='" & tmimaid & "'", ComboBox1, "description")
        '        'ComboBox1.SelectedIndex = 0
        '    ElseIf task = "" Then
        '        tmimaid = 0
        '        'runquerycombo("SELECT description FROM tmimata WHERE tmimaid='" & tmimaid & "'", ComboBox1, "description")
        '        'ComboBox1.SelectedIndex = 0
        '    ElseIf task = "" Then
        '        tmimaid = 0
        '    'runquerycombo("SELECT description FROM tmimata WHERE tmimaid='" & tmimaid & "'", ComboBox1, "description")
        '    'ComboBox1.SelectedIndex = 0
        '    'Else

        '    'runquerycombo("SELECT description FROM tmimata JOIN promusers ON tmimaid=tmima WHERE promusers.task is not null", ComboBox1, "description") 'task exoun mono ta tmimata,oxi oi extra users
        'End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'If ComboBox1.SelectedItem = Nothing Then
        Dim level As Integer = 0
        'If tmimaid <> 0 Then
        tmimaid = returnsinglevaluequery("SELECT tmima FROM promusers where password='" & TextBox1.Text.ToUpper & "'")

        If tmimaid <> 0 Then
            Form1.userlevel = returnsinglevaluequery("SELECT level FROM promusers WHERE password='" & TextBox1.Text.ToUpper & "'")
            Form1.tmima = tmimaid
            Form1.TextBox3.Text = returnsinglevaluequery("SELECT description from tmimata WHERE tmimaid='" & tmimaid & "'")

            Form1.Show()
            Me.Close()
        Else
            MessageBox.Show("Λάθος κωδικός")
            End If
        'End If
        'If returnsinglevaluequery("SELECT level FROM promusers WHERE password='" & TextBox1.Text.ToUpper & "' AND level<>0") Is Nothing Then 'ΟΛΟΙ ΟΙ ΚΩΔΙΚΟΙ ΣΤΗ ΒΑΣΗ ΝΑ ΑΠΟΘΗΚΕΥΟΝΤΑΙ ΜΕ ΚΕΦΑΛΑΙΑ
        '        MessageBox.Show("Λάθος κωδικός")
        '        Exit Sub

        '    Else
        '        level = returnsinglevaluequery("SELECT level FROM promusers WHERE password='" & TextBox1.Text.ToUpper & "' AND level<>0")

        '        'If TextBox1.Text <> "" And TextBox1.Text.ToUpper = level Then
        '        Form1.userlevel = level
        '        Form1.Show()
        '        Me.Close()
        '        Exit Sub
        '        'Else
        '        '    MessageBox.Show("Λάθος κωδικός")
        '        '    Exit Sub
        '    End If
        ''End If
        'If tmimaid = 0 Then
        '    'tmimaid = returnsinglevaluequery("SELECT tmimaid FROM tmimata WHERE description='" & ComboBox1.SelectedItem.ToString & "'")
        'End If
        'dtvaluetoset("SELECT password FROM promusers where tmima='" & tmimaid & "'")

        'If TextBox1.Text = dt.Rows(0).Item(0).ToString Then
        '    Form1.userlevel = returnsinglevaluequery("SELECT level FROM promusers WHERE tmima='" & tmimaid & "'")
        '    Form1.tmima = tmimaid
        '    Form1.TextBox3.Text = returnsinglevaluequery("SELECT perigrafi from tmimata WHERE tmimaid='" & tmimaid & "'")

        '    Form1.Show()
        '    Me.Close()
        'Else
        '    MessageBox.Show("Λάθος κωδικός")
        'End If
    End Sub

    Private Sub Form4_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If

    End Sub

    'Private Sub Form4_Click(sender As Object, e As EventArgs) Handles Me.Click
    '    ComboBox1.SelectedItem = Nothing
    'End Sub

End Class