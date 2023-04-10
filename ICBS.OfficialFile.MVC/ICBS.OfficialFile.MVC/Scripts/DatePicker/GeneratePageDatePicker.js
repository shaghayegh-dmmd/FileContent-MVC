$(document).ready(function () {
    $(".datePicker").datepicker({
        dateFormat: "yy/mm/dd",
        changeMonth: true,
        changeYear: true
    });

    if (!Modernizr.inputtypes.date) {
        $('.datePicker').datepicker({
                dateFormat: "yy/mm/dd",
                changeMonth: true,
                changeYear: true
            }
        );
    }

    $(".datePicker").css("direction", "ltr");
});