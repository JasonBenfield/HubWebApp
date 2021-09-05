"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Block_1 = require("XtiShared/Html/Block");
var Container_1 = require("XtiShared/Html/Container");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var TextHeading1_1 = require("XtiShared/Html/TextHeading1");
var PaddingCss_1 = require("XtiShared/PaddingCss");
var xtistart_1 = require("xtistart");
var HubAppApi_1 = require("../Api/HubAppApi");
var LoginComponent_1 = require("./LoginComponent");
var LoginPage = /** @class */ (function () {
    function LoginPage(page) {
        this.page = page;
        this.hubApi = this.page.api(HubAppApi_1.HubAppApi);
        var flexColumn = this.page.addContent(new FlexColumn_1.FlexColumn());
        flexColumn
            .addContent(new Block_1.Block())
            .addContent(new Container_1.Container())
            .addContent(new TextHeading1_1.TextHeading1('Login'));
        flexColumn
            .addContent(new FlexColumnFill_1.FlexColumnFill())
            .container
            .configure(function (c) { return c.setPadding(PaddingCss_1.PaddingCss.top(3)); })
            .addContent(new LoginComponent_1.LoginComponent(this.hubApi));
    }
    return LoginPage;
}());
new LoginPage(new xtistart_1.Startup().build());
//# sourceMappingURL=LoginPage.js.map