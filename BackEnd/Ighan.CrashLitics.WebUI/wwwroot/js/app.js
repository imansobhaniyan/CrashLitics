function setCookie(key, value) {
    localStorage.setItem(key, value);
}

function getCookie(key) {
    var value = localStorage.getItem(key);
    if (!value)
        value = "";
    return value;
}