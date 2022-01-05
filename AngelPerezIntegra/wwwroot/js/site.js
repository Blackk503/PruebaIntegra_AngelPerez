$("#BuscarEmpleado").on('keyup', function (e) {
    if (e.key === 'Enter' || e.keyCode === 13) {
        var valor = $("#BuscarEmpleado").val();
        var url = "Empleado/LstEmpleadoPartial";
        var data = { valor: valor }
        debugger;
        $.post(url, data).done(function (r) {
            $("#TblEmpleados").html("");
            $("#TblEmpleados").html(r);
        })
    }
});