Public Class Form8
    Public Case8 As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Case8 = "arxiki" Then
            runquery("UPDATE prom_mail_texts set arxikoell='" & TextBox1.Text & "';UPDATE prom_mail_texts set arxikoen='" & TextBox2.Text & "';")
        ElseIf Case8 = "teliki" Then
            runquery("UPDATE prom_mail_texts set telikoell='" & TextBox1.Text & "';UPDATE prom_mail_texts set telikoen='" & TextBox2.Text & "';")
        End If
        If queryerror = False Then
            MessageBox.Show("Επιτυχής Καταχώρηση !")
            If Form7.agglika = False Then
                Form7.TextBox3.Text = returnsinglevaluequery("SELECT arxikoell FROM prom_mail_texts")
                Form7.TextBox2.Text = returnsinglevaluequery("SELECT telikoell FROM prom_mail_texts")
            Else
                Form7.TextBox3.Text = returnsinglevaluequery("SELECT arxikoen FROM prom_mail_texts")
                Form7.TextBox2.Text = returnsinglevaluequery("SELECT telikoen FROM prom_mail_texts")

            End If
            Me.Close()
        End If
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ShowIcon = False
        If Case8 = "arxiki" Then
            TextBox1.Text = returnsinglevaluequery("SELECT arxikoell FROM prom_mail_texts")
            TextBox2.Text = returnsinglevaluequery("SELECT arxikoen FROM prom_mail_texts")
        ElseIf Case8 = "teliki" Then
            TextBox1.Text = returnsinglevaluequery("SELECT telikoell FROM prom_mail_texts")
            TextBox2.Text = returnsinglevaluequery("SELECT telikoen FROM prom_mail_texts")
        End If
    End Sub
End Class