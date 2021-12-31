"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUserDataPanel = exports.AppUserDataPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var AppUserOptions_1 = require("./AppUserOptions");
var AppUserDataPanelResult = /** @class */ (function () {
    function AppUserDataPanelResult(results) {
        this.results = results;
    }
    AppUserDataPanelResult.done = function (appUserData) {
        return new AppUserDataPanelResult({ done: { appUserOptions: appUserData } });
    };
    Object.defineProperty(AppUserDataPanelResult.prototype, "done", {
        get: function () { return this.results.done; },
        enumerable: false,
        configurable: true
    });
    return AppUserDataPanelResult;
}());
exports.AppUserDataPanelResult = AppUserDataPanelResult;
var AppUserDataPanel = /** @class */ (function () {
    function AppUserDataPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
    }
    AppUserDataPanel.prototype.start = function (userID) {
        this.userID = userID;
        new DelayedAction_1.DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    };
    AppUserDataPanel.prototype.delayedStart = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var appUserData;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            var app, user, defaultModifier;
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.App.GetApp()];
                                    case 1:
                                        app = _a.sent();
                                        return [4 /*yield*/, this.hubApi.UserInquiry.GetUser(this.userID)];
                                    case 2:
                                        user = _a.sent();
                                        return [4 /*yield*/, this.hubApi.App.GetDefaultModiifer()];
                                    case 3:
                                        defaultModifier = _a.sent();
                                        appUserData = new AppUserOptions_1.AppUserOptions(app, user, defaultModifier);
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, this.awaitable.resolve(AppUserDataPanelResult.done(appUserData))];
                }
            });
        });
    };
    AppUserDataPanel.prototype.activate = function () { this.view.show(); };
    AppUserDataPanel.prototype.deactivate = function () { this.view.hide(); };
    return AppUserDataPanel;
}());
exports.AppUserDataPanel = AppUserDataPanel;
//# sourceMappingURL=AppUserDataPanel.js.map