
function IsEmptyInput(inputField) {
    var input = $(inputField).val();
    if (input.length < 1) {
        return true;
    }

    var j = 0;
    for (var i = 0; i < input.length; ++i) {
        if (input[i] === ' ') {
            ++j;
        }
    }
    return j === input.length;
}

function SearchChecker() {
    if (IsEmptyInput('#user-search')) {
        $('#searchButton').prop('disabled', true);
    }
    else {
        $('#searchButton').prop('disabled', false);
    }
}

function CheckFeedbackFormForContent() {
    var isFormFilled = IsEmptyInput('#Name') ||
        IsEmptyInput('#Surname') ||
        IsEmptyInput('#Email') ||
        IsEmptyInput('#messageText');
    if (isFormFilled) {
        $('#sendFeedback').prop('disabled', true);
    } else {
        $('#sendFeedback').prop('disabled', false);
    }
}
