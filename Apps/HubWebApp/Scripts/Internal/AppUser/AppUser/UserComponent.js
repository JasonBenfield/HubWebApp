"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponent = void 0;
var tslib_1 = require("tslib");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var UserComponent = /** @class */ (function () {
    function UserComponent(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new CardTitleHeader_1.CardTitleHeader('User', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
    }
    UserComponent.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var user;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getUser(this.userID)];
                    case 1:
                        user = _a.sent();
                        this.view.setUserName(user.UserName);
                        this.view.setFullName(user.Name);
                        this.view.showCardBody();
                        return [2 /*return*/];
                }
            });
        });
    };
    UserComponent.prototype.getUser = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var user;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
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