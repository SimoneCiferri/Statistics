class Adversary {
    constructor(m, n, p) {
        this.M = m;
        this.N = n;
        this.P = p;

        this.lineChartValuesChart1 = [];
        
        this.generateAttacks();
    }

    generateAttacks() {
        let random = Math.random();

        for (let i = 0; i < this.M; i++) {
            const valuesChart1 = [];

            for (let j = 0; j < this.N; j++) {
                // Generate random from (0, 1]
                let randomNumber = Math.random();

                if (randomNumber > this.P) {
                    // Attack success
                    valuesChart1.push(-1);
                } else {
                    // Attack failed
                    valuesChart1.push(1);
                }
            }
            this.lineChartValuesChart1.push(valuesChart1);
        }
		console.log("Data generated (adv)");
    }

    createHistoDistrib(values) {
        const finalValues = {};

        for (let i = 0; i < values.length; i++) {
            let sum = 0;

            for (let s = 0; s < values[i].length; s++) {
                sum += values[i][s];
            }
            finalValues[i] = sum;
        }

        return finalValues;
    }

    createHistoDistribFloat(values) {
        const finalValues = {};

        for (let i = 0; i < values.length; i++) {
            finalValues[i] = values[i][values[i].length - 1];
        }

        return finalValues;
    }

    getLineChart1AttackList() {
		console.log("Data retrieved (adv)");
        return this.lineChartValuesChart1;
    }

}
