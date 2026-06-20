$(function(e) {
	
	//______Basic Data Table
	$('#example-2').DataTable({
		language: {
			searchPlaceholder: 'Search...',
			sSearch: '',
		}
	});
	

	//______Responsive Data Table
	$('#example-1').DataTable({
		responsive: true,
		language: {
			searchPlaceholder: 'Search...',
			sSearch: '',
		}
	});

	//______Modal Data Table
	$('#modalDataTable').DataTable( {
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.modal( {
                    header: function ( row ) {
                        var data = row.data();
                        return 'Details for '+data[0]+' '+data[1];
                    }
                } ),
                renderer: $.fn.dataTable.Responsive.renderer.tableAll( {
                    tableClass: 'table'
                } )
            }
        }
    } );

	//______File-Export Data Table
	var table = $('#example').DataTable({
		buttons: [ 'copy', 'excel', 'pdf', 'colvis' ],
		responsive: true,
		language: {
			searchPlaceholder: 'Search...',
			sSearch: '',
		}
	});
	table.buttons().container()
	.appendTo( '#example_wrapper .col-md-6:eq(0)');	

	//______Delete Data Table
	var table = $('#deletableRowDataTable').DataTable({
		language: {
			searchPlaceholder: 'Search...',
			sSearch: '',
		}
	}); 
    $('#deletableRowDataTable tbody').on( 'click', 'tr', function () {
        if ( $(this).hasClass('selected') ) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    } );
    $('#button').click( function () {
        table.row('.selected').remove().draw( false );
    } );

	//______Form Input Datatable 
	$('#form-input-datatable').DataTable({
		language: {
			searchPlaceholder: 'Search...',
			sSearch: '',
		},
		responsive: true,
		columnDefs: [{
			orderable: false,
			targets: [1,2,3]
		}]
		
	});

	//______Select2 
	$('.select2').select2({
		minimumResultsForSearch: Infinity,
	});

	// $('#form-input-datatable').on('draw.dt', function() {
	// 	$('.select2').select2({
	// 		minimumResultsForSearch: Infinity,
	// 		placeholder: 'Choose one',
	// 	});
	// });


});