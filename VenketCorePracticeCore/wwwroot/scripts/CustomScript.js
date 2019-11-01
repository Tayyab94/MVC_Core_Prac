function DeleteConfirm(uniqueID, isDeleted) {
    var deletespan = 'deletespan_' + uniqueID;

    var confirmDeleteSpan = 'confirmDeletespan_' + uniqueID;

    if (isDeleted) {
        $("#" + deletespan).hide();
        $("#" + confirmDeleteSpan).show();
    }
    else {
        $("#" + deletespan).show();
        $("#" + confirmDeleteSpan).hide();
    }
}