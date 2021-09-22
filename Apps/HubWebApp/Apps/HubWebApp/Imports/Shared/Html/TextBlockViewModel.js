"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TextBlockViewModel = void 0;
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./TextBlock.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ko = require("knockout");
var TextBlockViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(TextBlockViewModel, _super);
    function TextBlockViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('text-block', template)) || this;
        _this.text = ko.observable('');
        return _this;
    }
    return TextBlockViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.TextBlockViewModel = TextBlockViewModel;
//# sourceMappingURL=TextBlockViewModel.js.map