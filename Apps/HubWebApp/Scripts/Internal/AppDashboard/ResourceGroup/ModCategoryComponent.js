"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponent = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var Events_1 = require("XtiShared/Events");
var ModCategoryComponent = /** @class */ (function () {
    function ModCategoryComponent(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this._clicked = new Events_1.DefaultEvent(this);
        this.clicked = this._clicked.handler();
        this.alert = new Alert_1.Alert(this.vm.alert);
        this.vm.clicked.register(this.onClicked.bind(this));
    }
    ModCategoryComponent.prototype.onClicked = function () {
        this._clicked.invoke(this.modCategory);
    };
    ModCategoryComponent.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
        this.vm.name('');
    };
    ModCategoryComponent.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var modCategory;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getModCategory(this.groupID)];
                    case 1:
                        modCategory = _a.sent();
                        this.vm.name(modCategory.Name);
                        this.modCategory = modCategory;
                        return [2 /*return*/];
                }
            });
        });
    };
    ModCategoryComponent.prototype.getModCategory = function (groupID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var modCategory;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
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
}());
exports.ModCategoryComponent = ModCategoryComponent;
//# sourceMappingURL=ModCategoryComponent.js.map