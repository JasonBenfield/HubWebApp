"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var ListItemViewModel_1 = require("./ListItemViewModel");
var ListItem = /** @class */ (function (_super) {
    tslib_1.__extends(ListItem, _super);
    function ListItem(vm) {
        if (vm === void 0) { vm = new ListItemViewModel_1.ListItemViewModel(); }
        return _super.call(this, vm) || this;
    }
    ListItem.prototype.getData = function () { return this.data; };
    ListItem.prototype.setData = function (data) { this.data = data; };
    ListItem.prototype.addToList = function (list) {
        list.addListItem(this.vm, this);
        return this;
    };
    return ListItem;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.ListItem = ListItem;
//# sourceMappingURL=ListItem.js.map