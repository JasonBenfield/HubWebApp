"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierCategoryListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var ModifierCategoryListItem_1 = require("./ModifierCategoryListItem");
var ModifierCategoryListCard = /** @class */ (function () {
    function ModifierCategoryListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this._modCategorySelected = new Events_1.DefaultEvent(this);
        this.modCategorySelected = this._modCategorySelected.handler();
        new TextBlock_1.TextBlock('Modifier Categories', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.modCategories = new ListGroup_1.ListGroup(this.view.modCategories);
        this.modCategories.itemClicked.register(this.onItemSelected.bind(this));
    }
    ModifierCategoryListCard.prototype.onItemSelected = function (item) {
        this._modCategorySelected.invoke(item.modCategory);
    };
    ModifierCategoryListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var modCategories;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getModCategories()];
                    case 1:
                        modCategories = _a.sent();
                        this.modCategories.setItems(modCategories, function (modCategory, itemView) {
                            return new ModifierCategoryListItem_1.ModifierCategoryListItem(modCategory, itemView);
                        });
                        if (modCategories.length === 0) {
                            this.alert.danger('No Modifier Categories were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ModifierCategoryListCard.prototype.getModCategories = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var modCategories;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.App.GetModifierCategories()];
                                    case 1:
                                        modCategories = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, modCategories];
                }
            });
        });
    };
    return ModifierCategoryListCard;
}());
exports.ModifierCategoryListCard = ModifierCategoryListCard;
//# sourceMappingURL=ModifierCategoryListCard.js.map