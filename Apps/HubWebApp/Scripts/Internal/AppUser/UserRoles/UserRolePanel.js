"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRolePanel = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command/Command");
var Result_1 = require("XtiShared/Result");
var EditUserRoleListItem_1 = require("./EditUserRoleListItem");
var HubTheme_1 = require("../../HubTheme");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var UserRolePanel = /** @class */ (function (_super) {
    tslib_1.__extends(UserRolePanel, _super);
    function UserRolePanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.backCommand = new Command_1.Command(_this.back.bind(_this));
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        var card = flexFill.addContent(new Card_1.Card());
        card.addCardTitleHeader('Edit User Roles');
        _this.alert = card.addCardAlert().alert;
        _this.userRoles = card.addButtonListGroup(function (itemVM) { return new EditUserRoleListItem_1.EditUserRoleListItem(_this.hubApi, itemVM); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backCommand.add(toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton()));
        return _this;
    }
    UserRolePanel.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserRolePanel.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var access, sourceItems, _i, _a, userRole, _b, _c, role;
            var _this = this;
            return tslib_1.__generator(this, function (_d) {
                switch (_d.label) {
                    case 0: return [4 /*yield*/, this.getUserRoleAccess(this.userID)];
                    case 1:
                        access = _d.sent();
                        sourceItems = [];
                        for (_i = 0, _a = access.AssignedRoles; _i < _a.length; _i++) {
                            userRole = _a[_i];
                            sourceItems.push(userRole);
                        }
                        for (_b = 0, _c = access.UnassignedRoles; _b < _c.length; _b++) {
                            role = _c[_b];
                            sourceItems.push(role);
                        }
                        sourceItems.sort(this.compare.bind(this));
                        this.userRoles.setItems(sourceItems, function (sourceItem, listItem) {
                            listItem.setUserID(_this.userID);
                            listItem.withAssignedRole(sourceItem);
                        });
                        return [2 /*return*/];
                }
            });
        });
    };
    UserRolePanel.prototype.compare = function (a, b) {
        var roleName;
        roleName = a.Name;
        var otherRoleName;
        otherRoleName = b.Name;
        var result;
        if (roleName < otherRoleName) {
            result = -1;
        }
        else if (roleName === otherRoleName) {
            result = 0;
        }
        else {
            result = 1;
        }
        return result;
    };
    UserRolePanel.prototype.getUserRoleAccess = function (userID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var access;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            var request;
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0:
                                        request = {
                                            UserID: userID,
                                            ModifierID: 0
                                        };
                                        return [4 /*yield*/, this.hubApi.AppUser.GetUserRoleAccess(request)];
                                    case 1:
                                        access = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, access];
                }
            });
        });
    };
    UserRolePanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserRolePanel.prototype.back = function () {
        this.awaitable.resolve(new Result_1.Result(UserRolePanel.ResultKeys.backRequested));
    };
    UserRolePanel.ResultKeys = {
        backRequested: 'back-requested'
    };
    return UserRolePanel;
}(Block_1.Block));
exports.UserRolePanel = UserRolePanel;
//# sourceMappingURL=UserRolePanel.js.map