// SEE Dashboard Skin Tutorial FOR MORE INFORMATION ABOUT THIS FILE

Funbit.Ets.Telemetry.Dashboard.prototype.filter = function (data, utils) {
	data.job.destinationCompany = "(" + data.job.destinationCompany + ")"
	data.job.sourceCompany = "(" + data.job.sourceCompany + ")"
    data.hasJob = data.trailer.attached;
    // round truck speed    
    data.truck.speedRounded = Math.abs(data.truck.speed > 0
        ? Math.floor(data.truck.speed)
        : Math.round(data.truck.speed));
    // convert kilometers per hour to miles per hour (just an example)
    data.truck.speedMph = data.truck.speed * 0.621371;
    // convert kg to t
    data.trailer.mass = (data.trailer.mass / 1000.0) + 't';
    // format odometer data as: 00000.0
    data.truck.odometer = utils.formatFloat(data.truck.odometer, 1);
    // convert gear to readable format
    data.truck.gear = data.truck.gear > 0 ? data.truck.gear : (data.truck.gear < 0 ? 'R' : 'N');
    // convert rpm to rpm * 100
    data.truck.engineRpm = data.truck.engineRpm / 100;
	// Is Own Trailler?
	if (data.trailer.mass == "0t") {
		data.trailer.name = "자가 트레일러";
		data.job.destinationCity = "";
		data.job.destinationCompany = "";
		data.job.sourceCity = "";
		data.job.sourceCompany = "";
		data.job.income = "0";
		data.job.remainingTime = 'own'
	};
    // return changed data to the core for rendering
    return data;
};

Funbit.Ets.Telemetry.Dashboard.prototype.render = function (data, utils) {    
}

Funbit.Ets.Telemetry.Dashboard.prototype.initialize = function (skinConfig, utils) {
    utils.preloadImages(['images/bg-on.jpg']);
}