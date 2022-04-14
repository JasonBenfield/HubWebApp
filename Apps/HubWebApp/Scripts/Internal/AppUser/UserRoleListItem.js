"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRoleListItem = void 0;
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var UserRoleListItem = /** @class */ (function () {
    function UserRoleListItem(role, view) {
        this.role = role;
        this.view = view;
        this._deleteButtonClicked = new Events_1.DefaultEvent(this);
        this.deleteButtonClicked = this._deleteButtonClicked.handler();
        new TextBlock_1.TextBlock(role.Name, view.roleName);
        view.deleteButton.clicked.register(this.onDeleteButtonClicked.bind(this), this);
    }
    UserRoleListItem.prototype.hideDeleteButton = function () {
        this.view.deleteButton.hide();
    };
    UserRoleListItem.prototype.onDeleteButtonClicked = function () {
        this._deleteButtonClicked.invoke(this.role);
    };
    UserRoleListItem.prototype.dispose = function () {
        this.view.deleteButton.clicked.unregister(this);
    };
    return UserRoleListItem;
}());
exports.UserRoleListItem = UserRoleListItem;
//# sourceMappingURL=UserRoleListItem.js.map