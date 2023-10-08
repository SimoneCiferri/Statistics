Public Class Form1
    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Dim g As Graphics = e.Graphics
        Dim p As New Pen(Color.Blue) ' Penna blu per disegnare le figure
        Dim cerchioX As Integer = 50 ' Coordinata x del centro del cerchio
        Dim cerchioY As Integer = 50 ' Coordinata y del centro del cerchio
        Dim raggio As Integer = 30 ' Raggio del cerchio
        Dim puntoX As Integer = 100 ' Coordinata x del punto
        Dim puntoY As Integer = 50 ' Coordinata y del punto
        Dim segmentoX1 As Integer = cerchioX + raggio * 2 + 10 ' Coordinata x iniziale del segmento
        Dim segmentoX2 As Integer = segmentoX1 + 50 ' Coordinata x finale del segmento
        Dim segmentoY As Integer = cerchioY ' Coordinata y del segmento
        Dim rettangoloX As Integer = cerchioX - raggio - 10 ' Coordinata x iniziale del rettangolo
        Dim rettangoloY As Integer = cerchioY + raggio + 10 ' Coordinata y iniziale del rettangolo
        Dim larghezzaRettangolo As Integer = 80 ' Larghezza del rettangolo
        Dim altezzaRettangolo As Integer = 40 ' Altezza del rettangolo

        ' Disegna il cerchio nel PictureBox
        g.DrawEllipse(p, cerchioX - raggio, cerchioY - raggio, raggio * 2, raggio * 2)

        ' Disegna il punto
        g.FillEllipse(Brushes.Red, puntoX, puntoY, 5, 5)

        ' Disegna il segmento
        g.DrawLine(p, segmentoX1, segmentoY, segmentoX2, segmentoY)

        ' Disegna il rettangolo
        g.DrawRectangle(p, rettangoloX, rettangoloY, larghezzaRettangolo, altezzaRettangolo)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Imposta il testo del TextBox
        Label1.Text = "Homework 1"
    End Sub
End Class
