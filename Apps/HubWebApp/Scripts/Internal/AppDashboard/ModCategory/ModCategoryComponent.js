"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponent = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var UnorderedList_1 = require("XtiShared/Html/UnorderedList");
var TextBlock_1 = require("XtiShared/Html/TextBlock");
var ModCategoryComponent = /** @class */ (function (_super) {
    tslib_1.__extends(ModCategoryComponent, _super);
    function ModCategoryComponent(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.addCardTitleHeader('Modifier Category');
        _this.alert = _this.addCardAlert().alert;
        _this.modCategoryName = _this.addCardBody()
            .addContent(new UnorderedList_1.UnorderedList())
            .addItem()
            .addContent(new TextBlock_1.TextBlock());
        return _this;
    }
    ModCategoryComponent.prototype.setModCategoryID = function (modCategoryID) {
        this.modCategoryID = modCategoryID;
        this.vm.name('');
    };
    ModCategoryComponent.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var modCategory;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getModCategory(this.modCategoryID)];
                    case 1:
                        modCategory = _a.sent();
                        this.modCategoryName.setText(modCategory.Name);
                        return [2 /*return*/];
                }
            });
        });
    };
    ModCategoryComponent.prototype.getModCategory = function (modCategoryID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var modCategory;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ModCategory.GetModCategory(modCategoryID)];
                                    case 1:
                                        modCategory = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, modCategory];
                }
            });
        });
    };
    return ModCategoryComponent;
}(Card_1.Card));
exports.ModCategoryComponent = ModCategoryComponent;
//# sourceMappingURL=ModCategoryComponent.js.map