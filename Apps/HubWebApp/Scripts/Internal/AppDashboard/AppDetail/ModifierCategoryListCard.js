"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierCategoryListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var Card_1 = require("XtiShared/Card/Card");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var ModifierCategoryListCard = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModifierCategoryListCard, _super);
    function ModifierCategoryListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this._modCategorySelected = new Events_1.DefaultEvent(_this);
        _this.modCategorySelected = _this._modCategorySelected.handler();
        _this.addCardTitleHeader('Modifier Categories');
        _this.alert = _this.addCardAlert().alert;
        _this.modCategories = _this.addButtonListGroup();
        _this.modCategories.itemClicked.register(_this.onItemSelected.bind(_this));
        return _this;
    }
    ModifierCategoryListCard.prototype.onItemSelected = function (item) {
        this._modCategorySelected.invoke(item.getData());
    };
    ModifierCategoryListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var modCategories;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getModCategories()];
                    case 1:
                        modCategories = _a.sent();
                        this.modCategories.setItems(modCategories, function (sourceItem, listItem) {
                            listItem.setData(sourceItem);
                            listItem.addContent(new Row_1.Row())
                                .addColumn()
                                .addContent(new TextSpan_1.TextSpan(sourceItem.Name));
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
}(Card_1.Card));
exports.ModifierCategoryListCard = ModifierCategoryListCard;
//# sourceMappingURL=ModifierCategoryListCard.js.map