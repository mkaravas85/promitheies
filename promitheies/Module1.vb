Imports MySql.Data.MySqlClient
Module Module1

    Public mysqlcon As MySqlConnection
    Public command As MySqlCommand
    Public reader As MySqlDataReader
    Public sda, sda2 As New MySqlDataAdapter
    Public dt, dt2 As New DataTable
    Public bs, bs2 As New BindingSource
    Public trans As MySqlTransaction
    Public queryerror As Boolean = False
    'Public constr = "server=195.167.110.35;userid=remote;password=enapassremote!@#;database=dbtcan;port=3306"
    'Public constr = "server=192.168.0.241;userid=signet;password=enapass;database=dbtcan;port=3306"
    Public constr = "server=192.168.1.10;userid=signet;password=enapass;database=dbtcan;port=33953;"
    Public date5 As String
    Public grammi As Integer = -1
    Public norecordsfound As Boolean = False
    Public Sub runquery(ByVal query As String)
        queryerror = False
        mysqlcon = New MySqlConnection
        'mysqlcon.ConnectionString = "server=localhost;userid=root;password=12345;database=sigmix"
        mysqlcon.ConnectionString = constr

        Try
            mysqlcon.Open()
            trans = mysqlcon.BeginTransaction
            command = New MySqlCommand(query, mysqlcon)
            command.ExecuteNonQuery()
            'reader = command.ExecuteReader
            trans.Commit()
            mysqlcon.Close()
        Catch ex As Exception
            queryerror = True
            MessageBox.Show(ex.Message)
            trans.Rollback()

        End Try
        If mysqlcon.State = ConnectionState.Open Then
            mysqlcon.Close()
        End If

    End Sub
    Public Sub dtvaluetoset(ByVal query As String)
        queryerror = False
        mysqlcon = New MySqlConnection
        'mysqlcon.ConnectionString = "server=localhost;userid=root;password=12345;database=sigmix"
        mysqlcon.ConnectionString = constr
        dt.Reset()

        Try

            mysqlcon.Open()
            command = New MySqlCommand(query, mysqlcon)
            sda.SelectCommand = command
            sda.Fill(dt)
            bs.DataSource = dt

            sda.Update(dt)
            mysqlcon.Close()

        Catch ex As Exception
            queryerror = True
            MessageBox.Show(ex.Message)

        End Try

        If mysqlcon.State = ConnectionState.Open Then
            mysqlcon.Close()
        End If
    End Sub
    Public Sub dtvaluetoset2(ByVal query As String)
        queryerror = False
        mysqlcon = New MySqlConnection
        'mysqlcon.ConnectionString = "server=localhost;userid=root;password=12345;database=sigmix"
        mysqlcon.ConnectionString = constr
        dt2.Reset()

        Try

            mysqlcon.Open()
            command = New MySqlCommand(query, mysqlcon)
            sda2.SelectCommand = command
            sda2.Fill(dt2)
            bs2.DataSource = dt2

            sda2.Update(dt2)
            mysqlcon.Close()

        Catch ex As Exception
            queryerror = True
            MessageBox.Show(ex.Message)

        End Try

        If mysqlcon.State = ConnectionState.Open Then
            mysqlcon.Close()
        End If
    End Sub
    Public Sub runquerycombo(ByVal query As String, cmb As ComboBox, readeritem As String)
        mysqlcon = New MySqlConnection
        'mysqlcon.ConnectionString = "server=localhost;userid=root;password=12345;database=sigmix"
        mysqlcon.ConnectionString = constr

        Try
            mysqlcon.Open()
            command = New MySqlCommand(query, mysqlcon)

            reader = command.ExecuteReader
            While reader.Read()
                cmb.Items.Add(reader(readeritem))

            End While

            mysqlcon.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
        If mysqlcon.State = ConnectionState.Open Then
            mysqlcon.Close()
        End If

    End Sub
    Public Sub loadgrid(ByVal query As String, dgv As DataGridView)
        norecordsfound = False
        mysqlcon = New MySqlConnection
        'mysqlcon.ConnectionString = "server=localhost;userid=root;password=12345;database=sigmix"
        mysqlcon.ConnectionString = constr
        Dim sda As New MySqlDataAdapter
        Dim dt As New DataTable
        Dim bs As New BindingSource
        Try
            mysqlcon.Open()
            command = New MySqlCommand(query, mysqlcon)
            sda.SelectCommand = command
            sda.Fill(dt)
            If dt.Rows.Count = 0 Then
                norecordsfound = True

            End If
            bs.DataSource = dt
            dgv.DataSource = bs
            sda.Update(dt)
            mysqlcon.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        If mysqlcon.State = ConnectionState.Open Then
            mysqlcon.Close()
        End If
    End Sub
    Public Function returnsinglevaluequery(ByVal query As String) As Object
        Dim item As Object

        mysqlcon = New MySqlConnection
        'mysqlcon.ConnectionString = "server=localhost;userid=root;password=12345;database=sigmix"
        mysqlcon.ConnectionString = constr

        Try
            mysqlcon.Open()

            command = New MySqlCommand(query, mysqlcon)
            item = command.ExecuteScalar()

            mysqlcon.Close()
        Catch ex As Exception

            MessageBox.Show(ex.Message)

            Exit Function
        End Try
        If mysqlcon.State = ConnectionState.Open Then
            mysqlcon.Close()
        End If
        Return item
    End Function
    Public Sub TODATE(dtp As DateTimePicker, ByRef dateext As String)
        Dim date1 As String = dtp.Value.ToString

        Dim date3 As String() = date1.Split(" ")

        Dim date4 As String() = date3(0).Split("/")
        dateext = date4(2) + "-" + date4(1) + "-" + date4(0)
    End Sub

End Module
