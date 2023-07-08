(function ($) {
    "use strict"; // Start of use strict

    // Toggle the side navigation
    $("#sidebarToggle, #sidebarToggleTop").on('click', function (e) {
        var url = "/Home/SetSideBarCookie";
        PushCookies(url);
        $("body").toggleClass("toggled");
        $(".sidebar").toggleClass("toggled");
        if ($(".sidebar").hasClass("toggled")) {
            $('.sidebar .collapse').collapse('hide');
        };
    });
    // Close any open menu accordions when window is resized below 768px
    $(window).resize(function () {
        if ($(window).width() < 768) {
            $('.sidebar .collapse').collapse('hide');
        };

        // Toggle the side navigation when window is resized below 480px
        if ($(window).width() < 480 && !$(".sidebar").hasClass("toggled")) {
            $("body").addClass("sidebar-toggled");
            $(".sidebar").addClass("toggled");
            $('.sidebar .collapse').collapse('hide');
        };
    });

    // Prevent the content wrapper from scrolling when the fixed side navigation hovered over
    $('body.fixed-nav .sidebar').on('mousewheel DOMMouseScroll wheel', function (e) {
        if ($(window).width() > 768) {
            var e0 = e.originalEvent,
                delta = e0.wheelDelta || -e0.detail;
            this.scrollTop += (delta < 0 ? 1 : -1) * 30;
            e.preventDefault();
        }
    });

    // Scroll to top button appear
    $(document).on('scroll', function () {
        var scrollDistance = $(this).scrollTop();
        if (scrollDistance > 100) {
            $('.scroll-to-top').fadeIn();
        } else {
            $('.scroll-to-top').fadeOut();
        }
    });

    // Smooth scrolling using jQuery easing
    $(document).on('click', 'a.scroll-to-top', function (e) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top)
        }, 1000, 'easeInOutExpo');
        e.preventDefault();
    });

})(jQuery); // End of use strict

function PushThemeCookies(url, selectedtheme) {
    var settings = {
        'cache': false,
        'dataType': "jsonp",
        "async": true,
        "crossDomain": true,
        "url": url,
        "data": { theme: selectedtheme },
        "method": "POST",
        "headers": {
            "accept": "application/json",
            "Access-Control-Allow-Origin": "*"
        }
    }

    $.ajax(settings);
            $(document).ajaxComplete(function () {
            this.location.reload();
        });

}
function PushCookies(url) {
    var settings = {
        'cache': false,
        'dataType': "jsonp",
        "async": true,
        "crossDomain": true,
        "url": url,
        "method": "GET",
        "headers": {
            "accept": "application/json",
            "Access-Control-Allow-Origin": "*"
        }
    }

    $.ajax(settings);
}
function allowDrop(ev) {
        ev.preventDefault();
    }

function drag(ev, style) {
    ev.dataTransfer.setData("text", ev.target.id);
    if (style != null)
        ev.dataTransfer.setData("borderstyle", style);
    }

function drop(ev, el) {
        ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    el.appendChild(document.getElementById(data));

    }
function dropWidget(ev, el) {
        ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    var style = ev.dataTransfer.getData("borderstyle");
    var status = ev.target.id;
    var targetStyle = $('#'+status).attr('name');
        var senddata = { id: data, status: status }
        $.ajax({
            type: 'Get',
            url: '/Intakes/UpdateIntake',
            dataType: 'json',
            data: senddata,
            success: function (Data) {
                //Perform After Task
                $("#" + data).removeClass('border-left-' + style);
                $("#" + data).addClass('border-left-' + targetStyle);
                el.appendChild(document.getElementById(data));
            },
            error: function (xhr, status, error) {

            }
        });

    }
function dropKanban(ev, el) {
        ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    var style = ev.dataTransfer.getData("borderstyle");
    var status = ev.target.id;
    var targetStyle = $('#'+status).attr('name');
        var senddata = { id: data, status: status }
        $.ajax({
            type: 'Get',
            url: '/Intakes/UpdateIntake',
            dataType: 'json',
            data: senddata,
            success: function (Data) {
                //Perform After Task
                $("#" + data).removeClass('border-left-' + style);
                $("#" + data).addClass('border-left-' + targetStyle);
                el.appendChild(document.getElementById(data));
                location.reload()
            },
            error: function (xhr, status, error) {
                location.reload()
            }
        });

    }
