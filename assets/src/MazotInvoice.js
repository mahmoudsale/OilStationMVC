$(function () {
    $('#ChargeAreaId').change(function () {
        var id = $("#ChargeAreaId").val();
        var Parameters = {
            ChargeAreaId: id
        }
        var Object = Manager.GetAllData("/MazotInvoice/ChargeAreaUnitriceAsync", Parameters);
        if (Object) {
            $("#UnitPrice").val(Object.Cost);
            CalcMazotInvoice();
        }
        else {
            $("#UnitPrice").val(0);
            CalcMazotInvoice();
        } 
    });
    $('#Qty').keyup(function () {
        CalcMazotInvoice();
    });
    $('#Fee').keyup(function () {
        CalcMazotInvoice();
    });

    $('#Tax').keyup(function () {
        CalcMazotInvoice();
    });

    $('#CustomerId').change(function () {
        var id = $("#CustomerId").val();
        var Parameters = {
            CustomerId: id
        }
        var Object = Manager.GetAllData("/MazotInvoice/GetMazotCustomerUnitriceAsync", Parameters);
        if (Object) {
            $("#CustomerUnitPrice").val(Object.MazotUnitPrice);
            CalcCustomerMazotInvoice();
        }
        else {
            $("#CustomerUnitPrice").val(0);
            CalcCustomerMazotInvoice();
        }

    });

    $('#CustomerQty').keyup(function () {
        CalcCustomerMazotInvoice();
        CalcAreaTransportAmountMazotInvoice();
    });

    $('#CustomerFee').keyup(function () {
        CalcCustomerMazotInvoice();
    });
    $('#AreaTransportId').change(function () {
        var id = $("#AreaTransportId").val();
        var Parameters = {
            AreaTransportId: id
        }
        var AreaTransport = Manager.GetAllData("/MazotInvoice/AreaTransportUnitriceAsync", Parameters);
        $("#AreaTransportUnit").val(AreaTransport.Cost);
        CalcAreaTransportAmountMazotInvoice();

    });
    function CalcMazotInvoice() { 
        var Qty = parseFloat($("#Qty").val());
        var UnitPrice = parseFloat($("#UnitPrice").val());
        var Fee = parseFloat($("#Fee").val());
        var Tax = parseFloat($("#Tax").val());
        var QtyPrice = UnitPrice * Qty/1000;
        var TotalPrice = UnitPrice * Qty/1000 + Fee + Tax;

        $("#QtyPrice").val(QtyPrice);
        $("#TotalPrice").val(TotalPrice);  
    }

    function CalcCustomerMazotInvoice() {
        var CustomerQty = parseFloat($("#CustomerQty").val());
        var CustomerUnitPrice = parseFloat($("#CustomerUnitPrice").val());
        var CustomerFee = parseFloat($("#CustomerFee").val());
        var CustomerQtyPrice = CustomerQty * CustomerUnitPrice/1000;
        var CustomerTotalPrice = CustomerQty * CustomerUnitPrice/1000 + CustomerFee  ;

        $("#CustomerQtyPrice").val(CustomerQtyPrice);
        $("#CustomerTotalPrice").val(CustomerTotalPrice);  

    }
    function CalcAreaTransportAmountMazotInvoice() {
        var CustomerQty = parseFloat($("#CustomerQty").val());
        var AreaTransportUnit = parseFloat($("#AreaTransportUnit").val());
        var AreaTransportAmount = CustomerQty * AreaTransportUnit/1000;
        $("#AreaTransportAmount").val(AreaTransportAmount);
    }
});

