<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PTZForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PTZForm))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupPan = New System.Windows.Forms.GroupBox()
        Me.ButtonRight = New System.Windows.Forms.Button()
        Me.ButtonDown = New System.Windows.Forms.Button()
        Me.ButtonLeft = New System.Windows.Forms.Button()
        Me.ButtonUp = New System.Windows.Forms.Button()
        Me.GroupZoom = New System.Windows.Forms.GroupBox()
        Me.ButtonMinus = New System.Windows.Forms.Button()
        Me.ButtonPlus = New System.Windows.Forms.Button()
        Me.GroupFocus = New System.Windows.Forms.GroupBox()
        Me.ButtonFar = New System.Windows.Forms.Button()
        Me.ButtonNear = New System.Windows.Forms.Button()
        Me.GroupIris = New System.Windows.Forms.GroupBox()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.ButtonOpen = New System.Windows.Forms.Button()
        Me.ButtonCloseForm = New System.Windows.Forms.Button()
        Me.ComboCamera = New System.Windows.Forms.ComboBox()
        Me.ComboPreset = New System.Windows.Forms.ComboBox()
        Me.TextBoxPresetDesc = New System.Windows.Forms.TextBox()
        Me.LabelCamera = New System.Windows.Forms.Label()
        Me.LabelPreset = New System.Windows.Forms.Label()
        Me.LabelPresetDesc = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.videoControl = New AxControlCenterVideoSDK.AxControlCenterVideoControl()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupPan.SuspendLayout()
        Me.GroupZoom.SuspendLayout()
        Me.GroupFocus.SuspendLayout()
        Me.GroupIris.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.videoControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel3, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 914)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1178, 41)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(249, 36)
        Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Bump
        Me.ToolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(245, 36)
        Me.ToolStripStatusLabel3.Text = "ToolStripStatusLabel3"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(249, 36)
        Me.ToolStripStatusLabel2.Text = "ToolStripStatusLabel2"
        '
        'GroupPan
        '
        Me.GroupPan.Controls.Add(Me.ButtonRight)
        Me.GroupPan.Controls.Add(Me.ButtonDown)
        Me.GroupPan.Controls.Add(Me.ButtonLeft)
        Me.GroupPan.Controls.Add(Me.ButtonUp)
        Me.GroupPan.Location = New System.Drawing.Point(854, 242)
        Me.GroupPan.Name = "GroupPan"
        Me.GroupPan.Size = New System.Drawing.Size(285, 266)
        Me.GroupPan.TabIndex = 1
        Me.GroupPan.TabStop = False
        Me.GroupPan.Text = "Pan"
        '
        'ButtonRight
        '
        Me.ButtonRight.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonRight.Location = New System.Drawing.Point(185, 99)
        Me.ButtonRight.Name = "ButtonRight"
        Me.ButtonRight.Size = New System.Drawing.Size(70, 70)
        Me.ButtonRight.TabIndex = 8
        Me.ButtonRight.Text = "4"
        Me.ButtonRight.UseVisualStyleBackColor = True
        '
        'ButtonDown
        '
        Me.ButtonDown.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonDown.Location = New System.Drawing.Point(115, 168)
        Me.ButtonDown.Name = "ButtonDown"
        Me.ButtonDown.Size = New System.Drawing.Size(70, 70)
        Me.ButtonDown.TabIndex = 7
        Me.ButtonDown.Text = "6"
        Me.ButtonDown.UseVisualStyleBackColor = True
        '
        'ButtonLeft
        '
        Me.ButtonLeft.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonLeft.Location = New System.Drawing.Point(46, 99)
        Me.ButtonLeft.Name = "ButtonLeft"
        Me.ButtonLeft.Size = New System.Drawing.Size(70, 70)
        Me.ButtonLeft.TabIndex = 6
        Me.ButtonLeft.Text = "3"
        Me.ButtonLeft.UseVisualStyleBackColor = True
        '
        'ButtonUp
        '
        Me.ButtonUp.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.ButtonUp.Location = New System.Drawing.Point(115, 30)
        Me.ButtonUp.Name = "ButtonUp"
        Me.ButtonUp.Size = New System.Drawing.Size(70, 70)
        Me.ButtonUp.TabIndex = 5
        Me.ButtonUp.Text = "5"
        Me.ButtonUp.UseVisualStyleBackColor = True
        '
        'GroupZoom
        '
        Me.GroupZoom.Controls.Add(Me.ButtonMinus)
        Me.GroupZoom.Controls.Add(Me.ButtonPlus)
        Me.GroupZoom.Location = New System.Drawing.Point(854, 514)
        Me.GroupZoom.Name = "GroupZoom"
        Me.GroupZoom.Size = New System.Drawing.Size(285, 100)
        Me.GroupZoom.TabIndex = 2
        Me.GroupZoom.TabStop = False
        Me.GroupZoom.Text = "Zoom"
        '
        'ButtonMinus
        '
        Me.ButtonMinus.Location = New System.Drawing.Point(160, 27)
        Me.ButtonMinus.Name = "ButtonMinus"
        Me.ButtonMinus.Size = New System.Drawing.Size(95, 67)
        Me.ButtonMinus.TabIndex = 5
        Me.ButtonMinus.Text = "-"
        Me.ButtonMinus.UseVisualStyleBackColor = True
        '
        'ButtonPlus
        '
        Me.ButtonPlus.Location = New System.Drawing.Point(51, 27)
        Me.ButtonPlus.Name = "ButtonPlus"
        Me.ButtonPlus.Size = New System.Drawing.Size(95, 67)
        Me.ButtonPlus.TabIndex = 4
        Me.ButtonPlus.Text = "+"
        Me.ButtonPlus.UseVisualStyleBackColor = True
        '
        'GroupFocus
        '
        Me.GroupFocus.Controls.Add(Me.ButtonFar)
        Me.GroupFocus.Controls.Add(Me.ButtonNear)
        Me.GroupFocus.Location = New System.Drawing.Point(854, 633)
        Me.GroupFocus.Name = "GroupFocus"
        Me.GroupFocus.Size = New System.Drawing.Size(285, 100)
        Me.GroupFocus.TabIndex = 2
        Me.GroupFocus.TabStop = False
        Me.GroupFocus.Text = "Focus"
        '
        'ButtonFar
        '
        Me.ButtonFar.Location = New System.Drawing.Point(160, 27)
        Me.ButtonFar.Name = "ButtonFar"
        Me.ButtonFar.Size = New System.Drawing.Size(95, 67)
        Me.ButtonFar.TabIndex = 7
        Me.ButtonFar.Text = "Far"
        Me.ButtonFar.UseVisualStyleBackColor = True
        '
        'ButtonNear
        '
        Me.ButtonNear.Location = New System.Drawing.Point(51, 27)
        Me.ButtonNear.Name = "ButtonNear"
        Me.ButtonNear.Size = New System.Drawing.Size(95, 67)
        Me.ButtonNear.TabIndex = 6
        Me.ButtonNear.Text = "Near"
        Me.ButtonNear.UseVisualStyleBackColor = True
        '
        'GroupIris
        '
        Me.GroupIris.Controls.Add(Me.ButtonClose)
        Me.GroupIris.Controls.Add(Me.ButtonOpen)
        Me.GroupIris.Location = New System.Drawing.Point(854, 753)
        Me.GroupIris.Name = "GroupIris"
        Me.GroupIris.Size = New System.Drawing.Size(285, 100)
        Me.GroupIris.TabIndex = 2
        Me.GroupIris.TabStop = False
        Me.GroupIris.Text = "Iris"
        '
        'ButtonClose
        '
        Me.ButtonClose.Location = New System.Drawing.Point(160, 21)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(95, 67)
        Me.ButtonClose.TabIndex = 9
        Me.ButtonClose.Text = "Close"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'ButtonOpen
        '
        Me.ButtonOpen.Location = New System.Drawing.Point(51, 21)
        Me.ButtonOpen.Name = "ButtonOpen"
        Me.ButtonOpen.Size = New System.Drawing.Size(95, 67)
        Me.ButtonOpen.TabIndex = 8
        Me.ButtonOpen.Text = "Open"
        Me.ButtonOpen.UseVisualStyleBackColor = True
        '
        'ButtonCloseForm
        '
        Me.ButtonCloseForm.Location = New System.Drawing.Point(854, 873)
        Me.ButtonCloseForm.Name = "ButtonCloseForm"
        Me.ButtonCloseForm.Size = New System.Drawing.Size(285, 52)
        Me.ButtonCloseForm.TabIndex = 3
        Me.ButtonCloseForm.Text = "Close"
        Me.ButtonCloseForm.UseVisualStyleBackColor = True
        '
        'ComboCamera
        '
        Me.ComboCamera.FormattingEnabled = True
        Me.ComboCamera.Location = New System.Drawing.Point(223, 37)
        Me.ComboCamera.Name = "ComboCamera"
        Me.ComboCamera.Size = New System.Drawing.Size(439, 33)
        Me.ComboCamera.TabIndex = 4
        '
        'ComboPreset
        '
        Me.ComboPreset.FormattingEnabled = True
        Me.ComboPreset.Location = New System.Drawing.Point(842, 40)
        Me.ComboPreset.Name = "ComboPreset"
        Me.ComboPreset.Size = New System.Drawing.Size(292, 33)
        Me.ComboPreset.TabIndex = 5
        '
        'TextBoxPresetDesc
        '
        Me.TextBoxPresetDesc.Location = New System.Drawing.Point(223, 106)
        Me.TextBoxPresetDesc.Multiline = True
        Me.TextBoxPresetDesc.Name = "TextBoxPresetDesc"
        Me.TextBoxPresetDesc.Size = New System.Drawing.Size(916, 112)
        Me.TextBoxPresetDesc.TabIndex = 6
        '
        'LabelCamera
        '
        Me.LabelCamera.AutoSize = True
        Me.LabelCamera.Location = New System.Drawing.Point(23, 37)
        Me.LabelCamera.Name = "LabelCamera"
        Me.LabelCamera.Size = New System.Drawing.Size(93, 25)
        Me.LabelCamera.TabIndex = 7
        Me.LabelCamera.Text = "Camera:"
        '
        'LabelPreset
        '
        Me.LabelPreset.AutoSize = True
        Me.LabelPreset.Location = New System.Drawing.Point(717, 37)
        Me.LabelPreset.Name = "LabelPreset"
        Me.LabelPreset.Size = New System.Drawing.Size(106, 25)
        Me.LabelPreset.TabIndex = 8
        Me.LabelPreset.Text = "Preset ID:"
        '
        'LabelPresetDesc
        '
        Me.LabelPresetDesc.AutoSize = True
        Me.LabelPresetDesc.Location = New System.Drawing.Point(23, 106)
        Me.LabelPresetDesc.Name = "LabelPresetDesc"
        Me.LabelPresetDesc.Size = New System.Drawing.Size(194, 25)
        Me.LabelPresetDesc.TabIndex = 9
        Me.LabelPresetDesc.Text = "Preset Description:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.videoControl)
        Me.Panel1.Location = New System.Drawing.Point(17, 242)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(817, 683)
        Me.Panel1.TabIndex = 12
        '
        'videoControl
        '
        Me.videoControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.videoControl.Enabled = True
        Me.videoControl.Location = New System.Drawing.Point(0, 0)
        Me.videoControl.Margin = New System.Windows.Forms.Padding(6)
        Me.videoControl.Name = "videoControl"
        Me.videoControl.OcxState = CType(resources.GetObject("videoControl.OcxState"), System.Windows.Forms.AxHost.State)
        Me.videoControl.Size = New System.Drawing.Size(817, 683)
        Me.videoControl.TabIndex = 12
        '
        'PTZForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1178, 955)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LabelPresetDesc)
        Me.Controls.Add(Me.LabelPreset)
        Me.Controls.Add(Me.LabelCamera)
        Me.Controls.Add(Me.TextBoxPresetDesc)
        Me.Controls.Add(Me.ComboPreset)
        Me.Controls.Add(Me.ComboCamera)
        Me.Controls.Add(Me.ButtonCloseForm)
        Me.Controls.Add(Me.GroupZoom)
        Me.Controls.Add(Me.GroupFocus)
        Me.Controls.Add(Me.GroupIris)
        Me.Controls.Add(Me.GroupPan)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "PTZForm"
        Me.Text = "PTZForm"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupPan.ResumeLayout(False)
        Me.GroupZoom.ResumeLayout(False)
        Me.GroupFocus.ResumeLayout(False)
        Me.GroupIris.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.videoControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents GroupPan As System.Windows.Forms.GroupBox
    Friend WithEvents GroupZoom As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonMinus As System.Windows.Forms.Button
    Friend WithEvents ButtonPlus As System.Windows.Forms.Button
    Friend WithEvents GroupFocus As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonFar As System.Windows.Forms.Button
    Friend WithEvents ButtonNear As System.Windows.Forms.Button
    Friend WithEvents GroupIris As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonClose As System.Windows.Forms.Button
    Friend WithEvents ButtonOpen As System.Windows.Forms.Button
    Friend WithEvents ButtonRight As System.Windows.Forms.Button
    Friend WithEvents ButtonDown As System.Windows.Forms.Button
    Friend WithEvents ButtonLeft As System.Windows.Forms.Button
    Friend WithEvents ButtonUp As System.Windows.Forms.Button
    Friend WithEvents ButtonCloseForm As System.Windows.Forms.Button
    Friend WithEvents ComboCamera As System.Windows.Forms.ComboBox
    Friend WithEvents ComboPreset As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxPresetDesc As System.Windows.Forms.TextBox
    Friend WithEvents LabelCamera As System.Windows.Forms.Label
    Friend WithEvents LabelPreset As System.Windows.Forms.Label
    Friend WithEvents LabelPresetDesc As System.Windows.Forms.Label
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents videoControl As AxControlCenterVideoSDK.AxControlCenterVideoControl
End Class
