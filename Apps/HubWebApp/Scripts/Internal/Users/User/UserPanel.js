"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command/Command");
var Result_1 = require("XtiShared/Result");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var MarginCss_1 = require("XtiShared/MarginCss");
var AppListCard_1 = require("../../Apps/AppListCard");
var UserComponent_1 = require("./UserComponent");
var HubTheme_1 = require("../../HubTheme");
var UserPanel = /** @class */ (function (_super) {
    tslib_1.__extends(UserPanel, _super);
    function UserPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.backCommand = new Command_1.Command(_this.back.bind(_this));
        _this.height100();
        _this.setName(UserPanel.name);
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.userComponent = flexFill.container.addContent(new UserComponent_1.UserComponent(_this.hubApi))
            .configure(function (c) { return c.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.appListCard = flexFill.container.addContent(new AppListCard_1.AppListCard(_this.hubApi, function (appID) { return _this.hubApi.UserInquiry.RedirectToAppUser.getUrl({
            AppID: appID,
            UserID: _this.userID
        }).toString(); })).configure(function (c) { return c.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backCommand.add(toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton())).configure(function (b) { return b.setText('App Permissions'); });
        _this.appListCard.appSelected.register(_this.onAppSelected.bind(_this));
        _this.userComponent.editRequested.register(_this.onEditRequested.bind(_this));
        return _this;
    }
    UserPanel.prototype.onAppSelected = function (app) {
        this.awaitable.resolve(new Result_1.Result(UserPanel.ResultKeys.appSelected, app));
    };
    UserPanel.prototype.onEditRequested = function (userID) {
        this.awaitable.resolve(new Result_1.Result(UserPanel.ResultKeys.editRequested, userID));
    };
    UserPanel.prototype.setUserID = function (userID) {
        this.userID = userID;
        this.userComponent.setUserID(userID);
    };
    UserPanel.prototype.refresh = function () {
        var promises = [
            this.userComponent.refresh(),
            this.appListCard.refresh()
        ];
        return Promise.all(promises);
    };
    UserPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserPanel.prototype.back = function () {
        this.awaitable.resolve(new Result_1.Result(UserPanel.ResultKeys.backRequested));
    };
    UserPanel.ResultKeys = {
        backRequested: 'back-requested',
        appSelected: 'app-selected',
        editRequested: 'edit-requested'
    };
    return UserPanel;
}(Block_1.Block));
exports.UserPanel = UserPanel;
//# sourceMappingURL=UserPanel.js.map