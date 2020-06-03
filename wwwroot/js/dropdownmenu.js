//$('.dropdown-toggle').dropdown();




$('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
    if (!$(this).next().hasClass('show')) {
        $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
    }
    var $subMenu = $(this).next(".dropdown-menu");
    $subMenu.toggleClass('show');


    $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
        $('.dropdown-submenu .show').removeClass("show");
    });


    return false;
});


//$(function () {
//    // ------------------------------------------------------- //
//    // Multi Level dropdowns
//    // ------------------------------------------------------ //
//    $("ul.dropdown-menu [data-toggle='dropdown']").on("click", function (event) {
//        event.preventDefault();
//        event.stopPropagation();

//        $(this).siblings().toggleClass("show");


//        if (!$(this).next().hasClass('show')) {
//            $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
//        }
//        $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
//            $('.dropdown-submenu .show').removeClass("show");
//        });

//    });
//});




