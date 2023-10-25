class LineChart {

    xMin = 0;
    xMax = 0;
    yMin = 0;
    yMax = 0;

    pictureBox;
    _PADDING;
    _graphBitmap;

    constructor(pictureBox) {

        this._PADDING = 50;
        this._graphBitmap = pictureBox;

        this._graphBitmap.width = 1000;
        this._graphBitmap.height = 1000;

    }


drawChart(data) {
    let g = this._graphBitmap.getContext("2d");

    let width = 1000 - 2 * this.PADDING;
    let height = 1000 - 2 * this.PADDING;

    this.xMin = 0;
    
    this.xMax = Math.max(...data.map(list => list.length));
    this.yMin = -Math.max(...data.map(list => list.length));
    this.yMax = Math.max(...data.map(list => list.length));

    g.clearRect(0, 0, 1000, 1000);
    g.beginPath();

    g.moveTo(this.PADDING, this.PADDING + height / 2);
    g.lineTo(this.PADDING + width, this.PADDING + height / 2);
    g.moveTo(this.PADDING, this.PADDING + height);
    g.lineTo(this.PADDING, this.PADDING);
    g.strokeStyle = "black";
    g.lineWidth = 2;
    g.stroke();

    for (let i = this.xMin; i <= this.xMax; i++) {
        let x = this.PADDING + (i - this.xMin) * width / (this.xMax - this.xMin);
        g.moveTo(x, this.PADDING + height / 2 - 5);
        g.lineTo(x, this.PADDING + height / 2 + 5);
        g.stroke();
        g.fillText(i.toString(), x - 10, this.PADDING + height / 2 + 10);
    }

    for (let i = this.yMin; i <= this.yMax; i++) {
        let y = this.PADDING + height - (i - this.yMin) * height / (this.yMax - this.yMin);
        g.moveTo(this.PADDING - 5, y);
        g.lineTo(this.PADDING + 5, y);
        g.stroke();
        g.fillText(i.toString(), this.PADDING - 30, y - 10);
    }

    let colors = ["blue", "green", "red", "orange", "purple", "brown", "magenta", "cyan"];
    for (let i = 0; i < data.length; i++) {
        let dataList = data[i];
        if (dataList.length < 2) continue;
        g.beginPath();
        g.strokeStyle = colors[i % colors.length];

        let x = this.PADDING;
        let y = this.PADDING + height / 2 - dataList[0] * height / (this.yMax - this.yMin);
        g.moveTo(x, y);

        for (let j = 1; j < dataList.length; j++) {
            x = this.PADDING + (j - this.xMin) * width / (this.xMax - this.xMin);
            y = this.PADDING + height - dataList[j] * height / (this.yMax - this.yMin);
            g.lineTo(x, y);
        }
        g.stroke();
    }
}

    get PADDING() {
        return this._PADDING;
    }

    set PADDING(value) {
        this._PADDING = value;
    }

    get pictureBox(){
        return this.pictureBox;
    }

    get _graphBitmap(){
        return this._graphBitmap;
    }
}

