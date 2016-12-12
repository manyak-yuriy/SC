function checkTimeTableQuickNavState() {
    var scrollOffset = $(".scrollMe").smoothDivScroll("getScrollerOffset");
    var totalWidth = $('.scrollWrapper').width();
    var currTimeSliderleft = parseInt($('#time-now-wrapper').css('left'));


    $('#rightTodayInTimeTable').css('visibility', 'hidden');
    $('#leftTodayInTimeTable').css('visibility', 'hidden');

    // slider is hidden to the left side
    if (currTimeSliderleft < 0) {
        $('#leftTodayInTimeTable').css('visibility', 'visible');
    }
        // slider is hidden to the right side
    else if (currTimeSliderleft > totalWidth) {
        $('#rightTodayInTimeTable').css('visibility', 'visible');
    }
}

$('#leftTodayInTimeTable').click(function () {
    var currTimeSliderleft = parseInt($('#time-now-wrapper').css('left'));

    $(".scrollMe").smoothDivScroll("move", currTimeSliderleft - 30);

    setTimeout(function () { updateCurrentTime(); updateHours(); checkTimeTableQuickNavState(); }, 500);
});

$('#rightTodayInTimeTable').click(function () {
    var currTimeSliderleft = parseInt($('#time-now-wrapper').css('left'));

    $(".scrollMe").smoothDivScroll("move", currTimeSliderleft + 30);

    setTimeout(function () { updateCurrentTime(); updateHours(); checkTimeTableQuickNavState(); }, 500);
});