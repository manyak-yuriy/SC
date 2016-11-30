var resizeOptions = {
    autoHide: true, ghost: true
}

$(  function() 
{
    $(".draggable").draggable();

    $(".resizeable").resizable(resizeOptions);
})