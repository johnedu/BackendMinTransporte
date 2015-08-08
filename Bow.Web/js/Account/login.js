(function () {
    $('#LoginButton').click(function (e) {
        $('#LoginButton').prop('disabled', true);
        e.preventDefault();
        
        abp.ui.setBusy(
            $('#LoginArea'),
            abp.ajax({
                url: abp.appPath + 'Account/Login',
                type: 'POST',
                data: JSON.stringify({
                    username: $('#userInput').val(),
                    password: $('#passwordInput').val(),
                    rememberMe: $('#rememberMeInput').is(':checked')
                })
            })
        );

        abp.message.error = function (message, title) {
            abp.notify.error(message, title);
            $('#LoginButton').prop('disabled', false);
        };
    });
})();