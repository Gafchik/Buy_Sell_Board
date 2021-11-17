function changeCat() {
    $.ajax({
        type: "POST",// тип апроса пост или гет
        url: '/Identity/Account/Manage/New_Announcement?handler=SubCut',// только полный путь
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
        url: '/Identity/Account/Manage/New_Announcement?handler=FirstCat',// только полный путь
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
        url: '/Identity/Account/Manage/New_Announcement?handler=FirstSubCat',// только полный путь
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
