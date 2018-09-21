var handleDataTableDefault = function (table, scrollX = false) {
        0 !== table.length &&
            table.DataTable({
                responsive: true,
                scrollX: scrollX,
                ordering: [],
                aaSorting: []
            });
    },
    TableManageDefault = function () {
        return {
            init: function (table, scrollX = false) {
                handleDataTableDefault(table, scrollX);
            }
        }
    }();