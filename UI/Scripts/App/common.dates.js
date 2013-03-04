$(document).ready(function () {
    $(".date").datepicker();
    $(".dateFrom").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 1,
        maxDate: $(".dateTo").val(),
        onClose: function (selectedDate) {
            $(".dateTo").datepicker("option", "minDate", selectedDate);
        }
    });
    $(".dateTo").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 1,
        minDate: $(".dateFrom").val(),
        onClose: function (selectedDate) {
            $(".dateFrom").datepicker("option", "maxDate", selectedDate);
        }
    });
});