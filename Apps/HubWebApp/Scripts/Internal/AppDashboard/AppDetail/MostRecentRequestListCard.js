"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentRequestListCard = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var RequestExpandedListItem_1 = require("../RequestExpandedListItem");
var MostRecentRequestListCard = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(MostRecentRequestListCard, _super);
    function MostRecentRequestListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.addCardTitleHeader('Most Recent Requests');
        _this.alert = _this.addCardAlert().alert;
        _this.requests = _this.addButtonListGroup();
        return _this;
    }
    MostRecentRequestListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var requests;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getRequests()];
                    case 1:
                        requests = _a.sent();
                        this.requests.setItems(requests, function (sourceItem, listItem) {
                            listItem.addContent(new RequestExpandedListItem_1.RequestExpandedListItem(sourceItem));
                        });
                        if (requests.length === 0) {
                            this.alert.danger('No Requests were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MostRecentRequestListCard.prototype.getRequests = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var requests;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.App.GetMostRecentRequests(10)];
                                    case 1:
                                        requests = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, requests];
                }
            });
        });
    };
    return MostRecentRequestListCard;
}(Card_1.Card));
exports.MostRecentRequestListCard = MostRecentRequestListCard;
//# sourceMappingURL=MostRecentRequestListCard.js.map