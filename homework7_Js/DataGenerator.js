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
	
	generateHullWhiteValues(numSteps, initialR, thetaFunction, meanReversionSpeed, volatility, timeStep) {
		initialR = initialR || 0.05; // initial short rate
		thetaFunction = thetaFunction || (t => 0.05); // default to a constant function
		meanReversionSpeed = meanReversionSpeed || 0.1; // mean reversion speed
		volatility = volatility || 0.1; // volatility of the short rate
		timeStep = timeStep || 0.1;
		numSteps = numSteps || 100;

		const values = [initialR];

		for (let i = 1; i <= numSteps; i++) {
			const theta = thetaFunction(i * timeStep); // evaluate theta at current time
			const drift = (theta - meanReversionSpeed * values[i - 1]) * timeStep;
			const diffusion = volatility * Math.sqrt(timeStep) * Math.random(); // using a random component for diffusion

			const nextValue = values[i - 1] + drift + diffusion;
			values.push(nextValue);
		}

		this.lineChartValuesHullWhite.push(values);
	}
	
	generateCIRValues(numSteps, r0, kappa, theta, sigma, timeStep) {
	  r0 = r0 || 0.05; // initial short rate
	  kappa = kappa || 0.1; // mean reversion speed
	  theta = theta || 0.05; // long-term mean of the short rate
	  sigma = sigma || 0.1; // volatility of the short rate
	  timeStep = timeStep || 0.1;
	  numSteps = numSteps || 100;

	  const values = [r0]; // array to store short rate values over time

	  for (let i = 1; i <= numSteps; i++) {
		const drift = kappa * (theta - values[i - 1]) * timeStep;
		const diffusion = sigma * Math.sqrt(values[i - 1]) * Math.sqrt(timeStep) * Math.random(); // using a random component for diffusion

		const nextValue = values[i - 1] + drift + diffusion;
		values.push(nextValue < 0 ? 0 : nextValue); // Ensure short rate is non-negative
	  }

	  this.lineChartValuesCIR.push(values);
	}

	generateBlackKarasinskiValues(numSteps, r0, kappa, theta, sigma, lambda, timeStep) {
		r0 = r0 || 0.05; // initial short rate
		kappa = kappa || 0.1; // mean reversion speed
		theta = theta || 0.05; // long-term mean of the short rate
		sigma = sigma || 0.1; // volatility of the short rate
		lambda = lambda || 0.1; // mean reversion level
		timeStep = timeStep || 0.1; // time step
		numSteps = numSteps || 100;

		const values = [r0]; // array to store short rate values over time

		for (let i = 1; i <= numSteps; i++) {
			const drift = kappa * (theta - values[i - 1]) * timeStep;
			const diffusion = sigma * Math.sqrt(values[i - 1]) * Math.random(); // using a random component for diffusion
			const jump = lambda * Math.sqrt(timeStep) * Math.random(); // jump component

			const nextValue = values[i - 1] + drift + diffusion + jump;
			values.push(nextValue);
		}

		this.lineChartValuesBlackKarasinski.push(values);
	}
	
	
	generateHestonValues(numSteps, initialV, initialRho, kappa, theta, sigma, v0, rho, xi, timeStep) {
	  initialV = initialV || 0.04; // initial volatility
	  initialRho = initialRho || -0.7; // initial correlation
	  kappa = kappa || 1.5; // mean reversion speed
	  theta = theta || 0.04; // long-term mean of the volatility
	  sigma = sigma || 0.2; // volatility of the volatility
	  v0 = v0 || initialV; // initial variance
	  rho = rho || initialRho; // correlation between Brownian motions
	  xi = xi || 0.1; // volatility of the variance
	  timeStep = timeStep || 0.01; // time step
	  numSteps = numSteps || 100;

	  const values = [];
	  let v = v0;
	  let rhoValue = rho;

	  for (let i = 0; i < numSteps; i++) {
		const dW1 = Math.sqrt(timeStep) * (Math.random() - 0.5) * 2; // Brownian motion for volatility
		const dW2 = Math.sqrt(timeStep) * (Math.random() - 0.5) * 2; // Brownian motion for asset price

		const dV = kappa * (theta - v) * timeStep + sigma * Math.sqrt(v) * dW1;
		v = Math.max(v + dV, 0); // Ensure volatility stays non-negative

		const dRho = xi * (rhoValue - initialRho) * timeStep;
		rhoValue += dRho;

		const dS = Math.sqrt(1 - rhoValue ** 2) * Math.sqrt(v) * dW2;

		/*values.push({
		  volatility: v,
		  assetPrice: dS,
		});*/
		values.push(v);
	  }

	  this.lineChartValuesHeston.push(values);
	}
	
	generateChenModelValues(numSteps, r0, kappa, theta, sigma, timeStep) {
	  r0 = r0 || 0.05; // initial short rate
	  kappa = kappa || 0.1; // mean reversion speed
	  theta = theta || 0.05; // long-term mean of the short rate
	  sigma = sigma || 0.1; // volatility of the short rate
	  timeStep = timeStep || 0.1; // time step
	  numSteps = numSteps || 100;

	  const values = [r0]; // array to store short rate values over time

	  for (let i = 1; i <= numSteps; i++) {
		const drift = kappa * (theta - values[i - 1]) * timeStep;
		const diffusion = sigma * Math.sqrt(Math.abs(values[i - 1])) * Math.sqrt(timeStep) * (Math.random() - 0.5);

		const nextValue = values[i - 1] + drift + diffusion;
		values.push(nextValue);
	  }

	  this.lineChartValuesChenModel.push(values);
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
	
	
	getHullWhiteValues(numSteps, initialR, thetaFunction, meanReversionSpeed, volatility, timeStep) {
		this.lineChartValuesHullWhite = [];
		for (let i = 0; i < this.M; i++) {
			this.generateHullWhiteValues(numSteps, initialR, thetaFunction, meanReversionSpeed, volatility, timeStep);
		}
		return this.lineChartValuesHullWhite;
	}

	getCIRValues(numSteps, r0, kappa, theta, sigma, timeStep) {
		this.lineChartValuesCIR = [];
		for (let i = 0; i < this.M; i++) {
			this.generateCIRValues(numSteps, r0, kappa, theta, sigma, timeStep);
		}
		return this.lineChartValuesCIR;
	}
	
	
	getBlackKarasinskiValues(numSteps, r0, kappa, theta, sigma, lambda, timeStep) {
		this.lineChartValuesBlackKarasinski = [];
		for (let i = 0; i < this.M; i++) {
			this.generateBlackKarasinskiValues(numSteps, r0, kappa, theta, sigma, lambda, timeStep);
		}
		return this.lineChartValuesBlackKarasinski;
	}

	getHestonValues(numSteps, initialV, initialRho, kappa, theta, sigma, v0, rho, xi, timeStep) {
		this.lineChartValuesHeston = [];
		for (let i = 0; i < this.M; i++) {
			this.generateHestonValues(numSteps, initialV, initialRho, kappa, theta, sigma, v0, rho, xi, timeStep);
		}
		return this.lineChartValuesHeston;
	}
	
	getChenModelValues(numSteps, r0, kappa, theta, sigma, timeStep) {
		this.lineChartValuesChenModel = [];
		for (let i = 0; i < this.M; i++) {
			this.generateChenModelValues(numSteps, r0, kappa, theta, sigma, timeStep);
		}
		return this.lineChartValuesChenModel;
	}

}
