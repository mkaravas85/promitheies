Imports System.ComponentModel
Imports MySql.Data.MySqlClient

Public Class Form3
    Dim monada As Integer
    Dim tim_eng As Integer = 0
    Public caseselection1 As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If caseselection1 <> "form1-prom" And caseselection1 <> "form1-prom-dgv1" Then

            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.SelectedItem = Nothing Then
                MessageBox.Show("Δεν έχετε συμπληρώσει όλα τα πεδία")
                Exit Sub
            End If
            'dtvaluetoset("SELECT monada FROM monades_metr WHERE perigrafi='" & ComboBox1.SelectedItem.ToString & "'")
            monada = returnsinglevaluequery("SELECT monada FROM monades_metr WHERE perigrafi='" & ComboBox1.SelectedItem.ToString & "'")
            If returnsinglevaluequery("SELECT id FROM eidiektos WHERE kwdikos='" & TextBox1.Text & "'") <> 0 Then
                MessageBox.Show("Ο κωδικός είδους που προσπαθείτε να καταχωρήσετε υπάρχει ήδη!")
                Exit Sub
            End If

            runquery("INSERT INTO eidiektos(kwdikos,perigrafi,perig_en,monada) VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & monada & "')")

            If Form1.datagridview1 IsNot Nothing Then
                Form1.datagridview1.Rows(grammi).Cells(12).Value = returnsinglevaluequery("SELECT id FROM eidiektos WHERE kwdikos='" & TextBox1.Text & "'")
                Form1.datagridview1.Rows(grammi).Cells(0).Value = TextBox1.Text
                Form1.datagridview1.Rows(grammi).Cells(1).Value = TextBox2.Text
                Form1.datagridview1.Rows(grammi).Cells(11).Value = 1
                'Form1.datagridview1.Rows(grammi).Cells(12).Value = monada
                Form1.datagridview1.Rows(grammi).Cells(2).Value = ComboBox1.SelectedItem.ToString
                Form1.datagridview1.CurrentCell = Form1.datagridview1.Rows(grammi).Cells(3)
                Form1.datagridview1.BeginEdit(True)
            End If
            If Form1.datagridview2 IsNot Nothing Then
                Form1.datagridview2.Rows(grammi).Cells(11).Value = returnsinglevaluequery("SELECT id FROM eidiektos WHERE kwdikos='" & TextBox1.Text & "'")
                Form1.datagridview2.Rows(grammi).Cells(0).Value = TextBox1.Text
                Form1.datagridview2.Rows(grammi).Cells(1).Value = TextBox2.Text
                'Form1.datagridview2.Rows(grammi).Cells(12).Value = 0
                Form1.datagridview2.Rows(grammi).Cells(10).Value = 1
                Form1.datagridview2.Rows(grammi).Cells(2).Value = ComboBox1.SelectedItem.ToString
                Form1.datagridview2.CurrentCell = Form1.datagridview2.Rows(grammi).Cells(3)
                Form1.datagridview2.BeginEdit(True)
            End If
        Else
            If returnsinglevaluequery("SELECT epon from pelproms_ektos where epon='" & TextBox1.Text & "'") <> "" Then
                MessageBox.Show("O προμηθευτής με τη συγκεκριμένη επωνυμία υπάρχει ήδη καταχωρημένος")
                Exit Sub
            End If
            If CheckBox1.Checked = True Then
                tim_eng = 1
            End If
            If TextBox1.Text <> "" Then
                runquery("INSERT INTO pelproms_ektos(epon,email,afm,tim_eng) VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & tim_eng & "')")
            Else
                MessageBox.Show("Δεν έχετε συμπληρώσει το υποχρεωτικό πεδίο επωνυμίας προμηθευτή")
                Exit Sub
            End If
            If queryerror = True Then
                queryerror = False
                Exit Sub
            End If
            If caseselection1 = "form1-prom" Then

                Form1.datagridview2.Rows(grammi).Cells(21).Value = returnsinglevaluequery("SELECT id FROM pelproms_ektos WHERE epon='" & TextBox1.Text & "'")
                    Form1.datagridview2.Rows(grammi).Cells(15).Value = TextBox1.Text

                    Form1.datagridview2.Rows(grammi).Cells(12).Value = 1
                    Form1.datagridview2.CurrentCell = Form1.datagridview2.Rows(grammi).Cells(16)

                End If
            If caseselection1 = "form1-prom-dgv1" Then

                If TextBox1.Text <> "" Then
                    'runquery("INSERT INTO pelproms_ektos(epon,email,afm) VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')")
                    Form1.datagridview1.Rows(grammi).Cells(16).Value = returnsinglevaluequery("SELECT id FROM pelproms_ektos WHERE epon='" & TextBox1.Text & "'")
                    Form1.datagridview1.Rows(grammi).Cells(14).Value = TextBox1.Text

                    Form1.datagridview1.Rows(grammi).Cells(15).Value = 1
                    Form1.datagridview1.CurrentCell = Form1.datagridview1.Rows(grammi).Cells(14)
                End If
            End If
        End If
        Me.Close()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Focus()
        ShowIcon = False

        runquerycombo("SELECT perigrafi FROM monades_metr", ComboBox1, "perigrafi")
        Me.Text = "SigNet S.A                                                 Καταχώρηση Νέου Είδους"
        If caseselection1 = "form1-prom" Or caseselection1 = "form1-prom-dgv1" Then
            Me.Text = "SigNet                                                 Καταχώρηση Νέου Προμηθευτή"
            CheckBox1.Visible = True
            'Label1.Visible = False
            Label3.Visible = False
            ' TextBox3.Visible = True
            ComboBox1.Visible = False
            Label1.Text = "Επωνυμία:"
            Label2.Text = "Email:"
            Label4.Text = "ΑΦΜ:"
        Else
            TextBox1.Focus()

        End If
    End Sub

End Class