class Adversary {
	
	lineChartValuesChart1 = [];
	lineChartValuesArithmeticBrownian = [];
	lineChartValuesGeometricBrownian = [];
	lineChartValuesOrnsteinUhlenbeck = [];
	lineChartValuesVasicek = [];
	lineChartValuesHullWhite = [];
	lineChartValuesCIR = [];
	lineChartValuesBlackKarasinski = [];
	lineChartValuesHeston = [];
	lineChartValuesChenModel = [];
	
    constructor(m, n, p) {
        this.M = m;
        this.N = n;
        this.P = p;
        
        this.generateAttacks();
		this.generateAllValues();
    }
		
	generateArithmeticBrownian() {
		const valuesChart = [];
		let currentValue = 0;

		for (let j = 0; j < this.N; j++) {
			const randomValue = (Math.random() - 0.5) * 2;
			currentValue += randomValue;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesArithmeticBrownian.push(valuesChart);
	}

	generateGeometricBrownian() {
		const valuesChart = [];
		let currentValue = 0;

		for (let j = 0; j < this.N; j++) {
			const randomValue = Math.random() - 0.5;
			currentValue += randomValue;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesGeometricBrownian.push(valuesChart);
	}

	generateOrnsteinUhlenbeck() {
		const valuesChart = [];
		let currentValue = 0;

		for (let j = 0; j < this.N; j++) {
			const randomValue = -0.1 * currentValue + Math.sqrt(0.1) * (Math.random() - 0.5);
			currentValue += randomValue;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesOrnsteinUhlenbeck.push(valuesChart);
	}

	generateVasicek() {
		const valuesChart = [];
		let currentValue = 0;

		for (let j = 0; j < this.N; j++) {
			const randomValue = 0.1 * (1 - currentValue) + 0.1 * Math.sqrt(currentValue) * (Math.random() - 0.5);
			currentValue += randomValue;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesVasicek.push(valuesChart);
	}

	generateHullWhite() {
		const valuesChart = [];
		let currentValue = 0;

		for (let j = 0; j < this.N; j++) {
			const randomValue = 0.1 * (Math.random() - 0.5);
			currentValue += randomValue;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesHullWhite.push(valuesChart);
	}

	generateCIR() {
		const valuesChart = [];
		let currentValue = 0.1;

		for (let j = 0; j < this.N; j++) {
			const randomValue = 0.1 * (0.1 - currentValue) + 0.1 * Math.sqrt(currentValue) * (Math.random() - 0.5);
			currentValue += randomValue;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesCIR.push(valuesChart);
	}

	generateBlackKarasinski() {
		const valuesChart = [];
		let currentValue = 0;

		for (let j = 0; j < this.N; j++) {
			const randomValue = 0.1 * (1 - currentValue) * (Math.random() - 0.5);
			currentValue += randomValue;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesBlackKarasinski.push(valuesChart);
	}

	generateHeston() {
		const valuesChart = [];
		let currentValue = 0.1;

		for (let j = 0; j < this.N; j++) {
			const randomValue1 = 0.1 * (0.1 - currentValue) + 0.1 * Math.sqrt(currentValue) * (Math.random() - 0.5);
			const randomValue2 = 0.1 * Math.sqrt(currentValue) * (Math.random() - 0.5);
			currentValue += randomValue1 + randomValue2;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesHeston.push(valuesChart);
	}

	generateChenModel() {
		const valuesChart = [];
		let currentValue = 0.1;

		for (let j = 0; j < this.N; j++) {
			const randomValue = 0.1 * Math.sqrt(Math.abs(currentValue)) * (Math.random() - 0.5);
			currentValue += randomValue;
			valuesChart.push(currentValue);
		}

		this.lineChartValuesChenModel.push(valuesChart);
	}

	generateAllValues() {
		for (let i = 0; i < this.M; i++) {
			this.generateArithmeticBrownian();
			this.generateGeometricBrownian();
			this.generateOrnsteinUhlenbeck();
			this.generateVasicek();
			this.generateHullWhite();
			this.generateCIR();
			this.generateBlackKarasinski();
			this.generateHeston();
			this.generateChenModel();
		}
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

    getLineChart1AttackList() {
		console.log("Data retrieved (adv)");
        return this.lineChartValuesChart1;
    }
	
	getAllValuesArithmeticBrownian() {
		return this.lineChartValuesArithmeticBrownian;
	}

	getAllValuesGeometricBrownian() {
		return this.lineChartValuesGeometricBrownian;
	}

	getAllValuesOrnsteinUhlenbeck() {
		return this.lineChartValuesOrnsteinUhlenbeck;
	}

	getAllValuesVasicek() {
		return this.lineChartValuesVasicek;
	}

	getAllValuesHullWhite() {
		return this.lineChartValuesHullWhite;
	}

	getAllValuesCIR() {
		return this.lineChartValuesCIR;
	}

	getAllValuesBlackKarasinski() {
		return this.lineChartValuesBlackKarasinski;
	}

	getAllValuesHeston() {
		return this.lineChartValuesHeston;
	}

	getAllValuesChenModel() {
		return this.lineChartValuesChenModel;
	}

}
