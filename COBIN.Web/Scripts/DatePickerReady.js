$(function () {
    $("#loading").hide();
});
$(document).ajaxStop(function () {
    $.validator.unobtrusive.parse('form');
});
$(document).ajaxError(function (e, xhr, settings) {
    if (xhr.status == 302) {
        window.location.replace("/");
    }
    else {
        alert("An error occurred! Please Try Refreshing the Page?");
    }
});
$(function () {
    try {
        $(".datefield").datepicker({
            dateFormat: 'yy-mm-dd',
            showOn: "button",
            changeMonth: true,
            changeYear: true,
            yearRange: "-100:+10",
            buttonImageOnly: true,
            buttonImage: "/Images/Icons/calendar-icon.png"
        });
        $.validator.addMethod(
    "date",
    function (value, element) {
        return $.validator.methods.dateISO.apply(this, arguments);
    },
    ""
    );
    }
    catch (err) {
        return;
    }
})




//this is just to display massegebox
//formname here is to post whole form(such as create,edit)
//if no formname ie.url is passed then redirect to the corresponding action(method)
function DisplayConfirmation(title, content, type, url, formname) {
    $.msgBox({
        title: "" + title + "",
        content: "<br />" + content + "",
        type: "" + type + "",
        success: function (result) {

            if (result == "Yes") {

                if (formname != '' && formname != undefined) {
                    formname.submit();
                }
                else {
                    window.location.replace(url)
                }

            }

        }
    });
}