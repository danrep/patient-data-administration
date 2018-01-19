/*
 * Angular Declarations andn Initializations 
 */
var pdaWeb = angular.module("pdaWeb", []);
function getNgScope() {
    return angular.element(document.getElementById("content")).scope();
};

/*
 * API Abstraction
 */
function api(apiConnectType, url, data, asyncMode, callback, feedBack = false) {

    $.ajax({
        type: apiConnectType,
        url: url,
        async: asyncMode,
        data: data,
        dataType: "json"
    })
    .success(function (remoteData) {
        if (remoteData.Status === true) {
            if (callback != null && typeof callback === "function") {
                if (feedBack) {
                    swalSuccess(remoteData.Message);
                    setTimeout(function () {
                        callback(remoteData.Data);
                    }, 2000);
                } else
                    callback(remoteData.Data);
            }
        } else {
            swalWarning(remoteData.Message);
        }
    })
    .error(function () {
        swalEx();
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
function reInitializeTable(table, scrollX = false) {
    if ($.fn.DataTable.isDataTable(table)) {
        table.dataTable().fnDestroy();
    }
    TableManageDefault.init(table, scrollX);
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

/*
 *  Charting Functions
 */
function lineChartOneToOne(chartElementName, arrayData, label, hoverCallBack) {
    var a = "#0D888B",
        b = "#00ACAC",
        c = "#3273B1",
        d = "#348FE2",
        e = "rgba(0,0,0,0.6)",
        f = "rgba(255,255,255,0.4)";

    Morris.Bar({
        element: chartElementName,
        data: arrayData,
        xkey: "x",
        ykeys: ["y"],
        labels: [label],
        lineColors: [a, c],
        xLabelMargin: 10,
        resize: !0,
        hoverCallback: hoverCallBack,
        gridTextFamily: "Open Sans",
        gridTextColor: f,
        gridTextWeight: "normal",
        gridTextSize: "11px",
        gridLineColor: "rgba(0,0,0,0.5)",
        hideHover: "auto"
    });
};
function donutChart(chartElementName, arrayData) {
    Morris.Donut({
        element: chartElementName,
        data: arrayData,
        labelFamily: "Open Sans",
        labelColor: "rgba(255,255,255,0.4)",
        labelTextSize: "11px",
        backgroundColor: "#242a30"
    });
}

/*
 * SWAL
 */
function swalWarning(message) {
    swalEngineNoCallBack("Wait a Minute...", message, "warning", "btn-warning");
};
function swalWarningConfirm(callBack) {
    swalEngineCallBack("Wait a Minute...", "This is a Sensitive Operation. Are you sure you want to Proceed", "warning", "btn-warning", callBack);
};
function swalInfo(message) {
    swalEngineNoCallBack("Just FYI", message, "info", "btn-default", false);
};
function swalError(message) {
    swalEngineNoCallBack("Oops. There seems to be a Problem", message, "error", "btn-danger");
};
function swalEx() {
    swalError("This is embarassing but something serious actually is wrong. Please check with Support");
};
function swalSuccess(message) {
    swalEngineNoCallBack("Awesome", message, "success", "btn-success", false);
};

function swalEngineNoCallBack(title, message, type, buttonClass, showConfirmButton = true) {
    if (showConfirmButton)
        swal({
            title: title,
            text: message,
            type: type,
            showCancelButton: false,
            confirmButtonClass: buttonClass,
            confirmButtonText: "Okay"
        });
    else
        swal({
            title: title,
            text: message,
            type: type,
            showCancelButton: false,
            showConfirmButton: showConfirmButton,
            timer: 2000
        });
};
function swalEngineCallBack(title, message, type, buttonClass, callback) {
    swal({
        title: title,
        text: message,
        type: type,
        showCancelButton: true,
        confirmButtonClass: buttonClass,
        confirmButtonText: "Okay"
    }).then((result) => {
        if (result.value) {
            callback();
        };
    });
};

// ReSharper disable once NativeTypePrototypeExtending
Number.prototype.format = function(n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};