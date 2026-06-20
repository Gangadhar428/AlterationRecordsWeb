(function($) {
	"use strict";
	
	//P-scrolling
	const ps = new PerfectScrollbar('.sidebar-right', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});
	
	//P-scrolling
	const ps10 = new PerfectScrollbar('.app-sidebar', {
	  useBothWheelAxes:true,
	  suppressScrollX:true,
	});

	//P-scrolling
	const ps11 = new PerfectScrollbar('.cart-dropdown-body', {
		useBothWheelAxes:true,
		suppressScrollX:true,
	  });

	  //P-scrolling
	const ps12 = new PerfectScrollbar('.Messages-dropdown-content', {
		useBothWheelAxes:true,
		suppressScrollX:true,
	  });

	  //P-scrolling
	const ps13 = new PerfectScrollbar('.Notifications-dropdown-body', {
		useBothWheelAxes:true,
		suppressScrollX:true,
	  });
	  //P-scrolling
	const ps14 = new PerfectScrollbar('.modal-card-body-chat', {
		useBothWheelAxes:true,
		suppressScrollX:true,
	  });

	

})(jQuery);