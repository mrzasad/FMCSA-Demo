AllVehicleList = {};
$(document).ready(function () {

    $('#ddlStates').select2({
        placeholder: "Please Select States",
        //theme: "bootstrap",
        //  width: '100%',
        tags: true,
        multiple: true,
        width: '100%',
        allowClear: true
    });

    $('#ddlVehicleType').select2({
        placeholder: "Please Select Vehicale Type",
        //theme: "bootstrap",
        //  width: '100%',
        tags: true,
        multiple: true,
        width: '100%',
        allowClear: true
    });
    $('#ddlStates').change(function () {
        AllVehicleList.table.draw();
    });
    $('#ddlVehicleType').change(function () {
        AllVehicleList.table.draw();
    }); 
    $("#ZipCode").change(function () {
        AllVehicleList.table.draw();
    });

    $("#Clear-data").click(function (e) {
        e.preventDefault();
        $('#ZipCode').val('');
        $("#ddlVehicleType").select2("val", " ");  
        $("#ddlStates").select2("val", " "); 
        AllVehicleList.table.draw();
    });
    AllVehicleList.table = $('#DTLoad').DataTable({
        dom: 'Bfrtip',
        select: {
            style: 'os',
            selector: 'td:first-child'
        },
        serverSide: true,
        paging: true,
        searching: true,
        ordering: false,
        "ajax": {
            url: '/Home/LoadDT',
            type: "get",
            //contentType: "application/json; charset=utf-8",
            //async: true, 
            data: function (data) {
                data.ZipCode = $('#ZipCode').val();//toDate($('#DateFrom').val().toString());
                data.VehicleType = $('#ddlVehicleType').val() !== null ? $('#ddlVehicleType').select2("val").join(',') : "0";
                data.States = $('#ddlStates').val() !== null ? $('#ddlStates').select2("val").join(',') : "0";
             
            }
        },
        buttons: true,
        columnDefs: [{
            orderable: false,
            targets: 0,
            "render": function (data, type, row, meta) {
                return row.companyName;
            }
        },
        {
            targets: 1,
            "render": function (data, type, row, meta) {
                return row.location;
            }
        },
        {
            targets: 2,
            "render": function (data, type, row, meta) {
                return row.dotNumber;
            }
        },
        {
            targets: 3,
            "render": function (data, type, row, meta) {
                return `<a  class="btn-sm m-1 text-white btn-info" target="_blank" rel="noopener noreferrer" data-ID='${row.vehicleInfoId}' data-link='${row.safetyLink}' href='${row.safetyLink}'> Safety Info</a>`;
            }
        }]
    });
});

$('#btnCopy').click(function (e) {
    e.preventDefault();
    $('.buttons-copy').trigger('click');

});
$('#btnCSV').click(function (e) {
    e.preventDefault();
    $('.buttons-csv').trigger('click');

});
$('#btnExcel').click(function (e) {
    e.preventDefault();
    $('.buttons-excel').trigger('click');

});
$('#btnPDF').click(function (e) {
    e.preventDefault();
    $('.buttons-pdf').trigger('click');

});
$('#btnPrint').click(function (e) {
    e.preventDefault();
    $('.buttons-print').trigger('click');

});