var handleDataTableDefault = function (table) {
        "use strict";
        0 !== table.length && table.DataTable({
            responsive: !0
        })
    },
    TableManageDefault = function () {
        "use strict";
        return {
            init: function (table) {
                handleDataTableDefault(table);
            }
        }
    }();