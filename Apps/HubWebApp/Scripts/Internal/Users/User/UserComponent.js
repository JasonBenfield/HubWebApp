"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponent = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var Events_1 = require("XtiShared/Events");
var Command_1 = require("../../../../Imports/Shared/Command");
var UserComponent = /** @class */ (function () {
    function UserComponent(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this._editRequested = new Events_1.DefaultEvent(this);
        this.editRequested = this._editRequested.handler();
        this.editCommand = new Command_1.Command(this.vm.editCommand, this.requestEdit.bind(this));
        this.alert = new Alert_1.Alert(this.vm.alert);
        var icon = this.editCommand.icon();
        icon.setName('fa-edit');
        this.editCommand.setText('Edit');
        this.editCommand.makePrimary();
    }
    UserComponent.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserComponent.prototype.requestEdit = function () {
        this._editRequested.invoke(this.userID);
    };
    UserComponent.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var user;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getUser(this.userID)];
                    case 1:
                        user = _a.sent();
                        this.vm.userName(user.UserName);
                        this.vm.name(user.Name);
                        this.vm.email(user.Email);
                        return [2 /*return*/];
                }
            });
        });
    };
    UserComponent.prototype.getUser = function (userID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var user;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
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
}());
exports.UserComponent = UserComponent;
//# sourceMappingURL=UserComponent.js.map