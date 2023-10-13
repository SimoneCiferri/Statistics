Public Class Form1

    Private b As Bitmap
    Private g As Graphics

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label1.Text = "Homework 1"
        Label1.Font = New Font("Arial", 12, FontStyle.Bold)

        b = New Bitmap(Me.PictureBox1.Width, Me.PictureBox1.Height)
        g = Graphics.FromImage(b)

        g.FillRectangle(Brushes.Black, 10, 5, 2, 2)

        g.FillRectangle(Brushes.Green, 10, 30, 100, 2)

        g.FillRectangle(Brushes.Orange, New Rectangle(10, 60, 25, 150))

        g.FillEllipse(Brushes.Purple, 10, 220, 30, 30)

        Me.PictureBox1.Image = b

    End Sub
End Class
