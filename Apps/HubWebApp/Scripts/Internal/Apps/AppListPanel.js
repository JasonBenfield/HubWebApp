"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListPanel = exports.AppListPanelResult = void 0;
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var AppListCard_1 = require("./AppListCard");
var AppListPanelResult = /** @class */ (function () {
    function AppListPanelResult(results) {
        this.results = results;
    }
    AppListPanelResult.appSelected = function (app) {
        return new AppListPanelResult({ appSelected: { app: app } });
    };
    Object.defineProperty(AppListPanelResult.prototype, "appSelected", {
        get: function () { return this.results.appSelected; },
        enumerable: false,
        configurable: true
    });
    return AppListPanelResult;
}());
exports.AppListPanelResult = AppListPanelResult;
var AppListPanel = /** @class */ (function () {
    function AppListPanel(hubApi, view) {
        var _this = this;
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.appListCard = new AppListCard_1.AppListCard(this.hubApi, function (modKey) { return _this.hubApi.App.Index.getModifierUrl(modKey, {}).toString(); }, this.view.appListCard);
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
    }
    AppListPanel.prototype.onAppSelected = function (app) {
        this.awaitable.resolve(AppListPanelResult.appSelected(app));
    };
    AppListPanel.prototype.refresh = function () {
        return this.appListCard.refresh();
    };
    AppListPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    AppListPanel.ResultKeys = {};
    return AppListPanel;
}());
exports.AppListPanel = AppListPanel;
//# sourceMappingURL=AppListPanel.js.map