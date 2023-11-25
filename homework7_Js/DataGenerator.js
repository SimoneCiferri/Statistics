class DataGenerator {
	
	lineChartValuesArithmeticBrownian = [];
	lineChartValuesGeometricBrownian = [];
	lineChartValuesOrnsteinUhlenbeck = [];
	lineChartValuesVasicek = [];
	lineChartValuesHullWhite = [];
	lineChartValuesCIR = [];
	lineChartValuesBlackKarasinski = [];
	lineChartValuesHeston = [];
	lineChartValuesChenModel = [];
	lineChartValuesChenModelX = [];
	lineChartValuesChenModelY = [];
	
    constructor(m, n) {
        this.M = m;
        this.N = n;
        
    }
	
	generateArithmeticBrownianValues(numSteps, drift, volatility, timeStep) {
		const valuesChart = [];
		let currentValue = 0;

		for (let i = 0; i < numSteps; i++) {
			const randomValue = (Math.random() - 0.5) * 2;
			const increment = drift * timeStep + volatility * Math.sqrt(timeStep) * randomValue;
			currentValue += increment;
			valuesChart.push(currentValue);
		}

		// Append the generated values to the global variable
		this.lineChartValuesArithmeticBrownian.push(valuesChart);
	}
	
	generateGeometricBrownianValues(numSteps, drift, volatility, timeStep) {
		const valuesChart = [];
		let currentValue = 2;

		for (let i = 0; i < numSteps; i++) {
			const randomValue = Math.random() - 0.5;
			const increment = drift * currentValue * timeStep + volatility * currentValue * Math.sqrt(timeStep) * randomValue;
			currentValue += increment;
			valuesChart.push(currentValue);
		}

		// Append the generated values to the global variable
		this.lineChartValuesGeometricBrownian.push(valuesChart);
	}
	
	
	generateOrnsteinUhlenbeckValues(numSteps, initialX, theta, mu, sigma, timeStep) {
		const valuesChart = [];
		let currentValue = initialX;

		for (let i = 0; i < numSteps; i++) {
			const randomValue = (Math.random() - 0.5) * 2;
			const increment = theta * (mu - currentValue) * timeStep + sigma * Math.sqrt(timeStep) * randomValue;
			currentValue += increment;
			valuesChart.push(currentValue);
		}

		// Append the generated values to the global variable
		this.lineChartValuesOrnsteinUhlenbeck.push(valuesChart);
	}
	
	generateVasicekValues(numSteps, r0, kappa, theta, sigma, timeStep) {
	  r0 = r0 || 0.05; // initial short rate
	  kappa = kappa || 0.1; // mean reversion speed
	  theta = theta || 0.05; // long-term mean of the short rate
	  sigma = sigma || 0.1; // volatility of the short rate
	  timeStep = timeStep || 0.1;
	  numSteps = numSteps || 100;

	  const values = [r0]; // array to store short rate values over time

	  for (let i = 1; i <= numSteps; i++) {
		const drift = kappa * (theta - values[i - 1]) * timeStep;
		const diffusion = sigma * Math.sqrt(timeStep) * Math.random(); // using a random component for diffusion

		const nextValue = values[i - 1] + drift + diffusion;
		values.push(nextValue);
	  }

	  this.lineChartValuesVasicek.push(values);
	}

	generateHullWhiteValues(numSteps, mean, speed, volatility, timeStep, longTermMean, meanReversionSpeed) {
		const valuesChart = [];
		let currentValue = 2;

		for (let i = 0; i < numSteps; i++) {
			const randomValue = Math.random() - 0.5;
			const increment = speed * (mean - currentValue) * timeStep + volatility * Math.sqrt(timeStep) * randomValue;
			const meanIncrement = meanReversionSpeed * (longTermMean - currentValue) * timeStep;
			currentValue += increment + meanIncrement;
			valuesChart.push(currentValue);
		}

		// Append the generated values to the global variable
		this.lineChartValuesHullWhite.push(valuesChart);
	}

	
	generateCIRValues(numSteps, mean, speed, volatility, timeStep, longTermMean, meanReversionSpeed) {
		const valuesChart = [];
		let currentValue = 2;

		for (let i = 0; i < numSteps; i++) {
			const randomValue = Math.random() - 0.5;
			const increment = speed * (mean - currentValue) * timeStep + volatility * Math.sqrt(currentValue) * Math.sqrt(timeStep) * randomValue;
			const meanIncrement = meanReversionSpeed * (longTermMean - currentValue) * timeStep;
			currentValue += increment + meanIncrement;
			valuesChart.push(currentValue);
		}

		// Append the generated values to the global variable
		this.lineChartValuesCIR.push(valuesChart);
	}

	generateBlackKarasinskiValues(numSteps, mean, speed, volatility, timeStep, longTermMean, meanReversionSpeed, eta) {
		const valuesChart = [];
		let currentValue = 2;

		for (let i = 0; i < numSteps; i++) {
			const randomValue = Math.random() - 0.5;
			const increment = speed * (mean - currentValue) * timeStep + volatility * Math.sqrt(currentValue) * Math.sqrt(timeStep) * randomValue;
			const meanIncrement = meanReversionSpeed * (longTermMean - currentValue) * timeStep;
			const etaIncrement = eta * Math.sqrt(currentValue) * Math.sqrt(timeStep) * randomValue;
			currentValue += increment + meanIncrement + etaIncrement;
			valuesChart.push(currentValue);
		}

		// Append the generated values to the global variable
		this.lineChartValuesBlackKarasinski.push(valuesChart);
	}

	
	generateHestonValues(numSteps, initialPrice, mean, speed, volatility, correlation, longTermVolatility, meanReversionSpeed, eta, timeStep) {
		const valuesChart = [];
		let currentPrice = initialPrice;
		let currentVolatility = volatility;

		for (let i = 0; i < numSteps; i++) {
			const randomValue1 = Math.random();
			const randomValue2 = Math.random();
			const priceIncrement = mean * currentPrice * timeStep + Math.sqrt(currentVolatility * timeStep) * Math.sqrt(1 - correlation ** 2) * randomValue1;
			const volatilityIncrement = meanReversionSpeed * (longTermVolatility - currentVolatility) * timeStep + eta * Math.sqrt(currentVolatility * timeStep) * randomValue2;

			currentPrice += priceIncrement;
			currentVolatility += volatilityIncrement;

			valuesChart.push(currentPrice);
		}

		// Append the generated values to the global variable
		this.lineChartValuesHeston.push(valuesChart);
	}
	
	
	generateChenModelValues(numSteps, initialX, initialY, alpha, beta, gamma, delta, rho, timeStep) {
		const valuesChartX = [];
		const valuesChartY = [];
		let currentX = initialX;
		let currentY = initialY;

		for (let i = 0; i < numSteps; i++) {
			const randomValue1 = Math.random();
			const randomValue2 = Math.random();
			const xIncrement = alpha * (beta * currentY - currentX) * timeStep;
			const yIncrement = (delta * currentX - gamma * currentY - currentX * currentZ) * timeStep;

			currentX += xIncrement;
			currentY += yIncrement;

			valuesChartX.push(currentX);
			valuesChartY.push(currentY);
		}

		// Append the generated values to the global variables
		this.lineChartValuesChenModelX.push(valuesChartX);
		this.lineChartValuesChenModelY.push(valuesChartY);
	}
	
	getArithmeticBrownianValues(numSteps, drift, volatility, timeStep) {
		this.lineChartValuesArithmeticBrownian = [];
		for (let i = 0; i < this.M; i++) {
			this.generateArithmeticBrownianValues(numSteps, drift, volatility, timeStep);
		}
		return this.lineChartValuesArithmeticBrownian;
	}
	
	getGeometricBrownianValues(numSteps, drift, volatility, timeStep) {
		this.lineChartValuesGeometricBrownian = [];
		for (let i = 0; i < this.M; i++) {
			this.generateGeometricBrownianValues(numSteps, drift, volatility, timeStep);
		}
		return this.lineChartValuesGeometricBrownian;
	}
	
	getOrnsteinUhlenbeckValues(numSteps, initialX, theta, mu, sigma, timeStep) {
		this.lineChartValuesOrnsteinUhlenbeck = [];
		for (let i = 0; i < this.M; i++) {
			this.generateOrnsteinUhlenbeckValues(numSteps, initialX, theta, mu, sigma, timeStep);
		}
		return this.lineChartValuesOrnsteinUhlenbeck;
	}
	
	getVasicekValues(numSteps, r0, kappa, theta, sigma, timeStep) {
		this.lineChartValuesVasicek = [];
		for (let i = 0; i < this.M; i++) {
			this.generateVasicekValues(numSteps, r0, kappa, theta, sigma, timeStep);
		}
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
