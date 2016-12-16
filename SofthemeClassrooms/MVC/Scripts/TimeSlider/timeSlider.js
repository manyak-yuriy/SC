// Time selection slider
hours = 0;
minutes = 0;

var updateCurrentTime =
    function (event, ui) {
        var hourScope = timetable.scope.hourEnd - timetable.scope.hourStart;
        var pxPerHour = $('section').width() / hourScope;

        var nowHours = new Date().getHours() - timetable.scope.hourStart;
        var nowMinutes = new Date().getMinutes();

        var scrollOffset = $(".scrollMe").smoothDivScroll("getScrollerOffset");

        var left = (nowHours + nowMinutes / 60) * pxPerHour;
        var totalWidth = $('.scrollWrapper').width();
        
        if (left < scrollOffset || left - scrollOffset > totalWidth)
            $('#time-now-wrapper').css('visibility', 'hidden');
        else
            $('#time-now-wrapper').css('visibility', 'visible');

        
        var calDate = $('#jqxWidget').jqxCalendar('getDate');
        // Hide if not current day is selected in calendar
        var nowDate = new Date();

        if (calDate.getDate() != nowDate.getDate() | calDate.getMonth() != nowDate.getMonth() || calDate.getYear() != nowDate.getYear())
            $('#time-now-wrapper').css('visibility', 'hidden');

        left = left - scrollOffset;

        hours = Math.floor(timetable.scope.hourStart + nowHours);
        var hourFract = nowHours - hours;
        minutes = new Date().getMinutes();

        //alert(nowHours + " : " + minutes);

        var prettyTime = ((hours < 10) ? "0" + hours : hours) + ":" + ((minutes < 10) ? "0" + minutes : minutes);

        $('#time-now-wrapper').css('left', left);
        $('#current-time').html(prettyTime);
    }



// put selected hours and minutes into global variables hours and minutes respectively
var updateHours =
    function (event, ui) {
        var hourScope = timetable.scope.hourEnd - timetable.scope.hourStart;
        var pxPerHour = $('section').width() / hourScope;

        var sliderPos = parseInt($("#custom-handle").css("left"));
        var scrollOffset = $(".scrollMe").smoothDivScroll("getScrollerOffset");

        var hoursElapsed = ((sliderPos + scrollOffset) / pxPerHour) + timetable.scope.hourStart;

        hours = Math.floor(hoursElapsed);
        var hourFract = hoursElapsed - hours;
        minutes = Math.floor(60 * hourFract);
        
        var prettyTime = ((hours < 10) ? "0" + hours : hours) + ":" + ((minutes < 10) ? "0" + minutes : minutes);

        var selectedTime = $('#jqxWidget').jqxCalendar('getDate');;
        selectedTime.setHours(hours, minutes);
        renderRoomTable(selectedTime);

        $('#selected-time').html(prettyTime);
    }