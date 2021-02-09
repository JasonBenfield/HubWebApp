"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var template = require("./ListItem.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var ListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ListItemViewModel, _super);
    function ListItemViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('list-item', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        _this.isClickable = false;
        return _this;
    }
    return ListItemViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.ListItemViewModel = ListItemViewModel;
//# sourceMappingURL=ListItemViewModel.js.map