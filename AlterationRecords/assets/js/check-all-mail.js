 $(function() {
 	'use strict'
 	$('#checkAll').on('click', function() {
 		if ($(this).is(':checked')) {
 			$('.main-mail-group .ckbox input').each(function() {
 				$(this).closest('.list-group-item').addClass('selected');
 				$(this).attr('checked', true);
 			});
 			$('.main-mail-options .btn').removeClass('disabled');
 		} else {
 			$('.main-mail-group .ckbox input').each(function() {
 				$(this).closest('.list-group-item').removeClass('selected');
 				$(this).attr('checked', false);
 			});
 			$('.main-mail-options .btn').addClass('disabled');
 		}
 	});
 	$('.list-group-item .ckbox input').on('click', function() {
 		if ($(this).is(':checked')) {
 			$(this).attr('checked', false);
 			$(this).closest('.list-group-item').addClass('selected');
 			$('.main-mail-options .btn').removeClass('disabled');
 		} else {
 			$(this).attr('checked', true);
 			$(this).closest('.list-group-item').removeClass('selected');
 			if (!$('.main-mail-group .selected').length) {
 				$('.main-mail-options .btn').addClass('disabled');
 			}
 		}
 	});
 	$('.main-mail-star').on('click', function(e) {
 		$(this).toggleClass('active');
 	});
 	$('#btnCompose').on('click', function(e) {
 		e.preventDefault();
 		$('.main-mail-compose').show();
 	});
 	$('.main-mail-compose-header a:first-child').on('click', function(e) {
 		e.preventDefault();
 		$('.main-mail-compose').toggleClass('main-mail-compose-minimize');
 	})
 	$('.main-mail-compose-header a:nth-child(2)').on('click', function(e) {
 		e.preventDefault();
 		$(this).find('.fas').toggleClass('fa-compress');
 		$(this).find('.fas').toggleClass('fa-expand');
 		$('.main-mail-compose').toggleClass('main-mail-compose-compress');
 		$('.main-mail-compose').removeClass('main-mail-compose-minimize');
 	});
 	$('.main-mail-compose-header a:last-child').on('click', function(e) {
 		e.preventDefault();
 		$('.main-mail-compose').hide(100);
 		$('.main-mail-compose').removeClass('main-mail-compose-minimize');
 	});
 });