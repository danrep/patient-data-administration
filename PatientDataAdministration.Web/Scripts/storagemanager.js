function getUserData() {
    return getCommonDataStore("sec_cred");
}
function storeUserData(userData) {
    setCommonDataStore("sec_cred", JSON.stringify(userData));
}


function getCommonDataStore(key)
{
    return JSON.parse(localStorage[key]);
}
function setCommonDataStore(key, data) {
    window.localStorage.setItem(key, JSON.stringify(data));
}