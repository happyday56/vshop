/// <reference path="../Templates/vshop/default/script/jquery-1.11.0.min.js" />


function vShop_RegionSelector(containerId, onSelected, defaultRegionText) {
    /// <param name="onSelected" type="function">选择地址后回调,包括两个参数，依次为址址和地址编码</param>

    var regionHandleUrl = '/Vshop/RegionHandler.aspx';
    init();
    var address = '';
    var code = 0;
    var country;

    function init() {
        if (!defaultRegionText)
            defaultRegionText = '请选择省市区';
        var text = '<div class="btn-group bmargin">\
        <button id="address-check-btn" type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">' + defaultRegionText + '<span class="caret"></span></button>\
        <ul name="province" class="dropdown-menu" role="menu"></ul>\
        <ul name="city" class="dropdown-menu hide" role="menu"></ul>\
        <ul name="district" class="dropdown-menu hide" role="menu"></ul>\
        </div>';

        $('#' + containerId).html(text);
        initCountryData(function () {
            getRegin("province", 0, function () { bind(); });

        });

    }

    function initCountryData(callBack) {
        var jsonFilePath = '/config/region.js';
        $.getJSON(jsonFilePath, {}, function (data) {
            country = data;
            callBack();
        });
    }

    function getRegin(regionType, parentRegionId, callback) {
        /// <param name="regionType" type="String">"province-省,city-市,district-区"</param>
        var text = '';

        if (!parentRegionId) {
            parentRegionId = 0;
            address = '';
        }

        var regions = getSubRegions(parentRegionId);

        $.each(regions, function (i, region) {
            text += '<li><a href="#" name="' + region.RegionId + '">' + region.RegionName + '</a></li>';
        });
        $('#' + containerId + ' ul[name="' + regionType + '"]').html(text);

        callback();
    }


    function getSubRegions(parentRegionId) {
        var regions = [];
        var nodes;
        parentRegionId = parseInt(parentRegionId);
        if (parentRegionId == 0) {
            nodes = country.province;
        }
        else {
            var province,id;
            for (var i = 0; i < country.province.length; i++) {//搜索省
                id = parseInt(country.province[i].id);
                if (parentRegionId < id) {
                    province = country.province[i > 0 ? i - 1 : 0];
                    break;
                }
                else if (parentRegionId == id) {
                    province = country.province[i];
                    break;
                }
            }

            if (parentRegionId == parseInt(province.id))//相等则表示该parentRegionId为省
                nodes = province.city; //直接获取该省下的所有市
            else {
                if (!$.isArray(province.city))
                    province.city = [province.city];

                var city;
                for (var i = 0; i < province.city.length; i++) {//搜索市,并且parentRegionId必定为市的id
                    id = parseInt(province.city[i].id);
                    if (parentRegionId < parseInt(province.city[i].id)) {
                        city = province.city[i > 0 ? i - 1 : 0];
                        break;
                    }
                    else if (parentRegionId == id) {
                        city = province.city[i];
                        break;
                    }
                }
                nodes = city.county;//此时必定为区
            }
        }
        if (!$.isArray(nodes))
            nodes = [nodes];
        $.each(nodes, function (i, node) {
            regions.push({ RegionId: node.id, RegionName: node.name });
        });
        return regions;
    }


    function bind() {
        $('#' + containerId + ' ul li a').unbind('click');
        $('#' + containerId + ' ul li a').click(function () {
            var currentUl = $(this).parent().parent();
            var regionId = $(this).attr('name');
            var nextRegionUl = currentUl.next();
            var prevRegionUl = currentUl.prev();
            var nextRegionType = nextRegionUl ? $(nextRegionUl).attr('name') : '';

            address += $(this).html() + " ";
            if (nextRegionType) {
                getRegin(nextRegionType, regionId, function () {
                    nextRegionUl && $(nextRegionUl).removeClass('hide');
                    currentUl.addClass('hide');
                    bind();
                    setTimeout(function () {
                        $(".btn-group").addClass('open');
                    }, 1);
                });
            }
            else {
                var first = currentUl.parent().find('ul').first();
                $(first).removeClass('hide');
                currentUl.addClass('hide');
                code = $(this).attr('name');
                onSelected(address, code);
                address = '';
            }
        });
    }
} 