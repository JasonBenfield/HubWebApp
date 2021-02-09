"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ko = require("knockout");
var Events_1 = require("../Events");
var HtmlComponentViewModel_1 = require("../Html/HtmlComponentViewModel");
var ComponentTemplate_1 = require("../ComponentTemplate");
var template = require("./ListBlock.html");
var ListBlockViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ListBlockViewModel, _super);
    function ListBlockViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('list-block', template)) || this;
        _this._itemClicked = new Events_1.DefaultEvent(_this);
        _this.itemClicked = _this._itemClicked.handler();
        _this.items = ko.observableArray([]);
        _this.hasItems = ko.observable(false);
        return _this;
    }
    ListBlockViewModel.prototype.click = function (item) {
        this._itemClicked.invoke(item);
    };
    return ListBlockViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.ListBlockViewModel = ListBlockViewModel;
//# sourceMappingURL=ListBlockViewModel.js.map