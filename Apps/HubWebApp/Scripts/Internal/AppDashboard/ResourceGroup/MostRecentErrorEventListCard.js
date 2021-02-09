"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentErrorEventListCard = void 0;
var tslib_1 = require("tslib");
var EventListItem_1 = require("../EventListItem");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var MostRecentErrorEventListCard = /** @class */ (function (_super) {
    tslib_1.__extends(MostRecentErrorEventListCard, _super);
    function MostRecentErrorEventListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.addCardTitleHeader('Most Recent Errors');
        _this.alert = _this.addCardAlert().alert;
        _this.errorEvents = _this.addButtonListGroup();
        return _this;
    }
    MostRecentErrorEventListCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    MostRecentErrorEventListCard.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var errorEvents;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getErrorEvents()];
                    case 1:
                        errorEvents = _a.sent();
                        this.errorEvents.setItems(errorEvents, function (sourceItem, listItem) {
                            listItem.addContent(new EventListItem_1.EventListItem(sourceItem));
                        });
                        if (errorEvents.length === 0) {
                            this.alert.danger('No Errors were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MostRecentErrorEventListCard.prototype.getErrorEvents = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var errorEvents;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetMostRecentErrorEvents({ GroupID: this.groupID, HowMany: 10 })];
                                    case 1:
                                        errorEvents = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, errorEvents];
                }
            });
        });
    };
    return MostRecentErrorEventListCard;
}(Card_1.Card));
exports.MostRecentErrorEventListCard = MostRecentErrorEventListCard;
//# sourceMappingURL=MostRecentErrorEventListCard.js.map