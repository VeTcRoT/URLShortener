$('document').ready(function () {
    $('.add-form').submit(function (event) {
        event.preventDefault();
        deleteValidationErrors();

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: {
                url: $('.add-url').val()
            },
            success: function (data) {
                if (data.success) {
                    var baseUrl = window.location.origin

                    $('.table tbody').prepend(
                        `<tr>
                            <td>
                                <a target="_blank" href="${data.urlData.originalUrl}">${data.urlData.originalUrl.length > 40 ? data.urlData.originalUrl.substr(0, 40) + '...' : data.urlData.originalUrl}</a>
                            </td>
                            <td>
                                <a target="_blank" href="${baseUrl + '/' + data.urlData.shortUrl}">${baseUrl + '/' + data.urlData.shortUrl}</a>
                            </td>
                            <td>
                                <a class="btn btn-outline-primary" href="/Home/Details/${data.urlData.id}">View Details</a>
                                <a class="btn btn-outline-danger delete-link" href="/Home/Delete/${data.urlData.id}">Delete</a>
                            </td>
                        </tr>`
                    );
                }
                else {
                    var addErrorsContainer = $('.errors-wrap');
                    showValidationErrors(addErrorsContainer, data.errors);
                }
            }
        });
    });

    $('.table').on('click', '.delete-link', function (e) {
        e.preventDefault();
        deleteValidationErrors();
        var url = $(this).attr('href');
        if (confirm('Are you sure you want to delete this record?')) {
            $.post(url, function (data) {
                if (data.success) {
                    $(`a[href='${url}']`).parent().parent().remove()
                }
                else {
                    var addErrorsContainer = $('.errors-wrap');
                    showValidationErrors(addErrorsContainer, data.errors);
                }
            });
        }
    });

    $('.delete-link-single').click(function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        if (confirm('Are you sure you want to delete this record?')) {
            $.post(url, function (data) {
                if (data.success) {
                    window.location.replace(window.location.origin);
                }
                else {
                    var addErrorsContainer = $('.errors-wrap');
                    showValidationErrors(addErrorsContainer, data.errors);
                }
            });
        }
    });

    function deleteValidationErrors() {
        $('.errors-wrap').empty();
    }

    function showValidationErrors(errorContainer, errors) {
        errorContainer.empty();
        if (Array.isArray(errors)) {
            $.each(errors, function (index, error) {
                var errorMessage = $('<span class="text-danger"></span>').text(error);
                errorContainer.append(errorMessage);
            });
        } else {
            var errorMessage = $('<span class="text-danger"></span>').text(errors);
            errorContainer.append(errorMessage);
        }
    }
});