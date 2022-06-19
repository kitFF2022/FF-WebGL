mergeInto(LibraryManager.library, {
    getToken: function () {
        var token = window.location.href;
        var tokenLen = lengthBytesUTF8(token) + 1;
        var buffer = _malloc(tokenLen);
        stringToUTF8(token, buffer, tokenLen);
        return token;
    },
});