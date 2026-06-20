
"use strict";

/* start line chart */
var options = {
	series: [{
		name: 'TEAM A',
		type: 'line',
		data: [10, 15, 9, 18, 10, 15, 15, 9, 18]
	}, {
		name: 'TEAM B',
		type: 'line',
		data: [12, 8, 15, 15, 9, 25, 14, 10, 15]
	}],
	chart: {
		height: 310,
		type: 'line',
		zoom: {
			enabled: false
		}
	},
	colors: ['#1396cc', '#089e60'],
	dataLabels: {
		enabled: false
	},
	stroke: {
		curve: 'smooth',
		width: [3, 3,],
		dashArray: [2, 0]
	},

	grid: {
		borderColor: '#f2f5f7',
	},
	title: {
		align: 'left',
		style: {
			fontSize: '13px',
			fontWeight: 'bold',
			color: '#8c9097'
		},
	}, markers: {
		size: 0
	},
	xaxis: {
		categories: ['Dec 01', 'Dec 02', 'Dec 03', 'Dec 04', 'Dec 05', 'Dec 06', 'Dec 07', 'Dec 08', 'Dec 09 ', 'Dec 10', 'Dec 11'],
		labels: {
			show: true,
			style: {
				colors: "#8c9097",
				fontSize: '11px',
				fontWeight: 600,
				cssClass: 'apexcharts-xaxis-label',
			},
		}
	},
	yaxis: [{
		title: {
			text: 'Series A',
		},

	}, {
		opposite: true,
		title: {
			text: 'Series B',
		}
	}],
	legend: {
		position: 'top'
	},
	tooltip: {
		shared: true,
		intersect: false,
		y: {
			formatter: function (y) {
				if (typeof y !== "undefined") {
					return y.toFixed(0) + " points";
				}
				return y;
			}
		}
	}
};
var chart = new ApexCharts(document.querySelector("#finance-Monitoring"), options);
if (document.querySelector("#finance-Monitoring")) {
	chart.render();
}
/* end line chart */

function finance() {
	chart.updateOptions({
		colors: ["#1396cc", myVarVal2],
	});
}