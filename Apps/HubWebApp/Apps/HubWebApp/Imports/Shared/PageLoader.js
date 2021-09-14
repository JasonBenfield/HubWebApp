"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PageLoader = void 0;
var ko = require("knockout");
require("./Styles/default.scss");
require("@fortawesome/fontawesome-free/css/all.css");
require("@fortawesome/fontawesome-free/webfonts/fa-brands-400.eot");
require("@fortawesome/fontawesome-free/webfonts/fa-brands-400.svg");
require("@fortawesome/fontawesome-free/webfonts/fa-brands-400.ttf");
require("@fortawesome/fontawesome-free/webfonts/fa-brands-400.woff");
require("@fortawesome/fontawesome-free/webfonts/fa-brands-400.woff2");
require("@fortawesome/fontawesome-free/webfonts/fa-regular-400.eot");
require("@fortawesome/fontawesome-free/webfonts/fa-regular-400.svg");
require("@fortawesome/fontawesome-free/webfonts/fa-regular-400.ttf");
require("@fortawesome/fontawesome-free/webfonts/fa-regular-400.woff");
require("@fortawesome/fontawesome-free/webfonts/fa-regular-400.woff2");
require("@fortawesome/fontawesome-free/webfonts/fa-solid-900.eot");
require("@fortawesome/fontawesome-free/webfonts/fa-solid-900.svg");
require("@fortawesome/fontawesome-free/webfonts/fa-solid-900.ttf");
require("@fortawesome/fontawesome-free/webfonts/fa-solid-900.woff");
require("@fortawesome/fontawesome-free/webfonts/fa-solid-900.woff2");
require("tslib");
var SubmitBindingHandler_1 = require("./SubmitBindingHandler");
var ModalBindingHandler_1 = require("./ModalBindingHandler");
var UrlBuilder_1 = require("./UrlBuilder");
var ConsoleLog_1 = require("./ConsoleLog");
var DropdownBindingHandler_1 = require("./DropdownBindingHandler");
var DelegatedEventBindingHandler_1 = require("./DelegatedEventBindingHandler");
var PageLoader = /** @class */ (function () {
    function PageLoader() {
    }
    PageLoader.prototype.loadPage = function (pageVM) {
        var _this = this;
        var defaultConfigLoader = {
            getConfig: function (name, callback) {
                if (name.indexOf('/') > -1) {
                    callback({
                        template: { templateUrl: name },
                        synchronous: true
                    });
                }
                else {
                    callback(null);
                }
            }
        };
        var defaultComponentLoader = {
            loadComponent: function (name, config, callback) {
                var templateConfig = config.template;
                if (templateConfig.templateUrl) {
                    _this.loadFromTemplateUrl(templateConfig.templateUrl, callback, templateConfig.errorMarkup);
                }
                else if (templateConfig.containerID) {
                    var container = document.getElementById(templateConfig.containerID);
                    callback({
                        template: container && container.childNodes,
                        createViewModel: _this.createViewModel
                    });
                }
                else {
                    callback(null);
                }
            }
        };
        ko.components.loaders.unshift(defaultConfigLoader);
        ko.components.loaders.unshift(defaultComponentLoader);
        ko.options.deferUpdates = true;
        ko.bindingHandlers.submit = new SubmitBindingHandler_1.SubmitBindingHandler();
        ko.bindingHandlers.modal = new ModalBindingHandler_1.ModalBindingHandler();
        ko.bindingHandlers.dropdown = new DropdownBindingHandler_1.DropdownBindingHandler();
        ko.bindingHandlers.delegatedEvent = new DelegatedEventBindingHandler_1.DelegatedEventBindingHandler();
        ko.applyBindings(pageVM);
    };
    PageLoader.prototype.createViewModel = function (params, componentInf) {
        return params;
    };
    PageLoader.prototype.loadFromTemplateUrl = function (templateUrl, callback, getErrorMarkup) {
        var _this = this;
        var urlBuilder = new UrlBuilder_1.UrlBuilder(templateUrl);
        if (!urlBuilder.hasQuery('cacheBust')) {
            urlBuilder.addQuery('cacheBust', pageContext.CacheBust);
        }
        var url = urlBuilder.value();
        function reqListener() {
            console.log(this.responseText);
        }
        var oReq = new XMLHttpRequest();
        oReq.withCredentials = true;
        oReq.onreadystatechange = function () {
            if (oReq.readyState == 4) {
                if (oReq.status === 200) {
                    var template = ko.utils.parseHtmlFragment(oReq.responseText);
                    callback({
                        template: template,
                        createViewModel: _this.createViewModel
                    });
                }
                else {
                    new ConsoleLog_1.ConsoleLog().error("Error loading " + url + "\r\nStatus: " + oReq.status + "\r\n" + oReq.responseText);
                    callback({
                        template: '',
                        createViewModel: function (params, componentInfo) {
                            return params;
                        }
                    });
                    ko.components.clearCachedDefinition(templateUrl);
                }
            }
        };
        oReq.addEventListener("load", reqListener.bind(oReq));
        oReq.open('GET', url);
        oReq.setRequestHeader("cache-control", "private");
        oReq.send('');
    };
    return PageLoader;
}());
exports.PageLoader = PageLoader;
//# sourceMappingURL=PageLoader.js.map