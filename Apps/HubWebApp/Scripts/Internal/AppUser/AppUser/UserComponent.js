"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponent = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("XtiShared/Card/CardAlert");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var Row_1 = require("XtiShared/Grid/Row");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var CardTitleHeader_1 = require("XtiShared/Card/CardTitleHeader");
var CardBody_1 = require("XtiShared/Card/CardBody");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var UserComponent = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserComponent, _super);
    function UserComponent(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.addContent(new CardTitleHeader_1.CardTitleHeader('User'));
        _this.alert = _this.addContent(new CardAlert_1.CardAlert()).alert;
        _this.cardBody = _this.addContent(new CardBody_1.CardBody());
        var row = _this.cardBody.addContent(new Row_1.Row());
        _this.userName = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new TextSpan_1.TextSpan('User'));
        _this.fullName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.cardBody.hide();
        return _this;
    }
    UserComponent.prototype.setUserID = function (userID) {
        this.userID = userID;
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
                        this.fullName.setText(user.Name);
                        this.cardBody.show();
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