Imports System.ComponentModel
Imports System.Net.Mail
Imports System.Net

Imports System.Net.Security

Imports System.Security.Cryptography.X509Certificates
Public Class Form7
    Public pelprom As Integer = 0
    Public ektos As Integer = 0
    Dim idgrammisprom As Integer = 0
    Public paraggelia, mailbody, date6 As String
    Public idlist As New List(Of Integer)
    Public agglika As Boolean = False

    Private ordertest2 As String = "ΠΕΡΙΓΡΑΦΗ".PadRight(50) & "ΠΟΣΟΤΗΤΑ".PadRight(50) & vbTab & "ΤΙΜΗ" & vbCrLf
    Private ordertest3 As String = "DESCRIPTION".PadRight(50) & "QUANTITY".PadRight(50) & vbTab & "PRICE" & vbCrLf
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ShowIcon = False
        ToolTip1.SetToolTip(TextBox4, "Διαχωρισμός πολλαπλών παραληπτών κοινοποίησης με κόμμα")
        If ektos = 0 Then
            agglika = returnsinglevaluequery("SELECT parastenglish FROM pelproms where pelprom='" & pelprom & "'")

        Else

            agglika = returnsinglevaluequery("SELECT tim_eng FROM pelproms_ektos WHERE id='" & pelprom & "'")

        End If
        If agglika = False Then
            TextBox3.Text = returnsinglevaluequery("SELECT arxikoell FROM prom_mail_texts")
            TextBox2.Text = returnsinglevaluequery("SELECT telikoell FROM prom_mail_texts")
            TextBox1.Text = ordertest2 & vbCrLf & paraggelia

        Else
            TextBox3.Text = returnsinglevaluequery("SELECT arxikoen FROM prom_mail_texts")
            TextBox2.Text = returnsinglevaluequery("SELECT telikoen FROM prom_mail_texts")
            TextBox1.Text = ordertest3 & vbCrLf & paraggelia

        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form8.Case8 = "arxiki"
        Form8.Show()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Form8.Case8 = "teliki"
        Form8.Show()
    End Sub
    Private Shared Function customCertValidation(ByVal sender As Object,
                                                ByVal cert As X509Certificate,
                                                ByVal chain As X509Chain,
                                                ByVal errors As SslPolicyErrors) As Boolean

        Return True

    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim email As New MailMessage
            dtvaluetoset2("SELECT * FROM el_inv_settings")
            Dim mailrecepient As String
            If ektos = 0 Then
                mailrecepient = returnsinglevaluequery("SELECT email FROM pelproms WHERE pelprom='" & pelprom & "'")
            ElseIf ektos = 1 Then
                mailrecepient = returnsinglevaluequery("SELECT email FROM pelproms_ektos WHERE id='" & pelprom & "'")
            End If
            If mailrecepient = "" Then
                MessageBox.Show("Η διεύθυνση του παραλήπτη δεν είναι συμπληρωμένη")
                Exit Sub
            End If
            email.From = New MailAddress(dt2.Rows(0).Item(3).ToString)
            email.To.Add(mailrecepient)
            email.CC.Add(TextBox4.Text)
            If agglika = False Then
                email.Subject = "Παραγγελία"

                email.Body = "<html><head><style>table, td, th {border: 1px solid green;padding: 5px;}</style></head><p>" & TextBox3.Text & "</p><table><th>ΠΕΡΙΓΡΑΦΗ</th><th>ΠΟΣΟΤΗΤΑ</th><th>ΤΙΜΗ</th>" & mailbody & "</table><p>" & TextBox2.Text & "</p></html>" 'TextBox1.Text 'ordertest1 & vbCrLf & ordertest3 & vbCrLf & mailbody
            Else
                email.Subject = "Order"

                email.Body = "<html><head><style>table, td, th {border: 1px solid green;padding: 5px;}</style></head><p>" & TextBox3.Text & "</p><table><th>DESCRIPTION</th><th>QUANTITY</th><th>PRICE</th>" & mailbody & "</table><p>" & TextBox2.Text & "</p></html>" 'TextBox1.Text 'ordertest1 & vbCrLf & ordertest3 & vbCrLf & mailbody

            End If
            email.IsBodyHtml = True
            ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf customCertValidation)
            Dim smtp As New SmtpClient(dt2.Rows(0).Item(1).ToString)
            smtp.Port = dt2.Rows(0).Item(2)
            smtp.EnableSsl = True
            smtp.Credentials = New System.Net.NetworkCredential(dt2.Rows(0).Item(3).ToString, dt2.Rows(0).Item(5).ToString)
            smtp.Send(email)
            MessageBox.Show("To mail εστάλη !")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        TODATETIME(Today)
        Dim cmd As String
        Dim lastinsertedvalue As Integer = returnsinglevaluequery("SELECT IFNULL(MAX(paraggelia),0) FROM grammesprom")
        Dim paraggelia As Integer = lastinsertedvalue + 1
        For i = 0 To idlist.Count - 1
            cmd = cmd & "UPDATE grammesprom SET date='" & date6 & "',paraggelia='" & paraggelia & "' WHERE id='" & idlist(i) & "';"
        Next
        runquery(cmd)
        Form1.initializedgv()
        Form1.AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=0 AND parastenglish=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=1 AND tim_eng=0) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=1 AND tim_eng=0) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=1) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=0 AND pelproms.parastenglish=1) UNION (SELECT eidiektos.kwdikos,eidiektos.perig_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=1 AND tim_eng=1) UNION (SELECT eidi.code,eidi.perigr_en,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=1 AND tim_eng=1)")

        'Form1.AddDatagridView2("(SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=0 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms.eponymia,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms ON pelproms.pelprom=supplier WHERE ektos=1 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=0) UNION (SELECT eidiektos.kwdikos,eidiektos.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidiektos ON eidiektos.id=grammesprom.eidos JOIN monades_metr ON eidiektos.monada=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=1 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=1) UNION (SELECT eidi.code,eidi.perigrafi,monades_metr.perigrafi,aitiologisi,axiologisi,egkrisi,ektos,grammesprom.eidos,supplier_ektos,posotita,paratiriseis,pelproms_ektos.epon,offered_price,paraggelia,date,supplier,imerominia_egkrisis,egkrisi2,grammesprom.id FROM grammesprom JOIN eidi ON eidi.eidos=grammesprom.eidos JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN pelproms_ektos ON pelproms_ektos.id=supplier WHERE ektos=0 AND promitheia='" & Form1.lst(Form1.counter) & "' AND supplier_ektos=1)")
        Form1.gridlayout2()
        runquery("CREATE TEMPORARY TABLE t1 SELECT kiniseis.imerominia,grammeskin.posotita,grammeskin.timimon,pelproms.eponymia,grammesprom.eidos FROM grammesprom LEFT JOIN eidi ON grammesprom.eidos=eidi.eidos LEFT JOIN monades_metr ON eidi.mm1=monades_metr.monada LEFT JOIN grammeskin ON grammesprom.eidos=grammeskin.eidos LEFT JOIN kiniseis ON grammeskin.kinisi=kiniseis.kinisi LEFT JOIN pelproms ON kiniseis.pelprom=pelproms.pelprom LEFT JOIN parastatika ON kiniseis.parastatiko=parastatika.parastatiko  WHERE ektos=0 AND promitheia='" & Form1.lst(Form1.counter) & "' AND parastatika.kinaxies='deb1' AND parastatika.typoskin='ΠΡΟΜΗΘΕΥΤΗΣ' AND parastatika.apoth='YES' ORDER BY imerominia DESC")
        dtvaluetoset2("SELECT * FROM t1 GROUP BY eidos")
        runquery("DROP TEMPORARY TABLE t1")
        If dt2.Rows.Count > 0 Then

            For x = 0 To Form1.datagridview2.RowCount - 1
                For y = 0 To dt2.Rows.Count - 1
                    If Form1.datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And Form1.datagridview2.Rows(x).Cells(10).Value <> 1 Then
                        Form1.datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                        Form1.datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                        Form1.datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                        Form1.datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                    End If
                Next
            Next
        End If


        runquery("create temporary table t1(select imerominia_egkrisis,posotita,offered_price,pelproms.eponymia,eidos from grammesprom join pelproms on pelprom=supplier where eidos in (select eidos from grammesprom where promitheia='" & Form1.lst(Form1.counter) & "') and ektos=1 and supplier_ektos=0 and offered_price is not null and imerominia_egkrisis is not null) union (select imerominia_egkrisis,posotita,offered_price,pelproms_ektos.epon,eidos from grammesprom join pelproms_ektos on pelproms_ektos.id=supplier where eidos in (select eidos from grammesprom where promitheia='" & Form1.lst(Form1.counter) & "') and ektos=1 and supplier_ektos=1 and offered_price is not null and imerominia_egkrisis is not null) order by imerominia_egkrisis desc")
        dtvaluetoset2("select * from t1 group by eidos")
        runquery("drop temporary table t1")
        If dt2.Rows.Count > 0 Then

            For x = 0 To Form1.datagridview2.RowCount - 1
                For y = 0 To dt2.Rows.Count - 1
                    If Form1.datagridview2.Rows(x).Cells(11).Value = dt2.Rows(y).Item(4) And Form1.datagridview2.Rows(x).Cells(10).Value = 1 Then
                        Form1.datagridview2.Rows(x).Cells(5).Value = dt2.Rows(y).Item(0)
                        Form1.datagridview2.Rows(x).Cells(6).Value = dt2.Rows(y).Item(1)
                        Form1.datagridview2.Rows(x).Cells(7).Value = dt2.Rows(y).Item(2)
                        Form1.datagridview2.Rows(x).Cells(8).Value = dt2.Rows(y).Item(3)
                    End If
                Next
            Next
        End If
        'idlist.Clear()
        'agglika = False

        Me.Close()

    End Sub

    Private Sub TODATETIME(DT As DateTime)
        Dim date1 As String = DT.Date.ToString

        Dim date3 As String() = date1.Split(" ")

        Dim date4 As String() = date3(0).Split("/")
        date6 = date4(2) + "-" + date4(1) + "-" + date4(0)
    End Sub

    Private Sub Form7_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        idlist.Clear()
        agglika = False
        Form1.multipleselected = False

    End Sub
End Class