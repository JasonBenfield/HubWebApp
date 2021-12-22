"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LoginPageView = void 0;
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var Container_1 = require("@jasonbenfield/sharedwebapp/Html/Container");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var TextHeading1_1 = require("@jasonbenfield/sharedwebapp/Html/TextHeading1");
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var LoginComponentView_1 = require("./LoginComponentView");
var LoginPageView = /** @class */ (function () {
    function LoginPageView(page) {
        this.page = page;
        var flexColumn = this.page.addContent(new FlexColumn_1.FlexColumn());
        flexColumn
            .addContent(new Block_1.Block())
            .addContent(new Container_1.Container())
            .addContent(new TextHeading1_1.TextHeading1('Login'));
        this.loginComponent = flexColumn
            .addContent(new FlexColumnFill_1.FlexColumnFill())
            .container
            .configure(function (c) { return c.setPadding(PaddingCss_1.PaddingCss.top(3)); })
            .addContent(new LoginComponentView_1.LoginComponentView());
    }
    return LoginPageView;
}());
exports.LoginPageView = LoginPageView;
//# sourceMappingURL=LoginPageView.js.map