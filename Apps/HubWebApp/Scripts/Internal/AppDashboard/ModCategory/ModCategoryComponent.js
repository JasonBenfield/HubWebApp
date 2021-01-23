"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponent = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var ModCategoryComponent = /** @class */ (function () {
    function ModCategoryComponent(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.alert = new Alert_1.Alert(this.vm.alert);
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
                        this.vm.name(modCategory.Name);
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
}());
exports.ModCategoryComponent = ModCategoryComponent;
//# sourceMappingURL=ModCategoryComponent.js.map