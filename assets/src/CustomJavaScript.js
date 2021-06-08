function ConfirmDelete(UniqueId, IsDeleteClicked) {
    var DeleteSpan = "SpanDelete_"+ UniqueId;
    var ConfirmDeleteSpan = "SpanConfirmDelete_" + UniqueId;
    if (IsDeleteClicked) {
        $("#" + DeleteSpan).hide();
        $("#" + ConfirmDeleteSpan).show();
    }
    else {
        $("#" + DeleteSpan).show();
        $("#" + ConfirmDeleteSpan).hide();
    }

}

