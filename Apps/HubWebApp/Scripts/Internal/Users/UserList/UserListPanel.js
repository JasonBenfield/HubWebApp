"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListPanel = exports.UserListPanelResult = void 0;
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var UserListCard_1 = require("./UserListCard");
var UserListPanelResult = /** @class */ (function () {
    function UserListPanelResult(results) {
        this.results = results;
    }
    UserListPanelResult.userSelected = function (user) {
        return new UserListPanelResult({ userSelected: { user: user } });
    };
    Object.defineProperty(UserListPanelResult.prototype, "userSelected", {
        get: function () { return this.results.userSelected; },
        enumerable: false,
        configurable: true
    });
    return UserListPanelResult;
}());
exports.UserListPanelResult = UserListPanelResult;
var UserListPanel = /** @class */ (function () {
    function UserListPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.userListCard = new UserListCard_1.UserListCard(this.hubApi, this.view.userListCard);
        this.userListCard.userSelected.register(this.onUserSelected.bind(this));
    }
    UserListPanel.prototype.onUserSelected = function (user) {
        this.awaitable.resolve(UserListPanelResult.userSelected(user));
    };
    UserListPanel.prototype.refresh = function () {
        return this.userListCard.refresh();
    };
    UserListPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserListPanel.prototype.activate = function () { this.view.show(); };
    UserListPanel.prototype.deactivate = function () { this.view.hide(); };
    return UserListPanel;
}());
exports.UserListPanel = UserListPanel;
//# sourceMappingURL=UserListPanel.js.map