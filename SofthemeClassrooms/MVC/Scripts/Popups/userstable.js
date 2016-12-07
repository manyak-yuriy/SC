function DeleteUserPopupShow(id)
{
    id = '#' + id;
    $(id).addClass('b-popup-show');
}

function DeleteUserPopupHide(id)
{
    id = '#' + id;
    $(id).removeClass('b-popup-show');
}