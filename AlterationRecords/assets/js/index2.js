
"use strict";
function spark1() {

	$(".sparkline_bar1").sparkline([5, 4, 5, 4, 3, 4, 5, 6, 4, 5, 6, 3, 2], {
		type: 'bar',
		height: 50,
		width: 180,
		barSpacing: 5,
		colorMap: {
			'9': '#a1a1a1'
		},
		barColor: "rgb(" + myVarVal + ")"
	});
}

$(".sparkline_bar2").sparkline([5, 4, 5, 4, 3, 4, 5, 6, 4, 5, 6, 3, 2], {
	type: 'bar',
	height: 50,
	width: 180,
	barSpacing: 5,
	colorMap: {
		'9': '#a1a1a1'
	},
	barColor: '#1396cc'
});

$(".sparkline_bar3").sparkline([5, 4, 5, 4, 3, 4, 5, 6, 4, 5, 6, 3, 2], {
	type: 'bar',
	height: 50,
	width: 180,
	barSpacing: 5,
	colorMap: {
		'9': '#a1a1a1'
	},
	barColor: '#ff9933'
});

$(".sparkline_bar4").sparkline([5, 4, 5, 4, 3, 4, 5, 6, 4, 5, 6, 3, 2], {
	type: 'bar',
	height: 50,
	width: 180,
	barSpacing: 5,
	colorMap: {
		'9': '#a1a1a1'
	},
	barColor: '#45aaf2'
});


/*---Website-activities---*/
var options = {
	series: [{
		name: 'series1',
		data: [10, 60, 30, 90, 120, 76, 35]
	}, {
		name: 'series2',
		data: [22, 88, 68, 140, 60, 80, 95]
	}],
	chart: {
		height: 350,
		type: 'area',
		toolbar: {
			show: false,
		}
	},
	legend: {
		position: 'top'
	},
	colors: ['#089e60', '#1396cc'],
	dataLabels: {
		enabled: false
	},
	markers: {
		size: 5,
		strokeWidth: 0,
	},
	stroke: {
		curve: 'smooth',
		width: 2,
	},
	fill: {
		colors: ['#089e60', '#1396cc'],
		opacityFrom: 9,
		opacityTo: 1,
	},
	grid: {
		borderColor: 'rgba(148, 158, 183, 0.2)',
	},
	xaxis: {
		type: 'datetime',
		categories: ["2018-09-19T00:00:00.000Z", "2018-09-19T01:30:00.000Z", "2018-09-19T02:30:00.000Z", "2018-09-19T03:30:00.000Z", "2018-09-19T04:30:00.000Z", "2018-09-19T05:30:00.000Z", "2018-09-19T06:30:00.000Z"]
	},
	tooltip: {
		x: {
			format: 'dd/MM/yy HH:mm'
		},
	},
};
var chart1 = new ApexCharts(document.querySelector("#Website-activities"), options);
chart1.render();

 /* start donut chart */
 var options13 = {
	series: [44, 55, 41],
	labels: ['Online', 'Ofline', 'Referals'],
	chart: {
		type: 'donut',
		height: 350
	},
	legend: {
		position: 'bottom'
	},
	colors: ["#089e60", "#1396cc", "#ff9933"],
	responsive: [{
		breakpoint: 480,
		options: {
			chart: {
				width: 150
			},
			legend: {
				position: 'bottom'
			}
		}
	}],
	dropShadow: {
		enabled: false,
	},
	dataLabels: {
		size: 0,
		dropShadow: {
			enabled: false,
		}
	},
	plotOptions: {
		pie: {
			size: 50
		}
	}
};
var chart13 = new ApexCharts(document.querySelector("#Order-Status"), options13);
chart13.render();
/* end donut chart */

function Website() {
	chart1.updateOptions({ 
		colors: ["rgb(" + myVarVal + ")",'#1396cc'], 
		fill: {
			colors: ["rgb(" + myVarVal + ")",'#1396cc'], 
		},
	});
}
function Order() {
	chart13.updateOptions({ 
		colors: ["rgb(" + myVarVal + ")", "#1396cc", "#ff9933"],
	});
}