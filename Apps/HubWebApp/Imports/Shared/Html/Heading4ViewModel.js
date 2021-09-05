"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Heading4ViewModel = void 0;
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./Heading4.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var Heading4ViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(Heading4ViewModel, _super);
    function Heading4ViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('heading4', template)) || this;
        _this.content = new AggregateComponentViewModel_1.AggregateComponentViewModel();
        return _this;
    }
    return Heading4ViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.Heading4ViewModel = Heading4ViewModel;
//# sourceMappingURL=Heading4ViewModel.js.map