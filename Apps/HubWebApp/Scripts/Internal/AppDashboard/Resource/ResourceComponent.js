"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceComponent = void 0;
var tslib_1 = require("tslib");
var ResourceResultType_1 = require("../../../Hub/Api/ResourceResultType");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var Row_1 = require("XtiShared/Grid/Row");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var ResourceComponent = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceComponent, _super);
    function ResourceComponent(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.addCardTitleHeader('Resource');
        _this.alert = _this.addCardAlert().alert;
        var listGroup = _this.addListGroup();
        var row = listGroup
            .addItem()
            .addContent(new Row_1.Row());
        _this.resourceName = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new TextSpan_1.TextSpan());
        _this.resultType = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.anonListItem = listGroup.addItem();
        _this.anonListItem.addContent(new Row_1.Row())
            .addColumn()
            .addContent(new TextSpan_1.TextSpan('Anonymous is Allowed'));
        _this.anonListItem.hide();
        return _this;
    }
    ResourceComponent.prototype.setResourceID = function (resourceID) {
        this.resourceID = resourceID;
        this.resourceName.setText('');
        this.resultType.setText('');
        this.anonListItem.hide();
    };
    ResourceComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resource, resultType, resultTypeText;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResource(this.resourceID)];
                    case 1:
                        resource = _a.sent();
                        this.resourceName.setText(resource.Name);
                        if (resource.IsAnonymousAllowed) {
                            this.anonListItem.show();
                        }
                        else {
                            this.anonListItem.hide();
                        }
                        resultType = ResourceResultType_1.ResourceResultType.values.value(resource.ResultType.Value);
                        if (resultType.equalsAny(ResourceResultType_1.ResourceResultType.values.None, ResourceResultType_1.ResourceResultType.values.Json)) {
                            resultTypeText = '';
                        }
                        else {
                            resultTypeText = resultType.DisplayText;
                        }
                        this.resultType.setText(resultTypeText);
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceComponent.prototype.getResource = function (resourceID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resource;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.Resource.GetResource({
                                            VersionKey: 'Current',
                                            ResourceID: resourceID
                                        })];
                                    case 1:
                                        resource = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, resource];
                }
            });
        });
    };
    return ResourceComponent;
}(Card_1.Card));
exports.ResourceComponent = ResourceComponent;
//# sourceMappingURL=ResourceComponent.js.map