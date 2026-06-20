
"use strict";

var options = {
  series: [{
    name: "Desktops",
    data: [0, 30, 10, 120, 50, 63, 10]
  }, {
    name: "Desktops",
    data: [0, 50, 40, 80, 40, 79, 120]
  }],
  chart: {
    height: 350,
    type: 'line',
    zoom: {
      enabled: false
    }
  },
  dataLabels: {
    enabled: false
  },
  stroke: {
    curve: 'smooth',
    width: 2,
    colors: ['#089e60', '#1396cc'],
  },
  colors: ['#089e60', '#1396cc'],
  markers: {
    strokeColor: '#fff',
  },
  grid: {
    show: true,
    row: {
      colors: ['transparent', 'transparent'], // takes an array which will be repeated on columns
      opacity: 0.5
    },
  },
  xaxis: {
    categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
  }
};

var chart = new ApexCharts(document.querySelector("#customer-analytics"), options);
chart.render();



function customer() {
  chart.updateOptions({
    stroke: {
      colors: [myVarVal2, "#1396cc"],
    },
    colors: [myVarVal2, "#1396cc"],
  });
}