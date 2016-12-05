
//确认退款
function CheckRefund(ReturnsId, RefundMoney, Comments, ProductId, OrderId,Status) {
     
    $("#ctl00_contentHolder_lblReturnsId").html(ReturnsId);
    $("#ctl00_contentHolder_hidReturnsId").val(ReturnsId);
    $("#ctl00_contentHolder_hidProductId").val(ProductId);
    $("#ctl00_contentHolder_hidOrderId").val(OrderId);
    $("#ctl00_contentHolder_hidStatus").val(Status);
    setArryText('ctl00_contentHolder_txtAdminRemark', '');
    setArryText('ctl00_contentHolder_lblOrderTotal', '');
    $("#ctl00_contentHolder_lblOrderTotal").val(RefundMoney);
                $("#ctl00_contentHolder_lblRefundRemark").html(Comments);
                ShowMessageDialog("确认退款", "checkrefund", "CheckRefund");
                
}
//审核
function CheckAuditRefund(ReturnsId, RefundMoney, Comments, ProductId, OrderId) {

    $("#ctl00_contentHolder_lblAuditReturnsId").html(ReturnsId);
    $("#ctl00_contentHolder_hidReturnsId").val(ReturnsId);
    $("#ctl00_contentHolder_hidProductId").val(ProductId);
    $("#ctl00_contentHolder_hidOrderId").val(OrderId);
    setArryText('ctl00_contentHolder_txtAdminRemark', '');
    setArryText('ctl00_contentHolder_lblAuditOrderTotal', '');
    $("#ctl00_contentHolder_lblAuditOrderTotal").val(RefundMoney);
    $("#ctl00_contentHolder_lblAuditRefundRemark").html(Comments);
    ShowMessageDialog("审核", "checkrefund", "CheckAudit");
}



//确认退货
function CheckReturn(orderId) {
    arrytext = null;
    $("#ctl00_contentHolder_return_lblOrderId").html(orderId);
    $("#ctl00_contentHolder_hidOrderId").val(orderId);
    setArryText('ctl00_contentHolder_return_txtRefundMoney', '');
    setArryText('ctl00_contentHolder_return_txtAdminRemark', '');
    $.ajax({
        url: "ManageOrder.aspx?type=return&orderId=" + orderId,
        type: 'post', dataType: 'json', timeout: 10000,
        data: {
            isCallback: "true"
        },
        async: false,
        success: function (resultData) {
            if (resultData.Status == "1") {
                $("#ctl00_contentHolder_return_lblOrderTotal").html(resultData.OrderTotal);
                $("#ctl00_contentHolder_return_lblRefundType").html(resultData.RefundTypeStr);
                $("#ctl00_contentHolder_hidRefundType").val(resultData.RefundType);
                $("#ctl00_contentHolder_return_lblContacts").html(resultData.Contacts);
                $("#ctl00_contentHolder_return_lblReturnRemark").html(resultData.Remark);
                $("#ctl00_contentHolder_return_lblEmail").html(resultData.Email);
                $("#ctl00_contentHolder_return_lblTelephone").html(resultData.Telephone);
                $("#ctl00_contentHolder_return_lblAddress").html(resultData.Address);
                ShowMessageDialog("确认退货", "checkreturn", "CheckReturn");
            }
        }

    });
}
//确认换货
function CheckReplace(orderId) {
    arrytext = null;
    $("#ctl00_contentHolder_replace_lblOrderId").html(orderId);
    $("#ctl00_contentHolder_hidOrderId").val(orderId);
    setArryText('ctl00_contentHolder_replace_txtAdminRemark', '');
    $.ajax({
        url: "ManageOrder.aspx?type=replace&orderId=" + orderId,
        type: 'post', dataType: 'json', timeout: 10000,
        data: {
            isCallback: "true"
        },
        async: false,
        success: function (resultData) {
            if (resultData.Status == "1") {
                $("#ctl00_contentHolder_replace_lblOrderTotal").html(resultData.OrderTotal);
                $("#ctl00_contentHolder_replace_lblComments").html(resultData.Comments);
                $("#ctl00_contentHolder_replace_lblContacts").html(resultData.Contacts);
                $("#ctl00_contentHolder_replace_lblEmail").html(resultData.Email);
                $("#ctl00_contentHolder_replace_lblTelephone").html(resultData.Telephone);
                $("#ctl00_contentHolder_replace_lblAddress").html(resultData.Address);
                $("#ctl00_contentHolder_replace_lblPostCode").html(resultData.PostCode);
                ShowMessageDialog("确认换货", "checkreplace", "CheckReplace");
            }
        }

    });
}
function auditRefund() {

    $("#ctl00_contentHolder_hidAuditM").val($("#ctl00_contentHolder_lblAuditOrderTotal").val());
    $("#ctl00_contentHolder_btnAuditAcceptRefund").trigger("click");
}
function auditrefuseRefund() {
    $("#ctl00_contentHolder_hidAuditM").val($("#ctl00_contentHolder_lblAuditOrderTotal").val());
    $("#ctl00_contentHolder_btnAuditRefuseRefund").trigger("click");
}
function acceptRefund() {
        var adminRemark = $("#ctl00_contentHolder_txtAdminRemark").val();
        $("#ctl00_contentHolder_hidAdminRemark").val(adminRemark);
        $("#ctl00_contentHolder_hidRefundM").val($("#ctl00_contentHolder_lblOrderTotal").val());
        $("#ctl00_contentHolder_btnAcceptRefund").trigger("click");
}
function refuseRefund() {
    var adminRemark = $("#ctl00_contentHolder_txtAdminRemark").val();
    $("#ctl00_contentHolder_hidRefundM").val($("#ctl00_contentHolder_lblOrderTotal").val());
    $("#ctl00_contentHolder_hidAdminRemark").val(adminRemark);
    $("#ctl00_contentHolder_btnRefuseRefund").trigger("click");
}
function acceptReplace() {
    var adminRemark = $("#ctl00_contentHolder_replace_txtAdminRemark").val();
    $("#ctl00_contentHolder_hidAdminRemark").val(adminRemark);
    $("#ctl00_contentHolder_btnAcceptReplace").trigger("click");
}
function refuseReplace() {
    var adminRemark = $("#ctl00_contentHolder_replace_txtAdminRemark").val();
    $("#ctl00_contentHolder_hidAdminRemark").val(adminRemark);
    $("#ctl00_contentHolder_btnRefuseReplace").trigger("click");
}
function acceptReturn() {
    var refundMoney = $("#ctl00_contentHolder_return_txtRefundMoney").val();
    $("#ctl00_contentHolder_hidRefundMoney").val(refundMoney);
    var adminRemark = $("#ctl00_contentHolder_return_txtAdminRemark").val();
    $("#ctl00_contentHolder_hidAdminRemark").val(adminRemark);
    var orderTotal = $("#ctl00_contentHolder_return_lblOrderTotal").html();
    $("#ctl00_contentHolder_hidOrderTotal").val(orderTotal);
    $("#ctl00_contentHolder_btnAcceptReturn").trigger("click");
}
function refuseReturn() {
    var adminRemark = $("#ctl00_contentHolder_return_txtAdminRemark").val();
    $("#ctl00_contentHolder_hidAdminRemark").val(adminRemark);
    $("#ctl00_contentHolder_btnRefuseReturn").trigger("click");
}

//验证
function validatorForm() {
   
    return true;
}