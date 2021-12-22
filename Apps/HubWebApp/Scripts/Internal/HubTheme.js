"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.HubTheme = void 0;
var ButtonCommandItem_1 = require("@jasonbenfield/sharedwebapp/Command/ButtonCommandItem");
var ContextualClass_1 = require("@jasonbenfield/sharedwebapp/ContextualClass");
var Toolbar_1 = require("@jasonbenfield/sharedwebapp/Html/Toolbar");
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var HubTheme = /** @class */ (function () {
    function HubTheme() {
        this.cardHeader = {
            editButton: function () {
                return new ButtonCommandItem_1.ButtonCommandItem()
                    .configure(function (b) {
                    b.icon.setName('edit');
                    b.setContext(ContextualClass_1.ContextualClass.primary);
                    b.useOutlineStyle();
                    b.setText('Edit');
                    b.setTitle('Edit');
                });
            }
        };
        this.commandToolbar = {
            toolbar: function () {
                return new Toolbar_1.Toolbar()
                    .configure(function (t) {
                    t.setBackgroundContext(ContextualClass_1.ContextualClass.secondary);
                    t.setPadding(PaddingCss_1.PaddingCss.xs(3));
                });
            },
            backButton: function () {
                return new ButtonCommandItem_1.ButtonCommandItem()
                    .configure(function (b) {
                    b.icon.setName('caret-left');
                    b.setText('Back');
                    b.setTitle('Back');
                    b.setContext(ContextualClass_1.ContextualClass.light);
                    b.useOutlineStyle();
                });
            },
            cancelButton: function () {
                return new ButtonCommandItem_1.ButtonCommandItem()
                    .configure(function (b) {
                    b.icon.setName('times');
                    b.setText('Cancel');
                    b.setTitle('Cancel');
                    b.setContext(ContextualClass_1.ContextualClass.danger);
                });
            },
            saveButton: function () {
                return new ButtonCommandItem_1.ButtonCommandItem()
                    .configure(function (b) {
                    b.icon.setName('check');
                    b.setText('Save');
                    b.setTitle('Save');
                    b.setContext(ContextualClass_1.ContextualClass.primary);
                });
            }
        };
    }
    HubTheme.instance = new HubTheme();
    return HubTheme;
}());
exports.HubTheme = HubTheme;
//# sourceMappingURL=HubTheme.js.map