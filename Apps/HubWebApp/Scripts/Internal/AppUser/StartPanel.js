"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.StartPanel = exports.StartPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var StartPanelResult = /** @class */ (function () {
    function StartPanelResult(results) {
        this.results = results;
    }
    StartPanelResult.done = function () { return new StartPanelResult({ done: {} }); };
    Object.defineProperty(StartPanelResult.prototype, "done", {
        get: function () { return this.results.done; },
        enumerable: false,
        configurable: true
    });
    return StartPanelResult;
}());
exports.StartPanelResult = StartPanelResult;
var StartPanel = /** @class */ (function () {
    function StartPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
    }
    StartPanel.prototype.start = function () {
        new DelayedAction_1.DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    };
    StartPanel.prototype.delayedStart = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            return (0, tslib_1.__generator)(this, function (_a) {
                return [2 /*return*/, 0];
            });
        });
    };
    StartPanel.prototype.getApp = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var app;
            return (0, tslib_1.__generator)(this, function (_a) {
                //await this.alert.infoAction(
                //    async 
                //);
                return [2 /*return*/, app];
            });
        });
    };
    return StartPanel;
}());
exports.StartPanel = StartPanel;
//# sourceMappingURL=StartPanel.js.map