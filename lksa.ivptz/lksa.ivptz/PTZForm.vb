Imports System.Windows.Forms
Imports ControlCenterVideoSDK
Imports AxControlCenterVideoSDK
Imports System.Runtime.InteropServices

Public Class PTZForm
    Dim nSize As Size = New Size(600, 550)
    Dim TAB As String = StrDup(3, " ")

    Dim DEBUG_MODE As Boolean = True
    Private textConsole As Console

    Private usernameTextBox As String = ""
    Private passwordTextBox As String = ""
    Private accessUrlTextBox As String = "http://10.1.166.6/onvif/device_service"
    Private profileTextBox As String = "Profile1"


    Private Shared MaxPanTiltSpeed As Integer = 5
    Private Shared MaxZoomSpeed As Integer = 1
    Private m_ptz As Ptz = Nothing
    Private m_isOnvifProtocol As Boolean = True

    '
    ' Store the event handlers, so they can be removed before the Ptz
    ' object is destroyed
    '
    Private initCompletedHandler As _IPtzEvents_InitCompletedEventHandler = Nothing
    Private presetStateChangedHandler As _IPtzEvents_PresetStateChangedEventHandler = Nothing
    Private ptzErrorHandler As _IPtzEvents_PtzErrorEventHandler = Nothing


    Private Sub PTZForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        With Me
            .Size = nSize
            .MaximumSize = nSize
            .MinimumSize = nSize
        End With

        With ToolStripStatusLabel1
            '.Width = StatusStrip1.Width / 2
            .Text = TAB & "Status: Connected" & TAB
            '.Spring = True
        End With

        With ToolStripStatusLabel2
            '.Width = StatusStrip1.Width / 2
            .Text = TAB & "Date: " & Now.ToString("d/M/yyyy   H:mm:ss tt") & TAB
            .Dock = DockStyle.Right
            '.Spring = True
        End With

        With ToolStripStatusLabel3
            '.Width = StatusStrip1.Width / 2
            .Text = StrDup(85, " ")
            .Dock = DockStyle.Right
            '.Spring = True
        End With

        If DEBUG_MODE Then
            textConsole = New Console()
            textConsole.Show()

            textConsole.AddMessage("Example Startup.")
        End If

    End Sub

    Private Sub LoadVideo()

        Dim username As String = usernameTextBox
        Dim password As String = passwordTextBox

        textConsole.AddMessage("Associating Camera.")
        videoControl.Pane.SetCredentials(username, password)

        Try
            videoControl.Pane.StartLiveVideo(accessUrlTextBox, profileTextBox, StreamTransClass.BestEffort)
        Catch ex As System.ArgumentException
            textConsole.AddMessage("Start Video exception caught : " + ex.ToString())
        End Try

    End Sub

    Private Sub UnloadVideo()

        videoControl.Pane.StopVideo()
        textConsole.AddMessage("Video stream stopped.")

    End Sub

    Private Sub PTZForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        Debug.Print(Me.Width & vbTab & Me.Height)

    End Sub

#Region "PTZ"

    ''' <summary>
    ''' Sends a ContinuousMove command to the PTZ camera, and handles
    ''' any errors returned by the Ptz object.
    ''' </summary>
    ''' <param name="pan">The pan speed (-5 to +5)</param>
    ''' <param name="tilt">The tilt speed (-5 to +5)</param>
    ''' <param name="zoom">The zoom speed (-1 to +1)</param>
    Private Sub SendContinuousMove(pan As Integer, tilt As Integer, zoom As Integer)
        If m_ptz Is Nothing Then
            textConsole.AddMessage("Ptz object not instantiated.")
            Return
        End If

        Try
            '
            ' Send the ContinuousMove command
            ' The timeout specifies the value for the timeout of the
            ' movement operation. The camera will stop moving when
            ' this timeout is exceeded.
            ' In order to extend the move command, call ContinuousMove
            ' again before the timeout.
            '
            m_ptz.ContinuousMove(pan, tilt, zoom, m_ptz.TimeoutDefault)
        Catch ex As System.Exception
            textConsole.AddMessage(New String("Exception thrown by ContinuousMove: {0}", ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Sends a GoToPreset command to the PTZ camera, and handles
    ''' any errors returned by the Ptz object.
    ''' </summary>
    ''' <param name="presetToken">The token of the preset to move to</param>
    Private Sub SendGoToPreset(presetToken As String)
        If m_ptz Is Nothing Then
            textConsole.AddMessage("Ptz object not instantiated.")
            Return
        End If

        Try
            m_ptz.GoToPreset(presetToken)
        Catch ex As System.Exception
            textConsole.AddMessage(New String("Exception thrown by GoToPreset: {0}", ex.Message))
        End Try
    End Sub
    ''' <summary>
    ''' Populates the preset list box
    ''' </summary>
    Private Sub LoadPresets()
        '
        ' Populate the preset list with an invalid preset, to test
        ' the PtzError event
        '
        'lstPresets.Items.Add("testInvalidPreset")
        ComboPreset.Items.Add("testInvalidPreset")

        '
        ' Presets are returned as an array of arrays, so iterate through
        ' the first array (the presets), then select the first element
        ' of the inner arrays for the token to add to the list
        '
        Try
            For Each preset As Array In DirectCast(m_ptz.Presets, Array)
                '
                ' The sub array in a preset is {token, name}, so 
                ' retrieve the 0th element (the token) and add it
                ' to the preset list, and write the 0th & 1st elements
                ' (token & name) to the debug output
                '
                Dim presetToken As String = preset.GetValue(0).ToString()
                Dim presetName As String = preset.GetValue(1).ToString()

                textConsole.AddMessage(New String("Adding camera preset: {0} ({1})", presetToken, presetName))
                'lstPresets.Items.Add(presetToken)
                ComboPreset.Items.Add(presetToken)
            Next
        Catch ex As System.Exception
            textConsole.AddMessage(New String("Exception retrieving presets: {0}", ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Tidies up everything and returns the app to the not connected
    ''' state
    ''' </summary>
    ''' <remarks>
    ''' This function calls ReleaseComObject on the Ptz object, releasing
    ''' it immediately. This function should NOT be called in a Ptz object
    ''' event handler directly. If it must be called, redirect the call
    ''' through a delegate, using BeginInvoke, to ensure the event handler
    ''' returns before the Ptz object is released. See ptz_InitComplete()
    ''' for an example.
    ''' </remarks>
    Private Sub Reset()
        '
        ' Clear out any existing presets
        ' 
        'lstPresets.Items.Clear()
        ComboPreset.Items.Clear()

        '
        ' Tidy up our handlers (if they have been created)
        ' 
        If initCompletedHandler IsNot Nothing Then
            m_ptz.InitCompleted -= initCompletedHandler
            m_ptz.PresetStateChanged -= presetStateChangedHandler
            m_ptz.PtzError -= ptzErrorHandler
            initCompletedHandler = Nothing
            presetStateChangedHandler = Nothing
            ptzErrorHandler = Nothing
        End If

        If m_ptz IsNot Nothing Then
            Marshal.ReleaseComObject(m_ptz)
            m_ptz = Nothing
        End If

        '
        ' Disable the parts of the GUI that can't be used until there is
        ' a camera connected
        ' 
        EnableConnectedControls(False)
        EnableDisconnectedControls(True)
        textConsole.AddMessage("No connection: configure parameters and press ""connect"".")
    End Sub

    ''' <summary>
    ''' Updates the enabled state of various controls based on whether
    ''' the app is disconnected, connecting or connected to a camera
    ''' </summary>
    ''' <param name="connected">
    ''' True = The app is connected to a camera
    ''' </param>
    Private Sub EnableConnectedControls(connected As Boolean)
        '
        ' Controls that are only valid when the app is connected to a
        ' camera
        '
        'grpPresets.Enabled = connected

        'grpPtz.Enabled = connected
        'btnDisconnect.Enabled = connected
    End Sub

    ''' <summary>
    ''' Updates the enabled state of various controls based on whether
    ''' the app is disconnected, connecting or connected to a camera
    ''' </summary>
    ''' <param name="disconnected">
    ''' True = The app is disconnected from the camera
    ''' </param>
    Private Sub EnableDisconnectedControls(disconnected As Boolean)
        '
        ' Controls that are only valid when the app is not connected to a
        ' camera
        ' 
        'btnConnect.Enabled = disconnected

        'txtIpAddress.Enabled = disconnected
        'txtUsername.Enabled = disconnected
        'txtPassword.Enabled = disconnected
        'txtProfile.Enabled = disconnected

        'lblIpAddress.Enabled = disconnected
        'lblUsername.Enabled = disconnected
        'lblPassword.Enabled = disconnected
        'lblProfile.Enabled = disconnected

        'lblProtocol.Enabled = disconnected
        'rdoProtocolOnvif.Enabled = disconnected
        'rdoProtocolIV.Enabled = disconnected

        'lblConfigFile.Enabled = disconnected
        'txtConfigFile.Enabled = disconnected
        'btnBrowse.Enabled = disconnected

        'lblPriority.Enabled = disconnected
        'numPriority.Enabled = disconnected

        'lblDescription.Enabled = disconnected
        'txtDescription.Enabled = disconnected
    End Sub


    ''' <summary>
    ''' Moves the camera left
    ''' </summary>
    ''' <param name="sender">See MSDN Button.Click Event</param>
    ''' <param name="e">See MSDN Button.Click Event</param>
    Private Sub btnPanLeft_Click(sender As Object, e As EventArgs) Handles ButtonLeft.Click
        ' Pan
        ' Tilt
        ' Zoom
        SendContinuousMove(-MaxPanTiltSpeed, 0, 0)
    End Sub

    ''' <summary>
    ''' Moves the camera right
    ''' </summary>
    ''' <param name="sender">See MSDN Button.Click Event</param>
    ''' <param name="e">See MSDN Button.Click Event</param>
    Private Sub btnPanRight_Click(sender As Object, e As EventArgs) Handles ButtonRight.Click
        ' Pan
        ' Tilt
        ' Zoom
        SendContinuousMove(MaxPanTiltSpeed, 0, 0)
    End Sub


    ''' <summary>
    ''' Moves the camera down
    ''' </summary>
    ''' <param name="sender">See MSDN Button.Click Event</param>
    ''' <param name="e">See MSDN Button.Click Event</param>
    Private Sub btnTiltDown_Click(sender As Object, e As EventArgs) Handles ButtonDown.Click
        ' Pan
        ' Tilt
        ' Zoom
        SendContinuousMove(0, -MaxPanTiltSpeed, 0)
    End Sub

    ''' <summary>
    ''' Moves the camera up
    ''' </summary>
    ''' <param name="sender">See MSDN Button.Click Event</param>
    ''' <param name="e">See MSDN Button.Click Event</param>
    Private Sub btnTiltUp_Click(sender As Object, e As EventArgs) Handles ButtonUp.Click
        ' Pan
        ' Tilt
        ' Zoom
        SendContinuousMove(0, MaxPanTiltSpeed, 0)
    End Sub

    ''' <summary>
    ''' Zooms the camera out
    ''' </summary>
    ''' <param name="sender">See MSDN Button.Click Event</param>
    ''' <param name="e">See MSDN Button.Click Event</param>
    Private Sub btnZoomOut_Click(sender As Object, e As EventArgs) Handles ButtonMinus.Click
        ' Pan
        ' Tilt
        ' Zoom
        SendContinuousMove(0, 0, -MaxZoomSpeed)
    End Sub

    ''' <summary>
    ''' Zooms the camera in
    ''' </summary>
    ''' <param name="sender">See MSDN Button.Click Event</param>
    ''' <param name="e">See MSDN Button.Click Event</param>
    Private Sub btnZoomIn_Click(sender As Object, e As EventArgs) Handles ButtonPlus.Click
        ' Pan
        ' Tilt
        ' Zoom
        SendContinuousMove(0, 0, MaxZoomSpeed)
    End Sub

    ''' <summary>
    ''' Stops any PTZ movement started with ContinuousMove by passing 0
    ''' speed for each parameter.
    ''' </summary>
    ''' <param name="sender">See MSDN Button.Click Event</param>
    ''' <param name="e">See MSDN Button.Click Event</param>
    Private Sub btnPtzStop_Click(sender As Object, e As EventArgs)
        ' Pan
        ' Tilt
        ' Zoom
        SendContinuousMove(0, 0, 0)
    End Sub



#End Region

#Region "Video Panel"


    ''' <summary>
    ''' Fired when a stream connection occurs
    ''' </summary>
    ''' <param name="sender">object that raised the event</param>
    ''' <param name="e">arguments for the event</param>
    Private Sub videoControl_StreamConnected(sender As Object, e As _IControlEvents_StreamConnectedEvent) Handles videoControl.StreamConnected
        textConsole.AddMessage("Stream Connected")
    End Sub

    ''' <summary>
    ''' Fired when a stream disconnection occurs
    ''' </summary>
    ''' <param name="sender">object that raised the event</param>
    ''' <param name="e">arguments for the event</param>
    Private Sub videoControl_StreamDisconnected(sender As Object, e As _IControlEvents_StreamDisconnectedEvent) Handles videoControl.StreamDisconnected
        textConsole.AddMessage("Stream Disconnected")
    End Sub

    ''' <summary>
    ''' Fired when a stream error occurs
    ''' </summary>
    ''' <param name="sender">object that raised the event</param>
    ''' <param name="e">arguments for the event</param>
    Private Sub videoControl_StreamError(sender As Object, e As _IControlEvents_StreamErrorEvent) Handles videoControl.StreamError
        textConsole.AddMessage("Stream Error, ClientError value : " + e.clientError.ToString())
    End Sub

    ''' <summary>
    ''' Fired when a stream starts to transfer
    ''' </summary>
    ''' <param name="sender">object that raised the event</param>
    ''' <param name="e">arguments for the event</param>
    Private Sub videoControl_StreamStarted(sender As Object, e As _IControlEvents_StreamStartedEvent) Handles videoControl.StreamStarted
        textConsole.AddMessage("Stream Started")
        'snapshotGroupBox.Enabled = True
    End Sub

#End Region

End Class