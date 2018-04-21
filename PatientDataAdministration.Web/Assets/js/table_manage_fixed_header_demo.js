var handleDataTableFixedHeader = function () {
        "use strict";
        0 !== $("#data-table").length &&
            $("#data-table").DataTable({
                lengthMenu: [20, 40, 60, 100],
                fixedHeader: {
                    header: true,
                    headerOffset: $("#header").height()
                },
                responsive: true
            });
    },
    TableManageFixedHeader = function () {
        "use strict";
        return {
            init: function () {
                handleDataTableFixedHeader();
            }
        }
    }();