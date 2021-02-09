"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./UnorderedList.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var Events_1 = require("../Events");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var UnorderedListViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(UnorderedListViewModel, _super);
    function UnorderedListViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('unordered-list', template)) || this;
        _this.items = ko.observableArray([]);
        _this.hasItems = ko.observable(false);
        _this._itemClicked = new Events_1.DefaultEvent(_this);
        _this.itemClicked = _this._itemClicked.handler();
        return _this;
    }
    UnorderedListViewModel.prototype.click = function (listItem) {
        this._itemClicked.invoke(listItem);
    };
    return UnorderedListViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.UnorderedListViewModel = UnorderedListViewModel;
//# sourceMappingURL=UnorderedListViewModel.js.map