Imports System.ComponentModel

Public Class Form2
    Public caseselection As String
    Dim imerominia, posotita, timi, promitheutis As String

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowIcon = False
        Me.Text = "SigNet"
        If caseselection = "form1" Or caseselection = "form5" Or caseselection = "form5-textbox3" Or caseselection = "form1-textbox4" Or caseselection = "form1-textbox6" Then

            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(1).HeaderText = "Kωδικός Είδους"
            DataGridView2.Columns(2).HeaderText = "Περιγραφή Είδους"
            DataGridView2.Columns(2).Width = 360

        End If
        If caseselection = "form1-checkbox" Then
            DataGridView2.Columns(0).HeaderText = "Α/Α"
            DataGridView2.Columns(1).HeaderText = "Αρχείο PDF"
            DataGridView2.Columns(2).HeaderText = "Ημερομηνία Καταχώρησης"
            DataGridView2.Columns(1).Width = 256
        End If
        If (caseselection = "form1-prom" Or caseselection = "form1-textbox5" Or caseselection = "form1-prom-dgv1") And Form1.ektos = False Then
            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(1).HeaderText = "Κωδικός"
            DataGridView2.Columns(2).HeaderText = "Επωνυμία"
            DataGridView2.Columns(2).Width = 286
        End If
        If (caseselection = "form1-prom" Or caseselection = "form1-textbox5" Or caseselection = "form1-prom-dgv1") And Form1.ektos = True Then
            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(2).HeaderText = "ΑΦΜ"
            DataGridView2.Columns(1).HeaderText = "Επωνυμία"
            DataGridView2.Columns(1).Width = 286
            DataGridView2.Columns(2).Width = 165

        End If
        If DataGridView2.RowCount = 2 Then
            DataGridView2.CurrentCell = DataGridView2.Rows(0).Cells(1)
            Button1.PerformClick()
        End If
    End Sub

    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        Button1.PerformClick()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If caseselection = "form1" Then
            If Form1.ektos = False Then
                dtvaluetoset("SELECT kiniseis.imerominia,SUM(posotita),timimon,pelproms.eponymia FROM grammeskin JOIN kiniseis ON kiniseis.kinisi=grammeskin.kinisi JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom JOIN parastatika ON parastatika.parastatiko=kiniseis.parastatiko WHERE grammeskin.eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES' GROUP by eidos,imerominia ORDER BY imerominia DESC LIMIT 1")

                Form1.monadametr = returnsinglevaluequery("SELECT perigrafi FROM monades_metr WHERE monada IN (SELECT mm1 FROM eidi WHERE eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "')")

            Else
                dtvaluetoset("(select imerominia_egkrisis,posotita,offered_price,pelproms.eponymia from grammesprom join pelproms on pelprom=supplier where eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' and ektos=1 and supplier_ektos=0 and offered_price is not null and imerominia_egkrisis is not null) union (select imerominia_egkrisis,posotita,offered_price,pelproms_ektos.epon from grammesprom join pelproms_ektos on pelproms_ektos.id=supplier where eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' and ektos=1 and supplier_ektos=1 and offered_price is not null and imerominia_egkrisis is not null) order by imerominia_egkrisis desc LIMIT 1")
                Form1.monadametr = returnsinglevaluequery("SELECT perigrafi FROM monades_metr WHERE monada IN (SELECT monada FROM eidiektos WHERE id='" & DataGridView2.CurrentRow.Cells(0).Value & "')")

                'Form1.datagridview1.Rows(grammi).Cells(11).Value = 1
                'Form1.ektos = False
            End If
            If dt.Rows.Count > 0 Then
                imerominia = dt.Rows(0).Item(0)
                posotita = dt.Rows(0).Item(1)
                timi = dt.Rows(0).Item(2)
                promitheutis = dt.Rows(0).Item(3)
            End If
            If Form1.datagridview1 IsNot Nothing Then
                If Form1.ektos = False Then
                    '    dtvaluetoset("SELECT kiniseis.imerominia,posotita,timimon,pelproms.eponymia FROM grammeskin JOIN kiniseis ON kiniseis.kinisi=grammeskin.kinisi JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom JOIN parastatika ON parastatika.parastatiko=kiniseis.parastatiko WHERE grammeskin.eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES' ORDER BY imerominia DESC LIMIT 1")
                    Form1.datagridview1.Rows(grammi).Cells(11).Value = 0
                    '    Form1.monadametr = returnsinglevaluequery("SELECT perigrafi FROM monades_metr WHERE monada IN (SELECT mm1 FROM eidi WHERE eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "')")

                Else
                    '    dtvaluetoset("(select imerominia_egkrisis,posotita,offered_price,pelproms.eponymia from grammesprom join pelproms on pelprom=supplier where eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' and ektos=1 and supplier_ektos=0 and offered_price is not null) union (select imerominia_egkrisis,posotita,offered_price,pelproms_ektos.epon from grammesprom join pelproms_ektos on pelproms_ektos.id=supplier where eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' and ektos=1 and supplier_ektos=1 and offered_price is not null) order by imerominia_egkrisis desc LIMIT 1")
                    '    Form1.monadametr = returnsinglevaluequery("SELECT perigrafi FROM monades_metr WHERE monada IN (SELECT monada FROM eidiektos WHERE id='" & DataGridView2.CurrentRow.Cells(0).Value & "')")

                    Form1.datagridview1.Rows(grammi).Cells(11).Value = 1
                    '    Form1.ektos = False
                    'End If
                    'If dt.Rows.Count > 0 Then
                    '    imerominia = dt.Rows(0).Item(0)
                    '    posotita = dt.Rows(0).Item(1)
                    '    timi = dt.Rows(0).Item(2)
                    '    promitheutis = dt.Rows(0).Item(3)
                End If

                Form1.datagridview1.Rows(grammi).Cells(12).Value = DataGridView2.CurrentRow.Cells(0).Value

                    Form1.datagridview1.Rows(grammi).Cells(0).Value = DataGridView2.CurrentRow.Cells(1).Value
                    Form1.datagridview1.Rows(grammi).Cells(1).Value = DataGridView2.CurrentRow.Cells(2).Value
                    Form1.datagridview1.Rows(grammi).Cells(2).Value = Form1.monadametr
                    Form1.datagridview1.Rows(grammi).Cells(5).Value = imerominia
                    Form1.datagridview1.Rows(grammi).Cells(6).Value = posotita
                    Form1.datagridview1.Rows(grammi).Cells(7).Value = timi
                    Form1.datagridview1.Rows(grammi).Cells(8).Value = promitheutis
                    Form1.datagridview1.CurrentCell = Form1.datagridview1.Rows(grammi).Cells(3)
                    Form1.datagridview1.BeginEdit(True)
                End If

            If Form1.datagridview2 IsNot Nothing Then

                If Form1.ektos = False Then
                    '    dtvaluetoset("SELECT kiniseis.imerominia,posotita,timimon,pelproms.eponymia FROM grammeskin JOIN kiniseis ON kiniseis.kinisi=grammeskin.kinisi JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom JOIN parastatika ON parastatika.parastatiko=kiniseis.parastatiko WHERE grammeskin.eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES' ORDER BY imerominia DESC LIMIT 1")

                    '    Form1.monadametr = returnsinglevaluequery("SELECT perigrafi FROM monades_metr WHERE monada IN (SELECT mm1 FROM eidi WHERE eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "')")
                    Form1.datagridview2.Rows(grammi).Cells(10).Value = 0
                Else
                    '    dtvaluetoset("(select imerominia_egkrisis,posotita,offered_price,pelproms.eponymia from grammesprom join pelproms on pelprom=supplier where eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' and ektos=1 and supplier_ektos=0 and offered_price is not null) union (select imerominia_egkrisis,posotita,offered_price,pelproms_ektos.epon from grammesprom join pelproms_ektos on pelproms_ektos.id=supplier where eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' and ektos=1 and supplier_ektos=1 and offered_price is not null) order by imerominia_egkrisis desc LIMIT 1")
                    '    Form1.monadametr = returnsinglevaluequery("SELECT perigrafi FROM monades_metr WHERE monada IN (SELECT monada FROM eidiektos WHERE id='" & DataGridView2.CurrentRow.Cells(0).Value & "')")

                    '    'dtvaluetoset("SELECT perigrafi FROM monades_metr WHERE monada IN (SELECT monada FROM eidiektos WHERE id='" & DataGridView2.CurrentRow.Cells(0).Value & "')")
                    Form1.datagridview2.Rows(grammi).Cells(10).Value = 1
                    '    Form1.ektos = False
                End If
                'If dt.Rows.Count > 0 Then
                '    'dtvaluetoset("SELECT MAX(kiniseis.imerominia),posotita,timimon,pelproms.eponymia FROM grammeskin JOIN kiniseis ON kiniseis.kinisi=grammeskin.kinisi JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom JOIN parastatika ON parastatika.parastatiko=kiniseis.parastatiko WHERE grammeskin.eidos='" & DataGridView2.CurrentRow.Cells(0).Value & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES'")
                '    imerominia = dt.Rows(0).Item(0)
                '    posotita = dt.Rows(0).Item(1)
                '    timi = dt.Rows(0).Item(2)
                '    promitheutis = dt.Rows(0).Item(3)
                'End If
                'Form1.monadametr = dt.Rows(0).Item(0)

                Form1.datagridview2.Rows(grammi).Cells(11).Value = DataGridView2.CurrentRow.Cells(0).Value

                Form1.datagridview2.Rows(grammi).Cells(0).Value = DataGridView2.CurrentRow.Cells(1).Value

                Form1.datagridview2.Rows(grammi).Cells(1).Value = DataGridView2.CurrentRow.Cells(2).Value

                Form1.datagridview2.Rows(grammi).Cells(2).Value = Form1.monadametr
                Form1.datagridview2.Rows(grammi).Cells(5).Value = imerominia
                Form1.datagridview2.Rows(grammi).Cells(6).Value = posotita
                Form1.datagridview2.Rows(grammi).Cells(7).Value = timi
                Form1.datagridview2.Rows(grammi).Cells(8).Value = promitheutis
                Form1.datagridview2.CurrentCell = Form1.datagridview2.Rows(grammi).Cells(3)
                'Form1.datagridview2.Rows(grammi).Cells(12).Value = 0
                Form1.datagridview2.BeginEdit(True)

            End If
            Form1.ektos = False
        End If
        If caseselection = "form5" Then
            If Form5.ektos = False Then
                Form5.eidos = DataGridView2.CurrentRow.Cells(0).Value
                Form5.TextBox1.Text = DataGridView2.CurrentRow.Cells(1).Value
                Form5.eidosektos = 0
            End If
            If Form5.ektos = True Then
                Form5.eidos = DataGridView2.CurrentRow.Cells(0).Value
                Form5.TextBox1.Text = DataGridView2.CurrentRow.Cells(1).Value
                Form5.eidosektos = 1
                Form5.ektos = False
            End If
        End If
        If caseselection = "form5-textbox3" Then
            If Form5.ektos = False Then
                Form5.eidos = DataGridView2.CurrentRow.Cells(0).Value
                Form5.TextBox3.Text = DataGridView2.CurrentRow.Cells(1).Value
                Form5.eidosektos = 0
            End If
            If Form5.ektos = True Then
                Form5.eidos = DataGridView2.CurrentRow.Cells(0).Value
                Form5.TextBox3.Text = DataGridView2.CurrentRow.Cells(1).Value
                Form5.eidosektos = 1
                Form5.ektos = False
            End If
        End If
        If caseselection = "form1-prom" Then
            If Form1.ektos = True Then
                Form1.datagridview2.Rows(grammi).Cells(12).Value = 1
                Form1.datagridview2.Rows(grammi).Cells(15).Value = DataGridView2.CurrentRow.Cells(1).Value
                Form1.ektos = False
            Else
                Form1.datagridview2.Rows(grammi).Cells(12).Value = 0
                Form1.datagridview2.Rows(grammi).Cells(15).Value = DataGridView2.CurrentRow.Cells(2).Value
            End If
            'Form1.caseselection = "form1-prom"
            Form1.datagridview2.Rows(grammi).Cells(21).Value = DataGridView2.CurrentRow.Cells(0).Value
            Form1.datagridview2.CurrentCell = Form1.datagridview2.Rows(grammi).Cells(16)

        End If
        If caseselection = "form1-textbox4" Or caseselection = "form1-textbox6" Then
            Form1.TextBox4.Text = DataGridView2.CurrentRow.Cells(1).Value.ToString.ToUpper
            Form1.TextBox6.Text = DataGridView2.CurrentRow.Cells(2).Value.ToString.ToUpper
            Form1.eidos1 = DataGridView2.CurrentRow.Cells(0).Value

            Form1.CheckBox1.Checked = True

        End If
        If caseselection = "form1-textbox5" Then

            Form1.promitheutis = DataGridView2.CurrentRow.Cells(0).Value
            If Form1.ektos = True Then
                Form1.TextBox5.Text = DataGridView2.CurrentRow.Cells(1).Value.ToString.ToUpper

            Else
                Form1.TextBox5.Text = DataGridView2.CurrentRow.Cells(2).Value.ToString.ToUpper
            End If
            Form1.CheckBox1.Checked = True
        End If
        If caseselection = "form1-prom-dgv1" Then
            If Form1.ektos = True Then
                Form1.datagridview1.Rows(grammi).Cells(15).Value = 1
                Form1.datagridview1.Rows(grammi).Cells(14).Value = DataGridView2.CurrentRow.Cells(1).Value
                Form1.ektos = False
            Else
                Form1.datagridview1.Rows(grammi).Cells(15).Value = 0
                Form1.datagridview1.Rows(grammi).Cells(14).Value = DataGridView2.CurrentRow.Cells(2).Value
            End If

            Form1.datagridview1.Rows(grammi).Cells(16).Value = DataGridView2.CurrentRow.Cells(0).Value
            Form1.datagridview1.CurrentCell = Form1.datagridview1.Rows(grammi).Cells(13)

        End If
        Me.Close()
    End Sub

End Class