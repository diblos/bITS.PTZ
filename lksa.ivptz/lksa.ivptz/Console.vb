Public Class Console

    Dim LISTBOX_CLEARLINES As Integer = 5000

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ListBox1.BackColor = Color.Black
        ListBox1.ForeColor = Color.Lime

    End Sub

    Public Delegate Sub AddItemsToListBoxDelegate( _
       ByVal ToListBox As ListBox, _
       ByVal AddText As String)

    Private Sub AddItemsToListBox(ByVal ToListBox As ListBox, _
                                 ByVal AddText As String)
        If ToListBox.Items.Count > LISTBOX_CLEARLINES Then
            ToListBox.Items.Clear()
            ToListBox.Items.Add("Old verboses truncated")
        End If

        ToListBox.Items.Add(AddText)
        ToListBox.SetSelected(ListBox1.Items.Count - 1, True)
        ToListBox.SetSelected(ListBox1.Items.Count - 1, False)
    End Sub

    Public Sub AddMessage(messageText As String)

        If (ListBox1.InvokeRequired) Then
            ListBox1.Invoke( _
                    New AddItemsToListBoxDelegate(AddressOf AddItemsToListBox), _
                    New Object() {ListBox1, (Convert.ToString(System.DateTime.UtcNow + " : ") & messageText)})
        Else
            If Me.ListBox1.Items.Count > LISTBOX_CLEARLINES Then
                ListBox1.Items.Clear()
                Me.ListBox1.Items.Add("Old verboses truncated")
            End If

            Me.ListBox1.Items.Add((Convert.ToString(System.DateTime.UtcNow + " : ") & messageText))
            Me.ListBox1.SetSelected(ListBox1.Items.Count - 1, True)
            Me.ListBox1.SetSelected(ListBox1.Items.Count - 1, False)
        End If

    End Sub
End Class