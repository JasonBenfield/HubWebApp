"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponent = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ModCategoryComponent = /** @class */ (function () {
    function ModCategoryComponent(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this._clicked = new Events_1.DefaultEvent(this);
        this.clicked = this._clicked.handler();
        new TextBlock_1.TextBlock('Modifier Category', this.view.titleHeader);
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
        this.modCategoryName = new TextBlock_1.TextBlock('', this.view.modCategoryName);
        this.view.clicked.register(this.onClicked.bind(this));
    }
    ModCategoryComponent.prototype.onClicked = function () {
        this._clicked.invoke(this.modCategory);
    };
    ModCategoryComponent.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
        this.view.hideModCategory();
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
                        this.view.showModCategory();
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
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetModCategory({
                                            VersionKey: 'Current',
                                            GroupID: groupID
                                        })];
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
}());
exports.ModCategoryComponent = ModCategoryComponent;
//# sourceMappingURL=ModCategoryComponent.js.map