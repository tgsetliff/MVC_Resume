
(function($) {

	$(document).ready( function() {

		// init scrollspy
		$('body').scrollspy({ target: '#main-nav' });
		
		// init scroll-to effect navigation, adjust the scroll speed in milliseconds			
		$('#main-nav').localScroll(1000);
		$('#header').localScroll(1000);


		// google maps 
		if( $('.map-canvas').length > 0) {
			
			var geocoder = new google.maps.Geocoder();
			var address = 'Google North Carolina, Reidsville, NC, USA';
			var contentString = '<div class="map-detail"><strong>Our Office:</strong><p>' + address + '</p></div>';
			
			geocoder.geocode({'address': address }, function(results, status) {
				if(status == google.maps.GeocoderStatus.OK) { 
					var latitude = results[0].geometry.location.lat();
					var longitude = results[0].geometry.location.lng();

					jQuery('.map-canvas').gmap().bind('init', function(ev, map) {
						jQuery('.map-canvas').gmap('addMarker', {'position': latitude+','+longitude, 'bounds': true}).click(function() {
							jQuery('.map-canvas').gmap('openInfoWindow', {'content': contentString}, this);
						});
						jQuery('.map-canvas').gmap('option', 'zoom', 8);
					});
				}else { alert('Google Maps had some trouble finding the address. Status: ' + status); }
			});
			
		}


	});

})(jQuery);