
//orders
var options = {
  series: [75],
  chart: {
    height: 150,
    type: 'radialBar',
    toolbar: {
      show: true
    }
  },
  plotOptions: {
    radialBar: {
      startAngle: -135,
      endAngle: 225,
      hollow: {
        margin: 0,
        size: '70%',
        background: '#fff',
        image: undefined,
        imageOffsetX: 0,
        imageOffsetY: 0,
        position: 'front',
        dropShadow: {
          enabled: false,
          top: 3,
          left: 0,
          blur: 4,
          opacity: 0.24
        }
      },
      track: {
        background: '#fff',
        strokeWidth: '67%',
        margin: 0, // margin is in pixels
        dropShadow: {
          enabled: true,
          top: 0,
          left: 0,
          blur: 5,
          opacity: 0.15
        }
      },

      dataLabels: {
        show: true,
        name: {
          offsetY: -6,
          show: true,
          color: '#888',
          fontSize: '13px'
        },
        value: {
          formatter: function (val) {
            return parseInt(val);
          },
          offsetY: -0,
          color: '#505062',
          fontSize: '20px',
          show: true,
        }
      }
    }
  },
  colors: ['#089e60'],
  stroke: {
    lineCap: 'round'
  },
  labels: ['Percent'],
};
var chart1 = new ApexCharts(document.querySelector("#orders"), options);
chart1.render();
//orders

//subscriptions
var options = {
  series: [50],
  chart: {
    height: 170,
    height: 150,
    type: 'radialBar',
    toolbar: {
      show: true
    }
  },
  plotOptions: {
    radialBar: {
      startAngle: -135,
      endAngle: 225,
      hollow: {
        margin: 0,
        size: '70%',
        background: '#fff',
        image: undefined,
        imageOffsetX: 0,
        imageOffsetY: 0,
        position: 'front',
        dropShadow: {
          enabled: false,
          top: 3,
          left: 0,
          blur: 4,
          opacity: 0.24
        }
      },
      track: {
        background: '#fff',
        strokeWidth: '67%',
        margin: 0, // margin is in pixels
        dropShadow: {
          enabled: true,
          top: 0,
          left: 0,
          blur: 5,
          opacity: 0.15
        }
      },

      dataLabels: {
        show: true,
        name: {
          offsetY: -6,
          show: true,
          color: '#888',
          fontSize: '13px'
        },
        value: {
          formatter: function (val) {
            return parseInt(val);
          },
          offsetY: -0,
          color: '#505062',
          fontSize: '20px',
          show: true,
        }
      }
    }
  },
  colors: ['#1396cc'],
  stroke: {
    lineCap: 'round'
  },
  labels: ['Percent'],
};
var chart2 = new ApexCharts(document.querySelector("#subscriptions"), options);
chart2.render();
//subscriptions

//profit
var options = {
  series: [70],
  chart: {
    height: 170,
    height: 150,
    type: 'radialBar',
    toolbar: {
      show: true
    }
  },
  plotOptions: {
    radialBar: {
      startAngle: -135,
      endAngle: 225,
      hollow: {
        margin: 0,
        size: '70%',
        background: '#fff',
        image: undefined,
        imageOffsetX: 0,
        imageOffsetY: 0,
        position: 'front',
        dropShadow: {
          enabled: false,
          top: 3,
          left: 0,
          blur: 4,
          opacity: 0.24
        }
      },
      track: {
        background: '#fff',
        strokeWidth: '67%',
        margin: 0, // margin is in pixels
        dropShadow: {
          enabled: true,
          top: 0,
          left: 0,
          blur: 5,
          opacity: 0.15
        }
      },

      dataLabels: {
        show: true,
        name: {
          offsetY: -6,
          show: true,
          color: '#888',
          fontSize: '13px'
        },
        value: {
          formatter: function (val) {
            return parseInt(val);
          },
          offsetY: -0,
          color: '#505062',
          fontSize: '20px',
          show: true,
        }
      }
    }
  },
  colors: ['#ff9933'],
  stroke: {
    lineCap: 'round'
  },
  labels: ['Percent'],
};
var chart3 = new ApexCharts(document.querySelector("#profit"), options);
chart3.render();
//profit

//earnings
var options = {
  series: [60],
  chart: {
    height: 170,
    height: 150,
    type: 'radialBar',
    toolbar: {
      show: true
    }
  },
  plotOptions: {
    radialBar: {
      startAngle: -135,
      endAngle: 225,
      hollow: {
        margin: 0,
        size: '70%',
        background: '#fff',
        image: undefined,
        imageOffsetX: 0,
        imageOffsetY: 0,
        position: 'front',
        dropShadow: {
          enabled: false,
          top: 3,
          left: 0,
          blur: 4,
          opacity: 0.24
        }
      },
      track: {
        background: '#fff',
        strokeWidth: '67%',
        margin: 0, // margin is in pixels
        dropShadow: {
          enabled: true,
          top: 0,
          left: 0,
          blur: 5,
          opacity: 0.15
        }
      },

      dataLabels: {
        show: true,
        name: {
          offsetY: -6,
          show: true,
          color: '#888',
          fontSize: '13px'
        },
        value: {
          formatter: function (val) {
            return parseInt(val);
          },
          offsetY: -0,
          color: '#505062',
          fontSize: '20px',
          show: true,
        }
      }
    }
  },
  colors: ['#e41313'],
  stroke: {
    lineCap: 'round'
  },
  labels: ['Percent'],
};
var chart4 = new ApexCharts(document.querySelector("#earnings"), options);
chart4.render();
//earnings

//Monthly Social Action
var options = {
  series: [{
    name: 'series1',
    data: [24, 23, 20, 25, 27, 26, 24, 23, 25, 23, 20, 22,]
  }, {
    name: 'series2',
    data: [20, 23, 26, 22, 20, 26, 28, 26, 22, 27, 25, 26,]
  }],
  chart: {
    height: 350,
    type: 'area'
  },
  colors: ['#089e60', '#1396cc'],
  dataLabels: {
    enabled: false
  },
  stroke: {
    curve: 'smooth',
    width: 2,
  },
	grid: {
		borderColor: 'rgba(148, 158, 183, 0.2)',
	},
  xaxis: {
    type: 'month',
    categories: ["jan", "Feb", "Mar", "April", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"]
  },
  tooltip: {
    x: {
      format: 'dd/MM/yy HH:mm'
    },
  },
};

var chart = new ApexCharts(document.querySelector("#social"), options);
chart.render();
//Monthly Social Action


//subscribers-map
$('#subscribers-map').vectorMap({
  map: 'world_en',
  scaleColors: ['#089e60', '#089e60'],
  normalizeFunction: 'polynomial',
  hoverOpacity: 0.7,
  hoverColor: false,
  regionStyle: {
    initial: {
      fill: '#e5e9f2'
    }
  },
  markerStyle: {
    initial: {
      r: 9,
      'fill': '#089e60',
      'fill-opacity': 0.9,
      'stroke': '#fff',
      'stroke-width': 9,
      'stroke-opacity': 0.2
    },

    hover: {
      'stroke': '#fff',
      'fill-opacity': 1,
      'stroke-width': 1.5
    }
  },
  backgroundColor: 'transparent',
  markers: [{
    latLng: [20.5937, 78.9629],
    name: 'India'
  }, {
    latLng: [41.90, 12.45],
    name: 'Vatican City'
  }, {
    latLng: [43.73, 7.41],
    name: 'Monaco'
  }, {
    latLng: [-0.52, 166.93],
    name: 'Nauru'
  }, {
    latLng: [-8.51, 179.21],
    name: 'Tuvalu'
  }, {
    latLng: [43.93, 12.46],
    name: 'San Marino'
  }, {
    latLng: [47.14, 9.52],
    name: 'Liechtenstein'
  }, {
    latLng: [7.11, 171.06],
    name: 'Marshall Islands'
  }, {
    latLng: [17.3, -62.73],
    name: 'Saint Kitts and Nevis'
  }, {
    latLng: [3.2, 73.22],
    name: 'Maldives'
  }, {
    latLng: [35.88, 14.5],
    name: 'Malta'
  }, {
    latLng: [12.05, -61.75],
    name: 'Grenada'
  }, {
    latLng: [13.16, -61.23],
    name: 'Saint Vincent and the Grenadines'
  }, {
    latLng: [13.16, -59.55],
    name: 'Barbados'
  }, {
    latLng: [17.11, -61.85],
    name: 'Antigua and Barbuda'
  }, {
    latLng: [-4.61, 55.45],
    name: 'Seychelles'
  }, {
    latLng: [7.35, 134.46],
    name: 'Palau'
  }, {
    latLng: [42.5, 1.51],
    name: 'Andorra'
  }, {
    latLng: [14.01, -60.98],
    name: 'Saint Lucia'
  }, {
    latLng: [6.91, 158.18],
    name: 'Federated States of Micronesia'
  }, {
    latLng: [1.3, 103.8],
    name: 'Singapore'
  }, {
    latLng: [1.46, 173.03],
    name: 'Kiribati'
  }, {
    latLng: [-21.13, -175.2],
    name: 'Tonga'
  }, {
    latLng: [56.1304, 106.3468],
    name: 'Canada'
  }, {
    latLng: [-20.2, 57.5],
    name: 'Mauritius'
  }, {
    latLng: [26.02, 50.55],
    name: 'Bahrain'
  }, {
    latLng: [0.33, 6.73],
    name: 'SÃ£o TomÃ© and PrÃ­ncipe'
  }]
});
//subscribers-map

function social() {
  chart.updateOptions({ colors: [myVarVal2,'#1396cc'], });
}
function orders() {
  chart1.updateOptions({ colors: [myVarVal2], });
}