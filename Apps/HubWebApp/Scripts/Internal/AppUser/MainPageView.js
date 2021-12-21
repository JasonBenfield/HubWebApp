"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MainPageView = void 0;
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var AppUserPanelView_1 = require("./AppUser/AppUserPanelView");
var UserRolePanelView_1 = require("./UserRoles/UserRolePanelView");
var MainPageView = /** @class */ (function () {
    function MainPageView(page) {
        this.page = page;
        this.appUserPanel = this.page.addContent(new AppUserPanelView_1.AppUserPanelView());
        this.userRolePanel = this.page.addContent(new UserRolePanelView_1.UserRolePanelView());
        this.page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
    }
    return MainPageView;
}());
exports.MainPageView = MainPageView;
//# sourceMappingURL=MainPageView.js.map