BI = BI || {};
BI.UserAccount = function () {
  var outputMsg,
    utils = BI.Utils;

  function init (prms) {
    outputMsg = $(prms.outputMsgSelector);
  }

  var register = function (opts) {
    $.ajax({
      url: 'api/account/register',
      dataType: 'json',
      method: 'post',
      data: {
        UserName: $('.register #txtUserName').val(),
        Password: $('.register #txtPwd').val(),
        ConfirmPassword: $('.register #txtPwd').val()
      },
      success: function (data) {
        if (data != null) {
          console.log(data);

          if (opts) {
            if (opts.success) {
              opts.success(data);
            }
          }
        }
      },
      error: function (answer) {
        console.log(answer);
        outputMsg.text(answer.responseText);
      }
    });
  };

  var login = function (opts) {
    $.ajax({
      url: 'api/account/login',
      method: 'post',
      data: {
        UserName: $('.register #txtUserName').val(),
        Password: $('.register #txtPwd').val(),
        RememberMe: true
      },
      success: function (data) {
        if (data != null) {
          console.log(data);
          if (opts) {
            if (opts.success) {
              opts.success(data);
            }
          }
        }
      },
      error: function (answer) {
        console.log(answer);
        outputMsg.text(answer.responseText);
      }
    });
  };

  var logout = function (opts) {
    $.ajax({
      url: 'api/account/logout',
      method: 'post',
      headers: utils.getAuthHeaders(),
      success: function (data) {
        if (data != null) {
          console.log(data);
          if (opts) {
            if (opts.success) {
              opts.success(data);
            }
          }
        }
      },
      error: function (answer) {
        console.log(answer);
        outputMsg.text(answer.responseText);
      }
    });
  };

  return {
    Init: init,
    Login: login,
    Logout: logout,
    Register: register
  }
};