"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListCard = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var ModifierListItem_1 = require("./ModifierListItem");
var ModifierListCard = /** @class */ (function () {
    function ModifierListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new TextBlock_1.TextBlock('Modifiers', this.view.titleHeader);
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
        this.modifiers = new ListGroup_1.ListGroup(this.view.modifiers);
    }
    ModifierListCard.prototype.setModCategoryID = function (modCategoryID) {
        this.modCategoryID = modCategoryID;
    };
    ModifierListCard.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var modifiers;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getModifiers()];
                    case 1:
                        modifiers = _a.sent();
                        this.modifiers.setItems(modifiers, function (sourceItem, listItem) {
                            return new ModifierListItem_1.ModifierListItem(sourceItem, listItem);
                        });
                        if (modifiers.length === 0) {
                            this.alert.danger('No Modifiers were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ModifierListCard.prototype.getModifiers = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var modifiers;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ModCategory.GetModifiers(this.modCategoryID)];
                                    case 1:
                                        modifiers = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, modifiers];
                }
            });
        });
    };
    return ModifierListCard;
}());
exports.ModifierListCard = ModifierListCard;
//# sourceMappingURL=ModifierListCard.js.map