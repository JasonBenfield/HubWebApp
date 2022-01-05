"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponent = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextValueFormGroup_1 = require("@jasonbenfield/sharedwebapp/Html/TextValueFormGroup");
var UserComponent = /** @class */ (function () {
    function UserComponent(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this._editRequested = new Events_1.DefaultEvent(this);
        this.editRequested = this._editRequested.handler();
        this.editCommand = new Command_1.Command(this.requestEdit.bind(this));
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
        this.userName = new TextValueFormGroup_1.TextValueFormGroup(view.userName);
        this.userName.setCaption('User Name');
        this.userName.syncValueTitleWithText();
        this.fullName = new TextValueFormGroup_1.TextValueFormGroup(view.fullName);
        this.fullName.setCaption('Name');
        this.fullName.syncValueTitleWithText();
        this.email = new TextValueFormGroup_1.TextValueFormGroup(view.email);
        this.email.setCaption('Email');
        this.email.syncValueTitleWithText();
        this.editCommand.add(this.view.editButton);
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
                        this.userName.setValue(user.UserName);
                        this.fullName.setValue(user.Name);
                        this.email.setValue(user.Email);
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