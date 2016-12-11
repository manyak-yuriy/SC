
// fix hour and minute display format at startup
$('.hour-picker .date-custom-input').each(function (i, obj) {
    var value = $(obj).val();
    if (value < 10)
        value = "0" + value;
    $(obj).val(value);
});

$('.minute-picker .date-custom-input').each(function (i, obj) {
    var value = $(obj).val();
    if (value < 10)
        value = "0" + value;
    $(obj).val(value);
});


var monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
];

var monthNamesRus = ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
  "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
];


$('.month-picker .date-custom-input').val(monthNamesRus[new Date().getMonth()]);

$(".month-picker input[type='hidden']").val(new Date().getMonth());



// Submit form
$('.send-event-form-data').click(function () {

});


function isPrecedingCorrect() {
    var h1 = parseInt($('.hour-picker#first .date-custom-input').val());
    var h2 = parseInt($('.hour-picker#second .date-custom-input').val());

    var m1 = parseInt($('.minute-picker#first .date-custom-input').val());
    var m2 = parseInt($('.minute-picker#second .date-custom-input').val());
    //alert(h1 + ":" + m1 + " - " + h2 + ":" + m2)
    return (h2 > h1) || ((h2 == h1) && (m2 > m1));
}

// increment hours
$('.hour-picker .fa-caret-up').unbind().click(function () {
    var $hours = $(this).parent().children('.date-custom-input');
    var $hoursValOld = parseInt($hours.val());
    var $hoursVal = parseInt($hours.val()) + 1;
    if ($hoursVal == 24)
        $hoursVal = 0;

    var output = $hoursVal;
    if ($hoursVal < 10)
        output = "0" + output;
    $hours.val(output);
    if (!isPrecedingCorrect())
        $hours.val($hoursValOld);
});

// decrement hours
$('.hour-picker .fa-caret-down').unbind().click(function () {
    var $hours = $(this).parent().children('.date-custom-input');
    var $hoursValOld = parseInt($hours.val());
    var $hoursVal = parseInt($hours.val()) - 1;
    if ($hoursVal == -1)
        $hoursVal = 23;

    var output = $hoursVal;
    if ($hoursVal < 10)
        output = "0" + output;
    $hours.val(output);

    if (!isPrecedingCorrect())
        $hours.val($hoursValOld);
});

$('.month-picker .fa-caret-up').unbind().click(function () {
    $month = $(".month-picker input[type='hidden']");
    $monthVal = parseInt($month.val()) + 1;

    if ($monthVal == 12)
        $monthVal = 0;

    $('.month-picker .date-custom-input').val(monthNamesRus[$monthVal]);

    $month.val($monthVal);
});

$('.month-picker .fa-caret-down').unbind().click(function () {
    $month = $(".month-picker input[type='hidden']");
    $monthVal = parseInt($month.val()) - 1;

    if ($monthVal == -1)
        $monthVal = 11;

    $('.month-picker .date-custom-input').val(monthNamesRus[$monthVal]);

    $month.val($monthVal);
});


// increment minutes
$('.minute-picker .fa-caret-up').unbind().click(function () {
    var $minutes = $(this).parent().children('.date-custom-input');
    var $minutesValOld = parseInt($minutes.val());
    var $minutesVal = parseInt($minutes.val()) + 1;
    if ($minutesVal == 60)
        $minutesVal = 0;

    var output = $minutesVal;
    if ($minutesVal < 10)
        output = "0" + output;
    $minutes.val(output);

    if (!isPrecedingCorrect())
        $minutes.val($minutesValOld);
});

// decrement minutes
$('.minute-picker .fa-caret-down').unbind().click(function () {
    if (!isPrecedingCorrect())
        return;

    var $minutes = $(this).parent().children('.date-custom-input');
    var $minutesValOld = parseInt($minutes.val());
    var $minutesVal = parseInt($minutes.val()) - 1;
    if ($minutesVal == -1)
        $minutesVal = 59;

    var output = $minutesVal;
    if ($minutesVal < 10)
        output = "0" + output;
    $minutes.val(output);

    if (!isPrecedingCorrect())
        $minutes.val($minutesValOld);
});


$('.event-edit-header .fa-window-minimize').click(function () {
    //alert("min");
    $(this).parents('.event-edit-popup').remove();
})