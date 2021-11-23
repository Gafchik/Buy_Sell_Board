function changeCat() {
    $.ajax({
        type: "POST",// тип апроса пост или гет
        url: '/Сategory/OnPostSubCat',// так выглядит путь к апи контроллеру без слова контроллер в названии
        contentType: "application/json; charset=utf-8", // тип передаваемого контента и кодировка
        dataType: "json",// тут понятно
        data: JSON.stringify({//парсим в json сохраняеть в ключь значение
            SelectedValue: $("#CategoryActionsList").find(":selected").val()
        }),
        // работа с токенами ЭТО НАДО!!!
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (rezult) {    // ответ сервера                                                   
            $("#SubcategoryActionsList").empty();// очищаем обновляемый список
            var q = Array.from(rezult); // парсим json в arr
            for (var i = 0; i < q.length; i++) {        // цикл  и добавление опшынов               
                $("#SubcategoryActionsList").append(  // пропы в json приходят с первой буквой мелкой
                    '<option value="' + q[i].id + '">' + (q[i].name) + '</option>');
            }
        },
        error: function () { //вункция при ошибке
            alert("НЕЕЕЕЕЕЕТ");
        }
    });
}
//загружает категории при первом гет запосе

    $.ajax({
        type: "POST",// тип апроса пост или гет
        async: true, // асинхронный запрос     
        url: '/Сategory/OnPostFirstCat',
        contentType: "application/json; charset=utf-8", // тип передаваемого контента и кодировка
        dataType: "json",// тут понятно
        // работа с токенами ЭТО НАДО!!!
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (rezult) {    // ответ сервера                                                   
            $("#CategoryActionsList").empty();// очищаем обновляемый список
            var q = Array.from(rezult); // парсим json в arr
            for (var i = 0; i < q.length; i++) {        // цикл  и добавление опшынов               
                $("#CategoryActionsList").append(
                    '<option value="' + q[i].id + '">' + (q[i].name) + '</option>');
            }
        },
        error: function () { //вункция при ошибке
            alert("НЕЕЕЕЕЕЕТ");
        }
    });


//загружает подкатегории при первом гет запосе

    $.ajax({
        type: "POST",// тип апроса пост или гет
        async: true,// асинхронный запрос   
        url: '/Сategory/OnPostFirstSubCat',
        contentType: "application/json; charset=utf-8", // тип передаваемого контента и кодировка
        dataType: "json",// тут понятно          
        // работа с токенами ЭТО НАДО!!!
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (rezult) {    // ответ сервера                                                   
            $("#SubcategoryActionsList").empty();// очищаем обновляемый список
            var q = Array.from(rezult); // парсим json в arr
            for (var i = 0; i < q.length; i++) {        // цикл  и добавление опшынов               
                $("#SubcategoryActionsList").append(
                    '<option value="' + q[i].id + '">' + (q[i].name) + '</option>');
            }
        },
        error: function () { //вункция при ошибке
            alert("НЕЕЕЕЕЕЕТ");
        }
    });
