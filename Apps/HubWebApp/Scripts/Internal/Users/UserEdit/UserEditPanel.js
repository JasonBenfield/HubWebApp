"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserEditPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command/Command");
var Result_1 = require("XtiShared/Result");
var EditUserForm_1 = require("../../../Hub/Api/EditUserForm");
var DelayedAction_1 = require("XtiShared/DelayedAction");
var AsyncCommand_1 = require("XtiShared/Command/AsyncCommand");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var Block_1 = require("XtiShared/Html/Block");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var MessageAlert_1 = require("XtiShared/MessageAlert");
var HubTheme_1 = require("../../HubTheme");
var Card_1 = require("XtiShared/Card/Card");
var TextCss_1 = require("XtiShared/TextCss");
var UserEditPanel = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserEditPanel, _super);
    function UserEditPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.cancelCommand = new Command_1.Command(_this.cancel.bind(_this));
        _this.saveCommand = new AsyncCommand_1.AsyncCommand(_this.save.bind(_this));
        _this.height100();
        _this.setName(UserEditPanel.name);
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.alert = flexFill.container.addContent(new MessageAlert_1.MessageAlert());
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.cancelCommand.add(toolbar.columnEnd.addContent(HubTheme_1.HubTheme.instance.commandToolbar.cancelButton()));
        _this.saveCommand.add(toolbar.columnEnd.addContent(HubTheme_1.HubTheme.instance.commandToolbar.saveButton()));
        var editCard = flexFill.addContent(new Card_1.Card());
        editCard.addCardTitleHeader('Edit User');
        var cardBody = editCard.addCardBody();
        _this.editUserForm = cardBody.addContent(new EditUserForm_1.EditUserForm());
        _this.editUserForm.addOffscreenSubmit();
        _this.editUserForm.executeLayout();
        _this.editUserForm.forEachFormGroup(function (fg) {
            fg.captionColumn.setTextCss(new TextCss_1.TextCss().end().bold());
        });
        return _this;
    }
    UserEditPanel.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserEditPanel.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var userForm;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getUserForEdit(this.userID)];
                    case 1:
                        userForm = _a.sent();
                        this.editUserForm.import(userForm);
                        return [4 /*yield*/, new DelayedAction_1.DelayedAction(function () { return _this.editUserForm.PersonName.setFocus(); }, 1000).execute()];
                    case 2:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    UserEditPanel.prototype.getUserForEdit = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var userForm;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.UserMaintenance.GetUserForEdit(userID)];
                                    case 1:
                                        userForm = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, userForm];
                }
            });
        });
    };
    UserEditPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserEditPanel.prototype.cancel = function () {
        this.awaitable.resolve(new Result_1.Result(UserEditPanel.ResultKeys.canceled));
    };
    UserEditPanel.prototype.save = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.editUserForm.save(this.hubApi.UserMaintenance.EditUserAction)];
                    case 1:
                        result = _a.sent();
                        if (result.succeeded()) {
                            this.awaitable.resolve(new Result_1.Result(UserEditPanel.ResultKeys.saved));
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    UserEditPanel.ResultKeys = {
        canceled: 'canceled',
        saved: 'saved'
    };
    return UserEditPanel;
}(Block_1.Block));
exports.UserEditPanel = UserEditPanel;
//# sourceMappingURL=UserEditPanel.js.map