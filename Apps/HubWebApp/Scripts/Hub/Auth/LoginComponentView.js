"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoginComponentView = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var ButtonCommandItem_1 = require("@jasonbenfield/sharedwebapp/Command/ButtonCommandItem");
var ContextualClass_1 = require("@jasonbenfield/sharedwebapp/ContextualClass");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var MessageAlertView_1 = require("@jasonbenfield/sharedwebapp/MessageAlertView");
var TextCss_1 = require("@jasonbenfield/sharedwebapp/TextCss");
var VerifyLoginFormView_1 = require("../Api/VerifyLoginFormView");
var LoginComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(LoginComponentView, _super);
    function LoginComponentView() {
        var _this = _super.call(this) || this;
        _this.verifyLoginForm = _this.addContent(new VerifyLoginFormView_1.VerifyLoginFormView());
        _this.verifyLoginForm = _this.addContent(new VerifyLoginFormView_1.VerifyLoginFormView());
        var commandBlock = _this.addContent(new Block_1.Block());
        commandBlock.addCssFrom(new TextCss_1.TextCss().end().cssClass());
        commandBlock.setMargin(MarginCss_1.MarginCss.bottom(3));
        _this.alert = _this.addContent(new MessageAlertView_1.MessageAlertView());
        _this.addCssName("container");
        _this.verifyLoginForm.forEachFormGroup(function (fg) {
            fg.captionColumn.setColumnCss(ColumnCss_1.ColumnCss.xs(3));
        });
        _this.verifyLoginForm.addOffscreenSubmit();
        _this.formSubmitted = _this.verifyLoginForm.submitted;
        _this.verifyLoginForm.executeLayout();
        new DelayedAction_1.DelayedAction(function () {
            _this.verifyLoginForm.UserName.input.setFocus();
        }, 100).execute();
        _this.loginButton = commandBlock.addContent(new ButtonCommandItem_1.ButtonCommandItem());
        _this.loginButton.setContext(ContextualClass_1.ContextualClass.primary);
        _this.loginButton.setText('Login');
        _this.loginButton.icon.solidStyle();
        _this.loginButton.icon.setName('sign-in-alt');
        return _this;
    }
    LoginComponentView.prototype.setFocusOnUserName = function () { this.verifyLoginForm.UserName.input.setFocus(); };
    return LoginComponentView;
}(Block_1.Block));
exports.LoginComponentView = LoginComponentView;
//# sourceMappingURL=LoginComponentView.js.map