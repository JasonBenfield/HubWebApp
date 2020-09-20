"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PageLoader = void 0;
var ko = require("knockout");
var template = require("./Templates/PageFrame.html");
require("./Styles/default.scss");
var ComponentTemplate_1 = require("./ComponentTemplate");
require("tslib");
var SubmitBindingHandler_1 = require("./SubmitBindingHandler");
var ModalBindingHandler_1 = require("./ModalBindingHandler");
var PageLoader = /** @class */ (function () {
    function PageLoader() {
    }
    PageLoader.prototype.load = function (pageVM) {
        new ComponentTemplate_1.ComponentTemplate('page-frame', template).register();
        ko.options.deferUpdates = true;
        ko.bindingHandlers.submit = new SubmitBindingHandler_1.SubmitBindingHandler();
        ko.bindingHandlers.modal = new ModalBindingHandler_1.ModalBindingHandler();
        ko.applyBindings(pageVM);
    };
    return PageLoader;
}());
exports.PageLoader = PageLoader;
//# sourceMappingURL=PageLoader.js.map