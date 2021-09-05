"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var Card_1 = require("XtiShared/Card/Card");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var ResourceResultType_1 = require("../../../Hub/Api/ResourceResultType");
var ResourceListCard = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceListCard, _super);
    function ResourceListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this._resourceSelected = new Events_1.DefaultEvent(_this);
        _this.resourceSelected = _this._resourceSelected.handler();
        _this.addCardTitleHeader('Resources');
        _this.alert = _this.addCardAlert().alert;
        _this.resources = _this.addButtonListGroup();
        _this.resources.itemClicked.register(_this.onItemSelected.bind(_this));
        return _this;
    }
    ResourceListCard.prototype.onItemSelected = function (item) {
        this._resourceSelected.invoke(item.getData());
    };
    ResourceListCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    ResourceListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resources;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResources()];
                    case 1:
                        resources = _a.sent();
                        this.resources.setItems(resources, function (sourceItem, listItem) {
                            listItem.setData(sourceItem);
                            var row = listItem.addContent(new Row_1.Row());
                            row.addColumn()
                                .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs(8)); })
                                .addContent(new TextSpan_1.TextSpan(sourceItem.Name));
                            var resultType = ResourceResultType_1.ResourceResultType.values.value(sourceItem.ResultType.Value);
                            var resultTypeText;
                            if (resultType.equalsAny(ResourceResultType_1.ResourceResultType.values.None, ResourceResultType_1.ResourceResultType.values.Json)) {
                                resultTypeText = '';
                            }
                            else {
                                resultTypeText = resultType.DisplayText;
                            }
                            row.addColumn()
                                .addContent(new TextSpan_1.TextSpan(resultTypeText));
                        });
                        if (resources.length === 0) {
                            this.alert.danger('No Resources were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceListCard.prototype.getResources = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resources;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetResources(this.groupID)];
                                    case 1:
                                        resources = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, resources];
                }
            });
        });
    };
    return ResourceListCard;
}(Card_1.Card));
exports.ResourceListCard = ResourceListCard;
//# sourceMappingURL=ResourceListCard.js.map