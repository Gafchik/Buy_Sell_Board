function Create() {
       
   
    var input = document.getElementById("file");
    var files = input.files;
    var formData = new FormData();
    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
   
    $.ajax({
        type: "post",// тип апроса пост или гет
        url: '/announcements/create',// так выглядит путь к апи контроллеру без слова контроллер в названии
        contenttype: "application/json; charset=utf-8", // тип передаваемого контента и кодировка
        datatype: "json",// тут понятно
        data: formData,
        /*data: json.stringify({//парсим в json сохраняеть в ключь значение
           // file: $("#file").prop("files"),
            file: formData,
            category: $("#categoryactionslist").find(":selected").val(),
            subcategory: $("#subcategoryactionslist").find(":selected").val(),
            product_name: $("#product_name").val(),
            product_moel: $("#product_moel").val(),
            product_dis: $("#product_dis").val(),
            product_price: $("#product_price").val()
        }),*/
        // работа с токенами это надо!!!
        beforesend: function (xhr) {
            xhr.setrequestheader("xsrf-token",
                $('input:hidden[name="__requestverificationtoken"]').val());
        },
        success: function (rezult) {    // ответ сервера    
            alert(rezult)         
        },
        error: function () { //вункция при ошибке
            alert("нееееееет");
        }
    });
}
