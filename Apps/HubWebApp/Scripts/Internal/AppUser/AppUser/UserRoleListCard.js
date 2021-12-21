"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRoleListCard = void 0;
var tslib_1 = require("tslib");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var RoleListItem_1 = require("./RoleListItem");
var UserRoleListCard = /** @class */ (function () {
    function UserRoleListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this._editRequested = new Events_1.SimpleEvent(this);
        this.editRequested = this._editRequested.handler();
        this.editCommand = new Command_1.Command(this.requestEdit.bind(this));
        this.editCommand.add(this.view.editButton);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.roles = new ListGroup_1.ListGroup(this.view.roles);
    }
    UserRoleListCard.prototype.requestEdit = function () {
        this._editRequested.invoke();
    };
    UserRoleListCard.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserRoleListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var roles;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getRoles()];
                    case 1:
                        roles = _a.sent();
                        this.roles.setItems(roles, function (role, listItem) {
                            return new RoleListItem_1.RoleListItem(role, listItem);
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
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var roles;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
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
}());
exports.UserRoleListCard = UserRoleListCard;
//# sourceMappingURL=UserRoleListCard.js.map