"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Link_1 = require("../Html/Link");
var ListGroupItem_1 = require("./ListGroupItem");
var LinkListGroupItem = /** @class */ (function (_super) {
    tslib_1.__extends(LinkListGroupItem, _super);
    function LinkListGroupItem(vm) {
        var _this = _super.call(this, vm) || this;
        _this.link = new Link_1.Link(vm);
        return _this;
    }
    LinkListGroupItem.prototype.setHref = function (href) {
        this.link.setHref(href);
    };
    LinkListGroupItem.prototype.enable = function () {
        this.link.enable();
    };
    LinkListGroupItem.prototype.disable = function () {
        this.link.disable();
    };
    return LinkListGroupItem;
}(ListGroupItem_1.ListGroupItem));
exports.LinkListGroupItem = LinkListGroupItem;
//# sourceMappingURL=LinkListGroupItem.js.map