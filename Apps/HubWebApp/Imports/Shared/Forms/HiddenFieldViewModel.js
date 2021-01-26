"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ko = require("knockout");
var ComponentViewModel_1 = require("../ComponentViewModel");
var template = require("./HiddenField.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var HiddenFieldViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(HiddenFieldViewModel, _super);
    function HiddenFieldViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('hidden-field', template)) || this;
        _this.name = ko.observable('');
        _this.value = ko.observable(null);
        return _this;
    }
    return HiddenFieldViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.HiddenFieldViewModel = HiddenFieldViewModel;
//# sourceMappingURL=HiddenFieldViewModel.js.map