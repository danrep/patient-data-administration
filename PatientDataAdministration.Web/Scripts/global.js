/*
 * Angular Declarations andn Initializations 
 */
var pdaWeb = angular.module("pdaWeb", []);
function getNgScope() {
    return angular.element(document.getElementById("content")).scope();
};

/*
 * Notification Declarations andn Initializations 
 */
function gritterConfirmCallback(data, callback) {
    /*
    gritter({
        text: 'Please confirm if you want to go ahead with the following operation?',
        layout: 'topRight',
        buttons: [
            {
                addClass: 'btn btn-success btn-sm',
                text: 'Okay',
                onClick: function($gritter) {
                    callback(data);
                    $gritter.close();
                }
            },
            {
                addClass: 'btn btn-danger btn-sm',
                text: 'Cancel',
                onClick: function($gritter) {
                    gritterDisplay('Operation Canceled', 'info');
                    $gritter.close();
                }
            }
        ]
    });
    */
};
function gritterDisplay(message, type) {
    //gritter({ text: message, layout: 'topRight', type: type, timeout: 3500 });
};
function gritterEx() {
    //gritter({ text: 'Oops! Something is not right. Please try again or contact Support.', layout: 'topRight', type: 'error', timeout: 3500 });
};
function gritterSuccess() {
    gritterDisplay("Great Job!", "success");
};

/*
 * API Abstraction
 */
function api(apiConnectType, url, data, asyncMode, callback) {

    $.ajax({
        type: apiConnectType,
        url: url,
        async: asyncMode,
        data: data,
        dataType: "json"
    })
    .success(function (remoteData) {
        if (remoteData.Status === true) {
            if (callback != null && typeof callback === "function")
                callback(remoteData.Data);
        } else {
            window.gritterDisplay(remoteData.status_message, "warning");
        }
    })
    .error(function () {
        window.gritterEx();
    });
};

/*
 * API Datatable Methods
 */
function initializeDataTable(table) {
    if (table.length > 0) {
        table.dataTable({ "ordering": [], "aaSorting": [] });
        table.on('page.dt', function () {
            onresize(100);
        });
    }
};
function resetDataTable(table) {
    table.dataTable().fnClearTable();
    table.dataTable().fnDestroy();
};
function reInitializeTable(table) {
    if ($.fn.DataTable.isDataTable(table)) {
        table.dataTable().fnDestroy();
    }
    TableManageDefault.init(table);
};

/*
 * API Form Operations
 */
function emptyForm(form) {
    form.find("input:text").val("");

    form.find(":input").each(function () {
        switch (this.type) {
            case "password":
            case "text":
            case "textarea":
            case "file":
            case "select":
            case "select-one":
            case "select-multiple":
            case "date":
            case "number":
            case "tel":
            case "email":
                jQuery(this).val("");
                break;
            case "checkbox":
            case "radio":
                this.checked = false;
                break;
        }
    });

    form.each(function () {
        $(this).val("");
    });
};
function resetFormData(form) {
    form.validationEngine("hide");
    emptyForm(form);
};

/*
 * Date Methods
 */
function formatDate(date) {
    return moment(date).format("DD-MM-YYYY");
}