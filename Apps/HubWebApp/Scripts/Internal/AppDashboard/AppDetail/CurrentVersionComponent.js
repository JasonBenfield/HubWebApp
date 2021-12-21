"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CurrentVersionComponent = void 0;
var tslib_1 = require("tslib");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var CurrentVersionComponent = /** @class */ (function () {
    function CurrentVersionComponent(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new CardTitleHeader_1.CardTitleHeader('Version', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
    }
    CurrentVersionComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var currentVersion;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getCurrentVersion()];
                    case 1:
                        currentVersion = _a.sent();
                        this.view.setVersionKey(currentVersion.VersionKey);
                        this.view.setVersion("".concat(currentVersion.Major, ".").concat(currentVersion.Minor, ".").concat(currentVersion.Patch));
                        return [2 /*return*/];
                }
            });
        });
    };
    CurrentVersionComponent.prototype.getCurrentVersion = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var currentVersion;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.Version.GetVersion('current')];
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