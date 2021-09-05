"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var ResourceGroupListItem_1 = require("../ResourceGroupListItem");
var ResourceGroupListCard = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupListCard, _super);
    function ResourceGroupListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this._resourceSelected = new Events_1.DefaultEvent(_this);
        _this.resourceGroupSelected = _this._resourceSelected.handler();
        _this.addCardTitleHeader('Resource Groups');
        _this.alert = _this.addCardAlert().alert;
        _this.requests = _this.addButtonListGroup();
        return _this;
    }
    ResourceGroupListCard.prototype.onItemSelected = function (item) {
        this._resourceSelected.invoke(item.getData());
    };
    ResourceGroupListCard.prototype.setModCategoryID = function (modCategoryID) {
        this.modCategoryID = modCategoryID;
    };
    ResourceGroupListCard.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var resourceGroups;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResourceGroups()];
                    case 1:
                        resourceGroups = _a.sent();
                        this.requests.setItems(resourceGroups, function (sourceItem, listItem) {
                            listItem.setData(sourceItem);
                            listItem.addContent(new ResourceGroupListItem_1.ResourceGroupListItem(sourceItem));
                        });
                        if (resourceGroups.length === 0) {
                            this.alert.danger('No Resource Groups were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceGroupListCard.prototype.getResourceGroups = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var resourceGroup;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ModCategory.GetResourceGroups(this.modCategoryID)];
                                    case 1:
                                        resourceGroup = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, resourceGroup];
                }
            });
        });
    };
    return ResourceGroupListCard;
}(Card_1.Card));
exports.ResourceGroupListCard = ResourceGroupListCard;
//# sourceMappingURL=ResourceGroupListCard.js.map