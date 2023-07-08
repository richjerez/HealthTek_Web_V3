jQuery(function(){


	initAddClass();
	// Add Class  init
	function initAddClass() {
		"use strict";
		
		jQuery('.nav-opener').on( "click", function(e){
			e.preventDefault();
			jQuery("body").toggleClass("nav-active");
		});
	}
	
	initTabs();
	// content tabs init
	function initTabs() {
		jQuery('.tabset').tabset({
			tabLinks: 'a',
			addToParent: true,
			defaultTab: true
		});

		jQuery('.tabset2').tabset({
			tabLinks: 'a',
			addToParent: true,
			defaultTab: true
		});
	}

	initProgressBar();
	// Progress Bar init
	function initProgressBar() {
		if( jQuery(".progress-bar").length != '' ){
			var waypoint = new Waypoint({
				element: document.getElementById('progress-bar'),
				handler: function(direction) {
					console.log('Scrolled to startpoint!');
					jQuery('.progress-bar li').each(function() {
							var widthBar = jQuery(this).find('.over').attr('data-percent');
							jQuery(this).find('.over').animate({
								'width': widthBar
							});
					});
				},
				offset: '80%'
			});
		}
	}

	/*$.scrollIt({
		topOffset: -105,
		scrollTime: 1500,
		easing: 'easeInOutExpo'
	});*/

	initAnchors();
	// initialize smooth anchor links
	function initAnchors() {
		new SmoothScroll({
			anchorLinks: 'a.smooth[href^="#"]:not([href="#"])',
			extraOffset: 0,
			activeClasses: 'link',
			wheelBehavior: 'none'
		});
	}
	
	initSlickSlider();
	// Slick Slider init
	function initSlickSlider() {
		"use strict";

		jQuery('.partner-slider').slick({
			dots: false,
			speed: 800,
			infinite: true,
			slidesToShow: 5,
			slidesToScroll: 1,
			adaptiveHeight: true,
			autoplay: true,
			arrows: true,
			autoplaySpeed: 2000,
			responsive: [
				{
			      breakpoint: 1199,
			      settings: {
			        slidesToShow: 4,
			        slidesToScroll: 1,
			      }
			    },
			    {
			      breakpoint: 1024,
			      settings: {
			        slidesToShow: 3,
			        slidesToScroll: 1
			      }
			    },
			    {
			      breakpoint: 767,
			      settings: {
			        slidesToShow: 2,
			        slidesToScroll: 1,
			        arrows: false
			      }
			    },
			    {
			      breakpoint: 480,
			      settings: {
			        slidesToShow: 1,
			        slidesToScroll: 1,
			        arrows: false
			      }
			    }
		    ]
		});
	}

	initStickyHeader();
	// sticky header init
	function initStickyHeader() {

		"use strict";

		var win = jQuery(window),
			stickyClass = 'sticky';

		jQuery('#header').each(function() {
			var header = jQuery(this);
			var headerOffset = header.offset().top || 0;
			var flag = true;


			jQuery(this).css('height' , jQuery(this).innerHeight());

			function scrollHandler() {
				if (win.scrollTop() > headerOffset) {
					if (flag){
						flag = false;
						header.addClass(stickyClass);
					}
				} else {
					if (!flag) {
						flag = true;
						header.removeClass(stickyClass);
					}
				}
			}

			scrollHandler();
			win.on('scroll resize orientationchange', scrollHandler);
		});
	}


	initbackTop();
	// backtop init
	function initbackTop() {
		"use strict";

	    var jQuerybackToTop = jQuery("#back-top");
	    jQuery(window).on('scroll', function() {
	        if (jQuery(this).scrollTop() > 100) {
	            jQuerybackToTop.addClass('active');
	        } else {
	            jQuerybackToTop.removeClass('active');
	        }
	    });
	    jQuerybackToTop.on('click', function(e) {
	        jQuery("html, body").animate({scrollTop: 0}, 900);
	    });
	}

	initFancybox();
	// lightbox init
	function initFancybox() {
		jQuery('a.lightbox, [data-fancybox]').fancybox({
			parentEl: 'body',
			margin: [50, 0]
		});
	}

});

jQuery(window).on('load', function() {

	"use strict";

	new WOW().init();

	initPreLoader()
	// PreLoader init
	function initPreLoader() {
	    "use strict";

	    jQuery('#loader').delay(1000).fadeOut();
	}
	
});