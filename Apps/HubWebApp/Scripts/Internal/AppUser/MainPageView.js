"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MainPageView = void 0;
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var AddRolePanelView_1 = require("./AddRolePanelView");
var AppUserDataPanelView_1 = require("./AppUserDataPanelView");
var SelectModCategoryPanelView_1 = require("./SelectModCategoryPanelView");
var SelectModifierPanelView_1 = require("./SelectModifierPanelView");
var UserRolesPanelView_1 = require("./UserRolesPanelView");
var MainPageView = /** @class */ (function () {
    function MainPageView(page) {
        this.page = page;
        this.page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
        this.appUserDataPanel = this.page.addContent(new AppUserDataPanelView_1.AppUserDataPanelView());
        this.selectModCategoryPanel = this.page.addContent(new SelectModCategoryPanelView_1.SelectModCategoryPanelView());
        this.selectModifierPanel = this.page.addContent(new SelectModifierPanelView_1.SelectModifierPanelView());
        this.userRolesPanel = this.page.addContent(new UserRolesPanelView_1.UserRolesPanelView());
        this.addRolePanel = this.page.addContent(new AddRolePanelView_1.AddRolePanelView());
    }
    return MainPageView;
}());
exports.MainPageView = MainPageView;
//# sourceMappingURL=MainPageView.js.map