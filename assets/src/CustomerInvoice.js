$(function () {
    $('#CustomerId').change(function () {
        var Object = Manager.GetAllData("/Product/ProductsAsync", null);
        if (Object) {
            $("#SolarUnitPrice").val(Object[0].SaleUnitPrice);
            $("#Oil80UnitPrice").val(Object[1].SaleUnitPrice);
            $("#Oil92UnitPrice").val(Object[2].SaleUnitPrice);
            $("#Oil95UnitPrice").val(Object[3].SaleUnitPrice);
        }
        else {
            $("#SolarUnitPrice").val(0);
            $("#Oil80UnitPrice").val(0);
            $("#Oil92UnitPrice").val(0);
            $("#Oil95UnitPrice").val(0);
        }

    });
    $('#SolarQTY').keyup(function () {
        var SolarQty = $("#SolarQTY").val();
        var SolarUnitPrice = $("#SolarUnitPrice").val();
        var Total = SolarQty * SolarUnitPrice;
        $("#SolarTotalPrice").val(Total);
        CalcCustomerInvoice(); 
    });

    $('#Oil80QTY').keyup(function () {
        var SolarQty = $("#Oil80QTY").val();
        var SolarUnitPrice = $("#Oil80UnitPrice").val();
        var Total = SolarQty * SolarUnitPrice;
        $("#Oil80TotalPrice").val(Total);
        CalcCustomerInvoice(); 
    });

    $('#Oil92QTY').keyup(function () {
        var SolarQty = $("#Oil92QTY").val();
        var SolarUnitPrice = $("#Oil92UnitPrice").val();
        var Total = SolarQty * SolarUnitPrice;
        $("#Oil92TotalPrice").val(Total);
        CalcCustomerInvoice(); 
    });

    $('#Oil95QTY').keyup(function () {
        var SolarQty = $("#Oil95QTY").val();
        var SolarUnitPrice = $("#Oil95UnitPrice").val();
        var Total = SolarQty * SolarUnitPrice;
        $("#Oil95TotalPrice").val(Total);
        CalcCustomerInvoice();
    });

    

});
 function CalcCustomerInvoice() {
    var TotalPrice = parseFloat($("#SolarTotalPrice").val()) + parseFloat($("#Oil80TotalPrice").val()) +
        parseFloat($("#Oil92TotalPrice").val())
        + parseFloat($("#Oil95TotalPrice").val());
    $("#TotalPrice").val(TotalPrice);
}
