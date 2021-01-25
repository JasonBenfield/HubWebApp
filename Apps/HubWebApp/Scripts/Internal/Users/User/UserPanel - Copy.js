"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("../../../../Imports/Shared/Awaitable");
var Command_1 = require("../../../../Imports/Shared/Command");
var Result_1 = require("../../../../Imports/Shared/Result");
var UserPanel = /** @class */ (function () {
    function UserPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.vm.backCommand, this.back.bind(this));
        var icon = this.backCommand.icon();
        icon.setName('fa-caret-left');
        this.backCommand.setText('Users');
        this.backCommand.makeLight();
    }
    UserPanel.prototype.setUserID = function (userID) {
    };
    UserPanel.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            return tslib_1.__generator(this, function (_a) {
                return [2 /*return*/];
            });
        });
    };
    UserPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserPanel.prototype.back = function () {
        this.awaitable.resolve(new Result_1.Result(UserPanel.ResultKeys.backRequested));
    };
    UserPanel.ResultKeys = {
        backRequested: 'back-requested'
    };
    return UserPanel;
}());
exports.UserPanel = UserPanel;
//# sourceMappingURL=UserPanel.js.map