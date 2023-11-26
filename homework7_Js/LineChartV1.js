class LineChartV1 {
  constructor(canvas) {
    this.canvas = canvas;
    this.ctx = canvas.getContext('2d');
    this.xPadding = 40;
    this.yPadding = 40;
  }

  drawChart(data) {
    const colors = ['#FF0000', '#00FF00', '#0000FF', '#FF00FF', '#FFFF00']; // You can add more colors as needed

    this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);

    const numLines = data.length;
    const numPoints = data[0].length;

    const xMax = this.canvas.width - this.xPadding;
    const yMax = this.canvas.height - this.yPadding;
    const yMin = this.yPadding;

    const xScale = (xMax - this.xPadding) / (numPoints - 1);
    const yScale = (yMax - this.yPadding * 2) / (this.getMaxValue(data) - this.getMinValue(data));

    // Draw x-axis
    this.ctx.beginPath();
    this.ctx.moveTo(this.xPadding, yMax);
    this.ctx.lineTo(xMax, yMax);
    this.ctx.strokeStyle = 'black';
    this.ctx.stroke();

    // Draw y-axis
    this.ctx.beginPath();
    this.ctx.moveTo(this.xPadding, yMax);
    this.ctx.lineTo(this.xPadding, yMin);
    this.ctx.strokeStyle = 'black';
    this.ctx.stroke();

    // Draw x-axis labels and ticks
    for (let i = 0; i < numPoints; i++) {
      const x = i * xScale + this.xPadding;
      this.ctx.fillText(i, x, yMax + 20); // Label
      this.ctx.beginPath();
      this.ctx.moveTo(x, yMax);
      this.ctx.lineTo(x, yMax + 5); // Tick mark
      this.ctx.strokeStyle = 'black';
      this.ctx.stroke();
    }

    // Draw y-axis labels and ticks
    const minValue = this.getMinValue(data);
    const numTicks = 10;
    for (let i = 0; i <= numTicks; i++) {
      const value = minValue + (i / numTicks) * (this.getMaxValue(data) - minValue);
      const y = yMax - (value - minValue) * yScale;
      this.ctx.fillText(value.toFixed(2), this.xPadding - 30, y + 5); // Label
      this.ctx.beginPath();
      this.ctx.moveTo(this.xPadding, y);
      this.ctx.lineTo(this.xPadding - 5, y); // Tick mark
      this.ctx.strokeStyle = 'black';
      this.ctx.stroke();
    }

    // Draw lines
    for (let i = 0; i < numLines; i++) {
      this.ctx.beginPath();
      this.ctx.moveTo(this.xPadding, yMax - (data[i][0] - minValue) * yScale);

      for (let j = 1; j < numPoints; j++) {
        const x = j * xScale + this.xPadding;
        const y = yMax - (data[i][j] - minValue) * yScale;

        this.ctx.lineTo(x, y);
      }

      this.ctx.strokeStyle = colors[i % colors.length];
      this.ctx.stroke();
    }
  }

  getMaxValue(data) {
    return Math.max(...data.flat());
  }

  getMinValue(data) {
    return Math.min(...data.flat());
  }
}