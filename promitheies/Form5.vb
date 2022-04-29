Imports System.IO

Public Class Form5
    Public ektos As Boolean = False
    Public eidos, eidosektos As Integer

    Dim slashedpath, date1, date2, date3, path1 As String
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.Title = "Επιλογή Αρχείου PDF"
        OpenFileDialog1.Filter = "PDF Files|*.pdf"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.InitialDirectory = TextBox4.Text

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            TextBox2.Text = Path.GetFileName(OpenFileDialog1.FileName)
        End If
    End Sub
    Private Sub frmCustomerDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then

            If TextBox1.Focused Then
                loadgrid("SELECT eidos,code,perigrafi FROM eidi WHERE code LIKE '" & TextBox1.Text & "%'", Form2.DataGridView2)
                If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                    loadgrid("SELECT id,kwdikos,perigrafi FROM eidiektos WHERE kwdikos LIKE '" & TextBox1.Text & "%'", Form2.DataGridView2)
                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                        MessageBox.Show("Δε βρέθηκαν είδη στο σύστημα")
                    Else
                        Form2.caseselection = "form5"
                        ektos = True
                        Form2.Show()
                    End If
                Else
                    Form2.caseselection = "form5"
                    Form2.Show()
                End If
            End If
            If TextBox3.Focused Then
                loadgrid("SELECT eidos,code,perigrafi FROM eidi WHERE code LIKE '" & TextBox3.Text & "%'", Form2.DataGridView2)
                If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                    loadgrid("SELECT id,kwdikos,perigrafi FROM eidiektos WHERE kwdikos LIKE '" & TextBox3.Text & "%'", Form2.DataGridView2)
                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                        MessageBox.Show("Δε βρέθηκαν είδη στο σύστημα")
                    Else
                        Form2.caseselection = "form5-textbox3"
                        ektos = True
                        Form2.Show()
                    End If
                Else
                    Form2.caseselection = "form5-textbox3"
                    Form2.Show()
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button6.BackColor = Color.CornflowerBlue Then

            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MessageBox.Show("Δεν έχετε συμπληρώσει όλα τα πεδία")
                Exit Sub
            End If
            If eidos = 0 Then
                MessageBox.Show("Δεν έχετε επιλέξει είδος")
                Exit Sub
            End If
            TODATE(DateTimePicker3, date1)
            runquery("INSERT INTO promeidi(eidos,ektos,pdf,date) VALUES('" & eidos & "','" & eidosektos & "','" & TextBox2.Text & "','" & date1 & "')")

            If queryerror = False Then
                MessageBox.Show("Επιτυχής καταχώρηση")
            End If

            If eidosektos = 1 Then
                loadgrid("Select promeidi.date,promeidi.pdf,promeidi.id FROM eidiektos JOIN promeidi On eidiektos.id=eidos WHERE eidiektos.id='" & eidos & "' ORDER BY date DESC LIMIT 15", DataGridView1)
            End If
            If eidosektos = 0 Then

                loadgrid("SELECT promeidi.date,promeidi.pdf,promeidi.id FROM eidi JOIN promeidi ON eidi.eidos=promeidi.eidos WHERE eidi.eidos='" & eidos & "' ORDER BY date DESC LIMIT 15", DataGridView1)
            End If
            gridlayout()
            'eidosektos = 0
            Button4.Enabled = True
            Button6.BackColor = Color.LightGray
        End If
        If Button4.BackColor = Color.CornflowerBlue Then
            'addslashes(TextBox2)
            runquery("UPDATE promeidi SET pdf='" & TextBox2.Text & "' WHERE id='" & DataGridView1.CurrentRow.Cells(2).Value & "'")
            Button4.BackColor = Color.LightGray
            Button6.Enabled = True
            If queryerror = False Then
                MessageBox.Show("Επιτυχής καταχώρηση")
            End If

            If eidosektos = 1 Then
                loadgrid("Select promeidi.date,promeidi.pdf,promeidi.id FROM eidiektos JOIN promeidi On eidiektos.id=eidos WHERE eidiektos.id='" & eidos & "' ORDER BY date DESC LIMIT 15", DataGridView1)
            End If
            If eidosektos = 0 Then

                loadgrid("SELECT promeidi.date,promeidi.pdf,promeidi.id FROM eidi JOIN promeidi ON eidi.eidos=promeidi.eidos WHERE eidi.eidos='" & eidos & "' ORDER BY date DESC LIMIT 15", DataGridView1)
            End If
            gridlayout()
        End If
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        eidos = 0
        TextBox1.ReadOnly = True
        eidosektos = 0
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.BackColor = Color.CornflowerBlue Then
            Button4.BackColor = Color.LightGray
            Button6.Enabled = True
            Exit Sub
        End If
        Button4.BackColor = Color.CornflowerBlue
        Button6.Enabled = False
        Button5.Enabled = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        path1 = TextBox4.Text & "\" & DataGridView1.CurrentRow.Cells(1).Value.ToString

        If System.IO.File.Exists(path1) Then

            Try
                'Process.Start("explorer.exe", path1)
                Process.Start(path1)
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        Else
            MessageBox.Show("Η διαδρομή αρχείου δεν υπάρχει")
        End If
        Button5.Enabled = False
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowIcon = False
        Me.Text = "SigNet S.A                                                                                                                              Καταχώρηση Προσφοράς"
        TextBox3.Focus()
        TextBox4.Text = returnsinglevaluequery("SELECT commonpath FROM prompaths WHERE user='" & Environment.MachineName & "'")
        Dim imerominia As Date = returnsinglevaluequery("SELECT MAX(DATE) FROM promitheies")

        DateTimePicker1.Value = imerominia.AddMonths(-1)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Button6.BackColor = Color.CornflowerBlue Then
            Button6.BackColor = Color.LightGray
            Button4.Enabled = True
            TextBox2.Text = ""
            TextBox1.Text = ""
            eidos = 0
            Exit Sub
        End If
        TextBox1.ReadOnly = False
        TextBox2.Text = ""
        DataGridView1.DataSource = Nothing
        TextBox3.Text = ""
        Button5.Enabled = False
        Button4.Enabled = False
        Button6.BackColor = Color.CornflowerBlue
        If eidos = 0 Then

            TextBox1.Focus()

            TextBox1.Text = ""

        Else
            If eidosektos = 0 Then
                TextBox1.Text = returnsinglevaluequery("SELECT code FROM eidi WHERE eidos='" & eidos & "'")
            Else

                TextBox1.Text = returnsinglevaluequery("SELECT kwdikos FROM eidiektos WHERE id='" & eidos & "'")
            End If

        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        FolderBrowserDialog1.ShowDialog()

        If FolderBrowserDialog1.SelectedPath <> "" Then
            TextBox4.Text = FolderBrowserDialog1.SelectedPath
            addslashes(TextBox4)
            If returnsinglevaluequery("SELECT commonpath FROM prompaths WHERE user='" & Environment.MachineName & "'") = "" Then
                runquery("INSERT INTO prompaths(user,commonpath) VALUES('" & Environment.MachineName & "','" & slashedpath & "')")
            Else
                runquery("UPDATE prompaths SET commonpath='" & slashedpath & "' WHERE user='" & Environment.MachineName & "'")
            End If

        End If

    End Sub

    Private Sub addslashes(tb As TextBox)

        slashedpath = tb.Text.Replace("\", "\\")

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TODATE(DateTimePicker1, date1)
        TODATE(DateTimePicker2, date2)
        If eidosektos = 1 Then
            loadgrid("Select promeidi.date,promeidi.pdf,promeidi.id FROM eidiektos JOIN promeidi On eidiektos.id=eidos WHERE eidiektos.id='" & eidos & "' AND date>='" & date1 & "' AND date<='" & date2 & "' ORDER BY date DESC", DataGridView1)
        End If
        If eidosektos = 0 Then
            loadgrid("SELECT promeidi.date,promeidi.pdf,promeidi.id FROM eidi JOIN promeidi ON eidi.eidos=promeidi.eidos WHERE eidi.eidos='" & eidos & "' AND date>='" & date1 & "' AND date<='" & date2 & "' ORDER BY date DESC", DataGridView1)
        End If
        If DataGridView1.RowCount = 1 Then
            DataGridView1.DataSource = Nothing
            MessageBox.Show("Δε βρέθηκαν εγγραφές")
            Exit Sub
        End If
        TextBox1.Text = TextBox3.Text

        gridlayout()
    End Sub
    Private Sub gridlayout()
        With DataGridView1
            .Columns(0).Width = 170
            .Columns(1).Width = 300

            .Columns(0).HeaderText = "Ημ/νία Καταχώρησης"
            .Columns(1).HeaderText = "Διαδρομή Αρχείου PDF"
            .Columns(2).Visible = False
        End With

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Button5.Enabled = True
        TextBox2.Text = DataGridView1.CurrentRow.Cells(1).Value
    End Sub

End Class