"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MainPageView = void 0;
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var UserPanelView_1 = require("./User/UserPanelView");
var UserEditPanelView_1 = require("./UserEdit/UserEditPanelView");
var UserListPanelView_1 = require("./UserList/UserListPanelView");
var MainPageView = /** @class */ (function () {
    function MainPageView(page) {
        this.page = page;
        this.userListPanel = this.page.addContent(new UserListPanelView_1.UserListPanelView());
        this.userPanel = this.page.addContent(new UserPanelView_1.UserPanelView());
        this.userEditPanel = this.page.addContent(new UserEditPanelView_1.UserEditPanelView());
        this.page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
    }
    return MainPageView;
}());
exports.MainPageView = MainPageView;
//# sourceMappingURL=MainPageView.js.map