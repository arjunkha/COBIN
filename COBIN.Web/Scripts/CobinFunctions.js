
function GetStatesList(Country, divId) {
    var items;
    $.getJSON('/Transaction/GetStatesList/', { Country: Country }, function (data) {
        $.each(data, function (i, SatesList) {
            items += "<option value='" + SatesList.State + "'>" + SatesList.State + "</option>";
        });
        $(divId).html(items);
    });
}

function GetDistricList(Country, States, divId, isAll) {
    var items;

    isAll = (isAll === undefined) ? "n" : isAll;
    if (isAll == "y") {
        items += "<option value=''>ALL</option>";
    }

    $.getJSON('/Transaction/GetDistrictList/', { Country: Country, States: States }, function (data) {
        $.each(data, function (i, DistrictList) {
            items += "<option value='" + DistrictList.District + "'>" + DistrictList.District + "</option>";
        });
        $(divId).html(items);
    });
}

function GetBranchList(AgentId, divId) {
    var items;
    $.getJSON('/Report/GetBranchList/', { AgentId: AgentId }, function (data) {
        items = "<option value=''>All</option>";
        $.each(data, function (i, BranchList) {
            items += "<option value='" + BranchList.Value + "'>" + BranchList.Label + "</option>";
        });
        $(divId).html(items);
    });
}

function GetReceiverDetail(ReceiverId) {

    $.ajax({
        url: "GetCustomerDetail",
        data: {
            'ReceiverId': ReceiverId
        },
        dataType: "json",
        type: 'POST',
        success: function (data) {
            $.each(data, function (i, rslt) {
                $("#RName").val(rslt.RName);
                $("#RAddress").val(rslt.RAddress);
                $("#RPhone").val(rslt.RPhone);
                $("#RMobile").val(rslt.RMobile);
                $("#RState").val(rslt.RState);
                $("#RDistrict").val(rslt.RDistrict);

                $("#RCity").val(rslt.RCity);
                $("#RGender").val(rslt.RGender);
                $("#Relationship").val(rslt.Relationship);

                $("#RIDType").val(rslt.RIDType);
                $("#RIDNumber").val(rslt.RIDNumber);
                $("#RIDExpiryDate").val(rslt.RIDExpiryDate);
                $("#RIDIssuedPlace").val(rslt.RIDIssuedPlace);
                $("#Districts").val(rslt.RIDIssuedPlace);

                var IDType = rslt.RIDType.toLowerCase();
                if (IDType == "nepalese citizenship") {
                    $("#RIDIssuedPlace").prop('disabled', true);
                    $("#RIDIssuedPlace").hide();
                    $("#Districts").prop('disabled', false);
                    $("#Districts").show();

                }
                else {
                    $("#RIDIssuedPlace").prop('disabled', false);
                    $("#RIDIssuedPlace").show();
                    $("#Districts").prop('disabled', true);
                    $("#Districts").hide();
                }

            });

        },
        error: function () {
            alert("Technical Error Occurred While Processing Your Request");
        }
    });
}

function GetServiceCharge() {

    var SendAmt = $("#SendAmt").val();
    var PaymentMode = $("#PaymentMode").val();
    var PayoutCountry = $("#RCountry").val();
    var BankBranchId = $("#BankBranchId").val();
    var NewACRequest = $("#NewACRequest").is(":checked") ? "y" : null;


    $.ajax({
        url: "GetServiceCharge",
        data: {
            'SendAmt': SendAmt,
            'PaymentMode': PaymentMode,
            'PayCountry': PayoutCountry,
            'NewACRequest': NewACRequest,
            'BankBranchId': BankBranchId
        },
        dataType: "json",
        type: 'POST',
        success: function (rslt) {
            if (rslt.Code == "000") {
                $("#SendCharge").val(rslt.ServiceCharge);
                $("#ExchangeRate").val(rslt.ExchangeRate);
                $("#PayAmt").val(rslt.PayAmount);
                $("#CollectedAmt").val(rslt.CollectedAmount);
                $("#SendCurrency").val(rslt.SendCurrency);
                $("#PayCurrency").val(rslt.PayCurrency);
                $("#SendChargeCurr").val(rslt.SendCurrency);
                $("#CollectedAmtCurr").val(rslt.SendCurrency);
                $("#ExchangeRateCurr").val(rslt.PayCurrency);

            }
            else {
                alert(rslt.Message);
                $("#SendCharge").val('');
                $("#ExchangeRate").val('');
                $("#PayAmt").val('');
                $("#CollectedAmt").val('');
                $("#SendCurrency").val('');
                $("#PayCurrency").val('');
            }
        },
        error: function () {
            alert("Technical Error Occurred While Processing Your Request");
        }
    });
}

function ValidateRupaiyaCard(CardNumber, ReceiverName) {


    $('#CardNumberValidateMsg').html('<img src="../../Images/Icons/connect.gif""> Connecting...');
    $.ajax({
        url: "ValidateRupaiyaCard",
        data: {
            'CardNumber': CardNumber
        },
        dataType: "json",
        type: 'POST',
        success: function (rslt) {
            var ResponseCode = rslt.CODE;

            if (ResponseCode == "0") {

                $('#CardNumberValidateMsg').html("");
                var CardHolderName = rslt.CUSTOMER_NAME;
                var CardReferenceNo = "";


                if (ReceiverName.toLowerCase() != CardHolderName.toLowerCase()) {

                    $("#CardNumberValidateMsg").text("CardHolder Name: " + CardHolderName + " and ReceiverName do not match.");
                    return;
                }

                $("#CardNumber").prop("readonly", true);
                $("#RName").prop("readonly", true);
                $("#ReceiverId").prop("disabled", true);
                $("#CardReferenceNo").val(CardReferenceNo);
                $("#submit").prop('disabled', false);

                $("#CardNumberValidateMsg").text("Card Validation Successful.");
            }

            else {
                $("#CardNumberValidateMsg").text("ErrorCode:" + rslt.CODE + " Validation Unsuccessful." + "(" + rslt.MESSAGE + ")");
            }



        },
        error: function () {
            alert("Technical Error Occurred While Processing Your Request");
        }
    });
}