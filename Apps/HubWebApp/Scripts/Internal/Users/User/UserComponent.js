"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponent = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var Command_1 = require("XtiShared/Command/Command");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var Row_1 = require("XtiShared/Grid/Row");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var FormGroup_1 = require("XtiShared/Html/FormGroup");
var TextCss_1 = require("XtiShared/TextCss");
var HubTheme_1 = require("../../HubTheme");
var UserComponent = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserComponent, _super);
    function UserComponent(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this._editRequested = new Events_1.DefaultEvent(_this);
        _this.editRequested = _this._editRequested.handler();
        _this.editCommand = new Command_1.Command(_this.requestEdit.bind(_this));
        _this.setName(UserComponent.name);
        var headerRow = _this.addCardHeader()
            .addContent(new Row_1.Row());
        headerRow.addColumn()
            .addContent(new TextSpan_1.TextSpan('User'));
        var editButton = _this.editCommand.add(headerRow.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(HubTheme_1.HubTheme.instance.cardHeader.editButton()));
        _this.alert = _this.addCardAlert().alert;
        var body = _this.addCardBody();
        _this.userName = _this.addBodyRow(body, 'User Name');
        _this.fullName = _this.addBodyRow(body, 'Name');
        _this.email = _this.addBodyRow(body, 'Email');
        return _this;
    }
    UserComponent.prototype.addBodyRow = function (body, caption) {
        var row = body.addContent(new FormGroup_1.FormGroup());
        row.captionColumn.addContent(new TextSpan_1.TextSpan(caption));
        row.captionColumn.setColumnCss(ColumnCss_1.ColumnCss.xs(4));
        row.valueColumn.setTextCss(new TextCss_1.TextCss().truncate());
        return row.valueColumn.addContent(new TextSpan_1.TextSpan())
            .configure(function (ts) { return ts.addCssName('form-control-plaintext'); });
    };
    UserComponent.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserComponent.prototype.requestEdit = function () {
        this._editRequested.invoke(this.userID);
    };
    UserComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var user;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getUser(this.userID)];
                    case 1:
                        user = _a.sent();
                        this.userName.setText(user.UserName);
                        this.userName.setTitleFromText();
                        this.fullName.setText(user.Name);
                        this.fullName.setTitleFromText();
                        this.email.setText(user.Email);
                        this.email.setTitleFromText();
                        return [2 /*return*/];
                }
            });
        });
    };
    UserComponent.prototype.getUser = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var user;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.UserInquiry.GetUser(userID)];
                                    case 1:
                                        user = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, user];
                }
            });
        });
    };
    return UserComponent;
}(Card_1.Card));
exports.UserComponent = UserComponent;
//# sourceMappingURL=UserComponent.js.map