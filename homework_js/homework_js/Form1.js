// Importa le classi Adversary e Histogram
import {Adversary} from './Adversary.js';
import {Histogram} from './Histogram.js';
import {LineChart} from './LineChart.js';



    function GraphHAndler(){
        this.mouseDownLocation = { X: 0, Y: 0 };
        this.isDragging = false;
        this.graphBitmap = null;

        // Genera gli attacchi
        let adv = new Adversary(1000, 20, 0.5);
        adv.generateAttacks();

        // Recupera i dati
        let data1 = adv.getLineChart1AttackList();
        let data2 = adv.getLineChart2AttackList();
        let data3 = adv.getLineChart3AttackList();
        let data4 = adv.getLineChart4AttackList();

        let chart1 = new LineChart(pictureBox1);
        chart1.drawChart(data1);

        let chart2 = new LineChart(pictureBox2);
        chart2.drawChart(data2);

        let chart3 = new LineChart(pictureBox3);
        chart3.drawChart(data3);

        let chart4 = new LineChart(pictureBox4);
        chart4.drawChart(data4);

        let k = 20;
        let histogramDistChart1 = adv.createHistoDistrib(data1, k);
        let histogramDistChart2 = adv.createHistoDistrib(data2, k);
        let histogramDistChart3 = adv.createHistoDistrib(data3, k);
        let histogramDistChart4 = adv.createHistoDistrib(data4, k);

        let histogram1 = new Histogram(this, chart1, histogramDistChart1, k);
        let histogram2 = new Histogram(this, chart2, histogramDistChart2, k);
        let histogram3 = new Histogram(this, chart3, histogramDistChart3, k);
        let histogram4 = new Histogram(this, chart4, histogramDistChart4, k);
    }

    Form1_Load(sender, e) {
        // Codice da eseguire durante il caricamento del form
    }
}