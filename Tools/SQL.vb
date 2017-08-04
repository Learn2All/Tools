Option Explicit On
Option Strict On


Public Class SQL
    Shared _values As String
    Public Shared Property ORAConvalue() As String
        Get
            Return _values
        End Get
        Set(value As String)
            _values = value
        End Set

    End Property
    Private Shared ConfigSet As String = "Settings"

#Region "Common Functions"

    Public Sub New(ByVal ConfigurationSettings As String)
        ConfigSet = ConfigurationSettings
    End Sub

    Public Shared Function DBClean(ByVal Input As String) As String
        Input = Replace(Input, "'", "''")
        Return Input
    End Function

    Public Shared Function CheckDiff(ByVal OrigionalDT As DataTable, ByVal UpdatedDT As DataTable) As String
        Dim Changes As String = ""
        Try
            Dim I As Integer = 0
            For I = 0 To OrigionalDT.Rows.Count - 1
                For Each DC As DataColumn In OrigionalDT.Columns
                    If OrigionalDT.Rows(I).Item(DC.ColumnName).ToString <> UpdatedDT.Rows(I).Item(DC.ColumnName).ToString Then
                        Changes &= DC.ColumnName & " Changed From: " & OrigionalDT.Rows(I).Item(DC.ColumnName).ToString & " To: " & UpdatedDT.Rows(I).Item(DC.ColumnName).ToString & vbCrLf
                    End If
                Next
            Next
        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> CheckDiff -> " & ex.Message)
        End Try
        Return Changes
    End Function

#End Region


#Region "Default Functions"
    Public Shared Function GetConnString() As String

        Try
            Return Settings.GetAppSetting("SQLConn", ConfigSet)

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> GetConnString -> " & ex.Message)
        End Try
    End Function

    Public Shared Function CheckOnline(Optional ByVal ConnectionString As String = "") As Boolean

        Try
            'Check For Connect to SQL Database
            Dim DBConn As String = ""

            If ConnectionString <> "" Then
                DBConn = ConnectionString
            Else
                DBConn = Settings.GetAppSetting("SQLConn", ConfigSet)
            End If

            Using Conn As New SqlClient.SqlConnection(DBConn)
                Conn.Open()
                Conn.Close()
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Shared Function ExecuteDT(ByVal SQL As String, Optional ByVal TableName As String = "Data", Optional ByVal ConnectionString As String = "", Optional ByVal ConnectionTimeout As Integer = -1) As DataTable

        Dim DT As New DataTable(TableName)

        Try
            Dim DBConn As String = ""

            If ConnectionString <> "" Then
                DBConn = ConnectionString
            Else
                DBConn = Settings.GetAppSetting("SQLConn", ConfigSet)
            End If

            Using Conn As New SqlClient.SqlConnection(DBConn)



                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)

                If ConnectionTimeout <> -1 Then
                    cmd.CommandTimeout = ConnectionTimeout
                End If



                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> ExecuteDT -> " & ex.Message)
        End Try

        Return DT

    End Function

    Public Shared Function ExecuteSQL(ByVal SQL As String, Optional ByVal ConnectionString As String = "") As Boolean

        Try

            Dim DBConn As String = ""

            If ConnectionString <> "" Then
                DBConn = ConnectionString
            Else
                DBConn = Settings.GetAppSetting("SQLConn", ConfigSet)
            End If

            Using Conn As New SqlClient.SqlConnection(DBConn)
                Conn.Open()

                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)
                If cmd.ExecuteNonQuery = 0 Then
                    Return False
                Else
                    Return True
                End If
                Conn.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> ExecuteSQL -> " & ex.Message)
        End Try


    End Function

    Public Shared Function ExecuteSQLIdent(ByVal SQL As String, Optional ByVal ConnectionString As String = "") As String

        Try

            Dim DBConn As String = ""

            If ConnectionString <> "" Then
                DBConn = ConnectionString
            Else
                DBConn = Settings.GetAppSetting("SQLConn", ConfigSet)
            End If

            Using Conn As New SqlClient.SqlConnection(DBConn)
                Conn.Open()

                SQL &= " ; Select @@Identity"
                Dim DT As New DataTable("Data")
                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)

                Return DT.Rows(0).Item(0).ToString
                Conn.Close()
            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> ExecuteSQLIdent -> " & ex.Message)
        End Try


    End Function

    Public Shared Function ExecuteSingleValue(ByVal SQL As String, Optional ByVal ConnectionString As String = "") As String

        Dim DT As New DataTable("Data")
        Dim Value As String = ""

        Try

            Dim DBConn As String = ""

            If ConnectionString <> "" Then
                DBConn = ConnectionString
            Else
                DBConn = Settings.GetAppSetting("SQLConn", ConfigSet)
            End If

            Using Conn As New SqlClient.SqlConnection(DBConn)

                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

                If DT.Rows.Count > 0 Then
                    Value = DT.Rows(0).Item(0).ToString
                End If
            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> ExecuteSingleValue -> " & ex.Message & " -> " & SQL)
        End Try

        Return Value

    End Function

    Public Shared Function GetNextKey(ByVal Setting As String, Optional ByVal Prefix As String = "", Optional ByVal ConnectionString As String = "") As String

        Dim DT As New DataTable("Data")
        Dim Value As String = ""

        Try

            Dim DBConn As String = ""

            If ConnectionString <> "" Then
                DBConn = ConnectionString
            Else
                DBConn = Settings.GetAppSetting("SQLConn", ConfigSet)
            End If

            Using Conn As New SqlClient.SqlConnection(DBConn)

                Dim GetNextKeyStr As String = "BEGIN TRANSACTION"
                GetNextKeyStr &= " SET NOCOUNT ON"
                GetNextKeyStr &= " declare @result int"
                GetNextKeyStr &= " select @result = sys_value from systemsettings"
                GetNextKeyStr &= " where sys_setting = '" & Setting & "'"
                GetNextKeyStr &= " update systemsettings set sys_value = @result + 1 where sys_Setting = '" & Setting & "' "
                GetNextKeyStr &= " SET NOCOUNT OFF"
                GetNextKeyStr &= " select @result NextKey"
                GetNextKeyStr &= " COMMIT TRANSACTION"

                Dim cmd As New SqlClient.SqlCommand(GetNextKeyStr, Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

                If DT.Rows.Count > 0 Then
                    Value = DT.Rows(0).Item(0).ToString
                End If

            End Using

            If Prefix <> "" Then
                Value = Prefix & Right("000000" & Value, 6)
            End If

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> GetNextKey -> " & ex.Message)
        End Try

        Return Value

    End Function

    Public Shared Function ToSQLDate(ByVal InputDate As String, Optional ByVal IncludeTime As Boolean = False) As String
        Try

            Dim SQLDate As String = Settings.GetAppSetting("SQLDateFormat", ConfigSet)

            Dim Day, Month, Year As String
            Dim Time As String = "00:00:00"

            'dd/MM/yyyy HH:mm:ss

            'Check For Year First or day first
            If InStr(InputDate, "/") = 5 Then
                Year = InputDate.Substring(0, 4)
                Month = InputDate.Substring(5, 2)
                Day = InputDate.Substring(8, 2)
            Else
                Day = InputDate.Substring(0, 2)
                Month = InputDate.Substring(3, 2)
                Year = InputDate.Substring(6, 4)
            End If

            Month = MonthName(CInt(Month)).ToUpper

            If IncludeTime = True Then
                If InStr(InputDate, ":") > 0 Then
                    If Len(InputDate) > 16 Then
                        Time = InputDate.Substring(11, 8)
                    Else
                        Time = InputDate.Substring(11, 5) & ":00"
                    End If
                End If
            End If

            InputDate = SQLDate.Replace("day", Day).Replace("month", Month).Replace("year", Year)

            If IncludeTime = True Then
                InputDate = InputDate & " " & Time
            End If


        Catch ex As Exception

        End Try

        Return InputDate

    End Function

    Public Shared Function GetRefName(ByVal ReferenceID As String, ByVal ReferenceType As String) As DataTable

        Dim DT As New DataTable("Lookup")

        Try

            Dim DBConn As String = Settings.GetAppSetting("SQLConn", ConfigSet)

            Using Conn As New SqlClient.SqlConnection(DBConn)
                Dim SQL As String = ""
                SQL = "Select * from ReferenceLookups Where REF_Delete = 0 "

                If ReferenceID <> "" Then
                    SQL &= "AND REF_ID = '" & ReferenceID & "' "
                End If
                If ReferenceType <> "" Then
                    SQL &= "AND REF_Type = '" & ReferenceType & "' "
                End If

                SQL &= "Order by REF_Desc1"

                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)

                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

            End Using

        Catch ex As Exception

            Dim ext As New Exception("Tools -> GetRefName -> " & ex.Message)
            Throw ext
        End Try

        Return DT

    End Function

    Public Shared Function GetSetting(ByVal Setting As String, Optional ByVal ConnectionString As String = "") As String

        Dim DT As New DataTable("Data")
        Dim Value As String = ""

        Try

            Dim DBConn As String = ""

            If ConnectionString <> "" Then
                DBConn = ConnectionString
            Else
                DBConn = Settings.GetAppSetting("SQLConn", ConfigSet)
            End If

            Using Conn As New SqlClient.SqlConnection(DBConn)

                Dim cmd As New SqlClient.SqlCommand("Select SYS_Value From SystemSettings Where SYS_Setting = '" & Setting & "'", Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

                If DT.Rows.Count > 0 Then
                    Value = DT.Rows(0).Item(0).ToString
                End If
            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> GetSetting -> " & ex.Message & " ConnectionString = " & Settings.GetAppSetting("SQLConn", ConfigSet))
        End Try

        Return Value

    End Function

    Public Shared Function SaveSetting(ByVal Setting As String, ByVal Value As String, Optional ByVal ConnectionString As String = "") As Boolean

        Dim DT As New DataTable("Data")

        Try

            Dim DBConn As String = ""

            If ConnectionString <> "" Then
                DBConn = ConnectionString
            Else
                DBConn = Settings.GetAppSetting("SQLConn", ConfigSet)
            End If

            Return ExecuteSQL("Update SystemSettings Set SYS_Value = '" & Value & "' Where SYS_Setting = '" & Setting & "'", DBConn)

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> SaveSetting -> " & ex.Message & " ConnectionString = " & Settings.GetAppSetting("SQLConn", ConfigSet))
        End Try

    End Function
  


#End Region

#Region "Override Functions"
    Public Shared Function GetConnStringOverride(ByVal OverrideConfigSet As String) As String

        Try
            Return Settings.GetAppSetting("SQLConn", OverrideConfigSet)

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> GetConnStringOverride -> " & ex.Message)
        End Try
    End Function

    Public Shared Function CheckOnlineOverride(ByVal OverrideConfigSet As String) As Boolean

        Try
            'Check For Connect to SQL Database
            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)

            Using Conn As New SqlClient.SqlConnection(DBConn)
                Conn.Open()
                Conn.Close()
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Shared Function ExecuteDTOverride(ByVal SQL As String, ByVal OverrideConfigSet As String, Optional ByVal TableName As String = "Data") As DataTable

        Dim DT As New DataTable(TableName)

        Try
            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)

            Using Conn As New SqlClient.SqlConnection(DBConn)

                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> ExecuteDTOverride -> " & ex.Message)
        End Try

        Return DT

    End Function

    Public Shared Function ExecuteSQLOverride(ByVal SQL As String, ByVal OverrideConfigSet As String) As Boolean

        Try

            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)


            Using Conn As New SqlClient.SqlConnection(DBConn)
                Conn.Open()

                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)
                If cmd.ExecuteNonQuery = 0 Then
                    Return False
                Else
                    Return True
                End If
                Conn.Close()

            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> ExecuteSQLOverride -> " & ex.Message)
        End Try


    End Function

    Public Shared Function ExecuteSQLIdentOverride(ByVal SQL As String, ByVal OverrideConfigSet As String) As String

        Try

            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)


            Using Conn As New SqlClient.SqlConnection(DBConn)
                Conn.Open()

                SQL &= " ; Select @@Identity"
                Dim DT As New DataTable("Data")
                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)

                Return DT.Rows(0).Item(0).ToString
                Conn.Close()
            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> ExecuteSQLIdentOverride -> " & ex.Message)
        End Try


    End Function

    Public Shared Function ExecuteSingleValueOverride(ByVal SQL As String, ByVal OverrideConfigSet As String) As String

        Dim DT As New DataTable("Data")
        Dim Value As String = ""

        Try

            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)


            Using Conn As New SqlClient.SqlConnection(DBConn)

                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

                If DT.Rows.Count > 0 Then
                    Value = DT.Rows(0).Item(0).ToString
                End If
            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> ExecuteSingleValueOverride -> " & ex.Message)
        End Try

        Return Value

    End Function

    Public Shared Function GetNextKeyOverride(ByVal Setting As String, ByVal OverrideConfigSet As String, Optional ByVal Prefix As String = "") As String

        Dim DT As New DataTable("Data")
        Dim Value As String = ""

        Try

            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)


            Using Conn As New SqlClient.SqlConnection(DBConn)

                Dim GetNextKeyStr As String = "BEGIN TRANSACTION"
                GetNextKeyStr &= " SET NOCOUNT ON"
                GetNextKeyStr &= " declare @result int"
                GetNextKeyStr &= " select @result = sys_value from systemsettings"
                GetNextKeyStr &= " where sys_setting = '" & Setting & "'"
                GetNextKeyStr &= " update systemsettings set sys_value = @result + 1 and sys_Setting = '" & Setting & "' "
                GetNextKeyStr &= " SET NOCOUNT OFF"
                GetNextKeyStr &= " select @result NextKey"
                GetNextKeyStr &= " COMMIT TRANSACTION"

                Dim cmd As New SqlClient.SqlCommand(GetNextKeyStr, Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

                If DT.Rows.Count > 0 Then
                    Value = DT.Rows(0).Item(0).ToString
                End If

            End Using

            If Prefix <> "" Then
                Value = Prefix & Right("000000" & Value, 6)
            End If

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> GetNextKeyOverride -> " & ex.Message)
        End Try

        Return Value

    End Function

    Public Shared Function ToSQLDateOverride(ByVal InputDate As String, ByVal OverrideConfigSet As String, Optional ByVal IncludeTime As Boolean = False) As String
        Try

            Dim SQLDate As String = Settings.GetAppSetting("SQLDateFormat", OverrideConfigSet)

            Dim Day, Month, Year As String
            Dim Time As String = "00:00:00"

            'dd/MM/yyyy HH:mm:ss

            'Check For Year First or day first
            If InStr(InputDate, "/") = 5 Then
                Year = InputDate.Substring(0, 4)
                Month = InputDate.Substring(5, 2)
                Day = InputDate.Substring(8, 2)
            Else
                Day = InputDate.Substring(0, 2)
                Month = InputDate.Substring(3, 2)
                Year = InputDate.Substring(6, 4)
            End If

            Month = MonthName(CInt(Month)).ToUpper

            If IncludeTime = True Then
                If InStr(InputDate, ":") > 0 Then
                    If Len(InputDate) > 16 Then
                        Time = InputDate.Substring(11, 8)
                    Else
                        Time = InputDate.Substring(11, 5) & ":00"
                    End If
                End If
            End If

            InputDate = SQLDate.Replace("day", Day).Replace("month", Month).Replace("year", Year)

            If IncludeTime = True Then
                InputDate = InputDate & " " & Time
            End If


        Catch ex As Exception

        End Try

        Return InputDate

    End Function

    Public Shared Function GetRefNameOverride(ByVal ReferenceID As String, ByVal ReferenceType As String, ByVal OverrideConfigSet As String) As DataTable

        Dim DT As New DataTable("Lookup")

        Try

            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)

            Using Conn As New SqlClient.SqlConnection(DBConn)
                Dim SQL As String = ""
                SQL = "Select * from ReferenceLookups Where REF_Delete = 0 "

                If ReferenceID <> "" Then
                    SQL &= "AND REF_ID = '" & ReferenceID & "' "
                End If
                If ReferenceType <> "" Then
                    SQL &= "AND REF_Type = '" & ReferenceType & "' "
                End If

                SQL &= "Order by REF_Desc1"

                Dim cmd As New SqlClient.SqlCommand(SQL, Conn)

                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

            End Using

        Catch ex As Exception

            Dim ext As New Exception("Tools -> GetRefNameOverride -> " & ex.Message)
            Throw ext
        End Try

        Return DT

    End Function

    Public Shared Function GetSettingOverride(ByVal Setting As String, ByVal OverrideConfigSet As String) As String

        Dim DT As New DataTable("Data")
        Dim Value As String = ""

        Try
            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)

            Using Conn As New SqlClient.SqlConnection(DBConn)

                Dim cmd As New SqlClient.SqlCommand("Select SYS_Value From SystemSettings Where SYS_Setting = '" & Setting & "'", Conn)
                Dim Da As New SqlClient.SqlDataAdapter(cmd)
                Da.Fill(DT)
                Conn.Close()

                If DT.Rows.Count > 0 Then
                    Value = DT.Rows(0).Item(0).ToString
                End If
            End Using

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> GetSettingOverride -> " & ex.Message & " ConnectionString = " & Settings.GetAppSetting("SQLConn", OverrideConfigSet))
        End Try

        Return Value

    End Function

    Public Shared Function SaveSettingOverride(ByVal Setting As String, ByVal Value As String, ByVal OverrideConfigSet As String) As Boolean

        Dim DT As New DataTable("Data")

        Try

            Dim DBConn As String = Settings.GetAppSetting("SQLConn", OverrideConfigSet)

            Return ExecuteSQL("Update SystemSettings Set SYS_Value = '" & Value & "' Where SYS_Setting = '" & Setting & "'", DBConn)

        Catch ex As Exception
            Throw New Exception("Tools -> SQL -> SaveSettingOverride -> " & ex.Message & " ConnectionString = " & Settings.GetAppSetting("SQLConn", OverrideConfigSet))
        End Try

    End Function
#End Region

End Class
