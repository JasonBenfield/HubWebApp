"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppComponent = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var AppComponent = /** @class */ (function () {
    function AppComponent(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.alert = new Alert_1.Alert(this.vm.alert);
    }
    AppComponent.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var app;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getApp()];
                    case 1:
                        app = _a.sent();
                        this.vm.appName(app.AppName);
                        this.vm.title(app.Title);
                        this.vm.appType(app.Type.DisplayText);
                        return [2 /*return*/];
                }
            });
        });
    };
    AppComponent.prototype.getApp = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var app;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.App.GetApp()];
                                    case 1:
                                        app = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, app];
                }
            });
        });
    };
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=AppComponent.js.map