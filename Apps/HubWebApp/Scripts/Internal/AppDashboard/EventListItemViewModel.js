"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EventListItemViewModel = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var template = require("./EventListItem.html");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var EventListItemViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(EventListItemViewModel, _super);
    function EventListItemViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('event-list-item', template)) || this;
        _this.timeOccurred = ko.observable('');
        _this.severity = ko.observable('');
        _this.caption = ko.observable('');
        _this.message = ko.observable('');
        return _this;
    }
    return EventListItemViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.EventListItemViewModel = EventListItemViewModel;
//# sourceMappingURL=EventListItemViewModel.js.map