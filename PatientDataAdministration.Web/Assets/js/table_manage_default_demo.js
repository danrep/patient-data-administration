var handleDataTableDefault = function (table, scrollX = false) {
        0 !== table.length &&
            table.DataTable({
                responsive: true,
                scrollX: scrollX
            });
    },
    TableManageDefault = function () {
        return {
            init: function (table, scrollX = false) {
                handleDataTableDefault(table, scrollX);
            }
        }
    }();