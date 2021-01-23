"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ListCardViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var Alert_1 = require("XtiShared/Alert");
var template = require("./ListCard.html");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ListCardViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ListCardViewModel, _super);
    function ListCardViewModel(componentTemplate) {
        if (componentTemplate === void 0) { componentTemplate = null; }
        var _this = _super.call(this, componentTemplate || new ComponentTemplate_1.ComponentTemplate('list-card', template)) || this;
        _this.alert = new Alert_1.AlertViewModel();
        _this.title = ko.observable('');
        _this.items = ko.observableArray([]);
        _this.hasItems = ko.observable(false);
        return _this;
    }
    return ListCardViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ListCardViewModel = ListCardViewModel;
//# sourceMappingURL=ListCardViewModel.js.map