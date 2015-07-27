(function () {
    $('#LoginButton').click(function (e) {
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
    });
})();