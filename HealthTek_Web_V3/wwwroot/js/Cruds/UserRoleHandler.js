function ShowRoles(itemId, ItemClass, name) {
    var url = "/" + ItemClass + "/UserRoles/" + itemId;
    $("#addeditModallabel").text("Manage User Roles")
    $(".modal-dialog").addClass("modal-xl")
    document.getElementById("modalSubHeading").innerHTML = "<code style='font-size: medium;font-weight: bolder;'>" + name.toUpperCase() + "</code> Roles";

    //Added backdrop attribute so clicking background does not close modal and resets data
    $("#addeditBody").load(url, function () {

        $("#addeditModal").modal({ backdrop: 'static' }, "show");
    });
}
