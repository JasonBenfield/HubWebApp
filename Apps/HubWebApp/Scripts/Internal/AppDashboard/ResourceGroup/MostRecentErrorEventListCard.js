"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentErrorEventListCard = void 0;
var tslib_1 = require("tslib");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var EventListItem_1 = require("../EventListItem");
var MostRecentErrorEventListCard = /** @class */ (function () {
    function MostRecentErrorEventListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new CardTitleHeader_1.CardTitleHeader('Most Recent Errors', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.errorEvents = new ListGroup_1.ListGroup(this.view.errorEvents);
    }
    MostRecentErrorEventListCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    MostRecentErrorEventListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var errorEvents;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getErrorEvents()];
                    case 1:
                        errorEvents = _a.sent();
                        this.errorEvents.setItems(errorEvents, function (sourceItem, listItem) {
                            return new EventListItem_1.EventListItem(sourceItem, listItem);
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
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var errorEvents;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetMostRecentErrorEvents({
                                            VersionKey: 'Current',
                                            GroupID: this.groupID,
                                            HowMany: 10
                                        })];
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
}());
exports.MostRecentErrorEventListCard = MostRecentErrorEventListCard;
//# sourceMappingURL=MostRecentErrorEventListCard.js.map