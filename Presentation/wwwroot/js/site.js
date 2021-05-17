// SideNav Button Initialization
$(".button-collapse").sideNav();
// SideNav Scrollbar Initialization
var sideNavScrollbar = document.querySelector('.custom-scrollbar');
var ps = new PerfectScrollbar(sideNavScrollbar);

// Material Select Initialization
$(document).ready(function () {
    $('.mdb-select').material_select();
    // Data Picker Initialization
    $('.datepicker').pickadate();
});


$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});


