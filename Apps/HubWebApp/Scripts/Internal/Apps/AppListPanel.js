"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListPanel = void 0;
var Awaitable_1 = require("XtiShared/Awaitable");
var Result_1 = require("XtiShared/Result");
var AppListCard_1 = require("./AppListCard");
var AppListPanel = /** @class */ (function () {
    function AppListPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.appListCard = new AppListCard_1.AppListCard(this.vm.appListCard, this.hubApi);
        this.awaitable = new Awaitable_1.Awaitable();
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
    }
    AppListPanel.prototype.onAppSelected = function (app) {
        this.awaitable.resolve(new Result_1.Result(AppListPanel.ResultKeys.appSelected, app));
    };
    AppListPanel.prototype.refresh = function () {
        return this.appListCard.refresh();
    };
    AppListPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    AppListPanel.ResultKeys = {
        appSelected: 'app-selected'
    };
    return AppListPanel;
}());
exports.AppListPanel = AppListPanel;
//# sourceMappingURL=AppListPanel.js.map