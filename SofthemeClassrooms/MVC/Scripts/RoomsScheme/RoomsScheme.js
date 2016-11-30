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

                        $('.table').html(data.SeatCount);
                        $('.board').html(data.BoardCount);
                        $('.laptop').html(data.LaptopCount);
                        $('.printer').html(data.PrinterCount);
                        $('.projector').html(data.ProjectorCount);

                        $('.line-details').css("display", "none");

                        $('.line-details').filter('#' + $(this).attr('id')).css("display", "block");
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