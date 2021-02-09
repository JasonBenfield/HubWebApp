"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./Heading5.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var Heading5ViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(Heading5ViewModel, _super);
    function Heading5ViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('heading5', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        return _this;
    }
    return Heading5ViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.Heading5ViewModel = Heading5ViewModel;
//# sourceMappingURL=Heading5ViewModel.js.map