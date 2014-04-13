BI = BI || {};
BI.Booking  = function() {
  var ddlResources = $('#ddlResources'),
      lblCurrentMsg = $('.currentMsg'),
      utils = BI.Utils,
      userControl = new BI.UserAccount(),
      waitList = $('#waitlist');

  function getJsonAuth(url, options) {
    $.ajax(url, {
      dataType: 'json',
      method: 'get',
      headers: utils.getAuthHeaders(),
      success: options.success,
      error: options.error
    });
  }

  function postJsonAuth(url, options) {
    $.ajax(url, {
      dataType: 'json',
      method: 'post',
      headers: utils.getAuthHeaders(),
      success: options.success,
      error: options.error
    });
  }

  function refreshList() {
    waitList.html('');

    getJsonAuth('api/waitlist/getwaitinglist', {
      success: function (data) {
        $.each(data, function (key, item) {
          if (item != null) {
            $('<li>', {
              text: formatUserBooked(item)
            }).appendTo(waitList);
          }
        });
      },
      error: function (data) {
        if (data.status == '401') waitList.text('No access');
      }
    });
  }

  function formatUserBooked(item) {
    return item.User.UserName + ' booked ' + item.HubResource.Name + ' at ' + item.BookedTime;
  }

  function addMeToLine(resId) {
    postJsonAuth('api/waitlist/addtolist?resourceId=' + resId, {
      success: function (item) {
        lblCurrentMsg.text('');
        if (item.HasError) {
          lblCurrentMsg.text(item.ErrorMsg);
          return;
        }
        $('<li>', {
          text: formatUserBooked(item.Value)
        }).appendTo(waitList);
      },
      error: function (data) {
        if (data.status == '401') waitList.text('No access');
      }
    });
  }

  var getAllResourcesAndFillDropdown = function () {
    ddlResources.html('');
    getJsonAuth('api/resources/all', {
      success: function (data) {
        $.each(data, function (key, value) {
          ddlResources.append($('<option></option>')
            .attr('value', value.Id)
            .attr('title', value.Description)
            .text(value.Name));
        });
      }
    });
  };

  $('.register #btnRegister').click(userControl.Register);
  $('.register #btnLogin').click(userControl.Login);
  $('.register #btnLogout').click(userControl.Logout);
  $('#addUserToLine').click(function () {
    addMeToLine(ddlResources.val());
  });

  refreshList();
  getAllResourcesAndFillDropdown();
}

$(document).ready(function() {
  var control = new BI.Booking();
});