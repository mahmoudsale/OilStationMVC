$(function () {
    $('#CompanyId').change(function () {
        var Object = Manager.GetAllData("/Product/ProductsAsync",null);
        if (Object) {
            $("#SolarUnitPrice").val(Object[0].BuyUnitPrice);
            $("#Oil80UnitPrice").val(Object[1].BuyUnitPrice);
            $("#Oil92UnitPrice").val(Object[2].BuyUnitPrice);
            $("#Oil95UnitPrice").val(Object[3].BuyUnitPrice);
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
        CalcCompanyInvoice();
        CalcNawloon();
    });

    $('#Oil80QTY').keyup(function () {
        var SolarQty = $("#Oil80QTY").val();
        var SolarUnitPrice = $("#Oil80UnitPrice").val();
        var Total = SolarQty * SolarUnitPrice;
        $("#Oil80TotalPrice").val(Total);
        CalcCompanyInvoice();
        CalcNawloon();
    });

    $('#Oil92QTY').keyup(function () {
        var SolarQty = $("#Oil92QTY").val();
        var SolarUnitPrice = $("#Oil92UnitPrice").val();
        var Total = SolarQty * SolarUnitPrice;
        $("#Oil92TotalPrice").val(Total);
        CalcCompanyInvoice();
        CalcNawloon();
    });

    $('#Oil95QTY').keyup(function () {
        var SolarQty = $("#Oil95QTY").val();
        var SolarUnitPrice = $("#Oil95UnitPrice").val();
        var Total = SolarQty * SolarUnitPrice;
        $("#Oil95TotalPrice").val(Total);
        CalcCompanyInvoice();
        CalcNawloon();
    });

    $('#StockId').change(function () {
        var id = $("#StockId").val();
        var Parameters = {
            StockId: id
        }
        var stock = Manager.GetAllData("/Stocks/Stock",Parameters);
        $("#StockTransportUnit").val(stock.Cost);

    });
    
});

function CalcCompanyInvoice() {
    var TotalPrice = parseFloat($("#SolarTotalPrice").val()) + parseFloat($("#Oil80TotalPrice").val()) +
        parseFloat($("#Oil92TotalPrice").val())
        + parseFloat($("#Oil95TotalPrice").val());
    $("#TotalPrice").val(TotalPrice);
}

function CalcNawloon() {
    var TotalQTY = parseFloat($("#SolarQTY").val())
        + parseFloat($("#Oil80QTY").val())
        + parseFloat($("#Oil92QTY").val())
        + parseFloat($("#Oil95QTY").val());
    var TotalNawloon = (TotalQTY / 1000) * parseFloat($("#StockTransportUnit").val());
    $("#StockTransportAmount").val(TotalNawloon);
}