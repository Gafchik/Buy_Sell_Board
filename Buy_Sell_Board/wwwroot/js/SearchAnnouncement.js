function Search() {
    $.ajax({
        type: "POST",// тип запроса пост или гет
        async: true,// асинхронный запрос
        url: '/api/Announcements',// так выглядит путь к апи контроллеру без слова контроллер в названии
        contentType: "application/json; charset=utf-8", // тип передаваемого контента и кодировка
        dataType: "json",// тут понятно
        data: JSON.stringify({//парсим в json сохраняеть в ключь значение
            Name: $("#Name").val(),
            CategoryID: $("#CategoryActionsList").find(":selected").val(),
            SubCategoryID: $("#SubcategoryActionsList").find(":selected").val()
        }),
        // работа с токенами ЭТО НАДО!!!
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (rezult) {    // ответ сервера 
            $("#Rezult").empty();// очищаем обновляемый список
            var q = Array.from(rezult); // парсим json в arr
            if (q.length === 0) {
                alert("Ничего не найдено");
                return;
            }
            for (var i = 0; i < q.length; i++)
            {        // цикл  и добавление    // пропы в json приходят с первой буквой мелкой
                $("#Rezult").append(
                    '<div class="card" style="width: 18rem;">'+
                       //картинка
                    '<div id="carouselExampleControls" class="carousel slide" data-ride="carousel">'+
                    '<div class="carousel-inner" id="carousel-inner-' + (q[i].id)+'">'+
                            '<div class="carousel-item active">'+
                                '<img class="d-block w-100" src="' + (q[i].img_url[0].path)+'" alt="Первый слайд">'+
                            '</div>'+                                                                             
                        '</div>'+
                        '<a class="carousel-control-prev" href="#carouselExampleControls" role="button" data-slide="prev">'+
                            '<span class="carousel-control-prev-icon" aria-hidden="true"></span>'+
                            '<span class="sr-only">Previous</span>'+
                        '</a>'+
                        '<a class="carousel-control-next" href="#carouselExampleControls" role="button" data-slide="next">'+
                            '<span class="carousel-control-next-icon" aria-hidden="true"></span>'+
                            '<span class="sr-only">Next</span>'+
                        '</a>'+
                    '</div>'+
                    //Конец картинки
                        '<div class="card-body">'+
                            '<h5 class="card-title">' + (q[i].product_Name)+'</h5>'+
                            '<p class="card-text">' + (q[i].description)+'</p>'+
                        '</div>'+
                        '<ul class="list-group list-group-flush">'+
                            '<li class="list-group-item">' + 'Продавец : ' + (q[i].user_Name)+'</li>'+
                            '<li class="list-group-item">' + 'Категория : ' + (q[i].category) +'</li>'+
                            '<li class= "list-group-item">' +' Подкатегория : ' + (q[i].subcategory) +'</li>'+
                            '<li class= "list-group-item">' +' Модель : ' + (q[i].product_Model) +'</li>'+
                            '<li class= "list-group-item">' + ' Цена : ' + (q[i].price) + ' грн'+'</li>'+
                        '</ul>'+
                        '<div class= "card-body">'+
                            '<a href = "#" class= "card-link" > Ссылка карты</a>'+
                            '<a href = "#" class= "card-link" > Другая ссылка</a>'+
                       '</div>'+
                   '</div>'
                );
                for (var j = 1; j < q[i].img_url.length; j++){
                    $("#carousel-inner-" + (q[i].id)).append(
                        '<div class="carousel-item">'+
                        '<img class="d-block w-100" src="' + (q[i].img_url[j].path)+'" alt="Второй слайд">'+
                        '</div>'
                    )
                 }

                
            }
          
        },
        error: function () { //вункция при ошибке
            alert("НЕЕЕЕЕЕЕТ");
        }
    });
}
function Clear() {
    $("#Rezult").empty();// очищаем обновляемый список
}