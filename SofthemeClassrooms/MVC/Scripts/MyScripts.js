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
