var timetable;
    var renderer;

    var firstLoad = true;

    // Calendar-related scripts


    // create jqxcalendar.
    $("#jqxWidget").jqxCalendar({ width: 220, height: 220 });


    $("#jqxWidget").jqxCalendar({ culture: 'ru-RU' });

    // set custom left and right arrows for calendar view
    $('.jqx-icon-arrow-left').append('<i class="fa fa-caret-left fa-2x" aria-hidden="true"></i>');
    $('.jqx-icon-arrow-right').append('<i class="fa fa-caret-right fa-2x" aria-hidden="true"></i>');

    // set yesterday in calendar
    $('#date-scroller .fa-caret-left').click(function () {
        var calDate = $('#jqxWidget').jqxCalendar('getDate');
        calDate.setDate(calDate.getDate() - 1);
        $('#jqxWidget').jqxCalendar('setDate', calDate);
    });

    // set tomorrow in calendar
    $('#date-scroller .fa-caret-right').click(function () {
        var calDate = $('#jqxWidget').jqxCalendar('getDate');
        calDate.setDate(calDate.getDate() + 1);
        $('#jqxWidget').jqxCalendar('setDate', calDate);
    });

// get left to the current month

    $('#leftTodayInCalendar').click(function () {
        $('#jqxWidget').jqxCalendar('setDate', new Date());
    });
// get right to the current month
    $('#rightTodayInCalendar').click(function () {
        $('#jqxWidget').jqxCalendar('setDate', new Date());
    });


    // Calendar state changed
    $('#jqxWidget').on('change viewChange', function (event) {

        var date = event.args.date;

        // Load event data into TimeTable

        $.ajax({
            url: $(this).data('req-url'),
            type: "GET",
            data: { daySelected: date.toISOString() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus, jqXHR) {
                //alert(JSON.stringify(data));

                renderTimeTable(data);
            }
        });


        var days = ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'];
        // Change the state of upper scroller
        $('#date-scroller-value').text(date.getDate() + ", " + days[date.getDay()]);


        var selectedMonth = date.getMonth();
        var selectedYear = date.getYear();
        var currMonth = new Date().getMonth();
        var currYear = new Date().getYear();

        // show or hide a link to current day's date
        if (selectedMonth == currMonth && selectedYear == currYear)
        {
            $('#TodayInCalendar').css("display", "inline-block");
            $('#leftTodayInCalendar').css("display", "none");
            $('#rightTodayInCalendar').css("display", "none");
        }
        else
            if (date > new Date())
        {
            $('#TodayInCalendar').css("display", "none");
            $('#leftTodayInCalendar').css("display", "inline-block");
            $('#rightTodayInCalendar').css("display", "none");
        }
        else
            if (date < new Date())
        {
            $('#TodayInCalendar').css("display", "none");
            $('#leftTodayInCalendar').css("display", "none");
            $('#rightTodayInCalendar').css("display", "inline-block");
        }
    });

    // Get back today on calendar
    $('#TodayInCalendar').click(function () {
        $('#jqxWidget').jqxCalendar('setDate', new Date());
    });


    $('#TodayInCalendar').click(function () {
        $('#jqxWidget').jqxCalendar('setDate', new Date());
    });

    $('#TodayInCalendar').click(function () {
        $('#jqxWidget').jqxCalendar('setDate', new Date());
    });


    // Set today for calendar after page load
    $('#jqxWidget').jqxCalendar('setDate', new Date());


    // Toggle calendar
    $('.fa-list').click(function () {
        $('.fa-align-justify').css('opacity', 0.15);
        $(this).css('opacity', 1);
        $("#jqxWidget").css("display", "inline-block");
    });

    $('.fa-align-justify').click(function () {
        $('.fa-list').css('opacity', 0.15);
        $(this).css('opacity', 1);
        $("#jqxWidget").css("display", "none");
    });