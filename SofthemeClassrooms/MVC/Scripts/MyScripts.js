function ActivateMenu(menuItemId)
{
    menuItemId = '#' + menuItemId;
    $(menuItemId).addClass("menu-item-active");
}

function PasswordChangedSuccessfull() {
    $('#is-password-changed').html('<i class=\"fa fa-thumbs-o-up\" aria-hidden=\"true\"></i>Пароль успешно изменен');
    $('#oldpassword').text("");
    $('#newpassword').text("");
    $('#confirmpassword').text("");
}

function toggleDisplayAndInputPInfo()
{
    $('#piEditor').addClass("visibleBlock");
    $('#displayPersonalInfo').addClass("invisibleBlock");
}

function SendSuccess()
{
    $('#SuccessMessage').html('Спасибо. Ваше сообщение отправлено администратору.');
}

function SendChecker() {
    var searchInput = $('#user-search:input').val;
    if (searchInput == "") {
        $('#searchButton').prop('disabled', true);
    }
    else {
        $('#searchButton').prop('disabled', false);
    }
}

function LoadingStarted() {
    $('#inputs').css('display','none');
    $('#load-icon').css('display','initial');
}

function LoadingFinished() {
    $('#inputs').css('display', 'initial');
    $('#load-icon').css('display', 'none');
}

