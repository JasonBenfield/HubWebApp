"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentErrorEventListCard = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var EventListItem_1 = require("../EventListItem");
var MostRecentErrorEventListCard = /** @class */ (function () {
    function MostRecentErrorEventListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new TextBlock_1.TextBlock('Most Recent Errors', this.view.titleHeader);
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
        this.errorEvents = new ListGroup_1.ListGroup(this.view.errorEvents);
    }
    MostRecentErrorEventListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var errorEvents;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getErrorEvents()];
                    case 1:
                        errorEvents = _a.sent();
                        this.errorEvents.setItems(errorEvents, function (errorEvent, itemView) {
                            return new EventListItem_1.EventListItem(errorEvent, itemView);
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
                                    case 0: return [4 /*yield*/, this.hubApi.App.GetMostRecentErrorEvents(10)];
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