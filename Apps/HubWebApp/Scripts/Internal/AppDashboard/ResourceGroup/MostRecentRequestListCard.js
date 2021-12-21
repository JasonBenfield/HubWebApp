"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentRequestListCard = void 0;
var tslib_1 = require("tslib");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var RequestExpandedListItem_1 = require("../RequestExpandedListItem");
var MostRecentRequestListCard = /** @class */ (function () {
    function MostRecentRequestListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new CardTitleHeader_1.CardTitleHeader('Most Recent Requests', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.requests = new ListGroup_1.ListGroup(this.view.requests);
    }
    MostRecentRequestListCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    MostRecentRequestListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var requests;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getRequests()];
                    case 1:
                        requests = _a.sent();
                        this.requests.setItems(requests, function (sourceItem, listItem) {
                            return new RequestExpandedListItem_1.RequestExpandedListItem(sourceItem, listItem);
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
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetMostRecentRequests({
                                            VersionKey: 'Current',
                                            GroupID: this.groupID,
                                            HowMany: 10
                                        })];
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
}());
exports.MostRecentRequestListCard = MostRecentRequestListCard;
//# sourceMappingURL=MostRecentRequestListCard.js.map