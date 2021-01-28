function setCookie(key, value) {
    localStorage.setItem(key, value);
}

function getCookie(key) {
    var value = localStorage.getItem(key);
    if (!value)
        value = "";
    return value;
}

function clearCookie(key) {
    localStorage.removeItem(key);
}