Imports System.Data.OleDb

Public Class Form1
    Dim con As New OleDbConnection
    Dim constring As String
    Dim cmd As New OleDbCommand
    Dim dr As OleDbDataReader

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|/dat.accdb"
        With con

            .ConnectionString = constring
            .Open()

        End With

        Me.Label4.Text = "Connection State : Active!"
        Me.Label4.ForeColor = Color.DarkGreen

        FillStates()
    End Sub

    Public Sub FillStates()
        With cmd
            .Connection = con
            .CommandType = CommandType.Text
            .CommandText = "SELECT DISTINCT tbldat.State from tbldat"
        End With

        dr = cmd.ExecuteReader()
        While dr.Read()
            cbostates.Items.Add(dr("State"))
        End While
    End Sub

    Public Sub FillCities()
        Dim cmd1 As New OleDbCommand
        Dim dr1 As OleDbDataReader

        With cmd1
            .Connection = con
            .CommandType = CommandType.Text
            .CommandText = "SELECT * from tbldat where [State] ='" & Me.cbostates.Text & "'"
        End With

        dr1 = cmd1.ExecuteReader()

        While dr1.Read()
            cbocities.Items.Add(dr1("City"))
        End While
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        con.Close()
        Dispose()

    End Sub

    Private Sub cbostates_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbostates.SelectedIndexChanged
        cbocities.Text = "-Select-"
        If Me.cbocities.SelectedIndex >= -1 Then
            Me.cbocities.Items.Clear()
            FillCities()
        Else
            FillCities()

        End If
        
    End Sub
End Class
