"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListCard = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var ModifierListItem_1 = require("./ModifierListItem");
var ModifierListCard = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModifierListCard, _super);
    function ModifierListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        vm.title('Modifiers');
        return _this;
    }
    ModifierListCard.prototype.setModCategoryID = function (modCategoryID) {
        this.modCategoryID = modCategoryID;
    };
    ModifierListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var modifiers;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getModifiers()];
                    case 1:
                        modifiers = _a.sent();
                        this.modifiers.setItems(modifiers, function (sourceItem, listItem) {
                            listItem.setData(sourceItem);
                            listItem.addContent(new ModifierListItem_1.ModifierListItem(sourceItem));
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
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var modifiers;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
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
}(Card_1.Card));
exports.ModifierListCard = ModifierListCard;
//# sourceMappingURL=ModifierListCard.js.map