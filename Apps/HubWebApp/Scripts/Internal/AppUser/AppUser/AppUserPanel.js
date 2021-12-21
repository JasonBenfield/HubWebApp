"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUserPanel = exports.AppUserPanelResult = void 0;
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var UserComponent_1 = require("./UserComponent");
var UserModCategoryListCard_1 = require("./UserModCategoryListCard");
var UserRoleListCard_1 = require("./UserRoleListCard");
var AppUserPanelResult = /** @class */ (function () {
    function AppUserPanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(AppUserPanelResult, "backRequested", {
        get: function () {
            return new AppUserPanelResult({ backRequested: {} });
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AppUserPanelResult, "editUserRolesRequested", {
        get: function () {
            return new AppUserPanelResult({ editUserRolesRequested: {} });
        },
        enumerable: false,
        configurable: true
    });
    AppUserPanelResult.editUserModCategoryRequested = function (userModCategory) {
        return new AppUserPanelResult({
            editUserModCategoryRequested: { userModCategory: userModCategory }
        });
    };
    Object.defineProperty(AppUserPanelResult.prototype, "backRequested", {
        get: function () { return this.results.backRequested; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AppUserPanelResult.prototype, "editUserRolesRequested", {
        get: function () { return this.results.editUserRolesRequested; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AppUserPanelResult.prototype, "editUserModCategoryRequested", {
        get: function () { return this.results.editUserModCategoryRequested; },
        enumerable: false,
        configurable: true
    });
    return AppUserPanelResult;
}());
exports.AppUserPanelResult = AppUserPanelResult;
var AppUserPanel = /** @class */ (function () {
    function AppUserPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.back.bind(this));
        this.userComponent = new UserComponent_1.UserComponent(this.hubApi, this.view.userComponent);
        this.userRoles = new UserRoleListCard_1.UserRoleListCard(this.hubApi, this.view.userRoles);
        this.userRoles.editRequested.register(this.onEditUserRolesRequested.bind(this));
        this.userModCategories = new UserModCategoryListCard_1.UserModCategoryListCard(this.hubApi, this.view.userModCategories);
        this.userModCategories.editRequested.register(this.onEditUserModCategoryRequested.bind(this));
        this.backCommand.add(this.view.backButton);
    }
    AppUserPanel.prototype.onEditUserRolesRequested = function () {
        this.awaitable.resolve(AppUserPanelResult.editUserRolesRequested);
    };
    AppUserPanel.prototype.onEditUserModCategoryRequested = function (userModCategory) {
        this.awaitable.resolve(AppUserPanelResult.editUserModCategoryRequested(userModCategory));
    };
    AppUserPanel.prototype.setUserID = function (userID) {
        this.userComponent.setUserID(userID);
        this.userRoles.setUserID(userID);
        this.userModCategories.setUserID(userID);
    };
    AppUserPanel.prototype.refresh = function () {
        var promises = [
            this.userComponent.refresh(),
            this.userRoles.refresh(),
            this.userModCategories.refresh()
        ];
        return Promise.all(promises);
    };
    AppUserPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    AppUserPanel.prototype.back = function () {
        this.awaitable.resolve(AppUserPanelResult.backRequested);
    };
    AppUserPanel.prototype.activate = function () { this.view.show(); };
    AppUserPanel.prototype.deactivate = function () { this.view.hide(); };
    return AppUserPanel;
}());
exports.AppUserPanel = AppUserPanel;
//# sourceMappingURL=AppUserPanel.js.map