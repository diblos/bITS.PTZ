<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
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
        Me.txtDebugOutput.Size = New System.Drawing.Size(1208, 335)
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
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1241, 1076)
        Me.Controls.Add(Me.grpConnection)
        Me.Controls.Add(Me.lblEventLog)
        Me.Controls.Add(Me.lstPresets)
        Me.Controls.Add(Me.txtDebugOutput)
        Me.Controls.Add(Me.grpPtz)
        Me.Controls.Add(Me.grpPresets)
        Me.Name = "Form1"
        Me.Text = "Form1"
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

End Class
