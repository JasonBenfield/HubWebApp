"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserModCategoryPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command/Command");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var Card_1 = require("XtiShared/Card/Card");
var HubTheme_1 = require("../../HubTheme");
var EditUserModifierListItem_1 = require("./EditUserModifierListItem");
var Result_1 = require("../../../../Imports/Shared/Result");
var DropDownFormGroup_1 = require("../../../../Imports/Shared/Forms/DropDownFormGroup");
var SelectOption_1 = require("../../../../Imports/Shared/Html/SelectOption");
var UserModCategoryPanel = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserModCategoryPanel, _super);
    function UserModCategoryPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.backCommand = new Command_1.Command(_this.back.bind(_this));
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        var card = flexFill.addContent(new Card_1.Card());
        card.addCardTitleHeader('Edit User Modifiers');
        _this.alert = card.addCardAlert().alert;
        var body = card.addCardBody();
        _this.hasAccessToAll = body.addContent(new DropDownFormGroup_1.DropDownFormGroup('', 'HasAccessToAll'));
        _this.hasAccessToAll.setCaption('Has Access to All Modifiers?');
        _this.hasAccessToAll.setItems(new SelectOption_1.SelectOption(true, 'Yes'), new SelectOption_1.SelectOption(false, 'No'));
        _this.hasAccessToAll.valueChanged.register(_this.onHasAccessToAllChanged.bind(_this));
        _this.userModifiers = card.addButtonListGroup(function (itemVM) { return new EditUserModifierListItem_1.EditUserModifierListItem(_this.hubApi, itemVM); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backCommand.add(toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton()));
        return _this;
    }
    UserModCategoryPanel.prototype.onHasAccessToAllChanged = function (hasAccessToAll) {
        if (hasAccessToAll) {
            this.userModifiers.hide();
        }
        else {
            this.userModifiers.show();
        }
    };
    UserModCategoryPanel.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserModCategoryPanel.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var access, sourceItems, _i, _a, userRole, _b, _c, role;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_d) {
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
                        this.userModifiers.setItems(sourceItems, function (sourceItem, listItem) {
                            listItem.setUserID(_this.userID);
                            listItem.withAssignedModifier(sourceItem);
                        });
                        return [2 /*return*/];
                }
            });
        });
    };
    UserModCategoryPanel.prototype.compare = function (a, b) {
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
    UserModCategoryPanel.prototype.getUserRoleAccess = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var access;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            var request;
                            return (0, tslib_1.__generator)(this, function (_a) {
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
    UserModCategoryPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserModCategoryPanel.prototype.back = function () {
        this.awaitable.resolve(new Result_1.Result(UserModCategoryPanel.ResultKeys.backRequested));
    };
    UserModCategoryPanel.ResultKeys = {
        backRequested: 'back-requested'
    };
    return UserModCategoryPanel;
}(Block_1.Block));
exports.UserModCategoryPanel = UserModCategoryPanel;
//# sourceMappingURL=UserModCategoryPanel.js.map