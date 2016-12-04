var posData = {};

var resizeOptions = {
                        autoHide: true, ghost: true
                    }

var loadPos = 
                function() 
                {

                  $.get("https://api.myjson.com/bins/19hhe", function(data, textStatus, jqXHR) {
                        //alert(textStatus);
                        posData = data;
                        //alert(JSON.stringify(posData));

                        $(".draggable").each( 
                            function() 
                            { 
                                //console.log($(this).text()); 
                                var key = $(this).attr("id") + $(this).hasClass('room-image');
                                

                                $(this).css("left", posData[key]["left"]);
                                $(this).css("top", posData[key]["top"]);

                                $img = $(this).find("img");
                                //$img.resizable( "destroy" ).width(posData[key]["width"]).resizable(resizeOptions); 
                                //$img.resizable( "destroy" ).height(posData[key]["height"]).resizable(resizeOptions);  
                            }
                        );


                  });
                  
                  console.log();
                }


var savePos = 
                function() 
                {
                  
                  $(".draggable").each( 
                    function() 
                    { 
                      //console.log($(this).text()); 
                        var key = $(this).attr("id") + $(this).hasClass('room-image');
                      posData[key] = {
                                         left: $(this).css("left"), 
                                         top: $(this).css("top"), 
                                         width: $(this).find("img").width(), 
                                         height: $(this).find("img").height()
                                     };
                    }
                  );

                   
                  var jsonData = JSON.stringify(posData);
/*
                  $.ajax({
                        url:"https://api.myjson.com/bins",
                        type:"POST",
                        data: jsonData,
                        contentType:"application/json; charset=utf-8",
                        dataType:"json",
                        success: function(data, textStatus, jqXHR){
                            var str = data["uri"];
                            var ind = str.lastIndexOf("/");
                            var id = str.substring(ind + 1); 
                            alert(id);
                        }
                  });
*/

                  $.ajax({
                        url:"https://api.myjson.com/bins/19hhe",
                        type:"PUT",
                        data: jsonData,
                        contentType:"application/json; charset=utf-8",
                        dataType:"json",
                        success: function(data, textStatus, jqXHR){
                            
                            //alert(jsonData);
                        }
                  });
                
                  console.log();
                }


$(  function() 
{
            loadPos();

            $(".draggable" ).draggable();

            $(".resizeable").resizable(resizeOptions);

            $('.line-details').css("display", "none");
            

            $(".room-image").mouseover(function () {
               
                $.ajax({
                    url: $('#prop-pane').data('request-url'),
                    type: "GET",
                    data: {roomId: 0},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    context: this,
                    success: function (data, textStatus, jqXHR) {

                        /*alert(textStatus);
                        alert(JSON.stringify(data));*/

                        if (data.SeatCount == 0)
                            $('.table').parent().css("display", "none");
                        else
                            $('.table').parent().css("display", "inline");

                        if (data.SeatCount > 1)
                            $('.table').html(data.SeatCount);
                        else
                            $('.table').html("");


                        if (data.BoardCount == 0)
                            $('.board').parent().css("display", "none");
                        else
                            $('.board').parent().css("display", "inline");

                        if (data.BoardCount > 1)
                            $('.board').html(data.BoardCount);
                        else
                            $('.board').html("");

                        if (data.LaptopCount == 0)
                            $('.laptop').parent().css("display", "none");
                        else
                            $('.laptop').parent().css("display", "inline");

                        if (data.LaptopCount > 1)
                            $('.laptop').html(data.LaptopCount);
                        else
                            $('.laptop').html("");

                        if (data.PrinterCount == 0)
                            $('.printer').parent().css("display", "none");
                        else
                            $('.printer').parent().css("display", "inline");

                        if (data.PrinterCount > 1)
                            $('.printer').html(data.PrinterCount);
                        else
                            $('.printer').html("");

                        if (data.ProjectorCount == 0)
                            $('.projector').parent().css("display", "none");
                        else
                            $('.projector').parent().css("display", "inline");

                        if (data.ProjectorCount > 1)
                            $('.projector').html(data.ProjectorCount);
                        else
                            $('.projector').html();

                        $('.line-details').css("display", "none");

                        $('.line-details').filter('#' + $(this).attr('id')).css("display", "block");

                        $('#prop-pane').css("display", "block");
                    }
                });
            });

            //$("#map" ).resizable(resizeOptions);

            $('#posSaver').click(
                () => { savePos(); }               
            );

            $('#posLoader').click(
                () => { loadPos(); }
            );

            $('#captionHide').click(
                () => {
                          $vis = 'hidden';
                          $(".caption").css('visibility', $vis); 
                      }
            );

   } 
);