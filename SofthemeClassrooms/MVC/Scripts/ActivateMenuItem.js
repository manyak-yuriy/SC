function ActivateMenu(menuItemId)
{
    menuItemId = '#' + menuItemId;
    $(menuItemId).addClass("menu-item-active");
}