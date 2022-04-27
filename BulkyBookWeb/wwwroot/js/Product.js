﻿var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tbData').DataTable({
        "ajax": {
            "url" : "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title","width":"15%"},
            { "data": "isbn","width":"15%"},
            { "data": "price","width":"15%"},
            { "data": "author","width":"15%"},
            { "data": "category.name","width":"15%"},
        ]
    });
}