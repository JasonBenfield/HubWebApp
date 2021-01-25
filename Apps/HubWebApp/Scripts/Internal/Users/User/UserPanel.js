"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserPanel = void 0;
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command");
var Result_1 = require("XtiShared/Result");
var AppListCard_1 = require("../../Apps/AppListCard");
var UserComponent_1 = require("./UserComponent");
var UserPanel = /** @class */ (function () {
    function UserPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.userComponent = new UserComponent_1.UserComponent(this.vm.userComponent, this.hubApi);
        this.appListCard = new AppListCard_1.AppListCard(this.vm.appListCard, this.hubApi);
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.vm.backCommand, this.back.bind(this));
        var icon = this.backCommand.icon();
        icon.setName('fa-caret-left');
        this.backCommand.setText('Users');
        this.backCommand.makeLight();
        this.appListCard.setTitle('App Permissions');
        this.appListCard.appSelected.register(this.onAppSelected.bind(this));
    }
    UserPanel.prototype.onAppSelected = function (app) {
        this.awaitable.resolve(new Result_1.Result(UserPanel.ResultKeys.appSelected, app));
    };
    UserPanel.prototype.setUserID = function (userID) {
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
        this.awaitable.resolve(new Result_1.Result(UserPanel.ResultKeys.backRequested));
    };
    UserPanel.ResultKeys = {
        backRequested: 'back-requested',
        appSelected: 'app-selected'
    };
    return UserPanel;
}());
exports.UserPanel = UserPanel;
//# sourceMappingURL=UserPanel.js.map