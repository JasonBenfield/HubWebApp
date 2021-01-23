"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CurrentVersionComponent = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var CurrentVersionComponent = /** @class */ (function () {
    function CurrentVersionComponent(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.alert = new Alert_1.Alert(this.vm.alert);
    }
    CurrentVersionComponent.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var currentVersion;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getCurrentVersion()];
                    case 1:
                        currentVersion = _a.sent();
                        this.vm.versionKey(currentVersion.VersionKey);
                        this.vm.version(currentVersion.Major + "." + currentVersion.Minor + "." + currentVersion.Patch);
                        return [2 /*return*/];
                }
            });
        });
    };
    CurrentVersionComponent.prototype.getCurrentVersion = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var currentVersion;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.App.GetCurrentVersion()];
                                    case 1:
                                        currentVersion = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, currentVersion];
                }
            });
        });
    };
    return CurrentVersionComponent;
}());
exports.CurrentVersionComponent = CurrentVersionComponent;
//# sourceMappingURL=CurrentVersionComponent.js.map