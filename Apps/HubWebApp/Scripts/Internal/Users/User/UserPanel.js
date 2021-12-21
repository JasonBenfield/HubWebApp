"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserPanel = exports.UserPanelResult = void 0;
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var AppListCard_1 = require("../../Apps/AppListCard");
var UserComponent_1 = require("./UserComponent");
var UserPanelResult = /** @class */ (function () {
    function UserPanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(UserPanelResult, "backRequested", {
        get: function () { return new UserPanelResult({ backRequested: {} }); },
        enumerable: false,
        configurable: true
    });
    UserPanelResult.appSelected = function (app) {
        return new UserPanelResult({ appSelected: { app: app } });
    };
    UserPanelResult.editRequested = function (userID) {
        return new UserPanelResult({ editRequested: { userID: userID } });
    };
    Object.defineProperty(UserPanelResult.prototype, "backRequested", {
        get: function () { return this.results.backRequested; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UserPanelResult.prototype, "appSelected", {
        get: function () { return this.results.appSelected; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UserPanelResult.prototype, "editRequested", {
        get: function () { return this.results.editRequested; },
        enumerable: false,
        configurable: true
    });
    return UserPanelResult;
}());
exports.UserPanelResult = UserPanelResult;
var UserPanel = /** @class */ (function () {
    function UserPanel(hubApi, view) {
        var _this = this;
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.back.bind(this));
        this.userComponent = new UserComponent_1.UserComponent(this.hubApi, this.view.userComponent);
        this.appListCard = new AppListCard_1.AppListCard(this.hubApi, function (appID) { return _this.hubApi.UserInquiry.RedirectToAppUser.getUrl({
            AppID: appID,
            UserID: _this.userID
        }).toString(); }, this.view.appListCard);
        this.backCommand.add(this.view.backButton);
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
        this.userComponent.editRequested.register(this.onEditRequested.bind(this));
    }
    UserPanel.prototype.onAppSelected = function (app) {
        this.awaitable.resolve(UserPanelResult.appSelected(app));
    };
    UserPanel.prototype.onEditRequested = function (userID) {
        this.awaitable.resolve(UserPanelResult.editRequested(userID));
    };
    UserPanel.prototype.setUserID = function (userID) {
        this.userID = userID;
        this.userComponent.setUserID(userID);
    };
    UserPanel.prototype.refresh = function () {
        var promises = [
            this.userComponent.refresh(),
            this.appListCard.refresh()
        ];
        return Promise.all(promises);
    };
    UserPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserPanel.prototype.back = function () {
        this.awaitable.resolve(UserPanelResult.backRequested);
    };
    UserPanel.prototype.activate = function () { this.view.show(); };
    UserPanel.prototype.deactivate = function () { this.view.hide(); };
    return UserPanel;
}());
exports.UserPanel = UserPanel;
//# sourceMappingURL=UserPanel.js.map