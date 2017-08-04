Option Explicit On
Option Strict On

Imports System.Reflection
Imports Microsoft.VisualBasic

Public Class Settings
    Public Shared Function GetAppSetting(ByVal Setting As String, _
                                         Optional ByVal SettingGroup As String = "", _
                                         Optional ByVal SettingFilePath As String = "") As String
        Try

            If SettingFilePath = "" Then SettingFilePath = GetClassPath().ToUpper.Replace("FILE:", "") & "\Settings.xml"
            If SettingGroup = "" Then SettingGroup = "Settings"

            If Not System.IO.File.Exists(SettingFilePath) Then
                Throw New Exception("Tools -> GetAppSetting -> Setting File Not found : " & SettingFilePath)
            End If

            Dim DS As New DataSet
            DS.ReadXml(SettingFilePath)

            If DS.Tables(SettingGroup).Select("Setting = '" & Setting.Replace("'", "''") & "'").Length = 0 Then
                Throw New Exception("Tools -> GetAppSetting -> Setting Not found " & Setting)
            End If

            Return DS.Tables(SettingGroup).Select("Setting = '" & Setting.Replace("'", "''") & "'")(0).Item("Value").ToString

        Catch ex As Exception
            Throw New Exception("Tools -> GetAppSetting -> " & ex.Message)
        End Try
    End Function

    Public Shared Function SaveAppSetting(ByVal Setting As String, ByVal Value As String, _
                                          Optional ByVal SettingGroup As String = "", _
                                          Optional ByVal SettingFilePath As String = "") As Boolean
        Try
            If SettingFilePath = "" Then SettingFilePath = GetClassPath() & "\Settings.xml"
            If SettingGroup = "" Then SettingGroup = "Settings"

            Dim DS As New DataSet

            If System.IO.File.Exists(SettingFilePath) Then
                DS.ReadXml(SettingFilePath)
            End If

            If Not DS.Tables.Contains(SettingGroup) Then
                Dim DT As New DataTable(SettingGroup)
                DT.Columns.Add("Setting")
                DT.Columns.Add("Value")
                DS.Tables.Add(DT)
            End If

            If DS.Tables(SettingGroup).Select("Setting = '" & Setting & "'").Length = 0 Then
                Dim DR As DataRow = DS.Tables(SettingGroup).Rows.Add
                DR("Setting") = Setting
                DR("Value") = Value
            Else
                Dim DR As DataRow = DS.Tables(SettingGroup).Select("Setting = '" & Setting & "'")(0)
                DR("Setting") = Setting
                DR("Value") = Value
            End If

            DS.WriteXml(SettingFilePath)

            Return True

        Catch ex As Exception
            Throw New Exception("Tools -> SaveAppSetting -> " & ex.Message)
        End Try
    End Function

    Public Shared Function GetClassPath(Optional ByVal IncludeClassName As Boolean = False) As String
        Dim myAssy As [Assembly]
        myAssy = [Assembly].GetExecutingAssembly

        Dim Location As String = myAssy.GetCallingAssembly.CodeBase.ToString.Replace("file:///", "").Replace("/", "\")

        If IncludeClassName = False Then
            Location = Mid(Location, 1, InStrRev(Location, "\") - 1)
        End If

        Return Location
    End Function

    Public Shared Function GetBaseDirectory() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory()
    End Function

End Class