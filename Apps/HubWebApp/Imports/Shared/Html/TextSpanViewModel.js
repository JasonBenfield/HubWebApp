"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TextSpanViewModel = void 0;
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./TextSpan.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ko = require("knockout");
var TextSpanViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(TextSpanViewModel, _super);
    function TextSpanViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('text-span', template)) || this;
        _this.text = ko.observable('');
        return _this;
    }
    return TextSpanViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.TextSpanViewModel = TextSpanViewModel;
//# sourceMappingURL=TextSpanViewModel.js.map