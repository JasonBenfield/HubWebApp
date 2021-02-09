"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./Heading1.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var Heading1ViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(Heading1ViewModel, _super);
    function Heading1ViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('heading1', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        return _this;
    }
    return Heading1ViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.Heading1ViewModel = Heading1ViewModel;
//# sourceMappingURL=Heading1ViewModel.js.map