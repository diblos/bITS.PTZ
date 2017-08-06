Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ControlCenterVideoSDK

Namespace PtzExample
    Partial Public Class MainForm
        Inherits Form
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

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New()
            InitializeComponent()
        End Sub

        ''' <summary>
        ''' Sends a ContinuousMove command to the PTZ camera, and handles
        ''' any errors returned by the Ptz object.
        ''' </summary>
        ''' <param name="pan">The pan speed (-5 to +5)</param>
        ''' <param name="tilt">The tilt speed (-5 to +5)</param>
        ''' <param name="zoom">The zoom speed (-1 to +1)</param>
        Private Sub SendContinuousMove(pan As Integer, tilt As Integer, zoom As Integer)
            If m_ptz Is Nothing Then
                WriteEventLog("Ptz object not instantiated.")
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
                WriteEventLog("Exception thrown by ContinuousMove: {0}", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Sends a GoToPreset command to the PTZ camera, and handles
        ''' any errors returned by the Ptz object.
        ''' </summary>
        ''' <param name="presetToken">The token of the preset to move to</param>
        Private Sub SendGoToPreset(presetToken As String)
            If m_ptz Is Nothing Then
                WriteEventLog("Ptz object not instantiated.")
                Return
            End If

            Try
                m_ptz.GoToPreset(presetToken)
            Catch ex As System.Exception
                WriteEventLog("Exception thrown by GoToPreset: {0}", ex.Message)
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
            lstPresets.Items.Add("testInvalidPreset")

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

                    WriteEventLog("Adding camera preset: {0} ({1})", presetToken, presetName)
                    lstPresets.Items.Add(presetToken)
                Next
            Catch ex As System.Exception
                WriteEventLog("Exception retrieving presets: {0}", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Writes any debug info to the debug text box, adding the date in
        ''' front
        ''' </summary>
        ''' <param name="format">See MSDN String.Format</param>
        ''' <param name="args">See MSDN String.Format</param>
        Private Sub WriteEventLog(format As String, ParamArray args As Object())
            txtDebugOutput.AppendText(String.Format("{0} - {1}" & vbLf, DateTime.Now.ToString(), String.Format(format, args)))
        End Sub

        ''' <summary>
        ''' Form loading event - just calls reset to ensure everything is
        ''' in the correct state for the user to configure and connect.
        ''' </summary>
        ''' <param name="sender">See MSDN Form.Load Event</param>
        ''' <param name="e">See MSDN Form.Load Event</param>
        Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load

            Dim nSize As Size = Me.Size
            Me.MaximumSize = nSize

            Reset()
        End Sub

        ''' <summary>
        ''' Form Closed event - removes the event handlers, so the COM object
        ''' can be cleaned up correctly
        ''' </summary>
        ''' <param name="sender">See MSDN Form.Closed Event</param>
        ''' <param name="e">See MSDN Form.Closed Event</param>
        Private Sub MainForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
            Reset()
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
            lstPresets.Items.Clear()

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
            WriteEventLog("No connection: configure parameters and press ""connect"".")
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
            grpPresets.Enabled = connected

            grpPtz.Enabled = connected
            btnDisconnect.Enabled = connected
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
            btnConnect.Enabled = disconnected

            txtIpAddress.Enabled = disconnected
            txtUsername.Enabled = disconnected
            txtPassword.Enabled = disconnected
            txtProfile.Enabled = disconnected

            lblIpAddress.Enabled = disconnected
            lblUsername.Enabled = disconnected
            lblPassword.Enabled = disconnected
            lblProfile.Enabled = disconnected

            lblProtocol.Enabled = disconnected
            rdoProtocolOnvif.Enabled = disconnected
            rdoProtocolIV.Enabled = disconnected

            lblConfigFile.Enabled = disconnected
            txtConfigFile.Enabled = disconnected
            btnBrowse.Enabled = disconnected

            lblPriority.Enabled = disconnected
            numPriority.Enabled = disconnected

            lblDescription.Enabled = disconnected
            txtDescription.Enabled = disconnected
        End Sub

        ''' <summary>
        ''' Moves the camera left
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>


        ''' <summary>
        ''' Moves the camera right
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>
        Private Sub btnPanRight_Click(sender As Object, e As EventArgs) Handles btnPanRight.Click
            ' Pan
            ' Tilt
            ' Zoom
            SendContinuousMove(MaxPanTiltSpeed, 0, 0)
        End Sub

        ''' <summary>
        ''' Moves the camera up and to the right
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>
        Private Sub btnUpRight_Click(sender As Object, e As EventArgs) Handles btnUpRight.Click
            ' Pan
            ' Tilt
            ' Zoom
            SendContinuousMove(MaxPanTiltSpeed, MaxPanTiltSpeed, 0)
        End Sub

        ''' <summary>
        ''' Moves the camera up and to the left
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>


        ''' <summary>
        ''' Moves the camera down and to the left
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>


        ''' <summary>
        ''' Moves the camera down and to the right
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>


        ''' <summary>
        ''' Moves the camera down
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>


        ''' <summary>
        ''' Moves the camera up
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>
        Private Sub btnTiltUp_Click(sender As Object, e As EventArgs) Handles btnTiltUp.Click
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


        ''' <summary>
        ''' Zooms the camera in
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>
        Private Sub btnZoomIn_Click(sender As Object, e As EventArgs) Handles btnZoomIn.Click
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
        Private Sub btnPtzStop_Click(sender As Object, e As EventArgs) Handles btnPtzStop.Click
            ' Pan
            ' Tilt
            ' Zoom
            SendContinuousMove(0, 0, 0)
        End Sub

        ''' <summary>
        ''' Uses the currently selected preset list item, and requests
        ''' the PTZ unit to move to the preset with that token
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>


        ''' <summary>
        ''' InitCompleted event handler:
        ''' Called when the Init() completes, with the result of the
        ''' initialisation in initResult. 
        ''' Writes the event to the event text box.
        ''' </summary>
        ''' <param name="initResult">
        ''' The result code of the initialisation
        ''' </param>
        Public Sub ptz_InitCompleted(initResult As ClientErrorType)
            WriteEventLog("Event InitComplete( {0} ) received" & vbLf, initResult)

            '
            ' If the camera has been initialised correctly, then show the
            ' timeouts & load the presets.
            '
            If initResult = ClientErrorType.Ok Then
                WriteEventLog("Camera timeouts (min/default/max) in milliseconds:" + "{0}/{1}/{2}", m_ptz.TimeoutMin, m_ptz.TimeoutDefault, m_ptz.TimeoutMax)

                LoadPresets()

                '
                ' Enabled the parts of the GUI that require a completed
                ' initialisation
                ' 
                EnableConnectedControls(True)
            Else
                WriteEventLog("Error initialising PTZ Unit: {0}", initResult)

                '
                ' Use BeginInvoke to call Reset(), so we can return from the
                ' event handler, before the Ptz object is destroyed.
                ' The call to reset is wrapped in a lambda, which is assigned 
                ' to an Action delegate, which BeginInvoke can call at a later
                ' time.
                '
                'Me.BeginInvoke(New Action(Function() Reset())) 'REVISIT
            End If
        End Sub

        ''' <summary>
        ''' PresetStateChanged event handler:
        ''' Called when the state of the PTZ preset changes - for example, when
        ''' the PTZ unit has left a preset, or arrived at a new one.
        ''' Writes the event to the event text box.
        ''' </summary>
        ''' <param name="eventType">
        ''' The type of preset state change that occurred
        ''' </param>
        ''' <param name="presetToken">
        ''' The token of the preset relating to the event
        ''' </param>
        ''' <param name="presetName">
        ''' The name of the preset relating to the event
        ''' </param>
        Public Sub ptz_PresetStateChanged(eventType As PresetEventType, presetToken As [String], presetName As [String])
            WriteEventLog("Event PresetStateChanged( {0}, {1}, {2} ) received" & vbLf, eventType, presetToken, presetName)
        End Sub

        ''' <summary>
        ''' PtzError event handler:
        ''' Called when there was an error moving the PTZ unit using PTZ
        ''' functions, for example ContinuousMove or GoToPreset
        ''' </summary>
        ''' <param name="clientError">
        ''' The translated error code returned by the PTZ unit
        ''' </param>
        Public Sub ptz_PtzError(clientError As ClientErrorType)
            WriteEventLog("Event PtzError( {0} ) received", clientError)

            If clientError = ClientErrorType.ConnectionLost Then
                '
                ' Use BeginInvoke to call Reset(), so we can return from the
                ' event handler, before the Ptz object is destroyed.
                '
                'Me.BeginInvoke(New Action(Function() Reset())) 'REVISIT
            End If
        End Sub

        ''' <summary>
        ''' Creates the PTZ control and starts the connection to the camera
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>
        Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
            '
            ' Disable disconnected controls
            ' 
            EnableDisconnectedControls(False)

            Dim accessUrl As [String]
            If m_isOnvifProtocol Then
                '
                ' Build the access URL using the known path plus the IP address
                ' 
                accessUrl = String.Format("http://{0}/onvif/device_service", txtIpAddress.Text)
            Else
                accessUrl = String.Format("iv://{0}", txtIpAddress.Text)
            End If

            WriteEventLog("Connecting to {0}...", accessUrl)

            '
            ' Instantiate the Ptz COM object, and link up the event handlers
            '
            Try
                m_ptz = New Ptz()

                initCompletedHandler = New _IPtzEvents_InitCompletedEventHandler(AddressOf ptz_InitCompleted)
                m_ptz.InitCompleted += initCompletedHandler

                presetStateChangedHandler = New _IPtzEvents_PresetStateChangedEventHandler(AddressOf ptz_PresetStateChanged)
                m_ptz.PresetStateChanged += presetStateChangedHandler

                ptzErrorHandler = New _IPtzEvents_PtzErrorEventHandler(AddressOf ptz_PtzError)
                m_ptz.PtzError += ptzErrorHandler
            Catch ex As System.Exception
                '
                ' If there was a problem instantiating the Ptz COM object, 
                ' show and error message and quit - there's no point in
                ' proceeding
                '
                Dim errorMessage As String = String.Format("An exception occurred creating Ptz instance." & vbLf + "Please ensure the ControlCenterVideoSDK DLL is correctly " + "registered." & vbLf + "The exception was: {0}", ex.Message)

                MessageBox.Show(errorMessage, "Initialisation error", MessageBoxButtons.OK, MessageBoxIcon.[Error])

                '
                ' Re-enable the controls
                ' 
                EnableDisconnectedControls(True)
                Return
            End Try

            '
            ' Initialise the Ptz object - this retrieves the timeouts & presets
            ' from the camera
            '
            Try
                If m_isOnvifProtocol Then
                    m_ptz.Init(accessUrl, txtProfile.Text, txtUsername.Text, txtPassword.Text)
                Else
                    m_ptz.InitIvStandard(accessUrl, txtConfigFile.Text, Int32.Parse(numPriority.Value.ToString()), txtDescription.Text)

                    '
                    ' When the asynchronous init function completes, the
                    ' InitComplete event is triggered. When it is, the presets &
                    ' timeouts can be retrieved and stored/displayed.
                    ' See ptz_InitComplete() for an example of this.
                    '
                End If
            Catch ex As System.Exception
                '
                ' Re-enable the controls
                ' 
                EnableDisconnectedControls(True)
                WriteEventLog("Exception initialising camera: {0}", ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Disconnects from the current camera
        ''' </summary>
        ''' <param name="sender">See MSDN Button.Click Event</param>
        ''' <param name="e">See MSDN Button.Click Event</param>
        Private Sub btnDisconnect_Click(sender As Object, e As EventArgs) Handles btnDisconnect.Click
            Reset()
        End Sub

        ''' <summary>
        ''' Handler for when the protocol option is changed
        ''' </summary>
        ''' <param name="sender">The radio button that caused the event</param>
        ''' <param name="e">Event arguments</param>


        ''' <summary>
        ''' Enable the controls for connecting to an ONVIF camera
        ''' </summary>
        Private Sub EnableOnvifControls()
            pnlProtocolOnvif.Visible = True
            pnlProtocolIV.Visible = False
        End Sub

        ''' <summary>
        ''' Enable the controls for connecting to an IndigoVision camera
        ''' </summary>
        Private Sub EnableIVControls()
            pnlProtocolOnvif.Visible = False
            pnlProtocolIV.Visible = True
        End Sub

        ''' <summary>
        ''' Handler for when the browse button is clicked
        ''' </summary>
        ''' <param name="sender">The button that was clicked</param>
        ''' <param name="e">Event arguments</param>
        Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Title = "Open PTZ configuration file"
            openFileDialog.Filter = "PTZ Files|*.ptz"
            openFileDialog.Multiselect = False

            Dim result As DialogResult = openFileDialog.ShowDialog()
            If result = DialogResult.OK Then
                txtConfigFile.Text = openFileDialog.FileName
            End If
        End Sub

#Region "Form Factors"

        Private Sub InitializeComponent()
            Me.txtDescription = New System.Windows.Forms.TextBox()
            Me.lblDescription = New System.Windows.Forms.Label()
            Me.btnBrowse = New System.Windows.Forms.Button()
            Me.numPriority = New System.Windows.Forms.NumericUpDown()
            Me.txtConfigFile = New System.Windows.Forms.TextBox()
            Me.lblPriority = New System.Windows.Forms.Label()
            Me.txtPassword = New System.Windows.Forms.TextBox()
            Me.txtUsername = New System.Windows.Forms.TextBox()
            Me.lblUsername = New System.Windows.Forms.Label()
            Me.lblPassword = New System.Windows.Forms.Label()
            Me.txtProfile = New System.Windows.Forms.TextBox()
            Me.lblProfile = New System.Windows.Forms.Label()
            Me.lblConfigFile = New System.Windows.Forms.Label()
            Me.rdoProtocolIV = New System.Windows.Forms.RadioButton()
            Me.grpConnection = New System.Windows.Forms.GroupBox()
            Me.rdoProtocolOnvif = New System.Windows.Forms.RadioButton()
            Me.lblProtocol = New System.Windows.Forms.Label()
            Me.txtIpAddress = New System.Windows.Forms.TextBox()
            Me.btnDisconnect = New System.Windows.Forms.Button()
            Me.btnConnect = New System.Windows.Forms.Button()
            Me.lblIpAddress = New System.Windows.Forms.Label()
            Me.pnlProtocolOnvif = New System.Windows.Forms.Panel()
            Me.pnlProtocolIV = New System.Windows.Forms.Panel()
            Me.lblEventLog = New System.Windows.Forms.Label()
            Me.btnPanRight = New System.Windows.Forms.Button()
            Me.btnTiltUp = New System.Windows.Forms.Button()
            Me.btnPanLeft = New System.Windows.Forms.Button()
            Me.btnTiltDown = New System.Windows.Forms.Button()
            Me.lstPresets = New System.Windows.Forms.ListBox()
            Me.btnPreset = New System.Windows.Forms.Button()
            Me.txtDebugOutput = New System.Windows.Forms.TextBox()
            Me.btnZoomIn = New System.Windows.Forms.Button()
            Me.btnZoomOut = New System.Windows.Forms.Button()
            Me.grpPtz = New System.Windows.Forms.GroupBox()
            Me.btnDownRight = New System.Windows.Forms.Button()
            Me.btnDownLeft = New System.Windows.Forms.Button()
            Me.btnUpLeft = New System.Windows.Forms.Button()
            Me.btnUpRight = New System.Windows.Forms.Button()
            Me.btnPtzStop = New System.Windows.Forms.Button()
            Me.grpPresets = New System.Windows.Forms.GroupBox()
            CType(Me.numPriority, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.grpConnection.SuspendLayout()
            Me.pnlProtocolOnvif.SuspendLayout()
            Me.pnlProtocolIV.SuspendLayout()
            Me.grpPtz.SuspendLayout()
            Me.grpPresets.SuspendLayout()
            Me.SuspendLayout()
            '
            'txtDescription
            '
            Me.txtDescription.Location = New System.Drawing.Point(400, 50)
            Me.txtDescription.Margin = New System.Windows.Forms.Padding(6)
            Me.txtDescription.Name = "txtDescription"
            Me.txtDescription.Size = New System.Drawing.Size(456, 31)
            Me.txtDescription.TabIndex = 15
            Me.txtDescription.Text = "SDK User"
            '
            'lblDescription
            '
            Me.lblDescription.AutoSize = True
            Me.lblDescription.Location = New System.Drawing.Point(268, 56)
            Me.lblDescription.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblDescription.Name = "lblDescription"
            Me.lblDescription.Size = New System.Drawing.Size(126, 25)
            Me.lblDescription.TabIndex = 10
            Me.lblDescription.Text = "Description:"
            '
            'btnBrowse
            '
            Me.btnBrowse.Location = New System.Drawing.Point(878, 0)
            Me.btnBrowse.Margin = New System.Windows.Forms.Padding(6)
            Me.btnBrowse.Name = "btnBrowse"
            Me.btnBrowse.Size = New System.Drawing.Size(150, 44)
            Me.btnBrowse.TabIndex = 9
            Me.btnBrowse.Text = "Browse..."
            Me.btnBrowse.UseVisualStyleBackColor = True
            '
            'numPriority
            '
            Me.numPriority.Location = New System.Drawing.Point(144, 52)
            Me.numPriority.Margin = New System.Windows.Forms.Padding(6)
            Me.numPriority.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
            Me.numPriority.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.numPriority.Name = "numPriority"
            Me.numPriority.Size = New System.Drawing.Size(98, 31)
            Me.numPriority.TabIndex = 8
            Me.numPriority.Value = New Decimal(New Integer() {1, 0, 0, 0})
            '
            'txtConfigFile
            '
            Me.txtConfigFile.Location = New System.Drawing.Point(144, 4)
            Me.txtConfigFile.Margin = New System.Windows.Forms.Padding(6)
            Me.txtConfigFile.Name = "txtConfigFile"
            Me.txtConfigFile.Size = New System.Drawing.Size(712, 31)
            Me.txtConfigFile.TabIndex = 7
            '
            'lblPriority
            '
            Me.lblPriority.AutoSize = True
            Me.lblPriority.Location = New System.Drawing.Point(8, 56)
            Me.lblPriority.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblPriority.Name = "lblPriority"
            Me.lblPriority.Size = New System.Drawing.Size(85, 25)
            Me.lblPriority.TabIndex = 6
            Me.lblPriority.Text = "Priority:"
            '
            'txtPassword
            '
            Me.txtPassword.Location = New System.Drawing.Point(484, 4)
            Me.txtPassword.Margin = New System.Windows.Forms.Padding(6)
            Me.txtPassword.Name = "txtPassword"
            Me.txtPassword.Size = New System.Drawing.Size(196, 31)
            Me.txtPassword.TabIndex = 5
            Me.txtPassword.Text = "1234"
            Me.txtPassword.UseSystemPasswordChar = True
            '
            'txtUsername
            '
            Me.txtUsername.Location = New System.Drawing.Point(144, 4)
            Me.txtUsername.Margin = New System.Windows.Forms.Padding(6)
            Me.txtUsername.Name = "txtUsername"
            Me.txtUsername.Size = New System.Drawing.Size(196, 31)
            Me.txtUsername.TabIndex = 3
            Me.txtUsername.Text = "Admin"
            '
            'lblUsername
            '
            Me.lblUsername.AutoSize = True
            Me.lblUsername.Location = New System.Drawing.Point(8, 10)
            Me.lblUsername.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblUsername.Name = "lblUsername"
            Me.lblUsername.Size = New System.Drawing.Size(116, 25)
            Me.lblUsername.TabIndex = 2
            Me.lblUsername.Text = "Username:"
            '
            'lblPassword
            '
            Me.lblPassword.AutoSize = True
            Me.lblPassword.Location = New System.Drawing.Point(360, 10)
            Me.lblPassword.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblPassword.Name = "lblPassword"
            Me.lblPassword.Size = New System.Drawing.Size(112, 25)
            Me.lblPassword.TabIndex = 4
            Me.lblPassword.Text = "Password:"
            '
            'txtProfile
            '
            Me.txtProfile.ImeMode = System.Windows.Forms.ImeMode.NoControl
            Me.txtProfile.Location = New System.Drawing.Point(144, 48)
            Me.txtProfile.Margin = New System.Windows.Forms.Padding(6)
            Me.txtProfile.Name = "txtProfile"
            Me.txtProfile.Size = New System.Drawing.Size(196, 31)
            Me.txtProfile.TabIndex = 7
            Me.txtProfile.Text = "Profile2"
            '
            'lblProfile
            '
            Me.lblProfile.AutoSize = True
            Me.lblProfile.Location = New System.Drawing.Point(8, 54)
            Me.lblProfile.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblProfile.Name = "lblProfile"
            Me.lblProfile.Size = New System.Drawing.Size(79, 25)
            Me.lblProfile.TabIndex = 6
            Me.lblProfile.Text = "Profile:"
            '
            'lblConfigFile
            '
            Me.lblConfigFile.AutoSize = True
            Me.lblConfigFile.Location = New System.Drawing.Point(8, 10)
            Me.lblConfigFile.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblConfigFile.Name = "lblConfigFile"
            Me.lblConfigFile.Size = New System.Drawing.Size(99, 25)
            Me.lblConfigFile.TabIndex = 2
            Me.lblConfigFile.Text = "PTZ File:"
            '
            'rdoProtocolIV
            '
            Me.rdoProtocolIV.AutoSize = True
            Me.rdoProtocolIV.Location = New System.Drawing.Point(274, 33)
            Me.rdoProtocolIV.Margin = New System.Windows.Forms.Padding(6)
            Me.rdoProtocolIV.Name = "rdoProtocolIV"
            Me.rdoProtocolIV.Size = New System.Drawing.Size(160, 29)
            Me.rdoProtocolIV.TabIndex = 12
            Me.rdoProtocolIV.Text = "IndigoVision"
            Me.rdoProtocolIV.UseVisualStyleBackColor = True
            '
            'grpConnection
            '
            Me.grpConnection.Controls.Add(Me.rdoProtocolIV)
            Me.grpConnection.Controls.Add(Me.rdoProtocolOnvif)
            Me.grpConnection.Controls.Add(Me.lblProtocol)
            Me.grpConnection.Controls.Add(Me.txtIpAddress)
            Me.grpConnection.Controls.Add(Me.btnDisconnect)
            Me.grpConnection.Controls.Add(Me.btnConnect)
            Me.grpConnection.Controls.Add(Me.lblIpAddress)
            Me.grpConnection.Controls.Add(Me.pnlProtocolOnvif)
            Me.grpConnection.Controls.Add(Me.pnlProtocolIV)
            Me.grpConnection.Location = New System.Drawing.Point(15, 15)
            Me.grpConnection.Margin = New System.Windows.Forms.Padding(6)
            Me.grpConnection.Name = "grpConnection"
            Me.grpConnection.Padding = New System.Windows.Forms.Padding(6)
            Me.grpConnection.Size = New System.Drawing.Size(1212, 235)
            Me.grpConnection.TabIndex = 6
            Me.grpConnection.TabStop = False
            Me.grpConnection.Text = "Connection"
            '
            'rdoProtocolOnvif
            '
            Me.rdoProtocolOnvif.AutoSize = True
            Me.rdoProtocolOnvif.Checked = True
            Me.rdoProtocolOnvif.Location = New System.Drawing.Point(146, 33)
            Me.rdoProtocolOnvif.Margin = New System.Windows.Forms.Padding(6)
            Me.rdoProtocolOnvif.Name = "rdoProtocolOnvif"
            Me.rdoProtocolOnvif.Size = New System.Drawing.Size(106, 29)
            Me.rdoProtocolOnvif.TabIndex = 11
            Me.rdoProtocolOnvif.TabStop = True
            Me.rdoProtocolOnvif.Text = "ONVIF"
            Me.rdoProtocolOnvif.UseVisualStyleBackColor = True
            '
            'lblProtocol
            '
            Me.lblProtocol.AutoSize = True
            Me.lblProtocol.Location = New System.Drawing.Point(10, 37)
            Me.lblProtocol.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblProtocol.Name = "lblProtocol"
            Me.lblProtocol.Size = New System.Drawing.Size(97, 25)
            Me.lblProtocol.TabIndex = 10
            Me.lblProtocol.Text = "Protocol:"
            '
            'txtIpAddress
            '
            Me.txtIpAddress.Location = New System.Drawing.Point(146, 75)
            Me.txtIpAddress.Margin = New System.Windows.Forms.Padding(6)
            Me.txtIpAddress.Name = "txtIpAddress"
            Me.txtIpAddress.Size = New System.Drawing.Size(196, 31)
            Me.txtIpAddress.TabIndex = 1
            Me.txtIpAddress.Text = "172.20.111.3"
            '
            'btnDisconnect
            '
            Me.btnDisconnect.Location = New System.Drawing.Point(1046, 162)
            Me.btnDisconnect.Margin = New System.Windows.Forms.Padding(6)
            Me.btnDisconnect.Name = "btnDisconnect"
            Me.btnDisconnect.Size = New System.Drawing.Size(150, 44)
            Me.btnDisconnect.TabIndex = 9
            Me.btnDisconnect.Text = "Disconnect"
            Me.btnDisconnect.UseVisualStyleBackColor = True
            '
            'btnConnect
            '
            Me.btnConnect.Location = New System.Drawing.Point(880, 162)
            Me.btnConnect.Margin = New System.Windows.Forms.Padding(6)
            Me.btnConnect.Name = "btnConnect"
            Me.btnConnect.Size = New System.Drawing.Size(150, 44)
            Me.btnConnect.TabIndex = 8
            Me.btnConnect.Text = "Connect"
            Me.btnConnect.UseVisualStyleBackColor = True
            '
            'lblIpAddress
            '
            Me.lblIpAddress.AutoSize = True
            Me.lblIpAddress.Location = New System.Drawing.Point(10, 81)
            Me.lblIpAddress.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblIpAddress.Name = "lblIpAddress"
            Me.lblIpAddress.Size = New System.Drawing.Size(122, 25)
            Me.lblIpAddress.TabIndex = 0
            Me.lblIpAddress.Text = "IP Address:"
            '
            'pnlProtocolOnvif
            '
            Me.pnlProtocolOnvif.Controls.Add(Me.txtPassword)
            Me.pnlProtocolOnvif.Controls.Add(Me.txtUsername)
            Me.pnlProtocolOnvif.Controls.Add(Me.lblUsername)
            Me.pnlProtocolOnvif.Controls.Add(Me.lblPassword)
            Me.pnlProtocolOnvif.Controls.Add(Me.txtProfile)
            Me.pnlProtocolOnvif.Controls.Add(Me.lblProfile)
            Me.pnlProtocolOnvif.Location = New System.Drawing.Point(2, 115)
            Me.pnlProtocolOnvif.Margin = New System.Windows.Forms.Padding(6)
            Me.pnlProtocolOnvif.Name = "pnlProtocolOnvif"
            Me.pnlProtocolOnvif.Size = New System.Drawing.Size(864, 110)
            Me.pnlProtocolOnvif.TabIndex = 13
            '
            'pnlProtocolIV
            '
            Me.pnlProtocolIV.Controls.Add(Me.txtDescription)
            Me.pnlProtocolIV.Controls.Add(Me.lblDescription)
            Me.pnlProtocolIV.Controls.Add(Me.btnBrowse)
            Me.pnlProtocolIV.Controls.Add(Me.numPriority)
            Me.pnlProtocolIV.Controls.Add(Me.txtConfigFile)
            Me.pnlProtocolIV.Controls.Add(Me.lblConfigFile)
            Me.pnlProtocolIV.Controls.Add(Me.lblPriority)
            Me.pnlProtocolIV.Location = New System.Drawing.Point(2, 115)
            Me.pnlProtocolIV.Margin = New System.Windows.Forms.Padding(6)
            Me.pnlProtocolIV.Name = "pnlProtocolIV"
            Me.pnlProtocolIV.Size = New System.Drawing.Size(1028, 110)
            Me.pnlProtocolIV.TabIndex = 14
            Me.pnlProtocolIV.Visible = False
            '
            'lblEventLog
            '
            Me.lblEventLog.AutoSize = True
            Me.lblEventLog.Location = New System.Drawing.Point(15, 696)
            Me.lblEventLog.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.lblEventLog.Name = "lblEventLog"
            Me.lblEventLog.Size = New System.Drawing.Size(145, 25)
            Me.lblEventLog.TabIndex = 10
            Me.lblEventLog.Text = "Debug Output"
            '
            'btnPanRight
            '
            Me.btnPanRight.Location = New System.Drawing.Point(248, 169)
            Me.btnPanRight.Margin = New System.Windows.Forms.Padding(6)
            Me.btnPanRight.Name = "btnPanRight"
            Me.btnPanRight.Size = New System.Drawing.Size(88, 85)
            Me.btnPanRight.TabIndex = 3
            Me.btnPanRight.Text = "Right"
            Me.btnPanRight.UseVisualStyleBackColor = True
            '
            'btnTiltUp
            '
            Me.btnTiltUp.Location = New System.Drawing.Point(158, 83)
            Me.btnTiltUp.Margin = New System.Windows.Forms.Padding(6)
            Me.btnTiltUp.Name = "btnTiltUp"
            Me.btnTiltUp.Size = New System.Drawing.Size(88, 85)
            Me.btnTiltUp.TabIndex = 1
            Me.btnTiltUp.Text = "Up"
            Me.btnTiltUp.UseVisualStyleBackColor = True
            '
            'btnPanLeft
            '
            Me.btnPanLeft.Location = New System.Drawing.Point(68, 169)
            Me.btnPanLeft.Margin = New System.Windows.Forms.Padding(6)
            Me.btnPanLeft.Name = "btnPanLeft"
            Me.btnPanLeft.Size = New System.Drawing.Size(88, 85)
            Me.btnPanLeft.TabIndex = 7
            Me.btnPanLeft.Text = "Left"
            Me.btnPanLeft.UseVisualStyleBackColor = True
            '
            'btnTiltDown
            '
            Me.btnTiltDown.Location = New System.Drawing.Point(158, 254)
            Me.btnTiltDown.Margin = New System.Windows.Forms.Padding(6)
            Me.btnTiltDown.Name = "btnTiltDown"
            Me.btnTiltDown.Size = New System.Drawing.Size(88, 85)
            Me.btnTiltDown.TabIndex = 5
            Me.btnTiltDown.Text = "Down"
            Me.btnTiltDown.UseVisualStyleBackColor = True
            '
            'lstPresets
            '
            Me.lstPresets.FormattingEnabled = True
            Me.lstPresets.ItemHeight = 25
            Me.lstPresets.Location = New System.Drawing.Point(619, 315)
            Me.lstPresets.Margin = New System.Windows.Forms.Padding(6)
            Me.lstPresets.Name = "lstPresets"
            Me.lstPresets.Size = New System.Drawing.Size(582, 279)
            Me.lstPresets.TabIndex = 8
            '
            'btnPreset
            '
            Me.btnPreset.Location = New System.Drawing.Point(454, 329)
            Me.btnPreset.Margin = New System.Windows.Forms.Padding(6)
            Me.btnPreset.Name = "btnPreset"
            Me.btnPreset.Size = New System.Drawing.Size(150, 44)
            Me.btnPreset.TabIndex = 0
            Me.btnPreset.Text = "Go to Preset"
            Me.btnPreset.UseVisualStyleBackColor = True
            '
            'txtDebugOutput
            '
            Me.txtDebugOutput.Location = New System.Drawing.Point(15, 728)
            Me.txtDebugOutput.Margin = New System.Windows.Forms.Padding(6)
            Me.txtDebugOutput.Multiline = True
            Me.txtDebugOutput.Name = "txtDebugOutput"
            Me.txtDebugOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtDebugOutput.Size = New System.Drawing.Size(1208, 423)
            Me.txtDebugOutput.TabIndex = 11
            '
            'btnZoomIn
            '
            Me.btnZoomIn.Location = New System.Drawing.Point(400, 119)
            Me.btnZoomIn.Margin = New System.Windows.Forms.Padding(6)
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.Size = New System.Drawing.Size(88, 85)
            Me.btnZoomIn.TabIndex = 9
            Me.btnZoomIn.Text = "+"
            Me.btnZoomIn.UseVisualStyleBackColor = True
            '
            'btnZoomOut
            '
            Me.btnZoomOut.Location = New System.Drawing.Point(400, 206)
            Me.btnZoomOut.Margin = New System.Windows.Forms.Padding(6)
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.Size = New System.Drawing.Size(88, 85)
            Me.btnZoomOut.TabIndex = 10
            Me.btnZoomOut.Text = "-"
            Me.btnZoomOut.UseVisualStyleBackColor = True
            '
            'grpPtz
            '
            Me.grpPtz.Controls.Add(Me.btnDownRight)
            Me.grpPtz.Controls.Add(Me.btnDownLeft)
            Me.grpPtz.Controls.Add(Me.btnUpLeft)
            Me.grpPtz.Controls.Add(Me.btnUpRight)
            Me.grpPtz.Controls.Add(Me.btnPanRight)
            Me.grpPtz.Controls.Add(Me.btnTiltUp)
            Me.grpPtz.Controls.Add(Me.btnZoomIn)
            Me.grpPtz.Controls.Add(Me.btnPanLeft)
            Me.grpPtz.Controls.Add(Me.btnTiltDown)
            Me.grpPtz.Controls.Add(Me.btnZoomOut)
            Me.grpPtz.Controls.Add(Me.btnPtzStop)
            Me.grpPtz.Location = New System.Drawing.Point(15, 280)
            Me.grpPtz.Margin = New System.Windows.Forms.Padding(6)
            Me.grpPtz.Name = "grpPtz"
            Me.grpPtz.Padding = New System.Windows.Forms.Padding(6)
            Me.grpPtz.Size = New System.Drawing.Size(574, 394)
            Me.grpPtz.TabIndex = 7
            Me.grpPtz.TabStop = False
            Me.grpPtz.Text = "Pan/Tilt/Zoom Control"
            '
            'btnDownRight
            '
            Me.btnDownRight.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
            Me.btnDownRight.Location = New System.Drawing.Point(248, 254)
            Me.btnDownRight.Margin = New System.Windows.Forms.Padding(6)
            Me.btnDownRight.Name = "btnDownRight"
            Me.btnDownRight.Size = New System.Drawing.Size(88, 85)
            Me.btnDownRight.TabIndex = 4
            Me.btnDownRight.Text = "æ"
            Me.btnDownRight.UseVisualStyleBackColor = True
            '
            'btnDownLeft
            '
            Me.btnDownLeft.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
            Me.btnDownLeft.Location = New System.Drawing.Point(68, 254)
            Me.btnDownLeft.Margin = New System.Windows.Forms.Padding(6)
            Me.btnDownLeft.Name = "btnDownLeft"
            Me.btnDownLeft.Size = New System.Drawing.Size(88, 85)
            Me.btnDownLeft.TabIndex = 6
            Me.btnDownLeft.Text = "å"
            Me.btnDownLeft.UseVisualStyleBackColor = True
            '
            'btnUpLeft
            '
            Me.btnUpLeft.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
            Me.btnUpLeft.Location = New System.Drawing.Point(68, 83)
            Me.btnUpLeft.Margin = New System.Windows.Forms.Padding(6)
            Me.btnUpLeft.Name = "btnUpLeft"
            Me.btnUpLeft.Size = New System.Drawing.Size(88, 85)
            Me.btnUpLeft.TabIndex = 0
            Me.btnUpLeft.Text = "ã"
            Me.btnUpLeft.UseVisualStyleBackColor = True
            '
            'btnUpRight
            '
            Me.btnUpRight.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
            Me.btnUpRight.Location = New System.Drawing.Point(248, 83)
            Me.btnUpRight.Margin = New System.Windows.Forms.Padding(6)
            Me.btnUpRight.Name = "btnUpRight"
            Me.btnUpRight.Size = New System.Drawing.Size(88, 85)
            Me.btnUpRight.TabIndex = 2
            Me.btnUpRight.Text = "ä"
            Me.btnUpRight.UseVisualStyleBackColor = True
            '
            'btnPtzStop
            '
            Me.btnPtzStop.Location = New System.Drawing.Point(158, 169)
            Me.btnPtzStop.Margin = New System.Windows.Forms.Padding(6)
            Me.btnPtzStop.Name = "btnPtzStop"
            Me.btnPtzStop.Size = New System.Drawing.Size(88, 85)
            Me.btnPtzStop.TabIndex = 8
            Me.btnPtzStop.Text = "Stop"
            Me.btnPtzStop.UseVisualStyleBackColor = True
            '
            'grpPresets
            '
            Me.grpPresets.Controls.Add(Me.btnPreset)
            Me.grpPresets.Location = New System.Drawing.Point(601, 280)
            Me.grpPresets.Margin = New System.Windows.Forms.Padding(6)
            Me.grpPresets.Name = "grpPresets"
            Me.grpPresets.Padding = New System.Windows.Forms.Padding(6)
            Me.grpPresets.Size = New System.Drawing.Size(622, 394)
            Me.grpPresets.TabIndex = 9
            Me.grpPresets.TabStop = False
            Me.grpPresets.Text = "Presets"
            '
            'MainForm
            '
            Me.ClientSize = New System.Drawing.Size(1245, 1165)
            Me.Controls.Add(Me.grpConnection)
            Me.Controls.Add(Me.lblEventLog)
            Me.Controls.Add(Me.lstPresets)
            Me.Controls.Add(Me.txtDebugOutput)
            Me.Controls.Add(Me.grpPtz)
            Me.Controls.Add(Me.grpPresets)
            Me.Name = "MainForm"
            CType(Me.numPriority, System.ComponentModel.ISupportInitialize).EndInit()
            Me.grpConnection.ResumeLayout(False)
            Me.grpConnection.PerformLayout()
            Me.pnlProtocolOnvif.ResumeLayout(False)
            Me.pnlProtocolOnvif.PerformLayout()
            Me.pnlProtocolIV.ResumeLayout(False)
            Me.pnlProtocolIV.PerformLayout()
            Me.grpPtz.ResumeLayout(False)
            Me.grpPresets.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Private WithEvents txtDescription As System.Windows.Forms.TextBox
        Private WithEvents lblDescription As System.Windows.Forms.Label
        Private WithEvents btnBrowse As System.Windows.Forms.Button
        Private WithEvents numPriority As System.Windows.Forms.NumericUpDown
        Private WithEvents txtConfigFile As System.Windows.Forms.TextBox
        Private WithEvents lblPriority As System.Windows.Forms.Label
        Private WithEvents txtPassword As System.Windows.Forms.TextBox
        Private WithEvents txtUsername As System.Windows.Forms.TextBox
        Private WithEvents lblUsername As System.Windows.Forms.Label
        Private WithEvents lblPassword As System.Windows.Forms.Label
        Private WithEvents txtProfile As System.Windows.Forms.TextBox
        Private WithEvents lblProfile As System.Windows.Forms.Label
        Private WithEvents lblConfigFile As System.Windows.Forms.Label
        Private WithEvents rdoProtocolIV As System.Windows.Forms.RadioButton
        Private WithEvents grpConnection As System.Windows.Forms.GroupBox
        Private WithEvents rdoProtocolOnvif As System.Windows.Forms.RadioButton
        Private WithEvents lblProtocol As System.Windows.Forms.Label
        Private WithEvents txtIpAddress As System.Windows.Forms.TextBox
        Private WithEvents btnDisconnect As System.Windows.Forms.Button
        Private WithEvents btnConnect As System.Windows.Forms.Button
        Private WithEvents lblIpAddress As System.Windows.Forms.Label
        Private WithEvents pnlProtocolOnvif As System.Windows.Forms.Panel
        Private WithEvents pnlProtocolIV As System.Windows.Forms.Panel
        Private WithEvents lblEventLog As System.Windows.Forms.Label
        Private WithEvents btnPanRight As System.Windows.Forms.Button
        Private WithEvents btnTiltUp As System.Windows.Forms.Button
        Private WithEvents btnPanLeft As System.Windows.Forms.Button
        Private WithEvents btnTiltDown As System.Windows.Forms.Button
        Private WithEvents lstPresets As System.Windows.Forms.ListBox
        Private WithEvents btnPreset As System.Windows.Forms.Button
        Private WithEvents txtDebugOutput As System.Windows.Forms.TextBox
        Private WithEvents btnZoomIn As System.Windows.Forms.Button
        Private WithEvents btnZoomOut As System.Windows.Forms.Button
        Private WithEvents grpPtz As System.Windows.Forms.GroupBox
        Private WithEvents btnDownRight As System.Windows.Forms.Button
        Private WithEvents btnDownLeft As System.Windows.Forms.Button
        Private WithEvents btnUpLeft As System.Windows.Forms.Button
        Private WithEvents btnUpRight As System.Windows.Forms.Button
        Private WithEvents btnPtzStop As System.Windows.Forms.Button
        Private WithEvents grpPresets As System.Windows.Forms.GroupBox

#End Region

    End Class
End Namespace
