(function ( $ ) { 
	       'use strict';
		    
			
   // ______________ Tooltip
    // var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    // var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    // return new bootstrap.Tooltip(tooltipTriggerEl)
    // })
    

    var tooltip = new bootstrap.Tooltip(document.querySelector('[data-bs-toggle="tooltip-primary"]'), {
        template: '<div class="tooltip tooltip-primary" role="tooltip"><div class="tooltip-arrow"><\/div><div class="tooltip-inner"><\/div><\/div>'
    })
	
    var tooltip = new bootstrap.Tooltip(document.querySelector('[data-bs-toggle="tooltip-secondary"]'), {
        template: '<div class="tooltip tooltip-secondary" role="tooltip"><div class="tooltip-arrow"><\/div><div class="tooltip-inner"><\/div><\/div>'
    })
    var tooltip = new bootstrap.Tooltip(document.querySelector('[data-bs-toggle="tooltip-warning"]'), {
        template: '<div class="tooltip tooltip-warning" role="tooltip"><div class="tooltip-arrow"><\/div><div class="tooltip-inner"><\/div><\/div>'
    })
    var tooltip = new bootstrap.Tooltip(document.querySelector('[data-bs-toggle="tooltip-danger"]'), {
        template: '<div class="tooltip tooltip-danger" role="tooltip"><div class="tooltip-arrow"><\/div><div class="tooltip-inner"><\/div><\/div>'
    })
	  
	
		
}( jQuery ));
