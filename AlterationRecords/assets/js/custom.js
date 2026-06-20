(function ($) {
	"use strict";
	
    /* footer year */
	if (!document.querySelector(".error-page1") && !document.querySelector(".auth-page")) {
    document.getElementById("year").innerHTML = new Date().getFullYear();
	}
    /* footer year */

	// ______________ PAGE LOADING
	$(window).on("load", function (e) {
		$("#global-loader").fadeOut("slow");
	})


	// ______________Full Screen
	$(document).on("click", ".fullscreen-button", function toggleFullScreen() {
		if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
			if (document.documentElement.requestFullScreen) {
				document.documentElement.requestFullScreen();
			} else if (document.documentElement.mozRequestFullScreen) {
				document.documentElement.mozRequestFullScreen();
			} else if (document.documentElement.webkitRequestFullScreen) {
				document.documentElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
			} else if (document.documentElement.msRequestFullscreen) {
				document.documentElement.msRequestFullscreen();
			}
		} else {
			if (document.cancelFullScreen) {
				document.cancelFullScreen();
			} else if (document.mozCancelFullScreen) {
				document.mozCancelFullScreen();
			} else if (document.webkitCancelFullScreen) {
				document.webkitCancelFullScreen();
			} else if (document.msExitFullscreen) {
				document.msExitFullscreen();
			}
		}
	})

	// ______________Headerfixed
	$(window).on("scroll", function (e) {
		if ($(window).scrollTop() >= 66) {
			$('header').addClass('fixed-header');
		}
		else {
			$('.header').removeClass('fixed-header');
		}
	});


	// ______________ Chart Circle
	if ($('.chart-circle').length) {
		$('.chart-circle').each(function () {
			let $this = $(this);

			$this.circleProgress({
				fill: {
					color: $this.attr('data-bs-color')
				},
				size: $this.height(),
				startAngle: -Math.PI / 4 * 2,
				emptyFill: '#e9eaf3',
				lineCap: 'round'
			});
		});
	}

	// ______________Cover Image
	$(".cover-image").each(function () {
		var attr = $(this).attr('data-bs-image-src');
		if (typeof attr !== typeof undefined && attr !== false) {
			$(this).css('background', 'url(' + attr + ') center center');
		}
	});




	// ______________ BACK TO TOP BUTTON
	$(window).on("scroll", function (e) {
		if ($(this).scrollTop() > 300) {
			$('#back-to-top').fadeIn('slow');
		} else {
			$('#back-to-top').fadeOut('slow');
		}
	});

	$("#back-to-top").on("click", function (e) {
		$("html, body").animate({
			scrollTop: 0
		}, 600);
		return false;
	});


	//Increment & Decrement
	var quantitiy = 0;
	$('.quantity-right-plus').on('click', function (e) {
		e.preventDefault();
		var quantity = parseInt($('.quantity').val());
		$('.quantity').val(quantity + 1);

	});
	$('.quantity-left-minus').on('click', function (e) {
		e.preventDefault();
		var quantity = parseInt($('.quantity').val());
		if (quantity > 0) {
			$('.quantity').val(quantity - 1);
		}
	});


	//calender2
	$('#calendar').tuiCalendar;

	/*----GlobalSearch----*/
	$(document).on("click", "[data-bs-toggle='search']", function (e) {
		var body = $("body");

		if (body.hasClass('search-gone')) {
			body.addClass('search-gone');
			body.removeClass('search-show');
		} else {
			body.removeClass('search-gone');
			body.addClass('search-show');
		}
	});
	var toggleSidebar = function () {
		var w = $(window);
		if (w.outerWidth() <= 1024) {
			$("body").addClass("sidebar-gone");
			$(document).off("click", "body").on("click", "body", function (e) {
				if ($(e.target).hasClass('sidebar-show') || $(e.target).hasClass('search-show')) {
					$("body").removeClass("sidebar-show");
					$("body").addClass("sidebar-gone");
					$("body").removeClass("search-show");
				}
			});
		} else {
			$("body").removeClass("sidebar-gone");
		}
	}
	toggleSidebar();
	$(window).resize(toggleSidebar);

	$(document).ready(function () {
		$(".slide-toggle").click(function () {
			$(".right-sidebarbox").animate({
				width: "toggle"
			});
		});
	});


	// ___________TOOLTIP	
	var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
	var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
		return new bootstrap.Tooltip(tooltipTriggerEl)
	})


	// __________POPOVER
	var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
	var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
		return new bootstrap.Popover(popoverTriggerEl)
	})


	// ______________ SWITCHER-toggle ______________//
	$('.layout-setting').on("click", function (e) {
		let html = document.querySelector('html');
		if (html.getAttribute('data-theme-color') === "dark") {
			html.setAttribute('data-theme-color', 'light');
			// html.setAttribute('data-header-style', 'light');
			// html.setAttribute('data-menu-style', 'light');
			$('#switchbtn-lightmenu').prop('checked', true);
			$('#switchbtn-lightheader').prop('checked', true);

			localStorage.setItem("spannerlighttheme", true);
			localStorage.removeItem("spannerdarktheme");
			localStorage.removeItem("spannerbgColor");
			// localStorage.removeItem("spannerheaderbg");
			localStorage.removeItem("spannerbgwhite");
			// localStorage.removeItem("spannermenubg");

			localStorage.setItem("spannerHeader", 'light');
			localStorage.setItem("spannerMenu", 'light');

			checkOptions();
			$('#switchbtn-light').prop('checked', true);

			if (!document.body.classList.contains('auth-page')) {
				let mainHeader = document.querySelector('.app-header');
				mainHeader.style = "";
				let appSidebar = document.querySelector('.app-sidebar');
				appSidebar.style = "";
			}
			document.querySelector('html').style = '';
			names();

		}
		else {
			html.setAttribute('data-theme-color', 'dark');
			// html.setAttribute('data-header-style', 'dark');
			// html.setAttribute('data-menu-style', 'dark');
			$('#switchbtn-darkmenu').prop('checked', true);
			$('#switchbtn-darkheader').prop('checked', true);

			localStorage.setItem("spannerdarktheme", true);
			// localStorage.removeItem("spannerlighttheme");
			localStorage.removeItem("spannerbgColor");
			// localStorage.removeItem("spannerheaderbg");
			// localStorage.removeItem("spannerbgwhite");
			// localStorage.removeItem("spannermenubg");

			localStorage.setItem("spannerHeader", 'dark');
			localStorage.setItem("spannerMenu", 'dark');

			$('#switchbtn-dark').prop('checked', true);
			checkOptions();

			if (!document.body.classList.contains('auth-page')) {
				let mainHeader = document.querySelector('.app-header');
				mainHeader.style = "";
				let appSidebar = document.querySelector('.app-sidebar');
				appSidebar.style = "";
			}
			document.querySelector('html').style = '';
			names();
		}
	});

})(jQuery);

$(function (e) {
	/** Constant div card */
	const DIV_CARD = 'div.card';
	/** Function for remove card */
	$('[data-bs-toggle="card-remove"]').on('click', function (e) {
		let $card = $(this).closest(DIV_CARD);

		$card.remove();

		e.preventDefault();
		return false;
	});
	/** Function for collapse card */
	$('[data-bs-toggle="card-collapse"]').on('click', function (e) {
		let $card = $(this).closest(DIV_CARD);

		$card.toggleClass('card-collapsed');

		e.preventDefault();
		return false;
	});
	/** Function for Fullscreen card */
	$('[data-bs-toggle="card-fullscreen"]').on('click', function (e) {
		let $card = $(this).closest(DIV_CARD);

		$card.toggleClass('card-fullscreen').removeClass('card-collapsed');

		e.preventDefault();
		return false;
	});
});


