"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ko = require("knockout");
var Url_1 = require("./Url");
var UrlBuilder_1 = require("./UrlBuilder");
var ComponentTemplateAsync = /** @class */ (function () {
    function ComponentTemplateAsync(name, url) {
        this.name = name;
        if (url instanceof Url_1.Url) {
            this.url = url.value();
        }
        else if (url instanceof UrlBuilder_1.UrlBuilder) {
            this.url = url.value();
        }
        else {
            this.url = url;
        }
    }
    ComponentTemplateAsync.prototype.register = function () {
        if (!ko.components.isRegistered(this.name)) {
            ko.components.register(this.name, {
                template: {
                    templateUrl: this.url
                }
            });
        }
    };
    return ComponentTemplateAsync;
}());
exports.ComponentTemplateAsync = ComponentTemplateAsync;
//# sourceMappingURL=ComponentTemplateAsync.js.map