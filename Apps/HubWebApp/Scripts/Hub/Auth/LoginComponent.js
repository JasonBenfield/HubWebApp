"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoginComponent = exports.LoginResult = void 0;
var tslib_1 = require("tslib");
var AsyncCommand_1 = require("XtiShared/Command/AsyncCommand");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var UrlBuilder_1 = require("XtiShared/UrlBuilder");
var VerifyLoginForm_1 = require("../Api/VerifyLoginForm");
var DelayedAction_1 = require("XtiShared/DelayedAction");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var Block_1 = require("XtiShared/Html/Block");
var MessageAlert_1 = require("XtiShared/MessageAlert");
var TextCss_1 = require("XtiShared/TextCss");
var MarginCss_1 = require("XtiShared/MarginCss");
var ButtonCommandItem_1 = require("XtiShared/Command/ButtonCommandItem");
var ContextualClass_1 = require("XtiShared/ContextualClass");
var LoginResult = /** @class */ (function () {
    function LoginResult(token) {
        this.token = token;
    }
    return LoginResult;
}());
exports.LoginResult = LoginResult;
var LoginComponent = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(LoginComponent, _super);
    function LoginComponent(authApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.authApi = authApi;
        _this.verifyLoginForm = _this.addContent(new VerifyLoginForm_1.VerifyLoginForm());
        _this.loginCommand = new AsyncCommand_1.AsyncCommand(_this.login.bind(_this));
        _this.commandBlock = _this.addContent(new Block_1.Block())
            .configure(function (b) {
            b.addCssFrom(new TextCss_1.TextCss().end().cssClass());
            b.setMargin(MarginCss_1.MarginCss.bottom(3));
        });
        _this.alert = _this.addContent(new MessageAlert_1.MessageAlert());
        _this.addCssName("container");
        _this.verifyLoginForm.forEachFormGroup(function (fg) {
            fg.captionColumn.setColumnCss(ColumnCss_1.ColumnCss.xs(3));
        });
        _this.verifyLoginForm.addOffscreenSubmit();
        _this.verifyLoginForm.submitted.register(_this.onSubmit.bind(_this));
        _this.verifyLoginForm.executeLayout();
        new DelayedAction_1.DelayedAction(function () {
            _this.verifyLoginForm.UserName.setFocus();
        }, 100).execute();
        var loginButton = _this.loginCommand.add(_this.commandBlock.addContent(new ButtonCommandItem_1.ButtonCommandItem()));
        loginButton.setContext(ContextualClass_1.ContextualClass.primary);
        loginButton.setText('Login');
        loginButton.icon.solidStyle();
        loginButton.icon.setName('sign-in-alt');
        return _this;
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
    LoginComponent.ResultKeys = {
        loginComplete: 'login-complete'
    };
    return LoginComponent;
}(Block_1.Block));
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=LoginComponent.js.map