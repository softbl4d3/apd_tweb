$(function() {
    // Обработчик для кнопок редактирования
    $(document).on('click', '.details-button', function() {
        var dishId = $(this).data('dish-id');
        
        $.get('@Url.Action("GetDishDetails", "Admin")', { id: dishId }, function(data) {
            $('#modalDishName').text(data.Name);
            $('#modalDishDescription').val(data.Description);
            $('#modalDishIngredients').val(data.Ingredients);
            $('#modalDishImageUrl').val(data.ImageUrl);
            
            // Показываем превью изображения
            var preview = $('#dishImagePreview');
            if (data.ImageUrl) {
                preview.attr('src', data.ImageUrl).show();
            } else {
                preview.hide();
            }
            
            // Сохраняем ID блюда в модальном окне
            $('#detailsModal').data('dish-id', dishId);
            
            // Открываем модальное окно
            $('#detailsModal').modal('show');
        });
    });

    // Обработчик сохранения изменений
    $('#saveModalChanges').click(function() {
        var dishId = $('#detailsModal').data('dish-id');
        var data = {
            Id: dishId,
            Description: $('#modalDishDescription').val(),
            Ingredients: $('#modalDishIngredients').val(),
            ImageUrl: $('#modalDishImageUrl').val()
        };

        $.post('@Url.Action("SaveDishDetails", "Admin")', data, function(response) {
            if (response.success) {
                $('#detailsModal').modal('hide');
                // Можно обновить только измененные данные на странице без перезагрузки
                showToast('Изменения сохранены успешно');
            } else {
                showToast('Ошибка при сохранении: ' + response.message, 'error');
            }
        });
    });

    // Функция для показа уведомлений
    function showToast(message, type = 'success') {
        // Реализация toast-уведомлений
    }
});