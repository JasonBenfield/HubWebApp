"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUserPanel = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command/Command");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var Result_1 = require("XtiShared/Result");
var UserComponent_1 = require("./UserComponent");
var UserRoleListCard_1 = require("./UserRoleListCard");
var HubTheme_1 = require("../../HubTheme");
var MarginCss_1 = require("XtiShared/MarginCss");
var UserModCategoryListCard_1 = require("./UserModCategoryListCard");
var AppUserPanel = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppUserPanel, _super);
    function AppUserPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.backCommand = new Command_1.Command(_this.back.bind(_this));
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.userComponent = flexFill.addContent(new UserComponent_1.UserComponent(_this.hubApi))
            .configure(function (c) { return c.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.userRoles = flexFill.addContent(new UserRoleListCard_1.UserRoleListCard(_this.hubApi))
            .configure(function (c) { return c.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.userRoles.editRequested.register(_this.onEditUserRolesRequested.bind(_this));
        _this.userModCategories = flexFill.addContent(new UserModCategoryListCard_1.UserModCategoryListCard(_this.hubApi));
        _this.userModCategories.editRequested.register(_this.onEditUserModCategoryRequested.bind(_this));
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backCommand.add(toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton())).configure(function (b) { return b.setText('User'); });
        return _this;
    }
    AppUserPanel.prototype.onEditUserRolesRequested = function () {
        this.awaitable.resolve(new Result_1.Result(AppUserPanel.ResultKeys.editUserRolesRequested));
    };
    AppUserPanel.prototype.onEditUserModCategoryRequested = function (userModCategory) {
        this.awaitable.resolve(new Result_1.Result(AppUserPanel.ResultKeys.editUserModCategoryRequested, userModCategory));
    };
    AppUserPanel.prototype.setUserID = function (userID) {
        this.userComponent.setUserID(userID);
        this.userRoles.setUserID(userID);
        this.userModCategories.setUserID(userID);
    };
    AppUserPanel.prototype.refresh = function () {
        var promises = [
            this.userComponent.refresh(),
            this.userRoles.refresh(),
            this.userModCategories.refresh()
        ];
        return Promise.all(promises);
    };
    AppUserPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    AppUserPanel.prototype.back = function () {
        this.awaitable.resolve(new Result_1.Result(AppUserPanel.ResultKeys.backRequested));
    };
    AppUserPanel.ResultKeys = {
        backRequested: 'back-requested',
        editUserRolesRequested: 'edit-user-roles-requested',
        editUserModCategoryRequested: 'edit-user-mod-category-requested'
    };
    return AppUserPanel;
}(Block_1.Block));
exports.AppUserPanel = AppUserPanel;
//# sourceMappingURL=AppUserPanel.js.map