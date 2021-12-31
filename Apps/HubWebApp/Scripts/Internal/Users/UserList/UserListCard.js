"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var UserListItem_1 = require("./UserListItem");
var UserListCard = /** @class */ (function () {
    function UserListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this._userSelected = new Events_1.DefaultEvent(this);
        this.userSelected = this._userSelected.handler();
        new TextBlock_1.TextBlock('Users', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.users = new ListGroup_1.ListGroup(this.view.users);
        this.users.itemClicked.register(this.onUserClicked.bind(this));
    }
    UserListCard.prototype.onUserClicked = function (listItem) {
        this._userSelected.invoke(listItem.user);
    };
    UserListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var users;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getUsers()];
                    case 1:
                        users = _a.sent();
                        this.users.setItems(users, function (user, listItem) {
                            return new UserListItem_1.UserListItem(user, listItem);
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
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var users;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
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
}());
exports.UserListCard = UserListCard;
//# sourceMappingURL=UserListCard.js.map