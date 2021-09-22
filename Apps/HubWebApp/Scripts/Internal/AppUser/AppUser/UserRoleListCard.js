"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRoleListCard = void 0;
var tslib_1 = require("tslib");
var AlignCss_1 = require("XtiShared/AlignCss");
var Card_1 = require("XtiShared/Card/Card");
var CardAlert_1 = require("XtiShared/Card/CardAlert");
var CardHeader_1 = require("XtiShared/Card/CardHeader");
var CardListGroup_1 = require("XtiShared/Card/CardListGroup");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var Command_1 = require("XtiShared/Command/Command");
var Events_1 = require("XtiShared/Events");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var HubTheme_1 = require("../../HubTheme");
var UserRoleListCard = /** @class */ (function (_super) {
    tslib_1.__extends(UserRoleListCard, _super);
    function UserRoleListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this._editRequested = new Events_1.SimpleEvent(_this);
        _this.editRequested = _this._editRequested.handler();
        _this.editCommand = new Command_1.Command(_this.requestEdit.bind(_this));
        var row = _this.addContent(new CardHeader_1.CardHeader())
            .addContent(new Row_1.Row())
            .configure(function (r) { return r.addCssFrom(new AlignCss_1.AlignCss().items(function (a) { return a.xs('baseline'); }).cssClass()); });
        row.addColumn()
            .addContent(new TextSpan_1.TextSpan('User Roles'));
        var editCommand = row.addColumn()
            .configure(function (col) { return col.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(HubTheme_1.HubTheme.instance.cardHeader.editButton());
        _this.editCommand.add(editCommand);
        _this.alert = _this.addContent(new CardAlert_1.CardAlert()).alert;
        _this.roles = _this.addContent(new CardListGroup_1.CardListGroup());
        return _this;
    }
    UserRoleListCard.prototype.requestEdit = function () {
        this._editRequested.invoke();
    };
    UserRoleListCard.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserRoleListCard.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var roles;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getRoles()];
                    case 1:
                        roles = _a.sent();
                        this.roles.setItems(roles, function (role, listItem) {
                            listItem.addContent(new Row_1.Row())
                                .addColumn()
                                .addContent(new TextSpan_1.TextSpan(role.Name));
                        });
                        if (roles.length === 0) {
                            this.alert.danger('No Roles were Found for User');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    UserRoleListCard.prototype.getRoles = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var roles;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.AppUser.GetUserRoles({
                                            UserID: this.userID,
                                            ModifierID: 0
                                        })];
                                    case 1:
                                        roles = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, roles];
                }
            });
        });
    };
    return UserRoleListCard;
}(Card_1.Card));
exports.UserRoleListCard = UserRoleListCard;
//# sourceMappingURL=UserRoleListCard.js.map