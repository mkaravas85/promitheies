Imports MySql.Data.MySqlClient
Imports System.Globalization
Public Class Form1
    Public monadametr, user, cmdtext, caseselection As String
    Dim dgvrowcounter As Integer = 0
    Public monada, eidos, tmima, userlevel, eidos1, promitheutis As Integer
    Public counter As Integer = 0

    Dim answer As Boolean = True
    Public ektos As Boolean = False
    Dim lst1 As New List(Of Integer)()
    Public lst As New List(Of Integer)()
    Dim col6, col7, col8, col5, col17, col18 As New DataGridViewColumn
    Dim dataGridViewCellStyle1 As New DataGridViewCellStyle
    Dim doubleselected As Boolean = False
    Public multipleselected As Boolean = False
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        AddDatagridView("")
        'gridlayout()
        MyBase.OnLoad(e)
        gridlayout()
    End Sub

    Public WithEvents datagridview1 As DataGridView
    Public WithEvents datagridview2 As DataGridView
    Private Sub AddDatagridView(query As String)

        If datagridview1 Is Nothing Then
            datagridview1 = New DataGridView
            With datagridview1
                .AutoGenerateColumns = True

                .Columns.AddRange(Enumerable.Range(0, 17).Select(Function(x) New DataGridViewTextBoxColumn).ToArray)
                .Rows.AddRange(Enumerable.Range(0, 18).Select(Function(x) New DataGridViewRow).ToArray)
                'End If
                .MultiSelect = False
                .Enabled = False
                '.Columns.Add(New DataGridViewTextBoxColumn)
                .Parent = Me
                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
                .CurrentCell = datagridview1.Rows(0).Cells(0)
                .BeginEdit(True)
                '.EditMode = DataGridViewEditMode.EditProgrammatically
                .Location = New Point(11, 154)
                .Size = New Size(1900, 468)

            End With
        End If

    End Sub
    Public Sub AddDatagridView2(query As String)
        'ΑΥΤΟ ΕΙΝΑΙ ΤΟ ΥΠΟΔΕΙΓΜΑ ΠΟΥ ΘΑ ΠΡΕΠΕΙ ΝΑ ΑΚΟΛΟΥΘΗΣΩ ΟΤΑΝ ΕΡΘΟΥΝ ΟΙ ΤΕΛΕΥΤΑΙΕΣ ΤΙΜΕΣ ΚΑΙ ΠΟΣΟΤΗΤΕΣ ΑΠΟ ΤΗ ΒΑΣΗ. ΙΣΩΣ ΚΑΠΟΙΕΣ ΑΠΟ ΤΙΣ ΣΧΟΛΙΑΣΜΕΝΕΣ ΓΡΑΜΜΕΣ ΠΑΡΑΚΑΤΩ ΧΡΕΙΑΣΤΟΥΝ ΑΠΟΣΧΟΛΙΑΣΜΟ

        If datagridview2 Is Nothing Then
            datagridview2 = New DataGridView

            With datagridview2
                .AutoGenerateColumns = True
                loadgrid(query, datagridview2)
                '.MultiSelect = False
                .Parent = Me
                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize '//////////////////////////
                '.EnableHeadersVisualStyles = False
                '.CurrentCell = datagridview1.Rows(0).Cells(0)
                '.BeginEdit(True)
                '.EditMode = DataGridViewEditMode.EditOnEnter

                .Location = New Point(11, 154) '//////////////////////////////////
                .Size = New Size(1900, 468)
            End With
        End If

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd/MM/yy"
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "dd/MM/yy"
        If tmima <> 100 Then
            ComboBox1.Visible = False

        Else
            runquerycombo("SELECT description FROM tmimata JOIN promusers ON tmimaid=tmima WHERE promusers.task is not null", ComboBox1, "description")
            TextBox3.Visible = False

        End If
        Me.ShowIcon = False
        Me.Text = "SigNet S.A                                                                                                                                                             Αίτημα προμήθειας                                          " & Date.Today.ToLongDateString.ToString
        If userlevel = 0 Or userlevel = 3 Then
            Button8.Enabled = False
        End If
        If userlevel = 3 Then
            Button6.Enabled = False
            Button9.Enabled = False
        End If

    End Sub
    Private Sub gridlayout()
        datagridview1.EnableHeadersVisualStyles = False

        With datagridview1
            .Columns(0).HeaderText = "Kωδικός Είδους"
            .Columns(1).HeaderText = "Περιγραφή Είδους"
            .Columns(2).HeaderText = "Μονάδα Μέτρησης"
            .Columns(3).HeaderText = "Ατιολόγηση Προμήθειας"
            .Columns(4).HeaderText = "Αξιολόγηση Τελευταίας Προμήθειας"
            .Columns(9).HeaderText = "Αιτούμενη Ποσότητα"
            .Columns(10).HeaderText = "Παρατηρήσεις"
            .Columns(5).HeaderText = "Ημ/νία Τελευταίας Προμήθειας"
            .Columns(6).HeaderText = "Ποσότητα Τελευταίας Προμήθειας"
            .Columns(7).HeaderText = "Τιμή Τελευταίας Προμήθειας"
            .Columns(8).HeaderText = "Προμηθευτής"
            .Columns(13).HeaderText = "Προτεινόμενη Τιμή"
            .Columns(14).HeaderText = "Προτεινόμενος Προμηθευτής"

            .Columns(1).Width = 260
            .Columns(2).Width = 73
            .Columns(3).Width = 260
            .Columns(4).Width = 165
            .Columns(7).Width = 72
            .Columns(6).Width = 72
            .Columns(10).Width = 300
            .Columns(8).Width = 150
            .Columns(9).Width = 72
            .Columns(13).Width = 82
            .Columns(14).Width = 150
            .Columns(1).ReadOnly = True
            .Columns(2).ReadOnly = True
            .Columns(11).Visible = False      'eidi ektos arxeiou eidwn

            .Columns(12).Visible = False       'id eidous
            .Columns(15).Visible = False       'promitheutis ektos
            .Columns(16).Visible = False          'id promitheuti 
        End With
        For i = 0 To datagridview1.RowCount - 1
            datagridview1.Rows(i).Cells(11).Value = 0

            For j = 5 To 8
                datagridview1.Columns(j).ReadOnly = True

                datagridview1.Rows(i).Cells(j).Style.BackColor = Color.LightGray
                datagridview1.Columns(j).DefaultCellStyle.SelectionBackColor = Color.Transparent 'datagridview2.DefaultCellStyle.BackColor
                datagridview1.Columns(j).DefaultCellStyle.SelectionForeColor = Color.Transparent ' datagridview2.DefaultCellStyle.ForeColor
                datagridview1.Columns(j).HeaderCell.Style.BackColor = Color.LightGray
            Next
        Next
        For z = 11 To datagridview1.ColumnCount - 1
            datagridview1.Columns(z).ReadOnly = True
        Next
        datagridview1.Columns(10).ReadOnly = False
        If userlevel >= 5 Then
            datagridview1.Columns(13).ReadOnly = False
            datagridview1.Columns(14).ReadOnly = False
        End If
    End Sub
    Public Sub gridlayout2()

        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        dataGridViewCellStyle1.NullValue = False

        With datagridview2
            .Columns(0).HeaderText = "Kωδικός Είδους"
            .Columns(1).HeaderText = "Περιγραφή Είδους"
            .Columns(2).HeaderText = "Μονάδα Μέτρησης"
            .Columns(3).HeaderText = "Αιτιολόγηση Προμήθειας"
            .Columns(4).HeaderText = "Αξιολόγηση Τελευταίας Προμήθειας"

            col5.CellTemplate = New DataGridViewTextBoxCell
            datagridview2.Columns.Insert(5, col5)
            .Columns(5).HeaderText = "Ημ/νία Τελευταίας Προμήθειας"

            col6.CellTemplate = New DataGridViewTextBoxCell
            datagridview2.Columns.Insert(6, col6)
            .Columns(6).HeaderText = "Ποσότητα Τελευταίας Προμήθειας"

            col7.CellTemplate = New DataGridViewTextBoxCell
            datagridview2.Columns.Insert(7, col7)
            .Columns(7).HeaderText = "Τιμή Τελευταίας Προμήθειας"

            col8.CellTemplate = New DataGridViewTextBoxCell
            datagridview2.Columns.Insert(8, col8)
            .Columns(8).HeaderText = "Προμηθευτής"

            .Columns(13).HeaderText = "Αιτούμενη Ποσότητα"
            .Columns(14).HeaderText = "Παρατηρήσεις"
            .Columns(15).HeaderText = "Προτεινόμενος Προμηθευτής"
            .Columns(16).HeaderText = "Προτεινόμενη Τιμή"
            col17.CellTemplate = New DataGridViewCheckBoxCell
            col17.DefaultCellStyle = dataGridViewCellStyle1
            datagridview2.Columns.Insert(17, col17)
            .Columns(17).HeaderText = "Αρχική Έγκριση"
            col18.CellTemplate = New DataGridViewCheckBoxCell
            col18.DefaultCellStyle = dataGridViewCellStyle1
            datagridview2.Columns.Insert(18, col18)
            .Columns(18).HeaderText = "Τελική Έγκριση"
            .Columns(19).HeaderText = "Αρ. Παραγγελίας"
            .Columns(20).HeaderText = "Ημ/νία Παραγγελίας"

            .Columns(1).Width = 240
            '.Columns(2).Width = 73
            .Columns(3).Width = 170
            .Columns(4).Width = 121

            .Columns(13).Width = 70
            .Columns(16).Width = 78
            .Columns(20).Width = 78
            .Columns(1).ReadOnly = True
            .Columns(2).ReadOnly = True
            .Columns(19).ReadOnly = True
            .Columns(20).ReadOnly = True
            .Columns(9).Visible = False 'mi egkekrimena eidi
            .Columns(10).Visible = False 'eidi ektos arxeiou eidwn
            .Columns(11).Visible = False 'id eidous
            .Columns(12).Visible = False 'promitheutis ektos
            '.Columns(16).Width = 80
            datagridview2.ReadOnly = True
            .Columns(5).DefaultCellStyle.Format = "dd/MM/yyyy"
            '.Columns(20).DefaultCellStyle.Format = "dd-MM-yyyy"
            .Columns(21).Visible = False 'id promitheuti
            .Columns(22).Visible = False ' imerominia egkrisis
            .Columns(23).Visible = False 'egkrisi2
            .Columns(24).Visible = False ' id grammis prom
            .Columns(6).DefaultCellStyle.Format = "N2"
            .Columns(13).DefaultCellStyle.Format = "N0"
            .Columns(16).DefaultCellStyle.Format = "N4"
            datagridview2.EnableHeadersVisualStyles = False
            For u = 0 To 22
                .Columns(u).SortMode = DataGridViewColumnSortMode.NotSortable
            Next
            For dgvrowcounter = 0 To datagridview2.RowCount - 1
                .Columns(dgvrowcounter).SortMode = DataGridViewColumnSortMode.NotSortable
                If datagridview2.Rows(dgvrowcounter).Cells(9).Value = 0 Then
                    '.Columns(i).HeaderCell.Style.BackColor = Color.LightGray
                    datagridview2.Rows(dgvrowcounter).Cells(18).Value = False
                    'datagridview2.Rows(dgvrowcounter).Cells(19).ReadOnly = True
                    'datagridview2.Rows(dgvrowcounter).Cells(20).ReadOnly = True
                ElseIf datagridview2.Rows(dgvrowcounter).Cells(9).Value = 1 Then
                    datagridview2.Rows(dgvrowcounter).Cells(18).Value = True
                    datagridview2.Rows(dgvrowcounter).ReadOnly = True
                    'datagridview2.Rows(dgvrowcounter).Cells(19).ReadOnly = False
                    'datagridview2.Rows(dgvrowcounter).Cells(20).ReadOnly = False
                End If
                If datagridview2.Rows(dgvrowcounter).Cells(23).Value = 0 Then
                    '.Columns(i).HeaderCell.Style.BackColor = Color.LightGray
                    datagridview2.Rows(dgvrowcounter).Cells(17).Value = False

                ElseIf datagridview2.Rows(dgvrowcounter).Cells(23).Value = 1 Then
                    datagridview2.Rows(dgvrowcounter).Cells(17).Value = True

                End If
                greywhitecolumns(1, 2, Color.LightGray)

                dgv2backcolor(1, 2, Color.LightGray, Color.Transparent)
                If userlevel = 0 Then

                    greywhitecolumns(0, 0, Color.White)

                    greywhitecolumns(15, 22, Color.LightGray)
                    dgv2backcolor(15, 22, Color.LightGray, Color.Transparent)

                End If
                If userlevel = 5 Then

                    greywhitecolumns(0, 0, Color.White)
                    greywhitecolumns(3, 17, Color.White)

                    greywhitecolumns(18, 18, Color.LightGray)
                    ' datagridview2.Columns(17).ReadOnly = True
                    datagridview2.Columns(18).ReadOnly = True
                    dgv2backcolor(18, 18, Color.LightGray, Color.LightGray)

                    greywhitecolumns(19, 22, Color.White)
                End If
                If userlevel = 3 Then
                    datagridview2.Rows(0).Cells(0).Selected = False

                    greywhitecolumns(0, 16, Color.LightGray)

                    dgv2backcolor(0, 16, Color.LightGray, Color.Transparent)
                    datagridview2.Columns(17).HeaderCell.Style.BackColor = Color.White

                    greywhitecolumns(18, 22, Color.LightGray)

                    dgv2backcolor(18, 22, Color.LightGray, Color.Transparent)
                    'Next
                End If
                For f = 5 To 8
                    datagridview2.Rows(dgvrowcounter).Cells(f).Style.BackColor = Color.LightGray

                Next
            Next

            For k = 5 To 8
                datagridview2.Columns(k).ReadOnly = True
                datagridview2.Columns(k).DefaultCellStyle.SelectionBackColor = Color.Transparent 'datagridview2.DefaultCellStyle.BackColor
                datagridview2.Columns(k).DefaultCellStyle.SelectionForeColor = Color.Transparent ' datagridview2.DefaultCellStyle.ForeColor
                datagridview2.Columns(k).HeaderCell.Style.BackColor = Color.LightGray
            Next

        End With

    End Sub
    Private Sub greywhitecolumns(start As Integer, stop1 As Integer, xroma As Color)
        Dim t As Integer = 0
        For t = start To stop1
            datagridview2.Columns(t).HeaderCell.Style.BackColor = xroma
        Next
    End Sub
    Private Sub dgv2backcolor(start As Integer, stop1 As Integer, xroma1 As Color, xroma2 As Color)
        Dim h As Integer = 0
        For h = start To stop1
            datagridview2.Rows(dgvrowcounter).Cells(h).Style.BackColor = xroma1
            datagridview2.Columns(h).DefaultCellStyle.SelectionBackColor = xroma2
            datagridview2.Columns(h).DefaultCellStyle.SelectionForeColor = xroma2
        Next
    End Sub
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Try
            ektos = False
            If datagridview1 IsNot Nothing Then
                If Button6.BackColor = Color.CornflowerBlue Then

                    If datagridview1.CurrentCell.ColumnIndex = 0 Then
                        datagridview1.CurrentCell.Value = ""
                        Try

                            datagridview1.CommitEdit(DataGridViewDataErrorContexts.Commit)
                            If keyData = Keys.Enter Then     ' YES
                                If String.IsNullOrWhiteSpace(datagridview1.CurrentCell.Value) = True Then
                                    grammi = datagridview1.CurrentRow.Index
                                    Form3.Show()

                                    Exit Function
                                End If
                                If datagridview1.CurrentCell.Value.ToString <> "" Then
                                    loadgrid("Select eidos,code,perigrafi FROM eidi WHERE code Like '" & datagridview1.CurrentCell.Value.ToString & "%'", Form2.DataGridView2)
                                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                                        loadgrid("SELECT id,kwdikos,perigrafi FROM eidiektos WHERE kwdikos LIKE '" & datagridview1.CurrentCell.Value.ToString & "%'", Form2.DataGridView2)
                                        If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                                            MessageBox.Show("Δε βρέθηκαν είδη στο σύστημα")
                                        Else
                                            ektos = True
                                            grammi = datagridview1.CurrentRow.Index
                                            Form2.caseselection = "form1"
                                            Form2.Show()
                                        End If
                                    Else
                                        grammi = datagridview1.CurrentRow.Index
                                        Form2.caseselection = "form1"
                                        Form2.Show()
                                    End If

                                    Return True

                                End If
                            End If

                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    End If
                    If datagridview1.CurrentCell.ColumnIndex = 14 And datagridview1.CurrentRow.Cells(0).Value.ToString <> "" Then
                        datagridview1.CurrentCell.Value = ""
                        Try

                            datagridview1.CommitEdit(DataGridViewDataErrorContexts.Commit)
                            If keyData = Keys.Enter Then     ' YES
                                If datagridview1.CurrentCell.Value.ToString <> "" Then
                                    loadgrid("SELECT pelprom,code,eponymia FROM pelproms WHERE eponymia LIKE '" & datagridview1.CurrentCell.Value.ToString & "%' AND ptype='ΠΡΟΜΗΘΕΥΤΗΣ'", Form2.DataGridView2)
                                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                                        loadgrid("SELECT id,epon,afm FROM pelproms_ektos WHERE epon LIKE '" & datagridview1.CurrentCell.Value.ToString & "%'", Form2.DataGridView2)
                                        If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                                            MessageBox.Show("Δε βρέθηκαν προμηθευτές στο σύστημα")
                                        Else
                                            ektos = True
                                            grammi = datagridview1.CurrentRow.Index
                                            Form2.caseselection = "form1-prom-dgv1"
                                            Form2.Show()
                                        End If
                                    Else
                                        grammi = datagridview1.CurrentRow.Index
                                        Form2.caseselection = "form1-prom-dgv1"
                                        Form2.Show()
                                    End If

                                    Return True
                                Else
                                    Form3.caseselection1 = "form1-prom-dgv1"
                                    grammi = datagridview1.CurrentRow.Index
                                    Form3.Show()
                                    'Form3.ShowDialog()
                                End If
                            End If

                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    End If
                End If
            End If
            If datagridview2 IsNot Nothing Then

                If datagridview2.CurrentCell.ColumnIndex = 0 Then
                    If Button2.BackColor = Color.CornflowerBlue Then
                        Try

                            datagridview2.CommitEdit(DataGridViewDataErrorContexts.Commit)
                            If keyData = Keys.Enter Then     ' YES
                                If datagridview2.CurrentCell.Value.ToString <> "" Then
                                    loadgrid("SELECT eidos,code,perigrafi FROM eidi WHERE code LIKE '" & datagridview2.CurrentCell.Value.ToString & "%'", Form2.DataGridView2)
                                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                                        loadgrid("SELECT id,kwdikos,perigrafi FROM eidiektos WHERE kwdikos LIKE '" & datagridview2.CurrentCell.Value.ToString & "%'", Form2.DataGridView2)
                                        If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                                            MessageBox.Show("Δε βρέθηκαν είδη στο σύστημα")
                                        Else
                                            ektos = True
                                            grammi = datagridview2.CurrentRow.Index
                                            Form2.caseselection = "form1"
                                            Form2.Show()
                                        End If
                                    Else
                                        grammi = datagridview2.CurrentRow.Index
                                        Form2.caseselection = "form1"
                                        Form2.Show()
                                    End If

                                    Return True
                                Else
                                    grammi = datagridview2.CurrentRow.Index
                                    Form3.Show()
                                    'Form3.ShowDialog()
                                End If
                            End If

                        Catch ex As Exception
                            MessageBox.Show(ex.ToString)
                        End Try
                    End If
                End If
                If datagridview2.CurrentCell.ColumnIndex = 15 And datagridview2.CurrentRow.Cells(0).Value.ToString <> "" Then

                    Try

                        datagridview2.CommitEdit(DataGridViewDataErrorContexts.Commit)
                        If keyData = Keys.Enter Then     ' YES
                            If datagridview2.CurrentCell.Value.ToString <> "" Then
                                loadgrid("SELECT pelprom,code,eponymia FROM pelproms WHERE eponymia LIKE '" & datagridview2.CurrentCell.Value.ToString & "%' AND ptype='ΠΡΟΜΗΘΕΥΤΗΣ'", Form2.DataGridView2)
                                If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                                    loadgrid("SELECT id,epon,afm FROM pelproms_ektos WHERE epon LIKE '" & datagridview2.CurrentCell.Value.ToString & "%'", Form2.DataGridView2)
                                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                                        MessageBox.Show("Δε βρέθηκαν προμηθευτές στο σύστημα")
                                    Else
                                        ektos = True
                                        grammi = datagridview2.CurrentRow.Index
                                        Form2.caseselection = "form1-prom"
                                        Form2.Show()
                                    End If
                                Else
                                    grammi = datagridview2.CurrentRow.Index
                                    Form2.caseselection = "form1-prom"
                                    Form2.Show()
                                End If

                                Return True
                            Else
                                Form3.caseselection1 = "form1-prom"
                                grammi = datagridview2.CurrentRow.Index
                                Form3.Show()
                                'Form3.ShowDialog()
                            End If
                        End If

                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If
            End If
        Catch ex As Exception
        End Try
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
    Private Sub datagridview1_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datagridview1.CellClick
        datagridview1.BeginEdit(True)
        If e.ColumnIndex = 13 Or e.ColumnIndex = 14 Then
            If userlevel < 5 Then
                MessageBox.Show("Ο χρήστης δεν έχει δικαιώματα")
            End If
        End If
    End Sub

    Public Sub initializedgv()
        datagridview2.CurrentCell = datagridview2.Rows(0).Cells(1)

        datagridview2.Dispose()

        datagridview2 = Nothing
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        lst.Clear()
        counter = 0

        Dim date1, date2 As String
        TODATE(DateTimePicker1, date1)
        TODATE(DateTimePicker2, date2)
        If userlevel > 0 And ComboBox1.SelectedItem IsNot Nothing Then
            tmima = returnsinglevaluequery("SELECT tmimaid FROM tmimata WHERE description='" & ComboBox1.SelectedItem.ToString & "'")

            dtvaluetoset("SELECT id,date FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "' AND tmima='" & tmima & "' ORDER BY date asc")
            'End If
        End If
        If userlevel > 0 And ComboBox1.SelectedItem = Nothing Then
            ComboBox1.Visible = False
            TextBox3.Visible = True

            dtvaluetoset("SELECT id,date,tmima FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "' ORDER BY date asc")
            'End If
        End If
        If userlevel = 0 Then

            dtvaluetoset("SELECT id,date FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "' AND tmima='" & tmima & "' ORDER BY date asc")
            'End If
        End If
        Dim rowcount As Integer = dt.Rows.Count
        If rowcount = 0 Then
            MessageBox.Show("Δε βρέθηκαν εγγραφές")
            If datagridview1 Is Nothing Then
                initializedgv()
                AddDatagridView("")
                gridlayout()

            End If
            Label10.Visible = False
            Label11.Visible = False
            Label12.Visible = False
            Label13.Visible = False
            Exit Sub
        End If
        Button4.Enabled = True
        Button5.Enabled = True
        If datagridview1 IsNot Nothing Then
            datagridview1.CurrentCell = datagridview1.Rows(0).Cells(1)

            datagridview1.Dispose()

            datagridview1 = Nothing
        ElseIf datagridview2 IsNot Nothing Then
            initializedgv()

        End If
        'cnt = 1
        For i = 0 To rowcount - 1

            lst.Add(dt.Rows(i).Item(0))

        Next

        If dt.Columns.Count = 3 Then

            TextBox3.Text = returnsinglevaluequery("SELECT description FROM tmimata WHERE tmimaid='" & dt.Rows(counter).Item(2) & "'")
        End If

        TextBox2.Text = dt.Rows(counter).Item(1)
        If CheckBox1.Checked = True And eidos1 = 0 And promitheutis = 0 Then
            AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0)")
        ElseIf CheckBox1.Checked = False And eidos1 = 0 And promitheutis = 0 Then
            'If CheckBox3.Checked = False Then
            'AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
            'Else
            'AddDatagridView2("(SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
            'End If
            'AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND parastenglish=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=0) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=0) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=1) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=1) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=1) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=1)")
            AddDatagridView2("(SELECT eidi.code,case when pelproms.parastenglish=1 then eidi.perigr_en else eidi.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,case when pelproms.parastenglish=1 then eidiektos.perig_en else eidiektos.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,case when pelproms_ektos.tim_eng=1 then eidiektos.perig_en else eidiektos.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,case when pelproms_ektos.tim_eng=1 then eidi.perigr_en else eidi.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
        ElseIf CheckBox1.Checked = False And eidos1 = 0 And promitheutis = 0 And CheckBox2.Checked = True Then
                AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND paraggelia IS NULL) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND paraggelia IS NULL) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND paraggelia IS NULL) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND paraggelia IS NULL)")

            ElseIf CheckBox1.Checked = True And eidos1 <> 0 And promitheutis = 0 Then
                AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND supplier_ektos=0 AND egkrisi=0 AND grammesprom.eidos='" & eidos1 & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND supplier_ektos=0 AND egkrisi=0 AND grammesprom.eidos='" & eidos1 & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND supplier_ektos=1 AND egkrisi=0 AND grammesprom.eidos='" & eidos1 & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND supplier_ektos=1 AND egkrisi=0 AND grammesprom.eidos='" & eidos1 & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "'))")
            ElseIf CheckBox1.Checked = True And eidos1 = 0 And promitheutis <> 0 Then
                'AddDatagridView2("SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE supplier='" & promitheutis & "' AND supplier_ektos='" & ektos2 & "' AND egkrisi=1")
                AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND supplier_ektos=0 AND egkrisi=0 AND grammesprom.supplier='" & promitheutis & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND supplier_ektos=0 AND egkrisi=0 AND grammesprom.supplier='" & promitheutis & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND supplier_ektos=1 AND egkrisi=0 AND grammesprom.supplier='" & promitheutis & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND supplier_ektos=1 AND egkrisi=0 AND grammesprom.supplier='" & promitheutis & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "'))")

            ElseIf CheckBox1.Checked = True And eidos1 <> 0 And promitheutis <> 0 Then
                'AddDatagridView2("SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE supplier='" & promitheutis & "' AND grammesprom.eidos='" & eidos1 & "' AND supplier_ektos='" & ektos2 & "' AND ektos='" & ektos1 & "' AND egkrisi=1")
                AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND supplier_ektos=0 AND egkrisi=0 AND grammesprom.supplier='" & promitheutis & "' AND grammesprom.eidos='" & eidos1 & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND supplier_ektos=0 AND egkrisi=0 AND grammesprom.supplier='" & promitheutis & "' AND grammesprom.eidos='" & eidos1 & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND supplier_ektos=1 AND egkrisi=0 AND grammesprom.supplier='" & promitheutis & "' AND grammesprom.eidos='" & eidos1 & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "')) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND supplier_ektos=1 AND egkrisi=0 AND grammesprom.supplier='" & promitheutis & "' AND grammesprom.eidos='" & eidos1 & "' AND promitheia IN (SELECT id FROM promitheies WHERE date>='" & date1 & "' AND date<='" & date2 & "'))")

        End If
        gridlayout2() '(DataGridView3)
        runquery("CREATE TEMPORARY TABLE t1 SELECT kiniseis.imerominia,SUM(grammeskin.posotita),grammeskin.timimon,pelproms.eponymia,grammesprom.eidos FROM grammesprom LEFT JOIN eidi ON grammesprom.eidos=eidi.eidos LEFT JOIN grammeskin ON grammesprom.eidos=grammeskin.eidos LEFT JOIN kiniseis ON grammeskin.kinisi=kiniseis.kinisi LEFT JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom LEFT JOIN parastatika ON kiniseis.parastatiko=parastatika.parastatiko  WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES' GROUP by eidos,imerominia ORDER BY imerominia DESC")
        dtvaluetoset2("SELECT * FROM t1 GROUP BY eidos")
        runquery("DROP TEMPORARY TABLE t1")
        If dt2.Rows.Count > 0 Then

            For x = 0 To datagridview2.RowCount - 1
                For y = 0 To dt2.Rows.Count - 1
                    If datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And datagridview2.Rows(x).Cells(10).Value = 0 Then
                        datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                        datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                        datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                        datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                    End If
                Next
            Next
        End If
        runquery("create temporary table t1(select imerominia_egkrisis,posotita,offered_price,pelproms.eponymia,eidos from grammesprom join pelproms on pelprom=supplier where eidos in (select eidos from grammesprom where promitheia='" & lst(0) & "') and ektos=1 and supplier_ektos=0 and offered_price is not null and imerominia_egkrisis is not null) union (select imerominia_egkrisis,posotita,offered_price,pelproms_ektos.epon,eidos from grammesprom join pelproms_ektos on pelproms_ektos.id=supplier where eidos in (select eidos from grammesprom where promitheia='" & lst(0) & "') and ektos=1 and supplier_ektos=1 and offered_price is not null and imerominia_egkrisis is not null) order by imerominia_egkrisis desc")
        dtvaluetoset2("select * from t1 group by eidos")
        runquery("drop temporary table t1")
        If dt2.Rows.Count > 0 Then

            For x = 0 To datagridview2.RowCount - 1
                For y = 0 To dt2.Rows.Count - 1
                    If datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And datagridview2.Rows(x).Cells(10).Value = 1 Then
                        datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                        datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                        datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                        datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                    End If
                Next
            Next
        End If
        If eidos1 <> 0 Or promitheutis <> 0 Then
            Button2.Enabled = False
        End If
        Label10.Visible = True
        Label11.Text = 1
        Label11.Visible = True
        Label12.Visible = True
        Label13.Visible = True
        Label13.Text = lst.Count
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button7.Enabled = True
        If datagridview2 IsNot Nothing Then
            Button2.BackColor = Color.CornflowerBlue
            Button6.Enabled = False
            Button3.Enabled = False
            Button1.Enabled = True
            datagridview2.ReadOnly = False
            Button9.Enabled = False
            TextBox4.Enabled = False
            TextBox5.Enabled = False
            For i = 0 To datagridview2.RowCount - 1

                If datagridview2.Rows(i).Cells(9).Value = 0 Then
                    '.Columns(i).HeaderCell.Style.BackColor = Color.LightGray
                    datagridview2.Rows(i).Cells(18).Value = False
                    datagridview2.Rows(i).Cells(19).ReadOnly = True
                    datagridview2.Rows(i).Cells(20).ReadOnly = True
                ElseIf datagridview2.Rows(i).Cells(9).Value = 1 Then
                    datagridview2.Rows(i).Cells(18).Value = True
                    datagridview2.Rows(i).ReadOnly = True
                    datagridview2.Rows(i).Cells(19).ReadOnly = False
                    datagridview2.Rows(i).Cells(20).ReadOnly = False
                End If
                'If datagridview2.Rows(i).Cells(23).Value = 1 Then
                '    datagridview2.Rows(i).ReadOnly = True

                'End If
            Next
            For k = 5 To 8
                datagridview2.Columns(k).ReadOnly = True
            Next
            If userlevel = 0 Then
                For t = 15 To 19
                    datagridview2.Columns(t).ReadOnly = True
                Next
            End If
            If userlevel = 3 Then
                For p = 0 To 16
                    datagridview2.Columns(p).ReadOnly = True
                Next

                For r = 18 To 22
                    datagridview2.Columns(r).ReadOnly = True
                Next
            End If
            If userlevel = 5 Then
                '    For x = 17 To 18
                datagridview2.Columns(18).ReadOnly = True
                '    Next
            End If
            datagridview2.Columns(1).ReadOnly = True
                datagridview2.Columns(2).ReadOnly = True
                datagridview2.Columns(19).ReadOnly = True
                datagridview2.Columns(20).ReadOnly = True
                If datagridview2.CurrentRow.Cells(15).Value.ToString <> "" Or datagridview2.CurrentRow.Cells(16).Value.ToString <> "" Then
                    datagridview2.CurrentRow.Cells(6).ReadOnly = True
                End If
                Dim rc As Integer = datagridview2.Rows().Count
                datagridview2.Rows(rc - 1).ReadOnly = True
                datagridview2.Rows(rc - 1).Cells(0).ReadOnly = False
            End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox2.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        Label10.Visible = False
        Label11.Visible = False
        Label12.Visible = False
        Label13.Visible = False
        eidos1 = 0
        promitheutis = 0
        Button6.BackColor = Color.CornflowerBlue
        Button2.Enabled = False
        Button3.Enabled = False
        Button7.Enabled = True
        If datagridview1 Is Nothing Then
            initializedgv()
            AddDatagridView("")
            gridlayout()

        End If
        If userlevel >= 5 Then
            ComboBox1.Visible = True
            TextBox3.Visible = False
        End If
        datagridview1.Enabled = True
        Button1.Enabled = True
        lst.Clear()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        Form5.Show()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim result As DialogResult = MessageBox.Show("Θέλετε να προxωρήσετε στη διαγραφή;", "", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then

            If lst.Count > 0 Then
                If returnsinglevaluequery("SELECT count(egkrisi) FROM grammesprom WHERE promitheia='" & lst(counter) & "' AND egkrisi=1") > 0 Then
                    MessageBox.Show("Δε μπορεί να πραγματοποιηθεί ή διαγραφή γιατί ορισμένα είδη έχουν εγκριθεί")
                    Exit Sub
                End If
                cmdtext = "DELETE FROM promitheies WHERE id='" & lst(counter) & "';"
                runquery("DELETE FROM grammesprom WHERE promitheia='" & lst(counter) & "';" & cmdtext)
                If queryerror = False Then

                    MessageBox.Show("Η διαγραφή πραγματοποιήθηκε")
                    lst.RemoveAt(counter)
                End If
                If counter = 0 Then
                    Button3.PerformClick()
                Else
                    Button4.PerformClick()
                    Label13.Text = lst.Count '- 1
                End If

            Else
                    MessageBox.Show("Δε βρέθηκαν δεδομένα για διαγραφή")
            End If
        End If

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Button6.BackColor = Color.LightGray
        Button2.BackColor = Color.LightGray
        Button2.Enabled = True
        If userlevel <> 3 Then
            Button6.Enabled = True
        End If
        Button3.Enabled = True
        If datagridview1 IsNot Nothing Then
            For i = 0 To 18
                For j = 0 To 16
                    datagridview1.Rows(i).Cells(j).Value = String.Empty
                Next
            Next
            datagridview1.Enabled = False
        End If
        If datagridview2 IsNot Nothing Then
            counter = counter + 1
            Button4.PerformClick()

        End If
        Button7.Enabled = False
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim lstlength As Integer = lst.Count


        If lstlength > 1 And counter < lstlength - 1 Then

            counter = counter + 1
            initializedgv()
            TextBox2.Text = dt.Rows(counter).Item(1)
            If CheckBox1.Checked Then
                AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0)")

            Else
                'AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND parastenglish=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=0) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=0) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=1) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=1) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=1) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=1)")
                AddDatagridView2("(SELECT eidi.code,case when pelproms.parastenglish=1 then eidi.perigr_en else eidi.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,case when pelproms.parastenglish=1 then eidiektos.perig_en else eidiektos.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,case when pelproms_ektos.tim_eng=1 then eidiektos.perig_en else eidiektos.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,case when pelproms_ektos.tim_eng=1 then eidi.perigr_en else eidi.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")

                'If CheckBox3.Checked = False Then
                'AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
                'Else
                'AddDatagridView2("(SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
                'End If
            End If
            If dt.Columns.Count = 3 Then

                TextBox3.Text = returnsinglevaluequery("SELECT description FROM tmimata WHERE tmimaid='" & dt.Rows(counter).Item(2) & "'")
            End If

            gridlayout2()
            runquery("CREATE TEMPORARY TABLE t1 SELECT kiniseis.imerominia,SUM(grammeskin.posotita),grammeskin.timimon,pelproms.eponymia,grammesprom.eidos FROM grammesprom LEFT JOIN eidi ON grammesprom.eidos=eidi.eidos LEFT JOIN grammeskin ON grammesprom.eidos=grammeskin.eidos LEFT JOIN kiniseis ON grammeskin.kinisi=kiniseis.kinisi LEFT JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom LEFT JOIN parastatika ON kiniseis.parastatiko=parastatika.parastatiko  WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES' GROUP by eidos,imerominia ORDER BY imerominia DESC")
            dtvaluetoset2("SELECT * FROM t1 GROUP BY eidos")
            runquery("DROP TEMPORARY TABLE t1")

            If dt2.Rows.Count > 0 Then

                For x = 0 To datagridview2.RowCount - 1
                    For y = 0 To dt2.Rows.Count - 1
                        If datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And datagridview2.Rows(x).Cells(10).Value = 0 Then
                            datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                            datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                            datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                            datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                        End If
                    Next
                Next
            End If
            runquery("create temporary table t1(select imerominia_egkrisis,posotita,offered_price,pelproms.eponymia,eidos from grammesprom join pelproms on pelprom=supplier where eidos in (select eidos from grammesprom where promitheia='" & lst(counter) & "') and ektos=1 and supplier_ektos=0 and offered_price is not null and imerominia_egkrisis is not null) union (select imerominia_egkrisis,posotita,offered_price,pelproms_ektos.epon,eidos from grammesprom join pelproms_ektos on pelproms_ektos.id=supplier where eidos in (select eidos from grammesprom where promitheia='" & lst(counter) & "') and ektos=1 and supplier_ektos=1 and offered_price is not null and imerominia_egkrisis is not null) order by imerominia_egkrisis desc")
            dtvaluetoset2("select * from t1 group by eidos")
            runquery("drop temporary table t1")
            If dt2.Rows.Count > 0 Then

                For x = 0 To datagridview2.RowCount - 1
                    For y = 0 To dt2.Rows.Count - 1
                        If datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And datagridview2.Rows(x).Cells(10).Value = 1 Then
                            datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                            datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                            datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                            datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                        End If
                    Next
                Next
            End If
            Label11.Text = counter + 1
        End If
        lst1.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If counter > 0 Then

            counter = counter - 1
            initializedgv()
            TextBox2.Text = dt.Rows(counter).Item(1)
            If CheckBox1.Checked Then
                AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0)")

            Else
                'If CheckBox3.Checked = False Then
                '    AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
                'Else
                '    AddDatagridView2("(SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
                'End If
                'AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND parastenglish=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=0) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=0) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=1) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=1) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=1) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND tim_eng=1)")
                AddDatagridView2("(SELECT eidi.code,case when pelproms.parastenglish=1 then eidi.perigr_en else eidi.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,case when pelproms.parastenglish=1 then eidiektos.perig_en else eidiektos.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,case when pelproms_ektos.tim_eng=1 then eidiektos.perig_en else eidiektos.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,case when pelproms_ektos.tim_eng=1 then eidi.perigr_en else eidi.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")

            End If
            If dt.Columns.Count = 3 Then

                TextBox3.Text = returnsinglevaluequery("SELECT description FROM tmimata WHERE tmimaid='" & dt.Rows(counter).Item(2) & "'")
            End If

            gridlayout2()
            runquery("CREATE TEMPORARY TABLE t1 SELECT kiniseis.imerominia,SUM(grammeskin.posotita),grammeskin.timimon,pelproms.eponymia,grammesprom.eidos FROM grammesprom LEFT JOIN eidi ON grammesprom.eidos=eidi.eidos LEFT JOIN grammeskin ON grammesprom.eidos=grammeskin.eidos LEFT JOIN kiniseis ON grammeskin.kinisi=kiniseis.kinisi LEFT JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom LEFT JOIN parastatika ON kiniseis.parastatiko=parastatika.parastatiko  WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES' GROUP by eidos,imerominia ORDER BY imerominia DESC")
            dtvaluetoset2("SELECT * FROM t1 GROUP BY eidos")
            runquery("DROP TEMPORARY TABLE t1")
            If dt2.Rows.Count > 0 Then

                For x = 0 To datagridview2.RowCount - 1
                    For y = 0 To dt2.Rows.Count - 1
                        If datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And datagridview2.Rows(x).Cells(10).Value = 0 Then
                            datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                            datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                            datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                            datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                        End If
                    Next
                Next
            End If
            runquery("create temporary table t1(select imerominia_egkrisis,posotita,offered_price,pelproms.eponymia,eidos from grammesprom join pelproms on pelprom=supplier where eidos in (select eidos from grammesprom where promitheia='" & lst(counter) & "') and ektos=1 and supplier_ektos=0 and offered_price is not null and imerominia_egkrisis is not null) union (select imerominia_egkrisis,posotita,offered_price,pelproms_ektos.epon,eidos from grammesprom join pelproms_ektos on pelproms_ektos.id=supplier where eidos in (select eidos from grammesprom where promitheia='" & lst(counter) & "') and ektos=1 and supplier_ektos=1 and offered_price is not null and imerominia_egkrisis is not null) order by imerominia_egkrisis desc")
            dtvaluetoset2("select * from t1 group by eidos")
            runquery("drop temporary table t1")
            If dt2.Rows.Count > 0 Then

                For x = 0 To datagridview2.RowCount - 1
                    For y = 0 To dt2.Rows.Count - 1
                        If datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And datagridview2.Rows(x).Cells(10).Value = 1 Then
                            datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                            datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                            datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                            datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                        End If
                    Next
                Next
            End If
            Label11.Text = counter + 1
        End If
        lst1.Clear()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Form6.Close()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        Dim lastselectedrowindex As Integer

        If lst1.Count > 0 Then
            Form7.pelprom = lst1.Item(0)

        End If
        If Form7.pelprom <> 0 Then

            For Each cell1 As DataGridViewCell In datagridview2.SelectedCells
                lastselectedrowindex = cell1.RowIndex
                If String.IsNullOrWhiteSpace(datagridview2.Rows(lastselectedrowindex).Cells(19).Value.ToString) = False Then
                    MessageBox.Show("Στην επιλογή σας υπάρχει είδος που έχει παραγγελθεί")
                    Exit Sub
                End If
                Dim mikos As Integer = datagridview2.Rows(lastselectedrowindex).Cells(1).Value.ToString.Length
                Dim mikos2 As Integer = datagridview2.Rows(lastselectedrowindex).Cells(2).Value.ToString.Length
                Form7.idlist.Add(datagridview2.Rows(lastselectedrowindex).Cells(24).Value)
                Form7.mailbody = Form7.mailbody & "<tr><td>" & datagridview2.Rows(lastselectedrowindex).Cells(1).Value.ToString & "</td>" & "<td>" & datagridview2.Rows(lastselectedrowindex).Cells(13).Value.ToString & "</td>" & "<td>" & datagridview2.Rows(lastselectedrowindex).Cells(16).Value.ToString & "</td></tr>"

                If datagridview2.Rows(lastselectedrowindex).Cells(1).Value.ToString.Length < 23 Then

                    Form7.paraggelia = Form7.paraggelia & datagridview2.Rows(lastselectedrowindex).Cells(1).Value.ToString.PadRight(59 - mikos) & vbTab & datagridview2.Rows(lastselectedrowindex).Cells(13).Value.ToString.PadRight(58 - mikos2) & vbTab & datagridview2.Rows(lastselectedrowindex).Cells(16).Value.ToString & vbCrLf

                    'Form7.mailbody = Form7.mailbody & "<tr><td>" & datagridview2.Rows(lastselectedrowindex).Cells(1).Value.ToString & "</td>" & "<td>" & datagridview2.Rows(lastselectedrowindex).Cells(13).Value.ToString & "</td>" & "<td>" & datagridview2.Rows(lastselectedrowindex).Cells(16).Value.ToString & "</td></tr>"

                Else
                    'Form7.paraggelia = Form7.paraggelia & datagridview2.Rows(lastselectedrowindex).Cells(1).Value.ToString & vbTab & datagridview2.Rows(lastselectedrowindex).Cells(13).Value.ToString & vbTab & vbTab & vbTab & datagridview2.Rows(lastselectedrowindex).Cells(16).Value.ToString & vbCrLf
                    Form7.paraggelia = Form7.paraggelia & datagridview2.Rows(lastselectedrowindex).Cells(1).Value.ToString.PadRight(59 - mikos) & vbTab & datagridview2.Rows(lastselectedrowindex).Cells(13).Value.ToString.PadRight(58 - mikos2) & vbTab & datagridview2.Rows(lastselectedrowindex).Cells(16).Value.ToString & vbCrLf

                End If

            Next
            Form7.ektos = datagridview2.Rows(lastselectedrowindex).Cells(12).Value
            'If CheckBox3.Checked = True Then
            '    Form7.agglika = True
            'End If
            lst1.Clear()
            Form7.Show()

        End If
    End Sub

    Private Function plithosgrammwn(dgv As DataGridView) As Integer
        Dim plithos As Integer
        If dgv Is datagridview1 Then

            For i = 0 To 18
                If dgv.Rows(i).Cells(0).Value = "" Then
                    plithos = i
                    i = 19
                End If
            Next
            plithos = plithos - 1
        End If
        If dgv Is datagridview2 Then
            Dim kelli As Object
            For i = 0 To dgv.Rows.Count

                kelli = dgv.Rows(i).Cells(0).Value

                If TypeOf (kelli) Is String Then
                    ' MessageBox.Show("fdsf")
                    If dgv.Rows(i).Cells(0).Value = "" Then
                        plithos = i
                        i = 19
                    End If
                Else
                    plithos = i
                    i = 19
                End If
            Next
            plithos = plithos - 1
        End If
        Return plithos
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cmdtext = ""
        Dim cmd As MySqlCommand
        If ComboBox1.Visible = True And ComboBox1.SelectedItem = Nothing Then
            MessageBox.Show("Δεν έχετε επιλέξει τμήμα")
            Exit Sub
        End If

        If Button6.BackColor = Color.CornflowerBlue Then
            For i = 0 To plithosgrammwn(datagridview1)

                If datagridview1.Rows(i).Cells(9).Value = "" Then
                    MessageBox.Show("Έχετε αφήσει ασυμπλήρωτο το πεδίο της ποσότητας στη γραμμή " & i + 1)
                    Exit Sub
                End If
                If IsNumeric(datagridview1.Rows(i).Cells(9).Value) = False Then
                    MessageBox.Show("Eλέγξτε τις τιμές στο πεδίο της ποσότητας που προσπαθείτε να καταχωρήσετε στη γραμμή  " & i + 1)
                    Exit Sub
                End If

            Next
            Dim dat As Date = Today
            TODATE(DateTimePicker3, date5)
            If ComboBox1.Visible = True Then
                tmima = returnsinglevaluequery("SELECT tmimaid FROM tmimata WHERE description='" & ComboBox1.SelectedItem.ToString & "'")
            End If
            Dim promitheia As Integer = returnsinglevaluequery("SELECT id FROM promitheies WHERE tmima='" & tmima & "' AND date='" & date5 & "'")
            If promitheia = 0 Then
                runquery("INSERT INTO promitheies(tmima,date) VALUES('" & tmima & "','" & date5 & "')")
            Else
                MessageBox.Show("Υπάρχει ήδη καταχώρηση για το συγκεκριμένο τμήμα για αυτή την ημερομηνία" & vbCrLf & "Εάν επιθυμείτε να συμπληρώσετε νέο είδος επιλέξτε μεταβολή της παραγγελίας")
                Exit Sub
            End If
            promitheia = returnsinglevaluequery("SELECT id FROM promitheies WHERE tmima='" & tmima & "' AND date='" & date5 & "'")
            'For i = 0 To plithosgrammwn(datagridview1)
            '    eidos = datagridview1.Rows(i).Cells(12).Value
            '    cmdtext = cmdtext & "INSERT INTO grammesprom(promitheia,eidos,ektos,aitiologisi,axiologisi,posotita,paratiriseis,supplier,supplier_ektos,offered_price) VALUES('" & promitheia & "','" & eidos & "','" & datagridview1.Rows(i).Cells(11).Value & "','" & datagridview1.Rows(i).Cells(3).Value & "','" & datagridview1.Rows(i).Cells(4).Value & "','" & datagridview1.Rows(i).Cells(9).Value & "','" & datagridview1.Rows(i).Cells(10).Value & "','" & datagridview1.Rows(i).Cells(16).Value & "','" & datagridview1.Rows(i).Cells(15).Value & "','" & datagridview1.Rows(i).Cells(13).Value & "' );"

            'Next
            'If cmdtext <> "" Then
            '    runquery(cmdtext)
            'Else
            '    queryerror = True
            '    'Exit Sub
            'End If
            cmdtext = cmdtext & "INSERT INTO grammesprom(promitheia,eidos,ektos,aitiologisi,axiologisi,posotita,paratiriseis,supplier,supplier_ektos,offered_price) VALUES('" & promitheia & "',@eidos,@ektos,@aitiologisi,@axiologisi,@posotita,@paratiriseis,@supplier,@supplier_ektos,@price);"

            mysqlcon.Open()
            Try
                trans = mysqlcon.BeginTransaction
                For i = 0 To plithosgrammwn(datagridview1)
                    cmd = New MySqlCommand(cmdtext, mysqlcon)
                    cmd.Transaction = trans
                    eidos = datagridview1.Rows(i).Cells(12).Value

                    If datagridview1.Rows(i).Cells(14).Value = "" Then
                        cmd.Parameters.Add("@supplier", MySqlDbType.Int32).Value = 0
                    Else
                        cmd.Parameters.Add("@supplier", MySqlDbType.Int32).Value = datagridview1.Rows(i).Cells(16).Value
                    End If

                    If String.IsNullOrWhiteSpace(datagridview1.Rows(i).Cells(15).Value) = True Then
                        cmd.Parameters.Add("@supplier_ektos", MySqlDbType.Int16).Value = 0
                    Else
                        cmd.Parameters.Add("@supplier_ektos", MySqlDbType.Int16).Value = datagridview1.Rows(i).Cells(15).Value
                    End If
                    If datagridview1.Rows(i).Cells(13).Value = "" Then
                        cmd.Parameters.Add("@price", MySqlDbType.Decimal).Value = DBNull.Value
                    Else
                        cmd.Parameters.Add("@price", MySqlDbType.Decimal).Value = datagridview1.Rows(i).Cells(13).Value 'todecimalvalue(datagridview2.Rows(i).Cells(16))

                    End If
                    cmd.Parameters.Add("@eidos", MySqlDbType.Int32).Value = eidos

                    cmd.Parameters.Add("@ektos", MySqlDbType.Int16).Value = datagridview1.Rows(i).Cells(11).Value
                    cmd.Parameters.Add("@aitiologisi", MySqlDbType.VarChar).Value = datagridview1.Rows(i).Cells(3).Value
                    cmd.Parameters.Add("@axiologisi", MySqlDbType.VarChar).Value = datagridview1.Rows(i).Cells(4).Value
                    cmd.Parameters.Add("@posotita", MySqlDbType.Int32).Value = datagridview1.Rows(i).Cells(9).Value
                    cmd.Parameters.Add("@paratiriseis", MySqlDbType.VarChar).Value = datagridview1.Rows(i).Cells(10).Value
                    cmd.ExecuteNonQuery()

                Next
                trans.Commit()
                mysqlcon.Close()

            Catch ex As Exception
                queryerror = True
                trans.Rollback()
                MessageBox.Show(ex.ToString)
            End Try
            If queryerror = False Then
                Form6.Label1.Text = "Επιτυχής Καταχώρηση"
                Timer1.Enabled = True
                Form6.Show()
                datagridview1.Dispose()
                datagridview1 = Nothing
                AddDatagridView("")
                ComboBox1.SelectedItem = Nothing
                gridlayout()
            Else
                runquery("DELETE FROM promitheies WHERE id='" & promitheia & "'")
            End If
            Button6.BackColor = Color.LightGray
        End If
        If Button2.BackColor = Color.CornflowerBlue Then

            For i = 0 To plithosgrammwn(datagridview2)

                If datagridview2.Rows(i).Cells(13).Value.ToString = "" Then
                    MessageBox.Show("Έχετε αφήσει ασυμπλήρωτο το πεδίο της ποσότητας στη γραμμή " & i + 1)
                    Exit Sub
                End If
                If IsNumeric(datagridview2.Rows(i).Cells(13).Value) = False Then
                    MessageBox.Show("Eλέγξτε τις τιμές στο πεδίο της ποσότητας που προσπαθείτε να καταχωρήσετε στη γραμμή  " & i + 1)
                    Exit Sub
                End If
                'If datagridview2.Rows(i).Cells(19).Value.ToString <> "" And datagridview2.Rows(i).Cells(20).Value.ToString = "" Then
                '    MessageBox.Show("Στη γραμμή " & i + 1 & " έχετε συμπλρώσει αριθμό παραγγελίας χωρίς να έχετε συμπληρώσει ημερομηνία παραγγελίας")
                '    Exit Sub
                'End If
                'If datagridview2.Rows(i).Cells(19).Value.ToString = "" And datagridview2.Rows(i).Cells(20).Value.ToString <> "" Then
                '    MessageBox.Show("Στη γραμμή " & i + 1 & " έχετε συμπλρώσει ημερομηνία παραγγελίας χωρίς να έχετε συμπληρώσει αριθμό παραγγελίας")
                '    Exit Sub
                'End If

            Next
            'mysqlcon = New MySqlConnection
            ''mysqlcon.ConnectionString = "server=localhost;userid=root;password=12345;database=sigmix"
            'mysqlcon.ConnectionString = constr
            Dim egkrisi, egkrisi2 As Integer
            '
            cmdtext = "DELETE FROM grammesprom WHERE promitheia='" & lst(counter) & "' AND id=@id;" & "INSERT INTO grammesprom(promitheia,eidos,ektos,aitiologisi,axiologisi,posotita,paratiriseis,egkrisi,supplier,supplier_ektos,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2) VALUES('" & lst(counter) & "',@eidos,@ektos,@aitiologisi,@axiologisi,@posotita,@paratiriseis,@egkrisi,@supplier,@supplier_ektos,@price,@paraggelia,@imerominia_paraggelias,@imerominia_egkrisis,@egkrisi2);"

            mysqlcon.Open()
            Try
                trans = mysqlcon.BeginTransaction

                For i = 0 To plithosgrammwn(datagridview2)

                    'If i > 0 Then
                    '    cmdtext = "INSERT INTO grammesprom(promitheia,eidos,ektos,aitiologisi,axiologisi,posotita,paratiriseis,egkrisi,supplier,supplier_ektos,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2) VALUES('" & lst(counter) & "',@eidos,@ektos,@aitiologisi,@axiologisi,@posotita,@paratiriseis,@egkrisi,@supplier,@supplier_ektos,@price,@paraggelia,@imerominia_paraggelias,@imerominia_egkrisis,@egkrisi2);"

                    'End If
                    cmd = New MySqlCommand(cmdtext, mysqlcon)
                    cmd.Transaction = trans
                    If datagridview2.Rows(i).Cells(18).Value = False Then
                        egkrisi = 0
                    Else
                        egkrisi = 1
                    End If
                    If datagridview2.Rows(i).Cells(17).Value = False Then
                        egkrisi2 = 0
                    Else
                        egkrisi2 = 1
                    End If
                    If datagridview2.Rows(i).Cells(16).Value.ToString = "" Then
                        cmd.Parameters.Add("@price", MySqlDbType.Decimal).Value = DBNull.Value
                    Else '
                        cmd.Parameters.Add("@price", MySqlDbType.Decimal).Value = datagridview2.Rows(i).Cells(16).Value 'todecimalvalue(datagridview2.Rows(i).Cells(16))

                    End If
                    If datagridview2.Rows(i).Cells(15).Value.ToString = "" Then
                        datagridview2.Rows(i).Cells(21).Value = 0
                    End If

                    cmd.Parameters.Add("@supplier", MySqlDbType.Int32).Value = datagridview2.Rows(i).Cells(21).Value

                    If datagridview2.Rows(i).Cells(12).Value.ToString = "" Then
                        datagridview2.Rows(i).Cells(12).Value = 0
                    End If
                    eidos = datagridview2.Rows(i).Cells(11).Value

                    If datagridview2.Rows(i).Cells(20).Value.ToString = "" Then
                        cmd.Parameters.Add("@imerominia_paraggelias", MySqlDbType.Date).Value = DBNull.Value

                    Else
                        cmd.Parameters.Add("@imerominia_paraggelias", MySqlDbType.Date).Value = datagridview2.Rows(i).Cells(20).Value

                    End If
                    If datagridview2.Rows(i).Cells(22).Value.ToString = "" Or egkrisi = 0 Then
                        cmd.Parameters.Add("@imerominia_egkrisis", MySqlDbType.Date).Value = DBNull.Value

                    ElseIf egkrisi = 1 Then
                        cmd.Parameters.Add("@imerominia_egkrisis", MySqlDbType.Date).Value = datagridview2.Rows(i).Cells(22).Value

                    End If
                    cmd.Parameters.Add("@eidos", MySqlDbType.Int32).Value = eidos

                    cmd.Parameters.Add("@ektos", MySqlDbType.Int16).Value = datagridview2.Rows(i).Cells(10).Value
                    cmd.Parameters.Add("@aitiologisi", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(3).Value
                    cmd.Parameters.Add("@axiologisi", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(4).Value
                    cmd.Parameters.Add("@posotita", MySqlDbType.Int32).Value = datagridview2.Rows(i).Cells(13).Value
                    cmd.Parameters.Add("@paratiriseis", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(14).Value
                    cmd.Parameters.Add("@egkrisi", MySqlDbType.Int16).Value = egkrisi

                    cmd.Parameters.Add("@supplier_ektos", MySqlDbType.Int16).Value = datagridview2.Rows(i).Cells(12).Value
                    cmd.Parameters.Add("@paraggelia", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(19).Value
                    cmd.Parameters.Add("@egkrisi2", MySqlDbType.Int16).Value = egkrisi2
                    cmd.Parameters.Add("@id", MySqlDbType.Int16).Value = datagridview2.Rows(i).Cells(24).Value
                    cmd.ExecuteNonQuery()

                Next
                trans.Commit()
                mysqlcon.Close()

            Catch ex As Exception
                queryerror = True
                trans.Rollback()
                MessageBox.Show(ex.ToString)
            End Try
            If queryerror = False Then
                Form6.Label1.Text = "Επιτυχής Καταχώρηση"
                Timer1.Enabled = True
                Form6.Show()
                datagridview2.Dispose()
                datagridview2 = Nothing
            End If
            'AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
            If CheckBox1.Checked = True And eidos1 = 0 And promitheutis = 0 Then
                AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0)")
            ElseIf CheckBox1.Checked = False And eidos1 = 0 And promitheutis = 0 Then
                AddDatagridView2("(SELECT eidi.code,case when pelproms.parastenglish=1 then eidi.perigr_en else eidi.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,case when pelproms.parastenglish=1 then eidiektos.perig_en else eidiektos.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,case when pelproms_ektos.tim_eng=1 then eidiektos.perig_en else eidiektos.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,case when pelproms_ektos.tim_eng=1 then eidi.perigr_en else eidi.perigrafi end,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1)")
            ElseIf CheckBox1.Checked = False And eidos1 = 0 And promitheutis = 0 And CheckBox2.Checked = True Then
                AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND paraggelia IS NULL) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND paraggelia IS NULL) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND paraggelia IS NULL) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND paraggelia IS NULL)")
            End If
            gridlayout2()
            runquery("CREATE TEMPORARY TABLE t1 SELECT kiniseis.imerominia,SUM(grammeskin.posotita),grammeskin.timimon,pelproms.eponymia,grammesprom.eidos FROM grammesprom LEFT JOIN eidi ON grammesprom.eidos=eidi.eidos LEFT JOIN grammeskin ON grammesprom.eidos=grammeskin.eidos LEFT JOIN kiniseis ON grammeskin.kinisi=kiniseis.kinisi LEFT JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom LEFT JOIN parastatika ON kiniseis.parastatiko=parastatika.parastatiko  WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES' GROUP by eidos,imerominia ORDER BY imerominia DESC")
            dtvaluetoset2("SELECT * FROM t1 GROUP BY eidos")
            runquery("DROP TEMPORARY TABLE t1")
            If dt2.Rows.Count > 0 Then

                For x = 0 To datagridview2.RowCount - 1
                    For y = 0 To dt2.Rows.Count - 1
                        If datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And datagridview2.Rows(x).Cells(10).Value <> 1 Then
                            datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                            datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                            datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                            datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                        End If
                    Next
                Next
            End If

            runquery("create temporary table t1(select imerominia_egkrisis,posotita,offered_price,pelproms.eponymia,eidos from grammesprom join pelproms on pelprom=supplier where eidos in (select eidos from grammesprom where promitheia='" & lst(counter) & "') and ektos=1 and supplier_ektos=0 and offered_price is not null and imerominia_egkrisis is not null) union (select imerominia_egkrisis,posotita,offered_price,pelproms_ektos.epon,eidos from grammesprom join pelproms_ektos on pelproms_ektos.id=supplier where eidos in (select eidos from grammesprom where promitheia='" & lst(counter) & "') and ektos=1 and supplier_ektos=1 and offered_price is not null and imerominia_egkrisis is not null) order by imerominia_egkrisis desc")
            dtvaluetoset2("select * from t1 group by eidos")
            runquery("drop temporary table t1")
            If dt2.Rows.Count > 0 Then

                For x = 0 To datagridview2.RowCount - 1
                    For y = 0 To dt2.Rows.Count - 1
                        If datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And datagridview2.Rows(x).Cells(10).Value = 1 Then
                            datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                            datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                            datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                            datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                        End If
                    Next
                Next
            End If
        End If
        Button2.BackColor = Color.LightGray

        Button2.Enabled = True
        If userlevel > 3 Then
            Button6.Enabled = True
        End If
        Button3.Enabled = True
        Button1.Enabled = False

        If userlevel > 3 Then
            Button9.Enabled = True
        End If
        TextBox4.Enabled = True
        TextBox5.Enabled = True
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles datagridview1.EditingControlShowing

        If datagridview1.CurrentCell.ColumnIndex = 9 Then

            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        End If
        If datagridview1.CurrentCell.ColumnIndex <> 9 Then

            RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        End If
        If datagridview1.CurrentCell.ColumnIndex = 13 Then

            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress3
        End If
        If datagridview1.CurrentCell.ColumnIndex <> 13 Then

            RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress3
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            CheckBox2.Checked = True
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        promitheutis = 0
        eidos1 = 0
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub

    Private Sub DataGridView2_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles datagridview2.EditingControlShowing

        If datagridview2.CurrentCell.ColumnIndex = 13 Then

            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        End If
        If datagridview2.CurrentCell.ColumnIndex <> 13 Then

            RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        End If
        If datagridview2.CurrentCell.ColumnIndex = 16 Then

            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress3
        End If
        If datagridview2.CurrentCell.ColumnIndex <> 16 Then

            RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress3
        End If
        'If datagridview2.CurrentCell.ColumnIndex = 20 Then

        '    AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress2
        'End If
        'If datagridview2.CurrentCell.ColumnIndex <> 20 Then

        '    RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress2
        'End If
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        If Char.IsDigit(CChar(CStr(e.KeyChar))) = False And e.KeyChar <> ControlChars.Back Then e.Handled = True

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        runquery("TRUNCATE rpt1")
        'runquery("DROP TEMPORARY TABLE IF EXISTS `tmp10`;CREATE TEMPORARY TABLE `tmp10` (`code` VARCHAR(50) NULL DEFAULT NULL,`perigrafi` VARCHAR(50) NULL DEFAULT NULL,`perigrafimm` VARCHAR(50) NULL DEFAULT NULL,`aitiologisi` VARCHAR(50) NULL DEFAULT NULL,`axiologisi` VARCHAR(50) NULL DEFAULT NULL,`egkrisi` TINYTEXT NULL DEFAULT NULL,`posotita` BIGINT(20) NULL DEFAULT NULL,`paratiriseis` VARCHAR(50) NULL DEFAULT NULL,`eponumia` VARCHAR(50) NULL DEFAULT NULL,`offeredprice` DOUBLE NULL DEFAULT NULL,`paraggelia` DOUBLE NULL DEFAULT NULL,`date` DATE NULL DEFAULT NULL,`imerominia_egkrisis` VARBINARY(50) NULL DEFAULT NULL,`egkrisi2` TINYTEXT NULL);INSERT INTO tmp10 (SELECT eidi.code,eidi.perigrafi as per1,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2 FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi as per1,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2 FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi as per1,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2 FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0) UNION (SELECT eidi.code,eidi.perigrafi as per1,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2 FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0);UPDATE tmp10 SET egkrisi='NAI' WHERE egkrisi=1;UPDATE tmp10 SET egkrisi='OXI' WHERE egkrisi=0;UPDATE tmp10 SET egkrisi2='NAI' WHERE egkrisi2=1;UPDATE tmp10 SET egkrisi2='OXI' WHERE egkrisi2=0")
        'runquery("INSERT INTO tmp10 (SELECT eidi.code,eidi.perigrafi as per1,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2 FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi as per1,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2 FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=0 AND egkrisi=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi as per1,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2 FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0) UNION (SELECT eidi.code,eidi.perigrafi as per1,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2 FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & lst(counter) & "' AND supplier_ektos=1 AND egkrisi=0);")
        'runquery("UPDATE tmp10 SET egkrisi='NAI' WHERE egkrisi=1;UPDATE tmp10 SET egkrisi='OXI' WHERE egkrisi=0;UPDATE tmp10 SET egkrisi2='NAI' WHERE egkrisi2=1;UPDATE tmp10 SET egkrisi2='OXI' WHERE egkrisi2=0")
        Dim cmd As MySqlCommand
        'runquery("drop temporary table tmp10")
        Dim egkrisi As Integer
        cmdtext = ""
        'For i = 0 To plithosgrammwn(datagridview2)
        '    cmdtext = "INSERT INTO rpt1(code,perigrafi,lastdate,lastposot,lasttimi,lastprom,egkrisi,posotita,eponumia,offeredprice,paraggelia,date) VALUES('" & datagridview2.Rows(i).Cells(0).Value.ToString & "','" & datagridview2.Rows(i).Cells(1).Value.ToString & "','" & datagridview2.Rows(i).Cells(5).Value & "','" & datagridview2.Rows(i).Cells(6).Value.ToString & "','" & datagridview2.Rows(i).Cells(7).Value.ToString & "','" & datagridview2.Rows(i).Cells(8).Value.ToString & "','" & datagridview2.Rows(i).Cells(9).Value.ToString & "','" & datagridview2.Rows(i).Cells(13).Value.ToString & "','" & datagridview2.Rows(i).Cells(15).Value.ToString & "','" & datagridview2.Rows(i).Cells(16).Value.ToString & "','" & datagridview2.Rows(i).Cells(19).Value.ToString & "','" & datagridview2.Rows(i).Cells(20).Value & "');" & cmdtext
        'Next
        'runquery(cmdtext)
        cmdtext = "INSERT INTO rpt1(code,perigrafi,lastdate,lastposot,lasttimi,lastprom,egkrisi,posotita,eponumia,offeredprice,paraggelia,date,imerominia_egkrisis) VALUES(@code,@perigrafi,@lastdate,@lastposot,@lasttimi,@lastprom,@egkrisi,@posotita,@eponumia,@offeredprice,@paraggelia,@date,@imerominia_egkrisis)"

        mysqlcon.Open()
        Try
            trans = mysqlcon.BeginTransaction

            For i = 0 To plithosgrammwn(datagridview2)

                'If i > 0 Then
                '    cmdtext = "INSERT INTO grammesprom(promitheia,eidos,ektos,aitiologisi,axiologisi,posotita,paratiriseis,egkrisi,supplier,supplier_ektos,offered_price,paraggelia,date,imerominia_egkrisis,egkrisi2) VALUES('" & lst(counter) & "',@eidos,@ektos,@aitiologisi,@axiologisi,@posotita,@paratiriseis,@egkrisi,@supplier,@supplier_ektos,@price,@paraggelia,@imerominia_paraggelias,@imerominia_egkrisis,@egkrisi2);"

                'End If

                cmd = New MySqlCommand(cmdtext, mysqlcon)
                cmd.Transaction = trans
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(0).Value.ToString) Then
                    cmd.Parameters.Add("@code", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@code", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(0).Value.ToString
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(1).Value.ToString) Then
                    cmd.Parameters.Add("@perigrafi", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@perigrafi", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(1).Value.ToString
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(5).Value.ToString) Then
                    cmd.Parameters.Add("@lastdate", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@lastdate", MySqlDbType.Date).Value = datagridview2.Rows(i).Cells(5).Value
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(6).Value.ToString) Then
                    cmd.Parameters.Add("@lastposot", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@lastposot", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(6).Value.ToString
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(7).Value.ToString) Then
                    cmd.Parameters.Add("@lasttimi", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@lasttimi", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(7).Value.ToString
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(8).Value.ToString) Then
                    cmd.Parameters.Add("@lastprom", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@lastprom", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(8).Value.ToString
                End If
                If datagridview2.Rows(i).Cells(9).Value = 0 Then
                    cmd.Parameters.Add("@egkrisi", MySqlDbType.VarChar).Value = "OXI"
                Else
                    cmd.Parameters.Add("@egkrisi", MySqlDbType.VarChar).Value = "NAI"
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(13).Value.ToString) Then
                    cmd.Parameters.Add("@posotita", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@posotita", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(13).Value.ToString
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(15).Value.ToString) Then
                    cmd.Parameters.Add("@eponumia", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@eponumia", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(15).Value.ToString
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(16).Value.ToString) Then
                    cmd.Parameters.Add("@offeredprice", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@offeredprice", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(16).Value.ToString
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(19).Value.ToString) Then
                    cmd.Parameters.Add("@paraggelia", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@paraggelia", MySqlDbType.VarChar).Value = datagridview2.Rows(i).Cells(19).Value.ToString
                End If
                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(20).Value.ToString) Then
                    cmd.Parameters.Add("@date", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@date", MySqlDbType.Date).Value = datagridview2.Rows(i).Cells(20).Value
                End If

                If String.IsNullOrWhiteSpace(datagridview2.Rows(i).Cells(22).Value.ToString) Then
                    cmd.Parameters.Add("@imerominia_egkrisis", MySqlDbType.VarChar).Value = DBNull.Value
                Else
                    cmd.Parameters.Add("@imerominia_egkrisis", MySqlDbType.Date).Value = datagridview2.Rows(i).Cells(22).Value
                End If
                cmd.ExecuteNonQuery()

            Next
            trans.Commit()
            mysqlcon.Close()

        Catch ex As Exception
            queryerror = True
            trans.Rollback()
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    'Private Sub TextBox_keyPress2(ByVal sender As Object, ByVal e As KeyPressEventArgs)

    '    If Char.IsDigit(CChar(CStr(e.KeyChar))) = False And e.KeyChar <> ControlChars.Back And e.KeyChar <> ChrW(47) Then e.Handled = True

    'End Sub
    Private Sub TextBox_keyPress3(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        If Char.IsDigit(CChar(CStr(e.KeyChar))) = False And e.KeyChar <> ControlChars.Back And e.KeyChar <> ChrW(44) Then e.Handled = True

    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles datagridview2.CellClick
        datagridview2.BeginEdit(True)
        If e.ColumnIndex = 18 Or e.ColumnIndex = 17 Then
            If Button2.BackColor = Color.CornflowerBlue And (datagridview2.CurrentRow.Cells(15).Value.ToString = "" Or datagridview2.CurrentRow.Cells(16).Value.ToString = "") And datagridview2.CurrentCell.Value = False Then
                datagridview2.CurrentCell.Value = False
                MessageBox.Show("Για να προχωρήσετε στην έγκριση πρέπει να έχει συμπληρωθεί προτεινόμενη τιμή και προμηθευτής")
                answer = False
                Exit Sub
            End If
        End If
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 18 Then

            If datagridview2.CurrentCell.Value = False And Button2.BackColor = Color.CornflowerBlue And userlevel = 10 Then
                Dim result As DialogResult = MessageBox.Show("Θέλετε οπωσδήποτε να προχωρήσετε στην έγκριση;", "Μήνυμα!", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    datagridview2.CurrentCell.Value = True
                    Dim d3 As Date = Today
                    datagridview2.Rows(e.RowIndex).Cells(22).Value = d3.ToString("dd-MM-yyyy")
                ElseIf result = DialogResult.No Then
                    'datagridview2.CurrentCell.Value = False
                    answer = False
                End If

            End If
        End If
        'If e.ColumnIndex = 20 And Button2.BackColor = Color.CornflowerBlue And datagridview2.CurrentRow.Cells(18).Value = True Then
        '    Dim d2 As Date = Today
        '    datagridview2.Rows(e.RowIndex).Cells(20).Value = d2.ToString("dd-MM-yyyy")
        '    datagridview2.RefreshEdit()

        'End If
        If e.ColumnIndex = 20 Or e.ColumnIndex = 19 Then
            MessageBox.Show("Ο χρήστης δεν έχει δικαιώματα")
        End If
        If datagridview2.CurrentCell.ReadOnly = True And e.ColumnIndex > 8 And e.ColumnIndex < 19 And Button2.BackColor = Color.CornflowerBlue And datagridview2.CurrentRow.Cells(18).Value = True AndAlso datagridview2.CurrentRow.Cells(9).Value = 1 And datagridview2.CurrentCell.Style.BackColor <> Color.LightGray And e.RowIndex <= datagridview2.Rows.Count Then
            MessageBox.Show("Δεν επιτρέπεται η μεταβολή της γραμμής γιατί η προμήθεια έχει εγκριθεί")
        End If
        If e.ColumnIndex = 6 Then
            If datagridview2.CurrentRow.Cells(15).Value.ToString <> "" Or datagridview2.CurrentRow.Cells(16).Value.ToString <> "" Then
                MessageBox.Show("Δεν επιτρέπεται η μεταβολή της ποσότητας γιατί έχει επιλεγεί προμηθευτής ή τιμή")
            End If
        End If
        If e.ColumnIndex = 15 And multipleselected = True Then
            lst1.Clear()
            multipleselected = False
            lst1.Add(datagridview2.Rows(e.RowIndex).Cells(21).Value)
        End If

    End Sub
    Protected Overrides Function ScrollToControl(activeControl As Control) As Point

        Return Me.AutoScrollPosition
    End Function

    Private Sub datagridview2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles datagridview2.CellValueChanged
        If e.RowIndex >= 0 AndAlso (e.ColumnIndex = 18 Or e.ColumnIndex = 17) Then
            If answer = False Then
                datagridview2.CurrentCell.Value = False
                answer = True
                datagridview2.RefreshEdit()
            End If
        End If
        If e.ColumnIndex = 11 Then
            datagridview2.Rows(e.RowIndex).ReadOnly = False
        End If
    End Sub

    Private Sub datagridview2_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles datagridview2.CurrentCellDirtyStateChanged
        If datagridview2.CurrentCell.ColumnIndex = 18 Or datagridview2.CurrentCell.ColumnIndex = 17 Then

            If datagridview2.IsCurrentCellDirty Then
                datagridview2.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        End If
    End Sub

    Private Sub datagridview2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles datagridview2.CellDoubleClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then
            Form5.TextBox3.Text = datagridview2.CurrentCell.Value
            Form5.eidos = datagridview2.CurrentRow.Cells(11).Value
            Form5.eidosektos = datagridview2.CurrentRow.Cells(10).Value
            Form5.Show()
        End If
    End Sub

    Private Sub datagridview2_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles datagridview2.DataError

        'If e.ColumnIndex = 20 Then
        '    MessageBox.Show("Λάθος μορφή ημερομηνίας στη γραμμή " & e.RowIndex + 1)
        '    datagridview2.Rows(e.RowIndex).Cells(20).Value = ""
        '    datagridview2.RefreshEdit()
        'End If
    End Sub

    Private Sub datagridview2_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles datagridview2.RowsAdded
        If datagridview2.Rows(e.RowIndex).IsNewRow Then
            datagridview2.Rows(e.RowIndex).ReadOnly = True
            datagridview2.Rows(e.RowIndex).Cells(0).ReadOnly = False

        End If
    End Sub

    Private Sub datagridview2_CellStateChanged(sender As Object, e As DataGridViewCellStateChangedEventArgs) Handles datagridview2.CellStateChanged
        If e.Cell.ColumnIndex = 15 And Button2.BackColor <> Color.CornflowerBlue Then

            If e.StateChanged <> DataGridViewElementStates.Selected Then

                Return
            Else
                If datagridview2.Rows(e.Cell.RowIndex).Cells(9).Value = 1 Then
                    If lst1.Count = 0 Then
                        lst1.Add(datagridview2.Rows(e.Cell.RowIndex).Cells(21).Value)
                    End If

                    If datagridview2.GetCellCount(DataGridViewElementStates.Selected) > 1 And doubleselected = False Then
                        'stack1.Push(datagridview2.Rows(e.Cell.RowIndex).Cells(21).Value)
                        lst1.Add(datagridview2.Rows(e.Cell.RowIndex).Cells(21).Value)
                        Dim listlength As Integer = lst1.Count 'lst1.Count

                        If listlength > 1 Then

                            If lst1.Item(listlength - 1) <> lst1.Item(listlength - 2) Then
                                'If stack1.ElementAt(listlength - 1) <> stack1.ElementAt(listlength - 2) Then
                                MessageBox.Show("Δεν επιτρέπεται επιλογή πέραν του ενός προμηθευτή")

                                lst1.RemoveAt(listlength - 1)

                                doubleselected = True
                                datagridview2.Rows(e.Cell.RowIndex).Cells(15).Selected = False

                            End If
                        End If
                    End If
                    If doubleselected = True Then
                        doubleselected = False
                    End If
                ElseIf doubleselected = False And datagridview2.CurrentRow.Cells(21).Value <> 0 And caseselection <> "form1-prom" Then
                    MessageBox.Show("Για να προχωρήσετε σε επιλογή προμηθευτή προς αποστολή της παραγγελίας για το συγκεκριμένο είδος πρέπει να υπάρχει έγκριση")
                    doubleselected = True
                    datagridview2.Rows(e.Cell.RowIndex).Cells(15).Selected = False

                End If
                If doubleselected = True Then
                    doubleselected = False
                End If
                caseselection = ""
            End If
        End If

    End Sub

    Private Sub datagridview2_KeyUp(sender As Object, e As KeyEventArgs) Handles datagridview2.KeyUp

        If e.KeyCode = Keys.ControlKey Then
            If datagridview2.SelectedCells.Count = 1 Then
                datagridview2.ClearSelection()
                'multipleselected = False
                lst1.Clear()
            ElseIf datagridview2.SelectedCells.Count > 1 Then
                multipleselected = True
            End If

        End If
    End Sub

    Private Sub datagridview2_KeyDown(sender As Object, e As KeyEventArgs) Handles datagridview2.KeyDown
        If e.KeyCode = Keys.ControlKey And multipleselected = True Then
            If datagridview2.SelectedCells.Count > 1 Then
                datagridview2.ClearSelection()
                lst1.Clear()
            End If

        End If
        multipleselected = False
    End Sub
    Private Sub frmCustomerDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        ektos = False
        If TextBox4.Focused Then
            If e.KeyCode = Keys.Enter Then
                loadgrid("SELECT DISTINCT eidi.eidos,code,perigrafi FROM eidi JOIN grammesprom ON eidi.eidos=grammesprom.eidos WHERE code LIKE '" & TextBox4.Text & "%'", Form2.DataGridView2)
        If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                    loadgrid("SELECT DISTINCT eidiektos.id,kwdikos,perigrafi FROM eidiektos JOIN grammesprom ON eidiektos.id=grammesprom.eidos WHERE kwdikos LIKE '" & TextBox4.Text & "%' AND ektos=1", Form2.DataGridView2)
                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                        MessageBox.Show("Δε βρέθηκαν είδη στο σύστημα")
                    Else
                        ektos = True

                        Form2.caseselection = "form1-textbox4"
                        Form2.Show()
                    End If
                Else

                    Form2.caseselection = "form1-textbox4"
                    Form2.Show()
                End If
            End If
        End If
        If TextBox5.Focused Then
            If e.KeyCode = Keys.Enter Then
                loadgrid("SELECT DISTINCT pelprom,code,eponymia FROM pelproms JOIN grammesprom ON pelprom=supplier WHERE eponymia LIKE '" & TextBox5.Text & "%' AND supplier_ektos=0", Form2.DataGridView2)
                If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                    loadgrid("SELECT DISTINCT pelproms_ektos.id,epon,afm FROM pelproms_ektos JOIN grammesprom ON pelproms_ektos.id=supplier WHERE epon LIKE '" & TextBox5.Text & "%' AND supplier_ektos=1", Form2.DataGridView2)
                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                        MessageBox.Show("Δε βρέθηκαν προμηθευτές στο σύστημα")
                    Else
                        ektos = True

                        Form2.caseselection = "form1-textbox5"
                        Form2.Show()
                    End If
                Else

                    Form2.caseselection = "form1-textbox5"
                    Form2.Show()
                End If
            End If
        End If
        If TextBox6.Focused Then
            If e.KeyCode = Keys.Enter Then
                loadgrid("SELECT DISTINCT eidi.eidos,code,eidi.perigrafi FROM eidi JOIN grammesprom ON eidi.eidos=grammesprom.eidos WHERE eidi.perigrafi LIKE '" & TextBox6.Text & "%'", Form2.DataGridView2)
                If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                    loadgrid("SELECT DISTINCT eidiektos.id,kwdikos,eidiektos.perigrafi FROM eidiektos JOIN grammesprom ON eidiektos.id=grammesprom.eidos WHERE eidiektos.perigrafi LIKE '" & TextBox6.Text & "%'", Form2.DataGridView2)
                    If norecordsfound = True Then 'Form2.DataGridView2.Rows(0).Cells(0).Value = Nothing Then
                        MessageBox.Show("Δε βρέθηκαν είδη στο σύστημα")
                    Else
                        ektos = True

                        Form2.caseselection = "form1-textbox6"
                        Form2.Show()
                    End If
                Else

                    Form2.caseselection = "form1-textbox6"
                    Form2.Show()
                End If
            End If
        End If
    End Sub

End Class
