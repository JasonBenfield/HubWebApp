"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoginComponent = exports.LoginResult = void 0;
var tslib_1 = require("tslib");
var AsyncCommand_1 = require("@jasonbenfield/sharedwebapp/Command/AsyncCommand");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var UrlBuilder_1 = require("@jasonbenfield/sharedwebapp/UrlBuilder");
var VerifyLoginForm_1 = require("../Api/VerifyLoginForm");
var LoginResult = /** @class */ (function () {
    function LoginResult(token) {
        this.token = token;
    }
    return LoginResult;
}());
exports.LoginResult = LoginResult;
var LoginComponent = /** @class */ (function () {
    function LoginComponent(authApi, view) {
        this.authApi = authApi;
        this.view = view;
        this.loginCommand = new AsyncCommand_1.AsyncCommand(this.login.bind(this));
        this.verifyLoginForm = new VerifyLoginForm_1.VerifyLoginForm(this.view.verifyLoginForm);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.view.formSubmitted.register(this.onSubmit.bind(this));
        this.loginCommand.add(this.view.loginButton);
    }
    LoginComponent.prototype.onSubmit = function () {
        return this.loginCommand.execute();
    };
    LoginComponent.prototype.login = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result, cred;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.alert.info('Verifying login...');
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, , 3, 4]);
                        return [4 /*yield*/, this.verifyLoginForm.save(this.authApi.Auth.VerifyLoginAction)];
                    case 2:
                        result = _a.sent();
                        if (result.succeeded()) {
                            cred = this.getCredentials();
                            this.alert.info('Opening page...');
                            this.postLogin(cred);
                        }
                        return [3 /*break*/, 4];
                    case 3:
                        this.alert.clear();
                        return [7 /*endfinally*/];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    LoginComponent.prototype.getCredentials = function () {
        return {
            UserName: this.verifyLoginForm.UserName.getValue(),
            Password: this.verifyLoginForm.Password.getValue()
        };
    };
    LoginComponent.prototype.postLogin = function (cred) {
        var form = document.createElement('form');
        form.action = this.authApi.Auth.Login
            .getUrl(null)
            .value();
        form.style.position = 'absolute';
        form.style.top = '-100px';
        form.style.left = '-100px';
        form.method = 'POST';
        var userNameInput = this.createInput('Credentials.UserName', cred.UserName, 'text');
        var passwordInput = this.createInput('Credentials.Password', cred.Password, 'password');
        var urlBuilder = UrlBuilder_1.UrlBuilder.current();
        var startUrlInput = this.createInput('StartUrl', urlBuilder.getQueryValue('startUrl'));
        var returnUrlInput = this.createInput('ReturnUrl', urlBuilder.getQueryValue('returnUrl'));
        form.append(userNameInput, passwordInput, startUrlInput, returnUrlInput);
        document.body.append(form);
        form.submit();
    };
    LoginComponent.prototype.createInput = function (name, value, type) {
        if (type === void 0) { type = 'hidden'; }
        var input = document.createElement('input');
        input.type = type;
        input.name = name;
        input.value = value;
        return input;
    };
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=LoginComponent.js.map