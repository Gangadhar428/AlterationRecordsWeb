
"use strict";

/*----sales-report-chart----*/

var options = {
  series: [{
    name: 'TEAM A',
    type: 'column',
    data: [10, 15, 9, 18, 10, 15, 15, 9, 18]
  }, {
    name: 'TEAM B',
    type: 'column',
    data: [10, 14, 10, 15, 9, 25, 14, 10, 15]
  }, {
    name: 'TEAM C',
    type: 'line',
    data: [8, 5, 25, 10, 10, 5, 25, 10, 5]
  }],
  chart: {
    height: 300,
    type: 'line',
    stacked: false,
  },
  stroke: {
    width: [4, 4, 2],
    colors: ['transparent', 'transparent', '#ff9933'],
    curve: 'smooth'
  },
  plotOptions: {
    bar: {
      columnWidth: '60%',
      borderRadius: 10,
    }
  },
  legend: {
    show: false,
  },

  colors: ['#089e60', '#1396cc', '#ff9933'],
  labels: ['jan', 'feb', 'mar', 'apr', 'may', 'jun', 'jul', 'aug', 'sep', 'oct', 'nov', 'dec'],
  markers: {
    size: 0
  },
  grid: {
    borderColor: 'rgba(0, 0, 0, 0.2)',
  },
  xaxis: {
    type: 'month'
  },
  yaxis: {
    min: 0
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
var chart1 = new ApexCharts(document.querySelector("#sales-chart"), options);
chart1.render();

/*-----Today Profit-----*/
var options = {
  series: [{
    show: false,
    name: 'series1',
    data: [45, 55, 38, 67, 49, 72, 52,45, 55, 38, 67, 49, 72, 52,]
  },],
  chart: {
    height: 180,
    width: 1000,
    type: 'area'
  },
  dataLabels: {
    enabled: false
  },
  stroke: {
    curve: 'smooth',
    color: 'rgba(208, 238, 251, 0.9)',
    width: '1'
  },
  xaxis: {
    display: false,
  },
  markers: {
    size: 4,
    strokeColors: 'rgba(208, 238, 251, 0.2)',
  },
  grid: {
    show: false,
  },
  colors: ['rgba(208, 238, 251, 0.3)'],
  tooltip: {
    x: {
      format: 'dd/MM/yy HH:mm'
    },
  },
};
var chart = new ApexCharts(document.querySelector("#today-Profit"), options);
chart.render();


function sales() {
  chart1.updateOptions({ colors: ["rgb(" + myVarVal + ")",'#1396cc','#ff9933'], });
}


