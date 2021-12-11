"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PageFrame = void 0;
var ContextualClass_1 = require("./ContextualClass");
var AggregateComponent_1 = require("./Html/AggregateComponent");
var DropdownBlock_1 = require("./Dropdown/DropdownBlock");
var FlexColumn_1 = require("./Html/FlexColumn");
var Heading1_1 = require("./Html/Heading1");
var TextSmall_1 = require("./Html/TextSmall");
var TextSpan_1 = require("./Html/TextSpan");
var Toolbar_1 = require("./Html/Toolbar");
var PageViewModel_1 = require("./PageViewModel");
var FaIcon_1 = require("./FaIcon");
var ModalErrorComponent_1 = require("./Error/ModalErrorComponent");
var PaddingCss_1 = require("./PaddingCss");
var Block_1 = require("./Html/Block");
var PageLoader_1 = require("./PageLoader");
var AlignCss_1 = require("./AlignCss");
var AppApiFactory_1 = require("./AppApiFactory");
var TextCss_1 = require("./TextCss");
var PageFrame = /** @class */ (function () {
    function PageFrame(vm) {
        if (vm === void 0) { vm = new PageViewModel_1.PageViewModel(); }
        this.vm = vm;
        this.outerContent = new AggregateComponent_1.AggregateComponent(this.vm.content);
        this.modalError = new ModalErrorComponent_1.ModalErrorComponent(this.vm.modalError);
        this.apiFactory = new AppApiFactory_1.AppApiFactory();
        var frame = this.outerContent.addContent(new FlexColumn_1.FlexColumn());
        frame.setName('PageFrame');
        frame.flexFill();
        this.toolbar = frame.addContent(new Toolbar_1.Toolbar());
        this.toolbar.setName('PageFrame_MainToolbar');
        this.toolbar.setBackgroundContext(ContextualClass_1.ContextualClass.primary);
        this.toolbar.setPadding(PaddingCss_1.PaddingCss.xs(3));
        this.toolbar.columnStart.setTextCss(new TextCss_1.TextCss().context(ContextualClass_1.ContextualClass.light));
        var heading = this.toolbar.columnStart.addContent(new Heading1_1.Heading1());
        this.appTitle = heading.addContent(new TextSpan_1.TextSpan(pageContext.AppTitle));
        this.pageTitle = heading.addContent(new TextSmall_1.TextSmall(pageContext.PageTitle));
        this.toolbar.columnEnd.addCssFrom(new AlignCss_1.AlignCss().self(function (a) { return a.xs('center'); }).cssClass());
        var dropdown = this.toolbar.columnEnd.addContent(new DropdownBlock_1.DropdownBlock());
        if (!pageContext.IsAuthenticated) {
            dropdown.hide();
        }
        dropdown.button.setContext(ContextualClass_1.ContextualClass.light);
        dropdown.button.useOutlineStyle();
        dropdown.button.addContent(new FaIcon_1.FaIcon('user'));
        dropdown
            .addSpanItem()
            .span
            .addContent(new TextSpan_1.TextSpan(pageContext.UserName));
        this.logoutMenuItem = dropdown.addLinkItem();
        this.logoutMenuItem.link.addContent(new TextSpan_1.TextSpan('Logout'));
        this.logoutMenuItem.link.setHref(pageContext.BaseUrl + "/Hub/Current/Auth/Logout");
        this.content = frame.addContent(new Block_1.Block());
        this.content.flexFill();
        this.content.addCssName('h-100');
        this.content.setName('PageFrame_Content');
        var documentTitle = pageContext.AppTitle;
        if (pageContext.PageTitle) {
            documentTitle = documentTitle + " - " + pageContext.PageTitle;
        }
        document.title = documentTitle;
    }
    PageFrame.prototype.setName = function (name) {
    };
    PageFrame.prototype.addItem = function (itemVM, item) {
        return this.content.addItem(itemVM, item);
    };
    PageFrame.prototype.insertItem = function (index, itemVM, item) {
        return this.content.insertItem(index, itemVM, item);
    };
    PageFrame.prototype.removeItem = function (item) {
        return this.content.removeItem(item);
    };
    PageFrame.prototype.show = function () {
        this.content.show();
    };
    PageFrame.prototype.hide = function () {
        this.content.hide();
    };
    PageFrame.prototype.insertContent = function (index, item) {
        return item.insertIntoContainer(this.content, index);
    };
    PageFrame.prototype.addContent = function (item) {
        return item.addToContainer(this.content);
    };
    PageFrame.prototype.load = function () {
        new PageLoader_1.PageLoader().loadPage(this.vm);
    };
    PageFrame.prototype.defaultApi = function () {
        return this.apiFactory.defaultApi(this.modalError);
    };
    PageFrame.prototype.setDefaultApiType = function (defaultApi) {
        this.apiFactory.defaultApiType = defaultApi;
    };
    PageFrame.prototype.api = function (apiCtor) {
        return this.apiFactory.api(apiCtor, this.modalError);
    };
    return PageFrame;
}());
exports.PageFrame = PageFrame;
//# sourceMappingURL=PageFrame.js.map