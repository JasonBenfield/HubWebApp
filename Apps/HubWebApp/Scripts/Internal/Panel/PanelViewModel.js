"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PanelViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./Panel.html");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var PanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(PanelViewModel, _super);
    function PanelViewModel(content) {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('panel', template)) || this;
        _this.content = content;
        _this.isActive = ko.observable(false);
        return _this;
    }
    return PanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.PanelViewModel = PanelViewModel;
//# sourceMappingURL=PanelViewModel.js.map