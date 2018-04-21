function refreshDupData(url) {
    window.swalInfo("Your request has been received. This will take a while. Please Wait");

    window.api("GET",
        url,
        null,
        true,
        refreshPage);
}

function refreshPage() {
    setTimeout(function () {
        window.location.reload(true);
    }, 2000);
}