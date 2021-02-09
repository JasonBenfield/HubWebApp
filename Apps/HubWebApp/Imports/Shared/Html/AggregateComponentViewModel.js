"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("../ComponentTemplate");
var ComponentViewModel_1 = require("../ComponentViewModel");
var template = require("./AggregateComponent.html");
var ko = require("knockout");
var AggregateComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(AggregateComponentViewModel, _super);
    function AggregateComponentViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('aggregate-component', template)) || this;
        _this.name = ko.observable('');
        _this.items = ko.observableArray([]);
        _this.isVisible = ko.observable(true);
        return _this;
    }
    return AggregateComponentViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.AggregateComponentViewModel = AggregateComponentViewModel;
//# sourceMappingURL=AggregateComponentViewModel.js.map