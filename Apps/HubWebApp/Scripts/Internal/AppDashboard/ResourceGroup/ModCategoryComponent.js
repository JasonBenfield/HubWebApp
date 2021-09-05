"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponent = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var ModCategoryComponent = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModCategoryComponent, _super);
    function ModCategoryComponent(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this._clicked = new Events_1.DefaultEvent(_this);
        _this.clicked = _this._clicked.handler();
        _this.addCardTitleHeader('Modifier Category');
        _this.alert = _this.addCardAlert().alert;
        _this.listGroup = _this.addButtonListGroup();
        _this.modCategoryName = _this.listGroup
            .addItem()
            .addContent(new TextSpan_1.TextSpan());
        _this.listGroup.itemClicked.register(_this.onClicked.bind(_this));
        return _this;
    }
    ModCategoryComponent.prototype.onClicked = function () {
        this._clicked.invoke(this.modCategory);
    };
    ModCategoryComponent.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
        this.listGroup.hide();
    };
    ModCategoryComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var _a;
            return (0, tslib_1.__generator)(this, function (_b) {
                switch (_b.label) {
                    case 0:
                        _a = this;
                        return [4 /*yield*/, this.getModCategory(this.groupID)];
                    case 1:
                        _a.modCategory = _b.sent();
                        this.modCategoryName.setText(this.modCategory.Name);
                        this.listGroup.show();
                        return [2 /*return*/];
                }
            });
        });
    };
    ModCategoryComponent.prototype.getModCategory = function (groupID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var modCategory;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetModCategory(groupID)];
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