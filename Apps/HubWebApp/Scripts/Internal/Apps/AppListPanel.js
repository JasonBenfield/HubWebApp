"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Result_1 = require("XtiShared/Result");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var AppListCard_1 = require("./AppListCard");
var AppListPanel = /** @class */ (function (_super) {
    tslib_1.__extends(AppListPanel, _super);
    function AppListPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        _this.appListCard = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill())
            .addContent(new AppListCard_1.AppListCard(_this.hubApi, function (appID) { return _this.hubApi.Apps.RedirectToApp.getUrl(appID).toString(); }));
        _this.appListCard.appSelected.register(_this.onAppSelected.bind(_this));
        return _this;
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
}(Block_1.Block));
exports.AppListPanel = AppListPanel;
//# sourceMappingURL=AppListPanel.js.map