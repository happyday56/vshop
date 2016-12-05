$(document).ready(function () {

    var $0 = $("#ctl00_contentHolder_cbAll"); $0.attr("Privilege", "0");

    $0 = $("#ctl00_contentHolder_cbSummary"); $0.attr("Privilege", "99999");

    // 店铺管理
    $0 = $("#ctl00_contentHolder_cbShop"); $0.attr("Privilege", "10");
    $0 = $("#ctl00_contentHolder_cbSiteContent"); $0.attr("Privilege", "101");    
    $0 = $("#ctl00_contentHolder_cbPaymentModes"); $0.attr("Privilege", "104");
    $0 = $("#ctl00_contentHolder_cbShippingModes"); $0.attr("Privilege", "105");
    $0 = $("#ctl00_contentHolder_cbShippingTemplets"); $0.attr("Privilege", "106");
    $0 = $("#ctl00_contentHolder_cbExpressComputerpes"); $0.attr("Privilege", "107");
    $0 = $("#ctl00_contentHolder_cbPictureMange"); $0.attr("Privilege", "109");

    //页面管理
    $0 = $("#ctl00_contentHolder_cbPageManger"); $0.attr("Privilege", "11");
    $0 = $("#ctl00_contentHolder_cbVotes"); $0.attr("Privilege", "119");


    //商品管理
    $0 = $("#ctl00_contentHolder_cbProductCatalog"); $0.attr("Privilege", "12");
    $0 = $("#ctl00_contentHolder_cbManageProducts"); $0.attr("Privilege", "121");
    $0 = $("#ctl00_contentHolder_cbManageProductsView"); $0.attr("Privilege", "121_1");
    $0 = $("#ctl00_contentHolder_cbManageProductsAdd"); $0.attr("Privilege", "121_2");
    $0 = $("#ctl00_contentHolder_cbManageProductsEdit"); $0.attr("Privilege", "121_3");
    $0 = $("#ctl00_contentHolder_cbManageProductsDelete"); $0.attr("Privilege", "121_4");
    $0 = $("#ctl00_contentHolder_cbInStock"); $0.attr("Privilege", "121_5");
    $0 = $("#ctl00_contentHolder_cbManageProductsUp"); $0.attr("Privilege", "121_6");
    $0 = $("#ctl00_contentHolder_cbManageProductsDown"); $0.attr("Privilege", "121_7");

    $0 = $("#ctl00_contentHolder_cbProductUnclassified"); $0.attr("Privilege", "122");
    $0 = $("#ctl00_contentHolder_cbSubjectProducts"); $0.attr("Privilege", "123");
    $0 = $("#ctl00_contentHolder_cbProductBatchUpload"); $0.attr("Privilege", "124");
    $0 = $("#ctl00_contentHolder_cbProductBatchExport"); $0.attr("Privilege", "125");

    $0 = $("#ctl00_contentHolder_cbProductTypes"); $0.attr("Privilege", "127");
    $0 = $("#ctl00_contentHolder_cbProductTypesView"); $0.attr("Privilege", "127_1");
    $0 = $("#ctl00_contentHolder_cbProductTypesAdd"); $0.attr("Privilege", "127_2");
    $0 = $("#ctl00_contentHolder_cbProductTypesEdit"); $0.attr("Privilege", "127_3");
    $0 = $("#ctl00_contentHolder_cbProductTypesDelete"); $0.attr("Privilege", "127_4");

    $0 = $("#ctl00_contentHolder_cbManageCategories"); $0.attr("Privilege", "128");
    $0 = $("#ctl00_contentHolder_cbManageCategoriesView"); $0.attr("Privilege", "128_1");
    $0 = $("#ctl00_contentHolder_cbManageCategoriesAdd"); $0.attr("Privilege", "128_2");
    $0 = $("#ctl00_contentHolder_cbManageCategoriesEdit"); $0.attr("Privilege", "128_3");
    $0 = $("#ctl00_contentHolder_cbManageCategoriesDelete"); $0.attr("Privilege", "128_4");

    $0 = $("#ctl00_contentHolder_cbBrandCategories"); $0.attr("Privilege", "129");


    //订单管理
    $0 = $("#ctl00_contentHolder_cbSales"); $0.attr("Privilege", "13");
    $0 = $("#ctl00_contentHolder_cbManageOrder"); $0.attr("Privilege", "131");
    $0 = $("#ctl00_contentHolder_cbManageOrderView"); $0.attr("Privilege", "131_1");
    $0 = $("#ctl00_contentHolder_cbManageOrderDelete"); $0.attr("Privilege", "131_2");
    $0 = $("#ctl00_contentHolder_cbManageOrderEdit"); $0.attr("Privilege", "131_3");
    $0 = $("#ctl00_contentHolder_cbManageOrderConfirm"); $0.attr("Privilege", "131_4");
    $0 = $("#ctl00_contentHolder_cbManageOrderSendedGoods"); $0.attr("Privilege", "131_5");
    $0 = $("#ctl00_contentHolder_cbExpressPrint"); $0.attr("Privilege", "131_6");
    $0 = $("#ctl00_contentHolder_cbManageOrderRefund"); $0.attr("Privilege", "131_7");
    $0 = $("#ctl00_contentHolder_cbManageOrderRemark"); $0.attr("Privilege", "131_8");

    $0 = $("#ctl00_contentHolder_cbExpressTemplates"); $0.attr("Privilege", "132");
    $0 = $("#ctl00_contentHolder_cbShipper"); $0.attr("Privilege", "133");

//    $0 = $("#ctl00_contentHolder_cbOrderRefundApply"); $0.attr("Privilege", "134");
//    $0 = $("#ctl00_contentHolder_cbOrderReturnsApply"); $0.attr("Privilege", "135");
//    $0 = $("#ctl00_contentHolder_cbOrderReplaceApply"); $0.attr("Privilege", "136");




    //会员管理
    $0 = $("#ctl00_contentHolder_cbManageUsers"); $0.attr("Privilege", "14");
    $0 = $("#ctl00_contentHolder_cbManageMembers"); $0.attr("Privilege", "141");
    $0 = $("#ctl00_contentHolder_cbManageMembersView"); $0.attr("Privilege", "141_1");
    $0 = $("#ctl00_contentHolder_cbManageMembersEdit"); $0.attr("Privilege", "141_2");
    $0 = $("#ctl00_contentHolder_cbManageMembersDelete"); $0.attr("Privilege", "141_3");

    $0 = $("#ctl00_contentHolder_cbMemberRanks"); $0.attr("Privilege", "142");
    $0 = $("#ctl00_contentHolder_cbMemberRanksView"); $0.attr("Privilege", "142_1");
    $0 = $("#ctl00_contentHolder_cbMemberRanksAdd"); $0.attr("Privilege", "142_2");
    $0 = $("#ctl00_contentHolder_cbMemberRanksEdit"); $0.attr("Privilege", "142_3");
    $0 = $("#ctl00_contentHolder_cbMemberRanksDelete"); $0.attr("Privilege", "142_4");
    
    //CRM管理
    $0 = $("#ctl00_contentHolder_cbCRMmanager"); $0.attr("Privilege", "16");
    $0 = $("#ctl00_contentHolder_cbMemberMarket"); $0.attr("Privilege", "167");
    $0 = $("#ctl00_contentHolder_cbClientGroup"); $0.attr("Privilege", "167_1");
    $0 = $("#ctl00_contentHolder_cbClientNew"); $0.attr("Privilege", "167_2");
    $0 = $("#ctl00_contentHolder_cbClientActivy"); $0.attr("Privilege", "167_3");
    $0 = $("#ctl00_contentHolder_cbClientSleep"); $0.attr("Privilege", "167_4");


    //营销推广
    $0 = $("#ctl00_contentHolder_cbMarketing"); $0.attr("Privilege", "17");
    $0 = $("#ctl00_contentHolder_cbGifts"); $0.attr("Privilege", "171");
    $0 = $("#ctl00_contentHolder_cbProductPromotion"); $0.attr("Privilege", "172");
    $0 = $("#ctl00_contentHolder_cbOrderPromotion"); $0.attr("Privilege", "173");
    $0 = $("#ctl00_contentHolder_cbOrderPromotion"); $0.attr("Privilege", "174");
    $0 = $("#ctl00_contentHolder_cbGroupBuy"); $0.attr("Privilege", "176");
    $0 = $("#ctl00_contentHolder_cbCountDown"); $0.attr("Privilege", "177");
    $0 = $("#ctl00_contentHolder_cbCoupons"); $0.attr("Privilege", "178");

    //统计报表
    $0 = $("#ctl00_contentHolder_cbTotalReport"); $0.attr("Privilege", "19");
    $0 = $("#ctl00_contentHolder_cbSaleTotalStatistics"); $0.attr("Privilege", "191");
    $0 = $("#ctl00_contentHolder_cbUserOrderStatistics"); $0.attr("Privilege", "192");
    $0 = $("#ctl00_contentHolder_cbSaleList"); $0.attr("Privilege", "193");
    $0 = $("#ctl00_contentHolder_cbSaleTargetAnalyse"); $0.attr("Privilege", "194");

    $0 = $("#ctl00_contentHolder_cbProductSaleRanking"); $0.attr("Privilege", "195");
    $0 = $("#ctl00_contentHolder_cbProductSaleStatistics"); $0.attr("Privilege", "196");
    $0 = $("#ctl00_contentHolder_cbMemberRanking"); $0.attr("Privilege", "197");
    $0 = $("#ctl00_contentHolder_cbMemberArealDistributionStatistics"); $0.attr("Privilege", "198");
    $0 = $("#ctl00_contentHolder_cbUserIncreaseStatistics"); $0.attr("Privilege", "199");

    // 加载后的全选操作 ------------------------------------------------------------------------------------------------------------------------------------

    showOneLayerOnLoad();

    function showOneLayerOnLoad() {

        var flag;

        for (var i = 10; i <= 20; i++) {
            $_Control1 = $("input[type='checkbox'][Privilege='" + i + "']");
            var result1 = showTwoLayerOnLoad(i);
            // 如果当前一级下没有下级则判断自己，如果没有选择设置为 false            
            if (result1 == "no" && !$_Control1.attr("checked"))
                flag = false;
            else if (result1)
                $_Control1.attr("checked", true);
            else
                flag = false;
        }
        if (flag)
            $("input[type='checkbox'][Privilege='0']").attr("checked", true);
    };

    function showTwoLayerOnLoad(one) {

        var flag2 = true;
        for (var j = 1; j <= 15; j++) {

            $_Control2 = $("input[type='checkbox'][Privilege='" + one + j + "']");

            // 如果当前一级下没有下级则返回 no ,告诉上级无下级
            if ($_Control2.attr("id") == undefined && j == 1) {
                flag2 = "no";
                return flag2;
            }
            // 如果已经循环到尽头则返回结果
            else if ($_Control2.attr("id") == undefined) {
                return flag2;
            }
            // 如果有下级且没到尽头继续操作
            else if ($_Control2.attr("id") != undefined) {

                // 判断当前的二级下的三级情况
                var result2 = showTheeLayerOnLoad(one, j);


                // 如果当前二级下没有三级则判断自己,如果没选择设置为 false
                if (result2 == "no" && !$_Control2.attr("checked"))
                    flag2 = false;
                else if (result2)
                    $_Control2.attr("checked", true);
                else
                    flag2 = false;
            }
        }

        return flag2;
    };

    function showTheeLayerOnLoad(one, two) {

        var flag3 = true;
        for (var k = 1; k <= 15; k++) {

            $_Control3 = $("input[type='checkbox'][Privilege$='" + one + two + "_" + k + "']");

            // 如果当前二级下没有下级则返回 no ,告诉上级无下级
            if ($_Control3.attr("id") == undefined && k == 1)
                return "no";
            // 如果已经循环到尽头则返回结果
            else if ($_Control3.attr("id") == undefined)
                return flag3;
            // 如果有下级且没到尽头继续操作
            else if ($_Control3.attr("id") != undefined && !$_Control3.attr("checked"))
                flag3 = false;
        }

        return flag3;
    };

    // 单击触发事件 -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    $("input[type='checkbox']").bind('click', function () {

        var value = this.checked;
        // 全选
        if ($(this).attr("privilege") == "0")
            $("input[type='checkbox']").attr("checked", value);
        // 一层的选择
        else if ($(this).attr("privilege") >= 10 && $(this).attr("privilege") <= 20) {
            // 没有被选择时
            if (!value) {
                $("input[type='checkbox'][Privilege='0']").attr("checked", false);
                $("input[type='checkbox'][Privilege^='" + $(this).attr("privilege") + "']").attr("checked", value);
            }
            // 选择时
            else {
                if (IsOneLayerAllChecked()) {
                    $("input[type='checkbox'][Privilege='0']").attr("checked", true);
                }
                $("input[type='checkbox'][Privilege^='" + $(this).attr("privilege") + "']").attr("checked", value);
            }
            //showTheeLayer2($(this).attr("privilege"), value);
        }
        // 二层的选择
        else if ($(this).attr("privilege").length = 3 && $(this).attr("privilege") > 100) {
            // 没有被选择时
            if (!value) {
                $("input[type='checkbox'][Privilege='0']").attr("checked", false);
                $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, 1) + "']").attr("checked", false);
            }
            // 选择时
            else {
                if (IsTwoLayerAllCheckedOfOne($(this).attr("privilege").substring(0, 1)))
                    $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, 1) + "']").attr("checked", true);
                if (IsOneLayerAllChecked())
                    $("input[type='checkbox'][Privilege='0']").attr("checked", true);
            }
            showTheeLayer2($(this).attr("privilege"), value);
        }
        // 三层的选择
        else {
            // 没有被选择时
            if (!value) {
                $("input[type='checkbox'][Privilege='0']").attr("checked", false);
                $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, 1) + "']").attr("checked", false);
                $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, this.Privilege.indexOf("_")) + "']").attr("checked", false);
            }
            // 选择时
            else {
                if (IsThreeLayerAllCheckedOfTwo($(this).attr("privilege").substring(0, $(this).attr("privilege").indexOf("_"))))
                    $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, $(this).attr("privilege").indexOf("_")) + "']").attr("checked", true);
                if (IsTwoLayerAllCheckedOfOne($(this).attr("privilege").substring(0, 1)))
                    $("input[type='checkbox'][Privilege='" + $(this).attr("privilege").substring(0, 1) + "']").attr("checked", true);
                if (IsOneLayerAllChecked())
                    $("input[type='checkbox'][Privilege='0']").attr("checked", true);
            }
        }
    })

    // 选择后判断父类是否应该被选择-----------------------------------------------------------------------------------------------------------------------------------------

    // 判断一层是否都选中了
    var IsOneLayerAllChecked = function () {
        for (var i = 10; i <= 21; i++) {
            if (!$("input[type='checkbox'][Privilege='" + i + "']").attr("checked")) {
                return false;
            }
        }
        return true;
    }

    // 判断某一层下的二层是否都选中了
    var IsTwoLayerAllCheckedOfOne = function (one) {
        for (var i = 1; i <= 15; i++) {
            $_Control2 = $("input[type='checkbox'][Privilege='" + one + i + "']");
            if ($_Control2.attr("id") == undefined) {
                break;
            }

            if (!$_Control2.attr("checked")) {
                return false;
            }
        }
        return true;
    }

    // 判断某二层下的三层是否都选中了
    var IsThreeLayerAllCheckedOfTwo = function (two) {
        for (var i = 1; i <= 15; i++) {
            $_Control3 = $("input[type='checkbox'][Privilege='" + two + "_" + i + "']");

            if ($_Control3.attr("id") == undefined) {
                break;
            }

            if (!$_Control3.attr("checked")) {
                return false;
            }
        }
        return true;
    }

    var showTheeLayer = function (one, two, value) {

        for (var k = 1; k <= 15; k++) {

            $_Control2 = $("input[type='checkbox'][Privilege$='" + one + two + "_" + k + "']");

            if ($_Control2.attr("id") == undefined) {
                break;
            }
            $_Control2.attr("checked", value);

        }
    };

    var showTheeLayer2 = function (two, value) {

        for (var k = 1; k <= 15; k++) {

            $_Control2 = $("input[type='checkbox'][Privilege$='" + two + "_" + k + "']");

            if ($_Control2.attr("id") == undefined) {
                break;
            }
            $_Control2.attr("checked", value);

        }
    };

}
);
  