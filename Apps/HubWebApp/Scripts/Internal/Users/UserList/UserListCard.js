"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var Card_1 = require("XtiShared/Card/Card");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var UserListCard = /** @class */ (function (_super) {
    tslib_1.__extends(UserListCard, _super);
    function UserListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this._userSelected = new Events_1.DefaultEvent(_this);
        _this.userSelected = _this._userSelected.handler();
        _this.setName(UserListCard.name);
        _this.addCardTitleHeader('Users');
        _this.alert = _this.addCardAlert().alert;
        _this.users = _this.addButtonListGroup();
        _this.users.itemClicked.register(_this.onUserClicked.bind(_this));
        return _this;
    }
    UserListCard.prototype.onUserClicked = function (listItem) {
        this._userSelected.invoke(listItem.getData());
    };
    UserListCard.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var users;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getUsers()];
                    case 1:
                        users = _a.sent();
                        this.users.setItems(users, function (user, listItem) {
                            listItem.setData(user);
                            var row = listItem.addContent(new Row_1.Row());
                            row.addColumn()
                                .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs(4)); })
                                .addContent(new TextSpan_1.TextSpan(user.UserName));
                            row.addColumn()
                                .addContent(new TextSpan_1.TextSpan(user.Name));
                        });
                        if (users.length === 0) {
                            this.alert.danger('No Users were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    UserListCard.prototype.getUsers = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var users;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.Users.GetUsers()];
                                    case 1:
                                        users = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, users];
                }
            });
        });
    };
    return UserListCard;
}(Card_1.Card));
exports.UserListCard = UserListCard;
//# sourceMappingURL=UserListCard.js.map