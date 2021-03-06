﻿BI = BI || {};
BI.Utils = {
  //
  createCookie: function(name, value, days) {
    var expires;

    if (days) {
      var date = new Date();
      date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
      expires = "; expires=" + date.toGMTString();
    } else {
      expires = "";
    }
    document.cookie = escape(name) + "=" + escape(value) + expires + "; path=/";
  },
  //
  readCookie: function(name) {
    var nameEQ = escape(name) + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
      var c = ca[i];
      while (c.charAt(0) === ' ') c = c.substring(1, c.length);
      if (c.indexOf(nameEQ) === 0) return unescape(c.substring(nameEQ.length, c.length));
    }
    return null;
  },
  //
  eraseCookie: function(name) {
    createCookie(name, "", -1);
  },

  getAuthHeaders: function() {
    var headers = {};
    if (this.readCookie("bearerToken") != null) {
      headers.Authorization = "Bearer " + this.readCookie("bearerToken");
    }
    return headers;
  }
};